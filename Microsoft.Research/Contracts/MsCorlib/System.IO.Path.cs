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

using System.Diagnostics.Contracts;
using System;

namespace System.IO
{
  public class Path
  {
#if !SILVERLIGHT && !NETFRAMEWORK_3_5
    //
    // Summary:
    //     Combines an array of strings into a path.
    //
    // Parameters:
    //   paths:
    //     An array of parts of the path.
    //
    // Returns:
    //     The combined paths.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     One of the strings in the array contains one or more of the invalid characters
    //     defined in System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     One of the strings in the array is null.
    [Pure]
    public static string Combine(params string[] paths)
    {
      Contract.Requires(paths != null);
      Contract.Requires(Contract.ForAll(paths, path => path != null));
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Combines three strings into a path.
    //
    // Parameters:
    //   path1:
    //     The first path to combine.
    //
    //   path2:
    //     The second path to combine.
    //
    //   path3:
    //     The third path to combine.
    //
    // Returns:
    //     The combined paths.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path1, path2, or path3 contains one or more of the invalid characters defined
    //     in System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     path1, path2, or path3 is null.
    [Pure]
    public static string Combine(string path1, string path2, string path3)
    {
      Contract.Requires(path1 != null);
      Contract.Requires(path2 != null);
      Contract.Requires(path3 != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    //
    // Summary:
    //     Combines four strings into a path.
    //
    // Parameters:
    //   path1:
    //     The first path to combine.
    //
    //   path2:
    //     The second path to combine.
    //
    //   path3:
    //     The third path to combine.
    //
    //   path4:
    //     The fourth path to combine.
    //
    // Returns:
    //     The combined paths.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path1, path2, path3, or path4 contains one or more of the invalid characters
    //     defined in System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     path1, path2, path3, or path4 is null.
    [Pure]
    public static string Combine(string path1, string path2, string path3, string path4)
    {
      Contract.Requires(path1 != null);
      Contract.Requires(path2 != null);
      Contract.Requires(path3 != null);
      Contract.Requires(path4 != null);
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
#endif

    [Pure]
    public static string Combine(string path1, string path2)
    {
      Contract.Requires(path1 != null);
      Contract.Requires(path2 != null);
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length >= path2.Length);
      Contract.Ensures(!IsPathRooted(path2) || Contract.Result<string>() == path2);
      Contract.Ensures(IsPathRooted(path2) || Contract.Result<string>().Length >=  path1.Length + path2.Length);

      
      //This was wrong: Contract.Ensures(Contract.Result<string>().Length >= path1.Length + path2.Length);
      //MSDN: If path2 includes a root, path2 is returned

      return default(string);
    }
    [Pure]
    public static bool IsPathRooted(string path)
    {
      Contract.Ensures(!Contract.Result<bool>() || path.Length > 0);
      return default(bool);
    }
    [Pure]
    public static bool HasExtension(string path)
    {
      Contract.Ensures(!Contract.Result<bool>() || path.Length > 0);
      return default(bool);
    }
    [Pure]
    public static string GetPathRoot(string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>().Length <= path.Length);
      return default(string);
    }
    [Pure]
    public static string GetFileNameWithoutExtension(string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>().Length <= path.Length);
      return default(string);
    }
    [Pure]
    public static string GetFileName(string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>().Length <= path.Length);
      return default(string);
    }
    [Pure]
    public static string GetFullPath(string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      return default(string);
    }
    [Pure]
    public static string GetExtension(string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>().Length <= path.Length);
      return default(string);
    }
    [Pure]
    public static string GetDirectoryName(string path)
    {
      Contract.Ensures(path == null || Contract.Result<string>().Length <= path.Length);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs.");
      return default(string);
    }
    [Pure]
    public static string ChangeExtension(string path, string extension)
    {
      Contract.Ensures(path == null || Contract.Result<string>() != null);
      return default(string);
    }
#if !SILVERLIGHT
    [Pure]
    public static char[] GetInvalidFileNameChars()
    {
      Contract.Ensures(Contract.Result<char[]>() != null);
      return default(char[]);
    }
#endif
    [Pure]
    public static char[] GetInvalidPathChars()
    {
      Contract.Ensures(Contract.Result<char[]>() != null);
      return default(char[]);
    }

#if !SILVERLIGHT
    public static string GetRandomFileName()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
#endif

    public static string GetTempFileName()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length >= 4, @" length >= 4 since file name must end with '.TMP'");
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurs, such as no unique temporary file name is available. - or - This method was unable to create a temporary file.");
      return default(string);
    }
    public static string GetTempPath()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
  }
}
