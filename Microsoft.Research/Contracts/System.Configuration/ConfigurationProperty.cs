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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace System.Configuration
{
  public sealed class ConfigurationProperty
  {
    //public ConfigurationProperty(string name, Type type)
    //public ConfigurationProperty(string name, Type type, object defaultValue);
    //public ConfigurationProperty(string name, Type type, object defaultValue, ConfigurationPropertyOptions options);
    //public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options);
    //public ConfigurationProperty(string name, Type type, object defaultValue, TypeConverter typeConverter, ConfigurationValidatorBase validator, ConfigurationPropertyOptions options, string description);

    // Properties

    public TypeConverter Converter
    {
      get
      {
        Contract.Ensures(Contract.Result<TypeConverter>() != null);
        return default(TypeConverter);
      }
    }
    // public object DefaultValue { get; }
    // public string Description { get; }
    // public bool IsDefaultCollection { get; }
    // public bool IsKey { get; }
    // public bool IsRequired { get; }
    public string Name
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);
        return default(string);
      }
    }

    // public ConfigurationValidatorBase Validator { get; }

  }
}
