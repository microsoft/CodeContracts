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

// File System.Windows.Media.MediaPlayer.cs
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


namespace System.Windows.Media
{
  public partial class MediaPlayer : System.Windows.Media.Animation.Animatable, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public void Close()
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public MediaPlayer()
    {
    }

    public void Open(Uri source)
    {
    }

    public void Pause()
    {
    }

    public void Play()
    {
    }

    protected void ReadPreamble()
    {
    }

    public void Stop()
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }

    protected void WritePreamble()
    {
    }
    #endregion

    #region Properties and indexers
    public double Balance
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double BufferingProgress
    {
      get
      {
        return default(double);
      }
    }

    public bool CanPause
    {
      get
      {
        return default(bool);
      }
    }

    public MediaClock Clock
    {
      get
      {
        return default(MediaClock);
      }
      set
      {
      }
    }

    public double DownloadProgress
    {
      get
      {
        return default(double);
      }
    }

    public bool HasAudio
    {
      get
      {
        return default(bool);
      }
    }

    public bool HasVideo
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsBuffering
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsMuted
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Duration NaturalDuration
    {
      get
      {
        return default(System.Windows.Duration);
      }
    }

    public int NaturalVideoHeight
    {
      get
      {
        return default(int);
      }
    }

    public int NaturalVideoWidth
    {
      get
      {
        return default(int);
      }
    }

    public TimeSpan Position
    {
      get
      {
        return default(TimeSpan);
      }
      set
      {
      }
    }

    public bool ScrubbingEnabled
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public Uri Source
    {
      get
      {
        return default(Uri);
      }
    }

    public double SpeedRatio
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double Volume
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion

    #region Events
    public event EventHandler BufferingEnded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler BufferingStarted
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler MediaEnded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<ExceptionEventArgs> MediaFailed
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler MediaOpened
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<MediaScriptCommandEventArgs> ScriptCommand
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
