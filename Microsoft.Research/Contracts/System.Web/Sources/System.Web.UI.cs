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

// File System.Web.UI.cs
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
  public delegate Control BuildMethod();

  public delegate void BuildTemplateMethod(Control control);

  public enum ClientIDMode
  {
    Inherit = 0, 
    AutoID = 1, 
    Predictable = 2, 
    Static = 3, 
  }

  public enum CodeConstructType
  {
    CodeSnippet = 0, 
    ExpressionSnippet = 1, 
    DataBindingSnippet = 2, 
    ScriptTag = 3, 
    EncodedExpressionSnippet = 4, 
  }

  public enum CompilationMode
  {
    Auto = 0, 
    Never = 1, 
    Always = 2, 
  }

  public enum ConflictOptions
  {
    OverwriteChanges = 0, 
    CompareAllValues = 1, 
  }

  public delegate Control ControlSkinDelegate(Control control);

  public enum DataSourceCacheExpiry
  {
    Absolute = 0, 
    Sliding = 1, 
  }

  public enum DataSourceCapabilities
  {
    None = 0, 
    Sort = 1, 
    Page = 2, 
    RetrieveTotalRowCount = 4, 
  }

  public enum DataSourceOperation
  {
    Delete = 0, 
    Insert = 1, 
    Select = 2, 
    Update = 3, 
    SelectCount = 4, 
  }

  public delegate bool DataSourceViewOperationCallback(int affectedRecords, Exception ex);

  public delegate void DataSourceViewSelectCallback(System.Collections.IEnumerable data);

  public delegate System.Collections.Specialized.IOrderedDictionary ExtractTemplateValuesMethod(Control control);

  public enum HtmlTextWriterAttribute
  {
    Accesskey = 0, 
    Align = 1, 
    Alt = 2, 
    Background = 3, 
    Bgcolor = 4, 
    Border = 5, 
    Bordercolor = 6, 
    Cellpadding = 7, 
    Cellspacing = 8, 
    Checked = 9, 
    Class = 10, 
    Cols = 11, 
    Colspan = 12, 
    Disabled = 13, 
    For = 14, 
    Height = 15, 
    Href = 16, 
    Id = 17, 
    Maxlength = 18, 
    Multiple = 19, 
    Name = 20, 
    Nowrap = 21, 
    Onchange = 22, 
    Onclick = 23, 
    ReadOnly = 24, 
    Rows = 25, 
    Rowspan = 26, 
    Rules = 27, 
    Selected = 28, 
    Size = 29, 
    Src = 30, 
    Style = 31, 
    Tabindex = 32, 
    Target = 33, 
    Title = 34, 
    Type = 35, 
    Valign = 36, 
    Value = 37, 
    Width = 38, 
    Wrap = 39, 
    Abbr = 40, 
    AutoComplete = 41, 
    Axis = 42, 
    Content = 43, 
    Coords = 44, 
    DesignerRegion = 45, 
    Dir = 46, 
    Headers = 47, 
    Longdesc = 48, 
    Rel = 49, 
    Scope = 50, 
    Shape = 51, 
    Usemap = 52, 
    VCardName = 53, 
  }

  public enum HtmlTextWriterStyle
  {
    BackgroundColor = 0, 
    BackgroundImage = 1, 
    BorderCollapse = 2, 
    BorderColor = 3, 
    BorderStyle = 4, 
    BorderWidth = 5, 
    Color = 6, 
    FontFamily = 7, 
    FontSize = 8, 
    FontStyle = 9, 
    FontWeight = 10, 
    Height = 11, 
    TextDecoration = 12, 
    Width = 13, 
    ListStyleImage = 14, 
    ListStyleType = 15, 
    Cursor = 16, 
    Direction = 17, 
    Display = 18, 
    Filter = 19, 
    FontVariant = 20, 
    Left = 21, 
    Margin = 22, 
    MarginBottom = 23, 
    MarginLeft = 24, 
    MarginRight = 25, 
    MarginTop = 26, 
    Overflow = 27, 
    OverflowX = 28, 
    OverflowY = 29, 
    Padding = 30, 
    PaddingBottom = 31, 
    PaddingLeft = 32, 
    PaddingRight = 33, 
    PaddingTop = 34, 
    Position = 35, 
    TextAlign = 36, 
    VerticalAlign = 37, 
    TextOverflow = 38, 
    Top = 39, 
    Visibility = 40, 
    WhiteSpace = 41, 
    ZIndex = 42, 
  }

  public enum HtmlTextWriterTag
  {
    Unknown = 0, 
    A = 1, 
    Acronym = 2, 
    Address = 3, 
    Area = 4, 
    B = 5, 
    Base = 6, 
    Basefont = 7, 
    Bdo = 8, 
    Bgsound = 9, 
    Big = 10, 
    Blockquote = 11, 
    Body = 12, 
    Br = 13, 
    Button = 14, 
    Caption = 15, 
    Center = 16, 
    Cite = 17, 
    Code = 18, 
    Col = 19, 
    Colgroup = 20, 
    Dd = 21, 
    Del = 22, 
    Dfn = 23, 
    Dir = 24, 
    Div = 25, 
    Dl = 26, 
    Dt = 27, 
    Em = 28, 
    Embed = 29, 
    Fieldset = 30, 
    Font = 31, 
    Form = 32, 
    Frame = 33, 
    Frameset = 34, 
    H1 = 35, 
    H2 = 36, 
    H3 = 37, 
    H4 = 38, 
    H5 = 39, 
    H6 = 40, 
    Head = 41, 
    Hr = 42, 
    Html = 43, 
    I = 44, 
    Iframe = 45, 
    Img = 46, 
    Input = 47, 
    Ins = 48, 
    Isindex = 49, 
    Kbd = 50, 
    Label = 51, 
    Legend = 52, 
    Li = 53, 
    Link = 54, 
    Map = 55, 
    Marquee = 56, 
    Menu = 57, 
    Meta = 58, 
    Nobr = 59, 
    Noframes = 60, 
    Noscript = 61, 
    Object = 62, 
    Ol = 63, 
    Option = 64, 
    P = 65, 
    Param = 66, 
    Pre = 67, 
    Q = 68, 
    Rt = 69, 
    Ruby = 70, 
    S = 71, 
    Samp = 72, 
    Script = 73, 
    Select = 74, 
    Small = 75, 
    Span = 76, 
    Strike = 77, 
    Strong = 78, 
    Style = 79, 
    Sub = 80, 
    Sup = 81, 
    Table = 82, 
    Tbody = 83, 
    Td = 84, 
    Textarea = 85, 
    Tfoot = 86, 
    Th = 87, 
    Thead = 88, 
    Title = 89, 
    Tr = 90, 
    Tt = 91, 
    U = 92, 
    Ul = 93, 
    Var = 94, 
    Wbr = 95, 
    Xml = 96, 
  }

  public delegate void ImageClickEventHandler(Object sender, ImageClickEventArgs e);

  public enum OutputCacheLocation
  {
    Any = 0, 
    Client = 1, 
    Downstream = 2, 
    Server = 3, 
    None = 4, 
    ServerAndClient = 5, 
  }

  public enum PersistenceMode
  {
    Attribute = 0, 
    InnerProperty = 1, 
    InnerDefaultProperty = 2, 
    EncodedInnerDefaultProperty = 3, 
  }

  public delegate void RenderMethod(HtmlTextWriter output, Control container);

  public enum TemplateInstance
  {
    Multiple = 0, 
    Single = 1, 
  }

  public enum VerificationConditionalOperator
  {
    Equals = 0, 
    NotEquals = 1, 
  }

  public enum VerificationReportLevel
  {
    Error = 0, 
    Warning = 1, 
    Guideline = 2, 
  }

  public enum VerificationRule
  {
    Required = 0, 
    Prohibited = 1, 
    NotEmptyString = 2, 
  }

  public enum ViewStateEncryptionMode
  {
    Auto = 0, 
    Always = 1, 
    Never = 2, 
  }

  public enum ViewStateMode
  {
    Inherit = 0, 
    Enabled = 1, 
    Disabled = 2, 
  }

  public enum VirtualReferenceType
  {
    Page = 0, 
    UserControl = 1, 
    Master = 2, 
    SourceFile = 3, 
    Other = 4, 
  }

  public enum XhtmlMobileDocType
  {
    XhtmlBasic = 0, 
    XhtmlMobileProfile = 1, 
    Wml20 = 2, 
  }
}
