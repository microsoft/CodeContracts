// CodeContracts
// Copyright (c) Microsoft Corporation
// All rights reserved. 
// MIT License
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Microsoft.Contracts.Foxtrot
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Compiler;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using Microsoft.Contracts.Foxtrot.Properties;

    /// <summary>
    ///     A <see langword="sealed"/> class that reports error in case of any occurence of call to Code Contracts' methods.
    /// </summary>
    /// <remarks>
    ///     Functionality provided by this class is being used in Foxtrot (ccrewrite) to ensure that all calls
    ///     to Code Contracts' methods were rewritten. Otherwise "An assembly must be rewritten..." may be reported in
    ///     in case of rewriter failure.
    /// </remarks>
    public sealed class PostRewriteChecker : Inspector
    {
        /// <summary>
        ///     Holds a read-only reference to a stack which tracks visited members.
        /// </summary>
        private readonly Stack<Member> visitedMembers;

        /// <summary>
        ///     Holds a read-only reference to an error handler.
        /// </summary>
        private readonly Action<CompilerError> handleError;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PostRewriteChecker"/> class with
        ///     a reference to an action responsible for handling reported errors.
        /// </summary>
        /// <param name="handleError">
        ///     An action that will be called for each error occurred.
        /// </param>
        public PostRewriteChecker(Action<CompilerError> handleError)
        {
            Contract.Requires(handleError != null);

            this.handleError = handleError;
            this.visitedMembers = new Stack<Member>();
        }

        /// <summary>
        ///     A method that gets called for each property definition in analyzed assembly.
        /// </summary>
        /// <param name="member">
        ///     An object that represents a property.
        /// </param>
        public override void VisitProperty(Property member)
        {
            this.visitedMembers.Push(member);

            base.VisitProperty(member);

            this.visitedMembers.Pop();
        }

        /// <summary>
        ///     A method that gets called for each event definition in analyzed assembly.
        /// </summary>
        /// <param name="member">
        ///     An object that represents an event.
        /// </param>
        public override void VisitEvent(Event member)
        {
            this.visitedMembers.Push(member);

            base.VisitEvent(member);

            this.visitedMembers.Pop();
        }

        /// <summary>
        ///     A method that gets called for each method definition in analyzed assembly.
        /// </summary>
        /// <param name="member">
        ///     An object that represents a method.
        /// </param>
        public override void VisitMethod(Method member)
        {
            this.visitedMembers.Push(member);

            base.VisitMethod(member);

            this.visitedMembers.Pop();
        }

        /// <summary>
        ///     A method that gets called for each type's member binding used in analyzed assembly.
        /// </summary>
        /// <param name="binding">
        ///     An object that represents a member binding.
        /// </param>
        /// <remarks>
        ///     This method reports error for each call to Code Contracts' method which normally should be rewritten.
        /// </remarks>
        public override void VisitMemberBinding(MemberBinding binding)
        {
            base.VisitMemberBinding(binding);

            if (object.ReferenceEquals(binding, null) ||
                this.visitedMembers.Count == 0 ||
                object.ReferenceEquals(this.visitedMembers.Peek(), null))
            {
                return;
            }

            Method method = binding.BoundMember as Method;
            if (object.ReferenceEquals(method, null))
            {
                return;
            }

            // Ignore calls to the methods in types other than System.Diagnostics.Contracts.Contract.
            TypeNode declaringType = method.DeclaringType;
            if (!declaringType.Namespace.Matches(ContractNodes.ContractNamespace) ||
                !declaringType.Name.Matches(ContractNodes.ContractClassName))
            {
                return;
            }

            // Report errors only for methods being rewritten.
            Identifier methodName = method.Name;
            if (methodName.Matches(ContractNodes.RequiresName) ||
                methodName.Matches(ContractNodes.EnsuresName) ||
                methodName.Matches(ContractNodes.EnsuresOnThrowName) ||
                methodName.Matches(ContractNodes.OldName) ||
                methodName.Matches(ContractNodes.ResultName) ||
                methodName.Matches(ContractNodes.ValueAtReturnName) ||
                methodName.Matches(ContractNodes.EndContractBlockName))
            {
                string message = string.Format(Resources.Error_ContractNotRewritten_ContractName_MemberName, method.FullName, this.visitedMembers.Peek().FullName);
                this.handleError(new Error(1080, message, binding.SourceContext));
            }
        }
        
        /// <summary>
        ///     Object invariant method responsible for internal object state validation.
        /// </summary>
        /// <remarks>
        ///     This method is internally used by Code Contracts and cannot be called by the code directly.
        /// </remarks>
        [ContractInvariantMethod]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Required for code contracts.")]
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
            Justification = "Required for code contracts.")]
        private void Invariant()
        {
            Contract.Invariant(this.handleError != null);
            Contract.Invariant(this.visitedMembers != null);
        }
    }
}
