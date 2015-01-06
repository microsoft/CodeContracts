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
  //     Represents the HTML document in the browser.
  public sealed class HtmlDocument
  {
    internal HtmlDocument() { }

    // Summary:
    //     Gets a reference to the BODY element of the HTML document.
    //
    // Returns:
    //     The HTML document's BODY element.
    public HtmlElement Body
    {
      get
      {
        Contract.Ensures(Contract.Result<HtmlElement>() != null);
        return default(HtmlElement);
      }
    }
    //
    // Summary:
    //     Gets or sets the browser's cookie string.
    //
    // Returns:
    //     The cookie string that is stored by the browser. If the browser does not
    //     have a cookie string, the property returns an empty string.
    public string Cookies
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(String);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    //
    // Summary:
    //     Gets a reference to the browser's DOCUMENT element.
    //
    // Returns:
    //     The browser's DOCUMENT element.
    public HtmlElement DocumentElement
    {
      get
      {
        Contract.Ensures(Contract.Result<HtmlElement>() != null);
        return default(HtmlElement);
      }
    }
    //
    // Summary:
    //     Gets a Uniform Resource Identifier (URI) object that represents the HTML
    //     document on which the Silverlight plug-in is hosted.
    //
    // Returns:
    //     The URI of the current HTML document.
    public Uri DocumentUri
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);
        return default(Uri);
      }
    }
    //
    // Summary:
    //     Gets or sets a value that indicates whether the browser has completely loaded
    //     the HTML page.
    //
    // Returns:
    //     true if the browser has completely loaded the HTML page that contains the
    //     Silverlight plug-in, the page has been fully parsed, and all Document Object
    //     Model (DOM) objects are available for programming; otherwise, false.
    extern public bool IsReady { get; }
    //
    // Summary:
    //     Gets a navigable, read-only collection of name/value pairs that represent
    //     the query string parameters on the current page's URL.
    //
    // Returns:
    //     The query string parameters on the current page's URL.
    public IDictionary<string, string> QueryString
    {
      get
      {
        Contract.Ensures(Contract.Result<IDictionary<string, string>>() != null);
        return default(IDictionary<string, string>);
      }
    }

    // Summary:
    //     Occurs when the browser page has completely loaded.
    extern public event EventHandler DocumentReady;

    // Summary:
    //     Creates a browser element.
    //
    // Parameters:
    //   tagName:
    //     The tag name of the browser element to create.
    //
    // Returns:
    //     A reference to a browser element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     tagName is an empty string.
    //
    //   System.ArgumentNullException:
    //     tagName is null.
    public HtmlElement CreateElement(string tagName)
    {
      Contract.Requires(tagName != null);
      Contract.Ensures(Contract.Result<HtmlElement>() != null);
      return default(HtmlElement);
    }
    //
    // Summary:
    //     Gets a single browser element.
    //
    // Parameters:
    //   id:
    //     A string identifier for a named browser element.
    //
    // Returns:
    //     A reference to a browser element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     id is an empty string.
    //
    //   System.ArgumentNullException:
    //     id is null.
    [Pure]
    public HtmlElement GetElementById(string id)
    {
      Contract.Requires(id != null);
      return null;
    }
    //
    // Summary:
    //     Gets a collection of browser elements.
    //
    // Parameters:
    //   tagName:
    //     A browser element's tag name.
    //
    // Returns:
    //     A collection of references to HTML elements that correspond to the requested
    //     tag name.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     tagName is an empty string.
    //
    //   System.ArgumentNullException:
    //     tagName is null.
    [Pure]
    public ScriptObjectCollection GetElementsByTagName(string tagName)
    {
      Contract.Requires(tagName != null);
      Contract.Ensures(Contract.Result<ScriptObjectCollection>() != null);
      return default(ScriptObjectCollection);
    }
    //
    // Summary:
    //     Posts user data from the FORM element in the browser's Document Object Model
    //     (DOM) to the server.
    extern public void Submit();
    //
    // Summary:
    //     Posts user data from the specified FORM element to the server.
    //
    // Parameters:
    //   formId:
    //     The value of the id attribute for an HTML FORM element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     formId is an empty string.
    //
    //   System.ArgumentNullException:
    //     formId is null.
    extern public void Submit(string formId);
  }
}
