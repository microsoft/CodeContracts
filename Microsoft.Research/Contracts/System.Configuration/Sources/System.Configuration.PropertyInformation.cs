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

// File System.Configuration.PropertyInformation.cs
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


namespace System.Configuration
{
  sealed public partial class PropertyInformation
  {
    #region Methods and constructors
    internal PropertyInformation()
    {
    }
    #endregion

    #region Properties and indexers
    public System.ComponentModel.TypeConverter Converter
    {
      get
      {
        return default(System.ComponentModel.TypeConverter);
      }
    }

    public Object DefaultValue
    {
      get
      {
        return default(Object);
      }
    }

    public string Description
    {
      get
      {
        return default(string);
      }
    }

    public bool IsKey
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLocked
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsModified
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsRequired
    {
      get
      {
        return default(bool);
      }
    }

    public int LineNumber
    {
      get
      {
        return default(int);
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
    }

    public string Source
    {
      get
      {
        return default(string);
      }
    }

    public Type Type
    {
      get
      {
        return default(Type);
      }
    }

    public ConfigurationValidatorBase Validator
    {
      get
      {
        return default(ConfigurationValidatorBase);
      }
    }

    public Object Value
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public PropertyValueOrigin ValueOrigin
    {
      get
      {
        Contract.Ensures(((System.Configuration.PropertyValueOrigin)(0)) <= Contract.Result<System.Configuration.PropertyValueOrigin>());
        Contract.Ensures(Contract.Result<System.Configuration.PropertyValueOrigin>() <= ((System.Configuration.PropertyValueOrigin)(2)));

        return default(PropertyValueOrigin);
      }
    }
    #endregion
  }
}
