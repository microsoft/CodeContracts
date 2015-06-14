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

// File System.Web.UI.WebControls.CreateUserWizard.cs
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
  public partial class CreateUserWizard : Wizard
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    public CreateUserWizard()
    {
    }

    protected override System.Collections.IDictionary GetDesignModeState()
    {
      return default(System.Collections.IDictionary);
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnContinueButtonClick(EventArgs e)
    {
    }

    protected virtual new void OnCreatedUser(EventArgs e)
    {
    }

    protected virtual new void OnCreateUserError(CreateUserErrorEventArgs e)
    {
    }

    protected virtual new void OnCreatingUser(LoginCancelEventArgs e)
    {
    }

    protected override void OnNextButtonClick(WizardNavigationEventArgs e)
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
    public override int ActiveStepIndex
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new string Answer
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string AnswerLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string AnswerRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool AutoGeneratePassword
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public CompleteWizardStep CompleteStep
    {
      get
      {
        return default(CompleteWizardStep);
      }
    }

    public virtual new string CompleteSuccessText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle CompleteSuccessTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string ConfirmPassword
    {
      get
      {
        return default(string);
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

    public virtual new string ConfirmPasswordLabelText
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

    public virtual new string CreateUserButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style CreateUserButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string CreateUserButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType CreateUserButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public CreateUserWizardStep CreateUserStep
    {
      get
      {
        return default(CreateUserWizardStep);
      }
    }

    public virtual new bool DisableCreatedUser
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool DisplaySideBar
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string DuplicateEmailErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string DuplicateUserNameErrorMessage
    {
      get
      {
        return default(string);
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

    public virtual new string Email
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EmailLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EmailRegularExpression
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EmailRegularExpressionErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string EmailRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle ErrorMessageStyle
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

    public virtual new string InvalidAnswerErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string InvalidEmailErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string InvalidPasswordErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string InvalidQuestionErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public TableItemStyle LabelStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new bool LoginCreatedUser
    {
      get
      {
        return default(bool);
      }
      set
      {
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

    public virtual new string Password
    {
      get
      {
        return default(string);
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

    public virtual new string PasswordRegularExpression
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string PasswordRegularExpressionErrorMessage
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

    public virtual new string Question
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    internal protected bool QuestionAndAnswerRequired
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string QuestionLabelText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string QuestionRequiredErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool RequireEmail
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override string SkipLinkText
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public virtual new string UnknownErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public override WizardStepCollection WizardSteps
    {
      get
      {
        return default(WizardStepCollection);
      }
    }
    #endregion

    #region Events
    public event EventHandler ContinueButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CreatedUser
    {
      add
      {
      }
      remove
      {
      }
    }

    public event CreateUserErrorEventHandler CreateUserError
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoginCancelEventHandler CreatingUser
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
    public readonly static string ContinueButtonCommandName;
    #endregion
  }
}
