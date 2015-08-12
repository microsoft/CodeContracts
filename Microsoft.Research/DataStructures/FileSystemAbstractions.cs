// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        private static extern int WNetGetUniversalName(
            string lpLocalPath,
            [MarshalAs(UnmanagedType.U4)] int dwInfoLevel,
            IntPtr lpBuffer,
            [MarshalAs(UnmanagedType.U4)] ref int lpBufferSize);

        private const int UNIVERSAL_NAME_INFO_LEVEL = 0x00000001;
        private const int REMOTE_NAME_INFO_LEVEL = 0x00000002;
        private const int ERROR_MORE_DATA = 234;
        private const int ERROR_NOT_CONNECTED = 2250;
        private const int ERROR_NO_NETWORK = 1222;
        private const int NOERROR = 0;

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
