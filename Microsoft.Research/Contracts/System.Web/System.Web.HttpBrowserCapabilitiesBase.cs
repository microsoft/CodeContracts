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

#if NETFRAMEWORK_4_0

// File System.Web.HttpBrowserCapabilitiesBase.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
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
  abstract public partial class HttpBrowserCapabilitiesBase : System.Web.UI.IFilterResolutionService
  {
    #region Methods and constructors
    public virtual new void AddBrowser (string browserName)
    {
    }

    public virtual new int CompareFilters (string filter1, string filter2)
    {
      return default(int);
    }

    public virtual new System.Web.UI.HtmlTextWriter CreateHtmlTextWriter (TextWriter w)
    {
      return default(System.Web.UI.HtmlTextWriter);
    }

    public virtual new void DisableOptimizedCacheKey ()
    {
    }

    public virtual new bool EvaluateFilter (string filterName)
    {
      return default(bool);
    }

    public virtual new Version[] GetClrVersions ()
    {
      return default(Version[]);
    }

    protected HttpBrowserCapabilitiesBase ()
    {
    }

    public virtual new bool IsBrowser (string browserName)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public virtual new bool ActiveXControls
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Collections.IDictionary Adapters
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
    }

    public virtual new bool AOL
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool BackgroundSounds
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool Beta
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string Browser
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Collections.ArrayList Browsers
    {
      get
      {
        return default(System.Collections.ArrayList);
      }
    }

    public virtual new bool CanCombineFormsInDeck
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanInitiateVoiceCall
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderAfterInputOrSelectElement
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderEmptySelects
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderInputAndSelectElementsTogether
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderMixedSelects
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderOneventAndPrevElementsTogether
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderPostBackCards
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanRenderSetvarZeroWithMultiSelectionList
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool CanSendMail
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new System.Collections.IDictionary Capabilities
    {
      get
      {
        return default(System.Collections.IDictionary);
      }
      set
      {
      }
    }

    public virtual new bool CDF
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Version ClrVersion
    {
      get
      {
        return default(Version);
      }
    }

    public virtual new bool Cookies
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool Crawler
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new int DefaultSubmitButtonLimit
    {
      get
      {
        return default(int);
      }
    }

    public virtual new Version EcmaScriptVersion
    {
      get
      {
        return default(Version);
      }
    }

    public virtual new bool Frames
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new int GatewayMajorVersion
    {
      get
      {
        return default(int);
      }
    }

    public virtual new double GatewayMinorVersion
    {
      get
      {
        return default(double);
      }
    }

    public virtual new string GatewayVersion
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool HasBackButton
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool HidesRightAlignedMultiselectScrollbars
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string HtmlTextWriter
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public virtual new string Id
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string InputType
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool IsColor
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool IsMobileDevice
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string this [string key]
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool JavaApplets
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Version JScriptVersion
    {
      get
      {
        return default(Version);
      }
    }

    public virtual new int MajorVersion
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int MaximumHrefLength
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int MaximumRenderedPageSize
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int MaximumSoftkeyLabelLength
    {
      get
      {
        return default(int);
      }
    }

    public virtual new double MinorVersion
    {
      get
      {
        return default(double);
      }
    }

    public virtual new string MinorVersionString
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string MobileDeviceManufacturer
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string MobileDeviceModel
    {
      get
      {
        return default(string);
      }
    }

    public virtual new Version MSDomVersion
    {
      get
      {
        return default(Version);
      }
    }

    public virtual new int NumberOfSoftkeys
    {
      get
      {
        return default(int);
      }
    }

    public virtual new string Platform
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PreferredImageMime
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PreferredRenderingMime
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PreferredRenderingType
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PreferredRequestEncoding
    {
      get
      {
        return default(string);
      }
    }

    public virtual new string PreferredResponseEncoding
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool RendersBreakBeforeWmlSelectAndInput
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RendersBreaksAfterHtmlLists
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RendersBreaksAfterWmlAnchor
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RendersBreaksAfterWmlInput
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RendersWmlDoAcceptsInline
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RendersWmlSelectsAsMenuCards
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string RequiredMetaTagNameValue
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool RequiresAttributeColonSubstitution
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresContentTypeMetaTag
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresControlStateInSession
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresDBCSCharacter
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresHtmlAdaptiveErrorReporting
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresLeadingPageBreak
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresNoBreakInFormatting
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresOutputOptimization
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresPhoneNumbersAsPlainText
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresSpecialViewStateEncoding
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresUniqueFilePathSuffix
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresUniqueHtmlCheckboxNames
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresUniqueHtmlInputNames
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool RequiresUrlEncodedPostfieldValues
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new int ScreenBitDepth
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int ScreenCharactersHeight
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int ScreenCharactersWidth
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int ScreenPixelsHeight
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int ScreenPixelsWidth
    {
      get
      {
        return default(int);
      }
    }

    public virtual new bool SupportsAccesskeyAttribute
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsBodyColor
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsBold
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsCacheControlMetaTag
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsCallback
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsCss
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsDivAlign
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsDivNoWrap
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsEmptyStringInCookieValue
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsFontColor
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsFontName
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsFontSize
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsImageSubmit
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsIModeSymbols
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsInputIStyle
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsInputMode
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsItalic
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsJPhoneMultiMediaAttributes
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsJPhoneSymbols
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsQueryStringInFormAction
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsRedirectWithCookie
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsSelectMultiple
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsUncheck
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool SupportsXmlHttp
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool Tables
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new Type TagWriter
    {
      get
      {
        return default(Type);
      }
    }

    public virtual new string Type
    {
      get
      {
        return default(string);
      }
    }

    public virtual new bool UseOptimizedCacheKey
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool VBScript
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new string Version
    {
      get
      {
        return default(string);
      }
    }

    public virtual new System.Version W3CDomVersion
    {
      get
      {
        return default(System.Version);
      }
    }

    public virtual new bool Win16
    {
      get
      {
        return default(bool);
      }
    }

    public virtual new bool Win32
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}

#endif