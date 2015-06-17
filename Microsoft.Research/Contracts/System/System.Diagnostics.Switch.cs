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

#if !SILVERLIGHT
using System;
using System.Runtime;

namespace System.Diagnostics
{
  // Summary:
  //     Provides an abstract base class to create new debugging and tracing switches.
  public abstract class Switch
  {
    protected Switch(string displayName, string description) { }

    protected Switch(string displayName, string description, string defaultSwitchValue) { }

    //public StringDictionary Attributes { get; }
    //
    // Summary:
    //     Gets a description of the switch.
    //
    // Returns:
    //     The description of the switch. The default value is an empty string ("").
    // public string Description { get; }
    //
    // Summary:
    //     Gets a name used to identify the switch.
    //
    // Returns:
    //     The name used to identify the switch. The default value is an empty string
    //     ("").
    //public string DisplayName { get; }
    //
    // Summary:
    //     Gets or sets the current setting for this switch.
    //
    // Returns:
    //     The current setting for this switch. The default is zero.
    //protected int SwitchSetting { get; set; }
    //
    // Summary:
    //     Gets or sets the value of the switch.
    //
    // Returns:
    //     A string representing the value of the switch.
    //
    // Exceptions:
    //   System.Configuration.ConfigurationErrorsException:
    //     The value is null.-or-The value does not consist solely of an optional negative
    //     sign followed by a sequence of digits ranging from 0 to 9.-or-The value represents
    //     a number less than System.Int32.MinValue or greater than System.Int32.MaxValue.
    //protected string Value { get; set; }

    // Summary:
    //     Gets the custom attributes supported by the switch.
    //
    // Returns:
    //     A string array that contains the names of the custom attributes supported
    //     by the switch, or null if there no custom attributes are supported.
    //protected internal virtual string[] GetSupportedAttributes();
    //
    // Summary:
    //     Invoked when the System.Diagnostics.Switch.SwitchSetting property is changed.
    //protected virtual void OnSwitchSettingChanged();
    //
    // Summary:
    //     Invoked when the System.Diagnostics.Switch.Value property is changed.
    //protected virtual void OnValueChanged();
  }
}
#endif