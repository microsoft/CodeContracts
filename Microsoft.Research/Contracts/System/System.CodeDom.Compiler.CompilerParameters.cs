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

using System.Diagnostics.Contracts;
using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.CodeDom.Compiler {
  // Summary:
  //     Represents the parameters used to invoke a compiler.

  public class CompilerParameters {
    // Summary:
    //     Initializes a new instance of the System.CodeDom.Compiler.CompilerParameters
    //     class.
    public CompilerParameters();
    //
    // Summary:
    //     Initializes a new instance of the System.CodeDom.Compiler.CompilerParameters
    //     class using the specified assembly names.
    //
    // Parameters:
    //   assemblyNames:
    //     The names of the assemblies to reference.
    public CompilerParameters(string[] assemblyNames) 
    {
        //return default(CompilerParameters(string[]); [t-cyrils] should this method return anything?
    }
    //
    // Summary:
    //     Initializes a new instance of the System.CodeDom.Compiler.CompilerParameters
    //     class using the specified assembly names and output file name.
    //
    // Parameters:
    //   assemblyNames:
    //     The names of the assemblies to reference.
    //
    //   outputName:
    //     The output file name.
    public CompilerParameters(string[] assemblyNames, string outputName) { }
    //
    // Summary:
    //     Initializes a new instance of the System.CodeDom.Compiler.CompilerParameters
    //     class using the specified assembly names, output name, and a value indicating
    //     whether to include debug information.
    //
    // Parameters:
    //   assemblyNames:
    //     The names of the assemblies to reference.
    //
    //   outputName:
    //     The output file name.
    //
    //   includeDebugInformation:
    //     true if debug information should be included; false if debug information
    //     should be excluded.
    public CompilerParameters(string[] assemblyNames, string outputName, bool includeDebugInformation) {

    // Summary:
    //     Gets or sets the optional additional-command line arguments string to use
    //     when invoking the compiler.
    //
    // Returns:
    //     Any additional command line arguments for the compiler.
      return default(CompilerParameters(string[]);
    }
    public string CompilerOptions { get; set; }
    //
    // Summary:
    //     Gets the .NET Framework resource files to include when compiling the assembly
    //     output.
    //
    // Returns:
    //     A System.Collections.Specialized.StringCollection containing the file paths
    //     of .NET Framework resources to include in the generated assembly.
    [ComVisible(false)]
    public StringCollection EmbeddedResources { get { CodContract.Ensures(Contract.Result<StringCollection>() != null); } }
    //
    // Summary:
    //     Specifies an evidence object that represents the security policy permissions
    //     to grant the compiled assembly.
    //
    // Returns:
    //     An System.Security.Policy.Evidence object that represents the security policy
    //     permissions to grant the compiled assembly.
    public Evidence Evidence { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to generate an executable.
    //
    // Returns:
    //     true if an executable should be generated; otherwise, false.
    public bool GenerateExecutable { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to generate the output in memory.
    //
    // Returns:
    //     true if the compiler should generate the output in memory; otherwise, false.
    public bool GenerateInMemory { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to include debug information in the
    //     compiled executable.
    //
    // Returns:
    //     true if debug information should be generated; otherwise, false.
    public bool IncludeDebugInformation { get; set; }
    //
    // Summary:
    //     Gets the .NET Framework resource files that are referenced in the current
    //     source.
    //
    // Returns:
    //     A System.Collections.Specialized.StringCollection containing the file paths
    //     of .NET Framework resources that are referenced by the source.
    [ComVisible(false)]
    public StringCollection LinkedResources { get { CodContract.Ensures(Contract.Result<StringCollection>() != null); } }
    //
    // Summary:
    //     Gets or sets the name of the main class.
    //
    // Returns:
    //     The name of the main class.
    public string MainClass { get; set; }
    //
    // Summary:
    //     Gets or sets the name of the output assembly.
    //
    // Returns:
    //     The name of the output assembly.
    public string OutputAssembly { get; set; }
    //
    // Summary:
    //     Gets the assemblies referenced by the current project.
    //
    // Returns:
    //     A System.Collections.Specialized.StringCollection that contains the assembly
    //     names that are referenced by the source to compile.
    public StringCollection ReferencedAssemblies { get { CodContract.Ensures(Contract.Result<StringCollection>() != null); } }
    //
    // Summary:
    //     Gets or sets the collection that contains the temporary files.
    //
    // Returns:
    //     A System.CodeDom.Compiler.TempFileCollection that contains the temporary
    //     files.
    public TempFileCollection TempFiles { get; set; }
    //
    // Summary:
    //     Gets or sets a value indicating whether to treat warnings as errors.
    //
    // Returns:
    //     true if warnings should be treated as errors; otherwise, false.
    public bool TreatWarningsAsErrors { get; set; }
    //
    // Summary:
    //     Gets or sets the user token to use when creating the compiler process.
    //
    // Returns:
    //     The user token to use.
    public IntPtr UserToken { get; set; }
    //
    // Summary:
    //     Gets or sets the warning level at which the compiler aborts compilation.
    //
    // Returns:
    //     The warning level at which the compiler aborts compilation.
    public int WarningLevel { get; set; }
    //
    // Summary:
    //     Gets or sets the file name of a Win32 resource file to link into the compiled
    //     assembly.
    //
    // Returns:
    //     A Win32 resource file that will be linked into the compiled assembly.
    public string Win32Resource { get; set; }
  }
}
