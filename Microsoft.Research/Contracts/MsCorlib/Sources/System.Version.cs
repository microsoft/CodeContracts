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

// File System.Version.cs
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
  sealed public partial class Version : ICloneable, IComparable, IComparable<Version>, IEquatable<Version>
  {
    #region Methods and constructors
    public static bool operator != (System.Version v1, System.Version v2)
    {
      Contract.Ensures(Contract.Result<bool>() == ((v1.Equals(v2)) == false));

      return default(bool);
    }

    public static bool operator < (System.Version v1, System.Version v2)
    {
      return default(bool);
    }

    public static bool operator <=(System.Version v1, System.Version v2)
    {
      return default(bool);
    }

    public static bool operator == (System.Version v1, System.Version v2)
    {
      return default(bool);
    }

    public static bool operator > (System.Version v1, System.Version v2)
    {
      return default(bool);
    }

    public static bool operator >= (System.Version v1, System.Version v2)
    {
      return default(bool);
    }

    public Object Clone()
    {
      return default(Object);
    }

    public int CompareTo(Object version)
    {
      return default(int);
    }

    public int CompareTo(System.Version value)
    {
      return default(int);
    }

    public override bool Equals(Object obj)
    {
      return default(bool);
    }

    public bool Equals(System.Version obj)
    {
      return default(bool);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT_3_0 && !NETFRAMEWORK_3_5
    public static System.Version Parse(string input)
    {
      return default(System.Version);
    }
#endif
    public override string ToString()
    {
      return default(string);
    }

    public string ToString(int fieldCount)
    {
      Contract.Requires(fieldCount >= 0);
      Contract.Requires(fieldCount <= 4);
      Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()) || fieldCount == 0);
      return default(string);
    }

#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT_3_0 && !NETFRAMEWORK_3_5
    public static bool TryParse(string input, out System.Version result)
    {
      result = default(System.Version);

      return default(bool);
    }
#endif

    public Version(int major, int minor, int build, int revision)
    {
    }

    public Version(int major, int minor, int build)
    {
    }

#if SILVERLIGHT
    internal
#else
    public
#endif
    Version()
    {
    }

    public Version(string version)
    {
    }

    public Version(int major, int minor)
    {
    }
    #endregion

    #region Properties and indexers
    public int Build
    {
      get
      {
        return default(int);
      }
    }

    public int Major
    {
      get
      {
        return default(int);
      }
    }

#if NETFRAMEWORK_4_0
    public short MajorRevision
    {
      get
      {
        return default(short);
      }
    }

    public short MinorRevision
    {
      get
      {
        Contract.Ensures(0 <= Contract.Result<short>());

        return default(short);
      }
    }

#endif

    public int Minor
    {
      get
      {
        return default(int);
      }
    }
    
    public int Revision
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
