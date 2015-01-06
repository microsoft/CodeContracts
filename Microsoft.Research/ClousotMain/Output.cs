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
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Research.CodeAnalysis {

  static class OutputExtensions
  {
    public static APC MapPrimaryCausePC(this APC apc)
    {
      var context = apc.SubroutineContext;
      while (context != null)
      {
        var head = context.Head;
        switch (head.Three)
        {
          case "beforeCall":
            return APC.ForStart(head.Two, context.Tail);
        }
        context = context.Tail;
      }
      return apc;
    }

    public static TextWriter ToTextWriter<Method, Assembly>(this IOutputFullResults<Method, Assembly> output)
    {
      Contract.Requires(output != null);

      return new WriteToOutput<Method, Assembly>(output);
    }

    private class WriteToOutput<Method, Assembly> : TextWriter
    {
      private readonly IOutputFullResults<Method, Assembly> output;

      public WriteToOutput(IOutputFullResults<Method, Assembly> output)
        : base()
      {
        Contract.Requires(output != null);

        this.output = output;
      }

      public override Encoding Encoding
      {
        get { return System.Console.OutputEncoding; }
      }

      public override void WriteLine(string value)
      {
        output.WriteLine(string.Format("{1}", DateTime.Now, value.Replace("{", "{{").Replace("}","}}")));
        //output.WriteLine(string.Format("[{0}] {1}", DateTime.Now., value));
      }

      public override void Write(string value)
      {
#if DEBUG
        Contract.Assume(false, "We do not want you to call Write Directly!!!");
#endif
        var sanitized = value != null ? value.Replace("{", "{{").Replace("}", "}}") : "<null>";
        output.WriteLine(sanitized);
      }

    }

  }

  class XMLWriterOutput<Method, Assembly> : IOutputFullResults<Method, Assembly>
  {

    #region Object invariant

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.xmlwriter != null);
      Contract.Invariant(this.scoresManager != null);
    }
    
    #endregion

    XmlWriter xmlwriter;
    ILogOptions options;
    Converter<Method, string> fullName;
    Converter<Assembly, string> assemblyName;
    string outputname;
    private readonly WarningScoresManager scoresManager;

    public XMLWriterOutput(string outputname, TextWriter tw, ILogOptions options, Converter<Method, string> fullName, Converter<Assembly, string> assemblyName, WarningScoresManager scoresManager)
    {
      Contract.Requires(scoresManager != null);

      this.outputname = outputname;
      this.options = options;
      this.fullName = fullName;
      this.assemblyName = assemblyName;
      this.scoresManager = scoresManager;            
      this.xmlwriter = XmlWriter.Create(tw, new XmlWriterSettings() { Indent = true });
      this.xmlwriter.WriteStartDocument();
      this.xmlwriter.WriteStartElement(StringConstants.XML.Root);
    }

    #region IOutputResults Members

    public void WriteLine(string format, params object[] args)
    {
      try
      {
        this.xmlwriter.WriteElementString(StringConstants.XML.Message, String.Format(format, args));
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }
    }

    public void Statistic(string format, params object[] args)
    {
      try
      {
        this.xmlwriter.WriteElementString(StringConstants.XML.Statistics, String.Format(format, args));
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

    }

    public void FinalStatistic(string assemblyName, string msg)
    {
      try
      {
        this.xmlwriter.WriteStartElement(StringConstants.XML.FinalStatistics);
        this.xmlwriter.WriteAttributeString(StringConstants.XML.Assembly, assemblyName);
        this.xmlwriter.WriteValue(msg);
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

    }

    static string OutcomeToString(ProofOutcome outcome)
    {
      switch (outcome) {
        case ProofOutcome.True: return "true";
        case ProofOutcome.False: return "false";
        case ProofOutcome.Bottom: return "unreached";
        case ProofOutcome.Top: return "unknown";
        default: throw new NotImplementedException();
      }
    }

    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      try
      {
        this.xmlwriter.WriteStartElement(StringConstants.XML.Check);
        EmitOutcomeContent(witness, format, args);
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

      return true;
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      try
      {
        this.xmlwriter.WriteStartElement(StringConstants.XML.Check);
        EmitOutcomeContent(witness, format, args);
        foreach (var related in witness.PC.NonPrimaryRelatedLocations())
        {
          this.xmlwriter.WriteStartElement(StringConstants.XML.RelatedLocation);
          this.xmlwriter.WriteAttributeString(StringConstants.XML.RelatedLocation, related.PrimarySourceContext());
          this.xmlwriter.WriteEndElement();
        }

        foreach (var cause in witness.Trace)
        {
          var primaryPC = cause.Key.MapPrimaryCausePC();

          var sourceContext = primaryPC.HasRealSourceContext ? primaryPC.PrimarySourceContext() : primaryPC.ToString();
          this.xmlwriter.WriteStartElement(StringConstants.XML.Trace);
          this.xmlwriter.WriteAttributeString(StringConstants.XML.SourceLocation, sourceContext);
          this.xmlwriter.WriteString(cause.Value);
          this.xmlwriter.WriteEndElement();
        }
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

      return true;
    }

    private void EmitOutcomeContent(Witness witness, string format, object[] args)
    {
      try
      {
        this.xmlwriter.WriteAttributeString(StringConstants.XML.SourceLocation, witness.PC.PrimarySourceContext());
        this.xmlwriter.WriteAttributeString(StringConstants.XML.Message, String.Format(format, args));

        this.xmlwriter.WriteElementString(StringConstants.XML.Result, OutcomeToString(witness.Outcome));

        var score = witness.GetScore(this.scoresManager);
        
        this.xmlwriter.WriteElementString(StringConstants.XML.Score, score.ToString());
        this.xmlwriter.WriteElementString(StringConstants.XML.ConfidenceLevel, witness.GetWarningLevel(score, this.scoresManager));

        this.xmlwriter.WriteStartElement(StringConstants.XML.Justification);

        
        // Let's be a little more precise if it is a suggestion turned into a warning
        if (witness.Warning == WarningType.Suggestion && witness.TypeOfSuggestionTurnedIntoWarning.HasValue)
        {
          this.xmlwriter.WriteElementString(StringConstants.XML.Feature, StringConstants.XML.SuggestionTurnedIntoWarning+witness.TypeOfSuggestionTurnedIntoWarning.Value.ToString());
        }
        else
        {
          this.xmlwriter.WriteElementString(StringConstants.XML.Feature, witness.Warning.ToString());
        }

        foreach (var feature in witness.GetJustificationList(this.scoresManager))
        {
          this.xmlwriter.WriteElementString(StringConstants.XML.Feature, feature);
        }
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

    }

    private string SanitizeName(string s)
    {
      return s.Replace(" ", "_");
    }

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      try
      {
        this.xmlwriter.WriteStartElement(StringConstants.XML.Suggestion);
        this.xmlwriter.WriteAttributeString(StringConstants.XML.SuggestionKind, type.ToString());
        this.xmlwriter.WriteAttributeString(StringConstants.XML.SourceLocation, pc.PrimarySourceContext());
        this.xmlwriter.WriteAttributeString(StringConstants.XML.Kind, kind);
        this.xmlwriter.WriteAttributeString(StringConstants.XML.Suggested, suggestion);

        var first = true;

        foreach (var justification in extraInfo.GetXml())
        {
          if(first)
          {
            this.xmlwriter.WriteStartElement(StringConstants.XML.SuggestionExtraInfo);
            first = false;
          }
          this.xmlwriter.WriteAttributeString(justification.Item1, justification.Item2);
        }

        if(!first)
        {
          this.xmlwriter.WriteEndElement();
        }
        
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }

    }

    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      try
      {
        this.xmlwriter.WriteStartElement("Error");
        this.xmlwriter.WriteAttributeString("Document", error.FileName);
        this.xmlwriter.WriteAttributeString("Line", error.Line.ToString());
        this.xmlwriter.WriteAttributeString("column", error.Column.ToString());
        this.xmlwriter.WriteAttributeString("text", error.ErrorText);
        this.xmlwriter.WriteEndElement();
      }
      catch (Exception xe)
      {
        Console.WriteLine("XML exception: {0}", xe.ToString());
      }
    }

    public void Close()
    {
      xmlwriter.WriteEndElement();
      xmlwriter.Close();
    }

    public int RegressionErrors()
    {
      return 0;
    }

    IFrameworkLogOptions IOutput.LogOptions { get { return options; } }

    public ILogOptions LogOptions
    {
      get { return options; }
    }

    public int SwallowedMessagesCount(ProofOutcome outcome)
    { 
      return 0;  
    }

    public string Name { get { return this.outputname; } }

    public void StartMethod(Method method)
    {
      xmlwriter.WriteStartElement(StringConstants.XML.Method);
      xmlwriter.WriteAttributeString(StringConstants.XML.Name, fullName(method));
    }

    public bool IsMasked(Witness witness)
    {
      return false;
    }

    public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC = false)
    {
      xmlwriter.WriteEndElement();
    }

    public void StartAssembly(Assembly assembly)
    {
      xmlwriter.WriteStartElement(StringConstants.XML.Assembly);
      xmlwriter.WriteAttributeString(StringConstants.XML.Name, assemblyName(assembly));
    }

    public void EndAssembly()
    {
      xmlwriter.WriteEndElement();
    }
    public bool ErrorsWereEmitted
    {
      get { return false; }
    }

    #endregion

  }

  public class FullTextWriterOutput<Method, Assembly> : TextWriterOutput, IOutputFullResults<Method, Assembly>
  {
    ILogOptions options;
    string outputname;
    bool errorsWereEmitted;

    public FullTextWriterOutput(string outputname, TextWriter tw, ILogOptions options)
      : base(tw, options)
    {
      this.outputname = outputname;
      this.options = options;
      this.errorsWereEmitted = false;
    }

    #region IOutputFullResults<Method, Assembly> Members

    public string Name { get { return this.outputname; } }

    public int SwallowedMessagesCount(ProofOutcome outcome) 
    { 
      return 0; 
    }

    public void StartMethod(Method method)
    {
    }

    public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC = false)
    {
    }

    public bool IsMasked(Witness witness)
    {
      return false;
    }

    public void StartAssembly(Assembly assembly)
    {
    }

    public void EndAssembly()
    {
    }

    public new ILogOptions LogOptions
    {
      get { return options; }
    }

    public bool ErrorsWereEmitted
    {
      get { return this.errorsWereEmitted; }
    }

    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      string tag;
      switch (witness.Outcome)
      {
        case ProofOutcome.Top:
          if (options.WarningsAsErrors)
          {
            tag = " error :";
            this.errorsWereEmitted = true;
          }
          else
          {
            tag = " warning :";
          }
          break;
        case ProofOutcome.False:
          if (options.WarningsAsErrors)
          {
            tag = " error :";
            this.errorsWereEmitted = true;
          }
          else
          {
            tag = " warning :";
          }
          break; // keep it a warning to not confuse VS
        default: tag = ""; break;
      }
      
      string sourceContext;
      var primaryMethodLocation = witness.PC.PrimaryMethodLocation();
      if(primaryMethodLocation.Equals(APC.Dummy))
      {
        sourceContext = "no_source_context";
      }
      else
      {
        sourceContext = primaryMethodLocation.PrimarySourceContext();
      }

      base.WriteLine("{0}:{2} {1}", sourceContext, String.Format(format, args), tag);

      return true;
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
        /* var result = */
        EmitOutcome(witness, format, args);
        foreach (var related in witness.PC.NonPrimaryRelatedLocations())
        {
          // added warning so that VS shows it
          // only show if related is real source context
          if (related.HasRealSourceContext)
          {
            base.WriteLine("{0}: warning : location related to previous warning", related.PrimarySourceContext());
          }
        }

        foreach (var cause in witness.Trace)
        {
          var primaryPC = cause.Key.MapPrimaryCausePC();
          var sourceContext = primaryPC.HasRealSourceContext ? primaryPC.PrimarySourceContext() : primaryPC.ToString();

          if (this.options.IsRegression)
          {
            // force the output as suggestion only in regression mode
            this.Suggestion(ClousotSuggestion.Kind.InterMethodTrace, ClousotSuggestion.Kind.InterMethodTrace.Message(),
              primaryPC, cause.Value, null, ClousotSuggestion.ExtraSuggestionInfo.None);
          }
          else
          {
            base.WriteLine("{0}: {1}", sourceContext, cause.Value);
          }
        }
      
        return true;
    }


    public int RegressionErrors()
    {
      return 0;
    }


    #endregion



  }

  public class FullTextWriterOutputFactory<Method, Assembly> : IOutputFullResultsFactory<Method, Assembly>
  {
    private readonly TextWriter textWriter;
    private readonly string outputName;

    public FullTextWriterOutputFactory(TextWriter textWriter, string outputName = null)
    {
      this.textWriter = textWriter;
      this.outputName = outputName;
    }

    public IOutputFullResults<Method, Assembly> GetOutputFullResultsProvider(ILogOptions options)
    {
      var outputName = this.outputName;
      if (outputName == null)
      {
        var generalOptions = options as GeneralOptions;
        if (generalOptions != null)
          outputName = generalOptions.OutFileName;
        else
          outputName = "FullTextWriterOutput";
      }

      return new FullTextWriterOutput<Method, Assembly>(outputName, this.textWriter, options);
    }
  }

  public class IgnoreOutputFactory<Method, Assembly> : IOutputFullResultsFactory<Method, Assembly>
  {
    public IOutputFullResults<Method, Assembly> GetOutputFullResultsProvider(ILogOptions options)
    {
      return new IgnoreOutput(options);
    }

    private class IgnoreOutput : IOutputFullResults<Method, Assembly>
    {
      private readonly ILogOptions options;

      internal IgnoreOutput(ILogOptions options)
      {
        this.options = options;
      }
      
      public string Name { get { return "IgnoreOutput"; } }
      public ILogOptions LogOptions { get { return this.options; } }
      IFrameworkLogOptions IOutput.LogOptions { get { return this.LogOptions; } }

      public void WriteLine(string format, params object[] args) { }
      public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo) { }
      public void Statistic(string format, params object[] args) { }
      public void FinalStatistic(string assembly, string msg) { }
      public void EmitError(System.CodeDom.Compiler.CompilerError error) { }
      public void Close() { }
      public int SwallowedMessagesCount(ProofOutcome outcome) { return 0; }
      public void StartMethod(Method method) { }
      public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC=false) { }
      public bool IsMasked(Witness witness) { return false; }
      public void StartAssembly(Assembly assembly) { }
      public void EndAssembly() { }
      public bool EmitOutcome(Witness witness, string format, params object[] args) { return false; }
      public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args) { return false; }
      public int RegressionErrors() { return 0; }

      public bool ErrorsWereEmitted { get { return false; } }
    }
  }

  public interface IOutputFullResults<Method, Assembly> : IOutputResults {
    void StartAssembly(Assembly assembly);
    void EndAssembly();
    void StartMethod(Method method);
    void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC);
    
    /// <summary>
    /// How many <code>outcome</code>have been swallowed?
    /// </summary>
    int SwallowedMessagesCount(ProofOutcome outcome);

    /// <summary>
    /// returns the output name
    /// </summary>
    string Name { get; }
  }

  interface IRemoteOutput
  {
    void OutputError(string description, string code, string help,
                                   string proofOutcome, bool warning, bool additional, string subCategory, string fullPath,
                                   int startLine, int startColumn, int endLine, int endColumn);
    void OutputSuggestion(ClousotSuggestion.Kind type, string kind, string suggestion, string fullPath, int startLine, int startColumn, int endLine, int endColumn);
    void OutputLine(string msg, params object[] args);
    void Output(string msg, params object[] args);
    void Close();
  }
   
  class ConsoleRemoteOutput : IRemoteOutput
  {
    private const char splitC = ',';
        
    public void OutputError(string description, string code, string help,
                                   string proofOutcome, bool warning, bool additional, string subCategory, string fullPath,
                                   int startLine, int startColumn, int endLine, int endColumn)
    {
      OutputCommand("OutputError", Encode(description), Encode(code), Encode(help), 
                                       Encode(proofOutcome), Encode(warning), Encode(additional), Encode(subCategory), Encode(fullPath), 
                                       Encode(startLine), Encode(startColumn), Encode(endLine), Encode(endColumn));
    }

    public void OutputSuggestion(ClousotSuggestion.Kind type, string kind, string suggestion, string fullPath, int startLine, int startColumn, int endLine, int endColumn)
    {
      OutputCommand("OutputSuggestion", Encode(kind), Encode(suggestion), Encode(fullPath),
                                       Encode(startLine), Encode(startColumn), Encode(endLine), Encode(endColumn));
    }

    public void OutputLine(string msg, params object[] args)
    {
      OutputCommand("OutputLine", Encode(string.Format(msg, args)));
    }

    public void Output(string msg, params object[] args)
    {
      OutputCommand("Output", Encode(string.Format(msg, args)));
    }

    public void Close()
    {
      OutputCommand("Close");
    }

    private void OutputCommand(string cmd, params object[] args)
    {
      StringBuilder sb = new StringBuilder(cmd);
      foreach (string arg in args) {
        sb.Append(splitC);
        sb.Append(arg);
      }
      Console.Error.WriteLine(sb.ToString());
    }

    // replace: 
    //  ,       -> \,   (splitC = ',')
    //  \       -> \\
    //  newline -> \n
    //
    // (this assumes that '\,' is not a valid C# escape character)
    private static string Encode(string s)
    {
      if (s == null || s.Length == 0) {
        return "";
      }

      int specialCount = 0;
      foreach( char c in s ) {
        if (c == splitC || c == '\n' || c == '\\') {
          specialCount++;
        }
        else if (c > '\x7F') {
          if (c > '\xFFFF') specialCount += 9;
          else specialCount += 5;
        }
      }

      if (specialCount == 0) {
        return s;
      }
      else {
        char[] buf = new char[s.Length + specialCount];
        int i = 0;
        foreach( char c in s )
        {          
          if (c == splitC) {
            buf[i++] = '\\';
            buf[i++] = splitC;
          }
          else if (c == '\n') {
            buf[i++] = '\\';
            buf[i++] = 'n';            
          }
          else if (c == '\\') {
            buf[i++] = '\\';
            buf[i++] = '\\';
          }
          else if (c > '\x7F') {
            int value = Convert.ToInt32(c);
            string hex = value.ToString( value > 0xFFFF ? "X8" : "X4");
            buf[i++] = '\\'; 
            buf[i++] = (value > 0xFFFF ? 'X' : 'x'); 
            foreach (char h in hex) {
              buf[i++] = h;
            }            
          }
          else {
            buf[i++] = c;
          }
        }
        return (new string(buf));
      }
    }

    private static string Encode(bool b)
    {
      return b.ToString();
    }

    private static string Encode(int i)
    {
      return i.ToString();
    }
  }

  class RemoteWriterOutput<Method, Assembly> : IOutputFullResults<Method, Assembly>
  {
    ILogOptions options;
    string outputname;
    IRemoteOutput remote;
    readonly TextWriter tw;
    bool errorsWereEmitted;
    
    public RemoteWriterOutput(string outputname, TextWriter tw, GeneralOptions options)
    {
      this.outputname = outputname;
      this.options = options;
      this.tw = GeneralOptions.DefaultOutFileName != outputname ? tw : null; // We write on tw only if it is not the standard output
      this.remote = new ConsoleRemoteOutput();
      this.errorsWereEmitted = false;
    }

    #region IOutputFullResults<Method> Members

    public string Name { get { return this.outputname; } }

    public int SwallowedMessagesCount(ProofOutcome outcome) 
    { 
      return 0; 
    }

    public void StartMethod(Method method)
    {
    }

    public void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
    {
    }

    public bool IsMasked(Witness witness)
    {
      return false;
    }

    public void StartAssembly(Assembly assembly)
    {
    }

    public void EndAssembly()
    {
    }


    public ILogOptions LogOptions
    {
      get { return options; }
    }

    public bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      if (remote != null) {
        this.errorsWereEmitted = this.options.WarningsAsErrors;
        RemoteEmitOutcome(false, witness.Outcome, witness.PC, format, args);
      }

      if (tw != null) {        
        string tag;
        switch (witness.Outcome) {
          case ProofOutcome.Top:
            if (options.WarningsAsErrors)
            {
              tag = " error :";
              this.errorsWereEmitted = true;
            }
            else
            {
              tag = " warning :";
            }
            break;
          case ProofOutcome.False:
            if (options.WarningsAsErrors)
            {
              tag = " error :";
              this.errorsWereEmitted = true;
            }
            else
            {
              tag = " warning :";
            }
            break; // keep it a warning to not confuse VS
          default: tag = ""; break;
        }
        tw.WriteLine("{0}:{2} {1}", witness.PC.PrimaryMethodLocation().PrimarySourceContext(), String.Format(format, args), tag);
      }

      return true;
    }

    public bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      this.errorsWereEmitted = this.options.WarningsAsErrors;

      if (remote != null)
      {
        RemoteEmitOutcome(false, witness.Outcome, witness.PC.PrimaryMethodLocation(), format, args);
        foreach (var related in witness.PC.NonPrimaryRelatedLocations())
        {
          if (related.HasRealSourceContext)
          {
            RemoteEmitOutcome(true, witness.Outcome, related, "location related to previous warning");
          }
        }

        foreach (var cause in witness.Trace)
        {
          var primaryPC = cause.Key.MapPrimaryCausePC();
          RemoteEmitOutcome(true, witness.Outcome, primaryPC, cause.Value);
        }

      }

      if (tw != null)
      {
        string tag;
        switch (witness.Outcome)
        {
          case ProofOutcome.Top:
            if (options.WarningsAsErrors)
            {
              tag = " error :";
              this.errorsWereEmitted = true;
            }
            else
            {
              tag = " warning :";
            }   
            break;
          case ProofOutcome.False:
            if (options.WarningsAsErrors)
            {
              tag = " error :";
              this.errorsWereEmitted = true;
            }
            else
            {
              tag = " warning :";
            }
            break; // keep it a warning to not confuse VS
          default: tag = ""; break;
        }
        tw.WriteLine("{0}:{2} {1}", witness.PC.PrimaryMethodLocation().PrimarySourceContext(), String.Format(format, args), tag);
        foreach (var related in witness.PC.NonPrimaryRelatedLocations())
        {
          if (related.HasRealSourceContext)
          {
            tw.WriteLine("{0}: warning : location related to previous warning", related.PrimarySourceContext());
          }
        }
      }

      return true;
    }

    private void RemoteEmitOutcome(bool additional, ProofOutcome outcome, APC pc, string format, params object[] args)
    {
      if (remote != null) {

        string description = String.Format(format, args);
        string code = "";
        string help = null;
        bool warning = !options.WarningsAsErrors;
        string subCategory = (warning ? "warning" : "error");

        string proofOutcome;
        switch (outcome) {
          case ProofOutcome.True: proofOutcome = "true"; break;
          case ProofOutcome.False: proofOutcome = "false"; break;
          case ProofOutcome.Bottom: proofOutcome = "bottom"; break;
          case ProofOutcome.Top: proofOutcome = "top"; break;
          default: proofOutcome = "unknown"; break;
        }

        int startLine = 1;
        int endLine = 0;
        int startColumn = 1;
        int endColumn = 1;

        string fullPath = pc.Block.SourceDocument(pc);
        if (pc.HasRealSourceContext) {
          startLine = pc.Block.SourceStartLine(pc);
          endLine = pc.Block.SourceEndLine(pc);
          startColumn = pc.Block.SourceStartColumn(pc);
          endColumn = pc.Block.SourceEndColumn(pc);
        }
        else {
          // NOTE: should modify fullPath here too to point to the IL file
          description = pc.AlternateSourceContext() + ": " + description;
        }
      
        remote.OutputError(description, code, help, proofOutcome, warning, additional, subCategory, fullPath, startLine, startColumn, endLine, endColumn);
      }
    }


    public int RegressionErrors()
    {
      return 0;
    }

    #endregion

    #region IOutputResults Members

    public void Statistic(string format, params object[] args)
    {
      WriteLine(format, args);
    }

    public void FinalStatistic(string assemblyName, string msg)
    {
      if (remote != null)
      {
        remote.OutputSuggestion(ClousotSuggestion.Kind.NotInteresting, "", msg, assemblyName, 0, 0, 0, 0);
      }
      else if (tw != null)
      {
        tw.WriteLine(msg);
      }
    }

    public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (remote != null)
      {
        int startLine = 1;
        int endLine = 0;
        int startColumn = 1;
        int endColumn = 1;

        string fullPath = pc.Block.SourceDocument(pc);
        if (pc.HasRealSourceContext)
        {
          startLine = pc.Block.SourceStartLine(pc);
          endLine = pc.Block.SourceEndLine(pc);
          startColumn = pc.Block.SourceStartColumn(pc);
          endColumn = pc.Block.SourceEndColumn(pc);
        }
        else
        {
          // NOTE: should modify fullPath here too to point to the IL file
          suggestion = pc.AlternateSourceContext() + ": " + suggestion;
        }

        remote.OutputSuggestion(type, kind, suggestion, fullPath, startLine, startColumn, endLine, endColumn);
      }
      if (tw != null)
      {
        tw.WriteLine("{0}: Suggested {1}: {2}", pc.PrimarySourceContext(), kind, suggestion);
      }
    }

    
    public void Close()
    {
      if (remote != null) remote.Close();
      if (tw != null) tw.Close();
    }

    #endregion

    #region IOutput Members

    public void WriteLine(string format, params object[] args)
    {
      if (remote != null) remote.OutputLine(format, args);
      if (tw != null) tw.WriteLine(format, args);
    }

    public void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      if (remote != null) {
        remote.OutputError(error.ErrorText, error.ErrorNumber.ToString(), null, "false",
          error.IsWarning, false, (error.IsWarning ? "warning" : "error"),
          error.FileName, error.Line, error.Column, error.Line, error.Column);

        this.errorsWereEmitted = true;
      }
    }

    public bool ErrorsWereEmitted
    {
      get { return this.errorsWereEmitted; }
    }


    IFrameworkLogOptions IOutput.LogOptions
    {
      get { return options; }
    }

    #endregion

  }

  /// <summary>
  /// Helper class to build an IOutputFullResults on top of an existing one.
  /// You only need to implement non-trivial members
  /// </summary>
  public abstract class DerivableOutputFullResults<Method, Assembly> : IOutputFullResults<Method, Assembly>
  {
    private readonly IOutputFullResults<Method, Assembly> underlying; // In derived classes, use base. instead this.underlying.
    protected IOutputFullResults<Method, Assembly> UnderlyingOutput { get { return this.underlying; } } // However base is not an object but we sometimes need it

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(this.underlying != null);
    }

    protected DerivableOutputFullResults(IOutputFullResults<Method, Assembly> underlying)
    {
      Contract.Requires(underlying != null);

      this.underlying = underlying;
    }

    #region IOutputFullResults<Method, Assembly> Members

    public virtual void StartAssembly(Assembly assembly)
    {
      this.underlying.StartAssembly(assembly);
    }

    public virtual void EndAssembly()
    {
      this.underlying.EndAssembly();
    }

    public virtual void StartMethod(Method method)
    {
      this.underlying.StartMethod(method);
    }

    public virtual void EndMethod(APC methodEntry, bool ignoreMethodEntryAPC)
    {
      this.underlying.EndMethod(methodEntry, ignoreMethodEntryAPC);
    }

    public virtual int SwallowedMessagesCount(ProofOutcome outcome)
    {
      return this.underlying.SwallowedMessagesCount(outcome);
    }

    public virtual string Name { get { return this.underlying.Name; } }

    #endregion

    #region IOutputResults Members

    public virtual bool EmitOutcome(Witness witness, string format, params object[] args)
    {
      return this.underlying.EmitOutcome(witness, format, args);
    }

    public virtual bool EmitOutcomeAndRelated(Witness witness, string format, params object[] args)
    {
      return this.underlying.EmitOutcomeAndRelated(witness, format, args);
    }

    public virtual void Statistic(string format, params object[] args)
    {
      this.underlying.Statistic(format, args);
    }

    public virtual void FinalStatistic(string assemblyName, string message)
    {
      this.underlying.FinalStatistic(assemblyName, message);
    }

    public virtual void Close()
    {
      this.underlying.Close();
    }

    public virtual int RegressionErrors()
    {
      return this.underlying.RegressionErrors();
    }

    public virtual bool IsMasked(Witness witness)
    {
      return this.underlying.IsMasked(witness);
    }

    public virtual ILogOptions LogOptions { get { return this.underlying.LogOptions; } }

    #endregion

    #region IOutput Members

    public virtual void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      this.underlying.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
    }

    public virtual void EmitError(System.CodeDom.Compiler.CompilerError error)
    {
      this.underlying.EmitError(error);
    }

    IFrameworkLogOptions IOutput.LogOptions { get { return this.LogOptions; } }

    #endregion

    #region ISimpleTextWriter

    public virtual void WriteLine(string format, params object[] args)
    {
      this.underlying.WriteLine(format, args);
    }

    public virtual bool ErrorsWereEmitted
    {
      get { return this.underlying.ErrorsWereEmitted; }
    }


    #endregion
  }

  public class TextWriterWithDoubleWrite<A, B> : TextWriter
    where A: TextWriter
    where B: TextWriter
  {
    private readonly A a;
    private readonly B b;

    public TextWriterWithDoubleWrite(A a, B b)
    {
      Contract.Requires(a != null);
      Contract.Requires(b != null);
      this.a = a;
      this.b = b;
    }

    public override IFormatProvider FormatProvider { get { return a.FormatProvider; } }

    public override string NewLine { get { return a.NewLine; } set { a.NewLine = value; } }

    public override void Close()
    {
      a.Close();
      b.Close();
    }

    protected override void Dispose(bool disposing)
    {
      a.Dispose();
      b.Dispose();
    }

    public override void Flush()
    {
      a.Flush();
      b.Flush();
    }


    public override void Write(bool value)
    {
      a.Write(value);
      b.Write(value);
    }
 
    public override void Write(char value)
    {
      a.Write(value);
      b.Write(value);
    }

    public override void Write(char[] buffer)
    {
      a.Write(buffer);
      b.Write(buffer);
    }

    public override void Write(decimal value)
        {
      a.Write(value);
      b.Write(value);
    }


    public override void Write(double value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(float value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(int value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(long value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(object value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(string value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(uint value)
    {
      a.Write(value);
      b.Write(value);
    }
    public override void Write(ulong value)
    {
      a.Write(value);
      b.Write(value);
    }

    public override void Write(string format, object arg0)
    {
      a.Write(format, arg0);
      b.Write(format, arg0);
    }
    public override void Write(string format, params object[] arg)
    {
      a.Write(format, arg);
      b.Write(format, arg);
    }
    public override void Write(char[] buffer, int index, int count)
    {
      a.Write(buffer, index, count);
      b.Write(buffer, index, count);
    }

    public override void Write(string format, object arg0, object arg1)
    {
      a.Write(format, arg0, arg1);
      b.Write(format, arg0, arg1);
    }

    public override void Write(string format, object arg0, object arg1, object arg2)
    {
      a.Write(format, arg0, arg1, arg2);
      b.Write(format, arg0, arg1, arg2);
    }

    public override void WriteLine()
    {
      a.WriteLine();
      b.WriteLine();
    }

    public override void WriteLine(bool value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(char value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }
    public override void WriteLine(char[] buffer)
    {
      a.WriteLine(buffer);
      b.WriteLine(buffer);
    }
    public override void WriteLine(decimal value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(double value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(float value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

  public override void WriteLine(int value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(long value)
  {
    a.WriteLine(value);
    b.WriteLine(value);
  }

    public override void WriteLine(object value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(string value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(uint value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(ulong value)
    {
      a.WriteLine(value);
      b.WriteLine(value);
    }

    public override void WriteLine(string format, object arg0)
    {
      a.WriteLine(format, arg0);
      b.WriteLine(format, arg0);
    }

    public override void WriteLine(string format, params object[] arg)
    {
      a.WriteLine(format, arg);
      b.WriteLine(format, arg);
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
      a.WriteLine(buffer, index, count);
      b.WriteLine(buffer, index, count);
    }

    public override void WriteLine(string format, object arg0, object arg1)
    {
      a.WriteLine(format, arg0, arg1);
      b.WriteLine(format, arg0, arg1);
    }

    public override void WriteLine(string format, object arg0, object arg1, object arg2)
    {
      a.WriteLine(format, arg0, arg1, arg2);
      b.WriteLine(format, arg0, arg1, arg2);
    }


    public override Encoding Encoding
    {
      get { return a.Encoding; }
    }
  }

  public class TurnSuggestionIntoWarnings<Method, Assembly> : DerivableOutputFullResults<Method, Assembly>
  {
    private readonly List<SuggestionsAsWarnings> suggestionsAsWarnings;

    public TurnSuggestionIntoWarnings(IOutputFullResults<Method, Assembly> output,  List<SuggestionsAsWarnings> suggestionsAsWarnings)
       : base(output)
    {
      Contract.Requires(output != null);
      Contract.Requires(suggestionsAsWarnings != null);
      Contract.Requires(suggestionsAsWarnings.Count > 0);

      this.suggestionsAsWarnings = suggestionsAsWarnings;
    }

    public override void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
    {
      if (ShouldTurnIntoWarning(type))
      {
        var witness = new Witness(null, WarningType.Suggestion, ProofOutcome.Top, pc, type);
        base.EmitOutcome(witness, suggestion);
      }
      else
      {
        base.Suggestion(type, kind, pc, suggestion, causes, extraInfo);
      }
    }

    private bool ShouldTurnIntoWarning(ClousotSuggestion.Kind type)
    {
      switch(type)
      {
        case ClousotSuggestion.Kind.AbstractState:
          return false;

        case ClousotSuggestion.Kind.Action:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.codefixes);

        case ClousotSuggestion.Kind.AssertToContract:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.asserttocontracts);

        case ClousotSuggestion.Kind.AssumeOnCallee:
        case ClousotSuggestion.Kind.AssumeOnEntry:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.assumes);

        case ClousotSuggestion.Kind.AssumptionCanBeProven:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.redundantassume);

        case ClousotSuggestion.Kind.CalleeInvariant:
          return false;

        case ClousotSuggestion.Kind.CodeFix:
          return false;

        case ClousotSuggestion.Kind.Ensures:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.methodensures) || this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.nonnullreturn);

        case ClousotSuggestion.Kind.EnsuresExtractedMethod:
          return false;

        case ClousotSuggestion.Kind.EnsuresNecessary:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.necessaryensures);

        case ClousotSuggestion.Kind.InterMethodTrace:
        case ClousotSuggestion.Kind.NotInteresting:
          return false;

        case ClousotSuggestion.Kind.ObjectInvariant:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.objectinvariants);

        case ClousotSuggestion.Kind.ReadonlyField:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.readonlyfields);

        case ClousotSuggestion.Kind.Requires:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.requires);

        case ClousotSuggestion.Kind.RequiresCanBeProven:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.requires);

        case ClousotSuggestion.Kind.RequiresExtractedMethod:
          return false;

        case ClousotSuggestion.Kind.RequiresIsRedundant:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.requires);

        case ClousotSuggestion.Kind.RequiresOnBaseMethod:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.requiresbase);

        case ClousotSuggestion.Kind.SuppressionWarning:
          return false;

        case ClousotSuggestion.Kind.UnusedSuppressWarning:
          return this.suggestionsAsWarnings.Contains(SuggestionsAsWarnings.unusedsuppress);

        default:
          Contract.Assert(false, "Should be unreachable");
          return false;
      }
    }
  }

}
