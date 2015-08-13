// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
    [ContractClass(typeof(IOutputContracts))]
    public interface IOutput : ISimpleLineWriter
    {
        /// <summary>
        /// For general output that are suggestions to the user
        /// </summary>
        void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo);

        void EmitError(System.CodeDom.Compiler.CompilerError error);

        IFrameworkLogOptions LogOptions { get; }
    }


    #region Contracts for IOutput
    [ContractClassFor(typeof(IOutput))]
    internal abstract class IOutputContracts : IOutput
    {
        #region ISimpleLineWriter Members

        public void WriteLine(string format, params object[] args)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IOutput Members

        public void Suggestion(ClousotSuggestion.Kind type, string kind, APC pc, string suggestion, List<uint> causes, ClousotSuggestion.ExtraSuggestionInfo extraInfo)
        {
        }

        public void EmitError(System.CodeDom.Compiler.CompilerError error)
        {
            Contract.Requires(error != null);
        }

        public IFrameworkLogOptions LogOptions
        {
            get
            {
                Contract.Ensures(Contract.Result<IFrameworkLogOptions>() != null);

                return default(IFrameworkLogOptions);
            }
        }

        #endregion
    }
    #endregion
}
