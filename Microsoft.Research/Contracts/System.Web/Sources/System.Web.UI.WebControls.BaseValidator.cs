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

// File System.Web.UI.WebControls.BaseValidator.cs
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
  abstract public partial class BaseValidator : Label, System.Web.UI.IValidator
  {
    #region Methods and constructors
    protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
    {
    }

    protected BaseValidator()
    {
    }

    protected void CheckControlValidationProperty(string name, string propertyName)
    {
    }

    protected virtual new bool ControlPropertiesValid()
    {
      return default(bool);
    }

    protected virtual new bool DetermineRenderUplevel()
    {
      return default(bool);
    }

    protected abstract bool EvaluateIsValid();

    protected string GetControlRenderID(string name)
    {
      return default(string);
    }

    protected string GetControlValidationValue(string name)
    {
      return default(string);
    }

    public static System.ComponentModel.PropertyDescriptor GetValidationProperty(Object component)
    {
      return default(System.ComponentModel.PropertyDescriptor);
    }

    protected internal override void OnInit(EventArgs e)
    {
    }

    protected internal override void OnPreRender(EventArgs e)
    {
    }

    protected internal override void OnUnload(EventArgs e)
    {
    }

    protected void RegisterValidatorCommonScript()
    {
    }

    protected virtual new void RegisterValidatorDeclaration()
    {
    }

    protected internal override void Render(System.Web.UI.HtmlTextWriter writer)
    {
    }

    public void Validate()
    {
    }
    #endregion

    #region Properties and indexers
    public override string AssociatedControlID
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string ControlToValidate
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public ValidatorDisplay Display
    {
      get
      {
        return default(ValidatorDisplay);
      }
      set
      {
      }
    }

    public bool EnableClientScript
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override bool Enabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public string ErrorMessage
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override System.Drawing.Color ForeColor
    {
      get
      {
        return default(System.Drawing.Color);
      }
      set
      {
      }
    }

    public bool IsValid
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    protected bool PropertiesValid
    {
      get
      {
        return default(bool);
      }
    }

    protected bool RenderUplevel
    {
      get
      {
        return default(bool);
      }
    }

    public bool SetFocusOnError
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public override string Text
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string ValidationGroup
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
  }
}
