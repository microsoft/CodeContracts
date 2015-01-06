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

// File System.Web.UI.WebControls.PasswordRecovery.cs
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
  public partial class PasswordRecovery : CompositeControl, System.Web.UI.IBorderPaddingControl, System.Web.UI.IRenderOuterTableControl
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    protected internal override void LoadControlState(Object savedState)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    protected virtual new void OnAnswerLookupError(EventArgs e)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
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

    protected virtual new void OnUserLookupError(EventArgs e)
    {
    }

    protected virtual new void OnVerifyingAnswer(LoginCancelEventArgs e)
    {
    }

    protected virtual new void OnVerifyingUser(LoginCancelEventArgs e)
    {
    }

    public PasswordRecovery()
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
    public virtual new string Answer
    {
      get
      {
        return default(string);
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

    public TableItemStyle FailureTextStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new string GeneralFailureText
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public virtual new string Question
    {
      get
      {
        return default(string);
      }
      private set
      {
      }
    }

    public virtual new string QuestionFailureText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string QuestionInstructionText
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public virtual new System.Web.UI.ITemplate QuestionTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public System.Web.UI.Control QuestionTemplateContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public virtual new string QuestionTitleText
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

    public virtual new string SubmitButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style SubmitButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string SubmitButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType SubmitButtonType
    {
      get
      {
        return default(ButtonType);
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

    public virtual new LoginTextLayout TextLayout
    {
      get
      {
        return default(LoginTextLayout);
      }
      set
      {
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

    public virtual new string UserNameFailureText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string UserNameInstructionText
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

    public virtual new System.Web.UI.ITemplate UserNameTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public System.Web.UI.Control UserNameTemplateContainer
    {
      get
      {
        return default(System.Web.UI.Control);
      }
    }

    public virtual new string UserNameTitleText
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
    public event EventHandler AnswerLookupError
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

    public event EventHandler UserLookupError
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoginCancelEventHandler VerifyingAnswer
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoginCancelEventHandler VerifyingUser
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
    public readonly static string SubmitButtonCommandName;
    #endregion
  }
}
