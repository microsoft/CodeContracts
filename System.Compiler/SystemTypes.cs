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
using System.IO;
using System.Compiler.Metadata;

namespace System.Compiler{
  public sealed class SystemAssemblyLocation{
    static string location;
    public static string Location{
      get
      {
        return location;
      }
      set
      {
        //Debug.Assert(location == null || location == value, string.Format("You attempted to set the mscorlib.dll location to\r\n\r\n{0}\r\n\r\nbut it was already set to\r\n\r\n{1}\r\n\r\nThis may occur if you have multiple projects that target different platforms. Make sure all of your projects target the same platform.\r\n\r\nYou may try to continue, but targeting multiple platforms during the same session is not supported, so you may see erroneous behavior.", value, location));
        location = value;
      }
    }
    public static AssemblyNode ParsedAssembly;
    /// <summary>
    /// Allows compilers to share an assembly cache for loading runtime and system dlls.
    /// </summary>
    public static System.Collections.IDictionary SystemAssemblyCache = null;
  }
  public sealed class SystemDllAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeCollectionsAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemDiagnosticsDebugAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemDiagnosticsToolsAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemGlobalizationAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemReflectionAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemResourceManagerAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeExtensionsAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeInteropServicesAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeWindowsRuntimeAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemCoreAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeIOServicesAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeSerializationAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation {
    public static string Location = null;
  }
  public sealed class SystemThreadingAssemblyLocation {
    public static string Location = null;
  }

  public sealed class TargetPlatform{
    private TargetPlatform(){}
    public static bool BusyWithClear;
    public static bool DoNotLockFiles;
    public static bool GetDebugInfo;
    public static char GenericTypeNamesMangleChar = '_';

    private static bool useGenerics;

    public static bool UseGenerics {
      get {
        if (useGenerics) return true;
        Version v = TargetPlatform.TargetVersion;
        if (v == null) {
          v = CoreSystemTypes.SystemAssembly.Version;
          if (v == null)
            v = typeof(object).Assembly.GetName().Version;
        }
        return v.Major > 1 || v.Minor > 2 || v.Minor == 2 && v.Build >= 3300;
      }
      set
      {
        useGenerics = value;
      }
    }

    public static void Clear(){
      SystemAssemblyLocation.Location = null;
      SystemDllAssemblyLocation.Location = null;
      SystemRuntimeCollectionsAssemblyLocation.Location = null;
      SystemDiagnosticsDebugAssemblyLocation.Location = null;
      SystemDiagnosticsToolsAssemblyLocation.Location = null;
      SystemGlobalizationAssemblyLocation.Location = null;
      SystemReflectionAssemblyLocation.Location = null;
      SystemResourceManagerAssemblyLocation.Location = null;
      SystemRuntimeExtensionsAssemblyLocation.Location = null;
      SystemRuntimeInteropServicesAssemblyLocation.Location = null;
      SystemRuntimeWindowsRuntimeAssemblyLocation.Location = null;
      SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location = null;
      SystemRuntimeIOServicesAssemblyLocation.Location = null;
      SystemRuntimeSerializationAssemblyLocation.Location = null;
      SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation.Location = null;
      SystemThreadingAssemblyLocation.Location = null;
      TargetPlatform.DoNotLockFiles = false;
      TargetPlatform.GetDebugInfo = false;
      TargetPlatform.PlatformAssembliesLocation = "";
      TargetPlatform.BusyWithClear = true;
      SystemTypes.Clear();
      TargetPlatform.BusyWithClear = false;
    }
    public static System.Collections.IDictionary StaticAssemblyCache {
      get { return Reader.StaticAssemblyCache; }
    }
    public static Version TargetVersion =
 new Version(2, 0, 50727);  // Default for a WHIDBEY compiler
    public static string TargetRuntimeVersion;

    public static int LinkerMajorVersion {
      get {
        switch (TargetVersion.Major) {
          case 4: return 8;
          case 2: return 8;
          case 1: return 7;
          default: return 6;
        }
      }
    }
    public static int LinkerMinorVersion {
      get {
        return TargetVersion.Minor;
      }
    }

    public static int MajorVersion { get { return TargetVersion.Major; } }
    public static int MinorVersion { get { return TargetVersion.Minor; } }
    public static int Build { get { return TargetVersion.Build; } }

    public static string/*!*/ PlatformAssembliesLocation = String.Empty;
    private static TrivialHashtable assemblyReferenceFor;
    internal static bool AssemblyReferenceForInitialized{
      get { return assemblyReferenceFor != null; }
    }
    public static TrivialHashtable/*!*/ AssemblyReferenceFor {
      get{
        if (TargetPlatform.assemblyReferenceFor == null) {
          TargetPlatform.SetupAssemblyReferenceFor();
        }
        //^ assume TargetPlatform.assemblyReferenceFor != null;
        return TargetPlatform.assemblyReferenceFor;
      }
      set{
        TargetPlatform.assemblyReferenceFor = value;
      }
    }
    private readonly static string[]/*!*/ FxAssemblyNames = 
      new string[]{"Accessibility", "CustomMarshalers", "IEExecRemote", "IEHost", "IIEHost", "ISymWrapper", 
                    "Microsoft.JScript", "Microsoft.VisualBasic", "Microsoft.VisualBasic.Vsa", "Microsoft.VisualC",
                    "Microsoft.Vsa", "Microsoft.Vsa.Vb.CodeDOMProcessor", "mscorcfg", "Regcode", "System",
                    "System.Configuration.Install", "System.Data", "System.Design", "System.DirectoryServices",
                    "System.Drawing", "System.Drawing.Design", "System.EnterpriseServices", 
                    "System.Management", "System.Messaging", "System.Runtime.Remoting", "System.Runtime.Serialization.Formatters.Soap",
                    "System.Runtime.WindowsRuntime",
                    "System.Security", "System.ServiceProcess", "System.Web", "System.Web.Mobile", "System.Web.RegularExpressions",
                    "System.Web.Services", "System.Windows.Forms", "System.Xml", "TlbExpCode", "TlbImpCode", "cscompmgd",
                    "vjswfchtml", "vjswfccw", "VJSWfcBrowserStubLib", "vjswfc", "vjslibcw", "vjslib", "vjscor", "VJSharpCodeProvider"};
    private readonly static string[]/*!*/ FxAssemblyToken =
      new string[]{"b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a",
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a",
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b77a5c561934e089",
                    "b03f5f7f11d50a3a", "b77a5c561934e089", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a",
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", 
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b77a5c561934e089", "b03f5f7f11d50a3a",
                    "b77a5c561934e089",
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a",
                    "b03f5f7f11d50a3a", "b77a5c561934e089", "b77a5c561934e089", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a",
                    "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a", "b03f5f7f11d50a3a"};
    private readonly static string[]/*!*/ FxAssemblyVersion1 = 
      new string[]{"1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0",
                    "7.0.3300.0", "7.0.3300.0", "7.0.3300.0", "7.0.3300.0",
                    "7.0.3300.0", "7.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0",
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0",
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", 
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0",
                    "4.0.0.0",
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0",
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "7.0.3300.0",
                    "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "1.0.3300.0", "7.0.3300.0"};
    private readonly static string[]/*!*/ FxAssemblyVersion1_1 = 
      new string[]{"1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0",
                    "7.0.5000.0", "7.0.5000.0", "7.0.5000.0", "7.0.5000.0",
                    "7.0.5000.0", "7.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0",
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0",
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", 
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", 
                    "4.0.0.0",
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0",
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "7.0.5000.0",
                    "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "1.0.5000.0", "7.0.5000.0"};
    private static string[]/*!*/ FxAssemblyVersion2Build3600 =
      new string[]{"2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0",
                    "8.0.1200.0", "8.0.1200.0", "8.0.1200.0", "8.0.1200.0",
                    "8.0.1200.0", "8.0.1200.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0",
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0",
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", 
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", 
                    "4.0.0.0",
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0",
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "8.0.1200.0",
                    "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "2.0.3600.0", "7.0.5000.0"};
    private static string[]/*!*/ FxAssemblyVersion2 =
      new string[]{"2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0",
                    "8.0.0.0", "8.0.0.0", "8.0.0.0", "8.0.0.0",
                    "8.0.0.0", "8.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0",
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0",
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", 
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", 
                    "4.0.0.0",
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0",
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "8.0.0.0",
                    "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0", "2.0.0.0"};
    private static string[]/*!*/ FxAssemblyVersion4 =
      new string[]{"4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0",
                    "10.0.0.0", "10.0.0.0", "10.0.0.0", "10.0.0.0",
                    "10.0.0.0", "10.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0",
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0",
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", 
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", 
                    "4.0.0.0",
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0",
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "10.0.0.0",
                    "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0", "4.0.0.0"};
    private static void SetupAssemblyReferenceFor() {
      Version version = TargetPlatform.TargetVersion;
      if (version == null) version = typeof(object).Assembly.GetName().Version;
      TargetPlatform.SetTo(version);
    }
    public static void SetTo(Version/*!*/ version) {
      if (version == null) throw new ArgumentNullException();
      if (version.Major == 1) {
        if (version.Minor == 0 && version.Build == 3300) TargetPlatform.SetToV1();
        else if (version.Minor == 0 && version.Build == 5000) TargetPlatform.SetToV1_1();
        else if (version.Minor == 1 && version.Build == 9999) TargetPlatform.SetToPostV1_1(TargetPlatform.PlatformAssembliesLocation);
      } else if (version.Major == 2) {
        if (version.Minor == 0 && version.Build == 3600) TargetPlatform.SetToV2Beta1();
        else TargetPlatform.SetToV2();
      } else if (version.Major == 4) {
        if (version.Minor == 5) TargetPlatform.SetToV4_5();
        else TargetPlatform.SetToV4();
      } else
        TargetPlatform.SetToPostV2();      
    }
    public static void SetTo(Version/*!*/ version, string/*!*/ platformAssembliesLocation) {
      if (version == null || platformAssembliesLocation == null) throw new ArgumentNullException();
      if (version.Major == 1) {
        if (version.Minor == 0 && version.Build == 3300) TargetPlatform.SetToV1(platformAssembliesLocation);
        else if (version.Minor == 0 && version.Build == 5000) TargetPlatform.SetToV1_1(platformAssembliesLocation);
        else if (version.Minor == 1 && version.Build == 9999) TargetPlatform.SetToPostV1_1(platformAssembliesLocation);
      } else if (version.Major == 2) {
        if (version.Minor == 0 && version.Build == 3600) TargetPlatform.SetToV2Beta1(platformAssembliesLocation);
        else TargetPlatform.SetToV2(platformAssembliesLocation);
      } else if (version.Major == 4) {
        TargetPlatform.SetToV4(platformAssembliesLocation);
      } else
        TargetPlatform.SetToPostV2(platformAssembliesLocation);
    }
    public static void SetToV1() {
      TargetPlatform.SetToV1(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV1(string platformAssembliesLocation){
      TargetPlatform.TargetVersion = new Version(1, 0, 3300);
      TargetPlatform.TargetRuntimeVersion = "v1.0.3705";
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)
        platformAssembliesLocation = TargetPlatform.PlatformAssembliesLocation = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "..\\v1.0.3705");
      else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++){
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion1[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name+", Version="+version+", Culture=neutral, PublicKeyToken="+token);
        aref.Location = platformAssembliesLocation+"\\"+name+".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToV1_1(){
      TargetPlatform.SetToV1_1(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV1_1(string/*!*/ platformAssembliesLocation) {
      TargetPlatform.TargetVersion = new Version(1, 0, 5000);
      TargetPlatform.TargetRuntimeVersion = "v1.1.4322";
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)
        platformAssembliesLocation = TargetPlatform.PlatformAssembliesLocation = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "..\\v1.1.4322");
      else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++){
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion1_1[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name+", Version="+version+", Culture=neutral, PublicKeyToken="+token);
        aref.Location = platformAssembliesLocation+"\\"+name+".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToV2(){
      TargetPlatform.SetToV2(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV2(string platformAssembliesLocation){
      TargetPlatform.TargetVersion = new Version(2, 0, 50727);
      TargetPlatform.TargetRuntimeVersion = "v2.0.50727";
      TargetPlatform.GenericTypeNamesMangleChar = '`';
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)
        platformAssembliesLocation = TargetPlatform.PlatformAssembliesLocation = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "..\\v2.0.50727");
      else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++){
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion2[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name+", Version="+version+", Culture=neutral, PublicKeyToken="+token);
        aref.Location = platformAssembliesLocation+"\\"+name+".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToV2Beta1(){
      TargetPlatform.SetToV2Beta1(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV2Beta1(string/*!*/ platformAssembliesLocation) {
      TargetPlatform.TargetVersion = new Version(2, 0, 3600);
      TargetPlatform.GenericTypeNamesMangleChar = '!';
      string dotNetDirLocation = null;
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0) {
        DirectoryInfo dotNetDir = new FileInfo(new Uri(typeof(object).Assembly.Location).LocalPath).Directory.Parent;
        dotNetDirLocation = dotNetDir.FullName;
        if (dotNetDirLocation != null) dotNetDirLocation = dotNetDirLocation.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
        DateTime creationTime = DateTime.MinValue;
        foreach (DirectoryInfo subdir in dotNetDir.GetDirectories("v2.0*")) {
          if (subdir == null) continue;
          if (subdir.CreationTime < creationTime) continue;
          FileInfo[] mscorlibs = subdir.GetFiles("mscorlib.dll");
          if (mscorlibs != null && mscorlibs.Length == 1) {
            platformAssembliesLocation = subdir.FullName;
            creationTime = subdir.CreationTime;
          }
        }
      }else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      if (dotNetDirLocation != null && (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)){
        int pos = dotNetDirLocation.IndexOf("FRAMEWORK");
        if (pos > 0 && dotNetDirLocation.IndexOf("FRAMEWORK64") < 0) {
          dotNetDirLocation = dotNetDirLocation.Replace("FRAMEWORK", "FRAMEWORK64");
          if (Directory.Exists(dotNetDirLocation)) {
            DirectoryInfo dotNetDir = new DirectoryInfo(dotNetDirLocation);
            DateTime creationTime = DateTime.MinValue;
            foreach (DirectoryInfo subdir in dotNetDir.GetDirectories("v2.0*")) {
              if (subdir == null) continue;
              if (subdir.CreationTime < creationTime) continue;
              FileInfo[] mscorlibs = subdir.GetFiles("mscorlib.dll");
              if (mscorlibs != null && mscorlibs.Length == 1) {
                platformAssembliesLocation = subdir.FullName;
                creationTime = subdir.CreationTime;
              }
            }
          }
        }
      }
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++) {
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion2Build3600[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name + ", Version=" + version + ", Culture=neutral, PublicKeyToken=" + token);
        aref.Location = platformAssembliesLocation + "\\" + name + ".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToV4() {
      TargetPlatform.SetToV4(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV4(string platformAssembliesLocation) {
      TargetPlatform.TargetVersion = new Version(4, 0);
      TargetPlatform.TargetRuntimeVersion = "v4.0.30319";
      TargetPlatform.GenericTypeNamesMangleChar = '`';
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)
        platformAssembliesLocation = TargetPlatform.PlatformAssembliesLocation = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "..\\v4.0.30319");
      else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++) {
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion4[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name+", Version="+version+", Culture=neutral, PublicKeyToken="+token);
        aref.Location = platformAssembliesLocation+"\\"+name+".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToV4_5() {
      TargetPlatform.SetToV4_5(TargetPlatform.PlatformAssembliesLocation);
    }
    public static void SetToV4_5(string platformAssembliesLocation) {
      TargetPlatform.TargetVersion = new Version(4, 0);
      TargetPlatform.TargetRuntimeVersion = "v4.0.30319";
      TargetPlatform.GenericTypeNamesMangleChar = '`';
      if (platformAssembliesLocation == null || platformAssembliesLocation.Length == 0)
        platformAssembliesLocation = TargetPlatform.PlatformAssembliesLocation = Path.Combine(Path.GetDirectoryName(typeof(object).Assembly.Location), "..\\v4.0.30319");
      else
        TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation, "System.Runtime", "mscorlib");
      TrivialHashtable assemblyReferenceFor = new TrivialHashtable(46);
      for (int i = 0, n = TargetPlatform.FxAssemblyNames.Length; i < n; i++) {
        string name = TargetPlatform.FxAssemblyNames[i];
        string version = TargetPlatform.FxAssemblyVersion4[i];
        string token = TargetPlatform.FxAssemblyToken[i];
        AssemblyReference aref = new AssemblyReference(name+", Version="+version+", Culture=neutral, PublicKeyToken="+token);
        aref.Location = platformAssembliesLocation+"\\"+name+".dll";
        //^ assume name != null;
        assemblyReferenceFor[Identifier.For(name).UniqueIdKey] = aref;
      }
      TargetPlatform.assemblyReferenceFor = assemblyReferenceFor;
    }
    public static void SetToPostV2() {
      TargetPlatform.SetToPostV2(TargetPlatform.PlatformAssembliesLocation);
    }
    /// <summary>
    /// Use this to set the target platform to a platform with a superset of the platform assemblies in version 2.0, but
    /// where the public key tokens and versions numbers are determined by reading in the actual assemblies from
    /// the supplied location. Only assemblies recognized as platform assemblies in version 2.0 will be unified.
    /// </summary>
    public static void SetToPostV2(string/*!*/ platformAssembliesLocation) {
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.TargetVersion = new Version(2, 1, 9999);
      TargetPlatform.TargetRuntimeVersion = "v2.1.9999";
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TargetPlatform.assemblyReferenceFor = new TrivialHashtable(46);
      string[] dlls = Directory.GetFiles(platformAssembliesLocation, "*.dll");
      foreach (string dll in dlls) {
        if (dll == null) continue;
        string assemName = Path.GetFileNameWithoutExtension(dll);
        int i = Array.IndexOf(TargetPlatform.FxAssemblyNames, assemName);
        if (i < 0) continue;
        var loc = Path.Combine(platformAssembliesLocation, dll);
        var aref = new AssemblyReference(assemName);
        aref.Location = loc;
        TargetPlatform.assemblyReferenceFor[Identifier.For(assemName).UniqueIdKey] = aref;
      }
    }
    /// <summary>
    /// Use this to set the target platform to a platform with a superset of the platform assemblies in version 1.1, but
    /// where the public key tokens and versions numbers are determined by reading in the actual assemblies from
    /// the supplied location. Only assemblies recognized as platform assemblies in version 1.1 will be unified.
    /// </summary>
    public static void SetToPostV1_1(string/*!*/ platformAssembliesLocation) {
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      TargetPlatform.TargetVersion = new Version(1, 1, 9999);
      TargetPlatform.TargetRuntimeVersion = "v1.1.9999";
      TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);
      TargetPlatform.assemblyReferenceFor = new TrivialHashtable(46);
      string[] dlls = Directory.GetFiles(platformAssembliesLocation, "*.dll");
      foreach (string dll in dlls){
        if (dll == null) continue;
        string assemName = Path.GetFileNameWithoutExtension(dll);
        int i = Array.IndexOf(TargetPlatform.FxAssemblyNames, assemName);
        if (i < 0) continue;
        AssemblyNode assem = AssemblyNode.GetAssembly(Path.Combine(platformAssembliesLocation, dll));
        if (assem == null) continue;
        TargetPlatform.assemblyReferenceFor[Identifier.For(assem.Name).UniqueIdKey] = new AssemblyReference(assem);
      }
    }
    private static void InitializeStandardAssemblyLocationsWithDefaultValues(string platformAssembliesLocation){
      InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation, "mscorlib");
    }
    private static void InitializeStandardAssemblyLocationsWithDefaultValues(string platformAssembliesLocation, string mscorlibName, string alternateName)
    {
      var candidate = platformAssembliesLocation + "\\" + mscorlibName + ".dll";
      if (File.Exists(candidate))
      {
        InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation, mscorlibName);
      }
      else
      {
        InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation, alternateName);
      }
    }
    private static void InitializeStandardAssemblyLocationsWithDefaultValues(string platformAssembliesLocation, string mscorlibName) {
      SystemAssemblyLocation.Location = platformAssembliesLocation+"\\"+mscorlibName+".dll";
      if (SystemDllAssemblyLocation.Location == null)
        SystemDllAssemblyLocation.Location = platformAssembliesLocation+"\\system.dll";
    }
    public static void ResetCci(string platformAssembliesLocation, Version targetVersion, bool doNotLockFile, bool getDebugInfo, AssemblyNode.PostAssemblyLoadProcessor postAssemblyLoad = null) {
      TargetPlatform.Clear();

      //Tell Initialize where to get the platform assemblies from
      TargetPlatform.PlatformAssembliesLocation = platformAssembliesLocation;
      if (targetVersion.Major == 4 && targetVersion.Minor >= 5) {
        TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation, "System.Runtime");
        targetVersion = new Version(4, 0, 0, 0);
      } else
        TargetPlatform.InitializeStandardAssemblyLocationsWithDefaultValues(platformAssembliesLocation);

      //Force Initialize to set the TargetVersion to the version of the SystemAssembly that gets loaded
      TargetPlatform.TargetVersion = targetVersion;        

      //Force Initialize to load the appropriate unification table for the SystemAssembly version it is loading
      TargetPlatform.assemblyReferenceFor = null;
        
      SystemTypes.Initialize(doNotLockFile, getDebugInfo, postAssemblyLoad);
    }
  }
  public sealed class CoreSystemTypes{
    private CoreSystemTypes(){}
    internal static bool Initialized;
    internal static bool doNotLockFile;
    internal static bool getDebugInfo;
    internal static AssemblyNode.PostAssemblyLoadProcessor postAssemblyLoad;
    internal static bool IsInitialized { get { return Initialized; } }
    //system assembly (the basic runtime)
    public static AssemblyNode/*!*/ SystemAssembly;

    //Special base types
    public static Class/*!*/ Object;
    public static Class/*!*/ String;
    public static Class/*!*/ ValueType;
    public static Class/*!*/ Enum;
    public static Class/*!*/ MulticastDelegate;
    public static Class/*!*/ Array;
    public static Class/*!*/ Type;
    public static Class/*!*/ Delegate;
    public static Class/*!*/ Exception;
    public static Class/*!*/ Attribute;

    //primitive types
    public static Struct/*!*/ Boolean;
    public static Struct/*!*/ Char;
    public static Struct/*!*/ Int8;
    public static Struct/*!*/ UInt8;
    public static Struct/*!*/ Int16;
    public static Struct/*!*/ UInt16;
    public static Struct/*!*/ Int32;
    public static Struct/*!*/ UInt32;
    public static Struct/*!*/ Int64;
    public static Struct/*!*/ UInt64;
    public static Struct/*!*/ Single;
    public static Struct/*!*/ Double;
    public static Struct/*!*/ IntPtr;
    public static Struct/*!*/ UIntPtr;
    public static Struct/*!*/ DynamicallyTypedReference;

    //Classes need for System.TypeCode
    public static Class/*!*/ DBNull;
    public static Struct/*!*/ DateTime;
    public static Struct/*!*/ Decimal;

    //Special types
    public static Class/*!*/ IsVolatile;
    public static Struct/*!*/ Void;
    public static Struct/*!*/ ArgIterator;
    public static Struct/*!*/ RuntimeFieldHandle;
    public static Struct/*!*/ RuntimeMethodHandle;
    public static Struct/*!*/ RuntimeTypeHandle;
    public static Struct/*!*/ RuntimeArgumentHandle;

    //Special attributes    
    public static EnumNode SecurityAction;

    static CoreSystemTypes(){
      CoreSystemTypes.Initialize(TargetPlatform.DoNotLockFiles, TargetPlatform.GetDebugInfo);
    }

    public static void Clear(){
      lock (Module.GlobalLock){
        if (Reader.StaticAssemblyCache != null){
          foreach (AssemblyNode cachedAssembly in new System.Collections.ArrayList(Reader.StaticAssemblyCache.Values)){
            if (cachedAssembly != null) cachedAssembly.Dispose();
          }
          Reader.StaticAssemblyCache.Clear();
        }
        //Dispose the system assemblies in case they were not in the static cache. It is safe to dispose an assembly more than once.
        if (CoreSystemTypes.SystemAssembly != null && CoreSystemTypes.SystemAssembly != AssemblyNode.Dummy){
          CoreSystemTypes.SystemAssembly.Dispose();
          CoreSystemTypes.SystemAssembly = null;
        }
        CoreSystemTypes.ClearStatics();
        CoreSystemTypes.Initialized = false;
        TargetPlatform.AssemblyReferenceFor = new TrivialHashtable(0);
      }
    }
    public static void Initialize(bool doNotLockFile, bool getDebugInfo, AssemblyNode.PostAssemblyLoadProcessor postAssemblyLoad = null){
      CoreSystemTypes.doNotLockFile = doNotLockFile;
      CoreSystemTypes.getDebugInfo = getDebugInfo;
      CoreSystemTypes.postAssemblyLoad = postAssemblyLoad;
      if (CoreSystemTypes.Initialized) CoreSystemTypes.Clear();
      if (SystemAssembly == null)
        SystemAssembly = CoreSystemTypes.GetSystemAssembly(doNotLockFile, getDebugInfo, postAssemblyLoad);
      if (SystemAssembly == null) throw new InvalidOperationException(ExceptionStrings.InternalCompilerError);
      if (TargetPlatform.TargetVersion == null){
        TargetPlatform.TargetVersion = SystemAssembly.Version;
        if (TargetPlatform.TargetVersion == null)
          TargetPlatform.TargetVersion = typeof(object).Assembly.GetName().Version;
      }
      if (TargetPlatform.TargetVersion != null){
        if (TargetPlatform.TargetVersion.Major > 1 || TargetPlatform.TargetVersion.Minor > 1 ||
          (TargetPlatform.TargetVersion.Minor == 1 && TargetPlatform.TargetVersion.Build == 9999)){
          if (SystemAssembly.IsValidTypeName(StandardIds.System, Identifier.For("Nullable`1")))
            TargetPlatform.GenericTypeNamesMangleChar = '`';
          else if (SystemAssembly.IsValidTypeName(StandardIds.System, Identifier.For("Nullable!1")))
            TargetPlatform.GenericTypeNamesMangleChar = '!';
          else if (TargetPlatform.TargetVersion.Major == 1 && TargetPlatform.TargetVersion.Minor == 2)
            TargetPlatform.GenericTypeNamesMangleChar = (char)0;
        }
      }
      // This must be done in the order: Object, ValueType, Char, String
      // or else some of the generic type instantiations don't get filled
      // in correctly. (String ends up implementing IEnumerable<string>
      // instead of IEnumerable<char>.)
      Object = (Class)GetTypeNodeFor("System", "Object", ElementType.Object);
      ValueType = (Class)GetTypeNodeFor("System", "ValueType", ElementType.Class);
      Char = (Struct)GetTypeNodeFor("System", "Char", ElementType.Char);
      String = (Class)GetTypeNodeFor("System", "String", ElementType.String);
      Enum = (Class)GetTypeNodeFor("System", "Enum", ElementType.Class);
      MulticastDelegate = (Class)GetTypeNodeFor("System", "MulticastDelegate", ElementType.Class);
      Array = (Class)GetTypeNodeFor("System", "Array", ElementType.Class);
      Type = (Class)GetTypeNodeFor("System", "Type", ElementType.Class);
      Boolean = (Struct)GetTypeNodeFor("System", "Boolean", ElementType.Boolean);
      Int8 = (Struct)GetTypeNodeFor("System", "SByte", ElementType.Int8);
      UInt8 = (Struct)GetTypeNodeFor("System", "Byte", ElementType.UInt8);
      Int16 = (Struct)GetTypeNodeFor("System", "Int16", ElementType.Int16);
      UInt16 = (Struct)GetTypeNodeFor("System", "UInt16", ElementType.UInt16);
      Int32 = (Struct)GetTypeNodeFor("System", "Int32", ElementType.Int32);
      UInt32 = (Struct)GetTypeNodeFor("System", "UInt32", ElementType.UInt32);
      Int64 = (Struct)GetTypeNodeFor("System", "Int64", ElementType.Int64);
      UInt64 = (Struct)GetTypeNodeFor("System", "UInt64", ElementType.UInt64);
      Single = (Struct)GetTypeNodeFor("System", "Single", ElementType.Single);
      Double = (Struct)GetTypeNodeFor("System", "Double", ElementType.Double);
      IntPtr = (Struct)GetTypeNodeFor("System", "IntPtr", ElementType.IntPtr);
      UIntPtr = (Struct)GetTypeNodeFor("System", "UIntPtr", ElementType.UIntPtr);
      DynamicallyTypedReference = (Struct)GetTypeNodeFor("System", "TypedReference", ElementType.DynamicallyTypedReference);
      Delegate = (Class)GetTypeNodeFor("System", "Delegate", ElementType.Class);
      Exception = (Class)GetTypeNodeFor("System", "Exception", ElementType.Class);
      Attribute = (Class)GetTypeNodeFor("System", "Attribute", ElementType.Class);
      DBNull = (Class)GetTypeNodeFor("System", "DBNull", ElementType.Class);  //Where does this mscorlib type live in the new world of reference assemblies?
      DateTime = (Struct)GetTypeNodeFor("System", "DateTime", ElementType.ValueType);
      Decimal = (Struct)GetTypeNodeFor("System", "Decimal", ElementType.ValueType);
      ArgIterator = (Struct)GetTypeNodeFor("System", "ArgIterator", ElementType.ValueType); //Where does this mscorlib type live in the new world of reference assemblies?
      IsVolatile = (Class)GetTypeNodeFor("System.Runtime.CompilerServices", "IsVolatile", ElementType.Class);
      Void = (Struct)GetTypeNodeFor("System", "Void", ElementType.Void);
      RuntimeFieldHandle = (Struct)GetTypeNodeFor("System", "RuntimeFieldHandle", ElementType.ValueType);
      RuntimeMethodHandle = (Struct)GetTypeNodeFor("System", "RuntimeMethodHandle", ElementType.ValueType);
      RuntimeTypeHandle = (Struct)GetTypeNodeFor("System", "RuntimeTypeHandle", ElementType.ValueType);
      RuntimeArgumentHandle = (Struct)GetTypeNodeFor("System", "RuntimeArgumentHandle", ElementType.ValueType); //Where does this mscorlib type live in the new world of reference assemblies?
      SecurityAction = GetTypeNodeFor("System.Security.Permissions", "SecurityAction", ElementType.ValueType) as EnumNode; //Where does this mscorlib type live in the new world of reference assemblies?
      CoreSystemTypes.Initialized = true;
      CoreSystemTypes.InstantiateGenericInterfaces();
      Literal.Initialize();
      object dummy = TargetPlatform.AssemblyReferenceFor; //Force selection of target platform
      if (dummy == null) return;
    }
    private static void ClearStatics(){
      //Special base types
      Object = null;
      String = null;
      ValueType = null;
      Enum = null;
      MulticastDelegate = null;
      Array = null;
      Type = null;
      Delegate = null;
      Exception = null;
      Attribute = null;

      //primitive types
      Boolean = null;
      Char = null;
      Int8 = null;
      UInt8 = null;
      Int16 = null;
      UInt16 = null;
      Int32 = null;
      UInt32 = null;
      Int64 = null;
      UInt64 = null;
      Single = null;
      Double = null;
      IntPtr = null;
      UIntPtr = null;
      DynamicallyTypedReference = null;

      //Special types
      DBNull = null;
      DateTime = null;
      Decimal = null;
      RuntimeArgumentHandle = null;
      ArgIterator = null;
      RuntimeFieldHandle = null;
      RuntimeMethodHandle = null;
      RuntimeTypeHandle = null;
      IsVolatile = null;
      Void = null;
      SecurityAction = null;
    }
    private static void InstantiateGenericInterfaces(){
      if (TargetPlatform.TargetVersion != null && (TargetPlatform.TargetVersion.Major < 2 && TargetPlatform.TargetVersion.Minor < 2)) return;
      InstantiateGenericInterfaces(String);
      InstantiateGenericInterfaces(Boolean);
      InstantiateGenericInterfaces(Char);
      InstantiateGenericInterfaces(Int8);
      InstantiateGenericInterfaces(UInt8);
      InstantiateGenericInterfaces(Int16);
      InstantiateGenericInterfaces(UInt16);
      InstantiateGenericInterfaces(Int32);
      InstantiateGenericInterfaces(UInt32);
      InstantiateGenericInterfaces(Int64);
      InstantiateGenericInterfaces(UInt64);
      InstantiateGenericInterfaces(Single);
      InstantiateGenericInterfaces(Double);
      InstantiateGenericInterfaces(DBNull);
      InstantiateGenericInterfaces(DateTime);
      InstantiateGenericInterfaces(Decimal);
    }
    private static void InstantiateGenericInterfaces(TypeNode type){
      if (type == null) return;
      InterfaceList interfaces = type.Interfaces;
      for (int i = 0, n = interfaces == null ? 0 : interfaces.Count; i < n; i++){
        InterfaceExpression ifaceExpr = interfaces[i] as InterfaceExpression;
        if (ifaceExpr == null) continue;
        if (ifaceExpr.Template == null) {Debug.Assert(false); continue;}
        TypeNodeList templArgs = ifaceExpr.TemplateArguments;
        for (int j = 0, m = templArgs.Count; j < m; j++){
          InterfaceExpression ie = templArgs[j] as InterfaceExpression;
          if (ie != null) 
            templArgs[j] = ie.Template.GetGenericTemplateInstance(type.DeclaringModule, ie.ConsolidatedTemplateArguments);
        }
        interfaces[i] = (Interface)ifaceExpr.Template.GetGenericTemplateInstance(type.DeclaringModule, ifaceExpr.ConsolidatedTemplateArguments);
      }
    }

    private static AssemblyNode/*!*/ GetSystemAssembly(bool doNotLockFile, bool getDebugInfo, AssemblyNode.PostAssemblyLoadProcessor postAssemblyLoad) {
      AssemblyNode result = SystemAssemblyLocation.ParsedAssembly;
      if (result != null) {
        result.TargetRuntimeVersion = TargetPlatform.TargetRuntimeVersion;
        result.MetadataFormatMajorVersion = 1;
        result.MetadataFormatMinorVersion = 1;
        result.LinkerMajorVersion = 8;
        result.LinkerMinorVersion = 0;
        return result;
      }
      if (string.IsNullOrEmpty(SystemAssemblyLocation.Location))
        SystemAssemblyLocation.Location = typeof(object).Assembly.Location;
      result = (AssemblyNode)(new Reader(SystemAssemblyLocation.Location, SystemAssemblyLocation.SystemAssemblyCache, doNotLockFile, getDebugInfo, true, false)).ReadModule(postAssemblyLoad);
      if (result == null && TargetPlatform.TargetVersion != null && TargetPlatform.TargetVersion == typeof(object).Assembly.GetName().Version){
        SystemAssemblyLocation.Location = typeof(object).Assembly.Location;
        result = (AssemblyNode)(new Reader(SystemAssemblyLocation.Location, SystemAssemblyLocation.SystemAssemblyCache, doNotLockFile, getDebugInfo, true, false)).ReadModule(postAssemblyLoad);
      }
      if (result == null){
        result = new AssemblyNode();
        System.Reflection.AssemblyName aname = typeof(object).Assembly.GetName();
        result.Name = aname.Name;
        result.Version = TargetPlatform.TargetVersion;
        result.PublicKeyOrToken = aname.GetPublicKeyToken();
      }
      TargetPlatform.TargetVersion = result.Version;
      TargetPlatform.TargetRuntimeVersion = result.TargetRuntimeVersion;
      return result;
    }
    private static TypeNode/*!*/ GetTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemAssembly == null)
        Debug.Assert(false);
      else
        result = SystemAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }

    internal static TypeNode/*!*/ GetDummyTypeNode(AssemblyNode declaringAssembly, string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      switch (typeCode) {
        case ElementType.Object:
        case ElementType.String:
        case ElementType.Class:
          if (name.Length > 1 && name[0] == 'I' && char.IsUpper(name[1]))
            result = new Interface();
          else if (name == "MulticastDelegate" || name == "Delegate")
            result = new Class();
          else if (name.EndsWith("Callback") || name.EndsWith("Handler") || name.EndsWith("Delegate") || name == "ThreadStart" || name == "FrameGuardGetter" || name == "GuardThreadStart")
            result = new DelegateNode();
          else
            result = new Class();
          break;
        default:
          if (name == "CciMemberKind")
            result = new EnumNode();
          else
            result = new Struct();
          break;
      }
      result.Name = Identifier.For(name);
      result.Namespace = Identifier.For(nspace);
      result.DeclaringModule = declaringAssembly;

      return result;
    }
  }
  internal struct Delayed<T>
    where T:TypeNode
  {
    T cached;
    Func<TypeNode> delayed;

    public Delayed(Func<TypeNode> delayed)
    {
      this.delayed = delayed;
      this.cached = null;
    }

    public static implicit operator T(Delayed<T> delayed)
    {
      return delayed.GetValue();
    }

    public static implicit operator Delayed<T>(Func<TypeNode> delayed)
    {
      return new Delayed<T>(delayed);
    }


    private T GetValue()
    {
      if (cached == null && this.delayed != null)
      {
        this.cached = (T)this.delayed();
        this.delayed = null;
      }
      return this.cached;
    }

    public void Clear()
    {
      this.cached = null;
      this.delayed = null;
    }
  }
  public sealed class SystemTypes{
    private SystemTypes(){}
    internal static bool Initialized;
    public static bool IsInitialized { get { return Initialized; } }
    //system assembly (the basic runtime)
    public static AssemblyNode/*!*/ SystemAssembly {
      get{
          Contract.Ensures(Contract.Result<AssemblyNode>() != null);
          return CoreSystemTypes.SystemAssembly;
      }
      set{CoreSystemTypes.SystemAssembly = value;}
    }

    public static AssemblyNode/*!*/ CollectionsAssembly;
    public static AssemblyNode/*!*/ DiagnosticsDebugAssembly;
    public static AssemblyNode/*!*/ DiagnosticsToolsAssembly;
    public static AssemblyNode/*!*/ GlobalizationAssembly;
    public static AssemblyNode/*!*/ InteropAssembly;
    public static AssemblyNode/*!*/ IOAssembly;
    public static AssemblyNode/*!*/ ReflectionAssembly;
    public static AssemblyNode/*!*/ ResourceManagerAssembly;
    public static AssemblyNode/*!*/ SystemDllAssembly;
    public static AssemblyNode/*!*/ SystemRuntimeExtensionsAssembly;
    public static AssemblyNode/*!*/ SystemRuntimeSerializationAssembly;
    private static AssemblyNode systemRuntimeWindowsRuntimeAssembly;
    public static AssemblyNode/*!*/ SystemRuntimeWindowsRuntimeAssembly
    {
      get
      {
        if (systemRuntimeWindowsRuntimeAssembly == null) {
          systemRuntimeWindowsRuntimeAssembly = SystemTypes.GetSystemRuntimeWindowsRuntimeAssembly(CoreSystemTypes.doNotLockFile, CoreSystemTypes.getDebugInfo);
        }
        return systemRuntimeWindowsRuntimeAssembly;
      }
    }
    public static AssemblyNode/*!*/ SystemRuntimeWindowsRuntimeInteropAssembly;
    private static AssemblyNode/*!*/ systemRuntimeWindowsRuntimeUIXamlAssembly;
    public static AssemblyNode/*!*/ SystemRuntimeWindowsRuntimeUIXamlAssembly
    {
      get
      {
        if (systemRuntimeWindowsRuntimeUIXamlAssembly == null)
        {
          systemRuntimeWindowsRuntimeUIXamlAssembly = SystemTypes.GetSystemRuntimeWindowsRuntimeUIXamlAssembly(CoreSystemTypes.doNotLockFile, CoreSystemTypes.getDebugInfo);
        }
        return systemRuntimeWindowsRuntimeUIXamlAssembly;
      }
    }
    public static AssemblyNode/*!*/ ThreadingAssembly;

    //Special base types
    public static Class/*!*/ Object { 
        get {
            Contract.Ensures(Contract.Result<Class>() != null);
            return CoreSystemTypes.Object; 
        } 
    }
    public static Class/*!*/ String { 
        get {
            Contract.Ensures(Contract.Result<Class>() != null);
            return CoreSystemTypes.String; 
        } 
    }
    public static Class/*!*/ ValueType { get { return CoreSystemTypes.ValueType; } }
    public static Class/*!*/ Enum { get { return CoreSystemTypes.Enum; } }
    public static Class/*!*/ Delegate { get { return CoreSystemTypes.Delegate; } }
    public static Class/*!*/ MulticastDelegate { get { return CoreSystemTypes.MulticastDelegate; } }
    public static Class/*!*/ Array { get { return CoreSystemTypes.Array; } }
    public static Class/*!*/ Type { get { return CoreSystemTypes.Type; } }
    public static Class/*!*/ Exception { get { return CoreSystemTypes.Exception; } }
    public static Class/*!*/ Attribute { get { return CoreSystemTypes.Attribute; } }

    //primitive types
    public static Struct/*!*/ Boolean { get { return CoreSystemTypes.Boolean; } }
    public static Struct/*!*/ Char { get { return CoreSystemTypes.Char; } }
    public static Struct/*!*/ Int8 { get { return CoreSystemTypes.Int8; } }
    public static Struct/*!*/ UInt8 { get { return CoreSystemTypes.UInt8; } }
    public static Struct/*!*/ Int16 { get { return CoreSystemTypes.Int16; } }
    public static Struct/*!*/ UInt16 { get { return CoreSystemTypes.UInt16; } }
    public static Struct/*!*/ Int32 { get { return CoreSystemTypes.Int32; } }
    public static Struct/*!*/ UInt32 { get { return CoreSystemTypes.UInt32; } }
    public static Struct/*!*/ Int64 { get { return CoreSystemTypes.Int64; } }
    public static Struct/*!*/ UInt64 { get { return CoreSystemTypes.UInt64; } }
    public static Struct/*!*/ Single { get { return CoreSystemTypes.Single; } }
    public static Struct/*!*/ Double { get { return CoreSystemTypes.Double; } }
    public static Struct/*!*/ IntPtr { get { return CoreSystemTypes.IntPtr; } }
    public static Struct/*!*/ UIntPtr { get { return CoreSystemTypes.UIntPtr; } }
    public static Struct/*!*/ DynamicallyTypedReference { get { return CoreSystemTypes.DynamicallyTypedReference; } }

    // Types required for a complete rendering
    // of binary attribute information
    public static Class/*!*/ AttributeUsageAttribute;
    public static Class/*!*/ ConditionalAttribute;
    public static Class/*!*/ DefaultMemberAttribute;
    public static Class/*!*/ InternalsVisibleToAttribute;
    public static Class/*!*/ ObsoleteAttribute;

    // Types required to render arrays
    public static Interface/*!*/ GenericICollection;
    public static Interface/*!*/ GenericIEnumerable;
    public static Interface/*!*/ GenericIList;
    public static Interface/*!*/ ICloneable;
    public static Interface/*!*/ ICollection;
    public static Interface/*!*/ IEnumerable;
    public static Interface/*!*/ IList;

    //Special types
    public static Struct/*!*/ ArgIterator { get { return CoreSystemTypes.ArgIterator; } }
    public static Class/*!*/ IsVolatile { get { return CoreSystemTypes.IsVolatile; } }
    public static Struct/*!*/ Void { get { return CoreSystemTypes.Void; } }
    public static Struct/*!*/ RuntimeFieldHandle { get { return CoreSystemTypes.RuntimeTypeHandle; } }
    public static Struct/*!*/ RuntimeMethodHandle { get { return CoreSystemTypes.RuntimeTypeHandle; } }
    public static Struct/*!*/ RuntimeTypeHandle { get { return CoreSystemTypes.RuntimeTypeHandle; } }
    public static Struct/*!*/ RuntimeArgumentHandle { get { return CoreSystemTypes.RuntimeArgumentHandle; } }
   
    //Special attributes    
    public static Class/*!*/ AllowPartiallyTrustedCallersAttribute;
    public static Class/*!*/ AssemblyCompanyAttribute;
    public static Class/*!*/ AssemblyConfigurationAttribute;
    public static Class/*!*/ AssemblyCopyrightAttribute;
    public static Class/*!*/ AssemblyCultureAttribute;
    public static Class/*!*/ AssemblyDelaySignAttribute;
    public static Class/*!*/ AssemblyDescriptionAttribute;
    public static Class/*!*/ AssemblyFileVersionAttribute;
    public static Class/*!*/ AssemblyFlagsAttribute;
    public static Class/*!*/ AssemblyInformationalVersionAttribute;
    public static Class/*!*/ AssemblyKeyFileAttribute;
    public static Class/*!*/ AssemblyKeyNameAttribute;
    public static Class/*!*/ AssemblyProductAttribute;
    public static Class/*!*/ AssemblyTitleAttribute;
    public static Class/*!*/ AssemblyTrademarkAttribute;
    public static Class/*!*/ AssemblyVersionAttribute;
    public static Class/*!*/ ClassInterfaceAttribute;
    public static Class/*!*/ CLSCompliantAttribute;
    public static Class/*!*/ ComImportAttribute;
    public static Class/*!*/ ComRegisterFunctionAttribute;
    public static Class/*!*/ ComSourceInterfacesAttribute;
    public static Class/*!*/ ComUnregisterFunctionAttribute;
    public static Class/*!*/ ComVisibleAttribute;
    public static Class/*!*/ DebuggableAttribute;
    public static Class/*!*/ DebuggerHiddenAttribute;
    public static Class/*!*/ DebuggerStepThroughAttribute;
    public static EnumNode/*?*/ DebuggingModes;
    public static Class/*!*/ DllImportAttribute;
    public static Class/*!*/ FieldOffsetAttribute;
    public static Class/*!*/ FlagsAttribute;
    public static Class/*!*/ GuidAttribute;
    public static Class/*!*/ ImportedFromTypeLibAttribute;
    public static Class/*!*/ InAttribute;
    public static Class/*!*/ IndexerNameAttribute;
    public static Class/*!*/ InterfaceTypeAttribute;
    public static Class/*!*/ MethodImplAttribute;
    public static Class/*!*/ NonSerializedAttribute;
    public static Class/*!*/ OptionalAttribute;
    public static Class/*!*/ OutAttribute;
    public static Class/*!*/ ParamArrayAttribute;
    public static Class/*!*/ RuntimeCompatibilityAttribute;
    public static Class/*!*/ SatelliteContractVersionAttribute;
    public static Class/*!*/ SerializableAttribute;
    public static Class/*!*/ SecurityAttribute;
    public static Class/*!*/ SecurityCriticalAttribute;
    public static Class/*!*/ SecurityTransparentAttribute;
    public static Class/*!*/ SecurityTreatAsSafeAttribute;
    public static Class/*!*/ STAThreadAttribute;
    public static Class/*!*/ StructLayoutAttribute;
    public static Class/*!*/ SuppressMessageAttribute;
    public static Class/*!*/ SuppressUnmanagedCodeSecurityAttribute;
    public static EnumNode/*?*/ SecurityAction;

    //Classes need for System.TypeCode
    public static Class/*!*/ DBNull;
    public static Struct/*!*/ DateTime;
    public static Struct/*!*/ DateTimeOffset;
    public static Struct/*!*/ Decimal { get { return CoreSystemTypes.Decimal; } }
    public static Struct/*!*/ TimeSpan;

    //Classes and interfaces used by the Framework
    public static Class/*!*/ Activator;
    public static Class/*!*/ AppDomain;
    public static Class/*!*/ ApplicationException;
    public static Class/*!*/ ArgumentException;
    public static Class/*!*/ ArgumentNullException;
    public static Class/*!*/ ArgumentOutOfRangeException;
    public static Class/*!*/ ArrayList;
    public static DelegateNode/*!*/ AsyncCallback;
    public static Class/*!*/ Assembly;
    public static EnumNode/*?*/ AttributeTargets;
    public static Class/*!*/ CodeAccessPermission;
    public static Class/*!*/ CollectionBase;
    static Delayed<Struct>/*!*/ color;
    public static Struct Color { get { return color; } }
    static Delayed<Struct> cornerRadius;
    public static Struct/*!*/ CornerRadius { get { return cornerRadius; } }
    public static Class/*!*/ CultureInfo;
    public static Class/*!*/ DictionaryBase;
    public static Struct/*!*/ DictionaryEntry;
    public static Class/*!*/ DuplicateWaitObjectException;
    static Delayed<Struct> duration;
    public static Struct/*!*/ Duration { get { return duration; } }
    static Delayed<EnumNode> durationType;
    public static EnumNode/*?*/ DurationType { get { return durationType; } }
    public static Class/*!*/ Environment;
    public static Class/*!*/ EventArgs;
    public static DelegateNode/*?*/ EventHandler1;
    public static Struct/*!*/ EventRegistrationToken;
    public static Class/*!*/ ExecutionEngineException;
    static Delayed<Struct> generatorPosition;
    public static Struct/*!*/ GeneratorPosition { get { return generatorPosition; } }
    public static Struct/*!*/ GenericArraySegment;
    public static Class/*!*/ GenericArrayToIEnumerableAdapter;
    public static Class/*!*/ GenericDictionary;
    public static Interface/*!*/ GenericIComparable;
    public static Interface/*!*/ GenericIComparer;
    public static Interface/*!*/ GenericIDictionary;
    public static Interface/*!*/ GenericIEnumerator;
    public static Interface/*!*/ GenericIReadOnlyList;
    public static Interface/*!*/ GenericIReadOnlyDictionary;
    public static Struct/*!*/ GenericKeyValuePair;
    public static Class/*!*/ GenericList;
    public static Struct/*!*/ GenericNullable;
    public static Class/*!*/ GenericQueue;
    public static Class/*!*/ GenericSortedDictionary;
    public static Class/*!*/ GenericStack;
    public static Class/*!*/ GC;
    static Delayed<Struct> gridLength;
    public static Struct/*!*/ GridLength { get { return gridLength; } }
    static Delayed<EnumNode> gridUnitType;
    public static EnumNode/*?*/ GridUnitType { get { return gridUnitType; } }
    public static Struct/*!*/ Guid;
    public static Class/*!*/ __HandleProtector;
    public static Struct/*!*/ HandleRef;
    public static Class/*!*/ Hashtable;
    public static Interface/*!*/ IASyncResult;
    public static Interface/*!*/ ICommand;
    public static Interface/*!*/ IComparable;
    public static Interface/*!*/ IDictionary;
    public static Interface/*!*/ IComparer;
    public static Interface/*!*/ IDisposable;
    public static Interface/*!*/ IEnumerator;
    public static Interface/*!*/ IFormatProvider;
    public static Interface/*!*/ IHashCodeProvider;
    public static Interface/*!*/ IMembershipCondition;
    public static Interface/*!*/ INotifyPropertyChanged;
    public static Interface/*!*/ IBindableIterable;
    public static Interface/*!*/ IBindableVector;
    public static Interface/*!*/ INotifyCollectionChanged;
    public static Class/*!*/ IndexOutOfRangeException;
    public static Class/*!*/ InvalidCastException;
    public static Class/*!*/ InvalidOperationException;
    public static Interface/*!*/ IPermission;
    public static Interface/*!*/ ISerializable;
    public static Interface/*!*/ IStackWalk;
    static Delayed<Struct> keyTime;
    public static Struct/*!*/ KeyTime { get { return keyTime; } }
    public static Class/*!*/ Marshal;
    public static Class/*!*/ MarshalByRefObject;
    static Delayed<Struct> matrix;
    public static Struct/*!*/ Matrix { get { return matrix; } }
    static Delayed<Struct> matrix3D;
    public static Struct/*!*/ Matrix3D { get { return matrix3D; } }
    public static Class/*!*/ MemberInfo;
    public static Struct/*!*/ NativeOverlapped;
    public static Class/*!*/ Monitor;
    public static EnumNode/*?*/ NotifyCollectionChangedAction;
    public static Class/*!*/ NotifyCollectionChangedEventArgs;
    public static DelegateNode/*!*/ NotifyCollectionChangedEventHandler;
    public static Class/*!*/ NotSupportedException;
    public static Class/*!*/ NullReferenceException;
    public static Class/*!*/ OutOfMemoryException;
    public static Class/*!*/ ParameterInfo;
    public static Class/*!*/ PropertyChangedEventArgs;
    public static DelegateNode/*!*/ PropertyChangedEventHandler;
    static Delayed<Struct>/*!*/ point;
    public static Struct/*!*/ Point { get { return point; } }
    public static Class/*!*/ Queue;
    public static Class/*!*/ ReadOnlyCollectionBase;
    static Delayed<Struct>/*!*/ rect;
    public static Struct/*!*/ Rect { get { return rect; } }
    static Delayed<Struct> repeatBehavior;
    public static Struct/*!*/ RepeatBehavior { get { return repeatBehavior; } }
    static Delayed<EnumNode> repeatBehaviorType;
    public static EnumNode/*?*/ RepeatBehaviorType { get { return repeatBehaviorType; } }
    public static Class/*!*/ ResourceManager;
    public static Class/*!*/ ResourceSet;
    public static Class/*!*/ SerializationInfo;
    static Delayed<Struct> size;
    public static Struct/*!*/ Size { get { return size; } }
    public static Class/*!*/ Stack;
    public static Class/*!*/ StackOverflowException;
    public static Class/*!*/ Stream;
    public static Struct/*!*/ StreamingContext;
    public static Class/*!*/ StringBuilder;
    public static Class/*!*/ StringComparer;
    public static EnumNode/*?*/ StringComparison;
    public static Class/*!*/ SystemException;
    private static Delayed<Struct> thickness;
    public static Struct/*!*/ Thickness { get { return thickness; } }
    public static Class/*!*/ Thread;
    public static Class/*!*/ Uri;
    public static Class/*!*/ WindowsImpersonationContext;

    static SystemTypes(){
      SystemTypes.Initialize(TargetPlatform.DoNotLockFiles, TargetPlatform.GetDebugInfo);
    }

    public static void Clear(){
      lock (Module.GlobalLock){
        CoreSystemTypes.Clear();
        SystemTypes.ClearStatics();
        SystemTypes.Initialized = false;
      }
    }
    public static void Initialize(bool doNotLockFile, bool getDebugInfo, AssemblyNode.PostAssemblyLoadProcessor postAssemblyLoad = null){
      if (TargetPlatform.BusyWithClear) return;
      if (SystemTypes.Initialized){
        SystemTypes.Clear();
        CoreSystemTypes.Initialize(doNotLockFile, getDebugInfo, postAssemblyLoad);
      }else if (!CoreSystemTypes.Initialized){
        CoreSystemTypes.Initialize(doNotLockFile, getDebugInfo, postAssemblyLoad);
      }

      if (TargetPlatform.TargetVersion == null){
        TargetPlatform.TargetVersion = SystemAssembly.Version;
        if (TargetPlatform.TargetVersion == null)
          TargetPlatform.TargetVersion = typeof(object).Assembly.GetName().Version;
      }
      //TODO: throw an exception when the result is null
      CollectionsAssembly = SystemTypes.GetCollectionsAssembly(doNotLockFile, getDebugInfo);
      DiagnosticsDebugAssembly = SystemTypes.GetDiagnosticsDebugAssembly(doNotLockFile, getDebugInfo);
      DiagnosticsToolsAssembly = SystemTypes.GetDiagnosticsToolsAssembly(doNotLockFile, getDebugInfo);
      GlobalizationAssembly = SystemTypes.GetGlobalizationAssembly(doNotLockFile, getDebugInfo);  
      InteropAssembly = SystemTypes.GetInteropAssembly(doNotLockFile, getDebugInfo);
      IOAssembly = SystemTypes.GetIOAssembly(doNotLockFile, getDebugInfo);
      ReflectionAssembly = SystemTypes.GetReflectionAssembly(doNotLockFile, getDebugInfo);
      ResourceManagerAssembly = SystemTypes.GetResourceManagerAssembly(doNotLockFile, getDebugInfo);
      SystemDllAssembly = SystemTypes.GetSystemDllAssembly(doNotLockFile, getDebugInfo);
      SystemRuntimeExtensionsAssembly = SystemTypes.GetRuntimeExtensionsAssembly(doNotLockFile, getDebugInfo);
      SystemRuntimeSerializationAssembly = SystemTypes.GetRuntimeSerializationAssembly(doNotLockFile, getDebugInfo);
      SystemRuntimeWindowsRuntimeInteropAssembly = SystemTypes.GetWindowsRuntimeInteropAssembly(doNotLockFile, getDebugInfo);
      ThreadingAssembly = SystemTypes.GetThreadingAssembly(doNotLockFile, getDebugInfo);

      AttributeUsageAttribute = (Class)GetTypeNodeFor("System", "AttributeUsageAttribute", ElementType.Class);
      ConditionalAttribute = (Class)GetTypeNodeFor("System.Diagnostics", "ConditionalAttribute", ElementType.Class);
      DefaultMemberAttribute = (Class)GetTypeNodeFor("System.Reflection", "DefaultMemberAttribute", ElementType.Class);
      InternalsVisibleToAttribute = (Class)GetTypeNodeFor("System.Runtime.CompilerServices", "InternalsVisibleToAttribute", ElementType.Class);
      ObsoleteAttribute = (Class)GetTypeNodeFor("System", "ObsoleteAttribute", ElementType.Class);

      GenericICollection = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "ICollection", 1, ElementType.Class);
      GenericIEnumerable = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IEnumerable", 1, ElementType.Class);
      GenericIList = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IList", 1, ElementType.Class);
      ICloneable = (Interface)GetTypeNodeFor("System", "ICloneable", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      ICollection = (Interface)GetTypeNodeFor("System.Collections", "ICollection", ElementType.Class);
      IEnumerable = (Interface)GetTypeNodeFor("System.Collections", "IEnumerable", ElementType.Class);
      IList = (Interface)GetTypeNodeFor("System.Collections", "IList", ElementType.Class);

      AllowPartiallyTrustedCallersAttribute = (Class)GetTypeNodeFor("System.Security", "AllowPartiallyTrustedCallersAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      AssemblyCompanyAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyCompanyAttribute", ElementType.Class);
      AssemblyConfigurationAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyConfigurationAttribute", ElementType.Class);
      AssemblyCopyrightAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyCopyrightAttribute", ElementType.Class);
      AssemblyCultureAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyCultureAttribute", ElementType.Class);
      AssemblyDelaySignAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyDelaySignAttribute", ElementType.Class);
      AssemblyDescriptionAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyDescriptionAttribute", ElementType.Class);
      AssemblyFileVersionAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyFileVersionAttribute", ElementType.Class);
      AssemblyFlagsAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyFlagsAttribute", ElementType.Class);
      AssemblyInformationalVersionAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyInformationalVersionAttribute", ElementType.Class);
      AssemblyKeyFileAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyKeyFileAttribute", ElementType.Class);
      AssemblyKeyNameAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyKeyNameAttribute", ElementType.Class); 
      AssemblyProductAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyProductAttribute", ElementType.Class);
      AssemblyTitleAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyTitleAttribute", ElementType.Class);
      AssemblyTrademarkAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyTrademarkAttribute", ElementType.Class);
      AssemblyVersionAttribute = (Class)GetTypeNodeFor("System.Reflection", "AssemblyVersionAttribute", ElementType.Class);
      AttributeTargets =  GetTypeNodeFor("System", "AttributeTargets", ElementType.ValueType) as EnumNode;
      color = new Func<TypeNode>(() => GetWindowsRuntimeTypeNodeFor("Windows.UI", "Color", ElementType.ValueType)); //projected from Windows.UI.Xaml.Media, but to where?
      cornerRadius = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "CornerRadius", ElementType.ValueType)); //projected from Windows.UI.Xaml but to where?
      ClassInterfaceAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ClassInterfaceAttribute", ElementType.Class);
      CLSCompliantAttribute = (Class)GetTypeNodeFor("System", "CLSCompliantAttribute", ElementType.Class);
      ComImportAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ComImportAttribute", ElementType.Class);
      ComRegisterFunctionAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ComRegisterFunctionAttribute", ElementType.Class);
      ComSourceInterfacesAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ComSourceInterfacesAttribute", ElementType.Class);
      ComUnregisterFunctionAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ComUnregisterFunctionAttribute", ElementType.Class);
      ComVisibleAttribute = (Class)GetTypeNodeFor("System.Runtime.InteropServices", "ComVisibleAttribute", ElementType.Class);      
      DebuggableAttribute = (Class)GetTypeNodeFor("System.Diagnostics", "DebuggableAttribute", ElementType.Class);
      DebuggerHiddenAttribute = (Class)GetDiagnosticsDebugTypeNodeFor("System.Diagnostics", "DebuggerHiddenAttribute", ElementType.Class);
      DebuggerStepThroughAttribute = (Class)GetDiagnosticsDebugTypeNodeFor("System.Diagnostics", "DebuggerStepThroughAttribute", ElementType.Class);
      DebuggingModes = DebuggableAttribute == null ? null : DebuggableAttribute.GetNestedType(Identifier.For("DebuggingModes")) as EnumNode;
      DllImportAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "DllImportAttribute", ElementType.Class);
      duration = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "Duration", ElementType.ValueType)); //projected from Windows.UI.Xaml but to where?
      durationType = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "DurationType", ElementType.ValueType) as EnumNode); //projected from Windows.UI.Xaml but to where?
      FieldOffsetAttribute = (Class)GetTypeNodeFor("System.Runtime.InteropServices", "FieldOffsetAttribute", ElementType.Class);
      FlagsAttribute = (Class)GetTypeNodeFor("System", "FlagsAttribute", ElementType.Class);
      generatorPosition = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition", ElementType.ValueType)); //projected from Windows.UI.Xaml.Controls.Primitives but to where?
      Guid = (Struct)GetTypeNodeFor("System", "Guid", ElementType.ValueType);
      GuidAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "GuidAttribute", ElementType.Class);
      gridLength = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "GridLength", ElementType.ValueType)); //projected from Windows.UI.Xaml but to where?
      gridUnitType = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "GridUnitType", ElementType.ValueType) as EnumNode); //projected from Windows.UI.Xaml but to where?
      ImportedFromTypeLibAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "ImportedFromTypeLibAttribute", ElementType.Class);
      InAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "InAttribute", ElementType.Class);
      IndexerNameAttribute = (Class)GetTypeNodeFor("System.Runtime.CompilerServices", "IndexerNameAttribute", ElementType.Class);
      InterfaceTypeAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "InterfaceTypeAttribute", ElementType.Class);
      keyTime = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Media.Animation", "KeyTime", ElementType.ValueType)); //projected from Windows.UI.Xaml.Media.Animation but to where?
      repeatBehavior = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Media.Animation", "RepeatBehavior", ElementType.ValueType)); //projected from Windows.UI.Xaml.Media.Animation but to where?
      repeatBehaviorType = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType", ElementType.ValueType) as EnumNode); //projected from ?? to ??
      MethodImplAttribute = (Class)GetTypeNodeFor("System.Runtime.CompilerServices", "MethodImplAttribute", ElementType.Class);
      matrix = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Media", "Matrix", ElementType.ValueType)); //projected from Windows.UI.Xaml.Media but to where?
      matrix3D = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml.Media.Media3D", "Matrix3D", ElementType.ValueType)); //projected from Windows.UI.Xaml.Media.Media3D but to where?
      NonSerializedAttribute = (Class)GetTypeNodeFor("System", "NonSerializedAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?     
      OptionalAttribute = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "OptionalAttribute", ElementType.Class);
      OutAttribute = (Class)GetTypeNodeFor("System.Runtime.InteropServices", "OutAttribute", ElementType.Class);
      ParamArrayAttribute = (Class)GetTypeNodeFor("System", "ParamArrayAttribute", ElementType.Class);
      point = new Func<TypeNode>(() => GetWindowsRuntimeTypeNodeFor("Windows.Foundation", "Point", ElementType.ValueType));
      rect = new Func<TypeNode>(() => GetWindowsRuntimeTypeNodeFor("Windows.Foundation", "Rect", ElementType.ValueType));
      RuntimeCompatibilityAttribute = (Class)GetTypeNodeFor("System.Runtime.CompilerServices", "RuntimeCompatibilityAttribute", ElementType.Class);
      SatelliteContractVersionAttribute = (Class)GetResourceManagerTypeNodeFor("System.Resources", "SatelliteContractVersionAttribute", ElementType.Class);
      SerializableAttribute = (Class)GetTypeNodeFor("System", "SerializableAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      SecurityAttribute = (Class)GetTypeNodeFor("System.Security.Permissions", "SecurityAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      SecurityCriticalAttribute = (Class)GetTypeNodeFor("System.Security", "SecurityCriticalAttribute", ElementType.Class);
      SecurityTransparentAttribute = (Class)GetTypeNodeFor("System.Security", "SecurityTransparentAttribute", ElementType.Class);
      SecurityTreatAsSafeAttribute = (Class)GetTypeNodeFor("System.Security", "SecurityTreatAsSafeAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      size = new Func<TypeNode>(() => GetWindowsRuntimeTypeNodeFor("Windows.Foundation", "Size", ElementType.ValueType));
      STAThreadAttribute = (Class)GetTypeNodeFor("System", "STAThreadAttribute", ElementType.Class);
      StructLayoutAttribute = (Class)GetTypeNodeFor("System.Runtime.InteropServices", "StructLayoutAttribute", ElementType.Class);
      SuppressMessageAttribute = (Class)GetDiagnosticsToolsTypeNodeFor("System.Diagnostics.CodeAnalysis", "SuppressMessageAttribute", ElementType.Class);
      SuppressUnmanagedCodeSecurityAttribute = (Class)GetTypeNodeFor("System.Security", "SuppressUnmanagedCodeSecurityAttribute", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      SecurityAction = GetTypeNodeFor("System.Security.Permissions", "SecurityAction", ElementType.ValueType) as EnumNode; //Where does this mscorlib type live in the new world of reference assemblies?
      DBNull = (Class)GetTypeNodeFor("System", "DBNull", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      DateTime = (Struct)GetTypeNodeFor("System", "DateTime", ElementType.ValueType);
      DateTimeOffset = (Struct)GetTypeNodeFor("System", "DateTimeOffset", ElementType.ValueType);
      TimeSpan = (Struct)GetTypeNodeFor("System", "TimeSpan", ElementType.ValueType);
      Activator = (Class)GetTypeNodeFor("System", "Activator", ElementType.Class);
      AppDomain = (Class)GetTypeNodeFor("System", "AppDomain", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      ApplicationException = (Class)GetTypeNodeFor("System", "ApplicationException", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      ArgumentException = (Class)GetTypeNodeFor("System", "ArgumentException", ElementType.Class);
      ArgumentNullException = (Class)GetTypeNodeFor("System", "ArgumentNullException", ElementType.Class);
      ArgumentOutOfRangeException = (Class)GetTypeNodeFor("System", "ArgumentOutOfRangeException", ElementType.Class);
      ArrayList = (Class)GetTypeNodeFor("System.Collections", "ArrayList", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      AsyncCallback = (DelegateNode)GetTypeNodeFor("System", "AsyncCallback", ElementType.Class);
      Assembly = (Class)GetReflectionTypeNodeFor("System.Reflection", "Assembly", ElementType.Class);
      CodeAccessPermission = (Class)GetTypeNodeFor("System.Security", "CodeAccessPermission", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      CollectionBase = (Class)GetTypeNodeFor("System.Collections", "CollectionBase", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      CultureInfo = (Class)GetGlobalizationTypeNodeFor("System.Globalization", "CultureInfo", ElementType.Class);
      DictionaryBase = (Class)GetTypeNodeFor("System.Collections", "DictionaryBase", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      DictionaryEntry = (Struct)GetTypeNodeFor("System.Collections", "DictionaryEntry", ElementType.ValueType); //Where does this mscorlib type live in the new world of reference assemblies?
      DuplicateWaitObjectException = (Class)GetTypeNodeFor("System", "DuplicateWaitObjectException", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      Environment = (Class)GetRuntimeExtensionsTypeNodeFor("System", "Environment", ElementType.Class);
      EventArgs = (Class)GetTypeNodeFor("System", "EventArgs", ElementType.Class);
      EventHandler1 = GetGenericRuntimeTypeNodeFor("System", "EventHandler", 1, ElementType.Class) as DelegateNode;
      EventRegistrationToken = (Struct)GetWindowsRuntimeInteropTypeNodeFor("System.Runtime.InteropServices.WindowsRuntime", "EventRegistrationToken", ElementType.ValueType);
      ExecutionEngineException = (Class)GetTypeNodeFor("System", "ExecutionEngineException", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      GenericArraySegment = (Struct)GetGenericRuntimeTypeNodeFor("System", "ArraySegment", 1, ElementType.ValueType);
      GenericDictionary = (Class)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "Dictionary", 2, ElementType.Class);
      GenericIComparable = (Interface)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "IComparable", 1, ElementType.Class);
      GenericIComparer = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IComparer", 1, ElementType.Class);
      GenericIDictionary = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IDictionary", 2, ElementType.Class);
      GenericIEnumerator = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IEnumerator", 1, ElementType.Class);
      GenericIReadOnlyDictionary = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IReadOnlyDictionary", 2, ElementType.Class);
      GenericIReadOnlyList = (Interface)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "IReadOnlyList", 1, ElementType.Class);
      GenericKeyValuePair = (Struct)GetGenericRuntimeTypeNodeFor("System.Collections.Generic", "KeyValuePair", 2, ElementType.ValueType);
      GenericList = (Class)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "List", 1, ElementType.Class);
      GenericNullable = (Struct)GetGenericRuntimeTypeNodeFor("System", "Nullable", 1, ElementType.ValueType);
      GenericQueue = (Class)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "Queue", 1, ElementType.Class);
      GenericSortedDictionary = (Class)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "SortedDictionary", 2, ElementType.Class);
      GenericStack = (Class)GetCollectionsGenericRuntimeTypeNodeFor("System.Collections.Generic", "Stack", 1, ElementType.Class);
      GC = (Class)GetRuntimeExtensionsTypeNodeFor("System", "GC", ElementType.Class);
      __HandleProtector = (Class)GetTypeNodeFor("System.Threading", "__HandleProtector", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      HandleRef = (Struct)GetInteropTypeNodeFor("System.Runtime.InteropServices", "HandleRef", ElementType.ValueType);
      Hashtable = (Class)GetTypeNodeFor("System.Collections", "Hashtable", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IASyncResult = (Interface)GetTypeNodeFor("System", "IAsyncResult", ElementType.Class);
      ICommand = (Interface)GetTypeNodeFor("System.Windows.Input", "ICommand", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IComparable = (Interface)GetTypeNodeFor("System", "IComparable", ElementType.Class);
      IComparer = (Interface)GetTypeNodeFor("System.Collections", "IComparer", ElementType.Class);
      IDictionary = (Interface)GetTypeNodeFor("System.Collections", "IDictionary", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IDisposable = (Interface)GetTypeNodeFor("System", "IDisposable", ElementType.Class);
      IEnumerator = (Interface)GetTypeNodeFor("System.Collections", "IEnumerator", ElementType.Class);
      IFormatProvider = (Interface)GetTypeNodeFor("System", "IFormatProvider", ElementType.Class);
      IHashCodeProvider = (Interface)GetTypeNodeFor("System.Collections", "IHashCodeProvider", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IMembershipCondition = (Interface)GetTypeNodeFor("System.Security.Policy", "IMembershipCondition", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IndexOutOfRangeException = (Class)GetTypeNodeFor("System", "IndexOutOfRangeException", ElementType.Class);
      IBindableIterable = (Interface)GetTypeNodeFor("System.Collections", "IBindableIterable", ElementType.Class);
      IBindableVector = (Interface)GetTypeNodeFor("System.Collections", "IBindableVector", ElementType.Class);
      INotifyCollectionChanged = (Interface)GetSystemTypeNodeFor("System.Collections.Specialized", "INotifyCollectionChanged", ElementType.Class);
      INotifyPropertyChanged = (Interface)GetSystemTypeNodeFor("System.ComponentModel", "INotifyPropertyChanged", ElementType.Class);
      InvalidCastException = (Class)GetTypeNodeFor("System", "InvalidCastException", ElementType.Class);
      InvalidOperationException = (Class)GetTypeNodeFor("System", "InvalidOperationException", ElementType.Class);
      IPermission = (Interface)GetTypeNodeFor("System.Security", "IPermission", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      ISerializable = (Interface)GetTypeNodeFor("System.Runtime.Serialization", "ISerializable", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      IStackWalk = (Interface)GetTypeNodeFor("System.Security", "IStackWalk", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      Marshal = (Class)GetInteropTypeNodeFor("System.Runtime.InteropServices", "Marshal", ElementType.Class);
      MarshalByRefObject = (Class)GetTypeNodeFor("System", "MarshalByRefObject", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      MemberInfo = (Class)GetReflectionTypeNodeFor("System.Reflection", "MemberInfo", ElementType.Class);
      Monitor = (Class)GetThreadingTypeNodeFor("System.Threading", "Monitor", ElementType.Class);
      NativeOverlapped = (Struct)GetThreadingTypeNodeFor("System.Threading", "NativeOverlapped", ElementType.ValueType);
      NotifyCollectionChangedAction = GetSystemTypeNodeFor("System.Collections.Specialized", "NotifyCollectionChangedAction", ElementType.ValueType) as EnumNode;
      NotifyCollectionChangedEventArgs = (Class)GetSystemTypeNodeFor("System.Collections.Specialized", "NotifyCollectionChangedEventArgs", ElementType.Class);
      NotifyCollectionChangedEventHandler = (DelegateNode)GetSystemTypeNodeFor("System.Collections.Specialized", "NotifyCollectionChangedEventHandler", ElementType.Class);
      NotSupportedException = (Class)GetTypeNodeFor("System", "NotSupportedException", ElementType.Class);
      NullReferenceException = (Class)GetTypeNodeFor("System", "NullReferenceException", ElementType.Class);
      OutOfMemoryException = (Class)GetTypeNodeFor("System", "OutOfMemoryException", ElementType.Class);
      ParameterInfo = (Class)GetReflectionTypeNodeFor("System.Reflection", "ParameterInfo", ElementType.Class);
      PropertyChangedEventArgs = (Class)GetSystemTypeNodeFor("System.ComponentModel", "PropertyChangedEventArgs", ElementType.Class);
      PropertyChangedEventHandler = (DelegateNode)GetSystemTypeNodeFor("System.ComponentModel", "PropertyChangedEventHandler", ElementType.Class);
      Queue = (Class)GetCollectionsTypeNodeFor("System.Collections", "Queue", ElementType.Class);
      ReadOnlyCollectionBase = (Class)GetTypeNodeFor("System.Collections", "ReadOnlyCollectionBase", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      ResourceManager = (Class)GetResourceManagerTypeNodeFor("System.Resources", "ResourceManager", ElementType.Class);
      ResourceSet = (Class)GetTypeNodeFor("System.Resources", "ResourceSet", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      SerializationInfo = (Class)GetTypeNodeFor("System.Runtime.Serialization", "SerializationInfo", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      Stack = (Class)GetTypeNodeFor("System.Collections", "Stack", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      StackOverflowException = (Class)GetTypeNodeFor("System", "StackOverflowException", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      Stream = (Class)GetIOTypeNodeFor("System.IO", "Stream", ElementType.Class);
      StreamingContext = (Struct)GetRuntimeSerializationTypeNodeFor("System.Runtime.Serialization", "StreamingContext", ElementType.ValueType);
      StringBuilder = (Class)GetTypeNodeFor("System.Text", "StringBuilder", ElementType.Class);
      StringComparer = (Class)GetRuntimeExtensionsTypeNodeFor("System", "StringComparer", ElementType.Class);
      StringComparison =  GetTypeNodeFor("System", "StringComparison", ElementType.ValueType) as EnumNode;
      SystemException = (Class)GetTypeNodeFor("System", "SystemException", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      Thread = (Class)GetThreadingTypeNodeFor("System.Threading", "Thread", ElementType.Class);
      thickness = new Func<TypeNode>(() => GetWindowsRuntimeUIXamlTypeNodeFor("Windows.UI.Xaml", "Thickness", ElementType.ValueType)); //projected from Windows.UI.Xaml but to where?
      Uri = (Class)GetSystemTypeNodeFor("System", "Uri", ElementType.Class);
      WindowsImpersonationContext = (Class)GetTypeNodeFor("System.Security.Principal", "WindowsImpersonationContext", ElementType.Class); //Where does this mscorlib type live in the new world of reference assemblies?
      SystemTypes.Initialized = true;
      object dummy = TargetPlatform.AssemblyReferenceFor; //Force selection of target platform
      if (dummy == null) return;
    }
    private static void ClearStatics(){
      systemRuntimeWindowsRuntimeAssembly = null;
      systemRuntimeWindowsRuntimeUIXamlAssembly = null;
      AttributeUsageAttribute = null;
      ConditionalAttribute = null;
      DefaultMemberAttribute = null;
      InternalsVisibleToAttribute = null;
      ObsoleteAttribute = null;

      GenericICollection = null;
      GenericIEnumerable = null;
      GenericIList = null;
      ICloneable = null;
      ICollection = null;
      IEnumerable = null;
      IList = null;
      //Special attributes    
      AllowPartiallyTrustedCallersAttribute = null;
      AssemblyCompanyAttribute = null;
      AssemblyConfigurationAttribute = null;
      AssemblyCopyrightAttribute = null;
      AssemblyCultureAttribute = null;
      AssemblyDelaySignAttribute = null;
      AssemblyDescriptionAttribute = null;
      AssemblyFileVersionAttribute = null;
      AssemblyFlagsAttribute = null;
      AssemblyInformationalVersionAttribute = null;
      AssemblyKeyFileAttribute = null;
      AssemblyKeyNameAttribute = null;
      AssemblyProductAttribute = null;
      AssemblyTitleAttribute = null;
      AssemblyTrademarkAttribute = null;
      AssemblyVersionAttribute = null;
      ClassInterfaceAttribute = null;
      CLSCompliantAttribute = null;
      ComImportAttribute = null;
      ComRegisterFunctionAttribute = null;
      ComSourceInterfacesAttribute = null;
      ComUnregisterFunctionAttribute = null;
      ComVisibleAttribute = null;
      DebuggableAttribute = null;
      DebuggerHiddenAttribute = null;
      DebuggerStepThroughAttribute = null;
      DebuggingModes = null;
      DllImportAttribute = null;
      FieldOffsetAttribute = null;
      FlagsAttribute = null;
      GuidAttribute = null;
      ImportedFromTypeLibAttribute = null;
      InAttribute = null;
      IndexerNameAttribute = null;
      InterfaceTypeAttribute = null;
      MethodImplAttribute = null;
      NonSerializedAttribute = null;
      OptionalAttribute = null;
      OutAttribute = null;
      ParamArrayAttribute = null;
      RuntimeCompatibilityAttribute = null;
      SatelliteContractVersionAttribute = null;
      SerializableAttribute = null;
      SecurityAttribute = null;
      SecurityCriticalAttribute = null;
      SecurityTransparentAttribute = null;
      SecurityTreatAsSafeAttribute = null;
      STAThreadAttribute = null;
      StructLayoutAttribute = null;
      SuppressMessageAttribute = null;
      SuppressUnmanagedCodeSecurityAttribute = null;
      SecurityAction = null;

      //Classes need for System.TypeCode
      DBNull = null;
      DateTime = null;
      DateTimeOffset = null;
      TimeSpan = null;

      //Classes and interfaces used by the Framework
      Activator = null;
      AppDomain = null;
      ApplicationException = null;
      ArgumentException = null;
      ArgumentNullException = null;
      ArgumentOutOfRangeException = null;
      ArrayList = null;
      AsyncCallback = null;
      Assembly = null;
      AttributeTargets = null;
      CodeAccessPermission = null;
      CollectionBase = null;
      color.Clear();
      cornerRadius.Clear();
      CultureInfo = null;
      DictionaryBase = null;
      DictionaryEntry = null;
      DuplicateWaitObjectException = null;
      duration.Clear();
      durationType.Clear();
      Environment = null;
      EventArgs = null;
      EventRegistrationToken = null;
      ExecutionEngineException = null;
      generatorPosition.Clear();
      GenericArraySegment = null;
      GenericArrayToIEnumerableAdapter = null;
      GenericDictionary = null;
      GenericIComparable = null;
      GenericIComparer = null;
      GenericIDictionary = null;
      GenericIEnumerator = null;
      GenericKeyValuePair = null;
      GenericList = null;
      GenericNullable = null;
      GenericQueue = null;
      GenericSortedDictionary = null;
      GenericStack = null;
      GC = null;
      gridLength.Clear();
      gridUnitType.Clear();
      Guid = null;
      __HandleProtector = null;
      HandleRef = null;
      Hashtable = null;
      IASyncResult = null;
      IComparable = null;
      IDictionary = null;
      IComparer = null;
      IDisposable = null;
      IEnumerator = null;
      IFormatProvider = null;
      IHashCodeProvider = null;
      IMembershipCondition = null;
      IndexOutOfRangeException = null;
      InvalidCastException = null;
      InvalidOperationException = null;
      IPermission = null;
      ISerializable = null;
      IStackWalk = null;
      keyTime.Clear();
      Marshal = null;
      MarshalByRefObject = null;
      matrix.Clear();
      matrix3D.Clear();
      MemberInfo = null;
      NativeOverlapped = null;
      Monitor = null;
      NotSupportedException = null;
      NullReferenceException = null;
      OutOfMemoryException = null;
      ParameterInfo = null;
      point.Clear();
      Queue = null;
      ReadOnlyCollectionBase = null;
      rect.Clear();
      repeatBehavior.Clear();
      repeatBehaviorType.Clear();
      ResourceManager = null;
      ResourceSet = null;
      SerializationInfo = null;
      size.Clear();
      Stack = null;
      StackOverflowException = null;
      Stream = null;
      StreamingContext = null;
      StringBuilder = null;
      StringComparer = null;
      StringComparison = null;
      SystemException = null;
      thickness.Clear();
      Thread = null;
      Uri = null;
      WindowsImpersonationContext = null;
    }

    private static AssemblyNode/*!*/ GetSystemDllAssembly(bool doNotLockFile, bool getDebugInfo) {
      System.Reflection.AssemblyName aName = typeof(System.Uri).Assembly.GetName();
      Identifier SystemId = Identifier.For(aName.Name);
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[SystemId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = aName.Name;
        aref.PublicKeyOrToken = aName.GetPublicKeyToken();
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[SystemId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemDllAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemDllAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.dll");
        else
          SystemDllAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemDllAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetGenericRuntimeTypeNodeFor(string/*!*/ nspace, string/*!*/ name, int numParams, ElementType typeCode) {
      if (TargetPlatform.GenericTypeNamesMangleChar != 0) name = name + TargetPlatform.GenericTypeNamesMangleChar + numParams;
        return SystemTypes.GetTypeNodeFor(nspace, name, typeCode);
    }
    private static TypeNode/*!*/ GetTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemAssembly == null)
        Debug.Assert(false);
      else
        result = SystemAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static TypeNode/*!*/ GetSystemTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemDllAssembly == null)
        Debug.Assert(false);
      else
        result = SystemDllAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemDllAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    public static AssemblyNode/*!*/ GetSystemCoreAssembly(bool doNotLockFile, bool getDebugInfo)
    {
        Identifier AssemblyId = Identifier.For("System.Core");
        AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
        if (aref == null)
        {
            aref = new AssemblyReference();
            aref.Name = "System.Core";
            // b77a5c561934e089 (got from sn -T System.Core for .NET 4.0 assembly)
            aref.PublicKeyOrToken = new byte[] { 0xB7, 0x7A, 0x5C, 0x56, 0x19, 0x34, 0xE0, 0x89 };
            aref.Version = TargetPlatform.TargetVersion;
            TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
        }
        if (string.IsNullOrEmpty(SystemCoreAssemblyLocation.Location))
        {
            if (aref.Location == null)
                SystemCoreAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Core.dll");
            else
                SystemCoreAssemblyLocation.Location = aref.Location;
        }
        if (aref.assembly == null) aref.Location = SystemCoreAssemblyLocation.Location;
        return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static AssemblyNode/*!*/ GetCollectionsAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Collections");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Collections";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeCollectionsAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeCollectionsAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Collections.dll");
        else
          SystemRuntimeCollectionsAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeCollectionsAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetCollectionsGenericRuntimeTypeNodeFor(string/*!*/ nspace, string/*!*/ name, int numParams, ElementType typeCode) {
      if (TargetPlatform.GenericTypeNamesMangleChar != 0) name = name + TargetPlatform.GenericTypeNamesMangleChar +numParams;
      return SystemTypes.GetCollectionsTypeNodeFor(nspace, name, typeCode);
    }
    private static TypeNode/*!*/ GetCollectionsTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (CollectionsAssembly == null)
        Debug.Assert(false);
      else
        result = CollectionsAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(CollectionsAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetDiagnosticsDebugAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Diagnostics.Debug");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Diagnostics.Debug";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemDiagnosticsDebugAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemDiagnosticsDebugAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Diagnostics.Debug.dll");
        else
          SystemDiagnosticsDebugAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemDiagnosticsDebugAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetDiagnosticsDebugTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (DiagnosticsDebugAssembly == null)
        Debug.Assert(false);
      else
        result = DiagnosticsDebugAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(DiagnosticsDebugAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetDiagnosticsToolsAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Diagnostics.Tools");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Diagnostics.Debug";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemDiagnosticsToolsAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemDiagnosticsToolsAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Diagnostics.Tools.dll");
        else
          SystemDiagnosticsToolsAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemDiagnosticsToolsAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetDiagnosticsToolsTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (DiagnosticsToolsAssembly == null)
        Debug.Assert(false);
      else
        result = DiagnosticsToolsAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(DiagnosticsToolsAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetInteropAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Runtime.InteropServices");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Runtime.InteropServices";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeInteropServicesAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeInteropServicesAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Runtime.InteropServices.dll");
        else
          SystemRuntimeInteropServicesAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeInteropServicesAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetInteropTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemAssembly == null)
        Debug.Assert(false);
      else
        result = InteropAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(InteropAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    
    private static AssemblyNode/*!*/ GetIOAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.IO");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.IO";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeIOServicesAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeIOServicesAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.IO.dll");
        else
          SystemRuntimeIOServicesAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeIOServicesAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetIOTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (IOAssembly == null)
        Debug.Assert(false);
      else
        result = IOAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(IOAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetReflectionAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Reflection");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Reflection";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemReflectionAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemReflectionAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Reflection.dll");
        else
          SystemReflectionAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemReflectionAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetReflectionTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (ReflectionAssembly == null)
        Debug.Assert(false);
      else
        result = ReflectionAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(ReflectionAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetResourceManagerAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Resources.ResourceManager");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Resources.ResourceManager";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemResourceManagerAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemResourceManagerAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Resources.ResourceManager.dll");
        else
          SystemResourceManagerAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemResourceManagerAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetResourceManagerTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (ReflectionAssembly == null)
        Debug.Assert(false);
      else
        result = ReflectionAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(ReflectionAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetGlobalizationAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Globalization");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Globalization";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemGlobalizationAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemGlobalizationAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Globalization.dll");
        else
          SystemGlobalizationAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemGlobalizationAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetGlobalizationTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (GlobalizationAssembly == null)
        Debug.Assert(false);
      else
        result = GlobalizationAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(GlobalizationAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetRuntimeExtensionsAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Runtime.Extensions");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Runtime.Extensions";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeExtensionsAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeExtensionsAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Runtime.Extensions.dll");
        else
          SystemRuntimeExtensionsAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeExtensionsAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetRuntimeExtensionsTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemRuntimeExtensionsAssembly == null)
        Debug.Assert(false);
      else
        result = SystemRuntimeExtensionsAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemRuntimeExtensionsAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetRuntimeSerializationAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Serialization.DataContract");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Serialization.DataContract";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeSerializationAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeSerializationAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Serialization.DataContract.dll");
        else
          SystemRuntimeSerializationAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeSerializationAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetRuntimeSerializationTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemRuntimeSerializationAssembly == null)
        Debug.Assert(false);
      else
        result = SystemRuntimeSerializationAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemRuntimeSerializationAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetThreadingAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier ThreadingId = Identifier.For("System.Threading");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[ThreadingId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Threading";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[ThreadingId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemThreadingAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemThreadingAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Threading.dll");
        else
          SystemThreadingAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemThreadingAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetThreadingTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (ThreadingAssembly == null)
        Debug.Assert(false);
      else
        result = ThreadingAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(ThreadingAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetWindowsRuntimeInteropAssembly(bool doNotLockFile, bool getDebugInfo) {
      if (SystemAssembly.Name == "mscorlib") return SystemAssembly;
      Identifier AssemblyId = Identifier.For("System.Runtime.InteropServices.WindowsRuntime");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Runtime.InteropServices.WindowsRuntime";
        aref.PublicKeyOrToken = new byte[] { 0xB0, 0x3F, 0x5F, 0x7F, 0x11, 0xD5, 0x0A, 0x3A };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Runtime.InteropServices.WindowsRuntime.dll");
        else
          SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeWindowsRuntimeInteropServicesAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetWindowsRuntimeInteropTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemRuntimeWindowsRuntimeInteropAssembly == null)
        Debug.Assert(false);
      else
        result = SystemRuntimeWindowsRuntimeInteropAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemRuntimeWindowsRuntimeInteropAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetSystemRuntimeWindowsRuntimeAssembly(bool doNotLockFile, bool getDebugInfo) {
      Identifier AssemblyId = Identifier.For("System.Runtime.WindowsRuntime");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Runtime.WindowsRuntime";
        aref.PublicKeyOrToken = new byte[] {0xB7, 0x7A, 0x5C, 0x56, 0x19, 0x34, 0xE0, 0x89 };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeWindowsRuntimeAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeWindowsRuntimeAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Runtime.WindowsRuntime.dll");
        else
          SystemRuntimeWindowsRuntimeAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeWindowsRuntimeAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }    
    private static TypeNode/*!*/ GetGenericWindowsRuntimeTypeNodeFor(string/*!*/ nspace, string/*!*/ name, int numParams, ElementType typeCode) {
      if (TargetPlatform.GenericTypeNamesMangleChar != 0) name = name + TargetPlatform.GenericTypeNamesMangleChar + numParams;
      return SystemTypes.GetWindowsRuntimeTypeNodeFor(nspace, name, typeCode);
    }
    private static TypeNode/*!*/ GetWindowsRuntimeTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemRuntimeWindowsRuntimeAssembly == null)
        Debug.Assert(false);
      else
        result = SystemRuntimeWindowsRuntimeAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemRuntimeWindowsRuntimeAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
    private static AssemblyNode/*!*/ GetSystemRuntimeWindowsRuntimeUIXamlAssembly(bool doNotLockFile, bool getDebugInfo) {
      Identifier AssemblyId = Identifier.For("System.Runtime.WindowsRuntime.UI.Xaml");
      AssemblyReference aref = (AssemblyReference)TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey];
      if (aref == null) {
        aref = new AssemblyReference();
        aref.Name = "System.Runtime.WindowsRuntime.UI.Xaml";
        aref.PublicKeyOrToken = new byte[] { 0xB7, 0x7A, 0x5C, 0x56, 0x19, 0x34, 0xE0, 0x89 };
        aref.Version = TargetPlatform.TargetVersion;
        TargetPlatform.AssemblyReferenceFor[AssemblyId.UniqueIdKey] = aref;
      }
      if (string.IsNullOrEmpty(SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location)) {
        if (aref.Location == null)
          SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location = Path.Combine(Path.GetDirectoryName(SystemAssemblyLocation.Location), "System.Runtime.WindowsRuntime.UI.Xaml.dll");
        else
          SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location = aref.Location;
      }
      if (aref.assembly == null) aref.Location = SystemRuntimeWindowsRuntimeUIXamlAssemblyLocation.Location;
      return aref.assembly = AssemblyNode.GetAssembly(aref, doNotLockFile, getDebugInfo, true);
    }
    private static TypeNode/*!*/ GetGenericWindowsRuntimeUIXamlTypeNodeFor(string/*!*/ nspace, string/*!*/ name, int numParams, ElementType typeCode) {
      if (TargetPlatform.GenericTypeNamesMangleChar != 0) name = name + TargetPlatform.GenericTypeNamesMangleChar + numParams;
      return SystemTypes.GetWindowsRuntimeUIXamlTypeNodeFor(nspace, name, typeCode);
    }
    private static TypeNode/*!*/ GetWindowsRuntimeUIXamlTypeNodeFor(string/*!*/ nspace, string/*!*/ name, ElementType typeCode) {
      TypeNode result = null;
      if (SystemRuntimeWindowsRuntimeUIXamlAssembly == null)
        Debug.Assert(false);
      else
        result = SystemRuntimeWindowsRuntimeUIXamlAssembly.GetType(Identifier.For(nspace), Identifier.For(name));
      if (result == null) result = CoreSystemTypes.GetDummyTypeNode(SystemRuntimeWindowsRuntimeUIXamlAssembly, nspace, name, typeCode);
      result.typeCode = typeCode;
      return result;
    }
  }
}
