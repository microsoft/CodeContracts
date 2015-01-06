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

// F: It seems most of the methods below would require a != null postcondition.
// However, if this is true or not, seems depending on invariants of the Windows API, with which I am not really familiar

using System.Diagnostics.Contracts;


namespace System.Windows.Forms
{
  // Summary:
  //     Provides static methods and properties to manage an application, such as
  //     methods to start and stop an application, to process Windows messages, and
  //     properties to get information about an application. This class cannot be
  //     inherited.
  public sealed class Application
  {
    private Application() { }
    // Summary:
    //     Gets a value indicating whether the caller can quit this application.
    //
    // Returns:
    //     true if the caller can quit this application; otherwise, false.
    //public static bool AllowQuit { get; }
    //
    // Summary:
    //     Gets the path for the application data that is shared among all users.
    //
    // Returns:
    //     The path for the application data that is shared among all users.
    //public static string CommonAppDataPath { get; }
    //
    // Summary:
    //     Gets the registry key for the application data that is shared among all users.
    //
    // Returns:
    //     A Microsoft.Win32.RegistryKey representing the registry key of the application
    //     data that is shared among all users.
    //public static RegistryKey CommonAppDataRegistry { get; }
    //
    // Summary:
    //     Gets the company name associated with the application.
    //
    // Returns:
    //     The company name.
    //public static string CompanyName { get; }
    //
    // Summary:
    //     Gets or sets the culture information for the current thread.
    //
    // Returns:
    //     A System.Globalization.CultureInfo representing the culture information for
    //     the current thread.
    //public static CultureInfo CurrentCulture { get; set; }
    //
    // Summary:
    //     Gets or sets the current input language for the current thread.
    //
    // Returns:
    //     An System.Windows.Forms.InputLanguage representing the current input language
    //     for the current thread.
    //public static InputLanguage CurrentInputLanguage { get; set; }
    ////
    // Summary:
    //     Gets the path for the executable file that started the application, including
    //     the executable name.
    //
    // Returns:
    //     The path and executable name for the executable file that started the application.This
    //     path will be different depending on whether the Windows Forms application
    //     is deployed using ClickOnce. ClickOnce applications are stored in a per-user
    //     application cache in the C:\Documents and Settings\username directory. For
    //     more information, see Accessing Local and Remote Data in ClickOnce Applications.
    public static string ExecutablePath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null); return default(string);
      }
    }
    
    //
    // Summary:
    //     Gets the path for the application data of a local, non-roaming user.
    //
    // Returns:
    //     The path for the application data of a local, non-roaming user.
    //public static string LocalUserAppDataPath { get; }
    //
    // Summary:
    //     Gets a value indicating whether a message loop exists on this thread.
    //
    // Returns:
    //     true if a message loop exists; otherwise, false.
    //public static bool MessageLoop { get; }
    //
    // Summary:
    //     Gets a collection of open forms owned by the application.
    //
    // Returns:
    //     A System.Windows.Forms.FormCollection containing all the currently open forms
    //     owned by this application.
    public static FormCollection OpenForms
    {
      get
      {
        Contract.Ensures(Contract.Result<FormCollection>() != null);

        return default(FormCollection);
      }
    }
    //
    // Summary:
    //     Gets the product name associated with this application.
    //
    // Returns:
    //     The product name.
    //public static string ProductName { get; }
    //
    // Summary:
    //     Gets the product version associated with this application.
    //
    // Returns:
    //     The product version.
    //public static string ProductVersion { get; }
    //
    // Summary:
    //     Gets a value specifying whether the current application is drawing controls
    //     with visual styles.
    //
    // Returns:
    //     true if visual styles are enabled for controls in the client area of application
    //     windows; otherwise, false.
    //public static bool RenderWithVisualStyles { get; }
    //
    // Summary:
    //     Gets or sets the format string to apply to top-level window captions when
    //     they are displayed with a warning banner.
    //
    // Returns:
    //     The format string to apply to top-level window captions.
    //public static string SafeTopLevelCaptionFormat { get; set; }
    //
    // Summary:
    //     Gets the path for the executable file that started the application, not including
    //     the executable name.
    //
    // Returns:
    //     The path for the executable file that started the application.This path will
    //     be different depending on whether the Windows Forms application is deployed
    //     using ClickOnce. ClickOnce applications are stored in a per-user application
    //     cache in the C:\Documents and Settings\username directory. For more information,
    //     see Accessing Local and Remote Data in ClickOnce Applications.
    //public static string StartupPath { get; }
    //
    // Summary:
    //     Gets the path for the application data of a user.
    //
    // Returns:
    //     The path for the application data of a user.
    //public static string UserAppDataPath { get; }
    //
    // Summary:
    //     Gets the registry key for the application data of a user.
    //
    // Returns:
    //     A Microsoft.Win32.RegistryKey representing the registry key for the application
    //     data specific to the user.
    //public static RegistryKey UserAppDataRegistry { get; }
    //
    // Summary:
    //     Gets or sets whether the wait cursor is used for all open forms of the application.
    //
    // Returns:
    //     true is the wait cursor is used for all open forms; otherwise, false.
    //public static bool UseWaitCursor { get; set; }
    //
    // Summary:
    //     Gets a value that specifies how visual styles are applied to application
    //     windows.
    //
    // Returns:
    //     A bitwise combination of the System.Windows.Forms.VisualStyles.VisualStyleState
    //     values.
    //public static VisualStyleState VisualStyleState { get; set; }

    // Summary:
    //     Occurs when the application is about to shut down.
    // public static event EventHandler ApplicationExit;
    //
    // Summary:
    //     Occurs when the application is about to enter a modal state.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public static event EventHandler EnterThreadModal;
    //
    // Summary:
    //     Occurs when the application finishes processing and is about to enter the
    //     idle state.
    // public static event EventHandler Idle;
    //
    // Summary:
    //     Occurs when the application is about to leave a modal state.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // public static event EventHandler LeaveThreadModal;
    //
    // Summary:
    //     Occurs when an untrapped thread exception is thrown.
    // public static event ThreadExceptionEventHandler ThreadException;
    //
    // Summary:
    //     Occurs when a thread is about to shut down. When the main thread for an application
    //     is about to be shut down, this event is raised first, followed by an System.Windows.Forms.Application.ApplicationExit
    //     event.
    // public static event EventHandler ThreadExit;

    // Summary:
    //     Adds a message filter to monitor Windows messages as they are routed to their
    //     destinations.
    //
    // Parameters:
    //   value:
    //     The implementation of the System.Windows.Forms.IMessageFilter interface you
    //     want to install.
    //public static void AddMessageFilter(IMessageFilter value);
    //
    // Summary:
    //     Processes all Windows messages currently in the message queue.
    //public static void DoEvents();
    //
    // Summary:
    //     Enables visual styles for the application.
    //public static void EnableVisualStyles();
    //
    // Summary:
    //     Informs all message pumps that they must terminate, and then closes all application
    //     windows after the messages have been processed.
    //public static void Exit();
    //
    // Summary:
    //     Informs all message pumps that they must terminate, and then closes all application
    //     windows after the messages have been processed.
    //
    // Parameters:
    //   e:
    //     Returns whether any System.Windows.Forms.Form within the application cancelled
    //     the exit.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static void Exit(CancelEventArgs e);
    //
    // Summary:
    //     Exits the message loop on the current thread and closes all windows on the
    //     thread.
    //public static void ExitThread();
    //
    // Summary:
    //     Runs any filters against a window message, and returns a copy of the modified
    //     message.
    //
    // Parameters:
    //   message:
    //     The Windows event message to filter.
    //
    // Returns:
    //     True if the filters were processed; otherwise, false.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static bool FilterMessage(ref Message message);
    //
    // Summary:
    //     Initializes OLE on the current thread.
    //
    // Returns:
    //     One of the System.Threading.ApartmentState values.
    //public static ApartmentState OleRequired();
    //
    // Summary:
    //     Raises the System.Windows.Forms.Application.ThreadException event.
    //
    // Parameters:
    //   t:
    //     An System.Exception that represents the exception that was thrown.
    //public static void OnThreadException(Exception t);
    //
    // Summary:
    //     Raises the System.Windows.Forms.Application.Idle event in hosted scenarios.
    //
    // Parameters:
    //   e:
    //     The System.EventArgs objects to pass to the System.Windows.Forms.Application.Idle
    //     event.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static void RaiseIdle(EventArgs e);
    //
    // Summary:
    //     Registers a callback for checking whether the message loop is running in
    //     hosted environments.
    //
    // Parameters:
    //   callback:
    //     The method to call when Windows Forms needs to check if the hosting environment
    //     is still sending messages.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static void RegisterMessageLoop(Application.MessageLoopCallback callback);
    //
    // Summary:
    //     Removes a message filter from the message pump of the application.
    //
    // Parameters:
    //   value:
    //     The implementation of the System.Windows.Forms.IMessageFilter to remove from
    //     the application.
    //public static void RemoveMessageFilter(IMessageFilter value);
    //
    // Summary:
    //     Shuts down the application and starts a new instance immediately.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     Your code is not a Windows Forms application. You cannot call this method
    //     in this context.
    //public static void Restart();
    //
    // Summary:
    //     Begins running a standard application message loop on the current thread,
    //     without a form.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     A main message loop is already running on this thread.
    //public static void Run();
    //
    // Summary:
    //     Begins running a standard application message loop on the current thread,
    //     with an System.Windows.Forms.ApplicationContext.
    //
    // Parameters:
    //   context:
    //     An System.Windows.Forms.ApplicationContext in which the application is run.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     A main message loop is already running on this thread.
    //public static void Run(ApplicationContext context);
    //
    // Summary:
    //     Begins running a standard application message loop on the current thread,
    //     and makes the specified form visible.
    //
    // Parameters:
    //   mainForm:
    //     A System.Windows.Forms.Form that represents the form to make visible.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     A main message loop is already running on the current thread.
    //public static void Run(Form mainForm);
    //
    // Summary:
    //     Sets the application-wide default for the System.Windows.Forms.ButtonBase.UseCompatibleTextRendering
    //     property defined on certain controls.
    //
    // Parameters:
    //   defaultValue:
    //     The default value to use for new controls. If true, new controls that support
    //     System.Windows.Forms.ButtonBase.UseCompatibleTextRendering use GDI for text
    //     rendering; if false, new controls use GDI+.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     You can only call this method before the first window is created by your
    //     Windows Forms application.
    //public static void SetCompatibleTextRenderingDefault(bool defaultValue);
    //
    // Summary:
    //     Suspends or hibernates the system, or requests that the system be suspended
    //     or hibernated.
    //
    // Parameters:
    //   state:
    //     A System.Windows.Forms.PowerState indicating the power activity mode to which
    //     to transition.
    //
    //   force:
    //     true to force the suspended mode immediately; false to cause Windows to send
    //     a suspend request to every application.
    //
    //   disableWakeEvent:
    //     true to disable restoring the system's power status to active on a wake event,
    //     false to enable restoring the system's power status to active on a wake event.
    //
    // Returns:
    //     true if the system is being suspended, otherwise, false.
    //public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent);
    //
    // Summary:
    //     Instructs the application how to respond to unhandled exceptions.
    //
    // Parameters:
    //   mode:
    //     An System.Windows.Forms.UnhandledExceptionMode value describing how the application
    //     should behave if an exception is thrown without being caught.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     You cannot set the exception mode after the application has created its first
    //     window.
    //public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode);
    //
    // Summary:
    //     Instructs the application how to respond to unhandled exceptions, optionally
    //     applying thread-specific behavior.
    //
    // Parameters:
    //   mode:
    //     An System.Windows.Forms.UnhandledExceptionMode value describing how the application
    //     should behave if an exception is thrown without being caught.
    //
    //   threadScope:
    //     true to set the thread exception mode; otherwise, false.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     You cannot set the exception mode after the application has created its first
    //     window.
    //public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope);
    //
    // Summary:
    //     Unregisters the message loop callback made with System.Windows.Forms.Application.RegisterMessageLoop(System.Windows.Forms.Application.MessageLoopCallback).
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static void UnregisterMessageLoop();

    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public delegate bool MessageLoopCallback();
  }
}




