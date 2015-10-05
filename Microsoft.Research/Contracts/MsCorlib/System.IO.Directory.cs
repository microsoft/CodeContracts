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
using System.Collections.Generic;

namespace System.IO
{
#if !SILVERLIGHT_3_0
  // Summary:
  //     Specifies whether to search the current directory, or the current directory
  //     and all subdirectories.
  public enum SearchOption
  {
    // Summary:
    //     Includes only the current directory in a search operation.
    TopDirectoryOnly = 0,
    //
    // Summary:
    //     Includes the current directory and all its subdirectories in a search operation.
    //     This option includes reparse points such as mounted drives and symbolic links
    //     in the search.
    AllDirectories = 1,
  }
#endif

  public class Directory
  {
#if false
    public static void Delete(string path, bool recursive)
    {

    }
    public static void Delete(string path)
    {

    }
#endif
    public static void Move(string sourceDirName, string destDirName)
    {
      Contract.Requires(!String.IsNullOrEmpty(sourceDirName));
      Contract.Requires(!String.IsNullOrEmpty(destDirName));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An attempt was made to move a directory to a different volume. -or- destDirName already exists. -or- The sourceDirName and destDirName parameters refer to the same file or directory.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

    }
#if !SILVERLIGHT_5_0
    public static void SetCurrentDirectory(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length < 260);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
    }
#endif
    public static string GetCurrentDirectory()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      return default(string);
    }
    public static string GetDirectoryRoot(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(string);
    }
#if !SILVERLIGHT
    public static String[] GetLogicalDrives()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"An I/O error occurred.");
      return default(String[]);
    }
#endif

#if !SILVERLIGHT
    public static String[] GetFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
#endif
    public static String[] GetFileSystemEntries(string path, string searchPattern)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
    public static String[] GetFileSystemEntries(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length < 260);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
#if !SILVERLIGHT
    public static String[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
#endif
    public static String[] GetDirectories(string path, string searchPattern)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
    public static String[] GetDirectories(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), dir => dir != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
#if !SILVERLIGHT
    public static String[] GetFiles(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Requires(searchOption == SearchOption.AllDirectories || searchOption == SearchOption.TopDirectoryOnly);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
#endif
    public static String[] GetFiles(string path, string searchPattern)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(String[]);
    }
    public static String[] GetFiles(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Ensures(Contract.Result<string[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<string[]>(), file => file != null));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");
      return default(String[]);
    }

#if !SILVERLIGHT
    public static DateTime GetLastAccessTimeUtc(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#endif
    public static DateTime GetLastAccessTime(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#if !SILVERLIGHT
    public static void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

    }
    public static void SetLastAccessTime(string path, DateTime lastAccessTime)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
    }


    public static DateTime GetLastWriteTimeUtc(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#endif
    public static DateTime GetLastWriteTime(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }

#if !SILVERLIGHT
    public static void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
    }
    public static void SetLastWriteTime(string path, DateTime lastWriteTime)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
    }
#endif

#if !SILVERLIGHT
    public static DateTime GetCreationTimeUtc(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#endif

    public static DateTime GetCreationTime(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

      return default(DateTime);
    }
#if !SILVERLIGHT
    public static void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

    }
    public static void SetCreationTime(string path, DateTime creationTime)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.FileNotFoundException>(true, @"The specified path was not found.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");

    }
#endif

#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT_3_0 && !NETFRAMEWORK_3_5

    //
    // Summary:
    //     Returns an enumerable collection of directory names in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the directories
    //     in the directory specified by path.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     path is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateDirectories(string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), d => d != null));
      return null;
    }
    //
    // Summary:
    //     Returns an enumerable collection of directory names that match a search pattern
    //     in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the directories
    //     in the directory specified by path and that match the specified search pattern.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern) {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), d => d != null));
      return null;
    }

    //
    // Summary:
    //     Returns an enumerable collection of directory names that match a search pattern
    //     in a specified path, and optionally searches subdirectories.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    //   searchOption:
    //     One of the enumeration values that specifies whether the search operation
    //     should include only the current directory or should include all subdirectories.The
    //     default value is System.IO.SearchOption.TopDirectoryOnly.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the directories
    //     in the directory specified by path and that match the specified search pattern
    //     and option.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     searchOption is not a valid System.IO.SearchOption value.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateDirectories(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), d => d != null));
      return null;
    }

    //
    // Summary:
    //     Returns an enumerable collection of file names in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the files
    //     in the directory specified by path.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     path is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFiles(string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), f => f != null));
      return null;
    }
    //
    // Summary:
    //     Returns an enumerable collection of file names that match a search pattern
    //     in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the files
    //     in the directory specified by path and that match the specified search pattern.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
    {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), f => f != null));
      return null;
    }

    //
    // Summary:
    //     Returns an enumerable collection of file names that match a search pattern
    //     in a specified path, and optionally searches subdirectories.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    //   searchOption:
    //     One of the enumeration values that specifies whether the search operation
    //     should include only the current directory or should include all subdirectories.The
    //     default value is System.IO.SearchOption.TopDirectoryOnly.
    //
    // Returns:
    //     An enumerable collection of the full names (including paths) for the files
    //     in the directory specified by path and that match the specified search pattern
    //     and option.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     searchOption is not a valid System.IO.SearchOption value.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), f => f != null));
      return null;
    }

    //
    // Summary:
    //     Returns an enumerable collection of file-system entries in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    // Returns:
    //     An enumerable collection of file-system entries in the directory specified
    //     by path.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().
    //
    //   System.ArgumentNullException:
    //     path is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
      Contract.Requires(path != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), fs => fs != null));
      return null;
    }
    //
    // Summary:
    //     Returns an enumerable collection of file-system entries that match a search
    //     pattern in a specified path.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    // Returns:
    //     An enumerable collection of file-system entries in the directory specified
    //     by path and that match the specified search pattern.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern)
    {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), fs => fs != null));
      return null;
    }

    //
    // Summary:
    //     Returns an enumerable collection of file names and directory names that match
    //     a search pattern in a specified path, and optionally searches subdirectories.
    //
    // Parameters:
    //   path:
    //     The directory to search.
    //
    //   searchPattern:
    //     The search string to match against the names of directories in path.
    //
    //   searchOption:
    //     One of the enumeration values that specifies whether the search operation
    //     should include only the current directory or should include all subdirectories.The
    //     default value is System.IO.SearchOption.TopDirectoryOnly.
    //
    // Returns:
    //     An enumerable collection of file-system entries in the directory specified
    //     by path and that match the specified search pattern and option.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     path is a zero-length string, contains only white space, or contains invalid
    //     characters as defined by System.IO.Path.GetInvalidPathChars().- or -searchPattern
    //     does not contain a valid pattern.
    //
    //   System.ArgumentNullException:
    //     path is null.-or-searchPattern is null.
    //
    //   System.ArgumentOutOfRangeException:
    //     searchOption is not a valid System.IO.SearchOption value.
    //
    //   System.IO.DirectoryNotFoundException:
    //     path is invalid, such as referring to an unmapped drive.
    //
    //   System.IO.IOException:
    //     path is a file name.
    //
    //   System.IO.PathTooLongException:
    //     The specified path, file name, or combined exceed the system-defined maximum
    //     length. For example, on Windows-based platforms, paths must be less than
    //     248 characters and file names must be less than 260 characters.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //
    //   System.UnauthorizedAccessException:
    //     The caller does not have the required permission.
    public static IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, SearchOption searchOption)
    {
      Contract.Requires(path != null);
      Contract.Requires(searchPattern != null);
      Contract.Ensures(Contract.Result<IEnumerable<string>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<string>>(), fs => fs != null));
      return null;
    }
#endif

    public static bool Exists(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));

      return default(bool);
    }
    public static DirectoryInfo CreateDirectory(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.Requires(path.Length != 0);
      Contract.Ensures(Contract.Result<DirectoryInfo>() != null);
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(DirectoryInfo);
    }

#if !SILVERLIGHT
    public static DirectoryInfo GetParent(string path)
    {
      Contract.Requires(!String.IsNullOrEmpty(path));
      Contract.EnsuresOnThrow<System.IO.IOException>(true, @"path is a file name.");
      Contract.EnsuresOnThrow<System.IO.PathTooLongException>(true, @"The specified path, file name, or both exceed the system-defined maximum length.");
      Contract.EnsuresOnThrow<System.IO.DirectoryNotFoundException>(true, @"The specified path is invalid (for example, it is on an unmapped drive.");

      return default(DirectoryInfo);
    }
#endif

  }
}
