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
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;

namespace System.Compiler
{
  public class Inspector
  {
    public Inspector()
    {
    }
    public virtual void VisitUnknownNodeType(Node node)
    {
      Debug.Assert(false);
    }
    public virtual void Visit(Node node)
    {
      if (node == null) return;
      switch (node.NodeType)
      {
        case NodeType.AddressDereference:
          this.VisitAddressDereference((AddressDereference)node);
          return;
        case NodeType.Arglist:
          this.VisitExpression((Expression)node);
          return;
        case NodeType.ArrayType:
          Debug.Assert(false); return;
        case NodeType.Assembly:
          this.VisitAssembly((AssemblyNode)node);
          return;
        case NodeType.AssemblyReference:
          this.VisitAssemblyReference((AssemblyReference)node);
          return;
        case NodeType.Assertion:
          this.VisitAssertion((Assertion)node);
          return;
        case NodeType.Assumption:
          this.VisitAssumption((Assumption)node);
          return;
        case NodeType.AssignmentExpression:
          this.VisitAssignmentExpression((AssignmentExpression)node);
          return;
        case NodeType.AssignmentStatement:
          this.VisitAssignmentStatement((AssignmentStatement)node);
          return;
        case NodeType.Attribute:
          this.VisitAttributeNode((AttributeNode)node);
          return;
        case NodeType.Block:
          this.VisitBlock((Block)node);
          return;
        case NodeType.BlockExpression:
          this.VisitBlockExpression((BlockExpression)node);
          return;
        case NodeType.Branch:
          this.VisitBranch((Branch)node);
          return;
        case NodeType.DebugBreak:
          return;
        case NodeType.Call:
        case NodeType.Calli:
        case NodeType.Callvirt:
        case NodeType.Jmp:
        case NodeType.MethodCall:
          this.VisitMethodCall((MethodCall)node);
          return;
        case NodeType.Class:
          this.VisitClass((Class)node);
          return;
        case NodeType.Construct:
          this.VisitConstruct((Construct)node);
          return;
        case NodeType.ConstructArray:
          this.VisitConstructArray((ConstructArray)node);
          return;
        case NodeType.DelegateNode:
          this.VisitDelegateNode((DelegateNode)node);
          return;
        case NodeType.Dup:
          this.VisitExpression((Expression)node);
          return;
        case NodeType.EndFilter:
          this.VisitEndFilter((EndFilter)node);
          return;
        case NodeType.EndFinally:
          this.VisitEndFinally((EndFinally)node);
          return;
        case NodeType.EnumNode:
          this.VisitEnumNode((EnumNode)node);
          return;
        case NodeType.Event:
          this.VisitEvent((Event)node);
          return;
        case NodeType.EnsuresExceptional:
          this.VisitEnsuresExceptional((EnsuresExceptional)node);
          return;
        case NodeType.ExpressionStatement:
          this.VisitExpressionStatement((ExpressionStatement)node);
          return;
        case NodeType.Field:
          this.VisitField((Field)node);
          return;
        case NodeType.Identifier:
          this.VisitIdentifier((Identifier)node);
          return;
        case NodeType.Indexer:
          this.VisitIndexer((Indexer)node);
          return;
        case NodeType.InstanceInitializer:
          this.VisitInstanceInitializer((InstanceInitializer)node);
          return;
        case NodeType.Invariant:
          this.VisitInvariant((Invariant)node);
          return;
        case NodeType.StaticInitializer:
          this.VisitStaticInitializer((StaticInitializer)node);
          return;
        case NodeType.Method:
          this.VisitMethod((Method)node);
          return;
        case NodeType.Interface:
          this.VisitInterface((Interface)node);
          return;
        case NodeType.Literal:
          this.VisitLiteral((Literal)node);
          return;
        case NodeType.Local:
          this.VisitLocal((Local)node);
          return;
        case NodeType.MemberBinding:
          this.VisitMemberBinding((MemberBinding)node);
          return;
        case NodeType.MethodContract:
          this.VisitMethodContract((MethodContract)node);
          return;
        case NodeType.Module:
          this.VisitModule((Module)node);
          return;
        case NodeType.ModuleReference:
          this.VisitModuleReference((ModuleReference)node);
          return;
        case NodeType.NamedArgument:
          this.VisitNamedArgument((NamedArgument)node);
          return;
        case NodeType.Nop:
          return;
        case NodeType.EnsuresNormal:
          this.VisitEnsuresNormal((EnsuresNormal)node);
          return;
        case NodeType.OldExpression:
          this.VisitOldExpression((OldExpression)node);
          return;
        case NodeType.ReturnValue:
          this.VisitReturnValue((ReturnValue)node);
          return;
        case NodeType.RequiresOtherwise:
          this.VisitRequiresOtherwise((RequiresOtherwise)node);
          return;
        case NodeType.RequiresPlain:
          this.VisitRequiresPlain((RequiresPlain)node);
          return;
        case NodeType.OptionalModifier:
        case NodeType.RequiredModifier:
          //TODO: type modifers should only be visited via VisitTypeReference
          this.VisitTypeModifier((TypeModifier)node);
          return;
        case NodeType.Parameter:
          this.VisitParameter((Parameter)node);
          return;
        case NodeType.Pop:
          this.VisitExpression((Expression)node);
          return;
        case NodeType.Property:
          this.VisitProperty((Property)node);
          return;
        case NodeType.Rethrow:
        case NodeType.Throw:
          this.VisitThrow((Throw)node);
          return;
        case NodeType.Return:
          this.VisitReturn((Return)node);
          return;
        case NodeType.SecurityAttribute:
          this.VisitSecurityAttribute((SecurityAttribute)node);
          return;
        case NodeType.Struct:
          this.VisitStruct((Struct)node);
          return;
        case NodeType.SwitchInstruction:
          this.VisitSwitchInstruction((SwitchInstruction)node);
          return;
        case NodeType.This:
          this.VisitThis((This)node);
          return;
        case NodeType.TypeContract:
          this.VisitTypeContract((TypeContract)node);
          return;
        case NodeType.ClassParameter:
        case NodeType.TypeParameter:
          this.VisitTypeParameter((TypeNode)node);
          return;
        case NodeType.Cpblk:
        case NodeType.Initblk:
          this.VisitTernaryExpression((TernaryExpression)node);
          return;

        case NodeType.Add:
        case NodeType.Add_Ovf:
        case NodeType.Add_Ovf_Un:
        case NodeType.And:
        case NodeType.Box:
        case NodeType.Castclass:
        case NodeType.Ceq:
        case NodeType.Cgt:
        case NodeType.Cgt_Un:
        case NodeType.Clt:
        case NodeType.Clt_Un:
        case NodeType.Div:
        case NodeType.Div_Un:
        case NodeType.Eq:
        case NodeType.Ge:
        case NodeType.Gt:
        case NodeType.Isinst:
        case NodeType.Ldvirtftn:
        case NodeType.Le:
        case NodeType.Lt:
        case NodeType.Mkrefany:
        case NodeType.Mul:
        case NodeType.Mul_Ovf:
        case NodeType.Mul_Ovf_Un:
        case NodeType.Ne:
        case NodeType.Or:
        case NodeType.Refanyval:
        case NodeType.Rem:
        case NodeType.Rem_Un:
        case NodeType.Shl:
        case NodeType.Shr:
        case NodeType.Shr_Un:
        case NodeType.Sub:
        case NodeType.Sub_Ovf:
        case NodeType.Sub_Ovf_Un:
        case NodeType.Unbox:
        case NodeType.UnboxAny:
        case NodeType.Xor:
          this.VisitBinaryExpression((BinaryExpression)node);
          return;

        case NodeType.AddressOf:
        case NodeType.Ckfinite:
        case NodeType.Conv_I:
        case NodeType.Conv_I1:
        case NodeType.Conv_I2:
        case NodeType.Conv_I4:
        case NodeType.Conv_I8:
        case NodeType.Conv_Ovf_I:
        case NodeType.Conv_Ovf_I1:
        case NodeType.Conv_Ovf_I1_Un:
        case NodeType.Conv_Ovf_I2:
        case NodeType.Conv_Ovf_I2_Un:
        case NodeType.Conv_Ovf_I4:
        case NodeType.Conv_Ovf_I4_Un:
        case NodeType.Conv_Ovf_I8:
        case NodeType.Conv_Ovf_I8_Un:
        case NodeType.Conv_Ovf_I_Un:
        case NodeType.Conv_Ovf_U:
        case NodeType.Conv_Ovf_U1:
        case NodeType.Conv_Ovf_U1_Un:
        case NodeType.Conv_Ovf_U2:
        case NodeType.Conv_Ovf_U2_Un:
        case NodeType.Conv_Ovf_U4:
        case NodeType.Conv_Ovf_U4_Un:
        case NodeType.Conv_Ovf_U8:
        case NodeType.Conv_Ovf_U8_Un:
        case NodeType.Conv_Ovf_U_Un:
        case NodeType.Conv_R4:
        case NodeType.Conv_R8:
        case NodeType.Conv_R_Un:
        case NodeType.Conv_U:
        case NodeType.Conv_U1:
        case NodeType.Conv_U2:
        case NodeType.Conv_U4:
        case NodeType.Conv_U8:
        case NodeType.Ldftn:
        case NodeType.Ldlen:
        case NodeType.Ldtoken:
        case NodeType.Localloc:
        case NodeType.LogicalNot:
        case NodeType.Neg:
        case NodeType.Not:
        case NodeType.Refanytype:
        case NodeType.ReadOnlyAddressOf:
        case NodeType.Sizeof:
        case NodeType.SkipCheck:
          this.VisitUnaryExpression((UnaryExpression)node);
          return;
        default:
          this.VisitUnknownNodeType(node);
          return;
      }
    }
    public virtual void VisitAddressDereference(AddressDereference addr)
    {
      if (addr == null) return;
      this.VisitExpression(addr.Address);
    }
    public virtual void VisitAssembly(AssemblyNode assembly)
    {
      if (assembly == null) return;
      this.VisitModule(assembly);
      this.VisitAttributeList(assembly.ModuleAttributes);
      this.VisitSecurityAttributeList(assembly.SecurityAttributes);
    }
    public virtual AssemblyReference VisitAssemblyReference(AssemblyReference assemblyReference)
    {
      return assemblyReference;
    }
    public virtual void VisitAssertion(Assertion assertion)
    {
      if (assertion == null) return;
      this.VisitExpression(assertion.Condition);
    }
    public virtual void VisitAssumption(Assumption assumption)
    {
      if (assumption == null) return;
      this.VisitExpression(assumption.Condition);
    }
    public virtual void VisitAssignmentExpression(AssignmentExpression assignment)
    {
      if (assignment == null) return;
      this.Visit(assignment.AssignmentStatement);
    }
    public virtual void VisitAssignmentStatement(AssignmentStatement assignment)
    {
      if (assignment == null) return;
      this.VisitTargetExpression(assignment.Target);
      this.VisitExpression(assignment.Source);
    }
    public virtual void VisitAttributeConstructor(AttributeNode attribute)
    {
      if (attribute == null) return;
      this.VisitExpression(attribute.Constructor);
    }
    public virtual void VisitAttributeNode(AttributeNode attribute)
    {
      if (attribute == null) return;
      this.VisitAttributeConstructor(attribute);
      this.VisitExpressionList(attribute.Expressions);
    }
    public virtual void VisitAttributeList(AttributeList attributes)
    {
      if (attributes == null) return;
      for (int i = 0, n = attributes.Count; i < n; i++)
        this.VisitAttributeNode(attributes[i]);
    }
    public virtual void VisitBinaryExpression(BinaryExpression binaryExpression)
    {
      if (binaryExpression == null) return;
      this.VisitExpression(binaryExpression.Operand1);
      this.VisitExpression(binaryExpression.Operand2);
    }
    public virtual void VisitBlock(Block block)
    {
      if (block == null) return;
      this.VisitStatementList(block.Statements);
    }
    public virtual void VisitBlockExpression(BlockExpression blockExpression)
    {
      if (blockExpression == null) return;
      this.VisitBlock(blockExpression.Block);
    }
    public virtual void VisitBlockList(BlockList blockList)
    {
      if (blockList == null) return;
      for (int i = 0, n = blockList.Count; i < n; i++)
        this.VisitBlock(blockList[i]);
    }
    public virtual void VisitBranch(Branch branch)
    {
      if (branch == null) return;
      this.VisitExpression(branch.Condition);
    }
    public virtual void VisitClass(Class Class)
    {
      this.VisitTypeNode(Class);
    }
    public virtual void VisitConstruct(Construct cons)
    {
      if (cons == null) return;
      this.VisitExpression(cons.Constructor);
      this.VisitExpressionList(cons.Operands);
    }
    public virtual void VisitConstructArray(ConstructArray consArr)
    {
      if (consArr == null) return;
      this.VisitTypeReference(consArr.ElementType);
      this.VisitExpressionList(consArr.Operands);
      this.VisitExpressionList(consArr.Initializers);
      this.VisitExpression(consArr.Owner);
    }
    public virtual void VisitDelegateNode(DelegateNode delegateNode)
    {
      if (delegateNode == null) return;
      this.VisitTypeNode(delegateNode);
      this.VisitParameterList(delegateNode.Parameters);
      this.VisitTypeReference(delegateNode.ReturnType);
    }
    public virtual void VisitEndFilter(EndFilter endFilter)
    {
      if (endFilter == null) return;
      this.VisitExpression(endFilter.Value);
    }
    public virtual Statement VisitEndFinally(EndFinally endFinally)
    {
      return endFinally;
    }
    public virtual void VisitEnsuresList(EnsuresList Ensures)
    {
      if (Ensures == null) return;
      for (int i = 0, n = Ensures.Count; i < n; i++)
        this.Visit(Ensures[i]);
    }
    public virtual void VisitEnumNode(EnumNode enumNode)
    {
      this.VisitTypeNode(enumNode);
    }
    public virtual void VisitEvent(Event evnt)
    {
      if (evnt == null) return;
      this.VisitAttributeList(evnt.Attributes);
      this.VisitTypeReference(evnt.HandlerType);
    }
    public virtual void VisitEnsuresExceptional(EnsuresExceptional exceptional)
    {
      if (exceptional == null) return;
      this.VisitExpression(exceptional.PostCondition);
      this.VisitTypeReference(exceptional.Type);
      this.VisitExpression(exceptional.Variable);
    }

    public virtual void VisitExpression(Expression expression)
    {
      if (expression == null) return;
      switch (expression.NodeType)
      {
        case NodeType.Dup:
        case NodeType.Arglist:
          return;
        case NodeType.Pop:
          UnaryExpression uex = expression as UnaryExpression;
          if (uex != null)
          {
            this.VisitExpression(uex.Operand);
            return;
          }
          return;
        default:
          this.Visit(expression);
          return;
      }
    }
    public void VisitExpressionList(ExpressionList expressions)
    {
      if (expressions == null) return;
      for (int i = 0, n = expressions.Count; i < n; i++)
       this.VisitExpression(expressions[i]);
    }
    public virtual void VisitExpressionStatement(ExpressionStatement statement)
    {
      if (statement == null) return;
      this.VisitExpression(statement.Expression);
    }
    public virtual void VisitField(Field field)
    {
      if (field == null) return;
      this.VisitAttributeList(field.Attributes);
      this.VisitTypeReference(field.Type);
    }
    public virtual void VisitIdentifier(Identifier identifier)
    {
    }
    public virtual void VisitIndexer(Indexer indexer)
    {
      if (indexer == null) return;
      this.VisitExpression(indexer.Object);
      this.VisitExpressionList(indexer.Operands);
    }
    public virtual void VisitInterface(Interface Interface)
    {
      this.VisitTypeNode(Interface);
    }
    public virtual void VisitInterfaceReference(Interface Interface)
    {
      this.VisitTypeReference(Interface);
    }
    public virtual void VisitInterfaceReferenceList(InterfaceList interfaceReferences)
    {
      if (interfaceReferences == null) return;
      for (int i = 0, n = interfaceReferences.Count; i < n; i++)
        this.VisitInterfaceReference(interfaceReferences[i]);
    }
    public virtual void VisitInvariant(Invariant @invariant)
    {
      if (@invariant == null) return;
      VisitExpression(@invariant.Condition);
    }
    public virtual void VisitInvariantList(InvariantList invariants)
    {
      if (invariants == null) return;
      for (int i = 0, n = invariants.Count; i < n; i++)
        this.VisitInvariant(invariants[i]);
    }
    public virtual void VisitInstanceInitializer(InstanceInitializer cons)
    {
      this.VisitMethod(cons);
    }
    public virtual void VisitLiteral(Literal literal)
    {
    }
    public virtual void VisitLocal(Local local)
    {
      if (local == null) return;
      this.VisitTypeReference(local.Type);
    }
    public virtual void VisitMemberBinding(MemberBinding memberBinding)
    {
      if (memberBinding == null) return;
      this.VisitExpression(memberBinding.TargetObject);
    }
    public virtual void VisitMemberList(MemberList members)
    {
      if (members == null) return;
      for (int i = 0, n = members.Count; i < n; i++)
      {
        this.Visit(members[i]);
      }
    }
    public virtual void VisitMethod(Method method)
    {
      if (method == null) return;
      this.VisitAttributeList(method.Attributes);
      this.VisitAttributeList(method.ReturnAttributes);
      this.VisitSecurityAttributeList(method.SecurityAttributes);
      this.VisitTypeReference(method.ReturnType);
      this.VisitParameterList(method.Parameters);
      if (TargetPlatform.UseGenerics)
      {
        this.VisitTypeReferenceList(method.TemplateArguments);
        this.VisitTypeParameterList(method.TemplateParameters);
      }
      this.VisitMethodContract(method.Contract);
      this.VisitBlock(method.Body);
    }
    public virtual void VisitMethodCall(MethodCall call)
    {
      if (call == null) return;
      this.VisitExpression(call.Callee);
      this.VisitExpressionList(call.Operands);
      this.VisitTypeReference(call.Constraint);
    }
    public virtual void VisitMethodContract(MethodContract contract)
    {
      if (contract == null) return;
      // don't visit contract.DeclaringMethod
      // don't visit contract.OverriddenMethods
      this.VisitRequiresList(contract.Requires);
      this.VisitEnsuresList(contract.Ensures);
      this.VisitEnsuresList(contract.ModelEnsures);
      this.VisitExpressionList(contract.Modifies);
      this.VisitEnsuresList(contract.AsyncEnsures);
    }
    public virtual void VisitModule(Module module)
    {
      if (module == null) return;
      this.VisitAttributeList(module.Attributes);
      this.VisitTypeNodeList(module.Types);
    }
    public virtual void VisitModuleReference(ModuleReference moduleReference)
    {
    }
    public virtual void VisitNamedArgument(NamedArgument namedArgument)
    {
      if (namedArgument == null) return;
      this.VisitExpression(namedArgument.Value);
    }
    public virtual void VisitEnsuresNormal(EnsuresNormal normal)
    {
      if (normal == null) return;
      this.VisitExpression(normal.PostCondition);
    }
    public virtual void VisitOldExpression(OldExpression oldExpression)
    {
      if (oldExpression == null) return;
      this.VisitExpression(oldExpression.expression);
    }
    public virtual void VisitReturnValue(ReturnValue returnValue)
    {
      if (returnValue == null) return;
      this.VisitTypeReference(returnValue.Type);
    }
    public virtual void VisitRequiresOtherwise(RequiresOtherwise otherwise)
    {
      if (otherwise == null) return;
      this.VisitExpression(otherwise.Condition);
      this.VisitExpression(otherwise.ThrowException);
    }
    public virtual void VisitRequiresPlain(RequiresPlain plain)
    {
      if (plain == null) return;
      this.VisitExpression(plain.Condition);
    }
    public virtual void VisitParameter(Parameter parameter)
    {
      if (parameter == null) return;
      this.VisitAttributeList(parameter.Attributes);
      this.VisitTypeReference(parameter.Type);
      this.VisitExpression(parameter.DefaultValue);
    }
    public virtual void VisitParameterList(ParameterList parameterList)
    {
      if (parameterList == null) return;
      for (int i = 0, n = parameterList.Count; i < n; i++)
        this.VisitParameter(parameterList[i]);
    }
    public virtual void VisitProperty(Property property)
    {
      if (property == null) return;
      this.VisitAttributeList(property.Attributes);
      this.VisitParameterList(property.Parameters);
      this.VisitTypeReference(property.Type);
    }
    public virtual void VisitRequiresList(RequiresList Requires)
    {
      if (Requires == null) return;
      for (int i = 0, n = Requires.Count; i < n; i++)
        this.Visit(Requires[i]);
    }
    public virtual void VisitReturn(Return returnStatement)
    {
      if (returnStatement == null)
      {
        return;
      }

      this.VisitExpression(returnStatement.Expression);
    }
    public virtual void VisitSecurityAttribute(SecurityAttribute attribute)
    {
    }
    public virtual void VisitSecurityAttributeList(SecurityAttributeList attributes)
    {
      if (attributes == null) return;
      for (int i = 0, n = attributes.Count; i < n; i++)
        this.VisitSecurityAttribute(attributes[i]);
    }
    public virtual void VisitStatementList(StatementList statements)
    {
      if (statements == null) return;
      for (int i = 0, n = statements.Count; i < n; i++)
        this.Visit(statements[i]);
    }
    public virtual void VisitStaticInitializer(StaticInitializer cons)
    {
      this.VisitMethod(cons);
    }
    public virtual void VisitStruct(Struct Struct)
    {
      this.VisitTypeNode(Struct);
    }
    public virtual void VisitSwitchInstruction(SwitchInstruction switchInstruction)
    {
      if (switchInstruction == null) return;
      this.VisitExpression(switchInstruction.Expression);
    }
    public virtual void VisitTargetExpression(Expression expression)
    {
      this.VisitExpression(expression);
    }
    public virtual void VisitTernaryExpression(TernaryExpression expression)
    {
      if (expression == null) return;
      this.VisitExpression(expression.Operand1);
      this.VisitExpression(expression.Operand2);
      this.VisitExpression(expression.Operand3);
    }
    public virtual void VisitThis(This This)
    {
      if (This == null) return;
      this.VisitTypeReference(This.Type);
    }
    public virtual void VisitThrow(Throw Throw)
    {
      if (Throw == null) return;
      this.VisitExpression(Throw.Expression);
    }
    public virtual void VisitTypeContract(TypeContract contract)
    {
      if (contract == null) return;
      // don't visit contract.DeclaringType
      // don't visit contract.InheritedContracts
      this.VisitInvariantList(contract.Invariants);
    }
    public virtual void VisitTypeModifier(TypeModifier typeModifier)
    {
      if (typeModifier == null) return;
      this.VisitTypeReference(typeModifier.Modifier);
      this.VisitTypeReference(typeModifier.ModifiedType);
    }
    public virtual void VisitTypeNode(TypeNode typeNode)
    {
      if (typeNode == null) return;
      this.VisitAttributeList(typeNode.Attributes);
      this.VisitSecurityAttributeList(typeNode.SecurityAttributes);
      Class c = typeNode as Class;
      if (c != null) this.VisitTypeReference(c.BaseClass);
      this.VisitInterfaceReferenceList(typeNode.Interfaces);
      this.VisitTypeReferenceList(typeNode.TemplateArguments);
      this.VisitTypeParameterList(typeNode.TemplateParameters);
      this.VisitMemberList(typeNode.Members);
      // have to visit this *after* visiting the members since in Normalizer
      // it creates normalized method bodies for the invariant methods and
      // those shouldn't be visited again!!
      // REVIEW!! I don't think the method bodies created in Normalizer are necessarily normalized anymore!!
      this.VisitTypeContract(typeNode.Contract);
    }
    public virtual void VisitTypeNodeList(TypeNodeList types)
    {
      if (types == null) return;
      for (int i = 0; i < types.Count; i++) //Visiting a type may result in a new type being appended to this list
        this.Visit(types[i]);
    }
    public virtual void VisitTypeParameter(TypeNode typeParameter)
    {
      if (typeParameter == null) return;
      Class cl = typeParameter as Class;
      if (cl != null) this.VisitTypeReference(cl.BaseClass);
      this.VisitAttributeList(typeParameter.Attributes);
      this.VisitInterfaceReferenceList(typeParameter.Interfaces);
    }
    public virtual void VisitTypeParameterList(TypeNodeList typeParameters)
    {
      if (typeParameters == null) return;
      for (int i = 0, n = typeParameters.Count; i < n; i++)
        this.VisitTypeParameter(typeParameters[i]);
    }
    public virtual void VisitTypeReference(TypeNode type)
    {
    }
    public virtual void VisitTypeReference(TypeReference type)
    {
    }
    public virtual void VisitTypeReferenceList(TypeNodeList typeReferences)
    {
      if (typeReferences == null) return;
      for (int i = 0, n = typeReferences.Count; i < n; i++)
        this.VisitTypeReference(typeReferences[i]);
    }
    public virtual void VisitUnaryExpression(UnaryExpression unaryExpression)
    {
      if (unaryExpression == null) return;
      this.VisitExpression(unaryExpression.Operand);
    }
  }
}