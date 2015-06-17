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

// File System.TimeZoneInfo.TransitionTime.cs
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


namespace System
{
  sealed public partial class TimeZoneInfo
  {
    public partial struct TransitionTime : IEquatable<TransitionTime>, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
    {
      #region Methods and constructors
      public static bool operator != (TransitionTime t1, TransitionTime t2)
      {
        return default(bool);
      }

      public static bool operator == (TransitionTime t1, TransitionTime t2)
      {
        return default(bool);
      }

      public static TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
      {
        Contract.Ensures((timeOfDay.Year - day) <= 0);
        Contract.Ensures((timeOfDay.Year - month) <= 0);
        Contract.Ensures(timeOfDay.Day == 1);
        Contract.Ensures(timeOfDay.Kind == ((System.DateTimeKind)(0)));
        Contract.Ensures(timeOfDay.Month == 1);
        Contract.Ensures(timeOfDay.Year == 1);

        return default(TransitionTime);
      }

      public static TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
      {
        Contract.Ensures((timeOfDay.Year - month) <= 0);
        Contract.Ensures((timeOfDay.Year - week) <= 0);
        Contract.Ensures(timeOfDay.Day == 1);
        Contract.Ensures(timeOfDay.Kind == ((System.DateTimeKind)(0)));
        Contract.Ensures(timeOfDay.Month == 1);
        Contract.Ensures(timeOfDay.Year == 1);

        return default(TransitionTime);
      }

      public bool Equals(TransitionTime other)
      {
        return default(bool);
      }

      public override bool Equals(Object obj)
      {
        return default(bool);
      }

      public override int GetHashCode()
      {
        return default(int);
      }

      void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
      {
      }

      void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
      {
      }
      #endregion

      #region Properties and indexers
      public int Day
      {
        get
        {
          Contract.Ensures(0 <= Contract.Result<int>());
          Contract.Ensures(Contract.Result<int>() <= 255);

          return default(int);
        }
      }

      public DayOfWeek DayOfWeek
      {
        get
        {
          return default(DayOfWeek);
        }
      }

      public bool IsFixedDateRule
      {
        get
        {
          return default(bool);
        }
      }

      public int Month
      {
        get
        {
          Contract.Ensures(0 <= Contract.Result<int>());
          Contract.Ensures(Contract.Result<int>() <= 255);

          return default(int);
        }
      }

      public DateTime TimeOfDay
      {
        get
        {
          return default(DateTime);
        }
      }

      public int Week
      {
        get
        {
          Contract.Ensures(0 <= Contract.Result<int>());
          Contract.Ensures(Contract.Result<int>() <= 255);

          return default(int);
        }
      }
      #endregion
    }
  }
}
