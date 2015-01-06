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

#if NETFRAMEWORK_4_0
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Diagnostics.Contracts;

namespace System
{
  public sealed class TimeZoneInfo //: IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
  {
    private TimeZoneInfo() {}
    // Summary:
    //     Gets the time difference between the current time zone's standard time and
    //     Coordinated Universal Time (UTC).
    //
    // Returns:
    //     An object that indicates the time difference between the current time zone's
    //     standard time and Coordinated Universal Time (UTC).
    extern public TimeSpan BaseUtcOffset { get; }
    //
    // Summary:
    //     Gets the localized display name for the current time zone's daylight saving
    //     time.
    //
    // Returns:
    //     The display name for the time zone's localized daylight saving time.
    public string DaylightName { get { Contract.Ensures(Contract.Result<string>() != null); return default(string); } }
    //
    // Summary:
    //     Gets the localized general display name that represents the time zone.
    //
    // Returns:
    //     The time zone's localized general display name.
    public string DisplayName { get { Contract.Ensures(Contract.Result<string>() != null); return default(string); } }
    //
    // Summary:
    //     Gets the time zone identifier.
    //
    // Returns:
    //     The time zone identifier.
    public string Id { get { Contract.Ensures(Contract.Result<string>() != null); return default(string); } }
    //
    // Summary:
    //     Gets a System.TimeZoneInfo object that represents the local time zone.
    //
    // Returns:
    //     An object that represents the local time zone.
    public static TimeZoneInfo Local { get { Contract.Ensures(Contract.Result<TimeZoneInfo>() != null); return default(TimeZoneInfo); }}
    //
    // Summary:
    //     Gets the localized display name for the time zone's standard time.
    //
    // Returns:
    //     The localized display name of the time zone's standard time.
    public string StandardName { get { Contract.Ensures(Contract.Result<string>() != null); return default(string); } }
    //
    // Summary:
    //     Gets a value indicating whether the time zone has any daylight saving time
    //     rules.
    //
    // Returns:
    //     true if the time zone supports daylight saving time; otherwise, false.
    extern public bool SupportsDaylightSavingTime { get; }
    //
    // Summary:
    //     Gets a System.TimeZoneInfo object that represents the Coordinated Universal
    //     Time (UTC) zone.
    //
    // Returns:
    //     An object that represents the Coordinated Universal Time (UTC) zone.
    public static TimeZoneInfo Utc { get { Contract.Ensures(Contract.Result<TimeZoneInfo>() != null); return default(TimeZoneInfo); } }

    // Summary:
    //     Clears cached time zone data.
    extern public static void ClearCachedData();
    //
    // Summary:
    //     Converts a time to the time in a particular time zone.
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    //   destinationTimeZone:
    //     The time zone to convert dateTime to.
    //
    // Returns:
    //     The date and time in the destination time zone.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The value of the dateTime parameter represents an invalid time.
    //
    //   System.ArgumentNullException:
    //     The value of the destinationTimeZone parameter is null.
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      Contract.Requires(destinationTimeZone != null);

      return default(DateTime);
    }
    //
    // Summary:
    //     Converts a time to the time in a particular time zone.
    //
    // Parameters:
    //   dateTimeOffset:
    //     The date and time to convert.
    //
    //   destinationTimeZone:
    //     The time zone to convert dateTime to.
    //
    // Returns:
    //     The date and time in the destination time zone.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The value of the destinationTimeZone parameter is null.
    //public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone);
    //
    // Summary:
    //     Converts a time from one time zone to another.
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    //   sourceTimeZone:
    //     The time zone of dateTime.
    //
    //   destinationTimeZone:
    //     The time zone to convert dateTime to.
    //
    // Returns:
    //     The date and time in the destination time zone that corresponds to the dateTime
    //     parameter in the source time zone.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.DateTime.Kind property of the dateTime parameter is System.DateTimeKind.Local,
    //     but the sourceTimeZone parameter does not equal System.DateTimeKind.Local.-or-The
    //     System.DateTime.Kind property of the dateTime parameter is System.DateTimeKind.Utc,
    //     but the sourceTimeZone parameter does not equal System.TimeZoneInfo.Utc.-or-The
    //     dateTime parameter is an invalid time (that is, it represents a time that
    //     does not exist because of a time zone's adjustment rules).
    //
    //   System.ArgumentNullException:
    //     The sourceTimeZone parameter is null.-or-The destinationTimeZone parameter
    //     is null.
    public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
    {
      Contract.Requires(sourceTimeZone != null);
      Contract.Requires(destinationTimeZone != null);

      return default(DateTime);
    }
    //
    // Summary:
    //     Converts a time to the time in another time zone based on the time zone's
    //     identifier.
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    //   destinationTimeZoneId:
    //     The identifier of the destination time zone.
    //
    // Returns:
    //     The date and time in the destination time zone.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     destinationTimeZoneId is null.
    //
    //   System.InvalidTimeZoneException:
    //     The time zone identifier was found, but the registry data is corrupted.
    //
    //   System.Security.SecurityException:
    //     The process does not have the permissions required to read from the registry
    //     key that contains the time zone information.
    //
    //   System.TimeZoneNotFoundException:
    //     The destinationTimeZoneId identifier was not found on the local system.
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
    {
      Contract.Requires(destinationTimeZoneId != null);

      return default(DateTime);

    }
    //
    // Summary:
    //     Converts a time to the time in another time zone based on the time zone's
    //     identifier.
    //
    // Parameters:
    //   dateTimeOffset:
    //     The date and time to convert.
    //
    //   destinationTimeZoneId:
    //     The identifier of the destination time zone.
    //
    // Returns:
    //     The date and time in the destination time zone.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     destinationTimeZoneId is null.
    //
    //   System.InvalidTimeZoneException:
    //     The time zone identifier was found but the registry data is corrupted.
    //
    //   System.Security.SecurityException:
    //     The process does not have the permissions required to read from the registry
    //     key that contains the time zone information.
    //
    //   System.TimeZoneNotFoundException:
    //     The destinationTimeZoneId identifier was not found on the local system.
    //public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId);
    //
    // Summary:
    //     Converts a time from one time zone to another based on time zone identifiers.
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    //   sourceTimeZoneId:
    //     The identifier of the source time zone.
    //
    //   destinationTimeZoneId:
    //     The identifier of the destination time zone.
    //
    // Returns:
    //     The date and time in the destination time zone that corresponds to the dateTime
    //     parameter in the source time zone.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.DateTime.Kind property of the dateTime parameter does not correspond
    //     to the source time zone.-or-dateTime is an invalid time in the source time
    //     zone.
    //
    //   System.ArgumentNullException:
    //     sourceTimeZoneId is null.-or-destinationTimeZoneId is null.
    //
    //   System.InvalidTimeZoneException:
    //     The time zone identifiers were found, but the registry data is corrupted.
    //
    //   System.Security.SecurityException:
    //     The process does not have the permissions required to read from the registry
    //     key that contains the time zone information.
    //
    //   System.TimeZoneNotFoundException:
    //     The sourceTimeZoneId identifier was not found on the local system.-or-The
    //     destinationTimeZoneId identifier was not found on the local system.
    //
    //   System.Security.SecurityException:
    //     The user does not have the permissions required to read from the registry
    //     keys that hold time zone data.
    public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
    {
      Contract.Requires(sourceTimeZoneId != null);
      Contract.Requires(destinationTimeZoneId != null);
     
      return default(DateTime);

    }
    //
    // Summary:
    //     Converts a Coordinated Universal Time (UTC) to the time in a specified time
    //     zone.
    //
    // Parameters:
    //   dateTime:
    //     The Coordinated Universal Time (UTC).
    //
    //   destinationTimeZone:
    //     The time zone to convert dateTime to.
    //
    // Returns:
    //     The date and time in the destination time zone. Its System.DateTime.Kind
    //     property is System.DateTimeKind.Utc if destinationTimeZone is System.TimeZoneInfo.Utc;
    //     otherwise, its System.DateTime.Kind property is System.DateTimeKind.Unspecified.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.DateTime.Kind property of dateTime is System.DateTimeKind.Local.
    //
    //   System.ArgumentNullException:
    //     destinationTimeZone is null.
    public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
    {
      Contract.Requires(destinationTimeZone != null);

      return default(DateTime);

    }
    //
    // Summary:
    //     Converts the current date and time to Coordinated Universal Time (UTC).
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    // Returns:
    //     The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter.
    //     The System.DateTime value's System.DateTime.Kind property is always set to
    //     System.DateTimeKind.Utc.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     TimeZoneInfo.Local.IsInvalidDateTime(dateTime) returns true.
    public static DateTime ConvertTimeToUtc(DateTime dateTime)
    {
      Contract.Requires(!Local.IsInvalidTime(dateTime));
      Contract.Ensures(Contract.Result<DateTime>().Kind == DateTimeKind.Utc);

      return default(DateTime);
    }
    //
    // Summary:
    //     Converts the time in a specified time zone to Coordinated Universal Time
    //     (UTC).
    //
    // Parameters:
    //   dateTime:
    //     The date and time to convert.
    //
    //   sourceTimeZone:
    //     The time zone of dateTime.
    //
    // Returns:
    //     The Coordinated Universal Time (UTC) that corresponds to the dateTime parameter.
    //     The System.DateTime object's System.DateTime.Kind property is always set
    //     to System.DateTimeKind.Utc.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     dateTime.Kind is System.DateTimeKind.Utc and sourceTimeZone does not equal
    //     System.TimeZoneInfo.Utc.-or-dateTime.Kind is System.DateTimeKind.Local and
    //     sourceTimeZone does not equal System.TimeZoneInfo.Local.-or-sourceTimeZone.IsInvalidDateTime(dateTime)
    //     returns true.
    //
    //   System.ArgumentNullException:
    //     sourceTimeZone is null.
    public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
    {
      Contract.Requires(sourceTimeZone != null);

      return default(DateTime);

    }
    //
    // Summary:
    //     Creates a custom time zone with a specified identifier, an offset from Coordinated
    //     Universal Time (UTC), a display name, and a standard time display name.
    //
    // Parameters:
    //   id:
    //     The time zone's identifier.
    //
    //   baseUtcOffset:
    //     An object that represents the time difference between this time zone and
    //     Coordinated Universal Time (UTC).
    //
    //   displayName:
    //     The display name of the new time zone.
    //
    //   standardDisplayName:
    //     The name of the new time zone's standard time.
    //
    // Returns:
    //     The new time zone.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The id parameter is null.
    //
    //   System.ArgumentException:
    //     The id parameter is an empty string ("").-or-The baseUtcOffset parameter
    //     does not represent a whole number of minutes.
    //
    //   System.ArgumentOutOfRangeException:
    //     The baseUtcOffset parameter is greater than 14 hours or less than -14 hours.
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
    {
      Contract.Requires(!string.IsNullOrEmpty(id));

      return default(TimeZoneInfo);
    }
    //
    // Summary:
    //     Creates a custom time zone with a specified identifier, an offset from Coordinated
    //     Universal Time (UTC), a display name, a standard time name, a daylight saving
    //     time name, and daylight saving time rules.
    //
    // Parameters:
    //   id:
    //     The time zone's identifier.
    //
    //   baseUtcOffset:
    //     An object that represents the time difference between this time zone and
    //     Coordinated Universal Time (UTC).
    //
    //   displayName:
    //     The display name of the new time zone.
    //
    //   standardDisplayName:
    //     The new time zone's standard time name.
    //
    //   daylightDisplayName:
    //     The daylight saving time name of the new time zone.
    //
    //   adjustmentRules:
    //     An array that augments the base UTC offset for a particular period.
    //
    // Returns:
    //     A System.TimeZoneInfo object that represents the new time zone.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The id parameter is null.
    //
    //   System.ArgumentException:
    //     The id parameter is an empty string ("").-or-The baseUtcOffset parameter
    //     does not represent a whole number of minutes.
    //
    //   System.ArgumentOutOfRangeException:
    //     The baseUtcOffset parameter is greater than 14 hours or less than -14 hours.
    //
    //   System.InvalidTimeZoneException:
    //     The adjustment rules specified in the adjustmentRules parameter overlap.-or-The
    //     adjustment rules specified in the adjustmentRules parameter are not in chronological
    //     order.-or-One or more elements in adjustmentRules are null.-or-A date can
    //     have multiple adjustment rules applied to it.-or-The sum of the baseUtcOffset
    //     parameter and the System.TimeZoneInfo.AdjustmentRule.DaylightDelta value
    //     of one or more objects in the adjustmentRules array is greater than 14 hours
    //     or less than -14 hours.
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
    {
      Contract.Requires(!string.IsNullOrEmpty(id));

      return default(TimeZoneInfo);
    }
    //
    // Summary:
    //     Creates a custom time zone with a specified identifier, an offset from Coordinated
    //     Universal Time (UTC), a display name, a standard time name, a daylight saving
    //     time name, daylight saving time rules, and a value that indicates whether
    //     the returned object reflects daylight saving time information.
    //
    // Parameters:
    //   id:
    //     The time zone's identifier.
    //
    //   baseUtcOffset:
    //     A System.TimeSpan object that represents the time difference between this
    //     time zone and Coordinated Universal Time (UTC).
    //
    //   displayName:
    //     The display name of the new time zone.
    //
    //   standardDisplayName:
    //     The standard time name of the new time zone.
    //
    //   daylightDisplayName:
    //     The daylight saving time name of the new time zone.
    //
    //   adjustmentRules:
    //     An array of System.TimeZoneInfo.AdjustmentRule objects that augment the base
    //     UTC offset for a particular period.
    //
    //   disableDaylightSavingTime:
    //     true to discard any daylight saving time-related information present in adjustmentRules
    //     with the new object; otherwise, false.
    //
    // Returns:
    //     The new time zone. If the disableDaylightSavingTime parameter is true, the
    //     returned object has no daylight saving time data.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The id parameter is null.
    //
    //   System.ArgumentException:
    //     The id parameter is an empty string ("").-or-The baseUtcOffset parameter
    //     does not represent a whole number of minutes.
    //
    //   System.ArgumentOutOfRangeException:
    //     The baseUtcOffset parameter is greater than 14 hours or less than -14 hours.
    //
    //   System.InvalidTimeZoneException:
    //     The adjustment rules specified in the adjustmentRules parameter overlap.-or-The
    //     adjustment rules specified in the adjustmentRules parameter are not in chronological
    //     order.-or-One or more elements in adjustmentRules are null.-or-A date can
    //     have multiple adjustment rules applied to it.-or-The sum of the baseUtcOffset
    //     parameter and the System.TimeZoneInfo.AdjustmentRule.DaylightDelta value
    //     of one or more objects in the adjustmentRules array is greater than 14 hours
    //     or less than -14 hours.
    public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
    {
      Contract.Requires(!string.IsNullOrEmpty(id));

      return default(TimeZoneInfo);
    }
    //
    // Summary:
    //     Determines whether the current System.TimeZoneInfo object and another System.TimeZoneInfo
    //     object are equal.
    //
    // Parameters:
    //   other:
    //     A second object to compare with the current object.
    //
    // Returns:
    //     true if the two System.TimeZoneInfo objects are equal; otherwise, false.
    //extern public bool Equals(TimeZoneInfo other);
    //
    // Summary:
    //     Retrieves a System.TimeZoneInfo object from the registry based on its identifier.
    //
    // Parameters:
    //   id:
    //     The time zone identifier, which corresponds to the System.TimeZoneInfo.Id
    //     property.
    //
    // Returns:
    //     An object whose identifier is the value of the id parameter.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     The system does not have enough memory to hold information about the time
    //     zone.
    //
    //   System.ArgumentNullException:
    //     The id parameter is null.
    //
    //   System.TimeZoneNotFoundException:
    //     The time zone identifier specified by id was not found. This means that a
    //     registry key whose name matches id does not exist, or that the key exists
    //     but does not contain any time zone data.
    //
    //   System.Security.SecurityException:
    //     The process does not have the permissions required to read from the registry
    //     key that contains the time zone information.
    //
    //   System.InvalidTimeZoneException:
    //     The time zone identifier was found, but the registry data is corrupted.
    public static TimeZoneInfo FindSystemTimeZoneById(string id)
    {
      Contract.Requires(id != null);

      return default(TimeZoneInfo);
    }

    //
    // Summary:
    //     Deserializes a string to re-create an original serialized System.TimeZoneInfo
    //     object.
    //
    // Parameters:
    //   source:
    //     The string representation of the serialized System.TimeZoneInfo object.
    //
    // Returns:
    //     The original serialized object.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The source parameter is System.String.Empty.
    //
    //   System.ArgumentNullException:
    //     The source parameter is a null string.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The source parameter cannot be deserialized back into a System.TimeZoneInfo
    //     object.
    public static TimeZoneInfo FromSerializedString(string source)
    {
      Contract.Requires(!string.IsNullOrEmpty(source));

      return default(TimeZoneInfo);
    }
    //
    // Summary:
    //     Retrieves an array of System.TimeZoneInfo.AdjustmentRule objects that apply
    //     to the current System.TimeZoneInfo object.
    //
    // Returns:
    //     An array of objects for this time zone.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     The system does not have enough memory to make an in-memory copy of the adjustment
    //     rules.
    public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
    {
      Contract.Ensures(Contract.Result<TimeZoneInfo.AdjustmentRule[]>() != null);

      return null;
    }
    //
    // Summary:
    //     Returns information about the possible dates and times that an ambiguous
    //     date and time can be mapped to.
    //
    // Parameters:
    //   dateTime:
    //     A date and time.
    //
    // Returns:
    //     An array of objects that represents possible Coordinated Universal Time (UTC)
    //     offsets that a particular date and time can be mapped to.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     dateTime is not an ambiguous time.
    public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
    {
      Contract.Ensures(Contract.Result<TimeSpan[]>() != null);

      return null;
    }
    //
    // Summary:
    //     Returns information about the possible dates and times that an ambiguous
    //     date and time can be mapped to.
    //
    // Parameters:
    //   dateTimeOffset:
    //     A date and time.
    //
    // Returns:
    //     An array of objects that represents possible Coordinated Universal Time (UTC)
    //     offsets that a particular date and time can be mapped to.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     dateTime is not an ambiguous time.
    //public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset);
    //
    // Summary:
    //     Serves as a hash function for hashing algorithms and data structures such
    //     as hash tables.
    //
    // Returns:
    //     A 32-bit signed integer that serves as the hash code for this System.TimeZoneInfo
    //     object.
    extern public override int GetHashCode();
    //
    // Summary:
    //     Returns a sorted collection of all the time zones about which information
    //     is available on the local system.
    //
    // Returns:
    //     A read-only collection of System.TimeZoneInfo objects.
    //
    // Exceptions:
    //   System.OutOfMemoryException:
    //     There is insufficient memory to store all time zone information.
    //
    //   System.Security.SecurityException:
    //     The user does not have permission to read from the registry keys that contain
    //     time zone information.
    public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
    {
      Contract.Ensures(Contract.Result<ReadOnlyCollection<TimeZoneInfo>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<ReadOnlyCollection<TimeZoneInfo>>(), el => el != null));

      return null;
    }
    //
    // Summary:
    //     Calculates the offset or difference between the time in this time zone and
    //     Coordinated Universal Time (UTC) for a particular date and time.
    //
    // Parameters:
    //   dateTime:
    //     The date and time to determine the offset for.
    //
    // Returns:
    //     An object that indicates the time difference between the two time zones.
    public TimeSpan GetUtcOffset(DateTime dateTime)
    {
      return default(TimeSpan);
    }
    //
    // Summary:
    //     Calculates the offset or difference between the time in this time zone and
    //     Coordinated Universal Time (UTC) for a particular date and time.
    //
    // Parameters:
    //   dateTimeOffset:
    //     The date and time to determine the offset for.
    //
    // Returns:
    //     An object that indicates the time difference between Coordinated Universal
    //     Time (UTC) and the current time zone.
    //public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset);
    //
    // Summary:
    //     Indicates whether the current object and another System.TimeZoneInfo object
    //     have the same adjustment rules.
    //
    // Parameters:
    //   other:
    //     A second object to compare with the current System.TimeZoneInfo object.
    //
    // Returns:
    //     true if the two time zones have identical adjustment rules and an identical
    //     base offset; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The other parameter is null.
    public bool HasSameRules(TimeZoneInfo other)
    {
      Contract.Requires(other != null);

      return false;
    }
    //
    // Summary:
    //     Determines whether a particular date and time in a particular time zone is
    //     ambiguous and can be mapped to two or more Coordinated Universal Time (UTC)
    //     times.
    //
    // Parameters:
    //   dateTime:
    //     A date and time value.
    //
    // Returns:
    //     true if the dateTime parameter is ambiguous; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.DateTime.Kind property of the dateTime value is System.DateTimeKind.Local
    //     and dateTime is an invalid time.
    extern public bool IsAmbiguousTime(DateTime dateTime);
    //
    // Summary:
    //     Determines whether a particular date and time in a particular time zone is
    //     ambiguous and can be mapped to two or more Coordinated Universal Time (UTC)
    //     times.
    //
    // Parameters:
    //   dateTimeOffset:
    //     A date and time.
    //
    // Returns:
    //     true if the dateTimeOffset parameter is ambiguous in the current time zone;
    //     otherwise, false.
    //public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset);
    //
    // Summary:
    //     Indicates whether a specified date and time falls in the range of daylight
    //     saving time for the time zone of the current System.TimeZoneInfo object.
    //
    // Parameters:
    //   dateTime:
    //     A date and time value.
    //
    // Returns:
    //     true if the dateTime parameter is a daylight saving time; otherwise, false.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The System.DateTime.Kind property of the dateTime value is System.DateTimeKind.Local
    //     and dateTime is an invalid time.
    //public bool IsDaylightSavingTime(DateTime dateTime);
    //
    // Summary:
    //     Indicates whether a specified date and time falls in the range of daylight
    //     saving time for the time zone of the current System.TimeZoneInfo object.
    //
    // Parameters:
    //   dateTimeOffset:
    //     A date and time value.
    //
    // Returns:
    //     true if the dateTimeOffset parameter is a daylight saving time; otherwise,
    //     false.
    //public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset);
    //
    // Summary:
    //     Indicates whether a particular date and time is invalid.
    //
    // Parameters:
    //   dateTime:
    //     A date and time value.
    //
    // Returns:
    //     true if dateTime is invalid; otherwise, false.
    extern public bool IsInvalidTime(DateTime dateTime);
    //
    // Summary:
    //     Converts the current System.TimeZoneInfo object to a serialized string.
    //
    // Returns:
    //     A string that represents the current System.TimeZoneInfo object.
    public string ToSerializedString()
    {
      Contract.Ensures(Contract.Result<string>() != null);

      return null;
    }
    //
    // Summary:
    //     Returns the current System.TimeZoneInfo object's display name.
    //
    // Returns:
    //     The value of the System.TimeZoneInfo.DisplayName property of the current
    //     System.TimeZoneInfo object.
    extern public override string ToString();

    // Summary:
    //     Provides information about a time zone adjustment, such as the transition
    //     to and from daylight saving time.
    //[Serializable]
    //[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    public sealed class AdjustmentRule //: IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
    {
      private AdjustmentRule() { }

      // Summary:
      //     Gets the date when the adjustment rule ceases to be in effect.
      //
      // Returns:
      //     A System.DateTime value that indicates the end date of the adjustment rule.
      extern public DateTime DateEnd { get; }
      //
      // Summary:
      //     Gets the date when the adjustment rule takes effect.
      //
      // Returns:
      //     A System.DateTime value that indicates when the adjustment rule takes effect.
      extern public DateTime DateStart { get; }
      //
      // Summary:
      //     Gets the amount of time that is required to form the time zone's daylight
      //     saving time. This amount of time is added to the time zone's offset from
      //     Coordinated Universal Time (UTC).
      //
      // Returns:
      //     A System.TimeSpan object that indicates the amount of time to add to the
      //     standard time changes as a result of the adjustment rule.
      extern public TimeSpan DaylightDelta { get; }
      //
      // Summary:
      //     Gets information about the annual transition from daylight saving time back
      //     to standard time.
      //
      // Returns:
      //     A System.TimeZoneInfo.TransitionTime object that defines the annual transition
      //     from daylight saving time back to the time zone's standard time.
      extern public TimeZoneInfo.TransitionTime DaylightTransitionEnd { get; }
      //
      // Summary:
      //     Gets information about the annual transition from standard time to daylight
      //     saving time.
      //
      // Returns:
      //     A System.TimeZoneInfo.TransitionTime object that defines the annual transition
      //     from a time zone's standard time to daylight saving time.
      //public TimeZoneInfo.TransitionTime DaylightTransitionStart { get; }

      // Summary:
      //     Creates a new adjustment rule for a particular time zone.
      //
      // Parameters:
      //   dateStart:
      //     The effective date of the adjustment rule. If the value of the dateStart
      //     parameter is DateTime.MinValue.Date, this is the first adjustment rule in
      //     effect for a time zone.
      //
      //   dateEnd:
      //     The last date that the adjustment rule is in force. If the value of the dateEnd
      //     parameter is DateTime.MaxValue.Date, the adjustment rule has no end date.
      //
      //   daylightDelta:
      //     The time change that results from the adjustment. This value is added to
      //     the time zone's System.TimeZoneInfo.BaseUtcOffset property to obtain the
      //     correct daylight offset from Coordinated Universal Time (UTC). This value
      //     can range from -14 to 14.
      //
      //   daylightTransitionStart:
      //     A System.TimeZoneInfo.TransitionTime object that defines the start of daylight
      //     saving time.
      //
      //   daylightTransitionEnd:
      //     A System.TimeZoneInfo.TransitionTime object that defines the end of daylight
      //     saving time.
      //
      // Returns:
      //     A System.TimeZoneInfo.AdjustmentRule object that represents the new adjustment
      //     rule.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The System.DateTime.Kind property of the dateStart or dateEnd parameter does
      //     not equal System.DateTimeKind.Unspecified.-or-The daylightTransitionStart
      //     parameter is equal to the daylightTransitionEnd parameter.-or-The dateStart
      //     or dateEnd parameter includes a time of day value.
      //
      //   System.ArgumentOutOfRangeException:
      //     dateEnd is earlier than dateStart.-or-daylightDelta is less than -14 or greater
      //     than 14.-or-The System.TimeSpan.Milliseconds property of the daylightDelta
      //     parameter is not equal to 0.-or-The System.TimeSpan.Ticks property of the
      //     daylightDelta parameter does not equal a whole number of seconds.
      extern public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd);
      //
      // Summary:
      //     Determines whether the current System.TimeZoneInfo.AdjustmentRule object
      //     is equal to a second System.TimeZoneInfo.AdjustmentRule object.
      //
      // Parameters:
      //   other:
      //     A second System.TimeZoneInfo.AdjustmentRule object.
      //
      // Returns:
      //     true if both System.TimeZoneInfo.AdjustmentRule objects have equal values;
      //     otherwise, false.
      //extern public bool Equals(TimeZoneInfo.AdjustmentRule other);
      //
      // Summary:
      //     Serves as a hash function for hashing algorithms and data structures such
      //     as hash tables.
      //
      // Returns:
      //     A 32-bit signed integer that serves as the hash code for the current System.TimeZoneInfo.AdjustmentRule
      //     object.
      extern public override int GetHashCode();
    }

    // Summary:
    //     Provides information about a specific time change, such as the change from
    //     daylight saving time to standard time or vice versa, in a particular time
    //     zone.
    //[Serializable]
    //[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
    public struct TransitionTime //: IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
    {

      // Summary:
      //     Determines whether two specified System.TimeZoneInfo.TransitionTime objects
      //     are not equal.
      //
      // Parameters:
      //   t1:
      //     The first object to compare.
      //
      //   t2:
      //     The second object to compare.
      //
      // Returns:
      //     true if t1 and t2 have any different member values; otherwise, false.
      extern public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2);
      //
      // Summary:
      //     Determines whether two specified System.TimeZoneInfo.TransitionTime objects
      //     are equal.
      //
      // Parameters:
      //   t1:
      //     The first object to compare.
      //
      //   t2:
      //     The second object to compare.
      //
      // Returns:
      //     true if t1 and t2 have identical values; otherwise, false.
      extern public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2);

      // Summary:
      //     Gets the day on which the time change occurs.
      //
      // Returns:
      //     The day on which the time change occurs.
      public int Day
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);
          Contract.Ensures(Contract.Result<int>() <= 31);

          return 0;
        }
      }

      //
      // Summary:
      //     Gets the day of the week on which the time change occurs.
      //
      // Returns:
      //     The day of the week on which the time change occurs.
      //extern public DayOfWeek DayOfWeek { get; }
      //
      // Summary:
      //     Gets a value indicating whether the time change occurs at a fixed date and
      //     time (such as November 1) or a floating date and time (such as the last Sunday
      //     of October).
      //
      // Returns:
      //     true if the time change rule is fixed-date; false if the time change rule
      //     is floating-date.
      //extern public bool IsFixedDateRule { get; }
      //
      // Summary:
      //     Gets the month in which the time change occurs.
      //
      // Returns:
      //     The month in which the time change occurs.
      public int Month
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);
          Contract.Ensures(Contract.Result<int>() <= 12);

          return 0;
        }
      }

      //
      // Summary:
      //     Gets the hour, minute, and second at which the time change occurs.
      //
      // Returns:
      //     The time of day at which the time change occurs.
      //extern public DateTime TimeOfDay { get; }
      //
      // Summary:
      //     Gets the week of the month in which a time change occurs.
      //
      // Returns:
      //     The week of the month in which the time change occurs.
      public int Week
      {
        get
        {
          Contract.Ensures(Contract.Result<int>() >= 0);
          Contract.Ensures(Contract.Result<int>() <= 52);

          return 0;
        }
      }

      // Summary:
      //     Defines a time change that uses a fixed-date rule.
      //
      // Parameters:
      //   timeOfDay:
      //     The time at which the time change occurs.
      //
      //   month:
      //     The month in which the time change occurs.
      //
      //   day:
      //     The day of the month on which the time change occurs.
      //
      // Returns:
      //     Data about the time change.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The timeOfDay parameter has a non-default date component.-or-The timeOfDay
      //     parameter's System.DateTime.Kind property is not System.DateTimeKind.Unspecified.-or-The
      //     timeOfDay parameter does not represent a whole number of milliseconds.
      //
      //   System.ArgumentOutOfRangeException:
      //     The month parameter is less than 1 or greater than 12.-or-The day parameter
      //     is less than 1 or greater than 31.
      extern public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day);
      //
      // Summary:
      //     Defines a time change that uses a floating-date rule.
      //
      // Parameters:
      //   timeOfDay:
      //     The time at which the time change occurs.
      //
      //   month:
      //     The month in which the time change occurs.
      //
      //   week:
      //     The week of the month in which the time change occurs.
      //
      //   dayOfWeek:
      //     The day of the week on which the time change occurs.
      //
      // Returns:
      //     Data about the time change.
      //
      // Exceptions:
      //   System.ArgumentException:
      //     The timeOfDay parameter has a non-default date component.-or-The timeOfDay
      //     parameter does not represent a whole number of milliseconds.-or-The timeOfDay
      //     parameter's System.DateTime.Kind property is not System.DateTimeKind.Unspecified.
      //
      //   System.ArgumentOutOfRangeException:
      //     month is less than 1 or greater than 12.-or-week is less than 1 or greater
      //     than 5.-or-The dayOfWeek parameter is not a member of the System.DayOfWeek
      //     enumeration.
      extern public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek);
      //
      // Summary:
      //     Determines whether an object has identical values to the current System.TimeZoneInfo.TransitionTime
      //     object.
      //
      // Parameters:
      //   obj:
      //     An object to compare with the current System.TimeZoneInfo.TransitionTime
      //     object.
      //
      // Returns:
      //     true if the two objects are equal; otherwise, false.
      //extern public override bool Equals(object obj);
      //
      // Summary:
      //     Determines whether the current System.TimeZoneInfo.TransitionTime object
      //     has identical values to a second System.TimeZoneInfo.TransitionTime object.
      //
      // Parameters:
      //   other:
      //     An object to compare to the current instance.
      //
      // Returns:
      //     true if the two objects have identical property values; otherwise, false.
      //extern public bool Equals(TimeZoneInfo.TransitionTime other);
      //
      // Summary:
      //     Serves as a hash function for hashing algorithms and data structures such
      //     as hash tables.
      //
      // Returns:
      //     A 32-bit signed integer that serves as the hash code for this System.TimeZoneInfo.TransitionTime
      //     object.
      extern public override int GetHashCode();
    }
  }
}
#endif