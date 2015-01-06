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

// File System.Net.WebHeaderCollection.cs
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


namespace System.Net
{
  public partial class WebHeaderCollection : System.Collections.Specialized.NameValueCollection, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public override void Add (string name, string value)
    {
    }

    public void Add (string header)
    {
    }

    public void Add (HttpRequestHeader header, string value)
    {
    }

    public void Add (HttpResponseHeader header, string value)
    {
    }

    protected void AddWithoutValidate (string headerName, string headerValue)
    {
    }

    public override void Clear ()
    {
    }

    public override string Get (int index)
    {
      return default(string);
    }

    public override string Get (string name)
    {
      return default(string);
    }

    public override System.Collections.IEnumerator GetEnumerator ()
    {
      return default(System.Collections.IEnumerator);
    }

    public override string GetKey (int index)
    {
      return default(string);
    }

    //public override void GetObjectData (System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    //{
    //}

    public override string[] GetValues (int index)
    {
      return default(string[]);
    }

    public override string[] GetValues (string header)
    {
      return default(string[]);
    }

    public static bool IsRestricted (string headerName, bool response)
    {
      return default(bool);
    }

    public static bool IsRestricted (string headerName)
    {
      return default(bool);
    }

    //public override void OnDeserialization (Object sender)
    //{
    //}

    public override void Remove (string name)
    {
    }

    public void Remove (HttpRequestHeader header)
    {
    }

    public void Remove (HttpResponseHeader header)
    {
    }

    public void Set (HttpResponseHeader header, string value)
    {
    }

    public override void Set (string name, string value)
    {
    }

    public void Set (HttpRequestHeader header, string value)
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData (System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }

    public byte[] ToByteArray ()
    {
      return default(byte[]);
    }

    public WebHeaderCollection ()
    {
    }

    protected WebHeaderCollection (System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
    }
    #endregion

    #region Properties and indexers
    public override string[] AllKeys
    {
      get
      {
        return default(string[]);
      }
    }

    public override int Count
    {
      get
      {
        return default(int);
      }
    }

    public string this [HttpRequestHeader header]
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public string this [HttpResponseHeader header]
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys
    {
      get
      {
        return default(System.Collections.Specialized.NameObjectCollectionBase.KeysCollection);
      }
    }
    #endregion
  }
}

#endif