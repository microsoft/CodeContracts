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

// File System.ComponentModel.MaskedTextProvider.cs
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


namespace System.ComponentModel
{
  public partial class MaskedTextProvider : ICloneable
  {
    #region Methods and constructors
    public bool Add(char input)
    {
      return default(bool);
    }

    public bool Add(char input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-54)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Add(string input)
    {
      return default(bool);
    }

    public bool Add(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-54)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public void Clear()
    {
    }

    public void Clear(out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(2)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      resultHint = default(MaskedTextResultHint);
    }

    public Object Clone()
    {
      return default(Object);
    }

    public int FindAssignedEditPositionFrom(int position, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindAssignedEditPositionInRange(int startPosition, int endPosition, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindEditPositionFrom(int position, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindEditPositionInRange(int startPosition, int endPosition, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindNonEditPositionFrom(int position, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindNonEditPositionInRange(int startPosition, int endPosition, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindUnassignedEditPositionFrom(int position, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public int FindUnassignedEditPositionInRange(int startPosition, int endPosition, bool direction)
    {
      Contract.Ensures(-1 <= Contract.Result<int>());

      return default(int);
    }

    public static bool GetOperationResultFromHint(MaskedTextResultHint hint)
    {
      Contract.Ensures(Contract.Result<bool>() == (hint > ((System.ComponentModel.MaskedTextResultHint)(0))));

      return default(bool);
    }

    public bool InsertAt(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool InsertAt(char input, int position)
    {
      return default(bool);
    }

    public bool InsertAt(string input, int position)
    {
      return default(bool);
    }

    public bool InsertAt(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
    {
      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool IsAvailablePosition(int position)
    {
      return default(bool);
    }

    public bool IsEditPosition(int position)
    {
      return default(bool);
    }

    public static bool IsValidInputChar(char c)
    {
      return default(bool);
    }

    public static bool IsValidMaskChar(char c)
    {
      return default(bool);
    }

    public static bool IsValidPasswordChar(char c)
    {
      return default(bool);
    }

    public MaskedTextProvider(string mask, System.Globalization.CultureInfo culture, bool restrictToAscii)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask, System.Globalization.CultureInfo culture)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask, char passwordChar, bool allowPromptAsInput)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask, System.Globalization.CultureInfo culture, bool allowPromptAsInput, char promptChar, char passwordChar, bool restrictToAscii)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask, System.Globalization.CultureInfo culture, char passwordChar, bool allowPromptAsInput)
    {
      Contract.Requires(mask != null);
    }

    public MaskedTextProvider(string mask, bool restrictToAscii)
    {
      Contract.Requires(mask != null);
    }

    public bool Remove(out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(2)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(0 <= Contract.ValueAtReturn(out testPosition));
      Contract.Ensures(Contract.Result<bool>() == true);
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Remove()
    {
      Contract.Ensures(Contract.Result<bool>() == true);

      return default(bool);
    }

    public bool RemoveAt(int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool RemoveAt(int startPosition, int endPosition)
    {
      return default(bool);
    }

    public bool RemoveAt(int position)
    {
      return default(bool);
    }

    public bool Replace(char input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Replace(char input, int position, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Replace(char input, int position)
    {
      return default(bool);
    }

    public bool Replace(string input, int startPosition, int endPosition, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Replace(string input, int position, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Replace(string input, int position)
    {
      return default(bool);
    }

    public bool Set(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-54)) <= Contract.ValueAtReturn(out resultHint));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool Set(string input)
    {
      return default(bool);
    }

    public string ToDisplayString()
    {
      return default(string);
    }

    public string ToString(bool ignorePasswordChar)
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }

    public string ToString(bool ignorePasswordChar, bool includePrompt, bool includeLiterals, int startPosition, int length)
    {
      return default(string);
    }

    public string ToString(int startPosition, int length)
    {
      return default(string);
    }

    public string ToString(bool includePrompt, bool includeLiterals, int startPosition, int length)
    {
      return default(string);
    }

    public string ToString(bool ignorePasswordChar, int startPosition, int length)
    {
      return default(string);
    }

    public string ToString(bool includePrompt, bool includeLiterals)
    {
      return default(string);
    }

    public bool VerifyChar(char input, int position, out MaskedTextResultHint hint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-55)) <= Contract.ValueAtReturn(out hint));
      Contract.Ensures(Contract.ValueAtReturn(out hint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      hint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool VerifyEscapeChar(char input, int position)
    {
      return default(bool);
    }

    public bool VerifyString(string input, out int testPosition, out MaskedTextResultHint resultHint)
    {
      Contract.Ensures(((System.ComponentModel.MaskedTextResultHint)(-54)) <= Contract.ValueAtReturn(out resultHint));
      Contract.Ensures(Contract.ValueAtReturn(out resultHint) <= ((System.ComponentModel.MaskedTextResultHint)(4)));

      testPosition = default(int);
      resultHint = default(MaskedTextResultHint);

      return default(bool);
    }

    public bool VerifyString(string input)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public bool AllowPromptAsInput
    {
      get
      {
        return default(bool);
      }
    }

    public bool AsciiOnly
    {
      get
      {
        return default(bool);
      }
    }

    public int AssignedEditPositionCount
    {
      get
      {
        return default(int);
      }
    }

    public int AvailableEditPositionCount
    {
      get
      {
        return default(int);
      }
    }

    public System.Globalization.CultureInfo Culture
    {
      get
      {
        return default(System.Globalization.CultureInfo);
      }
    }

    public static char DefaultPasswordChar
    {
      get
      {
        Contract.Ensures(Contract.Result<char>() == 42);

        return default(char);
      }
    }

    public int EditPositionCount
    {
      get
      {
        return default(int);
      }
    }

    public System.Collections.IEnumerator EditPositions
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public bool IncludeLiterals
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool IncludePrompt
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public static int InvalidIndex
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() == -(1));
        Contract.Ensures(Contract.Result<int>() == -1);

        return default(int);
      }
    }

    public bool IsPassword
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public char this [int index]
    {
      get
      {
        return default(char);
      }
    }

    public int LastAssignedPosition
    {
      get
      {
        Contract.Ensures(-1 <= Contract.Result<int>());

        return default(int);
      }
    }

    public int Length
    {
      get
      {
        return default(int);
      }
    }

    public string Mask
    {
      get
      {
        return default(string);
      }
    }

    public bool MaskCompleted
    {
      get
      {
        return default(bool);
      }
    }

    public bool MaskFull
    {
      get
      {
        return default(bool);
      }
    }

    public char PasswordChar
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public char PromptChar
    {
      get
      {
        return default(char);
      }
      set
      {
      }
    }

    public bool ResetOnPrompt
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool ResetOnSpace
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public bool SkipLiterals
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
  }
}
