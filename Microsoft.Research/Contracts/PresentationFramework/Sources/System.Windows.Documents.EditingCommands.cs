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

// File System.Windows.Documents.EditingCommands.cs
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
  static public partial class EditingCommands
  {
    #region Properties and indexers
    public static System.Windows.Input.RoutedUICommand AlignCenter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand AlignJustify
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand AlignLeft
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand AlignRight
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand Backspace
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand CorrectSpellingError
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand DecreaseFontSize
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand DecreaseIndentation
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand Delete
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand DeleteNextWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand DeletePreviousWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand EnterLineBreak
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand EnterParagraphBreak
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand IgnoreSpellingError
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand IncreaseFontSize
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand IncreaseIndentation
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveDownByLine
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveDownByPage
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveDownByParagraph
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveLeftByCharacter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveLeftByWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveRightByCharacter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveRightByWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveToDocumentEnd
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveToDocumentStart
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveToLineEnd
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveToLineStart
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveUpByLine
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveUpByPage
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand MoveUpByParagraph
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectDownByLine
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectDownByPage
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectDownByParagraph
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectLeftByCharacter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectLeftByWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectRightByCharacter
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectRightByWord
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectToDocumentEnd
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectToDocumentStart
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectToLineEnd
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectToLineStart
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectUpByLine
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectUpByPage
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand SelectUpByParagraph
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand TabBackward
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand TabForward
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleBold
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleBullets
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleInsert
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleItalic
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleNumbering
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleSubscript
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleSuperscript
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }

    public static System.Windows.Input.RoutedUICommand ToggleUnderline
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.Input.RoutedUICommand>() != null);

        return default(System.Windows.Input.RoutedUICommand);
      }
    }
    #endregion
  }
}
