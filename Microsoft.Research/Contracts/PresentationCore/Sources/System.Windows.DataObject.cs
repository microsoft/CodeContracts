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

// File System.Windows.DataObject.cs
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


namespace System.Windows
{
  sealed public partial class DataObject : IDataObject, System.Runtime.InteropServices.ComTypes.IDataObject
  {
    #region Methods and constructors
    public static void AddCopyingHandler(DependencyObject element, DataObjectCopyingEventHandler handler)
    {
    }

    public static void AddPastingHandler(DependencyObject element, DataObjectPastingEventHandler handler)
    {
    }

    public static void AddSettingDataHandler(DependencyObject element, DataObjectSettingDataEventHandler handler)
    {
    }

    public bool ContainsAudio()
    {
      return default(bool);
    }

    public bool ContainsFileDropList()
    {
      return default(bool);
    }

    public bool ContainsImage()
    {
      return default(bool);
    }

    public bool ContainsText()
    {
      return default(bool);
    }

    public bool ContainsText(TextDataFormat format)
    {
      return default(bool);
    }

    public DataObject()
    {
    }

    public DataObject(Object data)
    {
    }

    public DataObject(string format, Object data, bool autoConvert)
    {
    }

    public DataObject(string format, Object data)
    {
    }

    public DataObject(Type format, Object data)
    {
    }

    public Stream GetAudioStream()
    {
      return default(Stream);
    }

    public Object GetData(string format)
    {
      return default(Object);
    }

    public Object GetData(string format, bool autoConvert)
    {
      return default(Object);
    }

    public Object GetData(Type format)
    {
      return default(Object);
    }

    public bool GetDataPresent(string format)
    {
      return default(bool);
    }

    public bool GetDataPresent(Type format)
    {
      return default(bool);
    }

    public bool GetDataPresent(string format, bool autoConvert)
    {
      return default(bool);
    }

    public System.Collections.Specialized.StringCollection GetFileDropList()
    {
      return default(System.Collections.Specialized.StringCollection);
    }

    public string[] GetFormats(bool autoConvert)
    {
      return default(string[]);
    }

    public string[] GetFormats()
    {
      return default(string[]);
    }

    public System.Windows.Media.Imaging.BitmapSource GetImage()
    {
      return default(System.Windows.Media.Imaging.BitmapSource);
    }

    public string GetText(TextDataFormat format)
    {
      return default(string);
    }

    public string GetText()
    {
      return default(string);
    }

    public static void RemoveCopyingHandler(DependencyObject element, DataObjectCopyingEventHandler handler)
    {
    }

    public static void RemovePastingHandler(DependencyObject element, DataObjectPastingEventHandler handler)
    {
    }

    public static void RemoveSettingDataHandler(DependencyObject element, DataObjectSettingDataEventHandler handler)
    {
    }

    public void SetAudio(byte[] audioBytes)
    {
    }

    public void SetAudio(Stream audioStream)
    {
    }

    public void SetData(Type format, Object data)
    {
    }

    public void SetData(string format, Object data)
    {
    }

    public void SetData(string format, Object data, bool autoConvert)
    {
    }

    public void SetData(Object data)
    {
    }

    public void SetFileDropList(System.Collections.Specialized.StringCollection fileDropList)
    {
    }

    public void SetImage(System.Windows.Media.Imaging.BitmapSource image)
    {
    }

    public void SetText(string textData)
    {
    }

    public void SetText(string textData, TextDataFormat format)
    {
    }

    int System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(ref System.Runtime.InteropServices.ComTypes.FORMATETC pFormatetc, System.Runtime.InteropServices.ComTypes.ADVF advf, System.Runtime.InteropServices.ComTypes.IAdviseSink pAdvSink, out int pdwConnection)
    {
      pdwConnection = default(int);

      return default(int);
    }

    void System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(int dwConnection)
    {
    }

    int System.Runtime.InteropServices.ComTypes.IDataObject.EnumDAdvise(out System.Runtime.InteropServices.ComTypes.IEnumSTATDATA enumAdvise)
    {
      enumAdvise = default(System.Runtime.InteropServices.ComTypes.IEnumSTATDATA);

      return default(int);
    }

    System.Runtime.InteropServices.ComTypes.IEnumFORMATETC System.Runtime.InteropServices.ComTypes.IDataObject.EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR dwDirection)
    {
      return default(System.Runtime.InteropServices.ComTypes.IEnumFORMATETC);
    }

    int System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(ref System.Runtime.InteropServices.ComTypes.FORMATETC pformatetcIn, out System.Runtime.InteropServices.ComTypes.FORMATETC pformatetcOut)
    {
      pformatetcOut = default(System.Runtime.InteropServices.ComTypes.FORMATETC);

      return default(int);
    }

    void System.Runtime.InteropServices.ComTypes.IDataObject.GetData(ref System.Runtime.InteropServices.ComTypes.FORMATETC formatetc, out System.Runtime.InteropServices.ComTypes.STGMEDIUM medium)
    {
      medium = default(System.Runtime.InteropServices.ComTypes.STGMEDIUM);
    }

    void System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(ref System.Runtime.InteropServices.ComTypes.FORMATETC formatetc, ref System.Runtime.InteropServices.ComTypes.STGMEDIUM medium)
    {
    }

    int System.Runtime.InteropServices.ComTypes.IDataObject.QueryGetData(ref System.Runtime.InteropServices.ComTypes.FORMATETC formatetc)
    {
      return default(int);
    }

    void System.Runtime.InteropServices.ComTypes.IDataObject.SetData(ref System.Runtime.InteropServices.ComTypes.FORMATETC pFormatetcIn, ref System.Runtime.InteropServices.ComTypes.STGMEDIUM pmedium, bool fRelease)
    {
    }
    #endregion

    #region Fields
    public readonly static RoutedEvent CopyingEvent;
    public readonly static RoutedEvent PastingEvent;
    public readonly static RoutedEvent SettingDataEvent;
    #endregion
  }
}
