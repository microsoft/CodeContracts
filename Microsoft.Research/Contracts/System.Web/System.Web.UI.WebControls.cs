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

// File System.Web.UI.WebControls.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  //public delegate void AdCreatedEventHandler (Object sender, AdCreatedEventArgs e);

  //public delegate void AuthenticateEventHandler (Object sender, AuthenticateEventArgs e);

  public enum AutoCompleteType
  {
    None = 0, 
    Disabled = 1, 
    Cellular = 2, 
    Company = 3, 
    Department = 4, 
    DisplayName = 5, 
    Email = 6, 
    FirstName = 7, 
    Gender = 8, 
    HomeCity = 9, 
    HomeCountryRegion = 10, 
    HomeFax = 11, 
    HomePhone = 12, 
    HomeState = 13, 
    HomeStreetAddress = 14, 
    HomeZipCode = 15, 
    Homepage = 16, 
    JobTitle = 17, 
    LastName = 18, 
    MiddleName = 19, 
    Notes = 20, 
    Office = 21, 
    Pager = 22, 
    BusinessCity = 23, 
    BusinessCountryRegion = 24, 
    BusinessFax = 25, 
    BusinessPhone = 26, 
    BusinessState = 27, 
    BusinessStreetAddress = 28, 
    BusinessUrl = 29, 
    BusinessZipCode = 30, 
    Search = 31, 
  }

  public enum BorderStyle
  {
    NotSet = 0, 
    None = 1, 
    Dotted = 2, 
    Dashed = 3, 
    Solid = 4, 
    Double = 5, 
    Groove = 6, 
    Ridge = 7, 
    Inset = 8, 
    Outset = 9, 
  }

  public enum BulletedListDisplayMode
  {
    Text = 0, 
    HyperLink = 1, 
    LinkButton = 2, 
  }

  //public delegate void BulletedListEventHandler (Object sender, BulletedListEventArgs e);

  public enum BulletStyle
  {
    NotSet = 0, 
    Numbered = 1, 
    LowerAlpha = 2, 
    UpperAlpha = 3, 
    LowerRoman = 4, 
    UpperRoman = 5, 
    Disc = 6, 
    Circle = 7, 
    Square = 8, 
    CustomImage = 9, 
  }

  public enum ButtonColumnType
  {
    LinkButton = 0, 
    PushButton = 1, 
  }

  public enum ButtonType
  {
    Button = 0, 
    Image = 1, 
    Link = 2, 
  }

  public enum CalendarSelectionMode
  {
    None = 0, 
    Day = 1, 
    DayWeek = 2, 
    DayWeekMonth = 3, 
  }

  //public delegate void CommandEventHandler (Object sender, CommandEventArgs e);

  public enum ContentDirection
  {
    NotSet = 0, 
    LeftToRight = 1, 
    RightToLeft = 2, 
  }

  //public delegate void CreateUserErrorEventHandler (Object sender, CreateUserErrorEventArgs e);

  public enum DataBoundControlMode
  {
    ReadOnly = 0, 
    Edit = 1, 
    Insert = 2, 
  }

  public enum DataControlCellType
  {
    Header = 0, 
    Footer = 1, 
    DataCell = 2, 
  }

  public enum DataControlRowState
  {
    Normal = 0, 
    Alternate = 1, 
    Selected = 2, 
    Edit = 4, 
    Insert = 8, 
  }

  public enum DataControlRowType
  {
    Header = 0, 
    Footer = 1, 
    DataRow = 2, 
    Separator = 3, 
    Pager = 4, 
    EmptyDataRow = 5, 
  }

  //public delegate void DataGridCommandEventHandler (Object source, DataGridCommandEventArgs e);

  //public delegate void DataGridItemEventHandler (Object sender, DataGridItemEventArgs e);

  //public delegate void DataGridPageChangedEventHandler (Object source, DataGridPageChangedEventArgs e);

  //public delegate void DataGridSortCommandEventHandler (Object source, DataGridSortCommandEventArgs e);
  
  //public delegate void DataListCommandEventHandler (Object source, DataListCommandEventArgs e);

  //public delegate void DataListItemEventHandler (Object sender, DataListItemEventArgs e);

  public enum DayNameFormat
  {
    Full = 0, 
    Short = 1, 
    FirstLetter = 2, 
    FirstTwoLetters = 3, 
    Shortest = 4, 
  }

  //public delegate void DayRenderEventHandler (Object sender, DayRenderEventArgs e);

  //public delegate void DetailsViewCommandEventHandler (Object sender, DetailsViewCommandEventArgs e);

  //public delegate void DetailsViewDeletedEventHandler (Object sender, DetailsViewDeletedEventArgs e);

  //public delegate void DetailsViewDeleteEventHandler (Object sender, DetailsViewDeleteEventArgs e);

  //public delegate void DetailsViewInsertedEventHandler (Object sender, DetailsViewInsertedEventArgs e);

  //public delegate void DetailsViewInsertEventHandler (Object sender, DetailsViewInsertEventArgs e);

  public enum DetailsViewMode
  {
    ReadOnly = 0, 
    Edit = 1, 
    Insert = 2, 
  }

  //public delegate void DetailsViewModeEventHandler (Object sender, DetailsViewModeEventArgs e);

  //public delegate void DetailsViewPageEventHandler (Object sender, DetailsViewPageEventArgs e);

  //public delegate void DetailsViewUpdatedEventHandler (Object sender, DetailsViewUpdatedEventArgs e);

  //public delegate void DetailsViewUpdateEventHandler (Object sender, DetailsViewUpdateEventArgs e);

  public enum FirstDayOfWeek
  {
    Sunday = 0, 
    Monday = 1, 
    Tuesday = 2, 
    Wednesday = 3, 
    Thursday = 4, 
    Friday = 5, 
    Saturday = 6, 
    Default = 7, 
  }

  public enum FontSize
  {
    NotSet = 0, 
    AsUnit = 1, 
    Smaller = 2, 
    Larger = 3, 
    XXSmall = 4, 
    XSmall = 5, 
    Small = 6, 
    Medium = 7, 
    Large = 8, 
    XLarge = 9, 
    XXLarge = 10, 
  }

  //public delegate void FormViewCommandEventHandler (Object sender, FormViewCommandEventArgs e);

  //public delegate void FormViewDeletedEventHandler (Object sender, FormViewDeletedEventArgs e);

  //public delegate void FormViewDeleteEventHandler (Object sender, FormViewDeleteEventArgs e);

  //public delegate void FormViewInsertedEventHandler (Object sender, FormViewInsertedEventArgs e);

  //public delegate void FormViewInsertEventHandler (Object sender, FormViewInsertEventArgs e);

  public enum FormViewMode
  {
    ReadOnly = 0, 
    Edit = 1, 
    Insert = 2, 
  }

  //public delegate void FormViewModeEventHandler (Object sender, FormViewModeEventArgs e);

  //public delegate void FormViewPageEventHandler (Object sender, FormViewPageEventArgs e);

  //public delegate void FormViewUpdatedEventHandler (Object sender, FormViewUpdatedEventArgs e);

  //public delegate void FormViewUpdateEventHandler (Object sender, FormViewUpdateEventArgs e);

  public enum GridLines
  {
    None = 0, 
    Horizontal = 1, 
    Vertical = 2, 
    Both = 3, 
  }

  //public delegate void GridViewCancelEditEventHandler (Object sender, GridViewCancelEditEventArgs e);

  //public delegate void GridViewCommandEventHandler (Object sender, GridViewCommandEventArgs e);

  //public delegate void GridViewDeletedEventHandler (Object sender, GridViewDeletedEventArgs e);

  //public delegate void GridViewDeleteEventHandler (Object sender, GridViewDeleteEventArgs e);

  //public delegate void GridViewEditEventHandler (Object sender, GridViewEditEventArgs e);

  //public delegate void GridViewPageEventHandler (Object sender, GridViewPageEventArgs e);

  //public delegate void GridViewRowEventHandler (Object sender, GridViewRowEventArgs e);

  //public delegate void GridViewSelectEventHandler (Object sender, GridViewSelectEventArgs e);

  //public delegate void GridViewSortEventHandler (Object sender, GridViewSortEventArgs e);

  //public delegate void GridViewUpdatedEventHandler (Object sender, GridViewUpdatedEventArgs e);

  //public delegate void GridViewUpdateEventHandler (Object sender, GridViewUpdateEventArgs e);

  public enum HorizontalAlign
  {
    NotSet = 0, 
    Left = 1, 
    Center = 2, 
    Right = 3, 
    Justify = 4, 
  }

  public enum HotSpotMode
  {
    NotSet = 0, 
    Navigate = 1, 
    PostBack = 2, 
    Inactive = 3, 
  }

  public enum ImageAlign
  {
    NotSet = 0, 
    Left = 1, 
    Right = 2, 
    Baseline = 3, 
    Top = 4, 
    Middle = 5, 
    Bottom = 6, 
    AbsBottom = 7, 
    AbsMiddle = 8, 
    TextTop = 9, 
  }

  //public delegate void ImageMapEventHandler (Object sender, ImageMapEventArgs e);

  public enum ListItemType
  {
    Header = 0, 
    Footer = 1, 
    Item = 2, 
    AlternatingItem = 3, 
    SelectedItem = 4, 
    EditItem = 5, 
    Separator = 6, 
    Pager = 7, 
  }

  public enum ListSelectionMode
  {
    Single = 0, 
    Multiple = 1, 
  }

  public enum LiteralMode
  {
    Transform = 0, 
    PassThrough = 1, 
    Encode = 2, 
  }

  //public delegate void LoginCancelEventHandler (Object sender, LoginCancelEventArgs e);

  public enum LoginFailureAction
  {
    Refresh = 0, 
    RedirectToLoginPage = 1, 
  }

  public enum LoginTextLayout
  {
    TextOnLeft = 0, 
    TextOnTop = 1, 
  }

  public enum LogoutAction
  {
    Refresh = 0, 
    Redirect = 1, 
    RedirectToLoginPage = 2, 
  }

  //public delegate void MailMessageEventHandler (Object sender, MailMessageEventArgs e);

  //public delegate void MenuEventHandler (Object sender, MenuEventArgs e);

#if NETFRAMEWORK_4_0
  public enum MenuRenderingMode
  {
    Default = 0, 
    Table = 1, 
    List = 2, 
  }
#endif

  //public delegate void MonthChangedEventHandler (Object sender, MonthChangedEventArgs e);

  public enum NextPrevFormat
  {
    CustomText = 0, 
    ShortMonth = 1, 
    FullMonth = 2, 
  }

  //public delegate void ObjectDataSourceDisposingEventHandler (Object sender, ObjectDataSourceDisposingEventArgs e);

  //public delegate void ObjectDataSourceFilteringEventHandler (Object sender, ObjectDataSourceFilteringEventArgs e);

  //public delegate void ObjectDataSourceMethodEventHandler (Object sender, ObjectDataSourceMethodEventArgs e);

  //public delegate void ObjectDataSourceObjectEventHandler (Object sender, ObjectDataSourceEventArgs e);

  //public delegate void ObjectDataSourceSelectingEventHandler (Object sender, ObjectDataSourceSelectingEventArgs e);

  //public delegate void ObjectDataSourceStatusEventHandler (Object sender, ObjectDataSourceStatusEventArgs e);

  public enum Orientation
  {
    Horizontal = 0, 
    Vertical = 1, 
  }

  public enum PagerButtons
  {
    NextPrevious = 0, 
    Numeric = 1, 
    NextPreviousFirstLast = 2, 
    NumericFirstLast = 3, 
  }

  public enum PagerMode
  {
    NextPrev = 0, 
    NumericPages = 1, 
  }

  public enum PagerPosition
  {
    Bottom = 0, 
    Top = 1, 
    TopAndBottom = 2, 
  }

  public enum PathDirection
  {
    RootToCurrent = 0, 
    CurrentToRoot = 1, 
  }

  public enum RepeatDirection
  {
    Horizontal = 0, 
    Vertical = 1, 
  }

  //public delegate void RepeaterCommandEventHandler (Object source, RepeaterCommandEventArgs e);

  //public delegate void RepeaterItemEventHandler (Object sender, RepeaterItemEventArgs e);

  public enum RepeatLayout
  {
    Table = 0, 
    Flow = 1,
#if NETFRAMEWORK_4_0
    UnorderedList = 2, 
    OrderedList = 3,
#endif
  }

  public enum ScrollBars
  {
    None = 0, 
    Horizontal = 1, 
    Vertical = 2, 
    Both = 3, 
    Auto = 4, 
  }

  //public delegate void SendMailErrorEventHandler (Object sender, SendMailErrorEventArgs e);

  //public delegate void ServerValidateEventHandler (Object source, ServerValidateEventArgs args);

  //public delegate void SiteMapNodeItemEventHandler (Object sender, SiteMapNodeItemEventArgs e);

  public enum SiteMapNodeItemType
  {
    Root = 0, 
    Parent = 1, 
    Current = 2, 
    PathSeparator = 3, 
  }

  public enum SortDirection
  {
    Ascending = 0, 
    Descending = 1, 
  }

  //public delegate void SqlDataSourceCommandEventHandler (Object sender, SqlDataSourceCommandEventArgs e);

  public enum SqlDataSourceCommandType
  {
    Text = 0, 
    StoredProcedure = 1, 
  }

  ///public delegate void SqlDataSourceFilteringEventHandler (Object sender, SqlDataSourceFilteringEventArgs e);

  public enum SqlDataSourceMode
  {
    DataReader = 0, 
    DataSet = 1, 
  }

  //public delegate void SqlDataSourceSelectingEventHandler (Object sender, SqlDataSourceSelectingEventArgs e);

  //public delegate void SqlDataSourceStatusEventHandler (Object sender, SqlDataSourceStatusEventArgs e);

  public enum TableCaptionAlign
  {
    NotSet = 0, 
    Top = 1, 
    Bottom = 2, 
    Left = 3, 
    Right = 4, 
  }

  public enum TableHeaderScope
  {
    NotSet = 0, 
    Row = 1, 
    Column = 2, 
  }

  public enum TableRowSection
  {
    TableHeader = 0, 
    TableBody = 1, 
    TableFooter = 2, 
  }

  public enum TextAlign
  {
    Left = 1, 
    Right = 2, 
  }

  public enum TextBoxMode
  {
    SingleLine = 0, 
    MultiLine = 1, 
    Password = 2, 
  }

  public enum TitleFormat
  {
    Month = 0, 
    MonthYear = 1, 
  }

  //public delegate void TreeNodeEventHandler (Object sender, TreeNodeEventArgs e);

  public enum TreeNodeSelectAction
  {
    Select = 0, 
    Expand = 1, 
    SelectExpand = 2, 
    None = 3, 
  }

  public enum TreeNodeTypes
  {
    None = 0, 
    Root = 1, 
    Parent = 2, 
    Leaf = 4, 
    All = 7, 
  }

  public enum TreeViewImageSet
  {
    Custom = 0, 
    XPFileExplorer = 1, 
    Msdn = 2, 
    WindowsHelp = 3, 
    Simple = 4, 
    Simple2 = 5, 
    BulletedList = 6, 
    BulletedList2 = 7, 
    BulletedList3 = 8, 
    BulletedList4 = 9, 
    Arrows = 10, 
    News = 11, 
    Contacts = 12, 
    Inbox = 13, 
    Events = 14, 
    Faq = 15, 
  }

  public enum UnitType
  {
    Pixel = 1, 
    Point = 2, 
    Pica = 3, 
    Inch = 4, 
    Mm = 5, 
    Cm = 6, 
    Percentage = 7, 
    Em = 8, 
    Ex = 9, 
  }

  public enum ValidationCompareOperator
  {
    Equal = 0, 
    NotEqual = 1, 
    GreaterThan = 2, 
    GreaterThanEqual = 3, 
    LessThan = 4, 
    LessThanEqual = 5, 
    DataTypeCheck = 6, 
  }

  public enum ValidationDataType
  {
    String = 0, 
    Integer = 1, 
    Double = 2, 
    Date = 3, 
    Currency = 4, 
  }

  public enum ValidationSummaryDisplayMode
  {
    List = 0, 
    BulletList = 1, 
    SingleParagraph = 2, 
  }

  public enum ValidatorDisplay
  {
    None = 0, 
    Static = 1, 
    Dynamic = 2, 
  }

  public enum VerticalAlign
  {
    NotSet = 0, 
    Top = 1, 
    Middle = 2, 
    Bottom = 3, 
  }

  //public delegate void WizardNavigationEventHandler (Object sender, WizardNavigationEventArgs e);

  public enum WizardStepType
  {
    Auto = 0, 
    Complete = 1, 
    Finish = 2, 
    Start = 3, 
    Step = 4, 
  }
}
