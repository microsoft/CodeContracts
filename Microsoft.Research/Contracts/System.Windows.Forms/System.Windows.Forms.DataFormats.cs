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
using System.Diagnostics.Contracts;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides static, predefined <see cref="T:System.Windows.Forms.Clipboard"/> format names. Use them to identify the format of data that you store in an <see cref="T:System.Windows.Forms.IDataObject"/>.
    /// </summary>
    
    public class DataFormats
    {
        // <summary>
        // Specifies the standard ANSI text format. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Text = "Text";

        // <summary>
        // Specifies the standard Windows Unicode text format. This static field is read-only.
        // </summary>
        // 
        // public static readonly string UnicodeText = "UnicodeText";

        // <summary>
        // Specifies the Windows device-independent bitmap (DIB) format. This static field is read-only.
        // </summary>
        // 
        // public static readonly string Dib = "DeviceIndependentBitmap";

        // <summary>
        // Specifies a Windows bitmap format. This static field is read-only.
        // </summary>
        // 

        // public static readonly string Bitmap = "Bitmap";
        // <summary>
        // Specifies the Windows enhanced metafile format. This static field is read-only.
        // </summary>
        // 
        // public static readonly string EnhancedMetafile = "EnhancedMetafile";

        // <summary>
        // Specifies the Windows metafile format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        // public static readonly string MetafilePict = "MetaFilePict";

        // <summary>
        // Specifies the Windows symbolic link format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        // public static readonly string SymbolicLink = "SymbolicLink";
        // <summary>
        // Specifies the Windows Data Interchange Format (DIF), which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Dif = "DataInterchangeFormat";

        // <summary>
        // Specifies the Tagged Image File Format (TIFF), which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Tiff = "TaggedImageFileFormat";

        // <summary>
        // Specifies the standard Windows original equipment manufacturer (OEM) text format. This static field is read-only.
        // </summary>
        // 
        //public static readonly string OemText = "OEMText";

        // <summary>
        // Specifies the Windows palette format. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Palette = "Palette";

        // <summary>
        // Specifies the Windows pen data format, which consists of pen strokes for handwriting software; Windows Forms does not use this format. This static field is read-only.
        // </summary>
        // 
        //public static readonly string PenData = "PenData";

        // <summary>
        // Specifies the Resource Interchange File Format (RIFF) audio format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Riff = "RiffAudio";

        // <summary>
        // Specifies the wave audio format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string WaveAudio = "WaveAudio";

        // <summary>
        // Specifies the Windows file drop format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string FileDrop = "FileDrop";

        // <summary>
        // Specifies the Windows culture format, which Windows Forms does not directly use. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Locale = "Locale";

        // <summary>
        // Specifies text in the HTML Clipboard format. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Html = "HTML Format";

        // <summary>
        // Specifies text consisting of Rich Text Format (RTF) data. This static field is read-only.
        // </summary>
        // 
        //public static readonly string Rtf = "Rich Text Format";

        // <summary>
        // Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets. This format is not used directly by Windows Forms. This static field is read-only.
        // </summary>
        // 
        //public static readonly string CommaSeparatedValue = "Csv";

        // <summary>
        // Specifies the Windows Forms string class format, which Windows Forms uses to store string objects. This static field is read-only.
        // </summary>
        // 
        //public static readonly string StringFormat = typeof(string).FullName;

        // <summary>
        // Specifies a format that encapsulates any type of Windows Forms object. This static field is read-only.
        // </summary>
        // public static readonly string Serializable = Application.WindowsFormsVersion + "PersistentObject";
        
        // <summary>
        // Returns a <see cref="T:System.Windows.Forms.DataFormats.Format"/> with the Windows Clipboard numeric ID and name for the specified format.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataFormats.Format"/> that has the Windows Clipboard numeric ID and the name of the format.
        // </returns>
        // <param name="format">The format name. </param><exception cref="T:System.ComponentModel.Win32Exception">Registering a new <see cref="T:System.Windows.Forms.Clipboard"/> format failed. </exception>
        // public static DataFormats.Format GetFormat(string format)
        
        // <summary>
        // Returns a <see cref="T:System.Windows.Forms.DataFormats.Format"/> with the Windows Clipboard numeric ID and name for the specified ID.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataFormats.Format"/> that has the Windows Clipboard numeric ID and the name of the format.
        // </returns>
        // <param name="id">The format ID. </param>
        // public static DataFormats.Format GetFormat(int id)
        
        /// <summary>
        /// Represents a Clipboard format type.
        /// </summary>
        public class Format
        { 
            // <summary>
            // Gets the name of this format.
            // </summary>
            // 
            // <returns>
            // The name of this format.
            // </returns>
            // public string Name {get;}
            
            // <summary>
            // Gets the ID number for this format.
            // </summary>
            // 
            // <returns>
            // The ID number for this format.
            // </returns>
            // public int Id { get; }
            
            // <summary>
            // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataFormats.Format"/> class with a Boolean that indicates whether a Win32 handle is expected.
            // </summary>
            // <param name="name">The name of this format. </param><param name="id">The ID number for this format. </param>
            // public Format(string name, int id)
        }
    }
}
