// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

/*============================================================
**
** 
**
** Implementation details of MSR library.
**
===========================================================*/
#define DEBUG // The behavior of this contract library should be consistent regardless of build type.

#if SILVERLIGHT
#define FEATURE_UNTRUSTED_CALLERS

#elif REDHAWK_RUNTIME

#elif BARTOK_RUNTIME

#else // CLR
#define FEATURE_LINK_DEMAND
#define FEATURE_UNTRUSTED_CALLERS
#define FEATURE_RELIABILITY_CONTRACTS
#define FEATURE_SERIALIZATION
#endif

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#if FEATURE_RELIABILITY_CONTRACTS
using System.Runtime.ConstrainedExecution;
#endif
#if FEATURE_UNTRUSTED_CALLERS
using System.Security;
using System.Security.Permissions;
#endif

namespace System.Diagnostics.Contracts
{
    public static partial class Contract
    {
        #region Private Methods

        /// <summary>
        /// This method is used internally to trigger a failure indicating to the "programmer" that he is using the interface incorrectly.
        /// It is NEVER used to indicate failure of actual contracts at runtime.
        /// </summary>
        static partial void AssertMustUseRewriter(ContractFailureKind kind, String contractKind)
        {
            // @TODO: localize this
            Internal.ContractHelper.TriggerFailure(kind, "Must use the rewriter when using Contract." + contractKind, null, null, null);
        }

        #endregion Private Methods

        #region Beta 1 Obsolete for testing
        /// <summary>
        /// Specifies a contract such that the expression <paramref name="condition"/> must be true before the enclosing method or property is invoked.
        /// </summary>
        /// <param name="condition">Boolean expression representing the contract.</param>
        /// <remarks>
        /// This call must happen at the beginning of a method or property before any other code.
        /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
        /// Use this form when you want a check in the release build and backward compatibility does not
        /// force you to throw a particular exception.  This is recommended for security-sensitive checks.
        /// </remarks>
        [Pure]
        [System.Diagnostics.DebuggerNonUserCode]
#if FEATURE_RELIABILITY_CONTRACTS
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        [Obsolete("use Requires and the appropriate runtime checking level, or use if-then-throw.")]
        public static void RequiresAlways(bool condition)
        {
            if (condition) return;
            var message = Internal.ContractHelper.RaiseContractFailedEvent(ContractFailureKind.Precondition, null, null, null);
            if (message == null) return;
            throw new ContractException(ContractFailureKind.Precondition, message, null, null, null);
        }
        /// <summary>
        /// Specifies a contract such that the expression <paramref name="condition"/> must be true before the enclosing method or property is invoked.
        /// </summary>
        /// <param name="condition">Boolean expression representing the contract.</param>
        /// <param name="userMessage">If it is not a constant string literal, then the contract may not be understood by tools.</param>
        /// <remarks>
        /// This call must happen at the beginning of a method or property before any other code.
        /// This contract is exposed to clients so must only reference members at least as visible as the enclosing method.
        /// Use this form when you want a check in the release build and backward compatibility does not
        /// force you to throw a particular exception.  This is recommended for security-sensitive checks.
        /// </remarks>
        [Pure]
        [System.Diagnostics.DebuggerNonUserCode]
#if FEATURE_RELIABILITY_CONTRACTS
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        [Obsolete("use Requires and the appropriate runtime checking level, or use if-then-throw.")]
        public static void RequiresAlways(bool condition, String userMessage)
        {
            if (condition) return;
            var message = Internal.ContractHelper.RaiseContractFailedEvent(ContractFailureKind.Precondition, userMessage, null, null);
            if (message == null) return;
            throw new ContractException(ContractFailureKind.Precondition, message, userMessage, null, null);
        }
        #endregion

        #region Failure Behavior

        /// <summary>
        /// Without contract rewriting, failing Assert/Assumes end up calling this method.
        /// Code going through the contract rewriter never calls this method. Instead, the rewriter produced failures call
        /// Internal.ContractHelper.RaiseContractFailedEvent, followed by Internal.ContractHelper.TriggerFailure.
        /// </summary>
        [SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Security.SecuritySafeCriticalAttribute")]
#if FEATURE_UNTRUSTED_CALLERS
        [SecuritySafeCritical]
#endif
        [System.Diagnostics.DebuggerNonUserCode]
#if FEATURE_RELIABILITY_CONTRACTS
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        static partial void ReportFailure(ContractFailureKind failureKind, String userMessage, String conditionText, Exception innerException)
        {
            if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
            {
                throw new ArgumentException("failureKind is not in range", "failureKind");
            }
            Contract.EndContractBlock();

            // displayMessage == null means: yes we handled it. Otherwise it is the localized failure message
            var displayMessage = Internal.ContractHelper.RaiseContractFailedEvent(failureKind, userMessage, conditionText, innerException);

            if (displayMessage == null) return;

            Internal.ContractHelper.TriggerFailure(failureKind, displayMessage, userMessage, conditionText, innerException);
        }

        /// <summary>
        /// Allows a managed application environment such as an interactive interpreter (IronPython) or a
        /// web browser host (Jolt hosting Silverlight in IE) to be notified of contract failures and 
        /// potentially "handle" them, either by throwing a particular exception type, etc.  If any of the
        /// event handlers sets the Cancel flag in the ContractFailedEventArgs, then the Contract class will
        /// not pop up an assert dialog box or trigger escalation policy.  Hooking this event requires 
        /// full trust.
        /// </summary>
        public static event EventHandler<ContractFailedEventArgs> ContractFailed
        {
#if FEATURE_UNTRUSTED_CALLERS
            [SecurityCritical]
#if FEATURE_LINK_DEMAND
            [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
#endif
#endif
            add
            {
                Internal.ContractHelper.InternalContractFailed += value;
            }
#if FEATURE_UNTRUSTED_CALLERS
            [SecurityCritical]
#if FEATURE_LINK_DEMAND
            [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
#endif
#endif
            remove
            {
                Internal.ContractHelper.InternalContractFailed -= value;
            }
        }

        #endregion FailureBehavior
    }

    public sealed class ContractFailedEventArgs : EventArgs
    {
        private ContractFailureKind _failureKind;
        private String _message;
        private String _condition;
        private Exception _originalException;
        private bool _handled;
        private bool _unwind;

        internal Exception thrownDuringHandler;

#if FEATURE_RELIABILITY_CONTRACTS
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        public ContractFailedEventArgs(ContractFailureKind failureKind, String message, String condition, Exception originalException)
        {
            Contract.Requires(originalException == null || failureKind == ContractFailureKind.PostconditionOnException);
            _failureKind = failureKind;
            _message = message;
            _condition = condition;
            _originalException = originalException;
        }

        public String Message { get { return _message; } }
        public String Condition { get { return _condition; } }
        public ContractFailureKind FailureKind { get { return _failureKind; } }
        public Exception OriginalException { get { return _originalException; } }

        // Whether the event handler "handles" this contract failure, or to fail via escalation policy.
        public bool Handled
        {
            get { return _handled; }
        }

#if FEATURE_UNTRUSTED_CALLERS
        [SecurityCritical]
#if FEATURE_LINK_DEMAND
        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
#endif
#endif
        public void SetHandled()
        {
            _handled = true;
        }

        public bool Unwind
        {
            get { return _unwind; }
        }

#if FEATURE_UNTRUSTED_CALLERS
        [SecurityCritical]
#if FEATURE_LINK_DEMAND
        [SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
#endif
#endif
        public void SetUnwind()
        {
            _unwind = true;
        }

        internal void SetUnwindNoDemand()
        {
            _unwind = true;
        }
    }

#if FEATURE_SERIALIZATION
    [Serializable]
#else
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors")]
#endif
    [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic")]
    internal sealed class ContractException : Exception
    {
        private readonly ContractFailureKind _Kind;
        private readonly string _UserMessage;
        private readonly string _Condition;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public ContractFailureKind Kind { get { return _Kind; } }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public string Failure { get { return this.Message; } }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public string UserMessage { get { return _UserMessage; } }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public string Condition { get { return _Condition; } }

        public ContractException() { }

#if false
        public ContractException(string msg) : base(msg)
        {
            _Kind = ContractFailureKind.Precondition;
        }

        public ContractException(string msg, Exception inner)
            : base(msg, inner)
        {
        }
#endif

        public ContractException(ContractFailureKind kind, string failure, string userMessage, string condition, Exception innerException)
            : base(failure, innerException)
        {
            _Kind = kind;
            _UserMessage = userMessage;
            _Condition = condition;
        }

#if FEATURE_SERIALIZATION
        private ContractException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            _Kind = (ContractFailureKind)info.GetInt32("Kind");
            _UserMessage = info.GetString("UserMessage");
            _Condition = info.GetString("Condition");
        }

#if FEATURE_UNTRUSTED_CALLERS
        [SecurityCritical]
#if FEATURE_LINK_DEMAND
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif
#endif
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("Kind", _Kind);
            info.AddValue("UserMessage", _UserMessage);
            info.AddValue("Condition", _Condition);
        }
#endif
    }
}


namespace System.Diagnostics.Contracts.Internal
{
    public static partial class ContractHelper
    {
        #region Private fields

        private static EventHandler<ContractFailedEventArgs> contractFailedEvent;
        private static readonly Object lockObject = new Object();
        private static System.Resources.ResourceManager myResourceManager = new System.Resources.ResourceManager("mscorlib", typeof(Object).Assembly);

        #endregion

        /// <summary>
        /// Allows a managed application environment such as an interactive interpreter (IronPython) or a
        /// web browser host (Jolt hosting Silverlight in IE) to be notified of contract failures and 
        /// potentially "handle" them, either by throwing a particular exception type, etc.  If any of the
        /// event handlers sets the Cancel flag in the ContractFailedEventArgs, then the Contract class will
        /// not pop up an assert dialog box or trigger escalation policy.  Hooking this event requires 
        /// full trust.
        /// </summary>
        internal static event EventHandler<ContractFailedEventArgs> InternalContractFailed
        {
#if FEATURE_UNTRUSTED_CALLERS
            [SecurityCritical]
#endif
            add
            {
                // Eagerly prepare each event handler _marked with a reliability contract_, to 
                // attempt to reduce out of memory exceptions while reporting contract violations.
                // This only works if the new handler obeys the constraints placed on 
                // constrained execution regions.  Eagerly preparing non-reliable event handlers
                // would be a perf hit and wouldn't significantly improve reliability.
                // UE: Please mention reliable event handlers should also be marked with the 
                // PrePrepareMethodAttribute to avoid CER eager preparation work when ngen'ed.
#if FEATURE_RELIABILITY_CONTRACTS
                System.Runtime.CompilerServices.RuntimeHelpers.PrepareDelegate(value);
#endif
                lock (lockObject)
                {
                    contractFailedEvent += value;
                }
            }
#if FEATURE_UNTRUSTED_CALLERS
            [SecurityCritical]
#endif
            remove
            {
                lock (lockObject)
                {
                    contractFailedEvent -= value;
                }
            }
        }

        /// <summary>
        /// Rewriter will call this method on a contract failure to allow listeners to be notified.
        /// The method should not perform any failure (assert/throw) itself.
        /// This method has 3 functions:
        /// 1. Call any contract hooks (such as listeners to Contract failed events)
        /// 2. Determine if the listeneres deem the failure as handled (then resultFailureMessage should be set to null)
        /// 3. Produce a localized resultFailureMessage used in advertising the failure subsequently.
        /// </summary>
        /// <param name="resultFailureMessage">Should really be out (or the return value), but partial methods are not flexible enough.
        /// On exit: null if the event was handled and should not trigger a failure.
        ///          Otherwise, returns the localized failure message</param>
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [System.Diagnostics.DebuggerNonUserCode]
        static partial void RaiseContractFailedEventImplementation(ContractFailureKind failureKind, String userMessage, String conditionText, Exception innerException, ref string resultFailureMessage)
        {
            if (failureKind < ContractFailureKind.Precondition || failureKind > ContractFailureKind.Assume)
                throw new ArgumentException("failureKind is not in range", "failureKind");
            Contract.EndContractBlock();

            String displayMessage = "contract failed.";  // Incomplete, but in case of OOM during resource lookup...
            ContractFailedEventArgs eventArgs = null;  // In case of OOM.
            string returnValue;
#if FEATURE_RELIABILITY_CONTRACTS
            // on ASP.NET with medium trust, this causes a security exception.
            //System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
#endif
            try
            {
                displayMessage = GetDisplayMessage(failureKind, userMessage, conditionText);
                if (contractFailedEvent != null)
                {
                    eventArgs = new ContractFailedEventArgs(failureKind, displayMessage, conditionText, innerException);
                    foreach (EventHandler<ContractFailedEventArgs> handler in contractFailedEvent.GetInvocationList())
                    {
                        try
                        {
                            handler(null, eventArgs);
                        }
                        catch (Exception e)
                        {
                            eventArgs.thrownDuringHandler = e;
                            eventArgs.SetUnwindNoDemand(); // avoid security exception on asp.net
                        }
                    }
                    if (eventArgs.Unwind)
                    {
                        // unwind
                        if (innerException == null) { innerException = eventArgs.thrownDuringHandler; }
                        throw new ContractException(failureKind, displayMessage, userMessage, conditionText, innerException);
                    }
                }
            }
            finally
            {
                if (eventArgs != null && eventArgs.Handled)
                {
                    returnValue = null; // handled
                }
                else
                {
                    returnValue = displayMessage;
                }
            }
            resultFailureMessage = returnValue;
        }

        /// <summary>
        /// Rewriter calls this method to get the default failure behavior.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "conditionText")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "userMessage")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "kind")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "innerException")]
        [System.Diagnostics.DebuggerNonUserCode]
        static partial void TriggerFailureImplementation(ContractFailureKind kind, String displayMessage, String userMessage, String conditionText, Exception innerException)
        {
#if FEATURE_RELIABILITY_CONTRACTS
            if (!Environment.UserInteractive)
            {
                Environment.FailFast(displayMessage);
            }
#endif
            Debug.Assert(false, displayMessage);
        }

#if FEATURE_RELIABILITY_CONTRACTS
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
#endif
        private static String GetDisplayMessage(ContractFailureKind failureKind, String userMessage, String conditionText)
        {
            String resourceName = null;
            switch (failureKind)
            {
                case ContractFailureKind.Assert:
                    resourceName = "AssertionFailed";
                    break;

                case ContractFailureKind.Assume:
                    resourceName = "AssumptionFailed";
                    break;

                case ContractFailureKind.Precondition:
                    resourceName = "PreconditionFailed";
                    break;

                case ContractFailureKind.Postcondition:
                    resourceName = "PostconditionFailed";
                    break;

                case ContractFailureKind.Invariant:
                    resourceName = "InvariantFailed";
                    break;

                case ContractFailureKind.PostconditionOnException:
                    resourceName = "PostconditionOnExceptionFailed";
                    break;

                default:
                    Contract.Assume(false, "Unreachable code");
                    resourceName = "AssumptionFailed";
                    break;
            }
            var failureMessage = myResourceManager.GetString(resourceName);
            if (failureMessage == null)
            { // Hack for pre-V4 CLR�s
                failureMessage = String.Format(System.Globalization.CultureInfo.CurrentUICulture, "{0} failed", failureKind);
            }
            // Now format based on presence of condition/userProvidedMessage
            if (!String.IsNullOrEmpty(conditionText))
            {
                if (!String.IsNullOrEmpty(userMessage))
                {
                    // both != null
                    return String.Format(System.Globalization.CultureInfo.CurrentUICulture, "{0}: {1} {2}", failureMessage, conditionText, userMessage);
                }
                else
                {
                    // condition != null, userProvidedMessage == null
                    return String.Format(System.Globalization.CultureInfo.CurrentUICulture, "{0}: {1}", failureMessage, conditionText);
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(userMessage))
                {
                    // condition null, userProvidedMessage != null
                    return String.Format(System.Globalization.CultureInfo.CurrentUICulture, "{0}: {1}", failureMessage, userMessage);
                }
                else
                {
                    // both null
                    return failureMessage;
                }
            }
        }
    }
}  // namespace System.Diagnostics.Contracts.Internal

