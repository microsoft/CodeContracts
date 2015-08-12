// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Some helper classes for tracing the behavior of the abstract domain, and the definition of the exception to be thrown when some error occured in the abstract domains

namespace Microsoft.Research.AbstractDomains
{
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.CodeDom.Compiler;
    using System.Xml;
    using System.Runtime.InteropServices;
    using Microsoft.Research.CodeAnalysis;
    using System.Diagnostics.Contracts;



    /// <summary>
    /// A class that does nothing....
    /// </summary>
#if SUBPOLY_ONLY
    internal
#else
    public
#endif
    class NullTextWriter : TextWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }

    /// <summary>
    /// Exception to be thrown when an error occured in the abstract domains
    /// </summary>
    [Serializable]
    public class AbstractInterpretationException : Exception
    {
        public AbstractInterpretationException(string msg)
          : base(msg)
        {
            Contract.Requires(msg != null);
        }

        public AbstractInterpretationException()
          : base()
        {
        }
    }

    /// <summary>
    /// Exception to be thrown when there is something not implemented yet
    /// </summary>
    [Serializable]
    public class AbstractInterpretationTODOException : Exception
    {
        public AbstractInterpretationTODOException(string msg)
          : base(msg)
        {
            Contract.Requires(msg != null);
        }

        public AbstractInterpretationTODOException()
          : base()
        {
        }
    }

#if SUBPOLY_ONLY
    public class TimeoutExceptionFixpointComputation : Exception
    {
    }
#endif
}