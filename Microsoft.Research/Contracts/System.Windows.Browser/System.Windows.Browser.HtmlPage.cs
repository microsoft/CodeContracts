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
  //     Permits access to, and manipulation of, the browser's Document Object Model
  //     (DOM).
  public static class HtmlPage
  {
    // Summary:
    //     Gets general information about the browser, such as name, version, and operating
    //     system.
    //
    // Returns:
    //     An object that represents general information about the hosting browser.
    public static BrowserInformation BrowserInformation
    {
      get
      {
        Contract.Ensures(Contract.Result<BrowserInformation>() != null);
        return default(BrowserInformation);
      }
    }
    //
    // Summary:
    //     Gets the browser's document object.
    //
    // Returns:
    //     A reference to the browser's document object.
    public static HtmlDocument Document
    {
      get
      {
        Contract.Ensures(Contract.Result<HtmlDocument>() != null);
        return default(HtmlDocument);
      }
    }
    //
    // Summary:
    //     Gets a value that indicates whether the rest of the public surface area of
    //     the HTML Bridge feature is enabled.
    //
    // Returns:
    //     true if the HTML Bridge feature is enabled; otherwise, false.
    extern public static bool IsEnabled { get; }
    //
    // Summary:
    //     Indicates whether pop-up windows are allowed.
    //
    // Returns:
    //     true if pop-up windows are allowed; otherwise, false.
    extern public static bool IsPopupWindowAllowed { get; }
    //
    // Summary:
    //     Gets a reference to the Silverlight plug-in that is defined within an <object>
    //     or <embed> tag on the host HTML page.
    //
    // Returns:
    //     The Silverlight plug-in in the Document Object Model (DOM).
    public static HtmlElement Plugin
    {
      get
      {
        Contract.Ensures(Contract.Result<HtmlElement>() != null);
        return default(HtmlElement);
      }
    }
    //
    // Summary:
    //     Gets the browser's window object.
    //
    // Returns:
    //     A reference to the browser's window object.
    public static HtmlWindow Window
    {
      get
      {
        Contract.Ensures(Contract.Result<HtmlWindow>() != null);
        return default(HtmlWindow);
      }
    }

    // Summary:
    //     Opens a pop-up window.
    //
    // Parameters:
    //   navigateToUri:
    //     Specifies the Uniform Resource Identifier (URI) for the pop-up window.
    //
    //   target:
    //     Specifies the window that will be opened. (For more information, see the
    //     Remarks section.)
    //
    //   options:
    //     Specifies an System.Windows.Browser.HtmlPopupWindowOptions object that controls
    //     the pop-up window. (For more information, see the Remarks section.)
    //
    // Returns:
    //     A handle to the new window; or null (see the Remarks section).
    //
    // Exceptions:
    //   System.ArgumentException:
    //     navigateToUri specifies a protocol other than http or https, such as file://,
    //     or a random URI moniker.
    //
    //   System.ArgumentNullException:
    //     navigateToUri is set to null.
    public static HtmlWindow PopupWindow(Uri navigateToUri, string target, HtmlPopupWindowOptions options)
    {
      Contract.Requires(navigateToUri != null);

      Contract.Ensures(Contract.Result<HtmlWindow>() != null);
      return default(HtmlWindow);

    }
    //
    // Summary:
    //     Registers a managed type as available for creation from JavaScript code,
    //     through the Content.services.createObject and Content.services.createManagedObject
    //     helper methods.
    //
    // Parameters:
    //   scriptAlias:
    //     The name used to register the managed type.
    //
    //   type:
    //     A managed type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     scriptAlias or type is null.
    //
    //   System.ArgumentException:
    //     type is not public.  -or- type does not have a public, parameterless constructor.
    //      -or- type is a string, primitive or value type, managed delegate, or empty
    //     string.  -or- type contains an embedded null character (\0).  -or- An attempt
    //     is made to reregister type.
    public static void RegisterCreateableType(string scriptAlias, Type type)
    {
      Contract.Requires(scriptAlias != null);
      Contract.Requires(type != null);
    }
    //
    // Summary:
    //     Registers a managed object for scriptable access by JavaScript code.
    //
    // Parameters:
    //   scriptKey:
    //     The name used to register the managed object.
    //
    //   instance:
    //     A managed object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     scriptKey or instance is null.
    //
    //   System.ArgumentException:
    //     An attempt is made to register a non-public type.  -or- scriptKey contains
    //     an embedded null character (\0).  -or- instance has no scriptable entry points.
    //      -or- A property, method, or event that is marked as scriptable uses one
    //     of the following reserved names: addEventListener, removeEventListener, constructor,
    //     or createManagedObject.
    public static void RegisterScriptableObject(string scriptKey, object instance)
    {
      Contract.Requires(scriptKey != null);
      Contract.Requires(instance != null);
    }
    //
    // Summary:
    //     Explicitly unregisters a managed type that was previously registered by using
    //     the System.Windows.Browser.HtmlPage.RegisterCreateableType(System.String,System.Type)
    //     method.
    //
    // Parameters:
    //   scriptAlias:
    //     The name of a registered managed type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     scriptAlias is null.
    //
    //   System.ArgumentException:
    //     scriptAlias is an empty string.
    public static void UnregisterCreateableType(string scriptAlias)
    {
      Contract.Requires(scriptAlias != null);
    }
  }
}
