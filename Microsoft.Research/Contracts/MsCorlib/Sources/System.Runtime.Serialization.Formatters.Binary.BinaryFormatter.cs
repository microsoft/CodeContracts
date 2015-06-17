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

// File System.Runtime.Serialization.Formatters.Binary.BinaryFormatter.cs
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


namespace System.Runtime.Serialization.Formatters.Binary
{
  sealed public partial class BinaryFormatter : System.Runtime.Remoting.Messaging.IRemotingFormatter, System.Runtime.Serialization.IFormatter
  {
    #region Methods and constructors
    public BinaryFormatter()
    {
    }

    public BinaryFormatter(System.Runtime.Serialization.ISurrogateSelector selector, System.Runtime.Serialization.StreamingContext context)
    {
    }

    public Object Deserialize(Stream serializationStream)
    {
      return default(Object);
    }

    public Object Deserialize(Stream serializationStream, System.Runtime.Remoting.Messaging.HeaderHandler handler)
    {
      return default(Object);
    }

    public Object DeserializeMethodResponse(Stream serializationStream, System.Runtime.Remoting.Messaging.HeaderHandler handler, System.Runtime.Remoting.Messaging.IMethodCallMessage methodCallMessage)
    {
      Contract.Ensures(false);

      return default(Object);
    }

    public void Serialize(Stream serializationStream, Object graph)
    {
    }

    public void Serialize(Stream serializationStream, Object graph, System.Runtime.Remoting.Messaging.Header[] headers)
    {
    }

    public Object UnsafeDeserialize(Stream serializationStream, System.Runtime.Remoting.Messaging.HeaderHandler handler)
    {
      Contract.Ensures(false);

      return default(Object);
    }

    public Object UnsafeDeserializeMethodResponse(Stream serializationStream, System.Runtime.Remoting.Messaging.HeaderHandler handler, System.Runtime.Remoting.Messaging.IMethodCallMessage methodCallMessage)
    {
      Contract.Ensures(false);

      return default(Object);
    }
    #endregion

    #region Properties and indexers
    public System.Runtime.Serialization.Formatters.FormatterAssemblyStyle AssemblyFormat
    {
      get
      {
        return default(System.Runtime.Serialization.Formatters.FormatterAssemblyStyle);
      }
      set
      {
      }
    }

    public System.Runtime.Serialization.SerializationBinder Binder
    {
      get
      {
        return default(System.Runtime.Serialization.SerializationBinder);
      }
      set
      {
      }
    }

    public System.Runtime.Serialization.StreamingContext Context
    {
      get
      {
        return default(System.Runtime.Serialization.StreamingContext);
      }
      set
      {
      }
    }

    public System.Runtime.Serialization.Formatters.TypeFilterLevel FilterLevel
    {
      get
      {
        return default(System.Runtime.Serialization.Formatters.TypeFilterLevel);
      }
      set
      {
      }
    }

    public System.Runtime.Serialization.ISurrogateSelector SurrogateSelector
    {
      get
      {
        return default(System.Runtime.Serialization.ISurrogateSelector);
      }
      set
      {
      }
    }

    public System.Runtime.Serialization.Formatters.FormatterTypeStyle TypeFormat
    {
      get
      {
        return default(System.Runtime.Serialization.Formatters.FormatterTypeStyle);
      }
      set
      {
      }
    }
    #endregion
  }
}
