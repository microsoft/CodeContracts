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
using System.IO;
using System.Diagnostics.Contracts;
using System.Web.UI.WebControls;

namespace System.Web.UI
{
  public class HtmlTextWriter
  {
    public HtmlTextWriter(TextWriter writer)
    {
      Contract.Requires(writer != null);
    }

    public HtmlTextWriter(TextWriter writer, string tabString)
    {
      Contract.Requires(writer != null);
    }
    // public virtual void AddAttribute(string name, string value);

    // public virtual void AddAttribute(HtmlTextWriterAttribute key, string value);
    // public virtual void AddAttribute(string name, string value, bool fEndode);
    // protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key);
    // public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode);

    // public virtual void AddStyleAttribute(string name, string value);
    // public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value);
    // protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key);
    // public virtual void BeginRender();

    protected string EncodeAttributeValue(string value, bool fEncode)
    {
      Contract.Ensures(Contract.Result<string>() != null || value == null);
      return default(string);
    }
    protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
    {
      Contract.Ensures(Contract.Result<string>() != null || value == null);
      return default(string);
    }
    // protected string EncodeUrl(string url);
    // public virtual void EndRender();
    public virtual void EnterStyle(Style style)
    {
      Contract.Requires(style != null);
    }
    public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
    {
      Contract.Requires(style != null);
    }

    public virtual void ExitStyle(Style style)
    {
      Contract.Requires(style != null);
    }
    public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
    {
      Contract.Requires(style != null);
    }
    // protected virtual void FilterAttributes();
    // public override void Flush();
    [Pure]
    protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
    {
      return default(HtmlTextWriterAttribute);
    }
    [Pure]
    protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    [Pure]
    protected HtmlTextWriterStyle GetStyleKey(string styleName)
    {
      return default(HtmlTextWriterStyle);
    }
    [Pure]
    protected string GetStyleName(HtmlTextWriterStyle styleKey)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    // protected virtual HtmlTextWriterTag GetTagKey(string tagName);
    [Pure]
    protected virtual string GetTagName(HtmlTextWriterTag tagKey)
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    [Pure]
    protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
    {
      return default(bool);
    }
    [Pure]
    protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      value = null;
      return default(bool);
    }
    [Pure]
    protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
    {
      return default(bool);
    }
    [Pure]
    protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
    {
      Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out value) != null);
      value = null;
      return default(bool);
    }
    [Pure]
    public virtual bool IsValidFormAttribute(string attribute)
    {
      return default(bool);
    }
    protected virtual bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
    {
      Contract.Requires(name != null);
      Contract.Requires(value != null);
      return default(bool);
    }
    protected virtual bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
    {
      Contract.Requires(name != null);
      Contract.Requires(value != null);
      return default(bool);
    }
    protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
    {
      Contract.Requires(name != null);
      return default(bool);
    }
    // protected virtual void OutputTabs();
    protected string PopEndTag()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    protected void PushEndTag(string endTag)
    {
      Contract.Requires(endTag != null);
    }
    protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
    {
      Contract.Requires(name != null);
    }
    protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
    {
      Contract.Requires(name != null);
    }
    protected static void RegisterTag(string name, HtmlTextWriterTag key)
    {
      Contract.Requires(name != null);
    }
    //protected virtual string RenderAfterContent();
    //protected virtual string RenderAfterTag();
    //protected virtual string RenderBeforeContent();
    //protected virtual string RenderBeforeTag();
    //public virtual void RenderBeginTag(string tagName);
    //public virtual void RenderBeginTag(HtmlTextWriterTag tagKey);
    //public virtual void RenderEndTag();
    //public override void Write(bool value);
#if false
    public override void Write(char value);
    public override void Write(char[] buffer);
    public override void Write(double value);
    public override void Write(int value);
    public override void Write(long value);
    public override void Write(object value);
    public override void Write(float value);
    public override void Write(string s);
    public virtual void Write(string format, params object[] arg)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);
    }
    public override void Write(string format, object arg0)
    {
      Contract.Requires(format != null);
    }
    public override void Write(char[] buffer, int index, int count)
    {
      Contract.Requires(buffer != null);
    }
    public override void Write(string format, object arg0, object arg1)
    {
      Contract.Requires(format != null);
    }
#endif
    //public virtual void WriteAttribute(string name, string value);
    //public virtual void WriteAttribute(string name, string value, bool fEncode);
    public virtual void WriteBeginTag(string tagName)
    {
      Contract.Requires(tagName != null);
    }
    //public virtual void WriteBreak();
    public virtual void WriteEncodedText(string text)
    {
      Contract.Requires(text != null);
    }
    public virtual void WriteEncodedUrl(string url)
    {
      Contract.Requires(url != null);
    }
    public virtual void WriteEncodedUrlParameter(string urlText)
    {
      Contract.Requires(urlText != null);
    }
    public virtual void WriteEndTag(string tagName)
    {
      Contract.Requires(tagName != null);
    }
    public virtual void WriteFullBeginTag(string tagName)
    {
      Contract.Requires(tagName != null);
    }
    //public void WriteLineNoTabs(string s);

    public virtual void WriteStyleAttribute(string name, string value)
    {
      Contract.Requires(name != null);
    }
    public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
    {
      Contract.Requires(name != null);
    }
    protected void WriteUrlEncodedString(string text, bool argument)
    {
      Contract.Requires(text != null);
    }

    public int Indent
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
    public TextWriter InnerWriter
    {
      get
      {
        Contract.Ensures(Contract.Result<TextWriter>() != null);
        return default(TextWriter);
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    // protected HtmlTextWriterTag TagKey { get; set; }
    // protected string TagName { get; set; }


  }

}
