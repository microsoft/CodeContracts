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
using System.Diagnostics.Contracts;


namespace System.Windows.Browser
{
  // Summary:
  //     Provides the managed representation of the JavaScript window object.
  public sealed class HtmlWindow
  {
    internal HtmlWindow() { }

    // Summary:
    //     Gets or sets a string that represents the hash value of the current page's
    //     URL.
    //
    // Returns:
    //     The hash value of the current page's URL. If the URL has no hash value, this
    //     property returns an empty string.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The return value is null.
    public string CurrentBookmark
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

    // Summary:
    //     Displays a dialog box that contains an application-defined message.
    //
    // Parameters:
    //   alertText:
    //     The text to display.
    [Pure]
    public void Alert(string alertText)
    {
    }
    //
    // Summary:
    //     Displays a confirmation dialog box that contains an optional message as well
    //     as OK and Cancel buttons.
    //
    // Parameters:
    //   confirmText:
    //     The text to display.
    //
    // Returns:
    //     true if the user clicked the OK button; otherwise, false.
    [Pure]
    public bool Confirm(string confirmText) {
      return default(bool);
    }
    //
    // Summary:
    //     Creates an instance of the specified JavaScript type.
    //
    // Parameters:
    //   typeName:
    //     An HTML tag name.
    //
    //   args:
    //     Creation parameters.
    //
    // Returns:
    //     A JavaScript type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     typeName is null.
    //
    //   System.ArgumentException:
    //     typeName is an empty string or cannot be resolved.  -or- There is a type
    //     mismatch between one of the supplied creation parameters and the target parameter(s).
    public ScriptObject CreateInstance(string typeName, params object[] args)
    {
      Contract.Requires(typeName != null);
      Contract.Ensures(Contract.Result<ScriptObject>() != null);
      return default(ScriptObject);
    }
    //
    // Summary:
    //     Evaluates a string that contains arbitrary JavaScript code.
    //
    // Parameters:
    //   code:
    //     JavaScript code.
    //
    // Returns:
    //     The results of the JavaScript engine's evaluation of the string in the code
    //     parameter.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     code is null.
    //
    //   System.ArgumentException:
    //     code is an empty string.
    public object Eval(string code)
    {
      Contract.Requires(code != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    //
    // Summary:
    //     Opens the specified page in the current browser.
    //
    // Parameters:
    //   navigateToUri:
    //     The URL of the page to open.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     navigateToUri is null.
    public void Navigate(Uri navigateToUri)
    {
      Contract.Requires(navigateToUri != null);
    }
    //
    // Summary:
    //     Opens the specified page in the specified browser instance.
    //
    // Parameters:
    //   navigateToUri:
    //     The URL of the page to open.
    //
    //   target:
    //     The name of the window or tab that navigateToUri should be opened in.
    //
    // Returns:
    //     A reference to the underlying window object that represents a new browser
    //     instance.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     navigateToUri is null.
    //
    //   System.ArgumentNullException:
    //     target is null.
    public HtmlWindow Navigate(Uri navigateToUri, string target)
    {
      Contract.Requires(navigateToUri != null);
      Contract.Requires(target != null);
      Contract.Ensures(Contract.Result<HtmlWindow>() != null);
      return default(HtmlWindow);
    }
    //
    // Summary:
    //     Opens the specified page in the specified browser instance, with the indicated
    //     user interface features.
    //
    // Parameters:
    //   navigateToUri:
    //     The URL of the page to open.
    //
    //   target:
    //     The name of the window or tab that navigateToUri should be opened in.
    //
    //   targetFeatures:
    //     A comma-delimited list of browser-specific features.
    //
    // Returns:
    //     A reference to the underlying window object that represents the new browser.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     navigateToUri is null.  -or- target is null.  -or- targetFeatures is null.
    public HtmlWindow Navigate(Uri navigateToUri, string target, string targetFeatures)
    {
      Contract.Requires(navigateToUri != null);
      Contract.Requires(target != null);
      Contract.Requires(targetFeatures != null);

      Contract.Ensures(Contract.Result<HtmlWindow>() != null);
      return default(HtmlWindow);
    }
    //
    // Summary:
    //     Changes the URL of the current page to a location that is specified by a
    //     named bookmark.
    //
    // Parameters:
    //   bookmark:
    //     The name of a bookmark.
    public void NavigateToBookmark(string bookmark)
    {
    }

    //
    // Summary:
    //     Displays a dialog box that prompts the user with a message and an input field.
    //
    // Parameters:
    //   promptText:
    //     Message to display in the dialog box.
    //
    // Returns:
    //     The user's input.
    public string Prompt(string promptText)
    {
      Contract.Requires(promptText != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
