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

// File System.Web.Profile.ProfileProvider.cs
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


namespace System.Web.Profile
{
  abstract public partial class ProfileProvider : System.Configuration.SettingsProvider
  {
    #region Methods and constructors
    public abstract int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

    public abstract int DeleteProfiles(ProfileInfoCollection profiles);

    public abstract int DeleteProfiles(string[] usernames);

    public abstract ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

    public abstract ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

    public abstract ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

    public abstract ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords);

    public abstract int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

    protected ProfileProvider()
    {
    }
    #endregion
  }

  #region ProfileProvider contract binding
  [ContractClass(typeof(ProfileProviderContract))]
  public partial class ProfileProvider {

  }

  [ContractClassFor(typeof(ProfileProvider))]
  abstract class ProfileProviderContract : ProfileProvider {
    public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate) {
      throw new NotImplementedException();
    }

    public override int DeleteProfiles(ProfileInfoCollection profiles) {
      throw new NotImplementedException();
    }

    public override int DeleteProfiles(string[] usernames) {
      Contract.Requires(usernames != null);
      Contract.Requires(0 < usernames.Length);
      Contract.Requires(Contract.ForAll(0, usernames.Length, i => usernames[i] != null));
      throw new NotImplementedException();
    }

    public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords) {
      Contract.Requires(!String.IsNullOrEmpty(usernameToMatch)); // actually cannot be all white space
      throw new NotImplementedException();
    }

    public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
      Contract.Requires(!String.IsNullOrEmpty(usernameToMatch)); // actually cannot be all white space
      Contract.Requires(0 <= pageIndex);
      Contract.Requires(1 <= pageSize);
      throw new NotImplementedException();
    }

    public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords) {
      Contract.Requires(0 <= pageIndex);
      throw new NotImplementedException();
    }

    public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords) {
      Contract.Requires(0 <= pageIndex);
      Contract.Requires(1 <= pageSize);
      throw new NotImplementedException();
    }

    public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate) {
      throw new NotImplementedException();
    }

    public override string ApplicationName {
      get {
        Contract.Ensures(Contract.Result<string>().Length <= 256);
        throw new NotImplementedException();
      }
      set {
        Contract.Requires(value.Length <= 256);
        throw new NotImplementedException();
      }
    }

    public override Configuration.SettingsPropertyValueCollection GetPropertyValues(Configuration.SettingsContext context, Configuration.SettingsPropertyCollection collection) {
      throw new NotImplementedException();
    }

    public override void SetPropertyValues(Configuration.SettingsContext context, Configuration.SettingsPropertyValueCollection collection) {
      throw new NotImplementedException();
    }
  }
  #endregion

}
