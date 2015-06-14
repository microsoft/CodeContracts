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

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics.Contracts;

namespace Microsoft.VisualBasic
{
  // Summary:
  //     The Interaction module contains procedures used to interact with objects,
  //     applications, and systems.
  //[StandardModule]
  public static class Interaction
  {
#if !SILVERLIGHT
    // Summary:
    //     Activates an application that is already running.
    //
    // Parameters:
    //   ProcessId:
    //     Integer specifying the Win32 process ID number assigned to this process.
    //     You can use the ID returned by the Shell Function, provided it is not zero.
    [Pure]
    extern public static void AppActivate(int ProcessId);
    
    //
    // Summary:
    //     Activates an application that is already running.
    //
    // Parameters:
    //   Title:
    //     String expression specifying the title in the title bar of the application
    //     you want to activate. You can use the title assigned to the application when
    //     it was launched.
   [Pure]
    extern public static void AppActivate(string Title);
    
    //
    // Summary:
    //     Sounds a tone through the computer's speaker.
    [Pure]
    extern public static void Beep();
#endif
    //
    // Summary:
    //     Executes a method on an object, or sets or returns a property on an object.
    //
    // Parameters:
    //   ObjectRef:
    //     Required. Object. A pointer to the object exposing the property or method.
    //
    //   ProcName:
    //     Required. String. A string expression containing the name of the property
    //     or method on the object.
    //
    //   UseCallType:
    //     Required. An enumeration member of type CallType Enumeration representing
    //     the type of procedure being called. The value of CallType can be Method,
    //     Get, or Set.
    //
    //   Args:
    //     Optional. ParamArray. A parameter array containing the arguments to be passed
    //     to the property or method being called.
    //
    // Returns:
    //     Executes a method on an object, or sets or returns a property on an object.
    // public static object CallByName(object ObjectRef, string ProcName, CallType UseCallType, params object[] Args);
    //
    // Summary:
    //     Selects and returns a value from a list of arguments.
    //
    // Parameters:
    //   Index:
    //     Required. Double. Numeric expression that results in a value between 1 and
    //     the number of elements passed in the Choice argument.
    //
    //   Choice:
    //     Required. Object parameter array. You can supply either a single variable
    //     or an expression that evaluates to the Object data type, to a list of Object
    //     variables or expressions separated by commas, or to a single-dimensional
    //     array of Object elements.
    //
    // Returns:
    //     Selects and returns a value from a list of arguments.
    [Pure]
    public static object Choose(double Index, params object[] Choice)
    {
      Contract.Requires(Choice.Rank == 1);
      Contract.Requires(Index >= 0);
      Contract.Requires(Index < Choice.Length);

      return default(object);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the argument portion of the command line used to start Visual Basic
    //     or an executable program developed with Visual Basic. The My feature provides
    //     greater productivity and performance than the Command function. For more
    //     information, see My.Application.CommandLineArgs Property.
    //
    // Returns:
    //     Returns the argument portion of the command line used to start Visual Basic
    //     or an executable program developed with Visual Basic.The My feature provides
    //     greater productivity and performance than the Command function. For more
    //     information, see My.Application.CommandLineArgs Property.
    [Pure]
    public static string Command()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#endif

    //
    // Summary:
    //     Creates and returns a reference to a COM object. CreateObject cannot be used
    //     to create instances of classes in Visual Basic unless those classes are explicitly
    //     exposed as COM components.
    //
    // Parameters:
    //   ProgId:
    //     Required. String. The program ID of the object to create.
    //
    //   ServerName:
    //     Optional. String. The name of the network server where the object will be
    //     created. If ServerName is an empty string (""), the local computer is used.
    //
    // Returns:
    //     Creates and returns a reference to a COM object. CreateObject cannot be used
    //     to create instances of classes in Visual Basic unless those classes are explicitly
    //     exposed as COM components.
    //public static object CreateObject(string ProgId, string ServerName);
    //
    // Summary:
    //     Deletes a section or key setting from an application's entry in the Windows
    //     registry. The My feature gives you greater productivity and performance in
    //     registry operations than the DeleteSetting function. For more information,
    //     see My.Computer.Registry Object.
    //
    // Parameters:
    //   AppName:
    //     Required. String expression containing the name of the application or project
    //     to which the section or key setting applies.
    //
    //   Section:
    //     Required. String expression containing the name of the section from which
    //     the key setting is being deleted. If only AppName and Section are provided,
    //     the specified section is deleted along with all related key settings.
    //
    //   Key:
    //     Optional. String expression containing the name of the key setting being
    //     deleted.
    //public static void DeleteSetting(string AppName, string Section, string Key);

#if !SILVERLIGHT    
    // Summary:
    //     Returns the string associated with an operating-system environment variable.
    //
    // Parameters:
    //   Expression:
    //     Required. Expression that evaluates either a string containing the name of
    //     an environment variable, or an integer corresponding to the numeric order
    //     of an environment string in the environment-string table.
    //
    // Returns:
    //     Returns the string associated with an operating-system environment variable.
    [Pure]
    public static string Environ(int Expression)
    {
      Contract.Requires(Expression > 0);
      Contract.Requires(Expression <= 0xff);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
#endif

    //
    // Summary:
    //     Returns the string associated with an operating-system environment variable.
    //
    // Parameters:
    //   Expression:
    //     Required. Expression that evaluates either a string containing the name of
    //     an environment variable, or an integer corresponding to the numeric order
    //     of an environment string in the environment-string table.
    //
    // Returns:
    //     Returns the string associated with an operating-system environment variable.
    //public static string Environ(string Expression);
    //
    // Summary:
    //     Returns a list of key settings and their respective values (originally created
    //     with SaveSetting) from an application's entry in the Windows registry. Using
    //     the My feature gives you greater productivity and performance in registry
    //     operations than GetAllSettings. For more information, see My.Computer.Registry
    //     Object.
    //
    // Parameters:
    //   AppName:
    //     Required. String expression containing the name of the application or project
    //     whose key settings are requested.
    //
    //   Section:
    //     Required. String expression containing the name of the section whose key
    //     settings are requested. GetAllSettings returns an object that contains a
    //     two-dimensional array of strings. The strings contain all the key settings
    //     in the specified section, plus their corresponding values.
    //
    // Returns:
    //     Returns a list of key settings and their respective values (originally created
    //     with SaveSetting) from an application's entry in the Windows registry.Using
    //     the My feature gives you greater productivity and performance in registry
    //     operations than GetAllSettings. For more information, see My.Computer.Registry
    //     Object.
    //public static string[,] GetAllSettings(string AppName, string Section);
    //
    // Summary:
    //     Returns a reference to an object provided by a COM component.
    //
    // Parameters:
    //   PathName:
    //     Optional. String. The full path and name of the file containing the object
    //     to retrieve. If PathName is omitted, Class is required.
    //
    //   Class:
    //     Required if PathName is not supplied. String. A string representing the class
    //     of the object. The Class argument has the following syntax and parts:appname.objecttypeParameterDescriptionappnameRequired.
    //     String. The name of the application providing the object.objecttypeRequired.
    //     String. The type or class of object to create.
    //
    // Returns:
    //     Returns a reference to an object provided by a COM component.
    //public static object GetObject(string PathName, string Class);
    //
    // Summary:
    //     Returns a key setting value from an application's entry in the Windows registry.
    //     The My feature gives you greater productivity and performance in registry
    //     operations than GetAllSettings. For more information, see My.Computer.Registry
    //     Object.
    //
    // Parameters:
    //   AppName:
    //     Required. String expression containing the name of the application or project
    //     whose key setting is requested.
    //
    //   Section:
    //     Required. String expression containing the name of the section in which the
    //     key setting is found.
    //
    //   Key:
    //     Required. String expression containing the name of the key setting to return.
    //
    //   Default:
    //     Optional. Expression containing the value to return if no value is set in
    //     the Key setting. If omitted, Default is assumed to be a zero-length string
    //     ("").
    //
    // Returns:
    //     Returns a key setting value from an application's entry in the Windows registry.The
    //     My feature gives you greater productivity and performance in registry operations
    //     than GetAllSettings. For more information, see My.Computer.Registry Object.
    //public static string GetSetting(string AppName, string Section, string Key, string Default);
    //
    // Summary:
    //     Returns one of two objects, depending on the evaluation of an expression.
    //
    // Parameters:
    //   Expression:
    //     Required. Boolean. The expression you want to evaluate.
    //
    //   TruePart:
    //     Required. Object. Returned if Expression evaluates to True.
    //
    //   FalsePart:
    //     Required. Object. Returned if Expression evaluates to False.
    //
    // Returns:
    //     Returns one of two objects, depending on the evaluation of an expression.
    [Pure]
    public static object IIf(bool Expression, object TruePart, object FalsePart)
    {
      Contract.Ensures(Contract.Result<object>() == (Expression? TruePart : FalsePart));

      return default(object);
    }
    //
    // Summary:
    //     Displays a prompt in a dialog box, waits for the user to input text or click
    //     a button, and then returns a string containing the contents of the text box.
    //
    // Parameters:
    //   Prompt:
    //     Required String expression displayed as the message in the dialog box. The
    //     maximum length of Prompt is approximately 1024 characters, depending on the
    //     width of the characters used. If Prompt consists of more than one line, you
    //     can separate the lines using a carriage return character (Chr(13)), a line
    //     feed character (Chr(10)), or a carriage return/line feed combination (Chr(13)
    //     & Chr(10)) between each line.
    //
    //   Title:
    //     Optional. String expression displayed in the title bar of the dialog box.
    //     If you omit Title, the application name is placed in the title bar.
    //
    //   DefaultResponse:
    //     Optional. String expression displayed in the text box as the default response
    //     if no other input is provided. If you omit DefaultResponse, the displayed
    //     text box is empty.
    //
    //   XPos:
    //     Optional. Numeric expression that specifies, in twips, the distance of the
    //     left edge of the dialog box from the left edge of the screen. If you omit
    //     XPos, the dialog box is centered horizontally.
    //
    //   YPos:
    //     Optional. Numeric expression that specifies, in twips, the distance of the
    //     upper edge of the dialog box from the top of the screen. If you omit YPos,
    //     the dialog box is positioned vertically approximately one-third of the way
    //     down the screen.
    //
    // Returns:
    //     Displays a prompt in a dialog box, waits for the user to input text or click
    //     a button, and then returns a string containing the contents of the text box.
    //public static string InputBox(string Prompt, string Title, string DefaultResponse, int XPos, int YPos);
    //
    // Summary:
    //     Displays a message in a dialog box, waits for the user to click a button,
    //     and then returns an integer indicating which button the user clicked.
    //
    // Parameters:
    //   Prompt:
    //     Required. String expression displayed as the message in the dialog box. The
    //     maximum length of Prompt is approximately 1024 characters, depending on the
    //     width of the characters used. If Prompt consists of more than one line, you
    //     can separate the lines using a carriage return character (Chr(13)), a line
    //     feed character (Chr(10)), or a carriage return/linefeed character combination
    //     (Chr(13) & Chr(10)) between each line.
    //
    //   Buttons:
    //     Optional. Numeric expression that is the sum of values specifying the number
    //     and type of buttons to display, the icon style to use, the identity of the
    //     default button, and the modality of the message box. If you omit Buttons,
    //     the default value is zero.
    //
    //   Title:
    //     Optional. String expression displayed in the title bar of the dialog box.
    //     If you omit Title, the application name is placed in the title bar.
    //
    // Returns:
    //     Constant OK, Value 1. Constant Cancel, Value 2. Constant Abort, Value 3.
    //     Constant Retry, Value 4. Constant Ignore, Value 5. Constant Yes, Value 6.
    //     Constant No, Value 7.
    //public static MsgBoxResult MsgBox(object Prompt, MsgBoxStyle Buttons, object Title);
    //
    // Summary:
    //     Returns a string representing the calculated range that contains a number.
    //
    // Parameters:
    //   Number:
    //     Required. Long. Whole number that you want to locate within one of the calculated
    //     ranges.
    //
    //   Start:
    //     Required. Long. Whole number that indicates the start of the set of calculated
    //     ranges. Start cannot be less than 0.
    //
    //   Stop:
    //     Required. Long. Whole number that indicates the end of the set of calculated
    //     ranges. Stop cannot be less than or equal to Start.
    //
    //   Interval:
    //     Required. Long. Whole number that indicates the size of each range calculated
    //     between Start and Stop. Interval cannot be less than 1.
    //
    // Returns:
    //     Returns a string representing the calculated range that contains a number.
    [Pure]
    public static string Partition(long Number, long Start, long Stop, long Interval)
    {
      Contract.Requires(Start >= 0);
      Contract.Requires(Interval >= 1);
      Contract.Requires(Stop > Start);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }

#if !SILVERLIGHT
    //
    // Summary:
    //     Saves or creates an application entry in the Windows registry. The My feature
    //     gives you greater productivity and performance in registry operations than
    //     SaveSetting. For more information, see My.Computer.Registry Object.
    //
    // Parameters:
    //   AppName:
    //     Required. String expression containing the name of the application or project
    //     to which the setting applies.
    //
    //   Section:
    //     Required. String expression containing the name of the section in which the
    //     key setting is being saved.
    //
    //   Key:
    //     Required. String expression containing the name of the key setting being
    //     saved.
    //
    //   Setting:
    //     Required. Expression containing the value to which Key is being set.
    [Pure]
    public static void SaveSetting(string AppName, string Section, string Key, string Setting)
    {
      Contract.Requires(Key != null);
    }
#endif

    //
    // Summary:
    //     Runs an executable program and returns an integer containing the program's
    //     process ID if it is still running.
    //
    // Parameters:
    //   PathName:
    //     Required. String. Name of the program to execute, together with any required
    //     arguments and command-line switches. PathName can also include the drive
    //     and the directory path or folder.If you do not know the path to the program,
    //     you can use the My.Computer.FileSystem.GetFiles Method to locate it. For
    //     example, you can call My.Computer.FileSystem.GetFiles("C:\", True, "testFile.txt"),
    //     which returns the full path of every file named testFile.txt anywhere on
    //     drive C:\.
    //
    //   Style:
    //     Optional. AppWinStyle. A value chosen from the AppWinStyle Enumeration specifying
    //     the style of the window in which the program is to run. If Style is omitted,
    //     Shell uses AppWinStyle.MinimizedFocus, which starts the program minimized
    //     and with focus.
    //
    //   Wait:
    //     Optional. Boolean. A value indicating whether the Shell function should wait
    //     for completion of the program. If Wait is omitted, Shell uses False.
    //
    //   Timeout:
    //     Optional. Integer. The number of milliseconds to wait for completion if Wait
    //     is True. If Timeout is omitted, Shell uses -1, which means there is no timeout
    //     and Shell does not return until the program finishes. Therefore, if you omit
    //     Timeout or set it to -1, it is possible that Shell might never return control
    //     to your program.
    //
    // Returns:
    //     Runs an executable program and returns an integer containing the program's
    //     process ID if it is still running.
    //public static int Shell(string PathName, AppWinStyle Style, bool Wait, int Timeout)
    //
    // Summary:
    //     Evaluates a list of expressions and returns an Object value corresponding
    //     to the first expression in the list that is True.
    //
    // Parameters:
    //   VarExpr:
    //     Required. Object parameter array. Must have an even number of elements. You
    //     can supply a list of Object variables or expressions separated by commas,
    //     or a single-dimensional array of Object elements.
    //
    // Returns:
    //     Evaluates a list of expressions and returns an Object value corresponding
    //     to the first expression in the list that is True.
    public static object Switch(params object[] VarExpr)
    {
      Contract.Requires(VarExpr == null || VarExpr.Length % 2 == 0);

      // F: can return null
      return default(object);
    }
  }
}
