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

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Adornments;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Formatting;

namespace MetadataContracts
{
    /// <summary>
    /// Establishes an <see cref="IAdornmentLayer"/> to place the adornment on and exports the <see cref="IWpfTextViewCreationListener"/>
    /// that instantiates the adornment on the event of a <see cref="IWpfTextView"/>'s creation
    /// </summary>
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class TextViewProvider : IWpfTextViewCreationListener
    {

        /// <summary>
        /// Defines the adornment layer for the scarlet adornment. This layer is ordered 
        /// after the selection layer in the Z-order
        /// </summary>
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("MetadataAdornments")]
        [Order(After = PredefinedAdornmentLayers.Text)]
        [TextViewRole(PredefinedTextViewRoles.Document)]
        public AdornmentLayerDefinition editorAdornmentLayer = null;

        #region GetDefaultFontSize
        [Import]
        internal IClassificationFormatMapService FormatMapService;
        [Import]
        internal IClassificationTypeRegistryService ClassificationTypeRegistryService;
        /// <summary>
        /// Gets the default font size used by VS.
        /// </summary>
        /// <remarks>
        /// The default font size is set in Tools->Options->Environment->Fonts and Colors.
        /// </remarks>
        public double GetDefaultFontSize(IWpfTextView textView)
        {
            IClassificationFormatMap formatMap = this.FormatMapService.GetClassificationFormatMap(textView);
            IClassificationType plainTextClassificationType = this.ClassificationTypeRegistryService.GetClassificationType("text");
            TextFormattingRunProperties textProperties = formatMap.GetTextProperties(plainTextClassificationType);
            return textProperties.FontRenderingEmSize;
        }
        #endregion

        public void TextViewCreated(IWpfTextView textView)
        {
            var manager = AdornmentManager.GetOrCreateAdornmentManager(textView, "MetadataAdornments");
            var defaultFontSize = GetDefaultFontSize(textView);
            var metadataTracker = MetadataTracker.GetOrCreateMetadataTracker(textView, GetDefaultFontSize(textView));
        }
    }
}
