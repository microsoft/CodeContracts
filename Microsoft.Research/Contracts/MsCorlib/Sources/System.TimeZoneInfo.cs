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

// File System.TimeZoneInfo.cs
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
  sealed public partial class TimeZoneInfo : IEquatable<TimeZoneInfo>, System.Runtime.Serialization.ISerializable, System.Runtime.Serialization.IDeserializationCallback
  {
    #region Methods and constructors
    public static void ClearCachedData()
    {
    }

    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
    {
      return default(DateTime);
    }

    public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
    {
      return default(DateTimeOffset);
    }

    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      return default(DateTime);
    }

    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
    {
      return default(DateTime);
    }

    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
    {
      return default(DateTime);
    }

    public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
    {
      return default(DateTimeOffset);
    }

    public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      return default(DateTime);
    }

    public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
      return default(DateTime);
    }

    public static DateTime ConvertTimeToUtc(DateTime dateTime)
    {
      return default(DateTime);
    }

    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
    {
      Contract.Ensures(false);

      return default(TimeZoneInfo);
    }

    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
    {
      Contract.Ensures(false);

      return default(TimeZoneInfo);
    }

    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      Contract.Ensures(false);

      return default(TimeZoneInfo);
    }

    public bool Equals(TimeZoneInfo other)
    {
      return default(bool);
    }

    public static TimeZoneInfo FindSystemTimeZoneById(string id)
    {
      Contract.Ensures(Contract.Result<System.TimeZoneInfo>() != null);

      return default(TimeZoneInfo);
    }

    public static TimeZoneInfo FromSerializedString(string source)
    {
      return default(TimeZoneInfo);
    }

    public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
    {
      return default(TimeZoneInfo.AdjustmentRule[]);
    }

    public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
    {
      Contract.Ensures(Contract.Result<System.TimeSpan[]>() != null);

      return default(TimeSpan[]);
    }

    public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
    {
      Contract.Ensures(Contract.Result<System.TimeSpan[]>() != null);

      return default(TimeSpan[]);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public static System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
    {
      Contract.Ensures(Contract.Result<System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo>>() != null);

      return default(System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo>);
    }

    public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
    {
      return default(TimeSpan);
    }

    public TimeSpan GetUtcOffset(DateTime dateTime)
    {
      return default(TimeSpan);
    }

    public bool HasSameRules(TimeZoneInfo other)
    {
      return default(bool);
    }

    public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
    {
      return default(bool);
    }

    public bool IsAmbiguousTime(DateTime dateTime)
    {
      return default(bool);
    }

    public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
    {
      return default(bool);
    }

    public bool IsDaylightSavingTime(DateTime dateTime)
    {
      return default(bool);
    }

    public bool IsInvalidTime(DateTime dateTime)
    {
      return default(bool);
    }

    void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(Object sender)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }

    internal TimeZoneInfo()
    {
    }

    public string ToSerializedString()
    {
      return default(string);
    }

    public override string ToString()
    {
      return default(string);
    }
    #endregion

    #region Properties and indexers
    public TimeSpan BaseUtcOffset
    {
      get
      {
        return default(TimeSpan);
      }
    }

    public string DaylightName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public string DisplayName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public string Id
    {
      get
      {
        return default(string);
      }
    }

    public static System.TimeZoneInfo Local
    {
      get
      {
        Contract.Ensures(Contract.Result<System.TimeZoneInfo>() != null);

        return default(System.TimeZoneInfo);
      }
    }

    public string StandardName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return default(string);
      }
    }

    public bool SupportsDaylightSavingTime
    {
      get
      {
        return default(bool);
      }
    }

    public static System.TimeZoneInfo Utc
    {
      get
      {
        Contract.Ensures(Contract.Result<System.TimeZoneInfo>() != null);

        return default(System.TimeZoneInfo);
      }
    }
    #endregion
  }
}
