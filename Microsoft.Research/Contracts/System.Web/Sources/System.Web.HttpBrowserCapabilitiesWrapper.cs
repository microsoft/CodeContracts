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

// File System.Web.HttpBrowserCapabilitiesWrapper.cs
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


namespace System.Web
{
  public partial class HttpBrowserCapabilitiesWrapper : HttpBrowserCapabilitiesBase
  {
    #region Methods and constructors
    public override void AddBrowser(string browserName)
    {
    }

    public override int CompareFilters(string filter1, string filter2)
    {
      return default(int);
    }

    public override System.Web.UI.HtmlTextWriter CreateHtmlTextWriter(TextWriter w)
    {
      return default(System.Web.UI.HtmlTextWriter);
    }

    public override void DisableOptimizedCacheKey()
    {
    }

    public override bool EvaluateFilter(string filterName)
    {
      return default(bool);
    }

    public override Version[] GetClrVersions()
    {
      return default(Version[]);
    }

    public HttpBrowserCapabilitiesWrapper(HttpBrowserCapabilities httpBrowserCapabilities)
    {
    }

    public override bool IsBrowser(string browserName)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public override bool ActiveXControls
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Collections.IDictionary Adapters
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public override bool AOL
    {
      get
      {
        return default(bool);
      }
    }

    public override bool BackgroundSounds
    {
      get
      {
        return default(bool);
      }
    }

    public override bool Beta
    {
      get
      {
        return default(bool);
      }
    }

    public override string Browser
    {
      get
      {
        return default(string);
      }
    }

    public override System.Collections.ArrayList Browsers
    {
      get
      {
        return default(System.Collections.ArrayList);
      }
    }

    public override bool CanCombineFormsInDeck
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanInitiateVoiceCall
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderAfterInputOrSelectElement
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderEmptySelects
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderInputAndSelectElementsTogether
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderMixedSelects
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderOneventAndPrevElementsTogether
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderPostBackCards
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanRenderSetvarZeroWithMultiSelectionList
    {
      get
      {
        return default(bool);
      }
    }

    public override bool CanSendMail
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Collections.IDictionary Capabilities
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
      set
      {
      }
    }

    public override bool CDF
    {
      get
      {
        return default(bool);
      }
    }

    public override Version ClrVersion
    {
      get
      {
        return default(Version);
      }
    }

    public override bool Cookies
    {
      get
      {
        return default(bool);
      }
    }

    public override bool Crawler
    {
      get
      {
        return default(bool);
      }
    }

    public override int DefaultSubmitButtonLimit
    {
      get
      {
        return default(int);
      }
    }

    public override Version EcmaScriptVersion
    {
      get
      {
        return default(Version);
      }
    }

    public override bool Frames
    {
      get
      {
        return default(bool);
      }
    }

    public override int GatewayMajorVersion
    {
      get
      {
        return default(int);
      }
    }

    public override double GatewayMinorVersion
    {
      get
      {
        return default(double);
      }
    }

    public override string GatewayVersion
    {
      get
      {
        return default(string);
      }
    }

    public override bool HasBackButton
    {
      get
      {
        return default(bool);
      }
    }

    public override bool HidesRightAlignedMultiselectScrollbars
    {
      get
      {
        return default(bool);
      }
    }

    public override string HtmlTextWriter
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public override string Id
    {
      get
      {
        return default(string);
      }
    }

    public override string InputType
    {
      get
      {
        return default(string);
      }
    }

    public override bool IsColor
    {
      get
      {
        return default(bool);
      }
    }

    public override bool IsMobileDevice
    {
      get
      {
        return default(bool);
      }
    }

    public override string this [string key]
    {
      get
      {
        return default(string);
      }
    }

    public override bool JavaApplets
    {
      get
      {
        return default(bool);
      }
    }

    public override Version JScriptVersion
    {
      get
      {
        return default(Version);
      }
    }

    public override int MajorVersion
    {
      get
      {
        return default(int);
      }
    }

    public override int MaximumHrefLength
    {
      get
      {
        return default(int);
      }
    }

    public override int MaximumRenderedPageSize
    {
      get
      {
        return default(int);
      }
    }

    public override int MaximumSoftkeyLabelLength
    {
      get
      {
        return default(int);
      }
    }

    public override double MinorVersion
    {
      get
      {
        return default(double);
      }
    }

    public override string MinorVersionString
    {
      get
      {
        return default(string);
      }
    }

    public override string MobileDeviceManufacturer
    {
      get
      {
        return default(string);
      }
    }

    public override string MobileDeviceModel
    {
      get
      {
        return default(string);
      }
    }

    public override Version MSDomVersion
    {
      get
      {
        return default(Version);
      }
    }

    public override int NumberOfSoftkeys
    {
      get
      {
        return default(int);
      }
    }

    public override string Platform
    {
      get
      {
        return default(string);
      }
    }

    public override string PreferredImageMime
    {
      get
      {
        return default(string);
      }
    }

    public override string PreferredRenderingMime
    {
      get
      {
        return default(string);
      }
    }

    public override string PreferredRenderingType
    {
      get
      {
        return default(string);
      }
    }

    public override string PreferredRequestEncoding
    {
      get
      {
        return default(string);
      }
    }

    public override string PreferredResponseEncoding
    {
      get
      {
        return default(string);
      }
    }

    public override bool RendersBreakBeforeWmlSelectAndInput
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RendersBreaksAfterHtmlLists
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RendersBreaksAfterWmlAnchor
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RendersBreaksAfterWmlInput
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RendersWmlDoAcceptsInline
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RendersWmlSelectsAsMenuCards
    {
      get
      {
        return default(bool);
      }
    }

    public override string RequiredMetaTagNameValue
    {
      get
      {
        return default(string);
      }
    }

    public override bool RequiresAttributeColonSubstitution
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresContentTypeMetaTag
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresControlStateInSession
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresDBCSCharacter
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresHtmlAdaptiveErrorReporting
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresLeadingPageBreak
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresNoBreakInFormatting
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresOutputOptimization
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresPhoneNumbersAsPlainText
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresSpecialViewStateEncoding
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresUniqueFilePathSuffix
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresUniqueHtmlCheckboxNames
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresUniqueHtmlInputNames
    {
      get
      {
        return default(bool);
      }
    }

    public override bool RequiresUrlEncodedPostfieldValues
    {
      get
      {
        return default(bool);
      }
    }

    public override int ScreenBitDepth
    {
      get
      {
        return default(int);
      }
    }

    public override int ScreenCharactersHeight
    {
      get
      {
        return default(int);
      }
    }

    public override int ScreenCharactersWidth
    {
      get
      {
        return default(int);
      }
    }

    public override int ScreenPixelsHeight
    {
      get
      {
        return default(int);
      }
    }

    public override int ScreenPixelsWidth
    {
      get
      {
        return default(int);
      }
    }

    public override bool SupportsAccesskeyAttribute
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsBodyColor
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsBold
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsCacheControlMetaTag
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsCallback
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsCss
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsDivAlign
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsDivNoWrap
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsEmptyStringInCookieValue
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsFontColor
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsFontName
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsFontSize
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsImageSubmit
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsIModeSymbols
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsInputIStyle
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsInputMode
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsItalic
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsJPhoneMultiMediaAttributes
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsJPhoneSymbols
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsQueryStringInFormAction
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsRedirectWithCookie
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsSelectMultiple
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsUncheck
    {
      get
      {
        return default(bool);
      }
    }

    public override bool SupportsXmlHttp
    {
      get
      {
        return default(bool);
      }
    }

    public override bool Tables
    {
      get
      {
        return default(bool);
      }
    }

    public override Type TagWriter
    {
      get
      {
        return default(Type);
      }
    }

    public override string Type
    {
      get
      {
        return default(string);
      }
    }

    public override bool UseOptimizedCacheKey
    {
      get
      {
        return default(bool);
      }
    }

    public override bool VBScript
    {
      get
      {
        return default(bool);
      }
    }

    public override string Version
    {
      get
      {
        return default(string);
      }
    }

    public override Version W3CDomVersion
    {
      get
      {
        return default(Version);
      }
    }

    public override bool Win16
    {
      get
      {
        return default(bool);
      }
    }

    public override bool Win32
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
