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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Cci;
using Microsoft.Cci.Contracts;
using System.IO;
using System.Diagnostics.Contracts;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Research.DataStructures;
using Microsoft.Cci.ILToCodeModel;

[assembly: ContractVerification(true)]

namespace CCDoc
{
  /// <summary>
  /// The custom options used by Program
  /// </summary>
  class Options : OptionParsing
  {
    #region Public options

    [OptionDescription("The name of the assembly that will be used to draw contract information from. NOTE: This is usually called something like: foo.contracts.dll.", Required = true)]
    public string assembly = null;

    [OptionDescription("The name of the XML reference file that this program will add contract information to. NOTE: If none is provided, a new XML file will be generated using the name of the given assembly file.")]
    public string xmlFile = null;

    [OptionDescription("Emit contracts into summary elements instead of individual contract elements so they can be read by IntelliSense in Visual Studios. NOTE: This should be left false (it's default value) if you plan to make a help file with the output.")]
    public bool toSummary = false;

    [OptionDescription("Output is writen to \"buildOutput.txt\" instead of the console.")]
    public bool outToFile = false;

    //[OptionDescription("")]
    //public List<string> libpaths = new List<string>();

    #endregion
  }

  /// <summary>
  /// The primary class of CCDoc.
  /// </summary>
  public class Program
  {
    /// <summary>
    /// The main entry point for the program
    /// </summary>
    /// <param name="args">An assembly to be loaded by the program</param>
    public static void Main(string[] args)
    {
      var options = new Options();
      options.Parse(args);

      if (options.HasErrors)
      {
        if (options.HelpRequested) options.PrintOptions("");
        Environment.Exit(-1);
      }

      HostEnvironment host = new HostEnvironment();
      IModule module = host.LoadUnitFrom(options.assembly) as IModule;
      if (module == null || module == Dummy.Module || module == Dummy.Assembly)
      {
        Console.WriteLine("'{0}' is not a PE file containing a CLR module or assembly.", options.assembly);
        return;
      }

      if (options.outToFile)
      {
        StreamWriter consoleStream = new StreamWriter("buildOutput.txt");
        Console.SetOut(consoleStream);
      }

      string xmlDocSaveName;
      XDocument xDoc = null;	//TODO: Use XmlReader instead of LINQ to XML
      XElement membersElement = null;
      if (!String.IsNullOrEmpty(options.xmlFile))
      {
        //Load the XML File
        try
        {
          xDoc = XDocument.Load(options.xmlFile);
        }
        catch
        {
          Console.WriteLine(options.xmlFile + " is not a XML file.");
          return;
        }
        var docEl = xDoc.Element("doc");	//Navigate to "doc"
        if (docEl == null)
        {
          Console.WriteLine(options.xmlFile + " is not a valid XML reference file; it does not contain a \"doc\" element.");
          return;
        }
        membersElement = docEl.Element("members");	//Navigate to "members"
        if (membersElement == null)
        {
          Console.WriteLine(options.xmlFile + " is not a valid XML reference file; it does not contain a \"members\" element.");
          return;
        }
        xmlDocSaveName = options.xmlFile;
      }
      else
      {
        //Build a new XML File
        XDeclaration xDeclaration = new XDeclaration("1.0", null, null);
        membersElement = new XElement("members");
        xDoc = new XDocument(xDeclaration,
          new XElement("doc",
             membersElement));

        // membersElement = xDoc.Element("doc").Element("members");

        string fileName = options.assembly;
        fileName = fileName.TrimEnd(".dll".ToCharArray());
        xmlDocSaveName = fileName + ".xml";
      }

      //Establish the traverser
      var contractMethods = new ContractMethods(host);
      IContractProvider contractProvider = new Microsoft.Cci.ILToCodeModel.LazyContractProvider(host, module, contractMethods);
      IMetadataVisitor traverser = new ContractTraverser(host, contractProvider, membersElement, options);
      traverser.Visit(module);

      xDoc.Save(xmlDocSaveName, SaveOptions.None);

      Console.Out.Close();
    }
  }

  /// <summary>
  /// Responsible for traversing ContractMethods
  /// </summary>
  sealed class ContractTraverser : BaseMetadataTraverser
  {
    /// <summary>
    /// The current IContractProvider
    /// </summary>
    IContractProvider contractProvider;
    /// <summary>
    /// The XML tree that contains all the types(T:), methods(M:), etc...
    /// </summary>
    XElement membersElement;
    /// <summary>
    /// Options brought in from the command line arguements and parsed by the OptionParsing class (See DataStructures9).
    /// </summary>
    Options options;
    /// <summary>
    /// The host environment of the traverser
    /// </summary>
    HostEnvironment host;

    [ContractInvariantMethod]
    void Invariants()
    {
      Contract.Invariant(contractProvider != null);
      Contract.Invariant(membersElement != null);
      Contract.Invariant(options != null);
    }

    /// <summary>
    /// Creates a ContractTraverser
    /// </summary>
    /// <param name="contractProvider">The contract provider.</param>
    /// <param name="membersElement">The XElement of the "members" element in a XML file</param>
    /// <param name="options">The options.</param>
    /// <param name="host">The host environment of the traverser. Used to pull inherited contracts.</param>
    public ContractTraverser(HostEnvironment host, IContractProvider contractProvider, XElement membersElement, Options options)
    {
      Contract.Requires(contractProvider != null);
      Contract.Requires(membersElement != null);
      Contract.Requires(options != null);

      this.contractProvider = contractProvider;
      this.membersElement = membersElement;
      this.options = options;
      this.host = host;
    }

    /// <summary>
    /// Listen to all the ITypeDefinitions
    /// </summary>
    /// <param name="typeDefinition"></param>
    public override void Visit(ITypeDefinition typeDefinition)
    {

      if (typeDefinition == null) return;

      string typeName = TypeHelper.GetTypeName(typeDefinition, NameFormattingOptions.TypeParameters);
      Console.WriteLine(typeName);

      ITypeContract typeContract = this.contractProvider.GetTypeContractFor(typeDefinition);
      if (typeContract != null)
      {
        XElement member = GetMemberElement(membersElement, typeDefinition);

        if (options.toSummary == false)
        {
          foreach (IContractElement contractElement in typeContract.Invariants)
          {
            Contract.Assume(contractElement != null, "Can't prove for now");
            WriteContractElement(member, contractElement, "invariant", GetDescriptionAttribute(contractElement));
          }
        }
        else
        {
          XElement summaryElement = GetSummaryElement(member);

          summaryElement.Add(new XElement("para", "Invariants: "));

          foreach (IContractElement contractElement in typeContract.Invariants)
          {
            Contract.Assume(contractElement != null, "Can't prove this for now");
            WriteContractElementToSummary(summaryElement, contractElement);
          }
        }
      }

      base.Visit(typeDefinition);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyDefinition"></param>
    public override void Visit(IPropertyDefinition propertyDefinition)
    {
      string propertyName = MemberHelper.GetMemberSignature(propertyDefinition, NameFormattingOptions.DocID);
      Console.WriteLine(propertyName);

      IMethodContract getterContract = null;
      IMethodContract setterContract = null;
      if (propertyDefinition.Getter != null)
        getterContract = this.contractProvider.GetMethodContractFor(propertyDefinition.Getter);
      if (propertyDefinition.Setter != null)
        setterContract = this.contractProvider.GetMethodContractFor(propertyDefinition.Setter);

      XElement member = null;
      XElement summaryElement = null;
      if (getterContract != null || setterContract != null)
      {
        member = GetMemberElement(membersElement, propertyDefinition);
        summaryElement = options.toSummary ? GetSummaryElement(member) : null;
      }

      if (getterContract != null)
      {
        Console.WriteLine("Getter: ");

        //Add contract exceptions
        WriteExceptions(member, getterContract);

        if (!options.toSummary)
        {
          XElement getterElement = new XElement("getter");
          member.Add(getterElement);

          foreach (IPrecondition contractElement in getterContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElement(getterElement, contractElement, "requires", GetDescriptionAttribute(contractElement), contractElement.ExceptionToThrow != null ? GetTypeAttribute("exception", contractElement.ExceptionToThrow.Type) : null);
          }
          foreach (IContractElement contractElement in getterContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElement(getterElement, contractElement, "ensures", GetDescriptionAttribute(contractElement));
          }
          foreach (IThrownException thrownException in getterContract.ThrownExceptions)
          {
            Contract.Assume(thrownException != null, "lacking contract for elements");
            Contract.Assume(thrownException.Postcondition != null, "lack of contracts on CCI2");
            WriteContractElement(getterElement, thrownException.Postcondition, "ensuresOnThrow", GetDescriptionAttribute(thrownException.Postcondition), GetTypeAttribute("exception", thrownException.ExceptionType));
          }
        }
        else
        {
          summaryElement.Add(new XElement("para", "Get"));

          summaryElement.Add(new XElement("para", "Requires: "));
          foreach (IContractElement contractElement in getterContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElementToSummary(summaryElement, contractElement);
          }

          summaryElement.Add(new XElement("para", "Ensures: "));
          foreach (IContractElement contractElement in getterContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElementToSummary(summaryElement, contractElement);
          }

          summaryElement.Add(new XElement("para", "EnsuresOnThrow: "));
          foreach (ThrownException thrownException in getterContract.ThrownExceptions)
          {
            Contract.Assume(thrownException != null, "lacking contract for elements");
            Contract.Assume(thrownException.Postcondition != null, "lack of contracts on CCI2");
            WriteContractElementToSummary(summaryElement, thrownException.Postcondition, thrownException.ExceptionType.ToString());
          }

        }
      }
      if (setterContract != null)
      {
        Console.WriteLine("Setter: ");

        //Add contract exceptions
        WriteExceptions(member, setterContract);

        if (!options.toSummary)
        {
          XElement setterElement = new XElement("setter");
          member.Add(setterElement);

          foreach (IPrecondition contractElement in setterContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElement(setterElement, contractElement, "requires", GetDescriptionAttribute(contractElement), contractElement.ExceptionToThrow != null ? GetTypeAttribute("exception", contractElement.ExceptionToThrow.Type) : null);
          }
          foreach (IContractElement contractElement in setterContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElement(setterElement, contractElement, "ensures", GetDescriptionAttribute(contractElement));
          }
          foreach (IThrownException thrownException in setterContract.ThrownExceptions)
          {
            Contract.Assume(thrownException.Postcondition != null, "lack of contracts on CCI2");
            Contract.Assume(thrownException != null, "lacking contract for elements");
            WriteContractElement(setterElement, thrownException.Postcondition, "ensuresOnThrow", GetDescriptionAttribute(thrownException.Postcondition), GetTypeAttribute("exception", thrownException.ExceptionType));
          }
        }
        else
        {
          summaryElement.Add(new XElement("para", "Set"));

          summaryElement.Add(new XElement("para", "Requires: "));
          foreach (IContractElement contractElement in setterContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElementToSummary(summaryElement, contractElement);
          }

          summaryElement.Add(new XElement("para", "Ensures: "));
          foreach (IContractElement contractElement in setterContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lacking contract for elements");
            WriteContractElementToSummary(summaryElement, contractElement);
          }
          summaryElement.Add(new XElement("para", "EnsuresOnThrow: "));
          foreach (ThrownException thrownException in setterContract.ThrownExceptions)
          {
            Contract.Assume(thrownException != null, "lacking contract for elements");
            Contract.Assume(thrownException.Postcondition != null, "lack of contracts on CCI2");
            WriteContractElementToSummary(summaryElement, thrownException.Postcondition, thrownException.ExceptionType.ToString());
          }
        }


      }

      base.Visit(propertyDefinition);
    }

    /// <summary>
    /// Listen to all the IMethodDefinitions
    /// </summary>
    /// <param name="methodDefinition"></param>
    public override void Visit(IMethodDefinition methodDefinition)
    {
      if (methodDefinition == null) return;

      if (IsGetter(methodDefinition) || IsSetter(methodDefinition))
        return;

      string methodName = MemberHelper.GetMethodSignature(methodDefinition, NameFormattingOptions.DocID);
      Console.WriteLine(methodName);

      IMethodContract methodContract = this.contractProvider.GetMethodContractFor(methodDefinition);
      //IMethodContract inheritedContract = ContractHelper.InheritMethodContracts(this.host, this.contractProvider, methodDefinition);
      if (methodContract != null)
      {
        XElement member = GetMemberElement(membersElement, methodDefinition);

        //Add contract exceptions
        WriteExceptions(member, methodContract);

        //Add a new element for each contract
        if (options.toSummary == false)
        {
          foreach (IPrecondition contractElement in methodContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            XAttribute exceptionAttribute = null;
            if (contractElement.ExceptionToThrow != null)
            {
              ITypeOf asTypeOf = contractElement.ExceptionToThrow as ITypeOf;
              if (asTypeOf != null)
                exceptionAttribute = GetTypeAttribute("exception", asTypeOf.TypeToGet);
            }
            WriteContractElement(member, contractElement, "requires", GetDescriptionAttribute(contractElement), exceptionAttribute);
          }
          foreach (IPostcondition contractElement in methodContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElement(member, contractElement, "ensures", GetDescriptionAttribute(contractElement));
          }
          foreach (IThrownException thrownException in methodContract.ThrownExceptions)
          {
            Contract.Assume(thrownException != null, "lack of contracts for collections");
            Contract.Assume(thrownException.Postcondition != null, "lack of CCI2 contracts");
            WriteContractElement(member, thrownException.Postcondition, "ensuresOnThrow", GetDescriptionAttribute(thrownException.Postcondition), GetTypeAttribute("exception", thrownException.ExceptionType));
          }
        }
        //Add contract information into the summary element
        else
        {
          XElement summaryElement = GetSummaryElement(member);

          summaryElement.Add(new XElement("para", "Requires: "));
          foreach (IContractElement contractElement in methodContract.Preconditions)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElementToSummary(summaryElement, contractElement);
          }

          summaryElement.Add(new XElement("para", "Ensures: "));
          foreach (IContractElement contractElement in methodContract.Postconditions)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElementToSummary(summaryElement, contractElement);
          }

          summaryElement.Add(new XElement("para", "EnsuresOnThrow: "));
          foreach (ThrownException thrownException in methodContract.ThrownExceptions)
          {
            Contract.Assume(thrownException != null, "lack of contracts for collections");
            Contract.Assume(thrownException.Postcondition != null, "lack of contracts for CCI2");
            Contract.Assume(thrownException.ExceptionType != null, "lack of contracts for CCI2");
            WriteContractElementToSummary(summaryElement, thrownException.Postcondition, thrownException.ExceptionType.ToString());
          }

          WriteContractElementsToReleventParameters(member, methodDefinition, methodContract);
        }
      }

      base.Visit(methodDefinition);
    }

    XElement GetMemberElement(XElement members, ITypeDefinitionMember typeDefinitionMember)
    {
      Contract.Requires(members != null);
      Contract.Requires(typeDefinitionMember != null);
      Contract.Ensures(Contract.Result<XElement>() != null);

      XElement member = GetMemberElement(members, MemberHelper.GetMemberSignature(typeDefinitionMember, NameFormattingOptions.DocID), true);

      return member;
    }
    XElement GetMemberElement(XElement members, ITypeReference typeReference)
    {
      Contract.Requires(members != null);
      Contract.Requires(typeReference != null);
      Contract.Ensures(Contract.Result<XElement>() != null);

      XElement member = GetMemberElement(members, TypeHelper.GetTypeName(typeReference, NameFormattingOptions.DocID), true);

      return member;
    }
    XElement GetMemberElement(XElement members, string id, bool canCreateNew)
    {
      Contract.Requires(members != null);
      Contract.Requires(id != null);
      Contract.Ensures(!canCreateNew || Contract.Result<XElement>() != null);


      //Select all XElements with the method ID of methodDefinition (should be only one)
      IEnumerable<XElement> els =
        from el in members.Elements("member")
        where String.Equals(el.Attribute("name").Value, id, StringComparison.Ordinal)//TODO: Use Ordinal compare?
        select el;

      //Contract.Assert(els.Count<XElement>() == 1);

      XElement member = null;
      foreach (XElement e in els)
        member = e;

      if (canCreateNew && member == null)
      {
        Console.WriteLine(id + " doesn't exist, adding to xml file...");

        member = new XElement("member");
        member.SetAttributeValue("name", id);
        members.Add(member);
      }

      return member;
    }

    XElement GetSummaryElement(XElement member)
    {
      Contract.Requires(member != null);

      XElement summaryElement = member.Element("summary");

      //Create a summary element if one doesn't exist
      if (summaryElement == null)
      {
        summaryElement = new XElement("summary");
        member.Add(summaryElement);
      }

      return summaryElement;
    }
    XElement GetParamElement(XElement member, IParameterDefinition param)
    {
      Contract.Requires(member != null);
      Contract.Ensures(Contract.Result<XElement>() != null);

      foreach (var el in member.Elements("param"))
        if (el.Attribute("name").Value == param.Name.Value)
          return el;

      var paramElement = new XElement("param");
      paramElement.SetAttributeValue("name", param.Name);

      return paramElement;
    }

    void WriteContractElement(XElement member, IContractElement contractElement, string contractElementName, params XAttribute[] contractAttributes)
    {
      Contract.Requires(member != null);
      Contract.Requires(contractElement != null);
      Contract.Requires(!String.IsNullOrEmpty(contractElementName));

      if (contractElement == null) return;

      XElement contractXElement = new XElement(contractElementName, contractElement.OriginalSource);

      if (contractAttributes != null)
        foreach (XAttribute contractAttribute in contractAttributes)
          if (contractAttribute != null)
            contractXElement.SetAttributeValue(contractAttribute.Name.LocalName, contractAttribute.Value);

      Console.WriteLine("\t\t" + contractElementName + ": " + contractElement.OriginalSource);

      member.Add(contractXElement);
    }

    void WriteContractElementToSummary(XElement summaryElement, IContractElement contractElement, params string[] info)
    {
      Contract.Requires(summaryElement != null);
      Contract.Requires(contractElement != null);
      Contract.Requires(info != null);

      StringBuilder infoBuilder = new StringBuilder(contractElement.OriginalSource);
      foreach (string infoString in info)
      {
        if (infoString != null)
        {
          infoBuilder.Append(" (");
          infoBuilder.Append(infoString);
          infoBuilder.Append(")");
        }
      }

      XElement contractXElement = new XElement("para", infoBuilder.ToString());

      summaryElement.Add(contractXElement);

      Console.WriteLine("\t\t" + infoBuilder.ToString());
    }

    void WriteException(XElement member, ITypeReference exceptionType, string exceptionCondition)
    {
      Contract.Requires(member != null);
      Contract.Requires(exceptionType != null);

      //TODO: Check if exception element already exists?

      XElement exceptionElement = new XElement("exception", exceptionCondition);
      exceptionElement.SetAttributeValue("cref", TypeHelper.GetTypeName(exceptionType, NameFormattingOptions.DocID));

      member.Add(exceptionElement);
    }
    void WriteExceptions(XElement member, IMethodContract methodContract)
    {
      Contract.Requires(member != null);
      Contract.Requires(methodContract != null);
      Contract.Assume(methodContract.Preconditions != null, "lack of CCI2 contracts");

      foreach (IPrecondition pre in methodContract.Preconditions)
      {
        if (pre.ExceptionToThrow != null)
        {
          Contract.Assume(pre.ExceptionToThrow.Type != null, "lack of CCI2 contracts");

          string conditionText = "";
          if (pre.OriginalSource != null)
          {
            conditionText = String.Format("If \"{0}\".", DocHelper.NegatePredicate(pre.OriginalSource));
          }
          WriteException(member, pre.ExceptionToThrow.Type, conditionText);
        }
      }

      foreach (IThrownException exc in methodContract.ThrownExceptions)
      {
        Contract.Assume(exc.ExceptionType != null, "lack of CCI2 contracts");

        string conditionText = "";
        if (exc.Postcondition.OriginalSource != null)
        {
          conditionText = String.Format("\"{0}\" will be true.", exc.Postcondition.OriginalSource);
        }
        WriteException(member, exc.ExceptionType, conditionText);
      }
    }

    XAttribute GetDescriptionAttribute(IContractElement contractElement)
    {
      if (contractElement == null) return null;

      string contractDescription = null;

      ICompileTimeConstant descAsCTC = contractElement.Description as ICompileTimeConstant;
      if (descAsCTC != null && descAsCTC.Value != null)
        contractDescription = descAsCTC.Value as string;

      if (contractDescription != null)
        return new XAttribute("description", contractDescription);
      else
        return null;
    }
    XAttribute GetTypeAttribute(string name, ITypeReference type)
    {
      Contract.Requires(type != null);

      return new XAttribute(name, TypeHelper.GetTypeName(type, NameFormattingOptions.DocID));
    }

    IEnumerable<IContractElement> GetReleventContracts<TContractElement>(IParameterDefinition parameter, IEnumerable<TContractElement> contractElements) where TContractElement : IContractElement
    {
      Contract.Requires(parameter != null);
      Contract.Requires(contractElements != null);

      return from contractElement in contractElements
             where (contractElement as IContractElement).OriginalSource.Contains(parameter.Name.Value)
             select (contractElement as IContractElement);
    }
    void WriteContractElementsToReleventParameters(XElement member, IMethodDefinition methodDefinition, IMethodContract methodContract)
    {
      Contract.Requires(member != null);
      Contract.Requires(methodContract != null);
      Contract.Requires(methodDefinition != null);

      Contract.Assume(methodDefinition.Parameters != null, "lack of CCI2 contracts");
      Contract.Assume(methodContract.Preconditions != null, "lack of CCI2 contracts");
      Contract.Assume(methodContract.Postconditions != null, "lack of CCI2 contracts");
      Contract.Assume(methodContract.ThrownExceptions != null, "lack of CCI2 contracts");

      foreach (IParameterDefinition param in methodDefinition.Parameters)
      {
        Contract.Assume(param != null, "lacking contracts for collection elements");

        var releventRequires = GetReleventContracts<IPrecondition>(param, methodContract.Preconditions);
        if (releventRequires != null)
        {
          XElement paramElement = GetParamElement(member, param);
          paramElement.Add(new XElement("para", "Requires:"));
          foreach (var contractElement in releventRequires)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElementToSummary(paramElement, contractElement);
          }
        }

        var releventEnsures = GetReleventContracts<IPostcondition>(param, methodContract.Postconditions);
        if (releventEnsures != null)
        {
          XElement paramElement = GetParamElement(member, param);
          paramElement.Add(new XElement("para", "Ensures:"));
          foreach (var contractElement in releventEnsures)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElementToSummary(paramElement, contractElement);
          }
        }

        var releventEnsuresOnThrow = GetReleventContracts<IPostcondition>(param, from thrownException in methodContract.ThrownExceptions select thrownException.Postcondition);
        if (releventEnsuresOnThrow != null)
        {
          XElement paramElement = GetParamElement(member, param);
          paramElement.Add(new XElement("para", "EnsuresOnThrow:"));
          foreach (var contractElement in releventEnsuresOnThrow)
          {
            Contract.Assume(contractElement != null, "lack of contracts for collections");
            WriteContractElementToSummary(paramElement, contractElement);
          }
        }
      }
    }

    bool IsGetter(IMethodDefinition methodDefinition)
    {
      Contract.Requires(methodDefinition != null);

      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("get_");
    }
    bool IsSetter(IMethodDefinition methodDefinition)
    {
      Contract.Requires(methodDefinition != null);

      return methodDefinition.IsSpecialName && methodDefinition.Name.Value.StartsWith("set_");
    }
  }

  /// <summary>
  /// Inherited from MetadataReaderHost
  /// </summary>
  internal class HostEnvironment : MetadataReaderHost
  {
    PeReader peReader;
    internal HostEnvironment()
    {
      this.peReader = new PeReader(this);
    }
    public override IUnit LoadUnitFrom(string location)
    {
      IUnit result = this.peReader.OpenModule(
        BinaryDocument.GetBinaryDocumentForFile(location, this));
      this.RegisterAsLatest(result);
      return result;
    }
  }

  internal class DocHelper
  {
    #region Code from BrianGru to negate predicates coming from if-then-throw preconditions
    // Recognize some common predicate forms, and negate them.  Also, fall back to a correct default.
    [ContractVerification(true)]
    public static String NegatePredicate(String predicate)
    {
      if (String.IsNullOrEmpty(predicate)) return "";
      if (predicate.Length < 2) return "!" + predicate;

      // "(p)", but avoiding stuff like "(p && q) || (!p)"
      if (predicate[0] == '(' && predicate[predicate.Length - 1] == ')')
      {
        if (predicate.IndexOf('(', 1) == -1)
          return '(' + NegatePredicate(predicate.Substring(1, predicate.Length - 2)) + ')';
      }

      // "!p"
      if (predicate[0] == '!' && (ContainsNoOperators(predicate, 1, predicate.Length - 1) || IsSimpleFunctionCall(predicate, 1, predicate.Length - 1)))
        return predicate.Substring(1);

      // "a < b" or "a <= b"
      int ltIndex = predicate.IndexOf('<');
      if (ltIndex >= 0)
      {
        int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
        bool ltOrEquals = ltIndex < bEnd - 1 ? predicate[ltIndex + 1] == '=' : false;
        aEnd = ltIndex;
        bStart = ltOrEquals ? ltIndex + 2 : ltIndex + 1;

        String a = predicate.Substring(aStart, aEnd - aStart);
        String b = predicate.Substring(bStart, bEnd - bStart);
        if (ContainsNoOperators(a) && ContainsNoOperators(b))
          return a + (ltOrEquals ? ">" : ">=") + b;
      }

      // "a > b" or "a >= b"
      int gtIndex = predicate.IndexOf('>');
      if (gtIndex >= 0)
      {
        int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
        bool gtOrEquals = gtIndex < bEnd - 1 ? predicate[gtIndex + 1] == '=' : false;
        aEnd = gtIndex;
        bStart = gtOrEquals ? gtIndex + 2 : gtIndex + 1;

        String a = predicate.Substring(aStart, aEnd - aStart);
        String b = predicate.Substring(bStart, bEnd - bStart);
        if (ContainsNoOperators(a) && ContainsNoOperators(b))
          return a + (gtOrEquals ? "<" : "<=") + b;
      }

      // "a == b"  or  "a != b"
      int eqIndex = predicate.IndexOf('=');
      if (eqIndex >= 0)
      {
        int aStart = 0, aEnd = -1, bStart = -1, bEnd = predicate.Length;
        bool skip = false;
        bool equalsOperator = false;
        if (eqIndex > 0 && predicate[eqIndex - 1] == '!')
        {
          aEnd = eqIndex - 1;
          bStart = eqIndex + 1;
          equalsOperator = false;
        }
        else if (eqIndex < bEnd - 1 && predicate[eqIndex + 1] == '=')
        {
          aEnd = eqIndex;
          bStart = eqIndex + 2;
          equalsOperator = true;
        }
        else
          skip = true;

        if (!skip)
        {
          String a = predicate.Substring(aStart, aEnd - aStart);
          String b = predicate.Substring(bStart, bEnd - bStart);
          if (ContainsNoOperators(a) && ContainsNoOperators(b))
            return a + (equalsOperator ? "!=" : "==") + b;
        }
      }

      if (predicate.Contains("&&") || predicate.Contains("||"))
      {
        // Consider predicates like "(P) && (Q)", "P || Q", "(P || Q) && R", etc.
        // Apply DeMorgan's law, and recurse to negate both sides of the binary operator.
        int aStart = 0, aEnd, bStart, bEnd = predicate.Length;
        int parenCount = 0;
        bool skip = false;
        bool foundAnd = false, foundOr = false;
        aEnd = 0;
        while (aEnd < predicate.Length && ((predicate[aEnd] != '&' && predicate[aEnd] != '|') || parenCount > 0))
        {
          if (predicate[aEnd] == '(')
            parenCount++;
          else if (predicate[aEnd] == ')')
            parenCount--;
          aEnd++;
        }
        if (aEnd >= predicate.Length - 1)
          skip = true;
        else
        {
          if (aEnd + 1 < predicate.Length && predicate[aEnd] == '&' && predicate[aEnd + 1] == '&')
            foundAnd = true;
          else if (aEnd + 1 < predicate.Length && predicate[aEnd] == '|' && predicate[aEnd + 1] == '|')
            foundOr = true;
          if (!foundAnd && !foundOr)
            skip = true;
        }

        if (!skip)
        {
          bStart = aEnd + 2;
          while (aEnd > 0 && Char.IsWhiteSpace(predicate[aEnd - 1]))
            aEnd--;
          while (Char.IsWhiteSpace(predicate[bStart]))
            bStart++;

          String a = predicate.Substring(aStart, aEnd - aStart);
          String b = predicate.Substring(bStart, bEnd - bStart);
          String op = foundAnd ? " || " : " && ";
          return NegatePredicate(a) + op + NegatePredicate(b);
        }
      }

      return String.Format("!({0})", predicate);
    }
    private static bool ContainsNoOperators(String s)
    {
      Contract.Requires(s != null);
      return ContainsNoOperators(s, 0, s.Length);
    }
    // These aren't operators like + per se, but ones that will cause evaluation order to possibly change,
    // or alter the semantics of what might be in a predicate.
    // @TODO: Consider adding '~'
    static readonly String[] Operators = new String[] { "==", "!=", "=", "<", ">", "(", ")", "//", "/*", "*/" };
    private static bool ContainsNoOperators(String s, int start, int end)
    {
      Contract.Requires(s != null);
      foreach (String op in Operators)
      {
        Contract.Assume(op != null, "lack of contract support for collections");
        if (s.IndexOf(op) >= 0)
          return false;
      }
      return true;
    }
    private static bool ArrayContains<T>(T[] array, T item)
    {
      Contract.Requires(array != null);

      foreach (T x in array)
        if (item.Equals(x))
          return true;
      return false;
    }
    // Recognize only SIMPLE method calls, like "System.String.Equals("", "")".
    private static bool IsSimpleFunctionCall(String s, int start, int end)
    {
      Contract.Requires(s != null);
      Contract.Requires(start >= 0);
      Contract.Requires(end <= s.Length);
      Contract.Requires(end >= 0);
      char[] badChars = { '+', '-', '*', '/', '~', '<', '=', '>', ';', '?', ':' };
      int parenCount = 0;
      int index = start;
      bool foundMethod = false;
      for (; index < end; index++)
      {
        if (s[index] == '(')
        {
          parenCount++;
          if (parenCount > 1)
            return false;
          if (foundMethod == true)
            return false;
          foundMethod = true;
        }
        else if (s[index] == ')')
        {
          parenCount--;
          if (index != end - 1)
            return false;
        }
        else if (ArrayContains(badChars, s[index]))
          return false;
      }
      return foundMethod;
    }
    #endregion Code from BrianGru to negate predicates coming from if-then-throw preconditions
  }
}
