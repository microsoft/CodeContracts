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

#if !SILVERLIGHT

using System;
using System.Security;
using System.Text;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

namespace System.Diagnostics
{
  // Summary:
  //     Specifies a set of values used when starting a process.
  public sealed class ProcessStartInfo
  {
#if false
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.ProcessStartInfo class
    //     and specifies a file name such as an application or document with which to
    //     start the process.
    //
    // Parameters:
    //   fileName:
    //     An application or document with which to start a process.

    public ProcessStartInfo(string fileName) {
    }
    //
    // Summary:
    //     Initializes a new instance of the System.Diagnostics.ProcessStartInfo class
    //     and specifies an application file name with which to start the process, as
    //     well as a set of command line arguments to pass to the application.
    //
    // Parameters:
    //   fileName:
    //     An application with which to start a process.
    //
    //   arguments:
    //     Command line arguments to pass to the application when the process starts.
    public ProcessStartInfo(string fileName, string arguments) {

      return default(ProcessStartInfo(string);
    }
#endif

    // Summary:
    //     Gets or sets the set of command line arguments to use when starting the application.
    //
    // Returns:
    //     File type-specific arguments that the system can associate with the application
    //     specified in the System.Diagnostics.ProcessStartInfo.FileName property. The
    //     default is an empty string (""). The maximum string length is 2003 characters
    //     in .NET Framework applications and 488 characters in .NET Compact Framework
    //     applications.
    public string Arguments
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }
#if false
    //
    // Summary:
    //     Gets or sets a value indicating whether to start the process in a new window.
    //
    // Returns:
    //     true to start the process without creating a new window to contain it; otherwise,
    //     false. The default is false.
    public bool CreateNoWindow { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets a value identifying the domain to use when starting the process.
    //
    // Returns:
    //     The Active Directory domain to use when starting the process. The domain
    //     property is primarily of interest to users within enterprise environments
    //     that utilize Active Directory.
#if false
    public string Domain { get; set; }
#endif
    //
    // Summary:
    //     Gets search paths for files, directories for temporary files, application-specific
    //     options, and other similar information.
    //
    // Returns:
    //     A System.Collections.Specialized.StringDictionary that provides environment
    //     variables that apply to this process and child processes. The default is
    //     null.
    public StringDictionary EnvironmentVariables
    {
      get
      {
        Contract.Ensures(Contract.Result<StringDictionary>() != null);
        return default(StringDictionary);

      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether an error dialog is displayed to the
    //     user if the process cannot be started.
    //
    // Returns:
    //     true to display an error dialog on the screen if the process cannot be started;
    //     otherwise, false.
#if false
    public bool ErrorDialog { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the window handle to use when an error dialog is shown for a
    //     process that cannot be started.
    //
    // Returns:
    //     An System.IntPtr that identifies the handle of the error dialog that results
    //     from a process start failure.
#if false
    public IntPtr ErrorDialogParentHandle { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the application or document to start.
    //
    // Returns:
    //     The name of the application to start, or the name of a document of a file
    //     type that is associated with an application and that has a default open action
    //     available to it. The default is an empty string ("").
    public string FileName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether the Windows user profile is to be
    //     loaded from the registry.
    //
    // Returns:
    //     true to load the Windows user profile; otherwise, false.
#if fales
    public bool LoadUserProfile { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets a secure string containing the user password to use when starting
    //     the process.
    //
    // Returns:
    //     A System.Security.SecureString containing the user password to use when starting
    //     the process.
#if false
    public SecureString Password { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets a value indicating whether the error output of an application
    //     is written to the System.Diagnostics.Process.StandardError stream.
    //
    // Returns:
    //     true to write error output to System.Diagnostics.Process.StandardError; otherwise,
    //     false.
#if false
    public bool RedirectStandardError { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets a value indicating whether the input for an application is read
    //     from the System.Diagnostics.Process.StandardInput stream.
    //
    // Returns:
    //     true to read input from System.Diagnostics.Process.StandardInput; otherwise,
    //     false.
#if false
    public bool RedirectStandardInput { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets a value indicating whether the output of an application is written
    //     to the System.Diagnostics.Process.StandardOutput stream.
    //
    // Returns:
    //     true to write output to System.Diagnostics.Process.StandardOutput; otherwise,
    //     false.
#if false
    public bool RedirectStandardOutput { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the preferred encoding for error output.
    //
    // Returns:
    //     An System.Text.Encoding object representing the preferred encoding for error
    //     output. The default is null.
    public Encoding StandardErrorEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }
    //
    // Summary:
    //     Gets or sets the preferred encoding for standard output.
    //
    // Returns:
    //     An System.Text.Encoding object representing the preferred encoding for standard
    //     output. The default is null.
    public Encoding StandardOutputEncoding
    {
      get
      {
        Contract.Ensures(Contract.Result<Encoding>() != null);
        return default(Encoding);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }
    //
    // Summary:
    //     Gets or sets the user name to be used when starting the process.
    //
    // Returns:
    //     The user name to use when starting the process.
    public string UserName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        Contract.Requires(value != null);

      }
    }
    //
    // Summary:
    //     Gets or sets a value indicating whether to use the operating system shell
    //     to start the process.
    //
    // Returns:
    //     true to use the shell when starting the process; otherwise, the process is
    //     created directly from the executable file. The default is true.
#if false
    public bool UseShellExecute { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the verb to use when opening the application or document specified
    //     by the System.Diagnostics.ProcessStartInfo.FileName property.
    //
    // Returns:
    //     The action to take with the file that the process opens. The default is an
    //     empty string ("").
    extern public string Verb { get; set; }
    //
    // Summary:
    //     Gets the set of verbs associated with the type of file specified by the System.Diagnostics.ProcessStartInfo.FileName
    //     property.
    //
    // Returns:
    //     The actions that the system can apply to the file indicated by the System.Diagnostics.ProcessStartInfo.FileName
    //     property.
    public string[] Verbs
    {
      get
      {
        Contract.Ensures(Contract.Result<string[]>() != null);
        return default(string[]);
      }
    }
#if false
    //
    // Summary:
    //     Gets or sets the window state to use when the process is started.
    //
    // Returns:
    //     A System.Diagnostics.ProcessWindowStyle that indicates whether the process
    //     is started in a window that is maximized, minimized, normal (neither maximized
    //     nor minimized), or not visible. The default is normal.
    //
    // Exceptions:
    //   System.ComponentModel.InvalidEnumArgumentException:
    //     The window style is not one of the System.Diagnostics.ProcessWindowStyle
    //     enumeration members.
    public ProcessWindowStyle WindowStyle { get; set; }
#endif
    //
    // Summary:
    //     Gets or sets the initial directory for the process to be started.
    //
    // Returns:
    //     The fully qualified name of the directory that contains the process to be
    //     started. The default is an empty string ("").
    public string WorkingDirectory
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
      set
      {
        // Contract.Requires(value != null);
      }
    }
  }
}

#endif