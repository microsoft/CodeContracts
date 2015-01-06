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

// File System.Web.UI.WebControls.ChangePassword.cs
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
  public partial class ChangePassword : CompositeControl, System.Web.UI.IBorderPaddingControl, System.Web.UI.INamingContainer, System.Web.UI.IRenderOuterTableControl
  {
    #region Methods and constructors
    public ChangePassword()
    {
    }

    protected internal override void CreateChildControls()
    {
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnCancelButtonClick(EventArgs e)
    {
    }

    protected virtual new void OnChangedPassword(EventArgs e)
    {
    }

    protected virtual new void OnChangePasswordError(EventArgs e)
    {
    }

    protected virtual new void OnChangingPassword(LoginCancelEventArgs e)
    {
    }

    protected virtual new void OnContinueButtonClick(EventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected virtual new void OnSendingMail(MailMessageEventArgs e)
    {
    }

    protected virtual new void OnSendMailError(SendMailErrorEventArgs e)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected internal override Object SaveControlState()
    {
      return default(Object);
    }

    protected override Object SaveViewState()
    {
      return default(Object);
    }

    protected override void SetDesignModeState(System.Collections.IDictionary data)
    {
    }

    protected override void TrackViewState()
    {
    }
    #endregion

    #region Properties and indexers
    public virtual new int BorderPadding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new string CancelButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style CancelButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string CancelButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType CancelButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new string CancelDestinationPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ChangePasswordButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style ChangePasswordButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string ChangePasswordButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType ChangePasswordButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new string ChangePasswordFailureText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate ChangePasswordTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public System.Web.UI.Control ChangePasswordTemplateContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public virtual new string ChangePasswordTitleText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ConfirmNewPassword
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string ConfirmNewPasswordLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ConfirmPasswordCompareErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ConfirmPasswordRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ContinueButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style ContinueButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string ContinueButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType ContinueButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new string ContinueDestinationPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CreateUserIconUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CreateUserText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CreateUserUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string CurrentPassword
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool DisplayUserName
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string EditProfileIconUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EditProfileText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EditProfileUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle FailureTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string HelpPageIconUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string HelpPageText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string HelpPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle HyperLinkStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string InstructionText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle InstructionTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public TableItemStyle LabelStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public MailDefinition MailDefinition
    {
      get
      {
        return default(MailDefinition);
      }
    }

    public virtual new string MembershipProvider
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string NewPassword
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string NewPasswordLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string NewPasswordRegularExpression
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string NewPasswordRegularExpressionErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string NewPasswordRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle PasswordHintStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string PasswordHintText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordRecoveryIconUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordRecoveryText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordRecoveryUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool RenderOuterTable
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string SuccessPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate SuccessTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public System.Web.UI.Control SuccessTemplateContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public virtual new string SuccessText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle SuccessTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string SuccessTitleText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    protected override System.Web.UI.HtmlTextWriterTag TagKey
    {
      get
      {
        return default(System.Web.UI.HtmlTextWriterTag);
      }
    }

    public Style TextBoxStyle
    {
      get
      {
        return default(Style);
      }
    }

    public TableItemStyle TitleTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string UserName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string UserNameLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string UserNameRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style ValidatorTextStyle
    {
      get
      {
        return default(Style);
      }
    }
    #endregion

    #region Events
    public event EventHandler CancelButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ChangedPassword
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ChangePasswordError
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoginCancelEventHandler ChangingPassword
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler ContinueButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event MailMessageEventHandler SendingMail
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SendMailErrorEventHandler SendMailError
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static string CancelButtonCommandName;
    public readonly static string ChangePasswordButtonCommandName;
    public readonly static string ContinueButtonCommandName;
    #endregion
  }
}
