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

// File System.Windows.Documents.Hyperlink.cs
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


namespace System.Windows.Documents
{
  public partial class Hyperlink : Span, System.Windows.Input.ICommandSource, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    public void DoClick()
    {
    }

    public Hyperlink(TextPointer start, TextPointer end)
    {
    }

    public Hyperlink()
    {
    }

    public Hyperlink(Inline childInline)
    {
    }

    public Hyperlink(Inline childInline, TextPointer insertionPosition)
    {
    }

    protected virtual new void OnClick()
    {
    }

    protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
    {
    }

    protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e)
    {
    }
    #endregion

    #region Properties and indexers
    protected virtual new Uri BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

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

    protected override bool IsEnabledCore
    {
      get
      {
        return default(bool);
      }
    }

    public Uri NavigateUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    Uri System.Windows.Markup.IUriContext.BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public string TargetName
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

    #region Events
    public event System.Windows.RoutedEventHandler Click
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Navigation.RequestNavigateEventHandler RequestNavigate
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
    public readonly static System.Windows.RoutedEvent ClickEvent;
    public readonly static System.Windows.DependencyProperty CommandParameterProperty;
    public readonly static System.Windows.DependencyProperty CommandProperty;
    public readonly static System.Windows.DependencyProperty CommandTargetProperty;
    public readonly static System.Windows.DependencyProperty NavigateUriProperty;
    public readonly static System.Windows.RoutedEvent RequestNavigateEvent;
    public readonly static System.Windows.DependencyProperty TargetNameProperty;
    #endregion
  }
}
