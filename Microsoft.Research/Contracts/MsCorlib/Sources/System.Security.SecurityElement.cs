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

// File System.Security.SecurityElement.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Security
{
  sealed public partial class SecurityElement : ISecurityElementFactory
  {
    #region Methods and constructors
    public void AddAttribute(string name, string value)
    {
    }

    public void AddChild(System.Security.SecurityElement child)
    {
    }

    public string Attribute(string name)
    {
      return default(string);
    }

    public System.Security.SecurityElement Copy()
    {
      Contract.Ensures(Contract.Result<System.Security.SecurityElement>() != null);

      return default(System.Security.SecurityElement);
    }

    public bool Equal(System.Security.SecurityElement other)
    {
      return default(bool);
    }

    public static string Escape(string str)
    {
      return default(string);
    }

    public static SecurityElement FromString(string xml)
    {
      return default(SecurityElement);
    }

    public static bool IsValidAttributeName(string name)
    {
      return default(bool);
    }

    public static bool IsValidAttributeValue(string value)
    {
      return default(bool);
    }

    public static bool IsValidTag(string tag)
    {
      return default(bool);
    }

    public static bool IsValidText(string text)
    {
      return default(bool);
    }

    public System.Security.SecurityElement SearchForChildByTag(string tag)
    {
      return default(System.Security.SecurityElement);
    }

    public string SearchForTextOfTag(string tag)
    {
      return default(string);
    }

    public SecurityElement(string tag)
    {
    }

    public SecurityElement(string tag, string text)
    {
    }

    string System.Security.ISecurityElementFactory.Attribute(string attributeName)
    {
      return default(string);
    }

    Object System.Security.ISecurityElementFactory.Copy()
    {
      return default(Object);
    }

    SecurityElement System.Security.ISecurityElementFactory.CreateSecurityElement()
    {
      return default(SecurityElement);
    }

    string System.Security.ISecurityElementFactory.GetTag()
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public System.Collections.Hashtable Attributes
    {
      get
      {
        return default(System.Collections.Hashtable);
      }
      set
      {
      }
    }

    public System.Collections.ArrayList Children
    {
      get
      {
        return default(System.Collections.ArrayList);
      }
      set
      {
      }
    }

    public string Tag
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion
  }
}
