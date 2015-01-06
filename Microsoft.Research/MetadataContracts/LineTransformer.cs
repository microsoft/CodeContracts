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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Utilities;
using Adornments;

namespace MetadataContracts
{
    /// <summary>
    /// Provides a line transform source so that line transforms can be properly calcualted for contract adornments.
    /// </summary>
    [Export(typeof(ILineTransformSourceProvider))]
    [ContentType("code")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class LineTransformSourceProvider : ILineTransformSourceProvider
    {
        public ILineTransformSource Create(IWpfTextView textView)
        {
            var adornmentManager = AdornmentManager.GetOrCreateAdornmentManager(textView, "MetadataAdornments");
            return new LineTransformSource(adornmentManager.Adornments);
        }
    }
    /// <summary>
    /// A custom line transform source that transforms lines to fit adornments as needed.
    /// </summary>
    class LineTransformSource : ILineTransformSource
    {
        readonly IEnumerable<IAdornment> adornments;
        public LineTransformSource(IEnumerable<IAdornment> adornments)
        {
            this.adornments = adornments;
        }
        public LineTransform GetLineTransform(ITextViewLine line, double yPosition, ViewRelativePosition placement)
        {
            foreach (IAdornment adornment in this.adornments)
            {
                if (line.ContainsBufferPosition(new SnapshotPoint(line.Snapshot, adornment.Span.GetStartPoint(line.Snapshot).Position)))
                {
                    //if (adornment.Visual.IsMeasureValid) {
                    //  adornment.ShouldRedrawAdornmentLayout = false;
                    //}
                    return new LineTransform(adornment.Visual.DesiredSize.Height, 0.0, 1.0);
                }
            }
            return new LineTransform(0, 0, 1);
        }
    }
}
