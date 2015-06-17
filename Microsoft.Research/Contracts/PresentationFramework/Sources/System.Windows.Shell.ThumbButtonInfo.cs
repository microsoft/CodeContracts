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

// File System.Windows.Shell.ThumbButtonInfo.cs
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


namespace System.Windows.Shell
{
  sealed public partial class ThumbButtonInfo : System.Windows.Freezable, System.Windows.Input.ICommandSource
  {
    #region Methods and constructors
    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public ThumbButtonInfo()
    {
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Input.ICommand Command
    {
      get
      {
        return default(System.Windows.Input.ICommand);
      }
      set
      {
      }
    }

    public Object CommandParameter
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public System.Windows.IInputElement CommandTarget
    {
      get
      {
        return default(System.Windows.IInputElement);
      }
      set
      {
      }
    }

    public string Description
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool DismissWhenClicked
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Media.ImageSource ImageSource
    {
      get
      {
        return default(System.Windows.Media.ImageSource);
      }
      set
      {
      }
    }

    public bool IsBackgroundVisible
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IsInteractive
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Visibility Visibility
    {
      get
      {
        return default(System.Windows.Visibility);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler Click
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
    public readonly static System.Windows.DependencyProperty CommandParameterProperty;
    public readonly static System.Windows.DependencyProperty CommandProperty;
    public readonly static System.Windows.DependencyProperty CommandTargetProperty;
    public readonly static System.Windows.DependencyProperty DescriptionProperty;
    public readonly static System.Windows.DependencyProperty DismissWhenClickedProperty;
    public readonly static System.Windows.DependencyProperty ImageSourceProperty;
    public readonly static System.Windows.DependencyProperty IsBackgroundVisibleProperty;
    public readonly static System.Windows.DependencyProperty IsEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsInteractiveProperty;
    public readonly static System.Windows.DependencyProperty VisibilityProperty;
    #endregion
  }
}
