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
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Implements a basic data transfer mechanism.
    /// </summary>

    public class DataObject // : IDataObject, System.Runtime.InteropServices.ComTypes.IDataObject 
    {
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject"/> class.
        // </summary>
        // public DataObject()
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject"/> class and adds the specified object to it.
        // </summary>
        // <param name="data">The data to store. </param>
        // public DataObject(object data)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject"/> class and adds the specified object in the specified format.
        // </summary>
        // <param name="format">The format of the specified data. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats.</param><param name="data">The data to store. </param>
        // public DataObject(string format, object data)
        
        // <summary>
        // Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.
        // </summary>
        // 
        // <returns>
        // The data associated with the specified format, or null.
        // </returns>
        // <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param><param name="autoConvert">true to the convert data to the specified format; otherwise, false. </param>
        // public virtual object GetData(string format, bool autoConvert)
        
        // <summary>
        // Returns the data associated with the specified data format.
        // </summary>
        // 
        // <returns>
        // The data associated with the specified format, or null.
        // </returns>
        // <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param>
        // public virtual object GetData(string format)
        
        // <summary>
        // Returns the data associated with the specified class type format.
        // </summary>
        // 
        // <returns>
        // The data associated with the specified format, or null.
        // </returns>
        // <param name="format">A <see cref="T:System.Type"/> representing the format of the data to retrieve. </param>
        // public virtual object GetData(System.Type format)
        
        // <summary>
        // Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with, or can be converted to, the specified format.
        // </summary>
        // 
        // <returns>
        // true if data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with, or can be converted to, the specified format; otherwise, false.
        // </returns>
        // <param name="format">A <see cref="T:System.Type"/> representing the format to check for. </param>
        // public virtual bool GetDataPresent(System.Type format)
        
        // <summary>
        // Determines whether this <see cref="T:System.Windows.Forms.DataObject"/> contains data in the specified format or, optionally, contains data that can be converted to the specified format.
        // </summary>
        // 
        // <returns>
        // true if the data is in, or can be converted to, the specified format; otherwise, false.
        // </returns>
        // <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param><param name="autoConvert">true to determine whether data stored in this <see cref="T:System.Windows.Forms.DataObject"/> can be converted to the specified format; false to check whether the data is in the specified format. </param>
        // public virtual bool GetDataPresent(string format, bool autoConvert)
        
        // <summary>
        // Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with, or can be converted to, the specified format.
        // </summary>
        // 
        // <returns>
        // true if data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with, or can be converted to, the specified format; otherwise, false.
        // </returns>
        // <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param>
        //public virtual bool GetDataPresent(string format)
        
        /// <summary>
        /// Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with or can be converted to, using an automatic conversion parameter to determine whether to retrieve only native data formats or all formats that the data can be converted to.
        /// </summary>
        /// 
        /// <returns>
        /// An array of type <see cref="T:System.String"/>, containing a list of all formats that are supported by the data stored in this object.
        /// </returns>
        /// <param name="autoConvert">true to retrieve all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with, or can be converted to; false to retrieve only native data formats. </param>
        public virtual string[] GetFormats(bool autoConvert)
        {
            Contract.Ensures(Contract.Result<string[]>() != null);
            return default(string[]);
        }

        /// <summary>
        /// Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject"/> is associated with or can be converted to.
        /// </summary>
        /// 
        /// <returns>
        /// An array of type <see cref="T:System.String"/>, containing a list of all formats that are supported by the data stored in this object.
        /// </returns>
        /// 
        public virtual string[] GetFormats()
        {
            Contract.Ensures(Contract.Result<string[]>() != null);
            return default(string[]);
        }

        // <summary>
        // Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio"/> format.
        // </summary>
        // 
        // <returns>
        // true if the data object contains audio data; otherwise, false.
        // </returns>
        // 
        // public virtual bool ContainsAudio()
        
        // <summary>
        // Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop"/> format or can be converted to that format.
        // </summary>
        // 
        // <returns>
        // true if the data object contains a file drop list; otherwise, false.
        // </returns>
        // 
        // public virtual bool ContainsFileDropList()
        
        // <summary>
        // Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap"/> format or can be converted to that format.
        // </summary>
        // 
        // <returns>
        // true if the data object contains image data; otherwise, false.
        // </returns>
        // 
        // public virtual bool ContainsImage()
        
        // <summary>
        // Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText"/> format.
        // </summary>
        // 
        // <returns>
        // true if the data object contains text data; otherwise, false.
        // </returns>
        // 
        // public virtual bool ContainsText()
        
        // <summary>
        // Indicates whether the data object contains text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat"/> value.
        // </summary>
        // 
        // <returns>
        // true if the data object contains text data in the specified format; otherwise, false.
        // </returns>
        // <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat"/> values.</param><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="format"/> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat"/> value.</exception>
        // public virtual bool ContainsText(TextDataFormat format)
        
        // <summary>
        // Retrieves an audio stream from the data object.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.IO.Stream"/> containing audio data or null if the data object does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio"/> format.
        // </returns>
        // 
        // public virtual Stream GetAudioStream()
        
        // <summary>
        // Retrieves a collection of file names from the data object.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Collections.Specialized.StringCollection"/> containing file names or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop"/> format or can be converted to that format.
        // </returns>
        // 
        // public virtual StringCollection GetFileDropList()
        
        // <summary>
        // Retrieves an image from the data object.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Drawing.Image"/> representing the image data in the data object or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap"/> format or can be converted to that format.
        // </returns>
        // 
        // public virtual Image GetImage()
        
        // <summary>
        // Retrieves text data from the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText"/> format.
        // </summary>
        // 
        // <returns>
        // The text data in the data object or <see cref="F:System.String.Empty"/> if the data object does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText"/> format.
        // </returns>
        // public virtual string GetText()
        
        // <summary>
        // Retrieves text data from the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat"/> value.
        // </summary>
        // 
        // <returns>
        // The text data in the data object or <see cref="F:System.String.Empty"/> if the data object does not contain data in the specified format.
        // </returns>
        // <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat"/> values.</param><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="format"/> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat"/> value.</exception>
        // public virtual string GetText(TextDataFormat format)
        
        /// <summary>
        /// Adds a <see cref="T:System.Byte"/> array to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio"/> format after converting it to a <see cref="T:System.IO.Stream"/>.
        /// </summary>
        /// <param name="audioBytes">A <see cref="T:System.Byte"/> array containing the audio data.</param><exception cref="T:System.ArgumentNullException"><paramref name="audioBytes"/> is null.</exception>
        public virtual void SetAudio(byte[] audioBytes)
        {
            Contract.Requires(audioBytes != null);
        }

        /// <summary>
        /// Adds a <see cref="T:System.IO.Stream"/> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio"/> format.
        /// </summary>
        /// <param name="audioStream">A <see cref="T:System.IO.Stream"/> containing the audio data.</param><exception cref="T:System.ArgumentNullException"><paramref name="audioStream"/> is null.</exception>
        public virtual void SetAudio(Stream audioStream)
        {
            Contract.Requires(audioStream != null);
        }

        /// <summary>
        /// Adds a collection of file names to the data object in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop"/> format.
        /// </summary>
        /// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection"/> containing the file names.</param><exception cref="T:System.ArgumentNullException"><paramref name="filePaths"/> is null.</exception>
        public virtual void SetFileDropList(StringCollection filePaths)
        {
            Contract.Requires(filePaths != null);
        }

        /// <summary>
        /// Adds an <see cref="T:System.Drawing.Image"/> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap"/> format.
        /// </summary>
        /// <param name="image">The <see cref="T:System.Drawing.Image"/> to add to the data object.</param><exception cref="T:System.ArgumentNullException"><paramref name="image"/> is null.</exception>
        public virtual void SetImage(Image image)
        {
            Contract.Requires(image != null);
        }

        /// <summary>
        /// Adds text data to the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText"/> format.
        /// </summary>
        /// <param name="textData">The text to add to the data object.</param><exception cref="T:System.ArgumentNullException"><paramref name="textData"/> is null or <see cref="F:System.String.Empty"/>.</exception>
        public virtual void SetText(string textData)
        {
            Contract.Requires(textData != null);
        }

        /// <summary>
        /// Adds text data to the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat"/> value.
        /// </summary>
        /// <param name="textData">The text to add to the data object.</param><param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat"/> values.</param><exception cref="T:System.ArgumentNullException"><paramref name="textData"/> is null or <see cref="F:System.String.Empty"/>.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="format"/> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat"/> value.</exception>
        public virtual void SetText(string textData, TextDataFormat format)
        {
            Contract.Requires(textData != null);
        }

        // <summary>
        // Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject"/> using the specified format and indicating whether the data can be converted to another format.
        // </summary>
        // <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param><param name="autoConvert">true to allow the data to be converted to another format; otherwise, false. </param><param name="data">The data to store. </param>
        // public virtual void SetData(string format, bool autoConvert, object data)

        // <summary>
        // Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject"/> using the specified format.
        // </summary>
        // <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats"/> for predefined formats. </param><param name="data">The data to store. </param>
        // public virtual void SetData(string format, object data)
        
        // <summary>
        // Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject"/> using the specified type as the format.
        // </summary>
        // <param name="format">A <see cref="T:System.Type"/> representing the format associated with the data. </param><param name="data">The data to store. </param>
        // 
        // public virtual void SetData(System.Type format, object data)
        
        // <summary>
        // Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject"/> using the object type as the data format.
        // </summary>
        // <param name="data">The data to store. </param>
        // public virtual void SetData(object data)
    }
}
