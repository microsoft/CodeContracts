// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace System.Compiler{
  /* Specializer walks an IR, replacing references to type parameters with references to actual types.
   * The main complication is that structural types involving type parameters need to be reconstructed.
   * Other complications arise from the fact that IL is not orthogonal and requires different instructions
   * to be used depending on whether a type is a reference type or a value type. In templates, type parameters
   * are treated as reference types when method bodies are generated. In order to instantiate a template with
   * a value type argument, it is necessary to walk the method bodies and transform some expressions. This is
   * not possible to do in a single pass because method bodies can contain references to signatures defined
   * in parts of the IR that have not yet been visited and specialized. Consequently, Specializer ignores
   * method bodies.
   * 
   * Once all signatures have been fixed up by Specializer, it is necessary to use MethodBodySpecializer
   * to walk the method bodies and fix up the IL to deal with value types that replaced type parameters.
   * Another complication to deal with is that MemberBindings and NameBindings can refer to members
   * defined in structural types based on type parameters. These must be substituted with references to the
   * corresponding members of structural types based on the type arguments. Note that some structural types
   * are themselves implemented as templates.
   */

  /// <summary>
  /// This class specializes a normalized IR by replacing type parameters with type arguments.
  /// </summary>
  public class Specializer : StandardVisitor{
    public TypeNodeList pars;
    public TypeNodeList args;
    public Method CurrentMethod;
    public TypeNode CurrentType;
    public Module TargetModule;
    private TrivialHashtable forwarding = new TrivialHashtable();

    public Specializer(Module targetModule, TypeNodeList pars, TypeNodeList args){
      Debug.Assert(pars != null && pars.Count > 0);
      Debug.Assert(args != null && args.Count > 0);
      this.pars = pars;
      this.args = args;
      this.TargetModule = targetModule;
    }
    public Specializer(Visitor callingVisitor)
      : base(callingVisitor){
    }
    public override void TransferStateTo(Visitor targetVisitor){
      base.TransferStateTo(targetVisitor);
      Specializer target = targetVisitor as Specializer;
      if (target == null) return;
      target.args = this.args;
      target.pars = this.pars;
      target.CurrentMethod = this.CurrentMethod;
      target.CurrentType = this.CurrentType;
    }
    public override DelegateNode VisitDelegateNode(DelegateNode delegateNode){
      return this.VisitTypeNode(delegateNode) as DelegateNode;
    }
    public override Interface VisitInterfaceReference(Interface Interface) {
      return this.VisitTypeReference(Interface) as Interface;
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding)
    {
        var result = base.VisitMemberBinding(memberBinding);
        var mb = result as MemberBinding;
        if (mb != null)
        {
            mb.BoundMember = this.VisitMemberReference(mb.BoundMember);
        }
        return result;
    }
    public virtual Member VisitMemberReference(Member member) {
      if (member == null) return null;
      var type = member as TypeNode;
      if (type != null) return this.VisitTypeReference(type);

      Method method = member as Method;
      if (method != null && method.Template != null && method.TemplateArguments != null && method.TemplateArguments.Count > 0)
      {
        Method template = this.VisitMemberReference(method.Template) as Method;
        // Assertion is wrong as declaring type could be instantiated and specialized. Debug.Assert(template == method.Template);
        bool needNewInstance = template != null && template != method.Template;
        TypeNodeList args = method.TemplateArguments.Clone();
        for (int i = 0, n = args.Count; i < n; i++)
        {
          TypeNode arg = this.VisitTypeReference(args[i]);
          if (arg != null && arg != args[i])
          {
            args[i] = arg;
            needNewInstance = true;
          }
        }
        if (needNewInstance)
        {
          //^ assert template != null;
          return template.GetTemplateInstance(this.CurrentType, args);
        }
        return method;
      }
      TypeNode specializedType = this.VisitTypeReference(member.DeclaringType);
      if (specializedType == member.DeclaringType || specializedType == null) return member;
      return Specializer.GetCorrespondingMember(member, specializedType);
    }
    public static Member GetCorrespondingMember (Member/*!*/ member, TypeNode/*!*/ specializedType) {
      //member belongs to a structural type based on a type parameter.
      //return the corresponding member from the structural type based on the type argument.
      if (member.DeclaringType == null) { Debug.Fail(""); return null; }
      MemberList unspecializedMembers = member.DeclaringType.Members;
      MemberList specializedMembers = specializedType.Members;
      if (unspecializedMembers == null || specializedMembers == null) { Debug.Assert(false); return null; }
      int unspecializedOffset = 0;
      int specializedOffset = 0;
      //The offsets can become > 0 when the unspecialized type and/or specialized type is imported from another assembly 
      //(and the unspecialized type is in fact a partially specialized type.)
      for (int i = 0, n = specializedMembers == null ? 0 : specializedMembers.Count; i < n; i++) {
        Member unspecializedMember = unspecializedMembers[i-unspecializedOffset];
        Member specializedMember = specializedMembers[i-specializedOffset];
        if (unspecializedMember == member) {
          Debug.Assert(specializedMember != null);
          return specializedMember;
        }
      }
      Debug.Assert(false);
      return null;
    }
    public readonly Block DummyBody = new Block();
    /// <summary>
    /// Called in 2 circumstances
    /// 1. Specializing a method because the parent type is instantiated. In this case, the method template
    ///    parameters if any, are not yet copied and need to be copied while keeping sharing alive. The copy
    ///    is necessary when the type parameter has interface constraints or base type constraints that are
    ///    being changed due to instantiation.
    ///    
    /// 2. Specializing when we instantiate a generic method itself. In this case, the template parameter list
    ///    of the method is null, so no template parameter copying is done (or necessary).
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public override Method VisitMethod(Method method) {
      if (method == null) return null;
      Method savedCurrentMethod = this.CurrentMethod;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentMethod = method;
      this.CurrentType = method.DeclaringType;

      // may need to copy the generic template parameters on this method and consistently substitute them
      // so we have to do this first.
      if (TargetPlatform.UseGenerics && this.args != method.TemplateArguments)
      {
          method.TemplateParameters = this.FreshTypeParameterListIfNecessary(method.TemplateParameters);
          method.TemplateArguments = this.VisitTypeReferenceList(method.TemplateArguments);
      }
      
      method.ThisParameter = (This)this.VisitThis(method.ThisParameter);
      method.Attributes = this.VisitAttributeList(method.Attributes);
      method.ReturnAttributes = this.VisitAttributeList(method.ReturnAttributes);
      method.SecurityAttributes = this.VisitSecurityAttributeList(method.SecurityAttributes);
      method.ReturnType = this.VisitTypeReference(method.ReturnType);
      method.Parameters = this.VisitParameterList(method.Parameters);
      if (method.contract != null)
      {
        method.contract = this.VisitMethodContract(method.contract);
      }
      else if (method.ProvideContract != null && method.ProviderHandle != null)
      {
        // delay 
        var origContractProvider = method.ProvideContract;
        method.ProvideContract = (mHandle, oHandle) => { 
          origContractProvider(mHandle, oHandle);
          var savedCurrentMethod2 = this.CurrentMethod;
          var savedCurrentType2 = this.CurrentType;
          this.CurrentMethod = mHandle;
          this.CurrentType = mHandle.DeclaringType;
          this.VisitMethodContract(mHandle.contract);
          this.CurrentType = savedCurrentType2;
          this.CurrentMethod = savedCurrentMethod2;
        }; 
      }
      method.ImplementedInterfaceMethods = this.VisitMethodList(method.ImplementedInterfaceMethods);
      this.CurrentMethod = savedCurrentMethod;
      this.CurrentType = savedCurrentType;
      return method;
    }

    /// <summary>
    /// Assumes the list itself was cloned by the duplicator.
    /// </summary>
    private TypeNodeList FreshTypeParameterListIfNecessary(TypeNodeList typeNodeList)
    {
        if (typeNodeList == null) return null;

        for (int i = 0; i < typeNodeList.Count; i++)
        {
            var tp = typeNodeList[i];
            if (tp == null) continue;
            if (tp.Interfaces != null && tp.Interfaces.Count > 0 || tp is ClassParameter)
            {
                typeNodeList[i] = FreshTypeParameterIfNecessary(tp);
            }
        }
        return this.VisitTypeParameterList(typeNodeList);
    }

    private TypeNode FreshTypeParameterIfNecessary(TypeNode typeParameter)
    {
        Contract.Ensures(typeParameter == null ||
          ((ITypeParameter)typeParameter).ParameterListIndex == ((ITypeParameter)Contract.Result<TypeNode>()).ParameterListIndex);

        if (typeParameter == null) return null;

        var cp = typeParameter as ClassParameter;
        if (cp != null)
        {
            if (cp.BaseClass == SystemTypes.Object && (cp.Interfaces == null || cp.Interfaces.Count == 0))
            {
                // no instantiation change possible
                return typeParameter;
            }
            return CopyTypeParameter(cp);
        }
        InterfaceList interfaces = typeParameter.Interfaces;
        if (interfaces == null || interfaces.Count == 0) return typeParameter;
        TypeNode baseType = this.VisitTypeReference(interfaces[0]);
        if (baseType is Interface)
        {
            return CopyTypeParameter(typeParameter);
        }
        // turn into class parameter
        return this.ConvertToClassParameter(baseType, typeParameter);
    }

    private TypeNode CopyTypeParameter(TypeNode typeParameter)
    {
        var fresh = (TypeNode)typeParameter.Clone();
        fresh.Interfaces = fresh.Interfaces.Clone();
        this.forwarding[typeParameter.UniqueKey] = fresh;
        return fresh;
    }
    private TypeNode ConvertToClassParameter(TypeNode baseType, TypeNode/*!*/ typeParameter)
    {
        ClassParameter result;
        if (typeParameter is MethodTypeParameter)
        {
            result = new MethodClassParameter();
        }
        else if (typeParameter is TypeParameter)
        {
            result = new ClassParameter();
            result.DeclaringType = typeParameter.DeclaringType;
        }
        else
            return typeParameter; //give up
        result.SourceContext = typeParameter.SourceContext;
        result.TypeParameterFlags = ((ITypeParameter)typeParameter).TypeParameterFlags;
        result.ParameterListIndex = ((ITypeParameter)typeParameter).ParameterListIndex;
        result.Name = typeParameter.Name;
        result.Namespace = StandardIds.ClassParameter;
        result.BaseClass = baseType is Class ? (Class)baseType : CoreSystemTypes.Object;
        result.DeclaringMember = ((ITypeParameter)typeParameter).DeclaringMember;
        result.DeclaringModule = typeParameter.DeclaringModule;
        result.Flags = typeParameter.Flags & ~TypeFlags.Interface;
        InterfaceList constraints = result.Interfaces = new InterfaceList();
        InterfaceList interfaces = typeParameter.Interfaces;
        for (int i = 1, n = interfaces == null ? 0 : interfaces.Count; i < n; i++)
        {
            //^ assert interfaces != null;
            constraints.Add(interfaces[i]);
        }
        this.forwarding[typeParameter.UniqueKey] = result;
        return result;
    }

    public override MethodContract VisitMethodContract(MethodContract contract){
      if (contract == null) return null;
      var specializer = this as MethodBodySpecializer;
      if (specializer == null)
      {
        specializer = contract.DeclaringMethod.DeclaringType.DeclaringModule.GetMethodBodySpecializer(this.pars, this.args);
        specializer.CurrentMethod = this.CurrentMethod;
        specializer.CurrentType = this.CurrentType;
      }
      contract.contractInitializer = specializer.VisitBlock(contract.contractInitializer);
      contract.postPreamble = specializer.VisitBlock(contract.postPreamble);
      contract.ensures = specializer.VisitEnsuresList(contract.ensures);
      contract.asyncEnsures = specializer.VisitEnsuresList(contract.asyncEnsures);
      contract.modelEnsures = specializer.VisitEnsuresList(contract.modelEnsures);
      contract.modifies = specializer.VisitExpressionList(contract.modifies);
      contract.requires = specializer.VisitRequiresList(contract.requires);
      return contract;
    }
    public virtual object VisitContractPart(Method method, object part) {
      if (method == null) { Debug.Fail("method == null"); return part; }
      this.CurrentMethod = method;
      this.CurrentType = method.DeclaringType;
      EnsuresList es = part as EnsuresList;
      if (es != null) return this.VisitEnsuresList(es);
      RequiresList rs = part as RequiresList;
      if (rs != null) return this.VisitRequiresList(rs);
      Block initializer = part as Block;
      if (initializer != null) return this.VisitBlock(initializer);
      return part;
    }
    public virtual MethodList VisitMethodList(MethodList methods){
      if (methods == null) return null;
      int n = methods.Count;
      for (int i = 0; i < n; i++)
        methods[i] = (Method)this.VisitMemberReference(methods[i]);
      return methods;
    }
    public override TypeNode VisitTypeNode(TypeNode typeNode){
      if (typeNode == null) return null;
      TypeNode savedCurrentType = this.CurrentType;
      if (savedCurrentType != null && savedCurrentType.TemplateArguments != null && savedCurrentType.TemplateArguments.Count > 0 &&
        typeNode.Template != null && (typeNode.Template.TemplateParameters == null || typeNode.Template.TemplateParameters.Count == 0)){
        typeNode.TemplateArguments = new TypeNodeList(0);
      }
      this.CurrentType = typeNode;
      if (typeNode.ProvideTypeMembers != null && /*typeNode.ProvideNestedTypes != null &&*/ typeNode.ProviderHandle != null){
        typeNode.members = null;
        typeNode.ProviderHandle = new SpecializerHandle(typeNode.ProvideNestedTypes, typeNode.ProvideTypeMembers, typeNode.ProvideTypeSignature, typeNode.ProvideTypeAttributes, typeNode.ProviderHandle);
        typeNode.ProvideNestedTypes = new TypeNode.NestedTypeProvider(this.ProvideNestedTypes);
        typeNode.ProvideTypeMembers = new TypeNode.TypeMemberProvider(this.ProvideTypeMembers);
        typeNode.ProvideTypeAttributes = new TypeNode.TypeAttributeProvider(this.ProvideTypeAttributes);
        typeNode.ProvideTypeSignature = new TypeNode.TypeSignatureProvider(this.ProvideTypeSignature);
        DelegateNode delegateNode = typeNode as DelegateNode;
        if (delegateNode != null){
          if (!delegateNode.IsNormalized){ //In the Normalized case Parameters are retrieved from the Invoke method, which means evaluating Members
            delegateNode.Parameters = this.VisitParameterList(delegateNode.Parameters);
            delegateNode.ReturnType = this.VisitTypeReference(delegateNode.ReturnType);
          }
        }
      }else{
        typeNode.Attributes = this.VisitAttributeList(typeNode.Attributes);
        typeNode.SecurityAttributes = this.VisitSecurityAttributeList(typeNode.SecurityAttributes);
        Class c = typeNode as Class;
        if (c != null) c.BaseClass = (Class)this.VisitTypeReference(c.BaseClass);
        typeNode.Interfaces = this.VisitInterfaceReferenceList(typeNode.Interfaces);
        typeNode.Members = this.VisitMemberList(typeNode.Members);
        DelegateNode delegateNode = typeNode as DelegateNode;
        if (delegateNode != null){
          delegateNode.Parameters = this.VisitParameterList(delegateNode.Parameters);
          delegateNode.ReturnType = this.VisitTypeReference(delegateNode.ReturnType);
        }
      }
      this.CurrentType = savedCurrentType;
      return typeNode;
    }
    private void ProvideTypeAttributes(TypeNode typeNode, object handle)
    {
      SpecializerHandle sHandler = (SpecializerHandle)handle;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentType = typeNode;
      if (sHandler.TypeAttributeProvider != null)
      {
        sHandler.TypeAttributeProvider(typeNode, sHandler.Handle);
      }
      typeNode.Attributes = this.VisitAttributeList(typeNode.Attributes);
      typeNode.SecurityAttributes = this.VisitSecurityAttributeList(typeNode.SecurityAttributes);

      this.CurrentType = savedCurrentType;
    }
    private void ProvideTypeSignature(TypeNode typeNode, object handle)
    {
      SpecializerHandle sHandler = (SpecializerHandle)handle;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentType = typeNode;
      if (sHandler.TypeSignatureProvider != null)
      {
        sHandler.TypeSignatureProvider(typeNode, sHandler.Handle);
      }
      Class c = typeNode as Class;
      if (c != null) c.BaseClass = (Class)this.VisitTypeReference(c.BaseClass);
      typeNode.Interfaces = this.VisitInterfaceReferenceList(typeNode.Interfaces);

      this.CurrentType = savedCurrentType;
    }
    private void ProvideNestedTypes(TypeNode/*!*/ typeNode, object/*!*/ handle) {
      SpecializerHandle sHandler = (SpecializerHandle)handle;
      if (sHandler.NestedTypeProvider == null) return;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentType = typeNode;
      sHandler.NestedTypeProvider(typeNode, sHandler.Handle);
      TypeNodeList nestedTypes = typeNode.nestedTypes;
      for (int i = 0, n = nestedTypes == null ? 0 : nestedTypes.Count; i < n; i++) {
        //^ assert nestedTypes != null;
        TypeNode nt = nestedTypes[i];
        if (nt == null) continue;
        this.VisitTypeNode(nt);
      }
      this.CurrentType = savedCurrentType;
    }
    private void ProvideTypeMembers(TypeNode/*!*/ typeNode, object/*!*/ handle) {
      SpecializerHandle sHandler = (SpecializerHandle)handle;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentType = typeNode;
      sHandler.TypeMemberProvider(typeNode, sHandler.Handle);
      typeNode.Members = this.VisitMemberList(typeNode.Members);
      DelegateNode delegateNode = typeNode as DelegateNode;
      if (delegateNode != null && delegateNode.IsNormalized){
        delegateNode.Parameters = this.VisitParameterList(delegateNode.Parameters);
        delegateNode.ReturnType = this.VisitTypeReference(delegateNode.ReturnType);
      }
      this.CurrentType = savedCurrentType;
    }
    internal class SpecializerHandle{
      readonly internal TypeNode.NestedTypeProvider/*!*/ NestedTypeProvider;
      readonly internal TypeNode.TypeMemberProvider/*!*/ TypeMemberProvider;
      readonly internal TypeNode.TypeSignatureProvider TypeSignatureProvider;
      readonly internal TypeNode.TypeAttributeProvider TypeAttributeProvider;
      readonly internal object/*!*/ Handle;
      internal SpecializerHandle(TypeNode.NestedTypeProvider/*!*/ nestedTypeProvider, TypeNode.TypeMemberProvider/*!*/ typeMemberProvider, TypeNode.TypeSignatureProvider typeSignatureProvider, TypeNode.TypeAttributeProvider typeAttributeProvider, object/*!*/ handle) {
        this.NestedTypeProvider = nestedTypeProvider;
        this.TypeMemberProvider = typeMemberProvider;
        this.TypeSignatureProvider = typeSignatureProvider;
        this.TypeAttributeProvider = typeAttributeProvider;
        this.Handle = handle;
        //^ base();
      }
    }
    public virtual Expression VisitTypeExpression(Expression expr){
      TypeNodeList pars = this.pars;
      TypeNodeList args = this.args;
      Identifier id = expr as Identifier;
      if (id != null){
        int key = id.UniqueIdKey;
        for (int i = 0, n = pars == null ? 0 : pars.Count, m = args == null ? 0 : args.Count; i < n && i < m; i++){
          //^ assert pars != null && args != null;
          TypeNode par = pars[i];
          if (par == null || par.Name == null) continue;
          if (par.Name.UniqueIdKey == key) return new Literal(args[i], CoreSystemTypes.Type);
        }
        return id;
      }
      return expr;
    }
    public override TypeNode VisitTypeParameter(TypeNode typeParameter){
      Contract.Ensures(typeParameter == null ||
        ((ITypeParameter)typeParameter).ParameterListIndex == ((ITypeParameter)Contract.Result<TypeNode>()).ParameterListIndex);

      return base.VisitTypeParameter(typeParameter);
    }
    public override TypeNode VisitTypeReference(TypeNode type){ //TODO: break up this method
      if (type == null) return null;
      TypeNodeList pars = this.pars;
      TypeNodeList args = this.args;
      switch (type.NodeType){
        case NodeType.ArrayType:
          ArrayType arrType = (ArrayType)type;
          TypeNode elemType = this.VisitTypeReference(arrType.ElementType);
          if (elemType == arrType.ElementType || elemType == null) return arrType;
          if (arrType.IsSzArray()) return elemType.GetArrayType(1);
          return elemType.GetArrayType(arrType.Rank, arrType.Sizes, arrType.LowerBounds);
        case NodeType.DelegateNode:{
          FunctionType ftype = type as FunctionType;
          if (ftype == null) goto default;
          TypeNode referringType = ftype.DeclaringType == null ? this.CurrentType : this.VisitTypeReference(ftype.DeclaringType);
          return FunctionType.For(this.VisitTypeReference(ftype.ReturnType), this.VisitParameterList(ftype.Parameters), referringType);}
        case NodeType.Pointer:
          Pointer pType = (Pointer)type;
          elemType = this.VisitTypeReference(pType.ElementType);
          if (elemType == pType.ElementType || elemType == null) return pType;
          return elemType.GetPointerType();
        case NodeType.Reference:
          Reference rType = (Reference)type;
          elemType = this.VisitTypeReference(rType.ElementType);
          if (elemType == rType.ElementType || elemType == null) return rType;
          return elemType.GetReferenceType();
        case NodeType.ArrayTypeExpression:
          ArrayTypeExpression aExpr = (ArrayTypeExpression)type;
          aExpr.ElementType = this.VisitTypeReference(aExpr.ElementType);
          return aExpr;
        case NodeType.BoxedTypeExpression:
          BoxedTypeExpression bExpr = (BoxedTypeExpression)type;
          bExpr.ElementType = this.VisitTypeReference(bExpr.ElementType);
          return bExpr;
        case NodeType.ClassExpression:{
          ClassExpression cExpr = (ClassExpression)type;
          cExpr.Expression = this.VisitTypeExpression(cExpr.Expression);
          Literal lit = cExpr.Expression as Literal; //Could happen if the expression is a template parameter
          if (lit != null) return lit.Value as TypeNode;
          cExpr.TemplateArguments = this.VisitTypeReferenceList(cExpr.TemplateArguments);
          return cExpr;}
        case NodeType.ClassParameter:
        case NodeType.TypeParameter:
          int key = type.UniqueKey;
          var mappedTarget = this.forwarding[key] as TypeNode;
          if (mappedTarget != null) return mappedTarget;
          for (int i = 0, n = pars == null ? 0 : pars.Count, m = args == null ? 0 : args.Count; i < n && i < m; i++){
            //^ assert pars != null && args != null;
            TypeNode tp = pars[i];
            if (tp == null) continue;
            if (tp.UniqueKey == key) return args[i];
          }
          return type;
        case NodeType.FlexArrayTypeExpression:
          FlexArrayTypeExpression flExpr = (FlexArrayTypeExpression)type;
          flExpr.ElementType = this.VisitTypeReference(flExpr.ElementType);
          return flExpr;
        case NodeType.FunctionTypeExpression:
          FunctionTypeExpression ftExpr = (FunctionTypeExpression)type;
          ftExpr.Parameters = this.VisitParameterList(ftExpr.Parameters);
          ftExpr.ReturnType = this.VisitTypeReference(ftExpr.ReturnType);
          return ftExpr;
        case NodeType.InvariantTypeExpression:
          InvariantTypeExpression invExpr = (InvariantTypeExpression)type;
          invExpr.ElementType = this.VisitTypeReference(invExpr.ElementType);
          return invExpr;
        case NodeType.InterfaceExpression:
          InterfaceExpression iExpr = (InterfaceExpression)type;
          if (iExpr.Expression == null) goto default;
          iExpr.Expression = this.VisitTypeExpression(iExpr.Expression);
          iExpr.TemplateArguments = this.VisitTypeReferenceList(iExpr.TemplateArguments);
          return iExpr;
        case NodeType.NonEmptyStreamTypeExpression:
          NonEmptyStreamTypeExpression neExpr = (NonEmptyStreamTypeExpression)type;
          neExpr.ElementType = this.VisitTypeReference(neExpr.ElementType);
          return neExpr;
        case NodeType.NonNullTypeExpression:
          NonNullTypeExpression nnExpr = (NonNullTypeExpression)type;
          nnExpr.ElementType = this.VisitTypeReference(nnExpr.ElementType);
          return nnExpr;
        case NodeType.NonNullableTypeExpression:
          NonNullableTypeExpression nbExpr = (NonNullableTypeExpression)type;
          nbExpr.ElementType = this.VisitTypeReference(nbExpr.ElementType);
          return nbExpr;
        case NodeType.NullableTypeExpression:
          NullableTypeExpression nuExpr = (NullableTypeExpression)type;
          nuExpr.ElementType = this.VisitTypeReference(nuExpr.ElementType);
          return nuExpr;
        case NodeType.OptionalModifier:{
          TypeModifier modType = (TypeModifier)type;
          TypeNode modifiedType = this.VisitTypeReference(modType.ModifiedType);
          TypeNode modifierType = this.VisitTypeReference(modType.Modifier);
          if (modifiedType == null || modifierType == null) { return type; }
          return OptionalModifier.For(modifierType, modifiedType);
        }
        case NodeType.RequiredModifier:{
          TypeModifier modType = (TypeModifier)type;
          TypeNode modifiedType = this.VisitTypeReference(modType.ModifiedType);
          TypeNode modifierType = this.VisitTypeReference(modType.Modifier);
          if (modifiedType == null || modifierType == null) { Debug.Fail(""); return type; }
          return RequiredModifier.For(modifierType, modifiedType);
        }
        default:
          if (type.Template != null)
          {
            Debug.Assert(TypeNode.IsCompleteTemplate(type.Template));
            // map consolidated arguments
            bool mustSpecializeFurther = false;
            TypeNodeList targs = type.ConsolidatedTemplateArguments;
            int numArgs = targs == null ? 0 : targs.Count;
            if (targs != null)
            {
              targs = targs.Clone();
              for (int i = 0; i < numArgs; i++)
              {
                TypeNode targ = targs[i];
                targs[i] = this.VisitTypeReference(targ);
                if (targ != targs[i]) { mustSpecializeFurther = true; }
              }
            }
            if (targs == null || !mustSpecializeFurther) return type;
            TypeNode t = type.Template.GetGenericTemplateInstance(this.TargetModule, targs);
            return t;
          }
          return type;
      }
    }
  }

  public class MethodBodySpecializer : Specializer{
    public TrivialHashtable/*!*/ alreadyVisitedNodes = new TrivialHashtable();
    public Method methodBeingSpecialized;
    public Method dummyMethod;

    public MethodBodySpecializer(Module module, TypeNodeList pars, TypeNodeList args)
      : base(module, pars, args){
      //^ base;
    }
    public MethodBodySpecializer(Visitor callingVisitor)
      : base(callingVisitor){
      //^ base;
    }
    public override Node Visit(Node node){
      Literal lit = node as Literal;
      if (lit != null && lit.Value == null) return lit;
      Expression e = node as Expression;
      if (e != null && !(e is Local || e is Parameter))
        e.Type = this.VisitTypeReference(e.Type);
      return base.Visit(node);
    }

    public override Expression VisitAddressDereference(AddressDereference addr){
      if (addr == null) return null;
      bool unboxDeref = addr.Address != null && addr.Address.NodeType == NodeType.Unbox;
      addr.Address = this.VisitExpression(addr.Address);
      if (addr.Address == null) return null;
      if (unboxDeref && addr.Address.NodeType != NodeType.Unbox) return addr.Address;
      Reference reference = addr.Address.Type as Reference;
      if (reference != null) addr.Type = reference.ElementType;
      return addr;
    }
    public override Statement VisitAssignmentStatement(AssignmentStatement assignment){
      assignment = (AssignmentStatement)base.VisitAssignmentStatement(assignment);
      if (assignment == null) return null;
      Expression target = assignment.Target;
      Expression source = assignment.Source;
      TypeNode tType = target == null ? null : target.Type;
      TypeNode sType = source == null ? null : source.Type;
      if (tType != null && sType != null){
        //^ assert target != null;
        if (tType.IsValueType){
          if (sType is Reference)
            assignment.Source = new AddressDereference(source, tType);
          else if (!sType.IsValueType && !(sType == CoreSystemTypes.Object && source is Literal && target.NodeType == NodeType.AddressDereference))
            assignment.Source = new AddressDereference(new BinaryExpression(source, new MemberBinding(null, sType), NodeType.Unbox), sType);
        }else{
          if (sType.IsValueType){
            if (!(tType is Reference))
              assignment.Source = new BinaryExpression(source, new MemberBinding(null, sType), NodeType.Box, tType);
          }
        }
      }
      return assignment;
    }
    public override Expression VisitBinaryExpression(BinaryExpression binaryExpression){
      if (binaryExpression == null) return null;
      bool opnd1IsInst = binaryExpression.Operand1 != null && binaryExpression.Operand1.NodeType == NodeType.Isinst;
      binaryExpression = (BinaryExpression)base.VisitBinaryExpression(binaryExpression);
      if (binaryExpression == null) return null;
      Expression opnd1 = binaryExpression.Operand1;
      Expression opnd2 = binaryExpression.Operand2;
      Literal lit = opnd2 as Literal;
      TypeNode t = lit == null ? null : lit.Value as TypeNode;
      if (binaryExpression.NodeType == NodeType.Castclass /*|| binaryExpression.NodeType == NodeType.ExplicitCoercion*/){
        //See if castclass must become box or unbox
        if (t != null){
          if (t.IsValueType){
            AddressDereference adref = new AddressDereference(new BinaryExpression(opnd1, lit, NodeType.Unbox), t);
            adref.Type = t;
            return adref;
          }
          if (opnd1 != null && opnd1.Type != null && opnd1.Type.IsValueType){
            return new BinaryExpression(opnd1, new MemberBinding(null, opnd1.Type), NodeType.Box, t);
          }
        }
      }else if (binaryExpression.NodeType == NodeType.Unbox){
        if (opnd1 != null && opnd1.Type != null && opnd1.Type.IsValueType)
          return opnd1;

      }else if (binaryExpression.NodeType == NodeType.Eq) {
        //For value types, turn comparisons against null into false
        if (lit != null && lit.Value == null && opnd1 != null && opnd1.Type != null && opnd1.Type.IsValueType)
          return Literal.False;
        lit = opnd1 as Literal;
        if (lit != null && lit.Value == null && opnd2 != null && opnd2.Type != null && opnd2.Type.IsValueType)
          return Literal.False;
      }else if (binaryExpression.NodeType == NodeType.Ne){
        //For value types, turn comparisons against null into true
        if (lit != null && lit.Value == null && opnd1 != null && opnd1.Type != null && opnd1.Type.IsValueType){
          if (opnd1IsInst && opnd1.Type == CoreSystemTypes.Boolean) return opnd1;
          return Literal.True;
        }
        lit = opnd1 as Literal;
        if (lit != null && lit.Value == null && opnd2 != null && opnd2.Type != null && opnd2.Type.IsValueType)
          return Literal.True;
      }else if (binaryExpression.NodeType == NodeType.Isinst){
        //Do not emit isinst instruction if opnd1 is a value type.
        if (opnd1 != null && opnd1.Type != null && opnd1.Type.IsValueType){
          if (opnd1.Type == t)
            return Literal.True;
          else
            return Literal.False;
        }
      }
      return binaryExpression;
    }
    public override Statement VisitBranch(Branch branch){
      branch = (Branch)base.VisitBranch(branch);
      if (branch == null) return null;
      if (branch.Condition != null && !(branch.Condition is BinaryExpression)){
        //Deal with implicit comparisons against null
        TypeNode ct = branch.Condition.Type;
        if (ct != null && !ct.IsPrimitiveInteger && ct != CoreSystemTypes.Boolean && ct.IsValueType){
          if (branch.Condition.NodeType == NodeType.LogicalNot)
            return null;
          branch.Condition = null;
        }
      }
      return branch;
    }
    public override Expression VisitExpression(Expression expression){
      if (expression == null) return null;
      switch(expression.NodeType){
        case NodeType.Dup: 
        case NodeType.Arglist:
          expression.Type = this.VisitTypeReference(expression.Type);
          return expression;
        case NodeType.Pop:
          expression.Type = this.VisitTypeReference(expression.Type);
          UnaryExpression uex = expression as UnaryExpression;
          if (uex != null){
            uex.Operand = this.VisitExpression(uex.Operand);
            return uex;
          }
          return expression;
        default:
          return (Expression)this.Visit(expression);
      }
    }
    public override Expression VisitIndexer(Indexer indexer){
      indexer = (Indexer)base.VisitIndexer(indexer);
      if (indexer == null || indexer.Object == null) return null;
      ArrayType arrType = indexer.Object.Type as ArrayType;
      if (arrType != null) indexer.Type = indexer.ElementType = arrType.ElementType;
      else
        indexer.ElementType = this.VisitTypeReference(indexer.ElementType);
      
      //if (elemType != null && elemType.IsValueType && !elemType.IsPrimitive)
        //return new AddressDereference(new UnaryExpression(indexer, NodeType.AddressOf), elemType);
      return indexer;
    }
    public override Expression VisitLiteral(Literal literal){
      if (literal == null) return null;
      TypeNode t = literal.Value as TypeNode;
      if (t != null && literal.Type == CoreSystemTypes.Type)
        return new Literal(this.VisitTypeReference(t), literal.Type, literal.SourceContext);
      return (Literal)literal.Clone();
    }
    public override Expression VisitLocal(Local local){
      if (local == null) return null;
      if (this.alreadyVisitedNodes[local.UniqueKey] != null) return local;
      this.alreadyVisitedNodes[local.UniqueKey] = local;
      return base.VisitLocal(local);
    }
    public override Expression VisitParameter(Parameter parameter){
      ParameterBinding pb = parameter as ParameterBinding;
      if (pb != null && pb.BoundParameter != null)
        pb.Type = pb.BoundParameter.Type;
      return parameter;
    }
    public override Expression VisitMemberBinding(MemberBinding memberBinding){
      if (memberBinding == null) return null;
      Expression tObj = memberBinding.TargetObject = this.VisitExpression(memberBinding.TargetObject);
      Member mem = this.VisitMemberReference(memberBinding.BoundMember);
      if (mem == this.dummyMethod)
        mem = this.methodBeingSpecialized;
      Debug.Assert(mem != null);
      memberBinding.BoundMember = mem;
      if (memberBinding == null) return null;
      Method method = memberBinding.BoundMember as Method;
      if (method != null){
        //Need to take the address of the target object (this parameter), or need to box it, if this target object type is value type
        if (tObj != null && tObj.Type != null && tObj.Type.IsValueType){
          if (tObj.NodeType != NodeType.This){ 
            if (method.DeclaringType != null && method.DeclaringType.IsValueType) //it expects the address of the value type
              memberBinding.TargetObject = new UnaryExpression(memberBinding.TargetObject, NodeType.AddressOf, memberBinding.TargetObject.Type.GetReferenceType());
            else{ //it expects a boxed copy of the value type
              MemberBinding obType = new MemberBinding(null, memberBinding.TargetObject.Type);
              memberBinding.TargetObject = new BinaryExpression(memberBinding.TargetObject, obType, NodeType.Box, method.DeclaringType);
            }
          }else{
            //REVIEW: perhaps This nodes of value types should be explicitly typed as reference types
            //TODO: assert false in that case
          }
        }
      }
      TypeNode t = memberBinding.BoundMember as TypeNode;
      if (t != null) {
        TypeNode t1 = this.VisitTypeReference(t);
        memberBinding.BoundMember = t1;
      }
      return memberBinding;
    }
    public override Method VisitMethod(Method method){
      if (method == null) return null;
      Method savedCurrentMethod = this.CurrentMethod;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentMethod = method;
      this.CurrentType = method.DeclaringType;
      method.Body = this.VisitBlock(method.Body);
      this.CurrentMethod = savedCurrentMethod;
      this.CurrentType = savedCurrentType;
      return method;
    }
    public override Expression VisitConstruct(Construct cons){
      cons = (Construct)base.VisitConstruct(cons);
      if (cons == null) return null;
      MemberBinding mb = cons.Constructor as MemberBinding;
      if (mb == null) return cons;
      Method meth = mb.BoundMember as Method;
      if (meth == null) return cons;
      ParameterList parameters = meth.Parameters;
      if (parameters == null) return cons;
      ExpressionList operands = cons.Operands;
      int n = operands == null ? 0 : operands.Count;
      if (n > parameters.Count) n = parameters.Count;
      for (int i = 0; i < n; i++){
        //^ assert operands != null;
        Expression e = operands[i];
        if (e == null) continue;
        Parameter p = parameters[i];
        if (p == null) continue;
        if (e.Type == null || p.Type == null) continue;
        if (e.Type.IsValueType && !p.Type.IsValueType)
          operands[i] = new BinaryExpression(e, new MemberBinding(null, e.Type), NodeType.Box, p.Type);
      }
      return cons;
    }
    public override Expression VisitMethodCall(MethodCall call){
      call = (MethodCall)base.VisitMethodCall(call);
      if (call == null) return null;
      MemberBinding mb = call.Callee as MemberBinding;
      if (mb == null) return call;
      Method meth = mb.BoundMember as Method;
      if (meth == null) return call;
      ParameterList parameters = meth.Parameters;
      if (parameters == null) return call;
      ExpressionList operands = call.Operands;
      int n = operands == null ? 0 : operands.Count;
      if (n > parameters.Count) n = parameters.Count;
      for (int i = 0; i < n; i++){
        //^ assert operands != null;
        Expression e = operands[i];
        if (e == null) continue;
        Parameter p = parameters[i];
        if (p == null) continue;
        if (e.Type == null || p.Type == null) continue;
        if (e.Type.IsValueType && !p.Type.IsValueType)
          operands[i] = new BinaryExpression(e, new MemberBinding(null, e.Type), NodeType.Box, p.Type);
      }
      if (meth.ReturnType != null && call.Type != null && meth.ReturnType.IsValueType && !call.Type.IsValueType)
        return new BinaryExpression(call, new MemberBinding(null, meth.ReturnType), NodeType.Box, call.Type);
      return call;
    }
    public override Statement VisitReturn(Return Return){
      Return = (Return)base.VisitReturn(Return);
      if (Return == null) return null;
      Expression rval = Return.Expression;
      if (rval == null || rval.Type == null || this.CurrentMethod == null || this.CurrentMethod.ReturnType == null)
        return Return;
      if (rval.Type.IsValueType && !this.CurrentMethod.ReturnType.IsValueType)
        Return.Expression = new BinaryExpression(rval, new MemberBinding(null, rval.Type), NodeType.Box, this.CurrentMethod.ReturnType);
      return Return;
    }
    public override TypeNode VisitTypeNode(TypeNode typeNode){
      if (typeNode == null) return null;
      TypeNode savedCurrentType = this.CurrentType;
      this.CurrentType = typeNode;
      MemberList members = typeNode.Members;
      for (int i = 0, n = members == null ? 0 : members.Count; i < n; i++){
        //^ assert members != null;
        Member mem = members[i];
        TypeNode t = mem as TypeNode;
        if (t != null){ this.VisitTypeNode(t); continue;}
        Method m = mem as Method;
        if (m != null){ this.VisitMethod(m); continue;}
      }
      this.CurrentType = savedCurrentType;
      return typeNode;
    }
    public override Expression VisitUnaryExpression(UnaryExpression unaryExpression){
      if (unaryExpression == null) return null;
      return base.VisitUnaryExpression((UnaryExpression)unaryExpression.Clone());
    }
  }
}
