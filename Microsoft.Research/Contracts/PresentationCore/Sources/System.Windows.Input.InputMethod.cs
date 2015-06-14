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

// File System.Windows.Input.InputMethod.cs
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


namespace System.Windows.Input
{
  public partial class InputMethod : System.Windows.Threading.DispatcherObject
  {
    #region Methods and constructors
    public static InputScope GetInputScope(System.Windows.DependencyObject target)
    {
      return default(InputScope);
    }

    public static bool GetIsInputMethodEnabled(System.Windows.DependencyObject target)
    {
      return default(bool);
    }

    public static bool GetIsInputMethodSuspended(System.Windows.DependencyObject target)
    {
      return default(bool);
    }

    public static ImeConversionModeValues GetPreferredImeConversionMode(System.Windows.DependencyObject target)
    {
      return default(ImeConversionModeValues);
    }

    public static ImeSentenceModeValues GetPreferredImeSentenceMode(System.Windows.DependencyObject target)
    {
      return default(ImeSentenceModeValues);
    }

    public static InputMethodState GetPreferredImeState(System.Windows.DependencyObject target)
    {
      return default(InputMethodState);
    }

    internal InputMethod()
    {
    }

    public static void SetInputScope(System.Windows.DependencyObject target, InputScope value)
    {
    }

    public static void SetIsInputMethodEnabled(System.Windows.DependencyObject target, bool value)
    {
    }

    public static void SetIsInputMethodSuspended(System.Windows.DependencyObject target, bool value)
    {
    }

    public static void SetPreferredImeConversionMode(System.Windows.DependencyObject target, ImeConversionModeValues value)
    {
    }

    public static void SetPreferredImeSentenceMode(System.Windows.DependencyObject target, ImeSentenceModeValues value)
    {
    }

    public static void SetPreferredImeState(System.Windows.DependencyObject target, InputMethodState value)
    {
    }

    public void ShowConfigureUI()
    {
    }

    public void ShowConfigureUI(System.Windows.UIElement element)
    {
    }

    public void ShowRegisterWordUI(System.Windows.UIElement element, string registeredText)
    {
    }

    public void ShowRegisterWordUI(string registeredText)
    {
    }

    public void ShowRegisterWordUI()
    {
    }
    #endregion

    #region Properties and indexers
    public bool CanShowConfigurationUI
    {
      get
      {
        return default(bool);
      }
    }

    public bool CanShowRegisterWordUI
    {
      get
      {
        return default(bool);
      }
    }

    public static System.Windows.Input.InputMethod Current
    {
      get
      {
        return default(System.Windows.Input.InputMethod);
      }
    }

    public InputMethodState HandwritingState
    {
      get
      {
        return default(InputMethodState);
      }
      set
      {
      }
    }

    public ImeConversionModeValues ImeConversionMode
    {
      get
      {
        return default(ImeConversionModeValues);
      }
      set
      {
      }
    }

    public ImeSentenceModeValues ImeSentenceMode
    {
      get
      {
        return default(ImeSentenceModeValues);
      }
      set
      {
      }
    }

    public InputMethodState ImeState
    {
      get
      {
        return default(InputMethodState);
      }
      set
      {
      }
    }

    public InputMethodState MicrophoneState
    {
      get
      {
        return default(InputMethodState);
      }
      set
      {
      }
    }

    public SpeechMode SpeechMode
    {
      get
      {
        return default(SpeechMode);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event InputMethodStateChangedEventHandler StateChanged
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
    public readonly static System.Windows.DependencyProperty InputScopeProperty;
    public readonly static System.Windows.DependencyProperty IsInputMethodEnabledProperty;
    public readonly static System.Windows.DependencyProperty IsInputMethodSuspendedProperty;
    public readonly static System.Windows.DependencyProperty PreferredImeConversionModeProperty;
    public readonly static System.Windows.DependencyProperty PreferredImeSentenceModeProperty;
    public readonly static System.Windows.DependencyProperty PreferredImeStateProperty;
    #endregion
  }
}
