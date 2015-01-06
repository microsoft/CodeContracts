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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.Language.StandardClassification;

namespace Adornments {
  public class VSTextPropertiesProvider {
    IClassificationFormatMapService _formatMapService;
    IClassificationTypeRegistryService _classificationTypeRegistryService;

    public VSTextPropertiesProvider(IClassificationFormatMapService formatMapService, IClassificationTypeRegistryService classificationTypeRegistryService) {
      Contract.Requires(formatMapService != null);
      Contract.Requires(classificationTypeRegistryService != null);
      _formatMapService = formatMapService;
      _classificationTypeRegistryService = classificationTypeRegistryService;
    }

    public VSTextProperties GetVSTextProperties(ITextView textView) {
      Contract.Requires(textView != null);

      var formatMap = _formatMapService.GetClassificationFormatMap(textView);
      var textProperties = GetTextProperties(formatMap, PredefinedClassificationTypeNames.Character);
      var keywordProperties = GetTextProperties(formatMap, PredefinedClassificationTypeNames.Keyword);
      var typeProperties = GetTextProperties(formatMap, PredefinedClassificationTypeNames.SymbolDefinition); 
      var tabSize = textView.Options.GetOptionValue(DefaultOptions.TabSizeOptionId);
      return new VSTextProperties() {
        FontSize = textProperties.FontRenderingEmSize,
        TextColor = textProperties.ForegroundBrush,
        TabSize = tabSize,
        TypeColor = typeProperties.ForegroundBrush,
        KeywordColor = keywordProperties.ForegroundBrush
      };
    }

    TextFormattingRunProperties GetTextProperties(IClassificationFormatMap formatMap, string classifactionType) {
      var plainTextClassificationType = _classificationTypeRegistryService.GetClassificationType(classifactionType);
      return formatMap.GetTextProperties(plainTextClassificationType);
    }
  }
  public struct VSTextProperties {
    public double FontSize;
    public Brush TextColor;
    public Brush KeywordColor;
    public Brush TypeColor;
    public int TabSize;
    /// <remarks>
    /// This field is NOT set by the VSTextPropertiesProvider. Must be set manually.
    /// </remarks>
    public double LineHeight;
  }
}
