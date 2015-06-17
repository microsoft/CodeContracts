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
using Collections = System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
#if INCLUDE_PEXINTEGRATION
using Microsoft.ExtendedReflection.Feedback;
using Microsoft.ExtendedReflection.Remote;
using Microsoft.ExtendedReflection.ComponentModel;
using Microsoft.ExtendedReflection.Metadata.Names;
using Microsoft.ExtendedReflection.Collections;
using Microsoft.ExtendedReflection.Metadata;
using Microsoft.ExtendedReflection.Utilities.Safe.Xml;
#endif
using Microsoft.Research.DataStructures;
using Microsoft.Research.CodeAnalysis;
using System.IO;
using Microsoft.ExtendedReflection.Metadata.Builders;

namespace Microsoft.Research.CodeAnalysis {

  public class SuggestedCodeFixes
  {

    #region Ranks
    
    public const int WARNING_ARRAY_BOUND = 4;
    public const int WARNING_ARRAY_CREATION = 3;
    
    public const int WARNING_ASSERTION = 3;
    
    public const int WARNING_DIVISION_BY_ZERO = 4;
    public const int WARNING_NEGATING_MININT = WARNING_DIVISION_BY_ZERO - 1;

    public const int WARNING_OVERFLOWORUNDERFLOW = 2;

    public const int WARNING_NON_NULL = 15;

    public const int WARNING_UNSAFE_BOUND = WARNING_ARRAY_BOUND + 2;
    public const int WARNING_UNSAFE_CREATION = WARNING_ARRAY_CREATION + 2;
    
    public const int SUGGESTED_PRECONDITION = 4;
    public const int SUGGESTED_POSTCONDITION = 2;

    public const int PROBABLY_NOT_INTERESTING = 0;

    #endregion

    #region Templates and utils to format preconditions and postconditions
    /// <summary>
    /// The Foxtrot syntax for the precondition. 
    /// It contains one hole, which is the condition
    /// </summary>
    private const string ContractPreconditionTemplate = "Contract.Requires({0});";

    /// <summary>
    /// The Foxtrot syntax for the postcondition. 
    /// It contains one hole, which is the condition
    /// </summary>
    private static string ContractPostconditionTemplate = "Contract.Ensures({0});";

    /// <summary>
    /// The Foxtrot syntax for the postcondition.
    /// It contains one hole, which is the return type of the method
    /// </summary>
    private static string ContractResultTemplate =  "Contract.Result<{0}>()";      

    public static string MakeFoxtrotEnsure(string type)
    {
      return string.Format(ContractResultTemplate, type);
    }

    private static string MakePreconditionString(string condition)
    {      
      return string.Format(ContractPreconditionTemplate, condition);
    }

    private static string MakePostconditionString(string condition)
    {
      return string.Format(ContractPostconditionTemplate, condition);
    }

    #endregion

    public static void Initialize(bool usePex)
    {
      // protect against Pex version mismatch or not being installed.
      try
      {
        InternalInitialize(usePex);
      }
      catch
      {
      }
    }

    private static void InternalInitialize(bool usePex)
    {
#if INCLUDE_PEXINTEGRATION
      SuggestedCodeFixes.usePex = usePex;
      if (!usePex) return;

      IFeedbackManager feedbackManager = GetFeedbackConnection();
      if (feedbackManager == null)
      {
        return;
      }

      // clear UI in VS
      feedbackManager.Clear();
#endif
    }

#if INCLUDE_PEXINTEGRATION
    static bool usePex;
    static IFeedbackManager cache;

    static IFeedbackManager GetFeedbackConnection()
    {
      if (!usePex) return null;
      if (cache != null) return cache;

      string connectionName = Environment.GetEnvironmentVariable("pex_server_channel");
      if (connectionName == null) return null;

      IRemoteServerConnector connector;
      try
      {
        connector = RemoteServer.GetConnector(connectionName);
      }
      catch
      {
        connector = null;
      }
      if (connector == null)
      {
        usePex = false; // no pex
        return null;
      }

      IService service;
      try
      {
        if (!connector.TryGetService(typeof(IFeedbackManager), out service))
        {
          usePex = false; // no pex
          return null;
        }
      }
      catch
      {
        usePex = false; // no pex
        return null;
      }
      cache = (IFeedbackManager)service;

      return cache;
    }
#endif

    /// <summary>
    /// Tries to connect to VS to send a code update. If not possible, it prints out the suggested 
    /// pre-condition on the given outptu.
    /// </summary>
    /// <param name="method">Method where to add preconditoin</param>
    /// <param name="precondition">string representation of what to add</param>
    /// <param name="symptom">A string describing why the precondition is added</param>
    /// <param name="output">alternative output if VS is not available</param>
    /// <param name="mdDecoder">decoder used to construct corresponding method name for ExtendedReflection</param>
    public static void AddPrecondition<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
      string precondition, int rank, string symptom, IOutput output, 
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      where Type : IEquatable<Type>
    {
      if (!output.LogOptions.SuggestRequires && !output.LogOptions.SuggestRequiresForArrays)
      {
        return;
      }
      output.Suggestion("precondition", GetPCForMethodEntry(context), MakePreconditionString(precondition));

      try
      {
        SuggestPreconditionViaPex<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(context, precondition, rank, symptom, output, mdDecoder);
      }
      catch
      {
        // guard against errors in ExtendedReflection versioning/VS integration
      }
    }

    private static void SuggestPreconditionViaPex<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context, string precondition, int rank, string symptom, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder) where Type : IEquatable<Type>
    {
#if INCLUDE_PEXINTEGRATION
      IFeedbackManager feedbackManager = GetFeedbackConnection();
      if (feedbackManager != null)
      {
        MethodDefinitionName target = Translate(context.MethodContext.CurrentMethod, output, mdDecoder);

        string group = Guid.NewGuid().ToString();
        var shortAssemblyName = ShortAssemblyName.FromName("Microsoft.Contracts");
        CodeUpdate update =
          CodeUpdate.InsertCheck("Requires", target, MakePreconditionString(precondition), new[]{"System.Diagnostics.Contracts"}, new[]{shortAssemblyName}, "Clousot");

        CodeFix fix = CodeFix.FromUpdate("Clousot", symptom, group, update, rank, CodeFixImage.Message); // TODO: figure out ranking
        try
        {
          feedbackManager.AddFix(fix);
        }
        catch { }
      }
#endif
    }

    public static void AddPostCondition<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
      string postcondition, int rank, string symptom, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      where Type : IEquatable<Type>
    {
      AddPostCondition(context, new System.Collections.Generic.List<string>() { postcondition }, rank, symptom, output, mdDecoder);
    }

    public static void AddPostCondition<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context,
      System.Collections.Generic.List<string> postconditions, int rank, string symptom, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
      where Type : IEquatable<Type>
    {
      var isProperty = mdDecoder.IsPropertyGetter(context.MethodContext.CurrentMethod) || mdDecoder.IsPropertySetter(context.MethodContext.CurrentMethod);
      if (!output.LogOptions.SuggestNonNullReturn && !output.LogOptions.SuggestEnsures(isProperty))
      {
        return;
      }

      APC sourcePC = GetPCForMethodEntry(context);

      foreach (string post in postconditions)
      {
        output.Suggestion("postcondition", sourcePC, MakePostconditionString(post));

        try
        {
          SuggestPostConditionViaPex<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(context, rank, symptom, output, mdDecoder, post);
        }
        catch
        {
          // guard against errors in ExtendedReflection version/VS integration
        }
      }
    }

    private static void SuggestPostConditionViaPex<Local, Parameter, Method, Field, Property, Event, Type, Expression, Variable, Attribute, Assembly>(IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context, int rank, string symptom, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, string post) where Type : IEquatable<Type>
    {
#if INCLUDE_PEXINTEGRATION
      IFeedbackManager feedbackManager = GetFeedbackConnection();
      if (feedbackManager != null)
      {
        MethodDefinitionName target = Translate(context.MethodContext.CurrentMethod, output, mdDecoder);

        string group = Guid.NewGuid().ToString();
        var microsoftContractsAssembly = ShortAssemblyName.FromName("Microsoft.Contracts");
        CodeUpdate update =
          CodeUpdate.InsertCheck("Ensures", target, MakePostconditionString(post), new[] { "System.Diagnostics.Contracts" }, new[]{microsoftContractsAssembly}, "Clousot");

        CodeFix fix = CodeFix.FromUpdate("Clousot", symptom, group, update, rank, CodeFixImage.Message);
        try
        {
          feedbackManager.AddFix(fix);
        }
        catch { }
      }
#endif
    }

    private static string GetSourceContextForMethodEntry<Local, Parameter, Method, Field, Type, Expression, Variable>(
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context)
      where Type : IEquatable<Type>
    {
      APC entry = GetPCForMethodEntry(context);
      return entry.PrimarySourceContext();
    }

    private static APC GetPCForMethodEntry<Local, Parameter, Method, Field, Type, Expression, Variable>(
      IExpressionContext<Local, Parameter, Method, Field, Type, Expression, Variable> context)
      where Type : IEquatable<Type>
    {
      APC entry = context.MethodContext.CFG.EntryAfterRequires;
      for (int count = 0; count < 10; count++) {
        if (entry.PrimaryMethodLocation().HasRealSourceContext) return entry.PrimaryMethodLocation();
        entry = context.MethodContext.CFG.Post(entry);
      }
      return context.MethodContext.CFG.Entry;
    }


#if INCLUDE_PEXINTEGRATION
    public static void AddRegressionAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      Method method,
      Enum outcome, 
      string message, 
      int primaryILOffset, 
      int methodILOffset,
      IOutput output,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      System.Type attributeType
    )
    {
      bool success = false;

      try
      {
        success = TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(method, outcome, message, primaryILOffset, methodILOffset, output, mdDecoder, attributeType);
      }
      catch
      {
        // guard against Pex integration not working/ExtendedReflection not being loadable
      }

      if (!success)
      {
        output.WriteLine("If outcome is okay, add attribute\n [RegressionOutcome(Outcome=ProofOutcome.{0},Message=@\"{1}\",PrimaryILOffset={2},MethodILOffset={3})]",
          outcome.ToString(), message, primaryILOffset.ToString(), methodILOffset.ToString());
      }

    }

    private static bool TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Method method, Enum outcome, string message, int primaryILOffset, int methodILOffset, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, System.Type attributeType)
    {
      IFeedbackManager feedbackManager = GetFeedbackConnection();
      bool success = false;
      if (feedbackManager != null)
      {
        PropertyInfo outcomeProp = attributeType.GetProperty("Outcome");
        PropertyInfo messageProp = attributeType.GetProperty("Message");
        PropertyInfo primaryILProp = attributeType.GetProperty("PrimaryILOffset");
        PropertyInfo methodILProp = attributeType.GetProperty("MethodILOffset");
        ConstructorInfo attrCtor = attributeType.GetConstructor(new System.Type[0]);

        MethodDefinitionName target = Translate(method, output, mdDecoder);

        MethodName attributeCtorName = MetadataFromReflection.GetMethod(attrCtor).SerializableName;

#if DEBUG_PEX_BY_XML
        SafeSimpleXmlWriter writer = SafeSimpleXmlWriter.Create(new StreamWriter(@"C:\temp\" + mdDecoder.Name(method) + ".xml"), true);
        target.WriteXml(writer, "method");
        writer.Close();

        SafeSimpleXmlWriter writer2 = SafeSimpleXmlWriter.Create(new StreamWriter(@"C:\temp\" + mdDecoder.Name(method) + "2.xml"), true);
        attributeCtorName2.WriteXml(writer2, "method");
        writer2.Close();

#endif
        string group = Guid.NewGuid().ToString();

        var outcomeArg = AttributeArgument.Named(MetadataFromReflection.GetProperty(outcomeProp), MetadataExpression.EnumValue(outcome));
#if DEBUG_PEX_BY_XML
        SafeSimpleXmlWriter writer3 = SafeSimpleXmlWriter.Create(new StreamWriter(@"C:\temp\" + mdDecoder.Name(method) + "3.xml"), true);
        outcomeArg.WriteXml(writer3, "method");
        writer3.Close();
#endif
        CodeUpdate update =
          CodeUpdate.AddAttribute("Regression", target, attributeCtorName,
                                  outcomeArg,
                                  AttributeArgument.Named(MetadataFromReflection.GetProperty(messageProp), MetadataExpression.String(message)),
                                  AttributeArgument.Named(MetadataFromReflection.GetProperty(primaryILProp), MetadataExpression.I4(MetadataFromReflection.GetType(typeof(int)), primaryILOffset)),
                                  AttributeArgument.Named(MetadataFromReflection.GetProperty(methodILProp), MetadataExpression.I4(MetadataFromReflection.GetType(typeof(int)), methodILOffset))
                                  );

        CodeFix fix = CodeFix.FromUpdate("ClousotRegression", "missing regression attribute", group, update, 100, CodeFixImage.Message);
        try
        {
          feedbackManager.AddFix(fix);
          success = true;
        }
        catch { }
      }
      return success;
    }

    public static void AddRegressionAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      Assembly assembly,
      string message,
      IOutput output,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      System.Type attributeType
    )
    {
      bool success = false;
      try
      {
        success = TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(assembly, message, mdDecoder, attributeType);
      }
      catch
      {
        // guard against Pex integration not working
      }
      if (!success)
      {
        output.WriteLine("If outcome is okay, add attribute\n [assembly: RegressionOutcome({0})]", message);
      }
    }

    private static bool TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Assembly assembly, string message, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, System.Type attributeType)
    {
      IFeedbackManager feedbackManager = GetFeedbackConnection();
      bool success = true;
      if (feedbackManager != null)
      {
        ConstructorInfo attrCtor = attributeType.GetConstructor(new System.Type[1] { typeof(string) });

#if DEBUG_PEX_BY_XML
        SafeSimpleXmlWriter writer = SafeSimpleXmlWriter.Create(new StreamWriter(@"C:\temp\" + mdDecoder.Name(method) + ".xml"), true);
        target.WriteXml(writer, "method");
        writer.Close();

        SafeSimpleXmlWriter writer2 = SafeSimpleXmlWriter.Create(new StreamWriter(@"C:\temp\" + mdDecoder.Name(method) + "2.xml"), true);
        attributeCtorName2.WriteXml(writer2, "method");
        writer2.Close();

#endif
        string group = Guid.NewGuid().ToString();
        ICustomAttribute ca = new CustomAttributeBuilder(MetadataFromReflection.GetMethod(attrCtor), MetadataExpression.String(message));

        CodeUpdate update =
          CodeUpdate.AddAttribute("Regression",
                                  new Microsoft.ExtendedReflection.Metadata.Names.ShortAssemblyName(mdDecoder.Name(assembly), null),
                                  ca);

        CodeFix fix = CodeFix.FromUpdate("ClousotRegression", "missing regression attribute", group, update, 100, CodeFixImage.Message);
        try
        {
          feedbackManager.AddFix(fix);
          success = true;
        }
        catch { }
      }
      return success;
    }

    public static void AddRegressionAttribute<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(
      Method method,
      string message,
      IOutput output,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder,
      System.Type attributeType
    )
    {
      bool success = false;
      try
      {
        success = TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(method, message, output, mdDecoder, attributeType);
      }
      catch
      {
        // guard against PEX integration not working
      }
      if (!success)
      {
        output.WriteLine("If outcome is okay, add attribute\n [RegressionOutcome({0})]", message);
      }
    }

    private static bool TryAddRegressionAttributeViaPex<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Method method, string message, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder, System.Type attributeType)
    {
      IFeedbackManager feedbackManager = GetFeedbackConnection();
      bool success = false;
      if (feedbackManager != null)
      {
        ConstructorInfo attrCtor = attributeType.GetConstructor(new System.Type[1] { typeof(string) });
        MethodDefinitionName target = Translate(method, output, mdDecoder);

        string group = Guid.NewGuid().ToString();
        ICustomAttribute ca = new CustomAttributeBuilder(MetadataFromReflection.GetMethod(attrCtor), MetadataExpression.String(message));

        CodeUpdate update = CodeUpdate.AddAttribute("Regression", target, ca);

        CodeFix fix = CodeFix.FromUpdate("ClousotRegression", "missing regression attribute", group, update, 100, CodeFixImage.Message);
        try
        {
          feedbackManager.AddFix(fix);
          success = true;
        }
        catch { }
      }
      return success;
    }

    static MethodDefinitionName Translate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Method method, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Type declaringType = mdDecoder.DeclaringType(method);
      TypeDefinitionName tdefName = Translate(declaringType, output, mdDecoder);
      TypeName resultTdefName = TranslateToTypeName(mdDecoder.ReturnType(method), output, mdDecoder);
      ParameterDefinitionName[] argTdefNames = Translate(mdDecoder.Parameters(method), method, output, mdDecoder);
      var name = mdDecoder.Name(method);
      if (name == "Finalize") {
        name = "~" + mdDecoder.Name(declaringType).Split(new char[]{'`'}, 2)[0];
      }
      return MethodDefinitionName.FromTypeMethod(0, tdefName, mdDecoder.IsStatic(method), name, null, resultTdefName, argTdefNames);
    }

    private static ParameterDefinitionName[] Translate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Microsoft.Research.DataStructures.IIndexable<Parameter> parameters, Method method, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      int count = parameters.Count + (mdDecoder.IsStatic(method) ? 0 : 1);
      ParameterDefinitionName[] result = new ParameterDefinitionName[count];

      int i = 0;
      if (!mdDecoder.IsStatic(method)) {
        result[i++] = Translate(mdDecoder.This(method), i, true, output, mdDecoder);
      }
      for (int j=0; j<parameters.Count; j++) {
        Parameter p = parameters[j];
        result[i++] = Translate(p, i, false, output, mdDecoder);
      }
      return result;
    }

    private static ParameterDefinitionName Translate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Parameter p, int pos, bool isThis, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      return new ParameterDefinitionName(TranslateToTypeName(mdDecoder.ParameterType(p), output, mdDecoder), mdDecoder.Name(p), pos, isThis, Microsoft.ExtendedReflection.Metadata.ParameterDirection.ByValueOrRef);
    }

    private static TypeDefinitionName Translate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Type type, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Type parent;

      if (mdDecoder.IsNested(type, out parent)) {
        TypeDefinitionName parentDef = Translate(parent, output, mdDecoder);

        TypeDefinitionName nested = TypeDefinitionName.FromName(0, mdDecoder.IsStruct(type), parentDef, mdDecoder.Name(type));
        return nested;

      }

      string moduleName = mdDecoder.DeclaringModuleName(type);
      if (moduleName == null) {
        // can't translate this as a type definition
        output.WriteLine("Can't translate type {0} as a type definition", mdDecoder.Name(type));
        throw new NotImplementedException();
      }

      Microsoft.Research.DataStructures.IIndexable<Type> formals;
      string[] args;
      if (mdDecoder.IsGeneric(type, out formals, true))
      {
        args = TranslateTypeFormals(formals, output, mdDecoder);
      }
      else
      {
        args = new string[0];
      }
      return TypeDefinitionName.FromName(ShortAssemblyName.FromName(mdDecoder.DeclaringModuleName(type)), 0, mdDecoder.IsStruct(type), mdDecoder.Namespace(type), mdDecoder.Name(type), args);
    }

    private static string[] TranslateTypeFormals<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Microsoft.Research.DataStructures.IIndexable<Type> types, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      var formals = new string[types.Count];
      for (int i = 0; i < types.Count; i++)
      {
        formals[i] = mdDecoder.Name(types[i]);
      }
      return formals;
    }

    private static TypeName[] Translate<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Microsoft.Research.DataStructures.IIndexable<Type> types, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      TypeName[] result = new TypeName[types.Count];
      for (int i = 0; i < types.Count; i++)
      {
        result[i] = TranslateToTypeName(types[i], output, mdDecoder);
      }
      return result;
    }

    private static TypeName TranslateToTypeName<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>(Type type, IOutput output, IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      // case analysis over kind of type we have here.
      if (mdDecoder.IsArray(type)) {
        TypeName elementType = TranslateToTypeName(mdDecoder.ElementType(type), output, mdDecoder);
        return elementType.SzArrayType;
      }
      if (mdDecoder.IsManagedPointer(type)) {
        TypeName elementType = TranslateToTypeName(mdDecoder.ElementType(type), output, mdDecoder);
        return elementType.ManagedPointerType;
      }
      if (mdDecoder.IsUnmanagedPointer(type)) {
        TypeName elementType = TranslateToTypeName(mdDecoder.ElementType(type), output, mdDecoder);
        return elementType.PointerType;
      }
      if (mdDecoder.IsFormalTypeParameter(type))
      {
        return TypeName.MakeGenericTypeParameter(mdDecoder.NormalizedFormalTypeParameterIndex(type));
      }
      if (mdDecoder.IsMethodFormalTypeParameter(type))
      {
        return TypeName.MakeGenericMethodParameter(mdDecoder.MethodFormalTypeParameterIndex(type));
      }
      Microsoft.Research.DataStructures.IIndexable<Type> typeArgs;
      if (mdDecoder.IsSpecialized(type, out typeArgs))
      {
        TypeDefinitionName genericType = Translate(mdDecoder.Unspecialized(type), output, mdDecoder);
        TypeName[] typeArgNames = Translate(typeArgs, output, mdDecoder);
        return genericType.Instantiate(typeArgNames);
      }
      // TODO: handle optional/required modifiers and generic type instances and type parameters

      TypeDefinitionName tdef = Translate(type, output, mdDecoder);
      return tdef.Instantiate();
    }
#endif
  }


  struct Precondition
  {
    readonly string message;
    readonly int rank;

    public Precondition(string message, int rank)
    {
      this.message = message;
      this.rank = rank;
    }

    public override int GetHashCode()
    {
      return this.message.GetHashCode();
    }

    public string Message
    {
      get
      {
      return this.message;
      }
    }

    public int Rank
    {
      get
      {
        return this.rank;
      }
    }


  }
}
