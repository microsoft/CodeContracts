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

// File System.Media.SoundPlayer.cs
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


namespace System.Media
{
  public partial class SoundPlayer : System.ComponentModel.Component, System.Runtime.Serialization.ISerializable
  {
    #region Methods and constructors
    public void Load()
    {
    }

    public void LoadAsync()
    {
    }

    protected virtual new void OnLoadCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
    {
    }

    protected virtual new void OnSoundLocationChanged(EventArgs e)
    {
    }

    protected virtual new void OnStreamChanged(EventArgs e)
    {
    }

    public void Play()
    {
    }

    public void PlayLooping()
    {
    }

    public void PlaySync()
    {
    }

    public SoundPlayer(Stream stream)
    {
    }

    public SoundPlayer(string soundLocation)
    {
    }

    protected SoundPlayer(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext context)
    {
      Contract.Requires(serializationInfo != null);
    }

    public SoundPlayer()
    {
    }

    public void Stop()
    {
    }

    void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
    {
    }
    #endregion

    #region Properties and indexers
    public bool IsLoadCompleted
    {
      get
      {
        return default(bool);
      }
    }

    public int LoadTimeout
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public string SoundLocation
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public Stream Stream
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public Object Tag
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event System.ComponentModel.AsyncCompletedEventHandler LoadCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler SoundLocationChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler StreamChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
