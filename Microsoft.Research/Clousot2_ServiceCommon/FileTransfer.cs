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
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO.Compression;
using Microsoft.Research.DataStructures;
using Microsoft.Research.Cloudot.Common;

namespace Microsoft.Research.CodeAnalysis
{
  public static class FileTransfer
  {
    #region AnalysisPackageInfo struct

    public struct AnalysisPackageInfo
    {
      public string PackageFileNameLocal; // The package locally
      public string PackageFileNameServer; // The location of the package on the server
      public string[] ClousotOptions; // Original options
      public string[] ExpandedClousotOptions; // Options with the response file inlined
      public string[] ExpandedClousotOptionsNormalized; // options with the response file inlined and paths to UNC format. It may be null if we failed
      public string[] AssembliesToAnalyze; // The Assembly to analyze
    }
    #endregion

    #region Statics and constants

    const string CloudotInstructionsFileName = @"cloudotPackageInfo.txt";
    const string CloudotOptionsMarkerBegin = "((((";
    const string CloudotOptionsMarkerEnd =   "))))";

    #endregion

    #region Public surface
    public static bool TryProcessPathsAndCreateAnalysisPackage(string outputDir, string[] originalClousotOptions, bool createThePackage, out AnalysisPackageInfo clousotOptions)
    {
      Contract.Requires(!string.IsNullOrEmpty(outputDir));
      Contract.Requires(originalClousotOptions != null);
      Contract.Ensures(!createThePackage || !Contract.Result<bool>() || Contract.ValueAtReturn(out clousotOptions).PackageFileNameLocal != null);

      clousotOptions = new AnalysisPackageInfo();
      clousotOptions.ClousotOptions = originalClousotOptions; 

      try
      {
        // Step 0: make sure the output dir exists
        DirectoryInfo outputDirInfo;
        if (Directory.Exists(outputDir))
        {
          outputDirInfo = new DirectoryInfo(outputDir);
        }
        else
        {
          outputDirInfo = Directory.CreateDirectory(outputDir);
        }

        // Step 1: Expand the response file, set the field
        if (!FlattenClousotOptions(originalClousotOptions, out clousotOptions.ExpandedClousotOptions))
        {
          return false;
        }

        Contract.Assert(clousotOptions.ExpandedClousotOptions != null);

        var pathsInTheExpandedCommandLine = ExtractPathsAndUpdateThePackageInfo(ref clousotOptions);

        if (createThePackage)
        {
          // Step 2: Create the analysis package --- Go over all the directories and files, compress them, and save it in outputDirInfo
          clousotOptions.PackageFileNameLocal = CreateAnalysisPackage(outputDirInfo, pathsInTheExpandedCommandLine, clousotOptions.ExpandedClousotOptions);
          
          return (clousotOptions.PackageFileNameLocal != null);
        }
        else
        {
          return true;
        }
      }
      catch (Exception)
      {
        // Something went wrong, we just abort
        return false;
      }
    }

    [Pure] // Even if we change the file system...
    public static bool TrySendTheFile(string fromFileName, string serverOutDirName, out string packageFullNameOnTheServer)
    {
      Contract.Requires(!string.IsNullOrEmpty(fromFileName));
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out packageFullNameOnTheServer) != null);

      var start = DateTime.Now;
      Console.WriteLine("Copying the file {0} to the server {1}".PrefixWithCurrentTime(), fromFileName, serverOutDirName);
      packageFullNameOnTheServer = null;
      try
      {
        packageFullNameOnTheServer = string.Format(@"{0}\{1}", serverOutDirName, Path.GetFileName(fromFileName));
        File.Copy(fromFileName, packageFullNameOnTheServer);
      }
      catch (Exception e)
      {
        Console.WriteLine("Error in sending the file to the server. Aborting.{0}Exception{1}".PrefixWithCurrentTime(), Environment.NewLine, e.ToString());

        return false;
      }

      Console.WriteLine("Time to copy {0}".PrefixWithCurrentTime(), DateTime.Now - start);
      return true;
    }

    [Pure] // Even if we change the file system...
    public static bool TryRestoreAnalysisPackage(string packageFileName, string outputDir, out string[] clousotNormalizedParameters)
    {
      Contract.Requires(packageFileName != null);
      Contract.Requires(!string.IsNullOrEmpty(outputDir));
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out clousotNormalizedParameters) != null);

      clousotNormalizedParameters = null;
      try
      {
        // Step 0: make sure the directory exists
        DirectoryInfo outputDirInfo;
        if (Directory.Exists(outputDir))
        {
          outputDirInfo = new DirectoryInfo(outputDir);
        }
        else
        {
          outputDirInfo = Directory.CreateDirectory(outputDir);
        }

        if(TryRestorePackage(packageFileName, outputDir))
        {
          clousotNormalizedParameters = ForgeClousotCommandLineFromOutputFile(outputDir);
          
          return clousotNormalizedParameters != null;
        }

        return false;

      }
      catch(Exception e)
      {
        CloudotLogging.WriteLine("Something went wrong in restoring the package (exception of type {0}). Aborting.", e.GetType());
        CloudotLogging.WriteLine("Exception: {0}", e);
        return false;
      }
    }

    #endregion

    #region Private implementation and various helpers 

    [Pure] // Even if we change the file system...
    private static string CreateAnalysisPackage(DirectoryInfo outputDir, List<Tuple<int, string>> paths, string[] commandLine)
    {
      Contract.Requires(commandLine != null);

      var start = DateTime.Now;
      string zipFileName;
      try
      {
        Console.WriteLine("Creating the package".PrefixWithCurrentTime());

        var mapping = new List<Tuple<string, string>>();
        zipFileName = string.Format(@"{0}\{1}.{2}", outputDir.FullName, Path.GetRandomFileName(),  "zip");
        
        // Create the stream
        using(var zipFile = new FileStream(zipFileName, FileMode.CreateNew))
        {
          // Create the archive
          using(var zipArchive = new ZipArchive(zipFile, ZipArchiveMode.Create))
          {
            var added = new List<string>();

            // TODO TODO TODO: Be smarter and avoid adding common reference assemblies, files we already uploaded, etc.

            // Add all files and dirs
            foreach(var path in paths)
            {
              // It seems that the .zip format allows to insert the same format twice. 
              // But we want to avoid it
              if(added.Contains(path.Item2))
              {
                continue;
              }

              // We are not sure that all the paths in the options really exists in the path, so we'd better make sure it is the case
              if (File.Exists(path.Item2))
              {
                // We should normalize with RemoveColomns as there is a bug in Windows Explorer unzip!!!
                zipArchive.CreateEntryFromFile(path.Item2, RemoveColons(path.Item2), CompressionLevel.Fastest);
                added.Add(path.Item2);
              }
              else if (Directory.Exists(path.Item2))
              {
                foreach (var fileInDir in Directory.GetFiles(path.Item2))
                {
                  if(added.Contains(fileInDir))
                  {
                    continue;
                  }
                  
                  // By some simple benchmark, it seems the Faster option is the better: it creates a slighter larger .zip but in less time that is not regained by coping things around the network
                  // Benchmarks:
                  //                Build  Size  cp to CloudotServer
                  //  NoCompression 1.0s  114M  11.00s    
                  //  Fastest       2.1s  26M    2.70 s       
                  //  Optimal       4.0s  21M    2.25s

                  // We should normalize with RemoveColomns as there is a bug in Windows Explorer unzip!!!
                  zipArchive.CreateEntryFromFile(fileInDir, RemoveColons(fileInDir), CompressionLevel.Fastest);
                  added.Add(fileInDir);
                }
              }
            }
            // Add a text file with the analysis options
            // Probably we should use an xml file for that, but I love text files so much...
            var readme = zipArchive.CreateEntry(CloudotInstructionsFileName);
            using(var writer = new StreamWriter(readme.Open()))
            {
              writer.WriteLine("** Cloudot analysis package **");
              writer.WriteLine("Created on {0} (Creation time {1})", start, DateTime.Now - start);
              writer.WriteLine("Original command line (length {0}):", commandLine.Length);
              writer.WriteLine(CloudotOptionsMarkerBegin);
              foreach (var entry in commandLine)
              {
                writer.WriteLine(entry);
              }
              writer.WriteLine(CloudotOptionsMarkerEnd);
            }
          }
        }
      }
      catch(Exception e)
      {
        Console.WriteLine("Error while creating the package. Exception of type {0}", e.GetType());
        return null;
      }

      Console.WriteLine("[Debug] Package {0} created in {1}", zipFileName, DateTime.Now - start);
      
      return zipFileName;
    }

    [Pure]
    private static bool TryRestorePackage(string fileName, string outputDir)
    {      

      var start = DateTime.Now;
      try
      {
        CloudotLogging.WriteLine("Restoring the analysis package {0} in {1}", fileName, outputDir);
        using (var zipArchive = ZipFile.OpenRead(fileName))
        {
          foreach(var entry in zipArchive.Entries)
          {
            var destinationFileName = MakeOutputFileName(outputDir, entry.FullName);

            // If the file was already unzipped, skip it
            if(File.Exists(destinationFileName))
            {
              continue; // Skip the file
            }

            // Make sure the destination directory exists
            var dir = Path.GetDirectoryName(destinationFileName);
            if(!Directory.Exists(dir))
            {
              Directory.CreateDirectory(dir);
            }
            
            entry.ExtractToFile(destinationFileName);
          }
        }
      }
      catch(Exception e)
      {
        CloudotLogging.WriteLine("Something went wrong in restoring the file {0} (got exception of type {1}). Aborting.", fileName, e.GetType());
        return false;
      }

      CloudotLogging.WriteLine("Done Restoring the analysis package ({0} elapsed)", DateTime.Now - start);

      return true;
    }


    /// <returns>May return null if something wrong happened</returns>
    [Pure]
    private static string[] ForgeClousotCommandLineFromOutputFile(string outputDir)
    {
      try
      {
        var lines = File.ReadAllLines(Path.Combine(outputDir, CloudotInstructionsFileName));
        var result = new List<string>();
        var optionsWithPaths = GeneralOptions.GetClousotOptionsWithPaths().ToArray();
        var optionsBounds = FindOptions(lines);
        for (var i = optionsBounds.Item1; i < optionsBounds.Item2; i++)
        {
          var line = lines[i];
          if (line.Length > 0)
          {
            var subLine = line.Substring(1);
            if (optionsWithPaths.Any(optionName => subLine.StartsWith(optionName)))
            {
              line = ReplaceVolumesWithLocalRootPath(outputDir, line);
            }
            else if(IsPath(line))
            {
              line = ReplaceVolumesWithLocalRootPath(outputDir, line);
            }
          }
          result.Add(line);
        }

        return result.ToArray();
      }
      catch (Exception)
      {
        CloudotLogging.WriteLine("Something went wrong while getting the cmd line from dir {0}", outputDir);
        return null;
      }
    } 

    [Pure]
    private static string ReplaceVolumesWithLocalRootPath(string localRootPath, string stringToUpdate)
    {
      var result = stringToUpdate;
      foreach (var drive in GetDrives(stringToUpdate))
      {
        var driveId = drive + @":\";
        Contract.Assume(driveId.Length > 0);
        var newPrefixReplacingDriveId = Path.Combine(localRootPath, drive + @"\");
        result = stringToUpdate.Replace(driveId, newPrefixReplacingDriveId);
      }
      return result;
    }

    [Pure]
    private static Tuple<int, int> FindOptions(string[] lines)
    {
      Contract.Ensures(Contract.Result<Tuple<int, int>>().Item1 <= Contract.Result<Tuple<int, int>>().Item2);

      var begin = 0;
      var end = lines.Length;
      // Forward search for the begin
      for(var i = 0; i < lines.Length; i++)
      {
        if(lines[i].Equals(CloudotOptionsMarkerBegin))
        {
          begin = i+1;
          break;
        }
      }

      // Backwards search for the end
      for (var i = lines.Length - 1; i >= begin; i--)
      {
        if(lines[i].Equals(CloudotOptionsMarkerEnd))
        {
          end = i;
          break;
        }
      }

        return new Tuple<int, int>(begin, end);         
    }

    [Pure]
    private static char[] GetDrives(string subline)
    {
      var result = new List<char>();
      // Search for x:\ occurrences
      for(var i = 1; i < subline.Length -1 ; i++)
      {
        if (subline[i] == ':' && subline[i + 1] == '\\')
        {
          var drive = subline[i - 1];
          // Add the index and the x:\ string
          result.Add(drive);
        }
      }

      return result.Distinct().ToArray();
    }

    [Pure]
    private static string MakeOutputFileName(string outputDir, string fileName)
    {
      return Path.Combine(outputDir, fileName);
    }

    private static List<Tuple<int, string>> ExtractPathsAndUpdateThePackageInfo(ref AnalysisPackageInfo packageInfo)
    {
      Contract.Requires(packageInfo.ExpandedClousotOptions != null);
      Contract.Ensures(packageInfo.AssembliesToAnalyze != null);
      Contract.Ensures(packageInfo.ExpandedClousotOptions == Contract.OldValue(packageInfo.ExpandedClousotOptions)); 

      var result = new List<Tuple<int, string>>();
      var assembliesToAnalyse = new List<string>();

      string[] expandedClousotOptions = packageInfo.ExpandedClousotOptions;
      
      // Very stuping parsing algorithm, to be improved?

      // Step 0: get the clousot options that contain paths
      var clousotOptionsForPaths = GeneralOptions.GetClousotOptionsWithPaths().ToArray(); // Use ToArray to speed up the look ups

      // Step 1: go all over the clousot options to find those fields, or the dirs we are interested in
      for (var i = 0; i < expandedClousotOptions.Length; i++)
      {
        var option = expandedClousotOptions[i];

        var pathsInTheOptions = GetPaths(option);

        // Step 1a : Is the current option a path?
        if(pathsInTheOptions.Count == 1)
        {
          // PrintPaths(pathsInTheOptions, "Found an assembly to analyze:");
          AddIntoResult(result, i, pathsInTheOptions);
          // Executed only once because of the test above
          foreach (var match in pathsInTheOptions)
          {
            assembliesToAnalyse.Add(match.ToString());
          }
        }
        // Step 1b : search inside the option
        else 
        {
          option = option.Substring(1); // Remove the first char
          var tryFind = clousotOptionsForPaths.Where(t => option.StartsWith(t));
          // Found a match
          if(tryFind.Any())
          {
            foreach (var candidatePath in option.Split(';'))
            {
              var matches = GetPaths(candidatePath);
              // PrintPaths(matches, "Found path(s) for option in position " + i);
              AddIntoResult(result, i, matches);
            }
          }
        }
      }

      // Step 2: create the normalized command line
      var clonedCommandLine = packageInfo.ExpandedClousotOptions.Clone() as string[];
      Contract.Assume(clonedCommandLine != null);
      foreach(var pair in result)
      {
        string unc;
        if(FileSystemAbstractions.TryGetUniversalName(Path.GetFullPath(pair.Item2), out unc))
        {
          clonedCommandLine[pair.Item1] = clonedCommandLine[pair.Item1].AssumeNotNull().Replace(pair.Item2, unc);
        }
        else
        {
          clonedCommandLine = null;
          break;
        }
      }

      packageInfo.AssembliesToAnalyze = assembliesToAnalyse.ToArray();
      packageInfo.ExpandedClousotOptionsNormalized = clonedCommandLine;

      return result;
    }

    private static void AddIntoResult(List<Tuple<int, string>> result, int position, MatchCollection matches)
    {
      Contract.Requires(result != null);
      foreach(var match in matches)
      {
        result.Add(new Tuple<int, string>(position, match.ToString()));
      }
    }

    [Conditional("DEBUG")]
    [Pure]
    private static void PrintPaths(MatchCollection pathsInTheOptions, string optionalHeadMsg = "")
    {
      // DEBUG code
      if(!String.IsNullOrEmpty(optionalHeadMsg))
      {
        Console.WriteLine(optionalHeadMsg);
      }

      foreach (var match in pathsInTheOptions)
      {
        Console.WriteLine("{0}", match);
      }
    }

    [Pure]
    private static MatchCollection GetPaths(string s)
    {
      var regularExpression = @"(([a-z]:|\\\\[a-z0-9_.$]+\\[a-z0-9_.$]+)?(\\?(?:[^\\/:*?""<>|\r\n]+\\)+)[^\\/:*?""<>|\r\n]+)";
      var regExp = new Regex(regularExpression, RegexOptions.IgnoreCase | RegexOptions.Singleline);
      var matches = regExp.Matches(s);

      return matches;
    }

    [Pure]
    private static bool IsPath(string s)
    {
      return GetPaths(s).Count == 1;
    }

    [Pure]
    private static bool IsDirectory(string path)
    {
      var attr = File.GetAttributes(path);

      return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
    }

    [Pure]
    public static bool FlattenClousotOptions(string[] clousotOptions, out string[] resultOptions)
    {
      Contract.Requires(clousotOptions != null);
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out resultOptions) != null);

      var result = new List<string>();
      for(var i = 0; i< clousotOptions.Length; i++)
      {
        var str = clousotOptions[i];

        if(str != null && str.Length > 0 && str[0] == '@')
        {
          var fileName = str.Substring(1);
          if (File.Exists(fileName))
          {
            var allText = File.ReadAllLines(fileName);
            for(var j = 0; j < allText.Length; j++)
            {
              var line = allText[j];
              if (string.IsNullOrEmpty(line))
              {
                continue;
              }
              if (line[0] == '#')
              {
                // It's a comment, let's skip it
                continue;
              }
              if (ContainsQuote(line))
              {
                var splittedLine = SplitLineWithQuotes(line);

                if (splittedLine == null)
                {
                  goto failed;
                }
                result.AddRange(splittedLine);
              }
              else
              {
                // simple splitting
                result.AddRange(line.Split(' '));
              }
            }
          }
          else
          {
            goto failed;
          }
        }
        else
        {
          result.Add(str);
        }
      }

      resultOptions = result.ToArray();
      return true;

      failed:
      resultOptions = null;
      return false;      

    }

    #endregion

    #region Helpers for parsing the response file and manipulating strings

    [Pure]
    private static string RemoveColons(string p)
    {
      return p.Replace(":", "");
    }

    [Pure]
    static private bool ContainsQuote(string line)
    {
      Contract.Requires(line != null);
      return (line.IndexOf('"') >= 0) || (line.IndexOf('\'') >= 0);
    }

    [Pure]
    // Method adapted from OptionParsing.cs 
   // TODO: Merge the two implementations (passing a delegate for the action to perform?)
    static private string[] SplitLineWithQuotes(string line)
    {
      Contract.Requires(line != null);

      var start = 0;
      var args = new List<string>();
      bool inDoubleQuotes = false;
      int escaping = 0; // number of escape characters in sequence
      var currentArg = new StringBuilder();

      for (var index = 0; index < line.Length; index++)
      {
        var current = line[index];

        if (current == '\\')
        {
          // escape character
          escaping++;
          // swallow the escape character for now
          // grab everything prior to prior escape character
          if (index > start)
          {
            currentArg.Append(line.Substring(start, index - start));
          }
          start = index + 1;
          continue;
        }
        if (escaping > 0)
        {
          if (current == '"')
          {
            var backslashes = escaping / 2;
            for (int i = 0; i < backslashes; i++) { currentArg.Append('\\'); }
            if (escaping % 2 == 1)
            {
              // escapes the "
              currentArg.Append('"');
              escaping = 0;
              start = index + 1;
              continue;
            }
          }
          else
          {
            var backslashes = escaping;
            for (int i = 0; i < backslashes; i++) { currentArg.Append('\\'); }
          }
          escaping = 0;
        }
        if (inDoubleQuotes)
        {
          if (current == '"')
          {
            // ending argument
            FinishArgument(line, start, args, currentArg, index, true);
            start = index + 1;
            inDoubleQuotes = false;
            continue;
          }
        }
        else // not in quotes
        {
          if (Char.IsWhiteSpace(current))
          {
            // end previous, start new
            FinishArgument(line, start, args, currentArg, index, false);
            start = index + 1;
            continue;
          }
          if (current == '"')
          {
            // starting double quote
            if (index != start)
            {
              return null; // ERROR!!!!
            }
            start = index + 1;
            inDoubleQuotes = true;
            continue;
          }
        }
      }
      // add outstanding escape characters
      while (escaping > 0) { currentArg.Append('\\'); escaping--; }
      FinishArgument(line, start, args, currentArg, line.Length, inDoubleQuotes);

      return args.ToArray();
    }

    [Pure]
    static private void FinishArgument(string line, int start, List<string> args, StringBuilder currentArg, int index, bool includeEmpty)
    {
      Contract.Requires(args != null);

      currentArg.Append(line.Substring(start, index - start));
      if (includeEmpty || currentArg.Length > 0)
      {
        args.Add(currentArg.ToString());
        currentArg.Length = 0;
      }
    }
    #endregion
  }
}
