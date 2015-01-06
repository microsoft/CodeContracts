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

// File System.Web.Util.IManagedContext.cs
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


namespace System.Web.Util
{
  internal partial interface IManagedContext
  {
    #region Methods and constructors
    string Application_GetContentsNames();

    Object Application_GetContentsObject(string name);

    string Application_GetStaticNames();

    Object Application_GetStaticObject(string name);

    void Application_Lock();

    void Application_RemoveAllContentsObjects();

    void Application_RemoveContentsObject(string name);

    void Application_SetContentsObject(string name, Object obj);

    void Application_UnLock();

    int Context_IsPresent();

    int Request_BinaryRead(byte[] bytes, int size);

    string Request_GetAsString(int what);

    string Request_GetCookiesAsString();

    int Request_GetTotalBytes();

    void Response_AddCookie(string name);

    void Response_AddHeader(string name, string value);

    void Response_AppendToLog(string entry);

    void Response_BinaryWrite(byte[] bytes, int size);

    void Response_Clear();

    void Response_End();

    void Response_Flush();

    string Response_GetCacheControl();

    string Response_GetCharSet();

    string Response_GetContentType();

    string Response_GetCookiesAsString();

    double Response_GetExpiresAbsolute();

    int Response_GetExpiresMinutes();

    int Response_GetIsBuffering();

    string Response_GetStatus();

    int Response_IsClientConnected();

    void Response_Pics(string value);

    void Response_Redirect(string url);

    void Response_SetCacheControl(string cacheControl);

    void Response_SetCharSet(string charSet);

    void Response_SetContentType(string contentType);

    void Response_SetCookieDomain(string name, string domain);

    void Response_SetCookieExpires(string name, double dtExpires);

    void Response_SetCookiePath(string name, string path);

    void Response_SetCookieSecure(string name, int secure);

    void Response_SetCookieSubValue(string name, string key, string value);

    void Response_SetCookieText(string name, string text);

    void Response_SetExpiresAbsolute(double dtExpires);

    void Response_SetExpiresMinutes(int expiresMinutes);

    void Response_SetIsBuffering(int isBuffering);

    void Response_SetStatus(string status);

    void Response_Write(string text);

    Object Server_CreateObject(string progId);

    void Server_Execute(string url);

    int Server_GetScriptTimeout();

    string Server_HTMLEncode(string str);

    string Server_MapPath(string logicalPath);

    void Server_SetScriptTimeout(int timeoutSeconds);

    void Server_Transfer(string url);

    string Server_URLEncode(string str);

    string Server_URLPathEncode(string str);

    void Session_Abandon();

    int Session_GetCodePage();

    string Session_GetContentsNames();

    Object Session_GetContentsObject(string name);

    string Session_GetID();

    int Session_GetLCID();

    string Session_GetStaticNames();

    Object Session_GetStaticObject(string name);

    int Session_GetTimeout();

    int Session_IsPresent();

    void Session_RemoveAllContentsObjects();

    void Session_RemoveContentsObject(string name);

    void Session_SetCodePage(int value);

    void Session_SetContentsObject(string name, Object obj);

    void Session_SetLCID(int value);

    void Session_SetTimeout(int value);
    #endregion
  }
}
