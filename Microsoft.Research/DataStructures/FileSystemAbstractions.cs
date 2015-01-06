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
using System.Runtime.InteropServices;
using System.Management;
using System.Text;

namespace Microsoft.Research.DataStructures
{
  public class FileSystemAbstractions
  {
    [DllImport("mpr.dll", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.U4)]
    static extern int WNetGetUniversalName(
        string lpLocalPath,
        [MarshalAs(UnmanagedType.U4)] int dwInfoLevel,
        IntPtr lpBuffer,
        [MarshalAs(UnmanagedType.U4)] ref int lpBufferSize);

    const int UNIVERSAL_NAME_INFO_LEVEL = 0x00000001;
    const int REMOTE_NAME_INFO_LEVEL = 0x00000002;
    const int ERROR_MORE_DATA = 234;
    const int ERROR_NOT_CONNECTED = 2250;
    const int ERROR_NO_NETWORK = 1222;
    const int NOERROR = 0;

    public static bool TryGetUniversalName(string localPath, out string result)
    {
      result = null;

      if (String.IsNullOrEmpty(localPath))
      {
        return false;
      }

      var buffer = IntPtr.Zero;

      try
      {
        // First, call WNetGetUniversalName to get the size.
        int size = 0;

        // Make the call.
        // Pass IntPtr.Size because the API doesn't like null, even though
        // size is zero.  We know that IntPtr.Size will be
        // aligned correctly.
        int apiRetVal = WNetGetUniversalName(localPath, UNIVERSAL_NAME_INFO_LEVEL, (IntPtr)IntPtr.Size, ref size);

        // Local Drive
        if (apiRetVal == ERROR_NOT_CONNECTED || apiRetVal == ERROR_NO_NETWORK)
        {
          return TryGetLocalUniversalName(localPath, out result);
        }

        if (apiRetVal == 1200 && localPath.StartsWith(@"\\"))
        {
          result = localPath;
          return true;
        }

        size = 1024;

        // Allocate the memory.
        buffer = Marshal.AllocCoTaskMem(size);

        // Now make the call.
        apiRetVal = WNetGetUniversalName(localPath, UNIVERSAL_NAME_INFO_LEVEL, buffer, ref size);

        // If it didn't succeed, then throw.
        if (apiRetVal != NOERROR)
          return false;

        // Now get the string.  It's all in the same buffer, but
        // the pointer is first, so offset the pointer by IntPtr.Size
        // and pass to PtrToStringAnsi.
        result = Marshal.PtrToStringAuto(new IntPtr(buffer.ToInt64() + IntPtr.Size), size);
        result = result.Substring(0, result.IndexOf('\0'));
      }
      finally
      {
        // Release the buffer.
        Marshal.FreeCoTaskMem(buffer);
      }

      return true;
    }

    static private bool TryGetLocalUniversalName(string localpath, out string result)
    {
      localpath = localpath.ToLower();
      result = null;
      var shares = new ManagementClass("Win32_Share").GetInstances();

      //System.Diagnostics.Debugger.Break();

      foreach (var share in shares)
      {
        var path = share["Path"].ToString().ToLower();
        if (!String.IsNullOrEmpty(path) && localpath.StartsWith(path))
        {
          var name = share["Name"].ToString();
          var caption = share["Caption"].ToString();
          var rest = localpath.Substring(path.Length);
          result = String.Format(@"\\{0}\{1}\{2}", Environment.MachineName, name, rest);

          return true;
        }
      }
      return result != null;
    }

  }
}
