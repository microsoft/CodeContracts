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

// File System.Web.UI.HtmlTextWriter.cs
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


namespace System.Web.UI
{
  public partial class HtmlTextWriter : TextWriter
  {
    #region Methods and constructors
    public virtual new void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
    {
    }

    protected virtual new void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
    {
    }

    public virtual new void AddAttribute(HtmlTextWriterAttribute key, string value)
    {
    }

    public virtual new void AddAttribute(string name, string value)
    {
    }

    public virtual new void AddAttribute(string name, string value, bool fEndode)
    {
    }

    public virtual new void AddStyleAttribute(HtmlTextWriterStyle key, string value)
    {
    }

    public virtual new void AddStyleAttribute(string name, string value)
    {
    }

    protected virtual new void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
    {
    }

    public virtual new void BeginRender()
    {
    }

    public override void Close()
    {
    }

    protected string EncodeAttributeValue(string value, bool fEncode)
    {
      return default(string);
    }

    protected virtual new string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
    {
      return default(string);
    }

    protected string EncodeUrl(string url)
    {
      return default(string);
    }

    public virtual new void EndRender()
    {
    }

    public virtual new void EnterStyle(System.Web.UI.WebControls.Style style)
    {
    }

    public virtual new void EnterStyle(System.Web.UI.WebControls.Style style, HtmlTextWriterTag tag)
    {
    }

    public virtual new void ExitStyle(System.Web.UI.WebControls.Style style, HtmlTextWriterTag tag)
    {
    }

    public virtual new void ExitStyle(System.Web.UI.WebControls.Style style)
    {
    }

    protected virtual new void FilterAttributes()
    {
    }

    public override void Flush()
    {
    }

    protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
    {
      return default(HtmlTextWriterAttribute);
    }

    protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
    {
      return default(string);
    }

    protected HtmlTextWriterStyle GetStyleKey(string styleName)
    {
      return default(HtmlTextWriterStyle);
    }

    protected string GetStyleName(HtmlTextWriterStyle styleKey)
    {
      return default(string);
    }

    protected virtual new HtmlTextWriterTag GetTagKey(string tagName)
    {
      return default(HtmlTextWriterTag);
    }

    protected virtual new string GetTagName(HtmlTextWriterTag tagKey)
    {
      return default(string);
    }

    public HtmlTextWriter(TextWriter writer)
    {
    }

    public HtmlTextWriter(TextWriter writer, string tabString)
    {
    }

    protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
    {
      value = default(string);

      return default(bool);
    }

    protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
    {
      return default(bool);
    }

    protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
    {
      value = default(string);

      return default(bool);
    }

    protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
    {
      return default(bool);
    }

    public virtual new bool IsValidFormAttribute(string attribute)
    {
      return default(bool);
    }

    protected virtual new bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
    {
      return default(bool);
    }

    protected virtual new bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
    {
      return default(bool);
    }

    protected virtual new bool OnTagRender(string name, HtmlTextWriterTag key)
    {
      return default(bool);
    }

    protected virtual new void OutputTabs()
    {
    }

    protected string PopEndTag()
    {
      return default(string);
    }

    protected void PushEndTag(string endTag)
    {
    }

    protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
    {
    }

    protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
    {
    }

    protected static void RegisterTag(string name, HtmlTextWriterTag key)
    {
    }

    protected virtual new string RenderAfterContent()
    {
      return default(string);
    }

    protected virtual new string RenderAfterTag()
    {
      return default(string);
    }

    protected virtual new string RenderBeforeContent()
    {
      return default(string);
    }

    protected virtual new string RenderBeforeTag()
    {
      return default(string);
    }

    public virtual new void RenderBeginTag(HtmlTextWriterTag tagKey)
    {
    }

    public virtual new void RenderBeginTag(string tagName)
    {
    }

    public virtual new void RenderEndTag()
    {
    }

    public override void Write(string format, Object arg0, Object arg1)
    {
    }

    public override void Write(string format, Object arg0)
    {
    }

    public override void Write(Object value)
    {
    }

    public override void Write(float value)
    {
    }

    public override void Write(long value)
    {
    }

    public override void Write(int value)
    {
    }

    public override void Write(string format, Object[] arg)
    {
    }

    public override void Write(char[] buffer, int index, int count)
    {
    }

    public override void Write(char[] buffer)
    {
    }

    public override void Write(char value)
    {
    }

    public override void Write(bool value)
    {
    }

    public override void Write(double value)
    {
    }

    public override void Write(string s)
    {
    }

    public virtual new void WriteAttribute(string name, string value, bool fEncode)
    {
    }

    public virtual new void WriteAttribute(string name, string value)
    {
    }

    public virtual new void WriteBeginTag(string tagName)
    {
    }

    public virtual new void WriteBreak()
    {
    }

    public virtual new void WriteEncodedText(string text)
    {
    }

    public virtual new void WriteEncodedUrl(string url)
    {
    }

    public virtual new void WriteEncodedUrlParameter(string urlText)
    {
    }

    public virtual new void WriteEndTag(string tagName)
    {
    }

    public virtual new void WriteFullBeginTag(string tagName)
    {
    }

    public override void WriteLine(Object value)
    {
    }

    public override void WriteLine(long value)
    {
    }

    public override void WriteLine(int value)
    {
    }

    public override void WriteLine(string format, Object arg0)
    {
    }

    public override void WriteLine(string format, Object[] arg)
    {
    }

    public override void WriteLine(string format, Object arg0, Object arg1)
    {
    }

    public override void WriteLine(uint value)
    {
    }

    public override void WriteLine(bool value)
    {
    }

    public override void WriteLine(char value)
    {
    }

    public override void WriteLine(string s)
    {
    }

    public override void WriteLine()
    {
    }

    public override void WriteLine(double value)
    {
    }

    public override void WriteLine(float value)
    {
    }

    public override void WriteLine(char[] buffer)
    {
    }

    public override void WriteLine(char[] buffer, int index, int count)
    {
    }

    public void WriteLineNoTabs(string s)
    {
    }

    public virtual new void WriteStyleAttribute(string name, string value)
    {
    }

    public virtual new void WriteStyleAttribute(string name, string value, bool fEncode)
    {
    }

    protected void WriteUrlEncodedString(string text, bool argument)
    {
    }
    #endregion

    #region Properties and indexers
    public override Encoding Encoding
    {
      get
      {
        return default(Encoding);
      }
    }

    public int Indent
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public TextWriter InnerWriter
    {
      get
      {
        return default(TextWriter);
      }
      set
      {
      }
    }

    public override string NewLine
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected HtmlTextWriterTag TagKey
    {
      get
      {
        return default(HtmlTextWriterTag);
      }
      set
      {
      }
    }

    protected string TagName
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

    #region Fields
    public static string DefaultTabString;
    public static char DoubleQuoteChar;
    public static string EndTagLeftChars;
    public static char EqualsChar;
    public static string EqualsDoubleQuoteString;
    public static string SelfClosingChars;
    public static string SelfClosingTagEnd;
    public static char SemicolonChar;
    public static char SingleQuoteChar;
    public static char SlashChar;
    public static char SpaceChar;
    public static char StyleEqualsChar;
    public static char TagLeftChar;
    public static char TagRightChar;
    #endregion
  }
}
