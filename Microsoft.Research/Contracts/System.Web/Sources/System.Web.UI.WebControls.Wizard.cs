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

// File System.Web.UI.WebControls.Wizard.cs
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
  public partial class Wizard : CompositeControl
  {
    #region Methods and constructors
    protected virtual new bool AllowNavigationToStep(int index)
    {
      return default(bool);
    }

    protected internal override void CreateChildControls()
    {
    }

    protected override System.Web.UI.ControlCollection CreateControlCollection()
    {
      return default(System.Web.UI.ControlCollection);
    }

    protected virtual new void CreateControlHierarchy()
    {
    }

    protected override Style CreateControlStyle()
    {
      return default(Style);
    }

    protected override System.Collections.IDictionary GetDesignModeState()
    {
      return default(System.Collections.IDictionary);
    }

    public System.Collections.ICollection GetHistory()
    {
      return default(System.Collections.ICollection);
    }

    public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
    {
      return default(WizardStepType);
    }

    protected internal override void LoadControlState(Object state)
    {
    }

    protected override void LoadViewState(Object savedState)
    {
    }

    public void MoveTo(WizardStepBase wizardStep)
    {
    }

    protected virtual new void OnActiveStepChanged(Object source, EventArgs e)
    {
    }

    protected override bool OnBubbleEvent(Object source, EventArgs e)
    {
      return default(bool);
    }

    protected virtual new void OnCancelButtonClick(EventArgs e)
    {
    }

    protected virtual new void OnFinishButtonClick(WizardNavigationEventArgs e)
    {
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected virtual new void OnNextButtonClick(WizardNavigationEventArgs e)
    {
    }

    protected virtual new void OnPreviousButtonClick(WizardNavigationEventArgs e)
    {
    }

    protected virtual new void OnSideBarButtonClick(WizardNavigationEventArgs e)
    {
    }

    protected internal void RegisterCommandEvents(IButtonControl button)
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

    protected override void TrackViewState()
    {
    }

    public Wizard()
    {
    }
    #endregion

    #region Properties and indexers
    public WizardStepBase ActiveStep
    {
      get
      {
        return default(WizardStepBase);
      }
    }

    public virtual new int ActiveStepIndex
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

    public virtual new int CellPadding
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new int CellSpacing
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public virtual new bool DisplayCancelButton
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new bool DisplaySideBar
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public virtual new string FinishCompleteButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style FinishCompleteButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string FinishCompleteButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType FinishCompleteButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new string FinishDestinationPageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate FinishNavigationTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string FinishPreviousButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style FinishPreviousButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string FinishPreviousButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType FinishPreviousButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public TableItemStyle HeaderStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new System.Web.UI.ITemplate HeaderTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string HeaderText
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public Style NavigationButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public TableItemStyle NavigationStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public Style SideBarButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public TableItemStyle SideBarStyle
    {
      get
      {
        return default(TableItemStyle);
      }
    }

    public virtual new System.Web.UI.ITemplate SideBarTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string SkipLinkText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate StartNavigationTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string StartNextButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style StartNextButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string StartNextButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType StartNextButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new System.Web.UI.ITemplate StepNavigationTemplate
    {
      get
      {
        return default(System.Web.UI.ITemplate);
      }
      set
      {
      }
    }

    public virtual new string StepNextButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style StepNextButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string StepNextButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType StepNextButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public virtual new string StepPreviousButtonImageUrl
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Style StepPreviousButtonStyle
    {
      get
      {
        return default(Style);
      }
    }

    public virtual new string StepPreviousButtonText
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new ButtonType StepPreviousButtonType
    {
      get
      {
        return default(ButtonType);
      }
      set
      {
      }
    }

    public TableItemStyle StepStyle
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

    public virtual new WizardStepCollection WizardSteps
    {
      get
      {
        return default(WizardStepCollection);
      }
    }
    #endregion

    #region Events
    public event EventHandler ActiveStepChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler CancelButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WizardNavigationEventHandler FinishButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WizardNavigationEventHandler NextButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public event WizardNavigationEventHandler PreviousButtonClick
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event WizardNavigationEventHandler SideBarButtonClick
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
    protected readonly static string CancelButtonID;
    public readonly static string CancelCommandName;
    protected readonly static string CustomFinishButtonID;
    protected readonly static string CustomNextButtonID;
    protected readonly static string CustomPreviousButtonID;
    protected readonly static string DataListID;
    protected readonly static string FinishButtonID;
    protected readonly static string FinishPreviousButtonID;
    public readonly static string HeaderPlaceholderId;
    public readonly static string MoveCompleteCommandName;
    public readonly static string MoveNextCommandName;
    public readonly static string MovePreviousCommandName;
    public readonly static string MoveToCommandName;
    public readonly static string NavigationPlaceholderId;
    protected readonly static string SideBarButtonID;
    public readonly static string SideBarPlaceholderId;
    protected readonly static string StartNextButtonID;
    protected readonly static string StepNextButtonID;
    protected readonly static string StepPreviousButtonID;
    public readonly static string WizardStepPlaceholderId;
    #endregion
  }
}
