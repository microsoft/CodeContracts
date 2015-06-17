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

// File System.Configuration.ConfigurationErrorsException.cs
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
  public partial class ConfigurationErrorsException : ConfigurationException
  {
    #region Methods and constructors
    public ConfigurationErrorsException(string message, System.Xml.XmlReader reader)
    {
    }

    public ConfigurationErrorsException(string message, Exception inner, System.Xml.XmlNode node)
    {
    }

    protected ConfigurationErrorsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(info != null);
    }

    public ConfigurationErrorsException(string message, Exception inner, System.Xml.XmlReader reader)
    {
    }

    public ConfigurationErrorsException(string message, System.Xml.XmlNode node)
    {
    }

    public ConfigurationErrorsException()
    {
    }

    public ConfigurationErrorsException(string message, Exception inner, string filename, int line)
    {
    }

    public ConfigurationErrorsException(string message)
    {
    }

    public ConfigurationErrorsException(string message, string filename, int line)
    {
      Contract.Ensures(0 <= filename.Length);
    }

    public ConfigurationErrorsException(string message, Exception inner)
    {
    }

    public static string GetFilename(System.Xml.XmlReader reader)
    {
      return default(string);
    }

    public static string GetFilename(System.Xml.XmlNode node)
    {
      return default(string);
    }

    public static int GetLineNumber(System.Xml.XmlNode node)
    {
      return default(int);
    }

    public static int GetLineNumber(System.Xml.XmlReader reader)
    {
      return default(int);
    }

    public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }
    #endregion

    #region Properties and indexers
    public override string BareMessage
    {
      get
      {
        return default(string);
      }
    }

    public System.Collections.ICollection Errors
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Collections.ICollection>() != null);

        return default(System.Collections.ICollection);
      }
    }

    public override string Filename
    {
      get
      {
        return default(string);
      }
    }

    public override int Line
    {
      get
      {
        return default(int);
      }
    }

    public override string Message
    {
      get
      {
        return default(string);
      }
    }
    #endregion
  }
}
