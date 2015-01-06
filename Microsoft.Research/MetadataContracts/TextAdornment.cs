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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;

namespace MetadataContracts
{

    ///<summary>
    ///ScarletCharacter adornment places red boxes behind all the "a"s in the editor window
    ///</summary>
    public class ScarletCharacter
    {
        IAdornmentLayer _layer;
        IWpfTextView _view;
        Brush _brush;
        Pen _pen;

        public ScarletCharacter(IWpfTextView view)
        {
            _view = view;
            _layer = view.GetAdornmentLayer("MetadataAdornments");

            //Listen to any event that changes the layout (text changes, scrolling, etc)
            _view.LayoutChanged += OnLayoutChanged;

            //Create the pen and brush to color the box behind the a's
            Brush brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            brush.Freeze();
            Brush penBrush = new SolidColorBrush(Colors.Red);
            penBrush.Freeze();
            Pen pen = new Pen(penBrush, 0.5);
            pen.Freeze();

            _brush = brush;
            _pen = pen;
        }

        /// <summary>
        /// On layout change add the adornment to any reformated lines
        /// </summary>
        private void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                this.CreateVisuals(line);
            }
        }

        /// <summary>
        /// Within the given line add the scarlet box behind the a
        /// </summary>
        private void CreateVisuals(ITextViewLine line)
        {
            //grab a reference to the lines in the current TextView 
            IWpfTextViewLineCollection textViewLines = _view.TextViewLines;
            int start = line.Start;
            int end = line.End;

            //Loop through each character, and place a box around any a 
            for (int i = start; (i < end); ++i)
            {
                if (_view.TextSnapshot[i] == 'a')
                {
                    SnapshotSpan span = new SnapshotSpan(_view.TextSnapshot, Span.FromBounds(i, i + 1));
                    Geometry g = textViewLines.GetMarkerGeometry(span);
                    if (g != null)
                    {
                        GeometryDrawing drawing = new GeometryDrawing(_brush, _pen, g);
                        drawing.Freeze();

                        DrawingImage drawingImage = new DrawingImage(drawing);
                        drawingImage.Freeze();

                        Image image = new Image();
                        image.Source = drawingImage;

                        //Align the image with the top of the bounds of the text geometry
                        Canvas.SetLeft(image, g.Bounds.Left);
                        Canvas.SetTop(image, g.Bounds.Top);

                        _layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
                    }

                }
            }
        }
    }
}
