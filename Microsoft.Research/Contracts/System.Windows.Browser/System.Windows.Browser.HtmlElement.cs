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
  //     Represents an HTML element in the Document Object Model (DOM) of a Web page.
  public sealed class HtmlElement 
  {
    internal HtmlElement() { }

    // Summary:
    //     Gets a read-only collection of HTML elements that are immediate descendants
    //     of the current HTML element.
    //
    // Returns:
    //     A collection of HTML elements. If the current HTML element has no children,
    //     the returned collection is empty.
    public ScriptObjectCollection Children
    {
      get
      {
        Contract.Ensures(Contract.Result<ScriptObjectCollection>() != null);
        return default(ScriptObjectCollection);
      }
    }
    //
    // Summary:
    //     Gets or sets the cascading style sheet (CSS) class string for the current
    //     HTML element.
    //
    // Returns:
    //     A CSS class string if the element is associated with a CSS class; otherwise,
    //     an empty string.
    public string CssClass { get
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
    //     Gets the identifier of the current HTML element.
    //
    // Returns:
    //     An HTML element ID string if the current element has an identifier; otherwise,
    //     an empty string.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The property is set to an empty string.
    //
    //   System.ArgumentNullException:
    //     The property is set to null.
    public string Id
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
    //     Gets a reference to the parent of the current HTML element.
    //
    // Returns:
    //     An HTML element reference if the element has a parent; otherwise, null.
    extern public HtmlElement Parent { get; }
    //
    // Summary:
    //     Gets the HTML tag name of the current HTML element.
    //
    // Returns:
    //     An HTML element tag name, such as div or span.
    public string TagName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }
    // Summary:
    //     Adds an element to the end of the child collection for the current HTML element.
    //
    // Parameters:
    //   element:
    //     The element to be added.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     element is null.
    public void AppendChild(HtmlElement element)
    {
      Contract.Requires(element != null);
    }
    //
    // Summary:
    //     Adds an element at a specified location in the child element collection for
    //     the current HTML element.
    //
    // Parameters:
    //   element:
    //     The element to be added.
    //
    //   referenceElement:
    //     The location to insert the element. The element is added before referenceElement.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     referenceElement is not in the child element collection of the current HTML
    //     element.
    //
    //   System.ArgumentNullException:
    //     element is null.
    public void AppendChild(HtmlElement element, HtmlElement referenceElement)
    {
      Contract.Requires(element != null);
    }
    //
    // Summary:
    //     Sets the browser focus to the current HTML element.
    extern public void Focus();
    //
    // Summary:
    //     Gets the value of the specified attribute on the current HTML element.
    //
    // Parameters:
    //   name:
    //     The name of an attribute.
    //
    // Returns:
    //     An attribute on the current HTML element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public string GetAttribute(string name)
    {
      Contract.Requires(name != null);
      return null;
    }
    //
    // Summary:
    //     Retrieves the specified style attribute for the current HTML element.
    //
    // Parameters:
    //   name:
    //     The name of the style attribute to retrieve.
    //
    // Returns:
    //     The style attribute for the current HTML element.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public string GetStyleAttribute(string name)
    {
      Contract.Requires(name != null);
      return null;
    }
    //
    // Summary:
    //     Removes an attribute from the current HTML element.
    //
    // Parameters:
    //   name:
    //     The name of the attribute to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public void RemoveAttribute(string name)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Removes the specified element from the child element collection of the current
    //     HTML element.
    //
    // Parameters:
    //   element:
    //     The element to remove.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     element is null.
    public void RemoveChild(HtmlElement element)
    {
      Contract.Requires(element != null);
    }
    //
    // Summary:
    //     Removes a style attribute on the current HTML element.
    //
    // Parameters:
    //   name:
    //     The style attribute to be removed.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public void RemoveStyleAttribute(string name)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Sets the value of an attribute on the current HTML element.
    //
    // Parameters:
    //   name:
    //     The attribute whose value will be set.
    //
    //   value:
    //     The attribute's new value.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public void SetAttribute(string name, string value)
    {
      Contract.Requires(name != null);
    }
    //
    // Summary:
    //     Sets the value of a style attribute on the current HTML element.
    //
    // Parameters:
    //   name:
    //     The style attribute whose value will be set.
    //
    //   value:
    //     The style attribute's new value.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     name is an empty string.
    //
    //   System.ArgumentNullException:
    //     name is null.
    public void SetStyleAttribute(string name, string value)
    {
      Contract.Requires(name != null);
    }
  }
}
