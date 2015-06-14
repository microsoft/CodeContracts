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
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.CodeTools;
using System.IO;

namespace Microsoft.Contracts.VisualStudio
{
  abstract class ProjectProperty
  {
    /// <summary>
    /// The value of this string is used in the .targets files to set options
    /// for the command line.
    /// </summary>
    protected readonly string propertyName;
    protected ProjectProperty(string propertyName)
    {
      this.propertyName = propertyName;
    }

    public abstract void Save(string[] configNames, IPropertyStorage storage);
    public abstract void Load(string[] configNames, IPropertyStorage storage);
  }

  class CheckBoxProperty : ProjectProperty
  {
    readonly CheckBox box;
    readonly bool defaultValue;

    public CheckBoxProperty(string propertyName, CheckBox box, bool defaultValue) :
      base(propertyName)
    {
      this.box = box;
      this.defaultValue = defaultValue;
    }

    public override void Save(string[] configNames, IPropertyStorage storage)
    {
      if (box.CheckState != CheckState.Indeterminate)
      {
        storage.SetProperties(false, configNames, propertyName, box.Checked);
        // TODO: avoid saving default values, once removing works
#if false
        if (box.Checked != defaultValue)
        {
          storage.SetProperties(false, configNames, propertyName, box.Checked);
        }
        else
        {
          // don't clutter csproj file
          foreach (var config in configNames)
          {
            storage.RemoveProperty(false, config, propertyName);
          }
        }
#endif
      }
    }

    CheckState CheckStateOfBool(bool checkedBool)
    {
      if (checkedBool) { return CheckState.Checked; }
      return CheckState.Unchecked;
    }

    public override void Load(string[] configNames, IPropertyStorage storage)
    {
      object result = storage.GetProperties(false, configNames, propertyName, defaultValue);
      if (result != null)
      {
        box.CheckState = CheckStateOfBool((bool)result);
      }
      else
      {
        box.CheckState = CheckState.Indeterminate;
      }
    }
  }

  class NegatedCheckBoxProperty : ProjectProperty
  {
    readonly CheckBox box;
    readonly bool defaultValue;

    public NegatedCheckBoxProperty(string propertyName, CheckBox box, bool defaultValue) :
      base(propertyName)
    {
      this.box = box;
      this.defaultValue = defaultValue;
    }

    public override void Save(string[] configNames, IPropertyStorage storage)
    {
      if (box.CheckState != CheckState.Indeterminate)
      {
        storage.SetProperties(false, configNames, propertyName, !box.Checked);
#if false
        if (box.Checked != defaultValue)
        {
          storage.SetProperties(false, configNames, propertyName, !box.Checked);
        }
        else
        {
          // don't clutter csproj file
          foreach (var config in configNames)
          {
            storage.RemoveProperty(false, config, propertyName);
          }
        }
#endif
      }
    }

    CheckState CheckStateOfBool(bool checkedBool)
    {
      if (checkedBool) { return CheckState.Checked; }
      return CheckState.Unchecked;
    }

    public override void Load(string[] configNames, IPropertyStorage storage)
    {
      object result = storage.GetProperties(false, configNames, propertyName, !defaultValue);
      if (result != null)
      {
        box.CheckState = CheckStateOfBool(!(bool)result);
      }
      else
      {
        box.CheckState = CheckState.Indeterminate;
      }
    }
  }

  class TextBoxProperty : ProjectProperty
  {
    readonly TextBox box;
    readonly string defaultValue;

    public TextBoxProperty(string propertyName, TextBox box, string defaultValue)
      : base(propertyName)
    {
      this.box = box;
      this.defaultValue = defaultValue;
    }

    public override void Save(string[] configNames, IPropertyStorage storage)
    {
      if (box.Text != null)
      {
        storage.SetProperties(false, configNames, propertyName, box.Text);
#if false
        if (box.Text != defaultValue)
        {
          storage.SetProperties(false, configNames, propertyName, box.Text);
        }
        else
        {
          // don't clutter csproj file
          foreach (var config in configNames)
          {
            storage.RemoveProperty(false, config, propertyName);
          }
        }
#endif
      }
    }

    public override void Load(string[] configNames, IPropertyStorage storage)
    {
      string result = storage.GetProperties(false, configNames, propertyName, defaultValue) as string;
      box.Text = result;
    }
  }
}