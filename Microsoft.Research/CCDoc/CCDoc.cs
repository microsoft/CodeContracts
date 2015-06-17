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
using Microsoft.Cci.ILToCodeModel;
using System.Diagnostics;
using Microsoft.Cci.MutableContracts;

[assembly: ContractVerification(false)]

namespace CCDoc {

  /// <summary>
  /// The main class of CCDoc containing the entry point into the program.
  /// </summary>
  [ContractVerification(true)]
  public static class CCDoc {
    /// <summary>
    /// The main entry point for the program. Real work is done in "RealMain". This just wraps RealMain in a try-catch-finally.
    /// </summary>
    public static int Main(string[] args) {
      int result = 0;
      var startTime = DateTime.Now;
      try {
        System.Diagnostics.Debug.Listeners.Clear(); // void debug boxes from popping up.
        result = RealMain(args);
      } catch (Exception e) { // swallow everything and just return an error code
        Console.WriteLine("CCDoc failed with uncaught exception: {0}", e.Message);
        Console.WriteLine("Stack trace: {0}", e.StackTrace);
        return 1;
      } finally {
        var delta = DateTime.Now - startTime;
        Console.WriteLine("elapsed time: {0}ms", delta.TotalMilliseconds);
      }
      return result;
    }
    /// <summary>
    /// The "real" main. This is where all the work in the program starts.
    /// </summary>
    ///<remarks>
    ///Due to technical reasons (see the remarks on "WriteContracts" and "GetContracts"),
    ///the contracts are gathered first then they are written out. This is instead of
    ///writting the contracts while they are being found.
    ///</remarks>
    ///<remarks>
    ///The contracts are gathered first then they are written out. We make one pass over the assembly 
    ///to gather contracts, then one pass over the XML file to write out the contracts. This is instead 
    ///of writing the contracts as they are being found, because all members in the assembly may not 
    ///be represented in the XML file or may appear in a different order. 
    ///</remarks>
    public static int RealMain(string[] args)
    {
      #region Parse options
      var options = new Options();
      options.Parse(args);
      if (options.HelpRequested)
      {
        options.PrintOptions("");
        return 1;
      }
      if (options.HasErrors)
      {
        options.PrintErrorsAndExit(Console.Out);
        return -1;
      }
      if (options.breakIntoDebugger) {
        System.Diagnostics.Debugger.Break();
      }
      if (options.assembly == null && options.GeneralArguments.Count > 0)
      {
        options.assembly = options.GeneralArguments[0];
      }
      #endregion
      try
      {
        TrySendLeaderBoardPing(options);

        #region Set up the DocTracker. (Used for debug output.)
        TextWriter writer = null; //A null writer is allowed.
        if (options.debug)
        {
          if (options.outFile != null)
            writer = new StreamWriter(options.outFile);
          else
            writer = Console.Out;
        }
        DocTracker docTracker = new DocTracker(writer);
        #endregion
        #region Collect and write contracts
        var contracts = GetContracts(options, docTracker);
        WriteContracts(contracts, options, docTracker);
        #endregion
        #region Write contract statistics
        docTracker.WriteContractsPerKind(MemberKind.Type);
        docTracker.WriteContractsPerKind(MemberKind.Property);
        docTracker.WriteContractsPerKind(MemberKind.Method);
        #endregion
      }
      catch
      {
        SendLeaderBoardFailure();
        throw;
      }
      return 0; //success
    }

    private static void SendLeaderBoardFailure()
    {
#if LeaderBoard
      var version = typeof(Options).Assembly.GetName().Version;
      LeaderBoard.LeaderBoardAPI.SendLeaderBoardFailure(LeaderBoard_CCDocId, version);
#endif
    }

    const int LeaderBoard_CCDocId = 0x2;
    const int LeaderBoardFeatureMask_CCDoc = LeaderBoard_CCDocId << 12;

    private static void TrySendLeaderBoardPing(Options options)
    {
#if LeaderBoard
      try
      {
        LeaderBoard.LeaderBoardAPI.SendLeaderBoardFeatureUse(LeaderBoardFeatureMask_CCDoc);
      }
      catch { }

#endif
    }
    /// <summary>
    /// Write contracts from dictionary to XML file.
    /// </summary>
    static void WriteContracts(IDictionary<string, XContract[]> contracts, Options options, DocTracker docTracker) {
      Contract.Requires(options != null);
      Contract.Requires(docTracker != null);
      Contract.Requires(contracts != null);
      string xmlDocName = options.xmlFile;
      #region If an XML file isn't provided, create a new one.
      if (string.IsNullOrEmpty(xmlDocName) || !File.Exists(xmlDocName)) {
        docTracker.WriteLine("XML file not provided. Creating a new one.");
        #region Trim assembly name
        var trimmedAssemblyName = options.assembly;
        trimmedAssemblyName = Path.GetFileNameWithoutExtension(trimmedAssemblyName);
        #endregion
        if (string.IsNullOrEmpty(xmlDocName)) {
          xmlDocName = trimmedAssemblyName + ".xml";
        }
        Contract.Assert(xmlDocName != null);
        #region Create new document
        XmlDocument doc = new XmlDocument();
        doc.AppendChild(doc.CreateXmlDeclaration("1.0", null, null));
        var docNode = doc.AppendChild(doc.CreateElement("doc"));                
        var assemblyNode = docNode.AppendChild(doc.CreateElement("assembly"));  
        var nameNode = assemblyNode.AppendChild(doc.CreateElement("name"));
        nameNode.AppendChild(doc.CreateTextNode(trimmedAssemblyName));
        docNode.AppendChild(doc.CreateElement("members"));
        #endregion
        doc.Save(xmlDocName);
      }
      #endregion
      #region Traverse the XML file and create a new XML file with contracts
      var xmlDocTempName = xmlDocName + ".temp"; // xmlDocName may have a path, so don't prepend!
      using (var reader = XmlReader.Create(xmlDocName)) {
        docTracker.WriteLine("Reading {0}...", xmlDocName);
        var settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.NewLineHandling = NewLineHandling.None;
        using (var writer = XmlWriter.Create(xmlDocTempName, settings)) {
          docTracker.WriteLine("Writing to {0} ...", xmlDocTempName);
          XmlTraverser xmlTraverser = new XmlTraverser(contracts, docTracker, options);
          xmlTraverser.Transform(reader, writer);
          writer.Flush();
        }
      }
      #endregion
      #region Rename the output XML file
      if (String.IsNullOrEmpty(options.outputFile)) {
        File.Replace(xmlDocTempName, xmlDocName, xmlDocName + ".old");
      } else {
        var outFile = options.outputFile;
        var ext = Path.GetExtension(outFile);
        if (ext != "xml") outFile = Path.ChangeExtension(outFile, ".xml");
        if (File.Exists(outFile)) {
          File.Replace(xmlDocTempName, outFile, xmlDocName + ".old");
        } else {
          File.Copy(xmlDocTempName, outFile);
        }
      }
      #endregion
    }
    /// <summary>
    /// Traverse the given assembly (located in options.Assembly) for contracts and return the contracts.
    /// </summary>
    /// <remarks>
    /// We use dictionary mapping special string ids that are unique to each member in an XML file to 
    /// the contracts that that member has.
    /// </remarks>
    static IDictionary<string, XContract[]> GetContracts(Options options, DocTracker docTracker) {
      Contract.Requires(options != null);
      Contract.Requires(docTracker != null);
      Contract.Ensures(Contract.Result<IDictionary<string,XContract[]>>() != null);
      
      #region Establish host and load assembly

      Contract.Assume(options.resolvedPaths != null);

      var host = new CodeContractAwareHostEnvironment(options.libpaths);
      foreach (var p in options.resolvedPaths) {
        host.AddResolvedPath(p);
      }
      IModule module = host.LoadUnitFrom(options.assembly) as IModule;
      if (module == null || module == Dummy.Module || module == Dummy.Assembly) {
        Console.WriteLine("'{0}' is not a PE file containing a CLR module or assembly.", options.assembly);
        Environment.Exit(1);
      }
      #endregion
      #region Create the contracts dictionary
      Dictionary<string, XContract[]> contracts = new Dictionary<string, XContract[]>(StringComparer.Ordinal);  //Use Ordinal compare for faster string comparison.
      #endregion
      #region Traverse module and extract contracts
      var contractsVisitor = new ContractVisitor(host, contracts, options, docTracker);
      var traverser = new ContractTraverser(host);
      traverser.PreorderVisitor = contractsVisitor;
      traverser.Traverse(module);
      #endregion
      return contracts;
    }
  }
}
