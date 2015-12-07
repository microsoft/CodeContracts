// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Text.RegularExpressions;

using Microsoft.Research.CodeAnalysis;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;
using Microsoft.Research.CodeAnalysis.Properties;
using System.Diagnostics.CodeAnalysis;


/*
 * Outputs inferred contracts under C# form for reuse
*/

namespace Microsoft.Research.CodeAnalysis.OutputPrettyCS
{
    /// <summary>
    /// Accessibility information for fields, methods, class, ...
    /// </summary>
    public enum Access
    {
        NONE,
        PUBLIC,
        PROTECTED,
        PRIVATE,
        INTERNAL,
        INTERNAL_AND_OR_PROTECTED,
    }

    /// <summary>
    /// All the indentation and newline stuff is handled here in the OutputHelper
    /// object associated to the current context.
    /// So, just use oh.OutputEOL when End Of Line is needed, and Output when
    /// regular text must be output. All the indentation is automatically taken
    /// care of.
    /// </summary>
    public class OutputHelper
    {
        public enum OutputStrategy
        {
            ONE_FILE_PER_NAMESPACE,
            ONE_FILE_PER_TOPLEVEL_CLASS,
            ONE_FILE_PER_CLASS
        }

        /// <summary>
        /// Symbol for tabulations and EOL. Change it here only.
        /// </summary>
        private const string mTabSymbol = "  "; // 2 spaces
        private const string mEOLSymbol = "\r\n";

        /// <param name="output">The stream where the IndentHelper will output its commands</param>
        public OutputHelper(OutputStrategy Strategy, string ProjectFolder, string RelativeFilesPath, List<string> mUsings, IOutput mDebugOutput, bool verbose)
        {
            Contract.Requires(ProjectFolder != null);
            Contract.Requires(RelativeFilesPath != null);
            Contract.Requires(mDebugOutput != null);

            mStrategy = Strategy;
            mCurIndent = 0;
            mOutput = null;
            mbEOL = false;
            this.mDebugOutput = mDebugOutput;
            mProjectFolder = ProjectFolder;
            mRelativeFilesPath = RelativeFilesPath;
            mCurrentFileName = "";
            mWrittenFiles = new List<string>();
            mWrittenFileSet = new Dictionary<string, string>();
            mVerbose = verbose;

            this.mUsings = mUsings;
        }

        public List<string> WrittenFiles { get { return mWrittenFiles; } }
        public OutputStrategy Strategy { get { return mStrategy; } }
        public List<string> Usings { get { return mUsings; } }

        #region Indentation control
        /// <summary>
        /// Adds one indentation level
        /// </summary>
        public void IncrIndent()
        {
            ++mCurIndent;
        }

        /// <summary>
        /// Removes one indentation level
        /// </summary>
        public void DecrIndent()
        {
            if (mCurIndent > 0)
                --mCurIndent;
            else
                throw new InvalidOperationException("Cannot DecrIndent, already decremented.");
        }
        #endregion

        #region Output control
        /// <summary>
        /// Outputs only an End Of Line character
        /// </summary>
        public void OutputEOL()
        {
            mOutput.Write(mEOLSymbol);
            mbEOL = true;
        }

        public void BeginRegion(string name)
        {
            Output("#region " + name, true);
        }

        public void EndRegion()
        {
            Output("#endregion", true);
        }

        /// <summary>
        /// Output only the leading indentation in the current context (only for manual handling, otherwise use Output)
        /// </summary>
        public void OutputIndent()
        {
            StringBuilder sb = new StringBuilder();
            sb.EnsureCapacity(mTabSymbol.Length * mCurIndent);
            for (int i = 0; i < mCurIndent; ++i)
                sb.Append(mTabSymbol);

            mOutput.Write(sb.ToString());
            mbEOL = false;
        }

        public void OutputEOLAndIndent()
        {
            OutputEOL();
            OutputIndent();
        }

        public void Output(string str, bool eol_after)
        {
            if (mbEOL)
                OutputIndent();

            mOutput.Write(str);
            mbEOL = false;

            if (eol_after)
                OutputEOL();
        }

        public void DebugOutput(string str)
        {
            Contract.Requires(str != null);

            mDebugOutput.WriteLine(str);
        }
        #endregion

        public void ForceNewFile(string file)
        {
            Contract.Requires(file != null);

            StartNewFile(file);
        }

        private void StartNewFile(string file)
        {
            Contract.Requires(file != null);
            EndFile();
            var path = Path.Combine(mRelativeFilesPath, file);

            var existingFile = false;
            if (mWrittenFileSet.ContainsKey(path.ToLower()))
            {
                //Console.WriteLine("Appending the file \"{0}\"!", path);
                existingFile = true;
            }

            mCurrentFileName = path;

            string fullpath = Path.Combine(mProjectFolder, path);
            try
            {
                mOutput = new StreamWriter(fullpath, existingFile);

                if (!existingFile)
                {
                    Output("// File " + file, true);
                    Output("// Automatically generated contract file.", true);
                    // Using: depending on what we simplify in SimplifyType
                    foreach (var use in mUsings)
                        Output("using " + use + ";", true);

                    OutputEOL();
                    Output("// Disable the \"this variable is not used\" warning as every field would imply it.", true);
                    Output("#pragma warning disable 0414", true);
                    Output("// Disable the \"this variable is never assigned to\".", true);
                    Output("#pragma warning disable 0067", true);
                    Output("// Disable the \"this event is never assigned to\".", true);
                    Output("#pragma warning disable 0649", true);
                    Output("// Disable the \"this variable is never used\".", true);
                    Output("#pragma warning disable 0169", true);
                    Output("// Disable the \"new keyword not required\" warning.", true);
                    Output("#pragma warning disable 0109", true);
                    Output("// Disable the \"extern without DllImport\" warning.", true);
                    Output("#pragma warning disable 0626", true);
                    Output("// Disable the \"could hide other member\" warning, can happen on certain properties.", true);
                    Output("#pragma warning disable 0108", true);
                    OutputEOL();
                    OutputEOL();
                }
            }
            catch (PathTooLongException)
            {
                mDebugOutput.WriteLine("OutputPretty C#: Unable to write file " + fullpath + ": Path is too long.");
                throw;
            }
            catch (IOException)
            {
                mDebugOutput.WriteLine("OutputPretty C#: Unable to write file " + fullpath + ": file may be readonly or disk may be full.");
                throw;
            }
            catch (ArgumentException)
            {
                mDebugOutput.WriteLine("OutputPretty C#: Unable to write file " + fullpath + ": character in the path is not allowed.");
                throw;
            }
        }

        public void EndFile()
        {
            if (mOutput != null)
            {
                if (mVerbose)
                {
                    mDebugOutput.WriteLine("OutputPretty C#: Inferred contracts successfully output in file: " + mCurrentFileName);
                }
                mOutput.Close();
                mOutput = null;
                var key = mCurrentFileName.ToLower();
                if (!mWrittenFileSet.ContainsKey(key))
                {
                    mWrittenFileSet.Add(key, mCurrentFileName);
                    mWrittenFiles.Add(mCurrentFileName);
                }
            }

            mCurIndent = 0;
            mbEOL = false;
            mCurrentFileName = "";
        }

        public void BeginNamespace(Namespace ns)
        {
            if (mStrategy == OutputStrategy.ONE_FILE_PER_NAMESPACE)
            {
                string name = ns.Name;
                if (name == "<toplevel>")
                    name = "toplevel";
                StartNewFile(name + ".cs");
            }
        }

        public void EndNamespace()
        {
            if (mStrategy == OutputStrategy.ONE_FILE_PER_NAMESPACE)
                EndFile();
        }

        public void BeginClass(Class cl)
        {
            if (mStrategy == OutputStrategy.ONE_FILE_PER_CLASS
              || (mStrategy == OutputStrategy.ONE_FILE_PER_TOPLEVEL_CLASS && cl.Parent == null))
            {
                string filename = cl.FullName;

                // postfix the filename with the number of formal parameters to allow for types
                // with the same name but different formal parameters
                int nb_formal_param = cl.ILFormalTypeParameterCount;
                if (nb_formal_param > 0)
                    filename += "_" + nb_formal_param;

                filename += ".cs";

                StartNewFile(filename);
            }
        }

        public void EndClass(Class cl)
        {
            if (mStrategy == OutputStrategy.ONE_FILE_PER_CLASS
              || (mStrategy == OutputStrategy.ONE_FILE_PER_TOPLEVEL_CLASS && cl.Parent == null))
                EndFile();
        }

        #region Privates
        readonly private bool mVerbose = false;
        readonly private OutputStrategy mStrategy;
        private int mCurIndent;
        private StreamWriter mOutput; /// Current destination
        readonly private string mProjectFolder;
        readonly private string mRelativeFilesPath;
        private bool mbEOL; /// Last thing output was a EOL
        readonly private IOutput mDebugOutput;
        readonly private List<string> mWrittenFiles;
        readonly private Dictionary<string, string> mWrittenFileSet;
        private string mCurrentFileName;
        readonly private List<string> mUsings; /// Using assemblies at the beginning of the output files (set in constructor)
                                               /// 
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(mCurIndent >= 0);
            Contract.Invariant(mRelativeFilesPath != null);
            Contract.Invariant(mProjectFolder != null);
        }

        #endregion Privates
    }

    [ContractVerification(false)]
    public class Namespace
    {
        public Namespace(string name)
        {
            mName = name;
            mChildren = new List<ElementBase>();
        }

        public void OpenNamespace(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            oh.BeginNamespace(this);
            if (mName != "<toplevel>")
            {
                oh.Output("namespace " + mName, true);
                oh.Output("{", true);

                oh.IncrIndent();
            }
        }

        public void CloseNamespace(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            if (mName != "<toplevel>")
            {
                oh.DecrIndent();

                oh.Output("}", true);
            }
            oh.EndNamespace();
        }

        public void Output(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            if (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
                OpenNamespace(oh);

            // Sort the children alphabetically
            mChildren.Sort(new ElementBaseComparer());

            bool first = true;
            bool bOutputOtherChildren = false;
            foreach (var child in mChildren)
            {
                if (child.IsClass)
                {
                    Class cl = child as Class;

                    Contract.Assume(cl != null);

                    if (cl.Parent == null
                      || (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS))
                    {
                        if (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
                        {
                            if (first)
                                first = false;
                            else
                                oh.OutputEOL();
                        }
                        cl.Output(oh);
                    }
                }
                else
                {
                    if (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
                    {
                        if (first)
                            first = false;
                        else
                            oh.OutputEOL();
                        child.Output(oh);
                    }
                    else
                        bOutputOtherChildren = true;
                }
            }

            if (oh.Strategy != OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE && bOutputOtherChildren)
            {
                string name = mName;
                if (name == "<toplevel>")
                    name = "toplevel";
                oh.ForceNewFile(name + ".cs");
                OpenNamespace(oh);
                first = true;
                foreach (var child in mChildren)
                {
                    if (!child.IsClass)
                    {
                        if (first)
                            first = false;
                        else
                            oh.OutputEOL();
                        child.Output(oh);
                    }
                }
                CloseNamespace(oh);
                oh.EndFile();
            }

            if (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
                CloseNamespace(oh);
        }

        public void AddChild(ElementBase child)
        {
            if (!mChildren.Contains(child))
                mChildren.Add(child);
        }

        public bool Empty()
        {
            return mChildren.Count == 0;
        }

        public bool HasUnsafeCode()
        {
            foreach (var ch in mChildren)
            {
                if (ch.HasUnsafeCode())
                    return true;
            }
            return false;
        }

        public virtual string Name { get { return mName; } }

        #region Privates
        /// <summary>
        /// Namespace name. Can be empty for toplevel
        /// </summary>
        private string mName;

        /// <summary>
        /// Objects in the namespace
        /// </summary>
        private List<ElementBase> mChildren;
        #endregion
    }

    public class ElementBaseComparer : IComparer<ElementBase>
    {
        public int Compare(ElementBase e1, ElementBase e2)
        {
            return e1.Name.CompareTo(e2.Name);
        }
    }

    public abstract class ElementBase
    {
        public abstract void Output(OutputHelper oh);

        public virtual string Name { get { throw new NotImplementedException(); } }

        public virtual bool IsClass { get { return false; } }
        public virtual bool IsMethod { get { return false; } }
        public virtual bool IsProperty { get { return false; } }
        public virtual bool IsEvent { get { return false; } }
        public virtual bool IsField { get { return false; } }
        public virtual bool IsEnum { get { return false; } }
        public virtual bool IsDelegate { get { return false; } }

        public virtual bool IsUnsafe { get { return false; } }
        // Classes may have unsafe code but not declared as unsafe
        public virtual bool HasUnsafeCode() { return IsUnsafe; }
    }

    [ContractVerification(false)]
    public class Class : ElementBase
    {
        public enum ClassType
        {
            CLASS,
            STRUCT,
            INTERFACE,
        }

        public Class(Access mAccess, bool mbIsAbstract, bool mbIsSealed, bool mbIsStatic, bool mbIsUnsafe, ClassType mType, string mName, Class mParent, string mBaseClass, Namespace mParentNS)
        {
            this.mAccess = mAccess;
            this.mbIsAbstract = mbIsAbstract;
            this.mbIsSealed = mbIsSealed;
            this.mbIsStatic = mbIsStatic;
            this.mbIsUnsafe = mbIsUnsafe;
            this.mType = mType;
            this.mName = mName;
            this.mParent = mParent;
            this.mBaseClass = mBaseClass;
            this.mParentNS = mParentNS;

            mFormals = new List<FormalTypeParameter>();
            mInterfaces = new List<string>();
            mChildren = new List<ElementBase>();
            mInvariants = new List<string>();
            mAttributes = new List<string>();
        }

        public override bool IsClass { get { return true; } }
        public override bool IsUnsafe { get { return mbIsUnsafe; } }

        public int ILFormalTypeParameterCount
        {
            get
            {
                var result = (this.FormalTypeParameters == null) ? 0 : this.FormalTypeParameters.Count;
                if (this.Parent != null) result += this.Parent.ILFormalTypeParameterCount;
                return result;
            }
        }
        public ClassType Type { get { return mType; } }
        public List<FormalTypeParameter> FormalTypeParameters { get { return mFormals; } }

        public override string Name { get { return mName; } }
        public string FullName
        {
            get
            {
                string fname = "";
                if (mParent == null)
                {
                    if (mParentNS.Name != "<toplevel>")
                        fname += mParentNS.Name + ".";
                }
                else
                    fname += mParent.FullName + ".";
                fname += mName;
                return fname;
            }
        }
        public Class Parent { get { return mParent; } set { mParent = value; } }
        public string BaseClass { get { return mBaseClass; } set { mBaseClass = value; } }
        public Namespace Namespace { get { return mParentNS; } }

        #region Output
        private void OutputName(OutputHelper oh)
        {
            oh.Output(mName, false);
        }

        private void OutputHeader(OutputHelper oh, bool bOutputPartial, bool bOutputInterfaces, bool bOutputAttributes)
        {
            if (bOutputAttributes && mAttributes.Count > 0)
            {
                foreach (var att in mAttributes)
                    oh.Output("[" + att + "]", true);
            }

            if (mbIsUnsafe)
                oh.Output("unsafe ", false);

            if (mbIsStatic)
                oh.Output("static ", false);
            if (mbIsAbstract)
                oh.Output("abstract ", false);
            if (mbIsSealed)
                oh.Output("sealed ", false);

            switch (mAccess)
            {
                case Access.PUBLIC:
                    oh.Output("public ", false);
                    break;
                case Access.PROTECTED:
                    oh.Output("protected ", false);
                    break;
                case Access.PRIVATE:
                    oh.Output("private ", false);
                    break;
                case Access.INTERNAL:
                    oh.Output("internal ", false);
                    break;
                case Access.INTERNAL_AND_OR_PROTECTED:
                    oh.Output("internal protected ", false);
                    break;
            }

            if (bOutputPartial)
                oh.Output("partial ", false);

            switch (mType)
            {
                case ClassType.CLASS:
                    oh.Output("class ", false);
                    break;
                case ClassType.STRUCT:
                    oh.Output("struct ", false);
                    break;
                case ClassType.INTERFACE:
                    oh.Output("interface ", false);
                    break;
            }

            OutputName(oh);

            //** Formal type parameters
            if (mFormals.Count > 0)
            {
                oh.Output("<", false);
                bool first = true;
                foreach (var f in mFormals)
                {
                    if (first)
                        first = false;
                    else
                        oh.Output(", ", false);
                    f.OutputName(oh);
                }
                oh.Output(">", false);
            }

            //** Base class, interfaces
            bool semicol = false;
            if (mBaseClass.Length > 0)
            {
                oh.Output(" : ", false);
                semicol = true;
                oh.Output(mBaseClass, false);
            }

            if (mInterfaces.Count > 0 && bOutputInterfaces)
            {
                if (!semicol)
                    oh.Output(" : ", false);

                bool first = true;
                foreach (var i in mInterfaces)
                {
                    if (!first || semicol)
                        oh.Output(", ", false);
                    first = false;

                    oh.Output(i, false);
                }
            }

            oh.OutputEOL();
        }

        private void OutputFormalTypeParameterConstraints(OutputHelper oh)
        {
            oh.IncrIndent();
            foreach (var formal in mFormals)
                formal.OutputConstraint(oh);
            oh.DecrIndent();
        }

        public int OutputOpening(OutputHelper oh, bool bNestingClasses, bool bOutputPartial, bool bFull)
        {
            Contract.Requires(oh != null);

            if (oh.Strategy != OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE
              && mParent == null)
                mParentNS.OpenNamespace(oh);

            // only output full information for the last class, not the nesting ones
            int indents = 1;
            if (bNestingClasses && mParent != null)
                indents += mParent.OutputOpening(oh, bNestingClasses, bOutputPartial, false);

            OutputHeader(oh, bOutputPartial, bFull, bFull);
            if (bFull)
                OutputFormalTypeParameterConstraints(oh);

            oh.Output("{", true);
            oh.IncrIndent();

            return indents;
        }

        public void OutputClosing(OutputHelper oh, int indents)
        {
            Contract.Requires(oh != null);

            for (int i = 0; i < indents; ++i)
            {
                oh.DecrIndent();
                oh.Output("}", true);
            }
        }

        public override void Output(OutputHelper oh)
        {
            oh.BeginClass(this);

            OutputIntern(oh,
              oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS,
              oh.Strategy != OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS);

            oh.EndClass(this);
        }

        private void OutputIntern(OutputHelper oh, bool bNestingClasses, bool bOutputNestedTypes)
        {
            int indents = OutputOpening(oh, bNestingClasses, bNestingClasses, true);

            // Sort the children alphabetically
            mChildren.Sort(new ElementBaseComparer());

            // Mic: Mmmhh... Each type is a separate case. It quites destroys the
            // idea to handle ElementBase elements without needing their actual
            // type but it's much more handy for output with regions (although it would be cleaner
            // by tuning up ElementBaseComparer perhaps).
            // So whatever...

            bool first = true;

            //** Object invariants
            bool firstinv = true;
            foreach (var inv in mInvariants)
            {
                if (firstinv)
                {
                    // open the invariants declaration
                    oh.BeginRegion("Object Invariants");
                    oh.Output("[ContractInvariantMethod]", true);
                    oh.Output("protected void ", false);

                    // make sure the method name is not already in use
                    string method_name = "InferredObjectInvariant";
                    int count = 0;
                    while (HasSymbol(method_name))
                        method_name = string.Format("{0}{1}", "InferredObjectInvariant", ++count);

                    oh.Output(method_name + "()", true);
                    oh.Output("{", true);
                    oh.IncrIndent();
                }

                oh.Output(string.Format("Contract.Invariant({0});", inv), true);
                first = false;
                firstinv = false;
            }

            if (!firstinv)
            {
                // close the invariants declaration
                oh.DecrIndent();
                oh.Output("}", true);
                oh.EndRegion();
            }

            //** Delegates
            bool firstdel = true;
            foreach (var child in mChildren)
            {
                if (child.IsDelegate)
                {
                    if (!first)
                        oh.OutputEOL();
                    // Don't output EOL between delegates
                    if (firstdel)
                        oh.BeginRegion("Delegates");
                    child.Output(oh);
                    first = false;
                    firstdel = false;
                }
            }
            if (!firstdel)
                oh.EndRegion();

            //** Enum
            bool firstenum = true;
            foreach (var child in mChildren)
            {
                if (child.IsEnum)
                {
                    if (!first)
                        oh.OutputEOL();
                    if (firstenum)
                        oh.BeginRegion("Enums");
                    child.Output(oh);
                    first = false;
                    firstenum = false;
                }
            }
            if (!firstenum)
                oh.EndRegion();

            //** Classes
            bool firstclass = true;
            foreach (var child in mChildren)
            {
                if (child.IsClass && bOutputNestedTypes)
                {
                    if (!first)
                        oh.OutputEOL();
                    if (firstclass)
                        oh.BeginRegion("Nested classes (from " + mName + ")");
                    child.Output(oh);
                    first = false;
                    firstclass = false;
                }
            }
            if (!firstclass)
                oh.EndRegion();

            #region Methods

            bool firstmeth = true;
            foreach (var child in mChildren)
            {
                if (child.IsMethod)
                {
                    if (!first)
                        oh.OutputEOL();
                    if (firstmeth)
                        oh.BeginRegion("Methods and constructors");
                    child.Output(oh);
                    first = false;
                    firstmeth = false;
                }
            }
            if (!firstmeth)
                oh.EndRegion();
            #endregion

            #region Properties

            bool firstprop = true;
            foreach (var child in mChildren)
            {
                if (child.IsProperty)
                {
                    if (!first)
                        oh.OutputEOL();
                    if (firstprop)
                        oh.BeginRegion("Properties and indexers");
                    child.Output(oh);
                    first = false;
                    firstprop = false;
                }
            }
            if (!firstprop)
                oh.EndRegion();
            #endregion

            #region Events

            bool firstEvent = true;
            foreach (var child in mChildren)
            {
                if (child.IsEvent)
                {
                    if (!first)
                        oh.OutputEOL();
                    if (firstEvent)
                        oh.BeginRegion("Events");
                    child.Output(oh);
                    first = false;
                    firstEvent = false;
                }
            }
            if (!firstEvent)
                oh.EndRegion();
            #endregion

            #region Fields

            bool firstfield = true;
            foreach (var child in mChildren)
            {
                if (child.IsField)
                {
                    // Don't output EOL between fields
                    if (firstfield && !first)
                        oh.OutputEOL();
                    if (firstfield)
                        oh.BeginRegion("Fields");
                    child.Output(oh);
                    firstfield = false;
                    first = false;
                }
            }
            if (!firstfield)
                oh.EndRegion();
            #endregion

            OutputClosing(oh, indents);

            if (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS
              || (oh.Strategy == OutputHelper.OutputStrategy.ONE_FILE_PER_TOPLEVEL_CLASS && mParent == null))
                mParentNS.CloseNamespace(oh);
        }
        #endregion

        public void ForceUnsafe(bool uns)
        {
            mbIsUnsafe = uns;
        }

        #region Registering functions
        public void AddChild(ElementBase child)
        {
            Contract.Requires(child != null);

            mChildren.Add(child);
            if (child.IsField && child.IsUnsafe)
                ForceUnsafe(true);
        }

        public void AddChildren(IEnumerable<ElementBase> children)
        {
            Contract.Requires(children != null);

            foreach (var ch in children)
                AddChild(ch);
        }

        public void AddInterface(string interf)
        {
            mInterfaces.Add(interf);
        }

        public void AddFormalTypeParameter(FormalTypeParameter ftp)
        {
            mFormals.Add(ftp);
        }

        public void AddFormalTypeParameters(List<FormalTypeParameter> ftpl)
        {
            Contract.Requires(ftpl != null);

            mFormals.AddRange(ftpl);
        }

        public void AddInvariant(string invariant)
        {
            Contract.Requires(invariant != null);

            if (invariant.Length > 0 && !mInvariants.Contains(invariant)) // sanity check
                mInvariants.Add(invariant);
        }

        public void AddAttribute(string att)
        {
            mAttributes.Add(att);
        }
        #endregion

        public override bool HasUnsafeCode()
        {
            foreach (var ch in mChildren)
            {
                if (ch.HasUnsafeCode())
                    return true;
            }
            return false;
        }

        public bool HasSymbol(string name)
        {
            foreach (var c in mChildren)
            {
                if (c.Name == name)
                    return true;
            }

            if (mParent != null)
                return mParent.HasSymbol(name);

            return false;
        }

        #region Privates
        private Namespace mParentNS;
        private Access mAccess;
        private bool mbIsAbstract;
        private bool mbIsSealed;
        private bool mbIsStatic;
        private bool mbIsUnsafe;
        private ClassType mType; // struct, class, interface
        private string mName;
        private Class mParent; // for nested classes
        private List<FormalTypeParameter> mFormals;
        // Strings for base class and interface because they might come
        // from references so we don't necessarily want to build objects for them
        private string mBaseClass; // for inheritance
        private List<string> mInterfaces;
        private List<string> mInvariants;
        private List<string> mAttributes; /// These are output before the class declaration

        private List<ElementBase> mChildren;
        #endregion
    }

    [ContractVerification(false)]
    public class Enum : ElementBase
    {
        public Enum(Access mAccess, Class mParent, Namespace mParentNS, string mName, string mUnderlyingType)
        {
            this.mAccess = mAccess;
            this.mParent = mParent;
            this.mParentNS = mParentNS;
            this.mName = mName;
            this.mUnderlyingType = mUnderlyingType;

            mFields = new List<EnumField>();
        }

        public override bool IsEnum { get { return true; } }
        public override string Name { get { return mName; } }

        public Class Parent { get { return mParent; } }
        public Namespace Namespace { get { return mParentNS; } }

        public override void Output(OutputHelper oh)
        {
            int indents = 0;
            if (mParent != null && false) // TODO: switch entre options
                indents = mParent.OutputOpening(oh, false, false, false);

            switch (mAccess)
            {
                case Access.PUBLIC:
                    oh.Output("public ", false);
                    break;
                case Access.PROTECTED:
                    oh.Output("protected ", false);
                    break;
                case Access.PRIVATE:
                    oh.Output("private ", false);
                    break;
                case Access.INTERNAL:
                    oh.Output("internal ", false);
                    break;
                case Access.INTERNAL_AND_OR_PROTECTED:
                    oh.Output("internal protected ", false);
                    break;
            }

            oh.Output("enum ", false);
            oh.Output(mName, false);

            if (mUnderlyingType.Length > 0 && mUnderlyingType != "int") // int is the default value so don't output it
                oh.Output(" : " + mUnderlyingType, false);

            oh.OutputEOL();
            oh.Output("{", true);
            oh.IncrIndent();

            foreach (var f in mFields)
                f.Output(oh);

            oh.DecrIndent();
            oh.Output("}", true);

            if (mParent != null && false)
                mParent.OutputClosing(oh, indents);
        }

        public void AddField(EnumField ef)
        {
            mFields.Add(ef);
        }

        #region Privates
        private Access mAccess;
        private Class mParent;
        private Namespace mParentNS;
        private string mName;
        private string mUnderlyingType;
        private List<EnumField> mFields;
        #endregion

        public class EnumField
        {
            public EnumField(string mName, string mInitialValue)
            {
                this.mName = mName;
                this.mInitialValue = mInitialValue;
            }

            public void Output(OutputHelper oh)
            {
                Contract.Requires(oh != null);

                oh.Output(mName, false);
                if (mInitialValue.Length > 0)
                    oh.Output(" = " + mInitialValue, false);
                oh.Output(", ", true);
            }

            private string mName;
            private string mInitialValue;
        }
    }

    public class FormalTypeParameter
    {
        public FormalTypeParameter(string mName, FormalTypeParamConstraint mConstraint)
        {
            this.mName = mName;
            setConstraint(mConstraint);
        }

        public FormalTypeParameter(string mName)
        {
            this.mName = mName;
            setConstraint(default(FormalTypeParamConstraint));
            mbHasContraint = false;
        }

        public void setConstraint(FormalTypeParamConstraint mConstraint)
        {
            this.mConstraint = mConstraint;
        }

        public void OutputName(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            oh.Output(mName, false);
        }

        public void OutputConstraint(OutputHelper oh)
        {
            if (mbHasContraint)
                mConstraint.Output(oh);
        }

        #region Privates
        readonly private string mName;
        readonly private bool mbHasContraint;
        private FormalTypeParamConstraint mConstraint;
        #endregion
    }

    public class FormalTypeParamConstraint
    {
        public enum TypeConstraint
        {
            NONE,
            CLASS,
            STRUCT,
            BASECLASS,
        }

        public FormalTypeParamConstraint(FormalTypeParameter mParent, TypeConstraint mTypeConstraint, string mClassConstraint, bool mbIsNew)
        {
            setInfos(mParent, mTypeConstraint, mClassConstraint, mbIsNew);
        }

        public void setInfos(FormalTypeParameter mParent, TypeConstraint mTypeConstraint, string mClassConstraint, bool mbIsNew)
        {
            this.mParent = mParent;
            this.mTypeConstraint = mTypeConstraint;
            this.mClassConstraint = mClassConstraint;
            mInterfaces = new List<string>();
            this.mbIsNew = mbIsNew;
        }

        public void AddInterfaceConstraint(string name)
        {
            mInterfaces.Add(name);
        }

        public void Output(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            oh.Output("where ", false);
            mParent.OutputName(oh);
            oh.Output(" : ", false);
            //** base class
            bool comma = false;
            if (mTypeConstraint == TypeConstraint.CLASS)
            {
                oh.Output("class", false);
                comma = true;
            }
            else if (mTypeConstraint == TypeConstraint.STRUCT)
            {
                oh.Output("struct", false);
                comma = true;
            }
            else if (mTypeConstraint == TypeConstraint.CLASS)
            {
                oh.Output(mClassConstraint, false);
                comma = true;
            }

            //** Interfaces
            foreach (var inter in mInterfaces)
            {
                if (comma)
                    oh.Output(", ", false);
                else
                    comma = true;

                oh.Output(inter, false);
            }

            //** The new() thing
            if (mbIsNew)
                oh.Output((comma ? ", " : "") + "new()", false);

            oh.OutputEOL();
        }

        #region Privates
        private FormalTypeParameter mParent;
        private TypeConstraint mTypeConstraint;
        private string mClassConstraint;
        private List<string> mInterfaces;
        private bool mbIsNew;
        #endregion
    }
    [ContractVerification(false)]
    public class Method : ElementBase
    {
        public Method(Access mAccess, Class mParent, string mReturnType, string mName, bool mbUnsafe, bool mbIsExtern, bool mbIsStatic, bool mbIsVirtual, bool mbIsNewSlot, bool mbIsSealed, bool mbIsAbstract, bool mbIsConstructor, bool mbIsFinalizer, bool mbIsOverride, string mCustomInitList)
        {
            Contract.Requires(mParent != null);

            this.mAccess = mAccess;
            mbIsInInterface = mParent.Type == Class.ClassType.INTERFACE;
            this.mParent = mParent;
            mParentNS = mParent.Namespace;
            this.mReturnType = mReturnType;
            this.mName = mName;
            mParams = new ParametersList();
            mLines = new List<string>();
            mPrecond = new List<string>();
            mPostcond = new List<string>();
            mFormalParams = new List<FormalTypeParameter>();
            mVarsToInitialize = new List<string>();
            mAttributes = new List<string>();

            this.Implicit = false;
            this.Explicit = false;
            this.Operator = false;

            this.mbUnsafe = mbUnsafe;
            this.mbIsExtern = mbIsExtern;
            this.mbIsStatic = mbIsStatic;
            this.mbIsVirtual = mbIsVirtual;
            this.mbIsNewSlot = mbIsNewSlot;
            this.mbIsSealed = mbIsSealed;
            this.mbIsAbstract = mbIsAbstract;
            this.mbIsConstructor = mbIsConstructor;
            this.mbIsFinalizer = mbIsFinalizer;
            this.mbIsOverride = mbIsOverride;
            this.mCustomInitList = mCustomInitList;
        }

        public override bool IsMethod { get { return true; } }
        public override bool IsUnsafe { get { return mbUnsafe; } }
        public override string Name { get { return mName; } }

        public bool Implicit { get; set; } /// useful for operators
        public bool Explicit { get; set; } /// useful for operators
        public string ReturnType { get { return mReturnType; } set { mReturnType = value; } } /// useful for operators
        public void SetName(string name) { mName = name; }
        public bool Operator { get; set; }

        public override void Output(OutputHelper oh)
        {
            if (mAttributes.Count > 0)
            {
                foreach (var att in mAttributes)
                    oh.Output("[" + att + "]", true);
            }

            if (mbUnsafe)
                oh.Output("unsafe ", false);

            if (Operator || (!mbIsFinalizer && !mName.Contains(".")))
            {
                //** Accessibility
                switch (mAccess)
                {
                    case Access.INTERNAL_AND_OR_PROTECTED:
                        oh.Output("protected internal ", false);
                        break;
                    case Access.INTERNAL:
                        oh.Output("internal ", false);
                        break;
                    case Access.PRIVATE:
                        oh.Output("private ", false);
                        break;
                    case Access.PROTECTED:
                        oh.Output("protected ", false);
                        break;
                    case Access.PUBLIC:
                        oh.Output("public ", false);
                        break;
                }

                //** Modifiers
                if (mbIsExtern)
                    oh.Output("extern ", false);
                if (mbIsStatic)
                    oh.Output("static ", false);
                if (mbIsVirtual)
                    oh.Output("virtual ", false);
                if (mbIsNewSlot)
                    oh.Output("new ", false);
                if (mbIsSealed)
                    oh.Output("sealed ", false);
                if (mbIsAbstract)
                    oh.Output("abstract ", false);
                if (mbIsOverride)
                    oh.Output("override ", false);
                if (Implicit)
                    oh.Output("implicit ", false);
                if (Explicit)
                    oh.Output("explicit ", false);
            }

            if (!mbIsConstructor && !mbIsFinalizer)
                oh.Output(mReturnType + (mReturnType.Length > 0 ? " " : ""), false);

            if (Operator)
                oh.Output("operator ", false);

            if (mbIsFinalizer)
                oh.Output("~", false);

            oh.Output(mName, false);

            //** Formal parameters
            if (mFormalParams.Count > 0)
            {
                oh.Output("<", false);
                bool first = true;
                foreach (var t in mFormalParams)
                {
                    if (first)
                        first = false;
                    else
                        oh.Output(", ", false);

                    t.OutputName(oh);
                }
                oh.Output(">", false);
            }

            oh.Output("(", false);
            mParams.OutputList(oh);
            oh.Output(")", false);

            // In these cases, stop there
            if (mbIsAbstract || mbIsExtern || mbIsInInterface)
            {
                oh.Output(";", true);
                return;
            }

            if (mCustomInitList.Length > 0)
                oh.Output(mCustomInitList, false);

            oh.OutputEOL();

            //** Formal type parameters constraints
            if (mFormalParams.Count > 0)
            {
                oh.IncrIndent();
                foreach (var cst in mFormalParams)
                    cst.OutputConstraint(oh);
                oh.DecrIndent();
            }

            //** Body
            oh.Output("{", true);
            oh.IncrIndent();

            // Determine if there is already a symbol named "Contract" accessible from the body of the method
            string contract_name = "Contract";
            if (mParent.HasSymbol("Contract"))
                contract_name = "System.Diagnostics.Contracts.Contract";

            foreach (var cond in mPrecond)
                oh.Output(contract_name + ".Requires(" + cond + ");", true);
            foreach (var cond in mPostcond)
                oh.Output(contract_name + ".Ensures(" + cond + ");", true);

            bool firstl = true;
            foreach (var l in mLines)
            {
                if ((mPrecond.Count > 0 || mPostcond.Count > 0) && firstl)
                    oh.OutputEOL();
                oh.Output(l, true);
                firstl = false;
            }

            bool firstv = true;
            foreach (var v in mVarsToInitialize)
            {
                if ((firstv && !firstl) || (firstv && firstl && (mPrecond.Count > 0 || mPostcond.Count > 0)))
                    oh.OutputEOL();
                oh.Output(v, true);
                firstv = false;
            }

            if (mReturnType != "void" && mReturnType != "System.Void"
              && !mbIsConstructor && !mbIsFinalizer)
            {
                if (mPrecond.Count > 0 || mPostcond.Count > 0
                  || mLines.Count > 0 || mVarsToInitialize.Count > 0)
                    oh.OutputEOL();
                if (Operator && mReturnType.Length == 0) // Particular case for conversion operators
                    oh.Output("return default(" + mName + ");", true);
                else
                    oh.Output("return default(" + mReturnType + ");", true);
            }

            oh.DecrIndent();
            oh.Output("}", true);
        }

        public void AddParameter(Parameter newparam)
        {
            Contract.Requires(newparam != null);

            mParams.AddParameter(newparam);
            if (newparam.GetModifier() == Parameter.Modifier.OUT)
                AddVarToInitialize(newparam.Type, newparam.Name);
            if (newparam.IsUnsafe)
                ForceUnsafe(true);
        }

        public void ForceUnsafe(bool uns)
        {
            mbUnsafe = uns;
        }

        public void AddFormalTypeParameter(FormalTypeParameter ftp)
        {
            mFormalParams.Add(ftp);
        }

        public void AddFormalTypeParameters(List<FormalTypeParameter> ftpl)
        {
            Contract.Requires(ftpl != null);

            mFormalParams.AddRange(ftpl);
        }

        public void AddVarToInitialize(string type, string name)
        {
            mVarsToInitialize.Add(name + " = default(" + type + ");");
        }

        public void AddLine(string line)
        {
            mLines.Add(line);
        }

        public void AddPrecond(string cond)
        {
            mPrecond.Add(cond);
        }

        public void AddPostcond(string cond)
        {
            mPostcond.Add(cond);
        }

        public void AddAttribute(string att)
        {
            mAttributes.Add(att);
        }

        #region Privates
        private Access mAccess;
        private bool mbIsInInterface;
        private Class mParent;
        private Namespace mParentNS;
        private string mReturnType;
        private string mName;
        private ParametersList mParams; /// In the form "modifier type name"
        private List<FormalTypeParameter> mFormalParams;
        private List<string> mLines;
        private List<string> mPrecond, mPostcond;
        private List<string> mVarsToInitialize;
        private List<string> mAttributes; /// Each string here is output as an attribute before the method body

        private bool mbUnsafe;
        private bool mbIsExtern;
        private bool mbIsStatic;
        private bool mbIsVirtual;
        private bool mbIsNewSlot;
        private bool mbIsSealed;
        private bool mbIsAbstract;
        private bool mbIsConstructor;
        private bool mbIsFinalizer;
        private bool mbIsOverride;
        private string mCustomInitList;
        #endregion
    }

    [ContractVerification(false)]
    public class Delegate : ElementBase
    {
        public Delegate(Access mAccess, bool mbUnsafe, string mName, Class mParent, Namespace mParentNS, string mReturnType)
        {
            this.mAccess = mAccess;
            this.mbUnsafe = mbUnsafe;
            this.mName = mName;
            this.mParent = mParent;
            this.mParentNS = mParentNS;
            this.mReturnType = mReturnType;

            mParams = new ParametersList();
            mFormalParams = new List<FormalTypeParameter>();
        }

        public override bool IsDelegate { get { return true; } }
        public override string Name { get { return mName; } }

        public Class Parent { get { return mParent; } }
        public Namespace Namespace { get { return mParentNS; } }

        public override void Output(OutputHelper oh)
        {
            //if (mParent == null && oh.Strategy != OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
            //  mParentNS.OpenNamespace(oh);

            if (mbUnsafe)
                oh.Output("unsafe ", false);

            switch (mAccess)
            {
                case Access.INTERNAL:
                    oh.Output("internal ", false);
                    break;
                case Access.PRIVATE:
                    oh.Output("private ", false);
                    break;
                case Access.PROTECTED:
                    oh.Output("protected ", false);
                    break;
                case Access.PUBLIC:
                    oh.Output("public ", false);
                    break;
                case Access.INTERNAL_AND_OR_PROTECTED:
                    oh.Output("internal protected ", false);
                    break;
            }

            oh.Output("delegate " + mReturnType + " " + mName, false);

            //** Formal parameters
            if (mFormalParams.Count > 0)
            {
                oh.Output("<", false);
                bool first = true;
                foreach (var t in mFormalParams)
                {
                    if (first)
                        first = false;
                    else
                        oh.Output(", ", false);

                    t.OutputName(oh);
                }
                oh.Output(">", false);
            }

            oh.Output("(", false);
            mParams.OutputList(oh);
            oh.Output(")", false);

            //** Formal type parameters constraints
            if (mFormalParams.Count > 0)
            {
                oh.IncrIndent();
                foreach (var cst in mFormalParams)
                    cst.OutputConstraint(oh);
                oh.DecrIndent();
            }

            oh.Output(";", true);

            //if (mParent == null && oh.Strategy != OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE)
            //  mParentNS.CloseNamespace(oh);
        }

        public void ForceUnsafe(bool uns)
        {
            mbUnsafe = uns;
        }

        public void AddParameter(Parameter param)
        {
            Contract.Requires(param != null);
            mParams.AddParameter(param);
            if (param.IsUnsafe)
                ForceUnsafe(true);
        }

        public void AddFormalTypeParameter(FormalTypeParameter ftp)
        {
            mFormalParams.Add(ftp);
        }

        public void AddFormalTypeParameters(List<FormalTypeParameter> ftpl)
        {
            Contract.Requires(ftpl != null);

            mFormalParams.AddRange(ftpl);
        }

        #region Privates.
        private Access mAccess;
        private bool mbUnsafe;
        private string mName;
        private Class mParent;
        private Namespace mParentNS;
        private ParametersList mParams;
        private string mReturnType;
        private List<FormalTypeParameter> mFormalParams;
        #endregion
    }

    [ContractVerification(false)]
    public class Parameter
    {
        public Parameter(string type, string name, Modifier mModifier)
        {
            mType = type;
            mName = name;
            this.mModifier = mModifier;
        }

        public string Name { get { return mName; } }
        public string Type { get { return mType; } }
        public Modifier GetModifier() { return mModifier; }
        public bool IsUnsafe { get { return mType.Contains("*"); } }

        public void Output(OutputHelper oh)
        {
            Contract.Requires(oh != null);

            if (mModifier == Modifier.OUT)
                oh.Output("out ", false);
            else if (mModifier == Modifier.REF)
                oh.Output("ref ", false);

            oh.Output(mType + " " + mName, false);
        }

        public enum Modifier
        {
            NONE,
            REF,
            OUT,
        }

        private string mType;
        private string mName;
        private Modifier mModifier;
    }

    public class ParametersList
    {
        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(mParams != null);
        }


        public ParametersList()
        {
            mParams = new List<Parameter>();
        }

        public void AddParameter(Parameter newparam)
        {
            mParams.Add(newparam);
        }

        public void OutputList(OutputHelper oh)
        {
            bool first = true;
            foreach (var p in mParams)
            {
                if (first)
                    first = false;
                else
                    oh.Output(", ", false);

                p.Output(oh);
            }
        }

        public bool IsEmpty()
        {
            return mParams.Count > 0;
        }

        readonly private List<Parameter> mParams;
    }

    [ContractVerification(false)]
    public class Property : ElementBase
    {
        public Property(Class mParent, bool mbHasGetter, bool mbHasSetter, string mName, bool mbIsUnsafe, bool mbIsInInterface, bool mbIsIndexer, string mReturnType, Access mAccess, Access mAccessGet, Access mAccessSet, bool mbIsStatic, bool mbIsNewSlot, bool mbIsOverride, bool mbIsSealed, bool mbIsVirtual, bool mbIsExtern, bool mbIsAbstract)
        {
            Contract.Requires(mParent != null);

            this.mParent = mParent;
            mParentNS = mParent.Namespace;
            this.mbHasGetter = mbHasGetter;
            this.mbHasSetter = mbHasSetter;
            this.mName = mName;
            this.mbIsUnsafe = mbIsUnsafe;
            this.mbIsInInterface = mbIsInInterface;
            this.mbIsIndexer = mbIsIndexer;
            this.mReturnType = mReturnType;
            this.mbIsNewSlot = mbIsNewSlot;
            this.mbIsOverride = mbIsOverride;
            this.mbIsSealed = mbIsSealed;
            this.mbIsStatic = mbIsStatic;
            this.mbIsVirtual = mbIsVirtual;
            this.mbIsExtern = mbIsExtern;
            this.mbIsAbstract = mbIsAbstract;

            mParameters = new ParametersList();
            mBodyGet = new List<string>();
            mBodySet = new List<string>();
            mGetPrecond = new List<string>();
            mGetPostcond = new List<string>();
            mSetPrecond = new List<string>();
            mSetPostcond = new List<string>();
            this.mAccess = mAccess;
            this.mAccessGet = mAccessGet;
            this.mAccessSet = mAccessSet;
        }

        public override bool IsProperty { get { return true; } }
        public override bool IsUnsafe { get { return mbIsUnsafe; } }
        public override string Name { get { return mName; } }

        public void AddLineToGetBody(string line)
        {
            mBodyGet.Add(line);
        }
        public void AddLineToSetBody(string line)
        {
            mBodySet.Add(line);
        }
        public void AddPrecondToGet(string precond)
        {
            mGetPrecond.Add(precond);
        }
        public void AddPostcondToGet(string postcond)
        {
            mGetPostcond.Add(postcond);
        }
        public void AddPrecondToSet(string precond)
        {
            mSetPrecond.Add(precond);
        }
        public void AddPostcondToSet(string postcond)
        {
            mSetPostcond.Add(postcond);
        }

        public void AddParameter(Parameter param) // No modifier for indexers
        {
            Contract.Requires(param != null);

            mParameters.AddParameter(param);
            if (param.IsUnsafe)
                ForceUnsafe(true);
        }

        public void ForceUnsafe(bool uns)
        {
            mbIsUnsafe = uns;
        }

        public override void Output(OutputHelper oh)
        {
            if (mbIsUnsafe)
                oh.Output("unsafe ", false);

            if (!mbHasGetter && !mbHasSetter)
            {
                string err = "Error, property \"" + mName + "\" must have either a getter or a setter."; // Empty property, should never happen
                oh.DebugOutput("Pretty C# Output error: " + err);
                oh.Output("/* " + err + " */", true);
            }

            if (mbIsIndexer && mName != "Item" && !mName.EndsWith(".Item"))
            {
                string alt = mName;
                if (alt.Contains("."))
                    alt = alt.Substring(alt.LastIndexOf('.') + 1);
                oh.Output("[System.Runtime.CompilerServices.IndexerName(\"" + alt + "\")]", true);
            }

            if (!mName.Contains("."))
            {
                OutputAccessibility(oh, mAccess);

                if (mbIsAbstract)
                    oh.Output("abstract ", false);
                if (mbIsVirtual)
                    oh.Output("virtual ", false);
                if (mbIsExtern)
                    oh.Output("extern ", false);
                if (mbIsStatic)
                    oh.Output("static ", false);
                if (mbIsNewSlot)
                    oh.Output("new ", false);
                if (mbIsOverride)
                    oh.Output("override ", false);
                if (mbIsSealed)
                    oh.Output("sealed ", false);
            }

            oh.Output(mReturnType + " ", false);

            if (!mbIsIndexer)
                oh.Output(mName, true);
            else
            {
                int lastdot = mName.LastIndexOf('.');
                if (lastdot < 0)
                {
                    oh.Output("this", false);
                }
                else // explicit implementation, such as NS.Interface.this
                {
                    string name = mName.Substring(0, lastdot) + ".this";

                    oh.Output(name, false);
                }

                oh.Output(" [", false);
                mParameters.OutputList(oh);
                oh.Output("]", true);
            }

            oh.Output("{", true);
            oh.IncrIndent();

            #region Getter
            if (mbHasGetter)
            {
                OutputAccessibility(oh, mAccessGet);
                oh.Output("get", false);
                if (mbIsInInterface || mbIsAbstract)
                    oh.Output(";", true);
                else
                {
                    oh.OutputEOL();
                    oh.Output("{", true);
                    oh.IncrIndent();

                    foreach (var cond in mGetPrecond)
                        oh.Output("Contract.Requires(" + cond + ");", true);
                    foreach (var cond in mGetPostcond)
                        oh.Output("Contract.Ensures(" + cond + ");", true);

                    if (mGetPrecond.Count > 0 || mGetPostcond.Count > 0)
                        oh.OutputEOL();

                    foreach (var line in mBodyGet)
                        oh.Output(line, true);

                    if (mBodyGet.Count > 0)
                        oh.OutputEOL();

                    oh.Output("return default(" + mReturnType + ");", true);

                    oh.DecrIndent();
                    oh.Output("}", true);
                }
            }
            #endregion

            #region Setter
            if (mbHasSetter)
            {
                OutputAccessibility(oh, mAccessSet);
                oh.Output("set", false);
                if (mbIsInInterface || mbIsAbstract)
                    oh.Output(";", true);
                else
                {
                    oh.OutputEOL();
                    oh.Output("{", true);
                    oh.IncrIndent();

                    // Determine if there is already a symbol named "Contract" accessible from the body of the method
                    string contract_name = "Contract";
                    if (mParent.HasSymbol("Contract"))
                        contract_name = "System.Diagnostics.Contracts.Contract";

                    foreach (var cond in mSetPrecond)
                        oh.Output(contract_name + ".Requires(" + cond + ");", true);
                    foreach (var cond in mSetPostcond)
                        oh.Output(contract_name + ".Ensures(" + cond + ");", true);

                    if (mBodySet.Count > 0
                      && (mSetPrecond.Count > 0 || mSetPostcond.Count > 0))
                        oh.OutputEOL();

                    foreach (var line in mBodySet)
                        oh.Output(line, true);

                    oh.DecrIndent();
                    oh.Output("}", true);
                }
            }
            #endregion

            oh.DecrIndent();
            oh.Output("}", true);
        }

        private void OutputAccessibility(OutputHelper oh, Access access)
        {
            if (mbIsInInterface || access == Access.NONE)
                return;

            switch (access)
            {
                case Access.INTERNAL:
                    oh.Output("internal ", false);
                    break;
                case Access.PRIVATE:
                    oh.Output("private ", false);
                    break;
                case Access.PROTECTED:
                    oh.Output("protected ", false);
                    break;
                case Access.PUBLIC:
                    oh.Output("public ", false);
                    break;
                case Access.INTERNAL_AND_OR_PROTECTED:
                    oh.Output("internal protected ", false);
                    break;
            }
        }

        private Class mParent;
        private Namespace mParentNS;
        private bool mbHasGetter, mbHasSetter;
        private string mName; /// Even if it's an indexer
        private bool mbIsUnsafe;
        private bool mbIsInInterface;
        private bool mbIsIndexer;
        private string mReturnType;

        private Access mAccess, mAccessGet, mAccessSet;

        private bool mbIsStatic;
        private bool mbIsNewSlot;
        private bool mbIsOverride;
        private bool mbIsSealed;
        private bool mbIsVirtual;
        private bool mbIsExtern;
        private bool mbIsAbstract;
        private ParametersList mParameters;

        private List<string> mBodyGet, mBodySet;
        private List<string> mGetPrecond, mGetPostcond;
        private List<string> mSetPrecond, mSetPostcond;
    }

    [ContractVerification(false)]
    public class Event : ElementBase
    {
        public Event(Class mParent, bool mbHasAdd, bool mbHasRemove, string mName, bool mbIsUnsafe, bool mbIsInInterface, string mReturnType, Access mAccess, Access mAccessGet, Access mAccessSet, bool mbIsStatic, bool mbIsNewSlot, bool mbIsOverride, bool mbIsSealed, bool mbIsVirtual, bool mbIsExtern, bool mbIsAbstract)
        {
            Contract.Requires(mParent != null);

            this.mParent = mParent;
            mParentNS = mParent.Namespace;
            this.mbHasAdd = mbHasAdd;
            this.mbHasRemove = mbHasRemove;
            this.mName = mName;
            this.mbIsUnsafe = mbIsUnsafe;
            this.mbIsInInterface = mbIsInInterface;
            this.mReturnType = mReturnType;
            this.mbIsNewSlot = mbIsNewSlot;
            this.mbIsOverride = mbIsOverride;
            this.mbIsSealed = mbIsSealed;
            this.mbIsStatic = mbIsStatic;
            this.mbIsVirtual = mbIsVirtual;
            this.mbIsExtern = mbIsExtern;
            this.mbIsAbstract = mbIsAbstract;

            mParameters = new ParametersList();
            mBodyAdd = new List<string>();
            mBodyRemove = new List<string>();
            mAddPrecond = new List<string>();
            mAddPostcond = new List<string>();
            mRemovePrecond = new List<string>();
            mRemovePostcond = new List<string>();
            this.mAccess = mAccess;
            this.mAccessGet = mAccessGet;
            this.mAccessSet = mAccessSet;
        }

        public override bool IsEvent { get { return true; } }
        public override bool IsUnsafe { get { return mbIsUnsafe; } }
        public override string Name { get { return mName; } }

        public void AddLineToGetBody(string line)
        {
            mBodyAdd.Add(line);
        }
        public void AddLineToSetBody(string line)
        {
            mBodyRemove.Add(line);
        }
        public void AddPrecondToGet(string precond)
        {
            mAddPrecond.Add(precond);
        }
        public void AddPostcondToGet(string postcond)
        {
            mAddPostcond.Add(postcond);
        }
        public void AddPrecondToSet(string precond)
        {
            mRemovePrecond.Add(precond);
        }
        public void AddPostcondToSet(string postcond)
        {
            mRemovePostcond.Add(postcond);
        }

        public void AddParameter(Parameter param) // No modifier for indexers
        {
            Contract.Requires(param != null);
            mParameters.AddParameter(param);
            if (param.IsUnsafe)
                ForceUnsafe(true);
        }

        public void ForceUnsafe(bool uns)
        {
            mbIsUnsafe = uns;
        }

        public override void Output(OutputHelper oh)
        {
            bool inInterface = (mParent.Type == Class.ClassType.INTERFACE);

            if (mbIsUnsafe)
                oh.Output("unsafe ", false);

            if (!mbHasAdd && !mbHasRemove)
            {
                string err = "Error, event \"" + mName + "\" must have either an add or a remove."; // Empty property, should never happen
                oh.DebugOutput("Pretty C# Output error: " + err);
                oh.Output("/* " + err + " */", true);
            }

            if (!mName.Contains("."))
            {
                OutputAccessibility(oh, mAccess);

                if (mbIsAbstract)
                    oh.Output("abstract ", false);
                if (mbIsVirtual)
                    oh.Output("virtual ", false);
                if (mbIsExtern)
                    oh.Output("extern ", false);
                if (mbIsStatic)
                    oh.Output("static ", false);
                if (mbIsNewSlot)
                    oh.Output("new ", false);
                if (mbIsOverride)
                    oh.Output("override ", false);
                if (mbIsSealed)
                    oh.Output("sealed ", false);
            }

            oh.Output("event ", false);

            oh.Output(mReturnType + " ", false);

            oh.Output(mName, true);

            if (inInterface)
            {
                oh.Output(";", true);
            }
            else
            {
                oh.Output("{", true);
                oh.IncrIndent();

                #region Add
                if (mbHasAdd)
                {
                    OutputAccessibility(oh, mAccessGet);
                    oh.Output("add", false);
                    if (mbIsInInterface || mbIsAbstract)
                        oh.Output(";", true);
                    else
                    {
                        oh.OutputEOL();
                        oh.Output("{", true);
                        oh.IncrIndent();

                        foreach (var cond in mAddPrecond)
                            oh.Output("Contract.Requires(" + cond + ");", true);
                        foreach (var cond in mAddPostcond)
                            oh.Output("Contract.Ensures(" + cond + ");", true);

                        if (mAddPrecond.Count > 0 || mAddPostcond.Count > 0)
                            oh.OutputEOL();

                        foreach (var line in mBodyAdd)
                            oh.Output(line, true);

                        if (mBodyAdd.Count > 0)
                            oh.OutputEOL();

                        //oh.Output("return default(" + mReturnType + ");", true);

                        oh.DecrIndent();
                        oh.Output("}", true);
                    }
                }
                #endregion

                #region Remove
                if (mbHasRemove)
                {
                    OutputAccessibility(oh, mAccessSet);
                    oh.Output("remove", false);
                    if (mbIsInInterface || mbIsAbstract)
                        oh.Output(";", true);
                    else
                    {
                        oh.OutputEOL();
                        oh.Output("{", true);
                        oh.IncrIndent();

                        // Determine if there is already a symbol named "Contract" accessible from the body of the method
                        string contract_name = "Contract";
                        if (mParent.HasSymbol("Contract"))
                            contract_name = "System.Diagnostics.Contracts.Contract";

                        foreach (var cond in mRemovePrecond)
                            oh.Output(contract_name + ".Requires(" + cond + ");", true);
                        foreach (var cond in mRemovePostcond)
                            oh.Output(contract_name + ".Ensures(" + cond + ");", true);

                        if (mBodyRemove.Count > 0
                          && (mRemovePrecond.Count > 0 || mRemovePostcond.Count > 0))
                            oh.OutputEOL();

                        foreach (var line in mBodyRemove)
                            oh.Output(line, true);

                        oh.DecrIndent();
                        oh.Output("}", true);
                    }
                }
                #endregion

                oh.DecrIndent();
                oh.Output("}", true);
            }
        }

        private void OutputAccessibility(OutputHelper oh, Access access)
        {
            if (mbIsInInterface || access == Access.NONE)
                return;

            switch (access)
            {
                case Access.INTERNAL:
                    oh.Output("internal ", false);
                    break;
                case Access.PRIVATE:
                    oh.Output("private ", false);
                    break;
                case Access.PROTECTED:
                    oh.Output("protected ", false);
                    break;
                case Access.PUBLIC:
                    oh.Output("public ", false);
                    break;
                case Access.INTERNAL_AND_OR_PROTECTED:
                    oh.Output("internal protected ", false);
                    break;
            }
        }

        private Class mParent;
        private Namespace mParentNS;
        private bool mbHasAdd, mbHasRemove;
        private string mName; /// Even if it's an indexer
        private bool mbIsUnsafe;
        private bool mbIsInInterface;
        private string mReturnType;

        private Access mAccess, mAccessGet, mAccessSet;

        private bool mbIsStatic;
        private bool mbIsNewSlot;
        private bool mbIsOverride;
        private bool mbIsSealed;
        private bool mbIsVirtual;
        private bool mbIsExtern;
        private bool mbIsAbstract;
        private ParametersList mParameters;

        private List<string> mBodyAdd, mBodyRemove;
        private List<string> mAddPrecond, mAddPostcond;
        private List<string> mRemovePrecond, mRemovePostcond;
    }

    [ContractVerification(false)]
    public class Field : ElementBase
    {
        public Field(string mType, string mName, Class mParent, string mOutputDefault, ParentType mParentType, Access mAccess, bool mbIsUnsafe, bool mbIsReadOnly, bool mbIsStatic, bool mbIsNewSlot, bool mbIsVolatile)
        {
            this.mType = mType;
            this.mName = mName;
            this.mParent = mParent;

            this.mOutputDefault = mOutputDefault;
            this.mParentType = mParentType;
            this.mAccess = mAccess;
            this.mbIsUnsafe = mbIsUnsafe;
            this.mbIsReadOnly = mbIsReadOnly;
            this.mbIsStatic = mbIsStatic;
            this.mbIsNewSlot = mbIsNewSlot;
            this.mbIsVolatile = mbIsVolatile;
        }

        public override bool IsField { get { return true; } }
        public override bool IsUnsafe { get { return mbIsUnsafe; } }
        public override string Name { get { return mName; } }

        public override void Output(OutputHelper oh)
        {
            // No accessibility nor modifiers on interface members
            if (mParentType != ParentType.INTERFACE)
            {
                switch (mAccess)
                {
                    case Access.PUBLIC:
                        oh.Output("public ", false);
                        break;
                    case Access.PROTECTED:
                        oh.Output("protected ", false);
                        break;
                    case Access.PRIVATE:
                        oh.Output("private ", false);
                        break;
                    case Access.INTERNAL:
                        oh.Output("internal ", false);
                        break;
                    case Access.INTERNAL_AND_OR_PROTECTED:
                        oh.Output("internal protected ", false);
                        break;
                }

                if (mbIsReadOnly)
                    oh.Output("readonly ", false);
                if (mbIsStatic)
                    oh.Output("static ", false);
                if (mbIsNewSlot)
                    oh.Output("new ", false);
                if (mbIsVolatile)
                    oh.Output("volatile ", false);
            }

            oh.Output(mType + " " + mName, false);
            if (mOutputDefault.Length > 0 && mParentType == ParentType.CLASS)
                oh.Output(" = " + mOutputDefault, false);

            oh.Output(";", true);
        }

        #region Enums
        public enum ParentType
        {
            CLASS,
            STRUCT,
            INTERFACE,
        }
        #endregion

        #region Privates
        private string mType;
        private string mName;

        private string mOutputDefault; /// Output "... = mOutputDefault" (nothing if empty string)
        private ParentType mParentType;
        private Class mParent;
        private Access mAccess;
        private bool mbIsUnsafe;

        private bool mbIsReadOnly;
        private bool mbIsStatic;
        private bool mbIsNewSlot;
        private bool mbIsVolatile;
        #endregion
    }

    /// <summary>
    /// This class is a container for outputing C# code
    /// </summary>
    public class CodeManager
    {
        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(mNamespaces != null);
        }


        public CodeManager()
        {
            mNamespaces = new List<Namespace>();
            mNamespaces.Add(new Namespace("<toplevel>"));
        }

        public void OutputCode(string ProjectFolder, string RelativeFilesPath, IOutput mOutput, out List<string> WrittenFiles, OutputHelper.OutputStrategy Strategy, List<string> Usings, bool verbose)
        {
            Contract.Requires(ProjectFolder != null);
            Contract.Requires(RelativeFilesPath != null);
            Contract.Requires(mOutput != null);

            var oh =
              new OutputHelper(
                //OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE,
                //OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS,
                //OutputHelper.OutputStrategy.ONE_FILE_PER_TOPLEVEL_CLASS,
                Strategy,
                ProjectFolder,
                RelativeFilesPath,
                Usings,
                mOutput,
                verbose);

            foreach (var ns in mNamespaces)
            {
                if (!ns.Empty())
                    ns.Output(oh);
            }

            WrittenFiles = oh.WrittenFiles;
        }

        public void OutputProperties(string folder, string assemblyName, List<string> writtenFiles)
        {
            Contract.Requires(folder != null);
            Contract.Requires(writtenFiles != null);

            try
            {
                var properties = Path.Combine(folder, "Properties");
                Directory.CreateDirectory(properties);

                StreamWriter assemblyInfoFile = null;
                var assemblyInfoFileName = Path.Combine(properties, "AssemblyInfo.cs");
                try
                {
                    assemblyInfoFile = File.CreateText(assemblyInfoFileName);
                    var assemblyInfoTemplate = Resources.AssemblyInfoTemplate;

                    assemblyInfoTemplate = assemblyInfoTemplate.Replace("$(ContractAssemblyName)", assemblyName + ".Contracts");
                    assemblyInfoFile.Write(assemblyInfoTemplate);
                }
                finally
                {
                    if (assemblyInfoFile != null)
                    {
                        assemblyInfoFile.Close();
                        writtenFiles.Add(@"Properties\AssemblyInfo.cs");
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>
        /// Outputs a MSBuild makefile for the newly created file contracts
        /// </summary>
        public void OutputProjectFile(string filename, List<string> WrittenFiles, List<Pair<string, Version>> References, IOutput mOutput, string AssemblyName, bool bUnsafe, bool verbose)
        {
            Contract.Requires(WrittenFiles != null);
            Contract.Requires(References != null);

            if (WrittenFiles.Count == 0)
            {
                if (mOutput != null)
                    mOutput.WriteLine("No source file written, no project output.");
                return;
            }

            System.IO.StreamWriter outfile = null;
            try
            {
                // first figure out some properties of the project file based on the references:
                bool usesXaml = false;
                bool noStdLib = true;
                int majorFrameworkVersion = 3;
                if (References.Count > 0)
                {
                    foreach (var ra in References)
                    {
                        var refname = ra.One;

                        Contract.Assume(refname != null);

                        if (refname.Contains("mscorlib"))
                        {
                            noStdLib = false;
                            if (ra.Two.Major == 4)
                            {
                                majorFrameworkVersion = 4;
                            }
                        }
                        if (refname.Contains("System.Xaml"))
                        {
                            usesXaml = true;
                        }
                    }
                }
                string frameworkVersion = (majorFrameworkVersion == 4) ? "4.0" : "3.5";

                outfile = new System.IO.StreamWriter(filename);
                outfile.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                outfile.WriteLine("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");

                outfile.WriteLine("<PropertyGroup>");
                outfile.WriteLine("<OutputType>Library</OutputType>");
                outfile.WriteLine("<AssemblyName>" + AssemblyName + "</AssemblyName>");
                outfile.WriteLine("<Optimize>true</Optimize>");
                outfile.WriteLine("<DefineConstants>CONTRACTS_FULL</DefineConstants>");
                outfile.WriteLine("<TargetFrameworkVersion>v{0}</TargetFrameworkVersion>", frameworkVersion);
                if (bUnsafe)
                    outfile.WriteLine("<AllowUnsafeBlocks>true</AllowUnsafeBlocks>");
                outfile.WriteLine(@"<OutputPath>bin\Debug\</OutputPath>");
                if (noStdLib)
                {
                    outfile.WriteLine("<NoStdLib>true</NoStdLib>");
                }
                if (usesXaml)
                {
                    outfile.WriteLine("<ProjectGuid>7521af18-5797-4511-aa3e-636c64b36dfc</ProjectGuid>");
                    outfile.WriteLine("<ProjectTypeGuids>{60dc8134-eba5-43b8-bcc9-bb4bc16c2548};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}</ProjectTypeGuids>");
                }
                outfile.WriteLine("<TargetFrameworkVersion>v{0}</TargetFrameworkVersion>", frameworkVersion);
                outfile.WriteLine("<AppDesignerFolder>Properties</AppDesignerFolder>");
                outfile.WriteLine("<SignAssembly>true</SignAssembly>");
                outfile.WriteLine("<DelaySign>true</DelaySign>");
                outfile.WriteLine(@"<AssemblyOriginatorKeyFile>..\..\common\Contracts.snk</AssemblyOriginatorKeyFile>");
                outfile.WriteLine(@"<CodeContractsReferenceAssembly>Build</CodeContractsReferenceAssembly>");

                outfile.WriteLine("</PropertyGroup>");

                outfile.WriteLine("<PropertyGroup Condition=\" '$(Configuration)' == 'Debug' \">");
                outfile.WriteLine("  <CodeContractsCheckReferenceAssembly>true</CodeContractsCheckReferenceAssembly>");
                outfile.WriteLine("</PropertyGroup>");

                var targets = "";
                try
                {
                    targets = GetTargets(AssemblyName);
                }
                catch { }

                outfile.WriteLine(@"  <ItemGroup>");
                outfile.WriteLine(@"    <BuildTargetList Include=""{0}""/>", targets);
                outfile.WriteLine(@"
    <MultiTargetReferencePaths Include=""..\..\Imported\ReferenceAssemblies\;..\$(OutputPath)"" />
  </ItemGroup>"
                  );

                outfile.WriteLine("<ItemGroup>");
                foreach (var wf in WrittenFiles)
                    outfile.WriteLine("\t<Compile Include = \"" + wf + "\" />");
                outfile.WriteLine("</ItemGroup>");

                if (References.Count > 0)
                {
                    outfile.WriteLine("<ItemGroup>");
                    foreach (var ra in References)
                    {
                        var refname = ra.One;
                        outfile.WriteLine("\t<Reference Include = \"" + refname + "\" />");
                    }
                    outfile.WriteLine("</ItemGroup>");
                }

                if (majorFrameworkVersion < 4)
                {
                    var progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
                    var contractHintPath = Path.Combine(progFiles, @"\Microsoft\Contracts\PublicAssemblies\v3.5\Microsoft.Contracts.dll");
                    outfile.WriteLine("<ItemGroup>");
                    outfile.WriteLine("\t<Reference Include = \"Microsoft.Contracts\">");
                    outfile.WriteLine("\t\t<HintPath>{0}</HintPath>", contractHintPath);
                    outfile.WriteLine("\t</Reference>");
                    outfile.WriteLine("</ItemGroup>");
                }
                //outfile.WriteLine("<Target Name=\"Build\">");
                //outfile.WriteLine("\t<Csc Sources=\"@(Compile)\" References=\"@(Reference)\" OutputAssembly = \"Output\\" + mAssemblyName + ".Contracts.Inferred.dll\"/>");
                outfile.WriteLine("<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
                outfile.WriteLine(@"
  <PropertyGroup>
    <ContractReferenceOutputPathRoot>..\$(OutputPath)</ContractReferenceOutputPathRoot>
  </PropertyGroup>
  <Import Project=""..\Multitarget.targets"" />"
                  );
                //outfile.WriteLine("</Target>");
                outfile.WriteLine("</Project>");

                if (mOutput != null && verbose)
                    mOutput.WriteLine("OutputPretty C#: Project file successfully output in " + filename + ".");
            }
            finally
            {
                if (outfile != null)
                    outfile.Close();
            }
        }

        private static string GetTargets(string name)
        {
            var sb = new StringBuilder();
            foreach (var dir in PathsToDll(name))
            {
                var suffix = RemovePrefix(dir);
                if (suffix == null) continue;

                sb.Append(@"Microsoft\Framework\");
                sb.Append(suffix);
                sb.Append(@"\Target;");
            }

            // trim last
            var result = sb.ToString();
            return result.TrimEnd(';');
        }

        private static string RemovePrefix(string dir)
        {
            var ri = dir.IndexOf("ReferenceAssemblies");
            if (ri >= 0)
            {
                return dir.Substring(ri + "ReferenceAssemblies".Length + 1);
            }
            return null;
        }

        private static IEnumerable<string> PathsToDll(string name)
        {
            var dirs = Directory.EnumerateDirectories(@"..\Imported\ReferenceAssemblies");
            return FindWithin(dirs, name + ".dll");
        }

        private static IEnumerable<string> FindWithin(IEnumerable<string> dirs, string file)
        {
            foreach (var dir in dirs)
            {
                if (File.Exists(Path.Combine(dir, file)))
                {
                    yield return dir;
                }
                foreach (var result in FindWithin(Directory.EnumerateDirectories(dir), file))
                {
                    yield return result;
                }
            }
        }

        public bool HasUnsafeCode()
        {
            foreach (var ns in mNamespaces)
            {
                if (ns.HasUnsafeCode())
                    return true;
            }
            return false;
        }

        public void AddNamespace(Namespace newNS)
        {
            mNamespaces.Add(newNS);
        }

        public Namespace LookupNamespaceByName(string name)
        {
            foreach (var ns in mNamespaces)
            {
                if (ns.Name == name)
                    return ns;
            }

            Namespace newns = new Namespace(name);
            AddNamespace(newns);
            return newns;
        }

        #region Privates
        readonly private List<Namespace> mNamespaces;
        #endregion
    }

    static public class TypeHelper
    {
        /// <summary>
        /// Simplifies class types
        /// </summary>
        /// <param name="type">Text representation of the type to reduce</param>

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public static string SimplifyType(string type)
        {
            Contract.Requires(type != null);

            type = type.Trim();

            switch (type)
            {
                case "System.Boolean":
                case "Boolean":
                    return "bool";
                case "System.Char":
                case "Char":
                    return "char";
                case "System.String":
                case "String":
                    return "string";
                case "System.Single":
                case "Single":
                    return "float";
                case "System.Double":
                case "Double":
                    return "double";
                case "System.SByte":
                case "SByte":
                    return "sbyte";
                case "System.Int16":
                case "Int16":
                    return "short";
                case "System.Int32":
                case "Int32":
                    return "int";
                case "System.Int64":
                case "Int64":
                    return "long";
                case "System.Byte":
                case "Byte":
                    return "byte";
                case "System.UInt16":
                case "UInt16":
                    return "ushort";
                case "System.UInt32":
                case "UInt32":
                    return "uint";
                case "System.UInt64":
                case "UInt64":
                    return "ulong";
                case "System.Void":
                case "Void":
                    return "void";
            }

            return type;
        }

        public static string GetNamespaceFromClass<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>
          (IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder,
          Type type)
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<string>() != null);

            Type parent;
            if (mdDecoder.IsNested(type, out parent))
                return GetNamespaceFromClass(mdDecoder, parent); // In CCI1, only toplevel classes have a namespace

            string nsname = mdDecoder.Namespace(type);
            if (nsname == null) // System.Int, ...
                return "";

            return nsname;
        }

        public static string TypeFullName<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>
          (IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder,
          Type type)
            where Type : IEquatable<Type>
        {
            Contract.Requires(mdDecoder != null);

            return TypeFullName(mdDecoder, type, null);
        }

        public enum StripContext
        {
            Namespace,
            ParentType
        }

        public static string TypeFullName<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>
          (IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder,
          Type type,
          Func<string, string, StripContext, bool> can_strip_prefix)
            where Type : IEquatable<Type>
        {
            Contract.Requires(mdDecoder != null);
            Contract.Ensures(Contract.Result<string>() != null);

            // Don't use FullName from meta data decoder because Clousot1
            // has some strange name mangling habits
            //string basecl = SimplifyType(mdDecoder.FullName(type));

            if (mdDecoder.IsArray(type))
            {
                var elementType = TypeFullName(mdDecoder, mdDecoder.ElementType(type), can_strip_prefix);
                var rank = mdDecoder.Rank(type);
                if (rank == 1)
                {
                    return elementType + "[]";
                }
                else
                {
                    return String.Format("{0}[{1}]", elementType, new String(',', rank - 1));
                }
            }

            if (mdDecoder.IsUnmanagedPointer(type))
                return TypeFullName(mdDecoder, mdDecoder.ElementType(type), can_strip_prefix) + "*";

            Type modified;
            Microsoft.Research.DataStructures.IIndexable<Microsoft.Research.DataStructures.Pair<bool, Type>> modifiers;
            if (mdDecoder.IsModified(type, out modified, out modifiers))
                type = modified;

            Microsoft.Research.DataStructures.IIndexable<Type> args;
            var bSpecialized = mdDecoder.IsSpecialized(type, out args);
            Type type_unspec = default(Type);
            if (bSpecialized)
                type_unspec = mdDecoder.Unspecialized(type); // take the unspecialized type, then we'll add the formal parameters later

            var typeName = mdDecoder.Name(bSpecialized ? type_unspec : type);
            string basecl;
            Type parentt;
            if (mdDecoder.IsNested(type, out parentt)) // crucial that we took the unspecialized version of the type to have the formal hierarchy
            {
                basecl = TypeFullName(mdDecoder, parentt, can_strip_prefix);
                Contract.Assume(basecl.Length > 0);
                if (CanStripPrefix(can_strip_prefix, basecl, typeName, StripContext.ParentType))
                {
                    basecl = typeName;
                }
                else
                {
                    basecl += "." + typeName;
                }
            }
            else
            {
                string ns = GetNamespaceFromClass(mdDecoder, bSpecialized ? type_unspec : type);
                if (ns == "<toplevel>" || ns == "type parameter" /* made-up naming in Clousot1 */)
                {
                    basecl = typeName;
                }
                else
                {
                    if (CanStripPrefix(can_strip_prefix, typeName, ns, StripContext.Namespace))
                    {
                        basecl = typeName;
                    }
                    else
                    {
                        if (ns.Length > 0)
                        {
                            basecl = ns + "." + typeName;
                        }
                        else
                        {
                            basecl = typeName;
                        }
                    }
                }
            }

            if (bSpecialized)
            {
                // output generic type parameters
                if (args.Count > 0)
                {
                    basecl += "<";
                    for (int i = 0; i < args.Count; ++i)
                    {
                        if (i > 0)
                            basecl += ", ";
                        basecl += TypeFullName(mdDecoder, args[i], can_strip_prefix);
                    }
                    basecl += ">";
                }
            }

            return SimplifyType(basecl);
        }

        private static bool CanStripPrefix(Func<string, string, StripContext, bool> can_strip_prefix, string typeName, string ns, StripContext context)
        {
            return can_strip_prefix != null && can_strip_prefix(ns, typeName, context);
        }
    }

    public class ConstructionHelper<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>
      where Type : IEquatable<Type>
    {
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(mCm != null);
            Contract.Invariant(mdDecoder != null);
        }

        public ConstructionHelper(IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder, bool visibleMembersOnly)
        {
            Contract.Requires(mdDecoder != null);

            this.mdDecoder = mdDecoder;
            this.visibleMembersOnly = visibleMembersOnly;
            mCm = new CodeManager();
            mClassesLookup = new Dictionary<Type, Class>();
            mMethodsLookup = new Dictionary<MethodExt, Method>();
            mPropertiesLookup = new Dictionary<PropertyExt, Property>();

            mUsings = new List<string>();
            // order is important !! you don't want "System" to be in first place,
            // otherwise the other won't ever match
            mUsings.Add("System.Collections.Generic");
            mUsings.Add("System.IO");
            mUsings.Add("System.Text");
            mUsings.Add("System.Diagnostics.Contracts");
            mUsings.Add("System");
        }

        public List<string> Usings { get { return mUsings; } }

        /// <summary>
        /// Returns the type full name as it should be output anywhere in the code (parameter, return type ...)
        /// </summary>
        /// <param name="type">The type to output</param>
        /// <param name="contextClass">If non-null, tells in which class the output will take place, so that the parent name can be stripped off</param>
        /// <param name="ParentNS">If non-null, tells in which namespace the output will take place so that the parent namespace can be stripped off</param>
        public string TypeFullName(Type type, Class contextClass, Namespace ParentNS)
        {
            List<string> prefixes = new List<string>();
            if (ParentNS != null)
                prefixes.Add(ParentNS.Name + ".");
            if (contextClass != null)
                prefixes.Add(contextClass.Name + ".");

            // finally, check if the base namespace is in the "using" namespaces, so we could skip it
            //foreach (var use in mUsings)
            //  prefixes.Add(use + ".");

            return TypeHelper.TypeFullName(mdDecoder, type,
              (prefix, typeName, context) =>
              {
                  switch (context)
                  {
                      case TypeHelper.StripContext.Namespace:

                          // don't want to capture name in parent context
                          if (contextClass != null && contextClass.HasSymbol(typeName))
                              return false;
                          if (ParentNS != null && prefix == ParentNS.Name)
                              return true;
                          if (mUsings.Contains(prefix))
                              return true;
                          break;

                      case TypeHelper.StripContext.ParentType:

                          if (contextClass != null && prefix == contextClass.Name)
                              return true;
                          break;
                  }

                  return false;
              }
            );
        }

        /// <summary>
        /// Returns whether there is a constructor for this type. Puts the one with the smaller number
        /// of arguments in the out parameter "constructor".
        /// If not a class, or if no constructor is found, just return false with nothing in "constructor".
        /// The returned constructor will be visible from the "calling_type" constructor (as given by metadatadecoder.DeclaringModuleName),
        /// and public or protected (ie. usable in a class derived from type).
        /// The possible base class is not visited.
        /// </summary>
        public bool GetAConstructor(Type type, Type calling_type, out MethodExt constructor)
        {
            constructor = default(MethodExt);
            if (!mdDecoder.IsClass(type))
                return false;

            int nb_min = 9999999; // should be enough
            bool bReturn = false;
            foreach (var m in mdDecoder.Methods(type))
            {
                if (mdDecoder.IsConstructor(m) && !mdDecoder.IsPrivate(m))
                {
                    var pars = mdDecoder.Parameters(m);
                    if (pars.Count < nb_min)
                    {
                        // Check that the constructor and the arguments types are visible to the current assembly
                        Microsoft.Research.DataStructures.IIndexable<Type> dummy;
                        string base_assembly = mdDecoder.DeclaringModuleName(mdDecoder.IsSpecialized(type, out dummy) ? mdDecoder.Unspecialized(type) : type);
                        string calling_assembly = mdDecoder.DeclaringModuleName(mdDecoder.IsSpecialized(calling_type, out dummy) ? mdDecoder.Unspecialized(calling_type) : calling_type);

                        bool bVisible = true;
                        if (calling_assembly == base_assembly || mdDecoder.IsVisibleOutsideAssembly(m))
                        {
                            // Then check every parameter type
                            for (int i = 0; i < pars.Count && bVisible; ++i)
                            {
                                Type ptype = mdDecoder.ParameterType(pars[i]);
                                if (mdDecoder.IsSpecialized(ptype, out dummy))
                                    ptype = mdDecoder.Unspecialized(ptype);
                                string asst = mdDecoder.DeclaringModuleName(ptype);
                                if (asst != calling_assembly)
                                {
                                    //if (mdDecoder.IsPrivate(ptype) || mdDecoder.IsInternal(ptype))
                                    if (!mdDecoder.IsPublic(ptype) && !mdDecoder.IsProtected(ptype))
                                    {
                                        bVisible = false;
                                        break; // reject
                                    }
                                }
                                else
                                    bVisible = mdDecoder.IsVisibleFrom(ptype, calling_type);
                            }
                        }
                        else
                            bVisible = false;

                        if (bVisible)
                        {
                            constructor = m;
                            nb_min = pars.Count;
                            bReturn = true;
                        }
                    }
                }
            }
            return bReturn;
        }

        private List<FormalTypeParameter> GetFormalTypeParameters(Microsoft.Research.DataStructures.IIndexable<Type> formals)
        {
            List<FormalTypeParameter> res = new List<FormalTypeParameter>();

            for (int i = 0; i < formals.Count; ++i)
            {
                var constraints = mdDecoder.TypeParameterConstraints(formals[i]);
                Contract.Assume(constraints != null);
                string name = mdDecoder.Name(formals[i]);
                var ftp = new FormalTypeParameter(name);

                var tc = FormalTypeParamConstraint.TypeConstraint.NONE;
                string classConstraint = "";
                if (mdDecoder.IsReferenceConstrained(formals[i]))
                    tc = FormalTypeParamConstraint.TypeConstraint.CLASS;
                else if (mdDecoder.IsValueConstrained(formals[i]))
                    tc = FormalTypeParamConstraint.TypeConstraint.STRUCT;
                else
                {
                    foreach (var cont in constraints)
                    {
                        if (!mdDecoder.IsInterface(cont))
                        {
                            classConstraint = TypeFullName(cont, null, LookupNamespaceFromClass(cont));
                            tc = FormalTypeParamConstraint.TypeConstraint.BASECLASS;
                            break;
                        }
                    }
                }

                bool bIsNew = mdDecoder.IsConstructorConstrained(formals[i]) && !mdDecoder.IsValueConstrained(formals[i]);

                var ftpc = new FormalTypeParamConstraint(ftp, tc, classConstraint, bIsNew);
                ftp.setConstraint(ftpc);

                // Interfaces
                foreach (var cont in constraints)
                {
                    if (mdDecoder.IsInterface(cont))
                        ftpc.AddInterfaceConstraint(TypeFullName(cont, null, LookupNamespaceFromClass(cont)));
                }

                res.Add(ftp);
            }

            return res;
        }

        private string GetNamespaceFromClass(Type type)
        {
            var nsname = TypeHelper.GetNamespaceFromClass(mdDecoder, type);

            if (nsname.Length == 0)
                nsname = "<toplevel>";

            return nsname;
        }

        public Namespace LookupNamespaceFromClass(Type type)
        {
            return mCm.LookupNamespaceByName(GetNamespaceFromClass(type));
        }

        [ContractVerification(false)]
        public Class ConstructClassFromMetaDataDecoder(Type type)
        {
            string name = mdDecoder.Name(type);
            Access access = Access.PUBLIC;
            Type dummy;
            if (mdDecoder.IsNested(type, out dummy))
            {
                if (mdDecoder.IsProtected(type) && mdDecoder.IsInternal(type))
                    access = Access.INTERNAL_AND_OR_PROTECTED;
                else if (mdDecoder.IsInternal(type))
                    access = Access.INTERNAL;
                else if (mdDecoder.IsPublic(type))
                    access = Access.PUBLIC;
                else if (mdDecoder.IsProtected(type))
                    access = Access.PROTECTED;
                else if (mdDecoder.IsPrivate(type))
                    access = Access.PRIVATE;
            }
            else
            {
                if (mdDecoder.IsPublic(type))
                    access = Access.PUBLIC;
                else
                    access = Access.INTERNAL;
            }

            Class.ClassType clt = Class.ClassType.CLASS;
            if (mdDecoder.IsClass(type))
                clt = Class.ClassType.CLASS;
            else if (mdDecoder.IsStruct(type))
                clt = Class.ClassType.STRUCT;
            else if (mdDecoder.IsInterface(type))
                clt = Class.ClassType.INTERFACE;

            Namespace ParentNS = mCm.LookupNamespaceByName(GetNamespaceFromClass(type));

            Class parent = null;
            Type parentt;
            if (mdDecoder.IsNested(type, out parentt))
                parent = LookupClass(parentt, false);

            string basecl = "";
            if (mdDecoder.HasBaseClass(type))
            {
                Type bt = mdDecoder.BaseClass(type);
                if (mdDecoder.FullName(bt) != "System.Object")
                    basecl = TypeFullName(bt, parent, ParentNS);
            }

            bool bIsAbstract = mdDecoder.IsAbstract(type) && !mdDecoder.IsInterface(type);
            bool bIsSealed = mdDecoder.IsSealed(type) && !mdDecoder.IsStruct(type);
            bool bIsStatic = false;//mdDecoder.IsStatic(type); types can be static only if abstract & sealed
            if (bIsAbstract && bIsSealed)
            {
                bIsAbstract = false;
                bIsSealed = false;
                bIsStatic = true;
            }

            Class cl = new Class(
              access,
              bIsAbstract,
              bIsSealed,
              bIsStatic,
              false, /* Updated when children are added */
              clt,
              name,
              parent,
              basecl,
              ParentNS);

            //** Attributes ?
            // Check if this class derives from System.Attribute, in which case we want to
            // assign it the attribute "AttributeUsage(AttributeTargets.All)"
            if (mdDecoder.HasBaseClass(type))
            {
                Type bt = mdDecoder.BaseClass(type);
                while (true)
                {
                    if (mdDecoder.FullName(bt) == "System.Attribute")
                        cl.AddAttribute("AttributeUsage(AttributeTargets.All)");

                    if (!mdDecoder.HasBaseClass(bt))
                        break;
                    else
                        bt = mdDecoder.BaseClass(bt);
                }
            }

            //** Formal type parameters
            Microsoft.Research.DataStructures.IIndexable<Type> formals;
            if (mdDecoder.IsGeneric(type, out formals, false))
                cl.AddFormalTypeParameters(GetFormalTypeParameters(formals));

            //** Interfaces
            var interfaces = mdDecoder.Interfaces(type);
            foreach (var it in interfaces)
            {
                if (this.SkipType(it)) continue;
                if (mdDecoder.FullName(it) != "System.Object")
                    cl.AddInterface(TypeFullName(it, cl, ParentNS));
            }

            Dictionary<EventExt, MethodExt> additionalEvents = new Dictionary<EventExt, MethodExt>();

            #region Methods
            bool hasRetainedConstructors = false;
            foreach (var m in mdDecoder.Methods(type))
            {
                if (mdDecoder.IsPropertyGetter(m) || mdDecoder.IsPropertySetter(m))
                    continue; // skip property accessors

                if (mdDecoder.IsFinalizer(m))
                    continue; // skip destructors

                if (SkipMethod(m))
                    continue; // compiler generated

                EventExt dev;
                if (mdDecoder.IsEventAdder(m, out dev) || mdDecoder.IsEventRemover(m, out dev))
                {
                    continue; // skip events adder/remover
                }

                bool foundEvt = false;
                foreach (var im in mdDecoder.ImplementedMethods(m))
                {
                    EventExt evt;
                    if (mdDecoder.IsEventAdder(im, out evt) || mdDecoder.IsEventRemover(im, out evt))
                    {
                        // looks like an event that is implemented with just add/remove but no explicit event decl
                        // create a dummy one
                        additionalEvents[evt] = m;
                        foundEvt = true;
                        break;
                    }
                }
                if (foundEvt)
                {
                    // skip the method
                    continue;
                }

                Method newm;
                var tmpName = mdDecoder.Name(m);
                if (tmpName.StartsWith("op_"))
                {
                    // user defined-conversion
                    Contract.Assert(tmpName.Length >= 3);
                    newm = ConstructUserOpFromMetaDataDecoder(m, cl);
                }
                else
                    newm = ConstructMethodFromMetaDataDecoder(m, cl);

                cl.AddChild(newm);
                hasRetainedConstructors |= mdDecoder.IsConstructor(m);

                // Keep track internally of the method
                mMethodsLookup.Add(m, newm);
            }

            // if there were no retained constructors, we might have to add an explicit one in order for base 
            // constructor call to work
            if (!hasRetainedConstructors && !bIsStatic && clt == Class.ClassType.CLASS)
            {
                var ctor = ConstructDefaultConstructor(type, cl);
                cl.AddChild(ctor);
            }
            #endregion

            #region Properties

            foreach (var prop in mdDecoder.Properties(type))
            {
                if (SkipProperty(prop)) { continue; }
                Property newprop = ConstructPropertyFromMetaDataDecoder(prop, cl);
                //Property newprop = LookupProperty(prop);
                cl.AddChild(newprop);

                // Keep track internally of the property
                mPropertiesLookup.Add(prop, newprop);
            }
            #endregion

            #region Events

            foreach (var evt in mdDecoder.Events(type))
            {
                if (SkipEvent(evt)) { continue; }

                Event newEvt = ConstructEventFromMetaDataDecoder(evt, cl);
                //Property newprop = LookupProperty(prop);
                cl.AddChild(newEvt);

                // Keep track internally of the event
                //mPropertiesLookup.Add(evt, newEvt);
            }

            foreach (var pair in additionalEvents)
            {
                var implEvent = pair.Key;
                var implMethod = pair.Value;

                var newEvt = ConstructEventFromProxy(implEvent, implMethod, cl);
                cl.AddChild(newEvt);
            }
            #endregion

            #region Fields
            //** Fields
            foreach (var f in mdDecoder.Fields(type))
            {
                if (!SkipField(f))
                {
                    Field newf = ConstructFieldFromMetaDataDecoder(f, cl);
                    cl.AddChild(newf);
                }
            }
            #endregion

            return cl;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private Event ConstructEventFromProxy(EventExt evt, MethodExt implMethod, Class clparent)
        {
            MethodExt Adder, Remover;
            bool bHasAdder = mdDecoder.HasAdder(evt, out Adder);
            bool bHasRemover = mdDecoder.HasRemover(evt, out Remover);
            string name = mdDecoder.Name(evt);
            bool bIsInInterface = false;

            MethodExt any = implMethod;
            Namespace ns = LookupNamespaceFromClass(mdDecoder.DeclaringType(any));

            Type handlerType = mdDecoder.HandlerType(evt);
            string returntype = TypeFullName(handlerType, clparent, ns);

            // Accessibility should be the less restrictive for the main property
            // And then if an accessor is more restrictive, it should be output just for it
            Access access = Access.NONE;
            Access accessget = Access.NONE;
            Access accessset = Access.NONE;
            if (!name.Contains(".") && !bIsInInterface)
                access = GetMostAccessible(bHasAdder, bHasRemover, Adder, Remover);

            bool bExtern = mdDecoder.IsExtern(any);
            bool bVirtual = mdDecoder.IsVirtual(any);
            bool bSealed = mdDecoder.IsSealed(any);
            bool bNewSlot = mdDecoder.IsNewSlot(any);
            bool bOverride = mdDecoder.IsOverride(any);
            bool bImplicitImpl = mdDecoder.IsImplicitImplementation(any);
            bool bAbstract = mdDecoder.IsAbstract(any);
            bool bStatic = mdDecoder.IsStatic(evt);

            bool bActualExtern = false;
            bool bActualStatic = false;
            bool bActualVirtual = false;
            bool bActualSealed = false;
            bool bActualNewSlot = false;
            bool bActualOverride = false;
            bool bActualAbstract = false;

            if (!bIsInInterface)
            {
                if (!mdDecoder.IsStruct(mdDecoder.DeclaringType(evt))
                    && !mdDecoder.IsSealed(mdDecoder.DeclaringType(evt)))
                {
                    if (bVirtual && bSealed && bNewSlot && bImplicitImpl)
                        access = Access.PUBLIC;
                    else if (bVirtual && bSealed)
                    {
                        bActualSealed = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bNewSlot && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bNewSlot)
                    {
                        bActualNewSlot = true;
                        bActualVirtual = true;
                    }
                    else if (bVirtual && access == Access.PRIVATE /* explicit interface */ && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bOverride)
                    {
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bExtern)
                        bActualAbstract = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                    else if (bVirtual)
                        bActualVirtual = true;
                }
                else
                {
                    if (bVirtual && bOverride)
                        bActualOverride = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                }

                bActualStatic = bStatic;
            }

            // get accessibility on getter and setter only when the main access has been discovered
            if (!name.Contains(".") && !bIsInInterface)
            {
                if (bHasAdder)
                {
                    accessget = GetAccessForMethod(Adder);
                    if (accessget == access) // not more restrictive than the property
                        accessget = Access.NONE;
                }

                if (bHasRemover)
                {
                    accessset = GetAccessForMethod(Remover);
                    if (accessset == access) // not more restrictive than the property
                        accessset = Access.NONE;
                }
            }

            Event newEvent = new Event(
              clparent,
              bHasAdder,
              bHasRemover,
              name,
              mdDecoder.IsUnmanagedPointer(handlerType), /* Parameters are handled when added */
              bIsInInterface,
              returntype,
              access,
              accessget,
              accessset,
              bActualStatic,
              bActualNewSlot,
              bActualOverride,
              bActualSealed,
              bActualVirtual,
              bActualExtern,
              bActualAbstract);

            return newEvent;
        }

        private bool MustKeepProperty(PropertyExt prop)
        {
            if (MustKeepType(mdDecoder.DeclaringType(prop)))
            {
                if (SkipType(mdDecoder.PropertyType(prop))) return false;
                return true;
            }
            return false;
        }

        private bool MustKeepEvent(EventExt evt)
        {
            if (MustKeepType(mdDecoder.DeclaringType(evt)))
            {
                if (SkipType(mdDecoder.HandlerType(evt))) return false;
                return true;
            }
            return false;
        }

        private bool MustKeepMethod(MethodExt method)
        {
            if (MustKeepType(mdDecoder.DeclaringType(method)))
            {
                if (SkipType(mdDecoder.ReturnType(method))) return false;
                foreach (var par in mdDecoder.Parameters(method).Enumerate())
                {
                    if (SkipType(mdDecoder.ParameterType(par))) return false;
                }
                return true;
            }
            return false;
        }

        private bool MustKeepType(Type type)
        {
            return MustKeepType(type, new Set<Type>());
        }

        private bool MustKeepType(Type type, Set<Type> seen)
        {
            if (seen.Contains(type)) return true;
            seen.Add(type);
            if (mdDecoder.IsInterface(type))
            {
                foreach (var intf in mdDecoder.Interfaces(type))
                {
                    IIndexable<Type> args;
                    if (mdDecoder.IsSpecialized(intf, out args))
                    {
                        foreach (var arg in args.Enumerate())
                        {
                            if (SkipType(arg, seen))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        private bool IsVirtual(PropertyExt prop, out MethodExt rep)
        {
            MethodExt getter, setter;
            bool isVirtual = false;
            rep = default(MethodExt);

            if (mdDecoder.HasGetter(prop, out getter))
            {
                rep = getter;
                isVirtual = mdDecoder.IsVirtual(getter);
            }
            if (mdDecoder.HasSetter(prop, out setter))
            {
                if (mdDecoder.IsVirtual(setter))
                {
                    rep = setter;
                    isVirtual = true;
                }
            }
            return isVirtual; ;
        }

        private bool SkipProperty(PropertyExt prop)
        {
            if (MustKeepProperty(prop)) return false;

            if (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(prop))
            {
                // check that it is not a visible interface implementation or required by abstract base class
                MethodExt rep;
                if (IsVirtual(prop, out rep))
                {
                    if (!SkipMethod(rep)) return false;
                }

                return true;
            }

            return ContainsNonCSharpType(mdDecoder.PropertyType(prop));
        }

        private bool IsVirtual(EventExt prop, out MethodExt rep)
        {
            MethodExt adder, remover;
            bool isVirtual = false;
            rep = default(MethodExt);

            if (mdDecoder.HasAdder(prop, out adder))
            {
                rep = adder;
                isVirtual = mdDecoder.IsVirtual(adder);
            }
            if (mdDecoder.HasRemover(prop, out remover))
            {
                if (mdDecoder.IsVirtual(remover))
                {
                    rep = remover;
                    isVirtual = true;
                }
            }
            return isVirtual; ;
        }

        private bool SkipEvent(EventExt evt)
        {
            if (MustKeepEvent(evt)) return false;

            if (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(evt))
            {
                MethodExt rep;
                if (IsVirtual(evt, out rep))
                {
                    if (!SkipMethod(rep)) return false;
                }
                return true;
            }
            return ContainsNonCSharpType(mdDecoder.HandlerType(evt));
        }


        private bool SkipField(FieldExt f)
        {
            if (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(f)) return true;

            return mdDecoder.IsCompilerGenerated(f) || IsNonCSharpType(mdDecoder.FieldType(f));
        }

        internal bool SkipMethod(MethodExt m)
        {
            if (MustKeepMethod(m)) return false;

            if (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(m))
            {
                // check that it is not a visible interface implementation or required by abstract base class
                if (mdDecoder.IsVirtual(m))
                {
                    foreach (var baseMethod in mdDecoder.OverriddenAndImplementedMethods(m))
                    {
                        if (!SkipMethod(baseMethod)) return false;
                    }
                }
                return true;
#if false
                bool isConstructor = mdDecoder.IsConstructor(m);
                // if it is a constructor of a publicly visible type, keep it unless its parameter types are not visible
                if (!isConstructor || AnyInternalParameterTypes(mdDecoder.Parameters(m)))
                {
                    return true;
                }
                if (!mdDecoder.IsVisibleOutsideAssembly(mdDecoder.DeclaringType(m)))
                {
                    return true;
                }
#endif
            }
            bool skip = mdDecoder.IsCompilerGenerated(m) || mdDecoder.Name(m) == ".cctor";
            if (skip) return true;

            if (ContainsNonCSharpType(mdDecoder.ReturnType(m))) return true;

            // check parameter types
            foreach (var p in mdDecoder.Parameters(m).Enumerate())
            {
                if (ContainsNonCSharpType(mdDecoder.ParameterType(p))) { return true; } // skip method
            }
            return false;
        }

        private bool AnyInternalParameterTypes(IIndexable<ParameterExt> iIndexable)
        {
            foreach (var p in iIndexable.Enumerate())
            {
                if (!mdDecoder.IsVisibleOutsideAssembly(mdDecoder.ParameterType(p)))
                {
                    return true;
                }
            }
            return false;
        }

        public Enum ConstructEnumFromMetaDataDecoder(Type type)
        {
            Access access = Access.PUBLIC;
            if (mdDecoder.IsPublic(type))
                access = Access.PUBLIC;
            else if (mdDecoder.IsProtected(type) && mdDecoder.IsInternal(type))
                access = Access.INTERNAL_AND_OR_PROTECTED;
            else if (mdDecoder.IsProtected(type))
                access = Access.PROTECTED;
            else if (mdDecoder.IsPrivate(type))
                access = Access.PRIVATE;
            else if (mdDecoder.IsInternal(type))
                access = Access.INTERNAL;

            Type parentt;
            Class clparent = null;
            if (mdDecoder.IsNested(type, out parentt))
                clparent = LookupClass(parentt, false);

            Namespace ns;
            if (clparent == null)
                ns = LookupNamespaceFromClass(type);
            else
                ns = clparent.Namespace;

            Type enumtype = mdDecoder.TypeEnum(type);

            Enum newenum = new Enum(access, clparent, ns, mdDecoder.Name(type), TypeFullName(enumtype, clparent, ns));

            foreach (var f in mdDecoder.Fields(type))
            {
                Contract.Assume(f != null, "missing contract");

                string name = mdDecoder.Name(f);
                object value;
                if (name != "value__" && mdDecoder.TryInitialValue(f, out value))
                {
                    newenum.AddField(new Enum.EnumField(name, value.ToString()));
                }
            }

            return newenum;
        }

        public Parameter ConstructParameterFromMetaDataDecoder(int index, ParameterExt param, Class clparent)
        {
            Type ptype = mdDecoder.ParameterType(param);
            MethodExt parentm = mdDecoder.DeclaringMethod(param);
            Type parent = mdDecoder.DeclaringType(parentm);
            Namespace ns = LookupNamespaceFromClass(parent);

            string typename = TypeFullName(ptype, clparent, ns);

            Parameter.Modifier modif = Parameter.Modifier.NONE;
            // Order is important because an out parameter is also a ref
            if (mdDecoder.IsOut(param) && mdDecoder.IsManagedPointer(ptype))
                modif = Parameter.Modifier.OUT;
            else if (mdDecoder.IsManagedPointer(ptype))
                modif = Parameter.Modifier.REF;

            if (modif != Parameter.Modifier.NONE)
                typename = TypeFullName(mdDecoder.ElementType(ptype), clparent, ns);

            var name = mdDecoder.Name(param);
            return new Parameter(typename, name, modif);
        }

        public Delegate ConstructDelegateFromMetaDataDecoder(Type type, Class clparent)
        {
            bool bIsInInterface = false;
            Type parentt;
            if (mdDecoder.IsNested(type, out parentt))
                bIsInInterface = mdDecoder.IsInterface(parentt);

            Access access;
            if (bIsInInterface)
                access = Access.NONE;
            else if (mdDecoder.IsPublic(type))
                access = Access.PUBLIC;
            else if (mdDecoder.IsProtected(type) && mdDecoder.IsInternal(type))
                access = Access.INTERNAL_AND_OR_PROTECTED;
            else if (mdDecoder.IsProtected(type))
                access = Access.PROTECTED;
            else if (mdDecoder.IsPrivate(type))
                access = Access.PRIVATE;
            else if (mdDecoder.IsInternal(type))
                access = Access.INTERNAL;
            else
                access = Access.NONE;

            Namespace ns = LookupNamespaceFromClass(type);

            // Look for the Invoke method to get the return type
            var methods = mdDecoder.Methods(type);
            Type rettypet = default(Type);
            string rettype = "";
            Microsoft.Research.DataStructures.IIndexable<ParameterExt> pars = null;
            foreach (var m in methods)
            {
                if (mdDecoder.Name(m) == "Invoke")
                {
                    rettypet = mdDecoder.ReturnType(m);
                    rettype = TypeFullName(rettypet, clparent, ns);
                    pars = mdDecoder.Parameters(m);
                    break;
                }
            }

            if (pars != null)
            {
                string name = mdDecoder.Name(type);
                Delegate newdel = new Delegate(
                  access,
                  mdDecoder.IsUnmanagedPointer(rettypet), /* Parameters are handled when added */
                  name,
                  clparent,
                  ns,
                  rettype);

                //** Formal type parameters
                Microsoft.Research.DataStructures.IIndexable<Type> formals;
                if (mdDecoder.IsGeneric(type, out formals, false))
                    newdel.AddFormalTypeParameters(GetFormalTypeParameters(formals));

                //** Add parameters
                int n = pars.Count;
                for (int i = 0; i < n; ++i)
                {
                    Parameter newparam = ConstructParameterFromMetaDataDecoder(i, pars[i], clparent);
                    newdel.AddParameter(newparam);
                }

                return newdel;
            }

            return null;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        private Method ConstructUserOpFromMetaDataDecoder(MethodExt mMethod, Class clparent)
        {
            Contract.Requires(mdDecoder.Name(mMethod).Length >= 3);
            // here we assume the name of the method is op_xxx
            // The full list is taken from ECMA standard
            // (http://www.ecma-international.org/publications/files/ECMA-ST/Ecma-335.pdf) chapter 10.3.1 and following

            Method newm = ConstructMethodFromMetaDataDecoder(mMethod, clparent);
            newm.Operator = true;

            string keyword = mdDecoder.Name(mMethod).Substring(3);
            string name = keyword;
            bool bImplicit = false;
            bool bExplicit = false;

            switch (keyword)
            {
                case "Implicit":
                    name = newm.ReturnType;
                    newm.ReturnType = "";
                    bImplicit = true;
                    break;
                case "Explicit":
                    name = newm.ReturnType;
                    newm.ReturnType = "";
                    bExplicit = true;
                    break;
                case "Decrement": name = "-- "; break;
                case "Increment": name = "++ "; break;
                case "UnaryNegation": name = "- "; break;
                case "UnaryPlus": name = "+ "; break;
                case "LogicalNot": name = "! "; break;
                case "AddressOf": name = "& "; break;
                case "OnesComplement": name = "~ "; break;
                case "PointerDereference": name = "* "; break;
                case "Addition": name = "+ "; break;
                case "Subtraction": name = "- "; break;
                case "Multiply": name = "* "; break;
                case "Division": name = "/ "; break;
                case "Modulus": name = "% "; break;
                case "ExclusiveOr": name = "^ "; break;
                case "BitwiseAnd": name = "& "; break;
                case "BitwiseOr": name = "| "; break;
                case "LogicalAnd": name = "&& "; break;
                case "LogicalOr": name = "|| "; break;
                case "LeftShift": name = "<< "; break;
                case "RightShift": name = ">> "; break;
                case "Equality": name = "== "; break;
                case "GreaterThan": name = "> "; break;
                case "LessThan": name = "< "; break;
                case "Inequality": name = "!= "; break;
                case "GreaterThanOrEqual": name = ">= "; break;
                case "LessThanOrEqual": name = "<="; break;
                case "MemberSelection": name = "->"; break;
                case "RightShiftAssignment": name = "<<= "; break;
                case "MultiplicationAssignment": name = "*= "; break;
                case "PointerToMemberSelection": name = "->* "; break;
                case "SubtractionAssignment": name = "-= "; break;
                case "ExclusiveOrAssignment": name = "^= "; break;
                case "LeftShiftAssignment": name = "<<="; break;
                case "ModulusAssignment": name = "%= "; break;
                case "AdditionAssignment": name = "+= "; break;
                case "BitwiseAndAssignment": name = "&= "; break;
                case "BitwiseOrAssignment": name = "|= "; break;
                case "Comma": name = ", "; break;
                case "DivisionAssignment": name = "/= "; break;
            }

            newm.Implicit = bImplicit;
            newm.Explicit = bExplicit;
            newm.SetName(name);

            return newm;
        }

        private Access GetAccessForMethod(MethodExt mMethod)
        {
            if (mdDecoder.IsProtected(mMethod) && mdDecoder.IsInternal(mMethod))
                return Access.INTERNAL_AND_OR_PROTECTED;
            else if (mdDecoder.IsProtected(mMethod))
                return Access.PROTECTED;
            else if (mdDecoder.IsPrivate(mMethod))
                return Access.PRIVATE;
            else if (mdDecoder.IsInternal(mMethod))
                return Access.INTERNAL;
            else if (mdDecoder.IsPublic(mMethod))
                return Access.PUBLIC;
            else
                return Access.NONE;
        }

        public Method ConstructDefaultConstructor(Type parentType, Class clparent)
        {
            Contract.Requires(clparent != null);
            Namespace ns = LookupNamespaceFromClass(parentType);

            string CustomInitList = "";
            bool bBaseConstructor = false;
            MethodExt baseConstructor = default(MethodExt);
            if (mdDecoder.HasBaseClass(parentType))
            {
                Type baseclass = mdDecoder.BaseClass(parentType);
                if (mdDecoder.FullName(baseclass) != "System.Object")
                    bBaseConstructor = GetAConstructor(baseclass, parentType, out baseConstructor);
            }

            if (bBaseConstructor)
            {
                var pars = mdDecoder.Parameters(baseConstructor);
                if (pars.Count > 0)
                {
                    // only output if not the default constructor
                    CustomInitList += " : base (";
                    for (int i = 0; i < pars.Count; ++i)
                    {
                        if (i > 0)
                            CustomInitList += ", ";
                        CustomInitList += "default(" + TypeFullName(mdDecoder.ParameterType(pars[i]), clparent, ns) + ")";
                    }
                    CustomInitList += ")";
                }
            }

            Method newmeth = new Method(
              Access.INTERNAL,
              clparent,
              "void",
              mdDecoder.Name(parentType),
              false,
              false,
              false,
              false,
              false,
              false,
              false,
              true,
              false,
              false,
              CustomInitList);

            //** Parameters

            // Initialize fields in a struct constructor
            if (mdDecoder.IsStruct(parentType))
            {
                foreach (var f in mdDecoder.Fields(parentType))
                {
                    if (SkipField(f)) continue;
                    if (!mdDecoder.IsStatic(f))
                        newmeth.AddVarToInitialize(TypeFullName(mdDecoder.FieldType(f), clparent, ns), "this." + mdDecoder.Name(f));
                }
            }

            return newmeth;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant", Justification = "Probably Clousot is right to report bExtern")]
        public Method ConstructMethodFromMetaDataDecoder(MethodExt mMethod, Class clparent)
        {
            bool bExtern = mdDecoder.IsExtern(mMethod);
            bool bStatic = mdDecoder.IsStatic(mMethod);
            bool bVirtual = mdDecoder.IsVirtual(mMethod);
            bool bSealed = mdDecoder.IsSealed(mMethod);
            bool bNewSlot = mdDecoder.IsNewSlot(mMethod);
            bool bAbstract = mdDecoder.IsAbstract(mMethod);
            bool bPrivate = mdDecoder.IsPrivate(mMethod);
            bool bOverride = mdDecoder.IsOverride(mMethod);
            bool bImplicitImpl = mdDecoder.IsImplicitImplementation(mMethod);

            bool bIsInInterface = mdDecoder.IsInterface(mdDecoder.DeclaringType(mMethod));
            Access access;
            if (bIsInInterface)
                access = Access.NONE;
            else if (mdDecoder.IsPublic(mMethod)
              || (bVirtual && bSealed && bNewSlot && bImplicitImpl))
                access = Access.PUBLIC;
            else
                access = GetAccessForMethod(mMethod);

            bool bIsConstructor = mdDecoder.IsConstructor(mMethod);
            bool bIsFinalizer = mdDecoder.IsFinalizer(mMethod);
            string rettype = "";
            Namespace ns = LookupNamespaceFromClass(mdDecoder.DeclaringType(mMethod));
            if (!bIsConstructor && !bIsFinalizer)
                rettype += TypeFullName(mdDecoder.ReturnType(mMethod), clparent, ns);

            //** Get actual modifiers from the metadata decoders values
            // and the correspondance table (cf. the .xlsx file)
            bool bActualExtern = false;
            bool bActualStatic = false;
            bool bActualVirtual = false;
            bool bActualSealed = false;
            bool bActualNewSlot = false;
            bool bActualAbstract = false;
            bool bActualOverride = false;

            if (!bIsInInterface)
            {
                if (!mdDecoder.IsStruct(mdDecoder.DeclaringType(mMethod))
                    && !mdDecoder.IsSealed(mdDecoder.DeclaringType(mMethod)))
                {
                    if (bVirtual && bSealed && bNewSlot && bImplicitImpl)
                        access = Access.PUBLIC;
                    else if (bVirtual && bSealed)
                    {
                        bActualSealed = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bNewSlot && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bNewSlot)
                    {
                        bActualNewSlot = true;
                        bActualVirtual = true;
                    }
                    else if (bVirtual && bPrivate /* explicit interface */ && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bOverride)
                    {
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bExtern)
                        bActualAbstract = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                    else if (bVirtual)
                        bActualVirtual = true;
                }
                else
                {
                    if (bVirtual && bOverride)
                        bActualOverride = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                }

                bActualStatic = bStatic;
            }

            Type parentType = mdDecoder.DeclaringType(mMethod);
            string name;
            if (bIsConstructor || bIsFinalizer)
                name = mdDecoder.Name(parentType);
            else
                name = mdDecoder.Name(mMethod);

            string CustomInitList = "";
            if (bIsConstructor)
            {
                bool bBaseConstructor = false;
                MethodExt baseConstructor = default(MethodExt);
                if (mdDecoder.HasBaseClass(parentType))
                {
                    Type baseclass = mdDecoder.BaseClass(parentType);
                    if (mdDecoder.FullName(baseclass) != "System.Object")
                        bBaseConstructor = GetAConstructor(baseclass, parentType, out baseConstructor);
                }

                if (bBaseConstructor)
                {
                    var pars = mdDecoder.Parameters(baseConstructor);
                    if (pars.Count > 0)
                    {
                        // only output if not the default constructor
                        CustomInitList += " : base (";
                        for (int i = 0; i < pars.Count; ++i)
                        {
                            if (i > 0)
                                CustomInitList += ", ";
                            CustomInitList += "default(" + TypeFullName(mdDecoder.ParameterType(pars[i]), clparent, ns) + ")";
                        }
                        CustomInitList += ")";
                    }
                }
            }

            Method newmeth = new Method(
              access,
              clparent,
              rettype,
              name,
              mdDecoder.IsUnmanagedPointer(mdDecoder.ReturnType(mMethod)), /* Parameters are taken into account below */
              bActualExtern,
              bActualStatic,
              bActualVirtual,
              bActualNewSlot,
              bActualSealed,
              bActualAbstract,
              bIsConstructor,
              bIsFinalizer,
              bActualOverride,
              CustomInitList);

            //** Formal type parameters
            Microsoft.Research.DataStructures.IIndexable<Type> formals;
            if (mdDecoder.IsGeneric(mMethod, out formals))
                newmeth.AddFormalTypeParameters(GetFormalTypeParameters(formals));

            //** Parameters
            var mpars = mdDecoder.Parameters(mMethod);
            int n = mpars.Count;
            for (int i = 0; i < n; ++i)
            {
                Parameter newparam = ConstructParameterFromMetaDataDecoder(i, mpars[i], clparent);
                newmeth.AddParameter(newparam);
            }

            // Initialize fields in a struct constructor
            if (mdDecoder.IsConstructor(mMethod) && mdDecoder.IsStruct(mdDecoder.DeclaringType(mMethod)))
            {
                foreach (var f in mdDecoder.Fields(parentType))
                {
                    if (SkipField(f)) continue;
                    if (!mdDecoder.IsStatic(f))
                        newmeth.AddVarToInitialize(TypeFullName(mdDecoder.FieldType(f), clparent, ns), "this." + mdDecoder.Name(f));
                    else if (!mdDecoder.IsReadonly(f) || mdDecoder.IsStatic(mMethod)) // if readonly, can only be assigned in a static constructor
                        newmeth.AddVarToInitialize(TypeFullName(mdDecoder.FieldType(f), clparent, ns), clparent.Name + "." + mdDecoder.Name(f));
                }
            }

            return newmeth;
        }

        private Access GetMostAccessible(bool bIsm1Valid, bool bIsm2Valid, MethodExt m1, MethodExt m2)
        {
            Access access1 = Access.NONE;
            Access access2 = Access.NONE;

            if (bIsm1Valid)
                access1 = GetAccessForMethod(m1);
            if (bIsm2Valid)
                access2 = GetAccessForMethod(m2);

            if (!bIsm1Valid)
                return access2;
            if (!bIsm2Valid)
                return access1;

            if (access1 == Access.PRIVATE)
                return access2;
            if (access2 == Access.PRIVATE)
                return access1;

            if (access1 == Access.INTERNAL)
                return access2;
            if (access2 == Access.INTERNAL)
                return access1;

            if (access1 == Access.PROTECTED)
                return access2;
            if (access2 == Access.PROTECTED)
                return access1;

            if (access1 == Access.INTERNAL_AND_OR_PROTECTED)
                return access2;
            if (access2 == Access.INTERNAL_AND_OR_PROTECTED)
                return access1;

            return Access.PUBLIC;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public Property ConstructPropertyFromMetaDataDecoder(PropertyExt prop, Class clparent)
        {
            var md = mdDecoder;
            MethodExt Getter, Setter;
            bool bHasGetter = md.HasGetter(prop, out Getter);
            bool bHasSetter = md.HasSetter(prop, out Setter);
            string name = md.Name(prop);
            bool bIsInInterface = mdDecoder.IsInterface(mdDecoder.DeclaringType(prop));

            bool bIsIndexer = (name == "Item" || name.EndsWith(".Item")); // could be an explicit implementation
                                                                          // An indexer property can have an other name than "Item"
                                                                          // cf. http://msdn.microsoft.com/en-us/library/2549tw02(VS.71).aspx
            MethodExt getorset = bHasGetter ? Getter : Setter;
            Namespace ns = LookupNamespaceFromClass(mdDecoder.DeclaringType(getorset));

            var atts = md.GetAttributes(mdDecoder.DeclaringType(getorset));
            foreach (var att in atts)
            {
                string attname = md.Name(mdDecoder.AttributeType(att));
                if (attname == "DefaultMemberAttribute")
                {
                    var inds = md.PositionalArguments(att);
                    if (inds.Count > 0)
                    {
                        // The first (and only !) argument is the name of the indexer
                        string indexname = inds[0] as string;
                        if (indexname != null)
                        {
                            var propName = md.Name(prop);
                            Contract.Assume(propName != null);
                            if (propName == indexname || propName.EndsWith(indexname)) // could be an explicit implementation
                            {
                                bIsIndexer = true;
                                break;
                            }
                        }
                    }
                }
            }

            Type returnt;
            // There is a special case here.
            // If the property only has a setter, then the return type
            // is the type of the last parameter
            if (bHasGetter)
                returnt = mdDecoder.ReturnType(Getter);
            else
            {
                var pars = mdDecoder.Parameters(Setter);
                Contract.Assume(pars.Count > 0, "Setters have at least one parameter");
                var lastpar = pars[pars.Count - 1];
                returnt = mdDecoder.ParameterType(lastpar);
            }
            string returntype = TypeFullName(returnt, clparent, ns);

            // Accessibility should be the less restrictive for the main property
            // And then if an accessor is more restrictive, it should be output just for it
            Access access = Access.NONE;
            Access accessget = Access.NONE;
            Access accessset = Access.NONE;
            if (!name.Contains(".") && !bIsInInterface)
                access = GetMostAccessible(bHasGetter, bHasSetter, Getter, Setter);

            bool bExtern = mdDecoder.IsExtern(getorset);
            bool bVirtual = mdDecoder.IsVirtual(getorset);
            bool bSealed = mdDecoder.IsSealed(getorset);
            bool bNewSlot = mdDecoder.IsNewSlot(getorset);
            bool bOverride = mdDecoder.IsOverride(getorset);
            bool bImplicitImpl = mdDecoder.IsImplicitImplementation(getorset);
            bool bAbstract = mdDecoder.IsAbstract(getorset);
            bool bStatic = mdDecoder.IsStatic(prop);

            bool bActualExtern = false;
            bool bActualStatic = false;
            bool bActualVirtual = false;
            bool bActualSealed = false;
            bool bActualNewSlot = false;
            bool bActualOverride = false;
            bool bActualAbstract = false;

            if (!bIsInInterface)
            {
                if (!mdDecoder.IsStruct(mdDecoder.DeclaringType(prop))
                    && !mdDecoder.IsSealed(mdDecoder.DeclaringType(prop)))
                {
                    if (bVirtual && bSealed && bNewSlot && bImplicitImpl)
                    {
                        access = Access.PUBLIC;
                    }
                    else if (bVirtual && bSealed)
                    {
                        bActualSealed = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bNewSlot && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bNewSlot)
                    {
                        bActualNewSlot = true;
                        bActualVirtual = true;
                    }
                    else if (bVirtual && access == Access.PRIVATE /* explicit interface */ && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bOverride)
                    {
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bExtern)
                        bActualAbstract = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                    else if (bVirtual)
                        bActualVirtual = true;
                }
                else
                {
                    if (bVirtual && bOverride)
                        bActualOverride = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                }

                bActualStatic = bStatic;
            }

            // get accessibility on getter and setter only when the main access has been discovered
            if (!name.Contains(".") && !bIsInInterface)
            {
                if (bHasGetter)
                {
                    accessget = GetAccessForMethod(Getter);
                    if (accessget == access) // not more restrictive than the property
                        accessget = Access.NONE;
                }

                if (bHasSetter)
                {
                    accessset = GetAccessForMethod(Setter);
                    if (accessset == access) // not more restrictive than the property
                        accessset = Access.NONE;
                }
            }



            //bool bIsStatic = mdDecoder.IsStatic (prop);
            //bool bIsNewSlot = mdDecoder.IsNewSlot(prop) && !mdDecoder.IsSealed(mdDecoder.DeclaringType(prop)) && !mdDecoder.IsStruct(mdDecoder.DeclaringType(prop));
            //bool bIsOverride = !bIsNewSlot && mdDecoder.IsOverride(prop) && !mdDecoder.IsSealed(mdDecoder.DeclaringType(prop)) && !mdDecoder.IsStruct(mdDecoder.DeclaringType(prop));
            //bool bIsSealed = !bIsNewSlot && mdDecoder.IsSealed(prop) && !mdDecoder.IsSealed(mdDecoder.DeclaringType(prop)) && !mdDecoder.IsStruct(mdDecoder.DeclaringType(prop));

            bIsIndexer = bIsIndexer && mdDecoder.Parameters(getorset).Count > 0; // double-check, you can have a property called Item

            Property newprop = new Property(
              clparent,
              bHasGetter,
              bHasSetter,
              name,
              mdDecoder.IsUnmanagedPointer(returnt), /* Parameters are handled when added */
              bIsInInterface,
              bIsIndexer,
              returntype,
              access,
              accessget,
              accessset,
              bActualStatic,
              bActualNewSlot,
              bActualOverride,
              bActualSealed,
              bActualVirtual,
              bActualExtern,
              bActualAbstract);

            // Add parameters for indexers
            if (bIsIndexer)
            {
                var pars = mdDecoder.Parameters(getorset);
                int nb_par = pars.Count;
                if (!bHasGetter)
                    --nb_par; // Skip last parameter, it's "value"
                for (int i = 0; i < nb_par; ++i)
                    newprop.AddParameter(ConstructParameterFromMetaDataDecoder(i, pars[i], clparent));
            }

            return newprop;
        }

        [SuppressMessage("Microsoft.Contracts", "TestAlwaysEvaluatingToAConstant")]
        public Event ConstructEventFromMetaDataDecoder(EventExt evt, Class clparent)
        {
            Contract.Requires(clparent != null);

            MethodExt Adder, Remover;
            bool bHasAdder = mdDecoder.HasAdder(evt, out Adder);
            bool bHasRemover = mdDecoder.HasRemover(evt, out Remover);
            string name = mdDecoder.Name(evt);
            bool bIsInInterface = mdDecoder.IsInterface(mdDecoder.DeclaringType(evt));

            MethodExt any = bHasAdder ? Adder : Remover;
            Namespace ns = LookupNamespaceFromClass(mdDecoder.DeclaringType(any));

            Type handlerType = mdDecoder.HandlerType(evt);
            string returntype = TypeFullName(handlerType, clparent, ns);

            // Accessibility should be the less restrictive for the main property
            // And then if an accessor is more restrictive, it should be output just for it
            Access access = Access.NONE;
            Access accessget = Access.NONE;
            Access accessset = Access.NONE;
            if (!name.Contains(".") && !bIsInInterface)
                access = GetMostAccessible(bHasAdder, bHasRemover, Adder, Remover);

            bool bExtern = mdDecoder.IsExtern(any);
            bool bVirtual = mdDecoder.IsVirtual(any);
            bool bSealed = mdDecoder.IsSealed(any);
            bool bNewSlot = mdDecoder.IsNewSlot(any);
            bool bOverride = mdDecoder.IsOverride(any);
            bool bImplicitImpl = mdDecoder.IsImplicitImplementation(any);
            bool bAbstract = mdDecoder.IsAbstract(any);
            bool bStatic = mdDecoder.IsStatic(evt);

            bool bActualExtern = false;
            bool bActualStatic = false;
            bool bActualVirtual = false;
            bool bActualSealed = false;
            bool bActualNewSlot = false;
            bool bActualOverride = false;
            bool bActualAbstract = false;

            if (!bIsInInterface)
            {
                if (!mdDecoder.IsStruct(mdDecoder.DeclaringType(evt))
                    && !mdDecoder.IsSealed(mdDecoder.DeclaringType(evt)))
                {
                    if (bVirtual && bSealed && bNewSlot && bImplicitImpl)
                        access = Access.PUBLIC;
                    else if (bVirtual && bSealed)
                    {
                        bActualSealed = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bNewSlot && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bAbstract)
                        bActualAbstract = true;
                    else if (bVirtual && bNewSlot)
                    {
                        bActualNewSlot = true;
                        bActualVirtual = true;
                    }
                    else if (bVirtual && access == Access.PRIVATE /* explicit interface */ && bOverride)
                    {
                        bActualAbstract = true;
                        bActualOverride = true;
                    }
                    else if (bVirtual && bOverride)
                    {
                        bActualOverride = true;
                    }
                    else if (bVirtual && bAbstract && bExtern)
                        bActualAbstract = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                    else if (bVirtual)
                        bActualVirtual = true;
                }
                else
                {
                    if (bVirtual && bOverride)
                        bActualOverride = true;
                    else if (bVirtual && bExtern)
                        bActualExtern = true;
                }

                bActualStatic = bStatic;
            }

            // get accessibility on getter and setter only when the main access has been discovered
            if (!name.Contains(".") && !bIsInInterface)
            {
                if (bHasAdder)
                {
                    accessget = GetAccessForMethod(Adder);
                    if (accessget == access) // not more restrictive than the property
                        accessget = Access.NONE;
                }

                if (bHasRemover)
                {
                    accessset = GetAccessForMethod(Remover);
                    if (accessset == access) // not more restrictive than the property
                        accessset = Access.NONE;
                }
            }

            Event newEvent = new Event(
              clparent,
              bHasAdder,
              bHasRemover,
              name,
              mdDecoder.IsUnmanagedPointer(handlerType), /* Parameters are handled when added */
              bIsInInterface,
              returntype,
              access,
              accessget,
              accessset,
              bActualStatic,
              bActualNewSlot,
              bActualOverride,
              bActualSealed,
              bActualVirtual,
              bActualExtern,
              bActualAbstract);

            return newEvent;
        }

        public Field ConstructFieldFromMetaDataDecoder(FieldExt f, Class clparent)
        {
            Contract.Requires(clparent != null);

            Field.ParentType pt;
            Type parent = mdDecoder.DeclaringType(f);
            if (mdDecoder.IsStruct(parent))
                pt = Field.ParentType.STRUCT;
            else if (mdDecoder.IsClass(parent))
                pt = Field.ParentType.CLASS;
            else
                pt = Field.ParentType.INTERFACE;

            Access acc;
            if (mdDecoder.IsProtected(f) && mdDecoder.IsInternal(f))
                acc = Access.INTERNAL_AND_OR_PROTECTED;
            else if (mdDecoder.IsInternal(f))
                acc = Access.INTERNAL;
            else if (mdDecoder.IsPublic(f))
                acc = Access.PUBLIC;
            else if (mdDecoder.IsProtected(f))
                acc = Access.PROTECTED;
            else
                acc = Access.PRIVATE;

            Type fieldt = mdDecoder.FieldType(f);
            string typename = TypeFullName(fieldt, clparent, clparent.Namespace);
            string defaut = "";
            // For readability, don't even output this, but disable the corresponding warning instead
            //if (pt != Field.ParentType.STRUCT)
            //  defaut = "default(" + typename + ")";

            return new Field(
              typename,
              mdDecoder.Name(f),
              clparent,
              defaut,
              pt,
              acc,
              mdDecoder.IsUnmanagedPointer(fieldt),
              mdDecoder.IsReadonly(f),
              mdDecoder.IsStatic(f),
              mdDecoder.IsNewSlot(f),
              mdDecoder.IsVolatile(f)
              );
        }

        [ContractVerification(false)]
        public Class LookupClass(Type type, bool bVisitNested)
        {
            if (!mClassesLookup.ContainsKey(type))
                AddClass(type, bVisitNested);
            else if (bVisitNested)
            {
                // Here, it is requested that nested types are well registered
                // they could be still pending because the class could have been
                // added before without the bVisitNested flag
                foreach (var nested in mdDecoder.NestedTypes(type))
                    RegisterType(nested);
            }

            return mClassesLookup[type];
        }

        [ContractVerification(false)]
        public Method LookupMethod(MethodExt mMethod)
        {
            // Ensures the declaring type has been registered
            // so that the method has been registered
            LookupClass(mdDecoder.DeclaringType(mMethod), false);

            if (mMethodsLookup.ContainsKey(mMethod))
                return mMethodsLookup[mMethod];
            else
                return null;
        }

        [ContractVerification(false)]
        public Property LookupProperty(PropertyExt prop)
        {
            // Ensures the declaring type has been registered
            // so that the propery has been registered
            LookupClass(mdDecoder.DeclaringType(prop), false);

            if (mPropertiesLookup.ContainsKey(prop))
                return mPropertiesLookup[prop];
            else
                return null;
        }

        [ContractVerification(false)]
        public Class AddClass(Type type, bool bVisitNested)
        {
            Class newcl = ConstructClassFromMetaDataDecoder(type);
            mClassesLookup.Add(type, newcl);

            // Register the class to its containers
            newcl.Namespace.AddChild(newcl);
            if (newcl.Parent != null) // only register toplevel classes to the namespace
                newcl.Parent.AddChild(newcl);

            if (bVisitNested)
            {
                // Also register all nested types
                foreach (var nested in mdDecoder.NestedTypes(type))
                    RegisterType(nested);
            }

            return newcl;
        }

        private bool SkipType(Type type)
        {
            if (MustKeepType(type)) return false;

            // if type contains nested type we must keep, then we keep it too
            foreach (var nested in mdDecoder.NestedTypes(type))
            {
                if (!SkipType(nested)) return false;
            }

            return (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(type));
        }

        private bool SkipType(Type type, Set<Type> seen)
        {
            if (MustKeepType(type, seen)) return false;

            return (visibleMembersOnly && !mdDecoder.IsVisibleOutsideAssembly(type));
        }

        public void RegisterType(Type type)
        {
            if (SkipType(type)) return;

            if (IsNonCSharpType(type))
                return;

            // Order is important because an enum is also said to be a struct!
            if (mdDecoder.IsEnum(type))
                RegisterEnum(type);
            else if (mdDecoder.IsDelegate(type))
                RegisterDelegate(type);
            else if (mdDecoder.IsClass(type)
              || mdDecoder.IsStruct(type)
              || mdDecoder.IsInterface(type))
                LookupClass(type, true);
        }

        private bool IsNonCSharpType(Type type)
        {
            return mdDecoder.IsCompilerGenerated(type) ||
                   mdDecoder.IsNativeCpp(type) ||
                   mdDecoder.Namespace(type) == "<CrtImplementationDetails>";
        }

        private bool ContainsNonCSharpType(Type type)
        {
            if (IsNonCSharpType(type)) return true;

            if (mdDecoder.IsUnmanagedPointer(type))
            {
                return ContainsNonCSharpType(mdDecoder.ElementType(type));
            }
            if (mdDecoder.IsManagedPointer(type))
            {
                return ContainsNonCSharpType(mdDecoder.ElementType(type));
            }
            if (mdDecoder.IsArray(type))
            {
                return ContainsNonCSharpType(mdDecoder.ElementType(type));
            }
            return false;
        }

        public void RegisterEnum(Type type)
        {
            if (mdDecoder.IsEnum(type))
            {
                Enum newenum = ConstructEnumFromMetaDataDecoder(type);
                if (newenum.Parent == null)
                    newenum.Namespace.AddChild(newenum);
                else
                    newenum.Parent.AddChild(newenum);
            }
        }

        public void RegisterDelegate(Type type)
        {
            // Ensures the declaring type has been registered
            // so that we can have access to the parent
            Class parent = null;
            Type parentt;
            if (mdDecoder.IsNested(type, out parentt))
                parent = LookupClass(parentt, false);

            if (mdDecoder.IsDelegate(type))
            {
                Delegate newdel = ConstructDelegateFromMetaDataDecoder(type, parent);
                if (newdel == null)
                    return;

                if (newdel.Parent == null)
                    newdel.Namespace.AddChild(newdel);
                else
                    newdel.Parent.AddChild(newdel);
            }
        }

        public Namespace AddNamespace(string name)
        {
            return mCm.LookupNamespaceByName(name);
            /*Namespace newNS = mCm.LookupNamespaceByName (name);
            if (newNS == null)
            {
              newNS = new Namespace(name);
              mCm.AddNamespace(newNS);
            }
            return newNS;*/
        }

        public void Reset()
        {
            mClassesLookup.Clear();
            mMethodsLookup.Clear();
            mPropertiesLookup.Clear();
        }

        #region Assembly managing
        private void AddRefRef(Assembly assemb, Microsoft.Research.DataStructures.IMutableSet<string> added, List<Pair<string, Version>> refnames, string mAssemblyName)
        {
            string assname = mdDecoder.Name(assemb);
            if (assname != mAssemblyName && added.Add(assname)) // don't reference itself, useful for analysing system dlls
            {
                refnames.Add(new Pair<string, Version>(assname, mdDecoder.Version(assemb)));
                foreach (var r in mdDecoder.AssemblyReferences(assemb))
                    AddRefRef(r, added, refnames, mAssemblyName);
            }
        }

        public List<Pair<string, Version>> GetReferenceAssemblies(Assembly assemb)
        {
            var assemblies = new List<Pair<string, Version>>();
            var added = new Set<string>();
            //string str = System.IO.Path.GetFullPath("Microsoft.Contracts.dll");
            //System.Reflection.Assembly ass = System.Reflection.Assembly.LoadFile(str);
            //var asss = ass.GetReferencedAssemblies();

            var asss = mdDecoder.AssemblyReferences(assemb);
            foreach (var r in asss)
                AddRefRef(r, added, assemblies, mdDecoder.Name(assemb));

            return assemblies;
        }
        #endregion

        public CodeManager CodeManager { get { return mCm; } }

        private readonly IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder;

        private Dictionary<Type, Class> mClassesLookup;
        private Dictionary<MethodExt, Method> mMethodsLookup;
        private Dictionary<PropertyExt, Property> mPropertiesLookup;
        private CodeManager mCm;
        private List<string> mUsings;
        private bool visibleMembersOnly;
    }

    public class ContractsHandlerMgr<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult>
      where LogOptions : IFrameworkLogOptions
      where Type : IEquatable<Type>
    {
        #region Object invariant

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            Contract.Invariant(mdDecoder != null);
            Contract.Invariant(mAnalyzer != null);
        }

        #endregion


        #region Constructor
        public ContractsHandlerMgr(
            AnalysisDriver<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> mAnalyzer,
            IOutput mOutput
            )
        {
            Contract.Requires(mAnalyzer != null);

            this.mAnalyzer = mAnalyzer;
            mdDecoder = mAnalyzer.MetaDataDecoder;
            this.mOutput = mOutput;
            mAssembly = default(Assembly);
            mAssemblyName = "";
            mWrittenFiles = new List<string>();
            mCH = new ConstructionHelper<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>(mdDecoder, mAnalyzer.Options.OutputOnlyExternallyVisibleMembers);
            mStrategy = OutputHelper.OutputStrategy.ONE_FILE_PER_CLASS; // should be set later by "SetStrategy"
        }
        #endregion

        #region Privates
        private readonly AnalysisDriver<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly, Expression, Variable, LogOptions, MethodResult> mAnalyzer;
        private readonly IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder;
        private IOutput mOutput;

        private ConstructionHelper<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mCH;

        private string mAssemblyName;
        private Assembly mAssembly;

        private List<string> mWrittenFiles;
        // Statistics
        private int mNbPreMethods; /// number of registered preconditions in methods
        private int mNbPostMethods; /// number of registered postconditions in methods
        private int mNbPreProperties; /// number of registered preconditions in properties
        private int mNbPostProperties; /// number of registered postconditions in properties
        private int mNbClassInvariants; /// number of registered class invariants

        private OutputHelper.OutputStrategy mStrategy;
        #endregion

        public void SetStrategy(OutputHelper.OutputStrategy strat)
        {
            mStrategy = strat;
        }

        private bool UnmanagedExpression(BoxedExpression expr)
        {
            if (expr.IsBinary)
                return UnmanagedExpression(expr.BinaryLeft) || UnmanagedExpression(expr.BinaryRight);
            else if (expr.IsUnary)
                return UnmanagedExpression(expr.UnaryArgument);
            else if (expr.IsVariable)
            {
                var expAccessPath = expr.AccessPath;
                if (expAccessPath != null)
                {
                    Contract.Assert(Contract.ForAll(expAccessPath, p => p != null));
                    for (int i = 0; i < expAccessPath.Length; ++i)
                    {
                        if (expAccessPath[i].IsUnmanagedPointer)
                            return true; // There is an unmanaged pointer in the way
                    }
                }
            }
            else if (expr.IsResult)
            {
                object rettype;
                if (expr.TryGetType(out rettype))
                    return mdDecoder.IsUnmanagedPointer((Type)rettype);
            }

            return false;
        }

        private string BoxedExpressionConverter(Type type)
        {
            return OutputPrettyCS.TypeHelper.TypeFullName(mdDecoder, type);
        }

        #region Register Methods
        public void RegisterClassInvariant(Type type, BoxedExpression invariant)
        {
            Contract.Requires(invariant != null);

            if (mAssemblyName.Length == 0) // sanity check: output pretty C# is disabled
                return;

            if (mAnalyzer.Options.OutputOnlyExternallyVisibleMembers && !mdDecoder.IsVisibleOutsideAssembly(type)) return;

            if (mdDecoder.IsCompilerGenerated(type)) // no compiler generated types
                return;
            if (!mdDecoder.IsClass(type) && !mdDecoder.IsStruct(type)) // only for classes and struct
                return;

            Class cl = mCH.LookupClass(type, false);

            if (cl != null)
            {
                cl.AddInvariant(invariant.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter)));
                mNbClassInvariants++;
            }
        }

        /// <summary>
        /// Replace out parameters occurences in contracts by "ValueAtReturn(out x)" in fixed_exp to trick the compiler.
        /// Return true if a change has been made, false if not.
        /// </summary>
        private bool FixOutParameter(MethodExt parent, BoxedExpression exp, out BoxedExpression fixed_exp)
        {
            Contract.Ensures(!Contract.Result<bool>() || Contract.ValueAtReturn(out fixed_exp) != null);

            fixed_exp = exp;

            if (exp.IsVariable)
            {
                Microsoft.Research.DataStructures.FList<PathElement> path = Microsoft.Research.DataStructures.FList<PathElement>.Empty;
                for (int i = 0; i < exp.AccessPath.Length; ++i)
                {
                    var elem = exp.AccessPath[exp.AccessPath.Length - i - 1];
                    Contract.Assume(elem != null);
                    if (elem.IsModel) return false;
                    path = Microsoft.Research.DataStructures.FList<PathElement>.Cons(elem, path);
                }

                if (path != null && path.Head != null && path.Head.IsParameterRef)
                {
                    var pars = mdDecoder.Parameters(parent);

                    for (int i = 0; i < pars.Count; ++i)
                    {
                        var par = pars[i];
                        //SymbolicValue vpar, vpar2, vpar3, vpar4, vpar5;
                        //context.TryParameterAddress(context.EntryAfterRequires, par, out vpar);
                        //context.TryParameterValue(context.EntryAfterRequires, par, out vpar2);
                        //context.TryLoadIndirect(context.EntryAfterRequires, vpar, out vpar3);
                        //context.TryLoadIndirect(context.EntryAfterRequires, vpar2, out vpar4);
                        //context.TryLoadIndirect(context.EntryAfterRequires, var, out vpar5);
                        //if (var.Equals(vpar))
                        if (exp.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter)).StartsWith(mdDecoder.Name(par)) && mdDecoder.IsOut(par)) // allow to have something after, will be fixed by FixOutContracts
                        {
                            fixed_exp = BoxedExpression.ValueAtReturn(exp, mdDecoder.ParameterType(par));
                            return true;
                        }
                    }
                }
                return true;
            }
            else if (exp.IsUnary)
            {
                BoxedExpression fixd;
                if (FixOutParameter(parent, exp.UnaryArgument, out fixd))
                {
                    fixed_exp = BoxedExpression.Unary(exp.UnaryOp, fixd);
                    return true;
                }
            }
            else if (exp.IsBinary)
            {
                BoxedExpression fl, fr;
                var bl = FixOutParameter(parent, exp.BinaryLeft, out fl);
                var br = FixOutParameter(parent, exp.BinaryRight, out fr);

                if (bl && br)
                {
                    BoxedExpression.BinaryExpressionMethodCall bemc = exp as BoxedExpression.BinaryExpressionMethodCall;
                    if (bemc != null)
                    {
                        fixed_exp = BoxedExpression.BinaryMethodToCall(exp.BinaryOp, fl, fr, bemc.MethodToCall);
                    }
                    else
                    {
                        // Mic: kind of a hack. Would be nicer to have some kind of visitor on BoxedExpression
                        if (exp.IsCast)
                        {
                            var castExpression = exp as ClousotExpression<Type>.CastExpression;
                            Contract.Assume(castExpression != null);
                            fixed_exp = BoxedExpression.BinaryCast(exp.BinaryOp, fl, fr, castExpression.mTypeCasting);
                        }
                        else
                        {
                            fixed_exp = BoxedExpression.Binary(exp.BinaryOp, fl, fr);
                        }
                    }

                    return true;
                }
            }
            else if (exp.IsResult || exp.IsConstant)
            {
                return true;
            }
            // FIXME: recursively apply this transformation
            return false;
        }

        /// <summary>
        /// Replace field parameters occurences in struct constructors contracts by "ValueAtReturn(out x)" to trick the compiler
        /// </summary>
        /// <param name="parent">Assumed to be a constructor</param>
        private BoxedExpression FixFieldInConstructor(MethodExt parent, BoxedExpression exp)
        {
            Contract.Requires(exp != null);
            Contract.Ensures(Contract.Result<BoxedExpression>() != null);

            if (!mdDecoder.IsStruct(mdDecoder.DeclaringType(parent)))
                return exp;

            if (exp.IsVariable)
            {
                SymbolicValue var;
                if (exp.TryGetFrameworkVariable(out var))
                {
                    var fields = mdDecoder.Fields(mdDecoder.DeclaringType(parent));
                    foreach (var f in fields)
                    {
                        if (exp.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter)).StartsWith("this." + mdDecoder.Name(f))) // allow to have something after, will be fixed by FixOutContracts
                        {
                            object t;
                            if (exp.TryGetType(out t))
                                return BoxedExpression.ValueAtReturn(exp, (Type)t);
                            else
                                return BoxedExpression.ValueAtReturn(exp, default(Type));
                        }
                    }
                }
                else if (exp.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter)).StartsWith("this.")) // sometimes TryGetFrameworkVariable fails, cf. explication elsewhere
                {
                    object t;
                    if (exp.TryGetType(out t))
                        return BoxedExpression.ValueAtReturn(exp, (Type)t);
                    else
                        return BoxedExpression.ValueAtReturn(exp, default(Type));
                }
            }
            else if (exp.IsUnary)
                return BoxedExpression.Unary(exp.UnaryOp, FixFieldInConstructor(parent, exp.UnaryArgument));
            else if (exp.IsBinary)
                return BoxedExpression.Binary(exp.BinaryOp, FixFieldInConstructor(parent, exp.BinaryLeft), FixFieldInConstructor(parent, exp.BinaryRight));

            return exp;
        }

        /// <summary>
        /// Add the method to the C# pretty output database. Then it will be output with all
        /// the saved pre/post-conditions at the end of the analysis.
        /// </summary>
        /// <param name="method">Can be a getter or a setter.</param>
        public void RegisterMethodPreCondition(MethodExt method, BoxedExpression precond)
        {
            if (mAssemblyName.Length == 0) // sanity check: output pretty C# is disabled
                return;

            if (mCH.SkipMethod(method)) return;

            if (mdDecoder.Name(method) == ".cctor")
                return;

            if (mdDecoder.IsCompilerGenerated(method) || mdDecoder.IsCompilerGenerated(mdDecoder.DeclaringType(method)))
                return;

            if (!FixOutParameter(method, precond, out precond))
                return;

            if (mdDecoder.IsConstructor(method))
                precond = FixFieldInConstructor(method, precond);

            string precond_str = precond.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter));
            // HACK: the out trick does not take into account such code:
            // public Socket EndAccept (out byte[] buffer, System.IAsyncResult asyncResult) {
            // Contract.Ensures (Contracts.ValueAtReturn (out buffer.Length) >= 0);
            // so we just want to move the last parenthese
            precond_str = FixOutContracts(precond_str);

            bool getter = false;
            if ((getter = mdDecoder.IsPropertyGetter(method)) || mdDecoder.IsPropertySetter(method))
            {
                PropertyExt prop = mdDecoder.GetPropertyFromAccessor(method);
                Property p = mCH.LookupProperty(prop);
                if (p != null)
                {
                    if (getter)
                        p.AddPrecondToGet(precond_str);
                    else
                        p.AddPrecondToSet(precond_str);

                    if (UnmanagedExpression(precond))
                        p.ForceUnsafe(true);

                    ++mNbPreProperties;
                }
            }
            else
            {
                Method m = mCH.LookupMethod(method);
                if (m != null)
                {
                    m.AddPrecond(precond_str);

                    if (UnmanagedExpression(precond))
                        m.ForceUnsafe(true);

                    ++mNbPreMethods;
                }
            }
        }

        private string FixOutContracts(string input)
        {
            int begin = input.IndexOf("Contract.ValueAtReturn");
            if (begin != -1)
            {
                string pattern = @"((?<1>(this\.)?\w*)([^\)]*)?\))";
                string pattern_full = @"Contract.ValueAtReturn\(out " + pattern + @"(.*)";

                //string testinput = "Contract.ValueAtReturn(out this.membera.memberb.member3)";
                //string testinput = "capacity == Contract.ValueAtReturn(out this._array.Length)";
                //string testinput = "(Contract.ValueAtReturn(out this._tail) - Contract.ValueAtReturn(out this._array.Length)) < 0";
                //Match m = Regex.Match(testinput, pattern_full);
                string refined = input.Substring(begin);
                Match m = Regex.Match(refined, pattern_full);
                if (m.Success)
                {
                    string new_str = input.Substring(0, begin);
                    new_str += @"Contract.ValueAtReturn(out " + m.Groups[1].Captures[0].Value + ")";
                    if (m.Groups[3].Captures.Count >= 1) // remaining accessors
                        new_str += m.Groups[3].Captures[0].Value;
                    if (m.Groups[4].Captures.Count >= 1) // After the ValueAtReturn(...)
                        new_str += FixOutContracts(m.Groups[4].Captures[0].Value);
                    return new_str;
                }
            }


            return input;
        }

        /// <summary>
        /// Add the method to the C# pretty output database. Then it will be output with all
        /// the saved pre/post-conditions at the end of the analysis.
        /// </summary>
        /// <param name="method">Can be a getter or a setter.</param>
        public void RegisterMethodPostCondition(MethodExt method, BoxedExpression postcond)
        {
            if (mAssemblyName.Length == 0) // sanity check: output pretty C# is disabled
                return;

            if (mCH.SkipMethod(method)) return;

            if (mdDecoder.Name(method) == ".cctor")
                return;

            if (mdDecoder.IsCompilerGenerated(method) || mdDecoder.IsCompilerGenerated(mdDecoder.DeclaringType(method)))
                return;

            if (FilterPostCondition(postcond)) return;

            if (!FixOutParameter(method, postcond, out postcond))
                return;

            if (mdDecoder.IsConstructor(method))
                postcond = FixFieldInConstructor(method, postcond);

            string postcond_str = postcond.ToString(new System.Converter<Type, string>(this.BoxedExpressionConverter));
            // HACK: the out trick does not take into account such code:
            // public Socket EndAccept (out byte[] buffer, System.IAsyncResult asyncResult) {
            // Contract.Ensures (Contracts.ValueAtReturn (out buffer.Length) >= 0);
            // so we just want to move the last parenthese
            postcond_str = FixOutContracts(postcond_str);

            bool getter = false;
            if ((getter = mdDecoder.IsPropertyGetter(method)) || mdDecoder.IsPropertySetter(method))
            {
                PropertyExt prop = mdDecoder.GetPropertyFromAccessor(method);
                Property p = mCH.LookupProperty(prop);
                if (p != null)
                {
                    if (getter)
                        p.AddPostcondToGet(postcond_str);
                    else
                        p.AddPostcondToSet(postcond_str);

                    if (UnmanagedExpression(postcond))
                        p.ForceUnsafe(true);

                    ++mNbPostProperties;
                }
            }
            else
            {
                Method m = mCH.LookupMethod(method);
                if (m != null)
                {
                    m.AddPostcond(postcond_str);

                    if (UnmanagedExpression(postcond))
                        m.ForceUnsafe(true);

                    ++mNbPostMethods;
                }
            }
        }

        private class FilterByVisibility : IBoxedExpressionVisitor
        {
            public bool ignoreMe;
            private IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> mdDecoder;

            public FilterByVisibility(IDecodeMetaData<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly> iDecodeMetaData)
            {
                mdDecoder = iDecodeMetaData;
            }

            public void Variable(object var, PathElement[] path, BoxedExpression parent)
            {
                if (ignoreMe) return;
                if (path != null)
                {
                    for (int i = 0; i < path.Length; i++)
                    {
                        CheckPathElementVisibility(path[i]);
                        if (ignoreMe) return;
                    }
                }
            }

            private void CheckPathElementVisibility(PathElement pathElement)
            {
                var asField = pathElement as OptimisticHeapAnalyzer<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>.Domain.PathElement<FieldExt>;
                if (asField != null)
                {
                    if (!mdDecoder.IsVisibleOutsideAssembly(asField.Element))
                    {
                        ignoreMe = true;
                    }
                    return;
                }
                var asMethod = pathElement as OptimisticHeapAnalyzer<Local, ParameterExt, MethodExt, FieldExt, PropertyExt, EventExt, Type, Attribute, Assembly>.Domain.PathElement<MethodExt>;
                if (asMethod != null)
                {
                    if (!mdDecoder.IsVisibleOutsideAssembly(asMethod.Element))
                    {
                        ignoreMe = true;
                    }
                    return;
                }
            }

            public void ArrayIndex<Typ>(Typ type, BoxedExpression array, BoxedExpression index, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
                array.Dispatch(this);
                index.Dispatch(this);
            }

            public void Constant<Typ>(Typ type, object value, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
            }

            public void Binary(BinaryOperator binaryOperator, BoxedExpression left, BoxedExpression right, BoxedExpression parent)
            {
                if (ignoreMe) return;
                left.Dispatch(this);
                right.Dispatch(this);
            }

            public void Unary(UnaryOperator unaryOperator, BoxedExpression argument, BoxedExpression parent)
            {
                if (ignoreMe) return;
                argument.Dispatch(this);
            }

            public void SizeOf<Typ>(Typ type, int sizeAsConstant, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
            }

            public void IsInst<Typ>(Typ type, BoxedExpression argument, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
            }

            private void CheckTypeVisibility<Typ>(Typ type)
            {
                if (type is Type)
                {
                    var ourtype = (Type)(object)type;
                    if (!mdDecoder.IsVisibleOutsideAssembly(ourtype))
                    {
                        ignoreMe = true;
                        return;
                    }
                }
            }

            public void Result<Typ>(Typ type, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
            }

            public void Old<Typ>(Typ type, BoxedExpression expression, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
                expression.Dispatch(this);
            }

            public void ValueAtReturn<Typ>(Typ type, BoxedExpression expression, BoxedExpression parent)
            {
                if (ignoreMe) return;
                CheckTypeVisibility<Typ>(type);
                expression.Dispatch(this);
            }

            public void Assert(BoxedExpression condition, BoxedExpression parent)
            {
                if (ignoreMe) return;
                condition.Dispatch(this);
            }

            public void Assume(BoxedExpression condition, BoxedExpression parent)
            {
                if (ignoreMe) return;
                condition.Dispatch(this);
            }

            public void StatementSequence(IIndexable<BoxedExpression> statements, BoxedExpression parent)
            {
                for (int i = 0; i < statements.Count; i++)
                {
                    if (ignoreMe) return;
                    statements[i].Dispatch(this);
                }
            }

            public void ForAll(BoxedExpression boundVariable, BoxedExpression lower, BoxedExpression upper, BoxedExpression body, BoxedExpression parent)
            {
                if (ignoreMe) return;
                boundVariable.Dispatch(this);
                lower.Dispatch(this);
                upper.Dispatch(this);
                body.Dispatch(this);
            }
        }

        private bool FilterPostCondition(BoxedExpression postcond)
        {
            if (!mAnalyzer.Options.OutputOnlyExternallyVisibleMembers) return false;
            var filter = new FilterByVisibility(mdDecoder);
            postcond.Dispatch(filter);
            return filter.ignoreMe;
        }
        #endregion

        #region Pretty Output

        private const string SourceFolder = @"Sources\";

        public void PrettyCSOutput(string folder, bool verbose)
        {
            if (mAssemblyName.Length == 0) // sanity check: output pretty C# is disabled
                return;

            if (verbose)
            {
                mOutput.WriteLine("OutputPretty C#: has started...");
                mOutput.WriteLine("OutputPretty C#: <{0}, {1}> preconditions have been registered in <methods, properties>.", mNbPreMethods, mNbPreProperties);
                mOutput.WriteLine("OutputPretty C#: <{0}, {1}> postconditions have been registered in <methods, properties>.", mNbPostMethods, mNbPostProperties);
                mOutput.WriteLine("OutputPretty C#: {0} class invariants have been registered.", mNbClassInvariants);
            }

            if (folder.Length == 0)
                return;

            // Handy
            if (folder[folder.Length - 1] != '\\')
                folder = folder + "\\";
            folder += mAssemblyName; // One directory per assembly
            if (folder[folder.Length - 1] != '\\')
                folder = folder + "\\";

            if (!Directory.Exists(folder + SourceFolder))
            {
                try
                {
                    Directory.CreateDirectory(folder + SourceFolder);
                }
                catch
                {
                    mOutput.WriteLine("OutputPretty C#: Unable to create directory " + folder + SourceFolder + ": OutputPretty C# could not continue.");
                    return;
                }
            }

            try
            {
                // Ensures that each class is in the output (to compile if referenced from somewhere, even if it does not have any contract)
                foreach (var cl in mdDecoder.GetTypes(mAssembly))
                {
                    if (!mdDecoder.IsCompilerGenerated(cl) && mdDecoder.Name(cl) != "<Module>")
                        mCH.RegisterType(cl);
                }

                // Output the code
                mCH.CodeManager.OutputCode(folder, SourceFolder, mOutput, out mWrittenFiles, mStrategy, mCH.Usings, verbose);

                mCH.CodeManager.OutputProperties(folder, mAssemblyName, mWrittenFiles);

                mCH.CodeManager.OutputProjectFile(
                    folder + mAssemblyName + ".csproj",
                    mWrittenFiles,
                    mCH.GetReferenceAssemblies(mAssembly),
                    mOutput,
                    mAssemblyName,
                    mCH.CodeManager.HasUnsafeCode(),
                    verbose);
            }
            catch (OutOfMemoryException)
            {
                mOutput.WriteLine("OutputPretty C#: ran out of memory and stopped.");
                return;
            }
            catch (Exception e)
            {
                mOutput.WriteLine("OutputPretty C#: encountered an error and stopped. {0}", e.Message);
                return;
            }
        }
        #endregion

        #region Assembly managing
        public void StartAssembly(Assembly assembly)
        {
            mAssembly = assembly;
            if (assembly != null)
                mAssemblyName = mdDecoder.Name(assembly);
        }

        public void EndAssembly()
        {
            mAssemblyName = "";
            mAssembly = default(Assembly);
            mCH.Reset();
        }
        #endregion
    }

    //class DemoDoc
    //{
    //  void f()
    //  {
    //    Namespace ns = new Namespace("NSDemo");
    //    Class cl = new Class(
    //      Access.PUBLIC,
    //      false, /* abstract */
    //      false, /* sealed */
    //      false, /* static */
    //      false, /* unsafe */
    //      Class.ClassType.CLASS, /* type of the class (can be struct or interface) */
    //      "ClDemo", /* name */
    //      null, /* no nesting class */
    //      null, /* no base class */
    //      ns); /* enclosing namespace */

    //    Method m = new Method(
    //      Access.PUBLIC,
    //      cl, /* declaring class */
    //      "bool", /* return type */
    //      "FctDemo", /* name */
    //      false, /* attributes (abstract, extern, static, ...) */
    //      false, false, false, false, false, false, false, false, false,
    //      ""); /* initialization list (for constructors, for example " : base (...)") */

    //    Parameter p1 = new Parameter("int", "x", Parameter.Modifier.NONE /* ref/out/none */, false /* unsafe */);
    //    Parameter p2 = new Parameter("float", "x", Parameter.Modifier.OUT /* ref/out/none */, false /* unsafe */);
    //    m.AddParameter(p1);
    //    m.AddParameter(p2);
    //    m.AddLine("y = x;");
    //    m.AddLine("return x == 0;");

    //    cl.AddChild(m);

    //    CodeManager cm = new CodeManager();
    //    cm.AddNamespace(ns);

    //    List<string> WrittenFiles;
    //    List<string> usings = /*...*/;
    //    cm.OutputCode(folder, "files\\", /* IOutput to console */, out WrittenFiles, OutputHelper.OutputStrategy.ONE_FILE_PER_NAMESPACE , usings);

    //    cm.OutputProjectFile(
    //        folder + mAssemblyName + ".csproj", /* Project filename */
    //        WrittenFiles, /* files to compile */
    //        /* List of the reference assemblies to put in the project file */,
    //        /* IOutput to console */,
    //        mAssemblyName,
    //        cm.HasUnsafeCode());
    //  }
    //}
}

