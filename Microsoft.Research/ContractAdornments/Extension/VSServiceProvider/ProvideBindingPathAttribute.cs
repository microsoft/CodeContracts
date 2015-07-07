// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;

    /// <summary>
    /// This attribute registers a path that should be probed for candidate assemblies at assembly load time.
    /// 
    /// For example:
    ///   [...\VisualStudio\10.0\BindingPaths\{5C48C732-5C7F-40f0-87A7-05C4F15BC8C3}]
    ///     "$PackageFolder$"=""
    ///     
    /// This would register the "PackageFolder" (i.e. the location of the pkgdef file) as a directory to be probed
    /// for assemblies to load.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false)]
    internal sealed class ProvideBindingPathAttribute : RegistrationAttribute
    {
        private static string GetPathToKey(RegistrationContext context)
        {
            Guid componentGuid = GetAssemblyGuid(context.CodeBase);
            return string.Concat(@"BindingPaths\", componentGuid.ToString("B").ToUpperInvariant());
        }

        private static Guid GetAssemblyGuid(string codeBase)
        {
            string assemblyFile = new Uri(codeBase).LocalPath;
            Assembly assembly = Assembly.LoadFrom(codeBase);
            object[] attributesData = assembly.GetCustomAttributes(typeof(GuidAttribute), false);
            if (attributesData.Length == 0)
                throw new ArgumentException("The specified assembly did not contain a [Guid] attribute.");

            return new Guid(((GuidAttribute)attributesData[0]).Value);
        }

        public override void Register(RegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            using (Key childKey = context.CreateKey(GetPathToKey(context)))
            {
                childKey.SetValue(context.ComponentPath, string.Empty);
            }
        }

        public override void Unregister(RegistrationContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.RemoveKey(GetPathToKey(context));
        }
    }
}
