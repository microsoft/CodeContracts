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

// File System.Web.UI.WebControls.Login.cs
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
  public partial class Login : CompositeControl, System.Web.UI.IBorderPaddingControl, System.Web.UI.IRenderOuterTableControl
  {
    #region Methods and constructors
    protected internal override void CreateChildControls()
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    public Login()
    {
    }

    protected virtual new void OnAuthenticate(AuthenticateEventArgs e)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnLoggedIn(EventArgs e)
    {
    }

    protected virtual new void OnLoggingIn(LoginCancelEventArgs e)
    {
    }

    protected virtual new void OnLoginError(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
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

    public TableItemStyle CheckBoxStyle
    {
      get
      {
        return default(TableItemStyle);
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

    public virtual new string DestinationPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new bool DisplayRememberMe
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new LoginFailureAction FailureAction
    {
      get
      {
        return default(LoginFailureAction);
      }
      set
      {
      }
    }

    public virtual new string FailureText
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

    public virtual new System.Web.UI.ITemplate LayoutTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string LoginButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style LoginButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string LoginButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType LoginButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
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

    public virtual new Orientation Orientation
    {
      get
      {
        return default(Orientation);
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

    public virtual new bool RememberMeSet
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string RememberMeText
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

    public virtual new string TitleText
    {
      get
      {
        return default(string);
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

    public virtual new bool VisibleWhenLoggedIn
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event AuthenticateEventHandler Authenticate
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler LoggedIn
    {
      add
      {
      }
      remove
      {
      }
    }

    public event LoginCancelEventHandler LoggingIn
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler LoginError
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
    public readonly static string LoginButtonCommandName;
    #endregion
  }
}
