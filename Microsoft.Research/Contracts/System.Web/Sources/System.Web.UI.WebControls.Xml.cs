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

// File System.Web.UI.WebControls.Xml.cs
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


namespace System.Web.UI.WebControls
{
  public partial class Xml : System.Web.UI.Control
  {
    #region Methods and constructors
    protected override void AddParsedSubObject(Object obj)
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    public override System.Web.UI.Control FindControl(string id)
    {
      return default(System.Web.UI.Control);
    }

    public override void Focus()
    {
    }

    protected override System.Collections.IDictionary GetDesignModeState()
    {
      return default(System.Collections.IDictionary);
    }

    public override bool HasControls()
    {
      return default(bool);
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter output)
    {
    }

    public Xml()
    {
    }
    #endregion

    #region Properties and indexers
    public override string ClientID
    {
      get
      {
        return default(string);
      }
    }

    public override System.Web.UI.ControlCollection Controls
    {
      get
      {
        return default(System.Web.UI.ControlCollection);
      }
    }

    public System.Xml.XmlDocument Document
    {
      get
      {
        return default(System.Xml.XmlDocument);
      }
      set
      {
      }
    }

    public string DocumentContent
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string DocumentSource
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override bool EnableTheming
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override string SkinID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Xml.Xsl.XslTransform Transform
    {
      get
      {
        return default(System.Xml.Xsl.XslTransform);
      }
      set
      {
      }
    }

    public System.Xml.Xsl.XsltArgumentList TransformArgumentList
    {
      get
      {
        return default(System.Xml.Xsl.XsltArgumentList);
      }
      set
      {
      }
    }

    public string TransformSource
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public System.Xml.XPath.XPathNavigator XPathNavigator
    {
      get
      {
        return default(System.Xml.XPath.XPathNavigator);
      }
      set
      {
      }
    }
    #endregion
  }
}
