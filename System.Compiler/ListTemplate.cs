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

using System;
using System.Diagnostics.Contracts;

namespace System.Compiler{
  public sealed class AssemblyNodeList
  {
    private AssemblyNode[]/*!*/ elements;
    private int count = 0;
    public AssemblyNodeList()
    {
      this.elements = new AssemblyNode[4];
      //^ base();
    }
    public AssemblyNodeList(int capacity)
    {
      this.elements = new AssemblyNode[capacity];
      //^ base();
    }
    public AssemblyNodeList(params AssemblyNode[] elements)
    {
      if (elements == null) elements = new AssemblyNode[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(AssemblyNode element)
    {
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n)
      {
        int m = n * 2; if (m < 4) m = 4;
        AssemblyNode[] newElements = new AssemblyNode[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count
    {
      get { return this.count; }
    }
    [Obsolete("Use Count property instead.")]
    public int Length
    {
      get { return this.count; }
    }
    public AssemblyNode this[int index]
    {
      get
      {
        return this.elements[index];
      }
      set
      {
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator()
    {
      return new Enumerator(this);
    }
    public struct Enumerator
    {
      private int index;
      private readonly AssemblyNodeList/*!*/ list;
      public Enumerator(AssemblyNodeList/*!*/ list)
      {
        this.index = -1;
        this.list = list;
      }
      public AssemblyNode Current
      {
        get
        {
          return this.list[this.index];
        }
      }
      public bool MoveNext()
      {
        return ++this.index < this.list.count;
      }
      public void Reset()
      {
        this.index = -1;
      }
    }
  }
  public sealed class AssemblyReferenceList
  {
    private AssemblyReference[]/*!*/ elements;
    private int count = 0;
    public AssemblyReferenceList(){
      this.elements = new AssemblyReference[4];
      //^ base();
    }
    public AssemblyReferenceList(int capacity){
      this.elements = new AssemblyReference[capacity];
      //^ base();
    }
    public void Add(AssemblyReference element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        AssemblyReference[] newElements = new AssemblyReference[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public AssemblyReferenceList/*!*/ Clone() {
      AssemblyReference[] elements = this.elements;
      int n = this.count;
      AssemblyReferenceList result = new AssemblyReferenceList(n);
      result.count = n;
      AssemblyReference[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public AssemblyReference this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly AssemblyReferenceList/*!*/ list;
      public Enumerator(AssemblyReferenceList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public AssemblyReference Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class AttributeList{
    private AttributeNode[]/*!*/ elements;
    private int count = 0;
    public AttributeList(){
      this.elements = new AttributeNode[4];
      //^ base();
    }
    public AttributeList(int capacity){
      this.elements = new AttributeNode[capacity];
      //^ base();
    }
    public AttributeList(params AttributeNode[] elements) {
      if (elements == null) elements = new AttributeNode[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(AttributeNode element) {
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        AttributeNode[] newElements = new AttributeNode[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public AttributeList/*!*/ Clone() {
      AttributeNode[] elements = this.elements;
      int n = this.count;
      AttributeList result = new AttributeList(n);
      result.count = n;
      AttributeNode[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public AttributeNode this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly AttributeList/*!*/ list;
      public Enumerator(AttributeList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public AttributeNode Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class BlockList{
    private Block[]/*!*/ elements;
    private int count = 0;
    public BlockList(){
      this.elements = new Block[4];
      //^ base();
    }
    public BlockList(int n){
      this.elements = new Block[n];
      //^ base();
    }
    public void Add(Block element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Block[] newElements = new Block[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public BlockList/*!*/ Clone() {
      Block[] elements = this.elements;
      int n = this.count;
      BlockList result = new BlockList(n);
      result.count = n;
      Block[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Block this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly BlockList/*!*/ list;
      public Enumerator(BlockList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Block Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class EventList{
    private Event[]/*!*/ elements;
    private int count = 0;
    public EventList(){
      this.elements = new Event[8];
      //^ base();
    }
    public EventList(int n){
      this.elements = new Event[n];
      //^ base();
    }
    public void Add(Event element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Event[] newElements = new Event[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Event this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly EventList/*!*/ list;
      public Enumerator(EventList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Event Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class ExpressionList{
    private Expression[]/*!*/ elements;
    private int count = 0;
    public ExpressionList(){
      this.elements = new Expression[8];
      //^ base();
    }
    public ExpressionList(int n){
      this.elements = new Expression[n];
      //^ base();
    }
    public ExpressionList(params Expression[] elements){
      if (elements == null) elements = new Expression[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Expression element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Expression[] newElements = new Expression[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public ExpressionList/*!*/ Clone(){
      Expression[] elements = this.elements;
      int n = this.count;
      ExpressionList result = new ExpressionList(n);
      result.count = n;
      Expression[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Expression this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ExpressionList/*!*/ list;
      public Enumerator(ExpressionList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Expression Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class ExceptionHandlerList{
    private ExceptionHandler[]/*!*/ elements = new ExceptionHandler[4];
    private int count = 0;
    public ExceptionHandlerList(){
      //^ base();
    }
    public ExceptionHandlerList(int n){
      this.elements = new ExceptionHandler[n];
      //^ base();
    }
    public void Add(ExceptionHandler element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        ExceptionHandler[] newElements = new ExceptionHandler[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public ExceptionHandlerList/*!*/ Clone() {
      ExceptionHandler[] elements = this.elements;
      int n = this.count;
      ExceptionHandlerList result = new ExceptionHandlerList(n);
      result.count = n;
      ExceptionHandler[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public ExceptionHandler this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ExceptionHandlerList/*!*/ list;
      public Enumerator(ExceptionHandlerList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public ExceptionHandler Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class FaultHandlerList{
    private FaultHandler[]/*!*/ elements;
    private int count = 0;
    public FaultHandlerList(){
      this.elements = new FaultHandler[4];
      //^ base();
    }
    public FaultHandlerList(int n){
      this.elements = new FaultHandler[n];
      //^ base();
    }
    public void Add(FaultHandler element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        FaultHandler[] newElements = new FaultHandler[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public FaultHandlerList/*!*/ Clone() {
      FaultHandler[] elements = this.elements;
      int n = this.count;
      FaultHandlerList result = new FaultHandlerList(n);
      result.count = n;
      FaultHandler[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public FaultHandler this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly FaultHandlerList/*!*/ list;
      public Enumerator(FaultHandlerList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public FaultHandler Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class FieldList{
    private Field[]/*!*/ elements; 
    private int count = 0;
    public FieldList(){
      this.elements = new Field[8];
      //^ base();
    }
    public FieldList(int capacity){
      this.elements = new Field[capacity];
      //^ base();
    }
    public FieldList(params Field[] elements){
      if (elements == null) elements = new Field[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Field element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Field[] newElements = new Field[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public FieldList/*!*/ Clone() {
      Field[] elements = this.elements;
      int n = this.count;
      FieldList result = new FieldList(n);
      result.count = n;
      Field[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Field this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly FieldList/*!*/ list;
      public Enumerator(FieldList /*!*/list) {
        this.index = -1;
        this.list = list;
      }
      public Field Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class FilterList{
    private Filter[]/*!*/ elements;
    private int count = 0;
    public FilterList(){
      this.elements = new Filter[4];
      //^ base();
    }
    public FilterList(int capacity){
      this.elements = new Filter[capacity];
      //^ base();
    }
    public void Add(Filter element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Filter[] newElements = new Filter[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public FilterList/*!*/ Clone() {
      Filter[] elements = this.elements;
      int n = this.count;
      FilterList result = new FilterList(n);
      result.count = n;
      Filter[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Filter this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly FilterList/*!*/ list;
      public Enumerator(FilterList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Filter Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class IdentifierList{
    private Identifier[]/*!*/ elements; 
    private int count = 0;
    public IdentifierList(){
      this.elements = new Identifier[8];
      //^ base();
    }
    public IdentifierList(int capacity){
      this.elements = new Identifier[capacity];
      //^ base();
    }
    public void Add(Identifier element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Identifier[] newElements = new Identifier[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Identifier this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly IdentifierList/*!*/ list;
      public Enumerator(IdentifierList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Identifier Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class InstructionList{
    private Instruction[]/*!*/ elements; 
    private int count = 0;
    public InstructionList(){
      this.elements = new Instruction[32];
      //^ base();
    }
    public InstructionList(int capacity){
      this.elements = new Instruction[capacity];
      //^ base();
    }
    public void Add(Instruction element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 32) m = 32;
        Instruction[] newElements = new Instruction[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Instruction this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly InstructionList/*!*/ list;
      public Enumerator(InstructionList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Instruction Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class InterfaceList{
    private Interface[]/*!*/ elements;
    private int count = 0;
    private AttributeList[] attributes;
    public InterfaceList(){
      this.elements = new Interface[8];
      //^ base();
    }
    public InterfaceList(int capacity){
      this.elements = new Interface[capacity];
      //^ base();
    }
    public InterfaceList(params Interface[] elements){
      if (elements == null) elements = new Interface[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Interface element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Interface[] newElements = new Interface[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
        if (this.attributes != null) {
          AttributeList[] newAttributes = new AttributeList[m];
          for (int j = 0; j < n; j++) newAttributes[j] = this.attributes[j];
          this.attributes = newAttributes;
        }
      }
      this.elements[i] = element;
    }
    public void AddAttributes(int index, AttributeList attributes) {
      if (this.attributes == null) this.attributes = new AttributeList[this.elements.Length];
      this.attributes[index] = attributes;
    }
    public AttributeList/*?*/ AttributesFor(int index) {
      if (this.attributes == null) return null;
      return this.attributes[index];
    }
    public InterfaceList/*!*/ Clone() {
      Interface[] elements = this.elements;
      int n = this.count;
      InterfaceList result = new InterfaceList(n);
      result.count = n;
      Interface[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public int SearchFor(Interface element){
      Interface[] elements = this.elements;
      for (int i = 0, n = this.count; i < n; i++)
        if ((object)elements[i] == (object)element) return i;
      return -1;
    }
    public Interface this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly InterfaceList/*!*/ list;
      public Enumerator(InterfaceList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Interface Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class InvariantList{
    private Invariant[]/*!*/ elements;
    private int count = 0;
    public InvariantList(){
      this.elements = new Invariant[8];
      //^ base();
    }
    public InvariantList(int n){
      this.elements = new Invariant[n];
      //^ base();
    }
    public InvariantList(params Invariant[] elements){
      if (elements == null) elements = new Invariant[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Invariant element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Invariant[] newElements = new Invariant[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public InvariantList/*!*/ Clone() {
      Invariant[] elements = this.elements;
      int n = this.count;
      InvariantList result = new InvariantList(n);
      result.count = n;
      Invariant[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Invariant this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly InvariantList/*!*/ list;
      public Enumerator(InvariantList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Invariant Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class Int32List{
    private Int32[]/*!*/ elements;
    private int count = 0;
    public Int32List(){
      this.elements = new Int32[8];
      //^ base();
    }
    public Int32List(int capacity){
      this.elements = new Int32[capacity];
      //^ base();
    }
    public Int32List(params Int32[] elements){
      if (elements == null) elements = new Int32[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Int32 element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Int32[] newElements = new Int32[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Int32 this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly Int32List/*!*/ list;
      public Enumerator(Int32List/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Int32 Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class RequiresList{
    private Requires[]/*!*/ elements;
    private int count = 0;
    public RequiresList(){
      this.elements = new Requires[2];
      //^ base();
    }
    public RequiresList(int capacity){
      this.elements = new Requires[capacity];
      //^ base();
    }
    public void Add(Requires element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Requires[] newElements = new Requires[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public RequiresList/*!*/ Clone() {
      Requires[] elements = this.elements;
      int n = this.count;
      RequiresList result = new RequiresList(n);
      result.count = n;
      Requires[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Requires this[int index]{
      get{
        Contract.Requires(index >= 0);
        Contract.Requires(index < Count);

        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly RequiresList/*!*/ list;
      public Enumerator(RequiresList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Requires Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class EnsuresList{
    private Ensures[]/*!*/ elements;
    private int count = 0;
    public EnsuresList(){
      this.elements = new Ensures[2];
      //^ base();
    }
    public EnsuresList(int capacity){
      this.elements = new Ensures[capacity];
      //^ base();
    }
    public void Add(Ensures element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Ensures[] newElements = new Ensures[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public EnsuresList/*!*/ Clone() {
      Ensures[] elements = this.elements;
      int n = this.count;
      EnsuresList result = new EnsuresList(n);
      result.count = n;
      Ensures[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Ensures this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly EnsuresList/*!*/ list;
      public Enumerator(EnsuresList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Ensures Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class MethodContractElementList
  {
    private MethodContractElement[]/*!*/ elements;
    private int count = 0;
    public MethodContractElementList()
    {
      this.elements = new MethodContractElement[2];
      //^ base();
    }
    public MethodContractElementList(int capacity)
    {
      this.elements = new MethodContractElement[capacity];
      //^ base();
    }
    public void Add(MethodContractElement element)
    {
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n)
      {
        int m = n * 2; if (m < 8) m = 8;
        MethodContractElement[] newElements = new MethodContractElement[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public MethodContractElementList/*!*/ Clone()
    {
      var elements = this.elements;
      int n = this.count;
      var result = new MethodContractElementList(n);
      result.count = n;
      var newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count
    {
      get { return this.count; }
    }
    public MethodContractElement this[int index]
    {
      get
      {
        return this.elements[index];
      }
      set
      {
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator()
    {
      return new Enumerator(this);
    }
    public struct Enumerator
    {
      private int index;
      private readonly MethodContractElementList/*!*/ list;
      public Enumerator(MethodContractElementList/*!*/ list)
      {
        this.index = -1;
        this.list = list;
      }
      public MethodContractElement Current
      {
        get
        {
          return this.list[this.index];
        }
      }
      public bool MoveNext()
      {
        return ++this.index < this.list.count;
      }
      public void Reset()
      {
        this.index = -1;
      }
    }
  }
  public sealed class LocalList{
    private Local[]/*!*/ elements; 
    private int count = 0;
    public LocalList(){
      this.elements = new Local[8];
      //^ base();
    }
    public LocalList(int capacity){
      this.elements = new Local[capacity];
      //^ base();
    }
    public void Add(Local element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Local[] newElements = new Local[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Local this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public struct Enumerator{
      private int index;
      private readonly LocalList/*!*/ list;
      public Enumerator(LocalList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Local Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class MemberList{
    private Member[]/*!*/ elements;
    private int count = 0;
    public MemberList(){
      this.elements = new Member[16];
      //^ base();
    }
    public MemberList(int capacity){
      this.elements = new Member[capacity];
      //^ base();
    }
    public MemberList(params Member[] elements){
      if (elements == null) elements = new Member[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Member element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 16) m = 16;
        Member[] newElements = new Member[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public bool Contains(Member element) {
      int n = this.count;
      for (int i = 0; i < n; i++)
        if (elements[i] == element)
          return true;
      return false;
    }
    public void AddList(MemberList memberList) {
      if (memberList == null || memberList.Count == 0) return;
      int n = this.elements.Length;
      int newN = this.count + memberList.count;
      if (newN > n) {
        int m = newN; if (m < 16) m = 16;
        Member[] newElements = new Member[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      for (int i = this.count, j = 0; i < newN; ++i, ++j) {
        this.elements[i] = memberList.elements[j];
      }
      this.count = newN;
    }
    /// <summary>
    /// Removes member (by nulling slot) if present
    /// </summary>
    public void Remove(Member member) {
      int n = this.count;
      for (int i=0; i<n; i++) {
        if (this.elements[i] == member) {
          this.elements[i] = null;
          return;
        }
      }
    }
    public void RemoveAt(int index) {
      if (index >= this.count || index < 0) return;
      int n = this.count;
      for (int i = index+1; i < n; ++i)
        this.elements[i-1] = this.elements[i];
      this.count--;
    }
    public MemberList/*!*/ Clone() {
      Member[] elements = this.elements;
      int n = this.count;
      MemberList result = new MemberList(n);
      result.count = n;
      Member[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Member this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly MemberList/*!*/ list;
      public Enumerator(MemberList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Member Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
    public Member[]/*!*/ ToArray() {
      Member[] m = new Member[this.count];
      Array.Copy(this.elements, m, this.count);
      return m;
    }
  }
  public sealed class MemberBindingList{
    private MemberBinding[]/*!*/ elements; 
    private int count = 0;
    public MemberBindingList(){
      this.elements = new MemberBinding[8];
      //^ base();
    }
    public MemberBindingList(int capacity){
      this.elements = new MemberBinding[capacity];
      //^ base();
    }
    public void Add(MemberBinding element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        MemberBinding[] newElements = new MemberBinding[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public MemberBinding this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly MemberBindingList/*!*/ list;
      public Enumerator(MemberBindingList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public MemberBinding Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class MethodList{
    private Method[]/*!*/ elements; 
    private int count = 0;
    public MethodList(){
      this.elements = new Method[8];
      //^ base();
    }
    public MethodList(int capacity){
      this.elements = new Method[capacity];
      //^ base();
    }
    public MethodList(params Method[] elements){
      if (elements == null) elements = new Method[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Method element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Method[] newElements = new Method[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public MethodList/*!*/ Clone() {
      Method[] elements = this.elements;
      int n = this.count;
      MethodList result = new MethodList(n);
      result.count = n;
      Method[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Method this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly MethodList/*!*/ list;
      public Enumerator(MethodList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Method Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }

  public sealed class ModuleList{
    private Module[]/*!*/ elements;
    private int count = 0;
    public ModuleList(){
      this.elements = new Module[4];
      //^ base();
    }
    public ModuleList(int capacity){
      this.elements = new Module[capacity];
      //^ base();
    }
    public void Add(Module element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Module[] newElements = new Module[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Module this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ModuleList/*!*/ list;
      public Enumerator(ModuleList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Module Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class ModuleReferenceList{
    private ModuleReference[]/*!*/ elements;
    private int count = 0;
    public ModuleReferenceList(){
      this.elements = new ModuleReference[4];
      //^ base();
    }
    public ModuleReferenceList(int capacity){
      this.elements = new ModuleReference[capacity];
      //^ base();
    }
    public ModuleReferenceList(params ModuleReference[] elements){
      if (elements == null) elements = new ModuleReference[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(ModuleReference element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        ModuleReference[] newElements = new ModuleReference[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public ModuleReferenceList/*!*/ Clone() {
      ModuleReference[] elements = this.elements;
      int n = this.count;
      ModuleReferenceList result = new ModuleReferenceList(n);
      result.count = n;
      ModuleReference[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public ModuleReference this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ModuleReferenceList/*!*/ list;
      public Enumerator(ModuleReferenceList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public ModuleReference Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class NamespaceList{
    private Namespace[]/*!*/ elements;
    private int count = 0;
    public NamespaceList(){
      this.elements = new Namespace[4];
      //^ base();
    }
    public NamespaceList(int capacity){
      this.elements = new Namespace[capacity];
      //^ base();
    }
    public void Add(Namespace element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Namespace[] newElements = new Namespace[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public NamespaceList/*!*/ Clone() {
      Namespace[] elements = this.elements;
      int n = this.count;
      NamespaceList result = new NamespaceList(n);
      result.count = n;
      Namespace[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Namespace this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly NamespaceList/*!*/ list;
      public Enumerator(NamespaceList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Namespace Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class NodeList{
    private Node[]/*!*/ elements;
    private int count = 0;
    public NodeList(){
      this.elements = new Node[4];
      //^ base();
    }
    public NodeList(int capacity){
      this.elements = new Node[capacity];
      //^ base();
    }
    public NodeList(params Node[] elements){
      if (elements == null) elements = new Node[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Node element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Node[] newElements = new Node[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public NodeList/*!*/ Clone() {
      Node[] elements = this.elements;
      int n = this.count;
      NodeList result = new NodeList(n);
      result.count = n;
      Node[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Node this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly NodeList/*!*/ list;
      public Enumerator(NodeList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Node Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class ParameterList{
    public readonly static ParameterList/*!*/ Empty = new ParameterList(0);

    private Parameter[]/*!*/ elements; 
    private int count = 0;
    public ParameterList(){
      this.elements = new Parameter[8];
      //^ base();
    }
    public ParameterList(int capacity){
      this.elements = new Parameter[capacity];
      //^ base();
    }
    public ParameterList(params Parameter[] elements){
      if (elements == null) elements = new Parameter[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Parameter element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Parameter[] newElements = new Parameter[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public ParameterList/*!*/ Clone() {
      Parameter[] elements = this.elements;
      int n = this.count;
      ParameterList result = new ParameterList(n);
      result.count = n;
      Parameter[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Parameter this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ParameterList/*!*/ list;
      public Enumerator(ParameterList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Parameter Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
    public override string ToString() {
      string res = "";
      for (int i = 0; i < this.count; i++) {
        if (i > 0) res += ",";
        Parameter par = elements[i];
        if (par == null) continue;
        res += par.ToString();
      }
      return res;
    }
  }

  public sealed class PropertyList{
    private Property[]/*!*/ elements; 
    private int count = 0;
    public PropertyList(){
      this.elements = new Property[8];
      //^ base();
    }
    public PropertyList(int capacity){
      this.elements = new Property[capacity];
      //^ base();
    }
    public void Add(Property element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        Property[] newElements = new Property[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Property this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly PropertyList/*!*/ list;
      public Enumerator(PropertyList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Property Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class ResourceList{
    private Resource[]/*!*/ elements;
    private int count = 0;
    public ResourceList(){
      this.elements = new Resource[4];
      //^ base();
    }
    public ResourceList(int capacity){
      this.elements = new Resource[capacity];
      //^ base();
    }
    public void Add(Resource element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Resource[] newElements = new Resource[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public ResourceList/*!*/ Clone() {
      Resource[] elements = this.elements;
      int n = this.count;
      ResourceList result = new ResourceList(n);
      result.count = n;
      Resource[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Resource this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly ResourceList/*!*/ list;
      public Enumerator(ResourceList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Resource Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class SecurityAttributeList{
    private SecurityAttribute[]/*!*/ elements;
    private int count = 0;
    public SecurityAttributeList(){
      this.elements = new SecurityAttribute[8];
      //^ base();
    }
    public SecurityAttributeList(int capacity){
      this.elements = new SecurityAttribute[capacity];
      //^ base();
    }
    public void Add(SecurityAttribute element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 8) m = 8;
        SecurityAttribute[] newElements = new SecurityAttribute[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public SecurityAttributeList/*!*/ Clone() {
      SecurityAttribute[] elements = this.elements;
      int n = this.count;
      SecurityAttributeList result = new SecurityAttributeList(n);
      result.count = n;
      SecurityAttribute[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public SecurityAttribute this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly SecurityAttributeList/*!*/ list;
      public Enumerator(SecurityAttributeList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public SecurityAttribute Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class SourceChangeList{
    private SourceChange[]/*!*/ elements;
    private int count = 0;
    public SourceChangeList(){
      this.elements = new SourceChange[4];
      //^ base();
    }
    public SourceChangeList(int capacity){
      this.elements = new SourceChange[capacity];
      //^ base();
    }
    public SourceChangeList(params SourceChange[] elements){
      if (elements == null) elements = new SourceChange[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(SourceChange element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        SourceChange[] newElements = new SourceChange[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public SourceChangeList/*!*/ Clone() {
      SourceChange[] elements = this.elements;
      int n = this.count;
      SourceChangeList result = new SourceChangeList(n);
      result.count = n;
      SourceChange[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public SourceChange this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly SourceChangeList/*!*/ list;
      public Enumerator(SourceChangeList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public SourceChange Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class StatementList{
    private Statement[]/*!*/ elements;
    private int count = 0;
    public StatementList(){
      this.elements = new Statement[4];
      //^ base();
    }
    public StatementList(int capacity){
      this.elements = new Statement[capacity];
      //^ base();
    }
    public StatementList(params Statement[] elements){
      if (elements == null) elements = new Statement[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Statement statement){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 32) m = 32;
        Statement[] newElements = new Statement[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = statement;
    }
    public StatementList/*!*/ Clone() {
      Statement[] elements = this.elements;
      int n = this.count;
      StatementList result = new StatementList(n);
      result.count = n;
      Statement[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{
        Contract.Ensures(Contract.Result<int>() >= 0);

        return this.count;
      }
      set{this.count = value;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
      set{this.count = value;}
    }
    public Statement this[int index]{
      get{
        System.Diagnostics.Contracts.Contract.Requires(index >= 0);
        System.Diagnostics.Contracts.Contract.Requires(index < Count);

        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly StatementList/*!*/ list;
      public Enumerator(StatementList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public Statement Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class StringList{
    private string[]/*!*/ elements = new string[4];
    private int count = 0;
    public StringList(){
      this.elements = new string[4];
      //^ base();
    }
    public StringList(int capacity){
      this.elements = new string[capacity];
      //^ base();
    }
    public StringList(params string[] elements){
      if (elements == null) elements = new string[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public StringList(System.Collections.Specialized.StringCollection/*!*/ stringCollection){
      int n = this.count = stringCollection == null ? 0 : stringCollection.Count;
      string[] elements = this.elements = new string[n];
      //^ base();
      if (n > 0) stringCollection.CopyTo(elements, 0);
    }
    public void Add(string element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        String[] newElements = new String[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public string this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly StringList/*!*/ list;
      public Enumerator(StringList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public String Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class TypeNodeList{
    private TypeNode[]/*!*/ elements;
    private int count = 0;
    public TypeNodeList(){
      this.elements = new TypeNode[32];
      //^ base();
    }
    public TypeNodeList(int capacity){
      this.elements = new TypeNode[capacity];
      //^ base();
    }
    public TypeNodeList(params TypeNode[] elements){
      if (elements == null) elements = new TypeNode[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(TypeNode element){
      TypeNode[] elements = this.elements;
      int n = elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 32) m = 32;
        TypeNode[] newElements = new TypeNode[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public TypeNodeList/*!*/ Clone(){
      TypeNode[] elements = this.elements;
      int n = this.count;
      TypeNodeList result = new TypeNodeList(n);
      result.count = n;
      TypeNode[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public void Insert(TypeNode element, int index){
      TypeNode[] elements = this.elements;
      int n = this.elements.Length;
      int i = this.count++;
      if (index >= i) throw new IndexOutOfRangeException();
      if (i == n){
        int m = n*2; if (m < 32) m = 32;
        TypeNode[] newElements = new TypeNode[m];
        for (int j = 0; j < index; j++) newElements[j] = elements[j];
        newElements[index] = element;
        for (int j = index; j < n; j++) newElements[j+1] = elements[j];
        return;
      }
      for (int j = index; j < i; j++){
        TypeNode t = elements[j];
        elements[j] = element;
        element = t;
      }
      elements[i] = element;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public int SearchFor(TypeNode element){
      TypeNode[] elements = this.elements;
      for (int i = 0, n = this.count; i < n; i++)
        if ((object)elements[i] == (object)element) return i;
      return -1;
    }
    public TypeNode this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly TypeNodeList/*!*/ list;
      public Enumerator(TypeNodeList/*!*/ list) {
        this.index = -1;
        this.list = list;
      }
      public TypeNode Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }

    internal bool Contains(TypeNode asType)
    {
      return SearchFor(asType) >= 0;
    }
  }
  public sealed class Win32ResourceList{
    private Win32Resource[]/*!*/ elements;
    private int count = 0;
    public Win32ResourceList(){
      this.elements = new Win32Resource[4];
      //^ base();
    }
    public Win32ResourceList(int capacity){
      this.elements = new Win32Resource[capacity];
      //^ base();
    }
    public void Add(Win32Resource element){
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n){
        int m = n*2; if (m < 4) m = 4;
        Win32Resource[] newElements = new Win32Resource[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public Win32ResourceList/*!*/ Clone(){
      Win32Resource[] elements = this.elements;
      int n = this.count;
      Win32ResourceList result = new Win32ResourceList(n);
      result.count = n;
      Win32Resource[] newElements = result.elements;
      for (int i = 0; i < n; i++)
        newElements[i] = elements[i];
      return result;
    }
    public int Count{
      get{return this.count;}
    }
    [Obsolete("Use Count property instead.")]
    public int Length{
      get{return this.count;}
    }
    public Win32Resource this[int index]{
      get{
        return this.elements[index];
      }
      set{
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator(){
      return new Enumerator(this);
    }
    public struct Enumerator{
      private int index;
      private readonly Win32ResourceList/*!*/ list;
      public Enumerator(Win32ResourceList/*!*/ list){
        this.index = -1;
        this.list = list;
      }
      public Win32Resource Current{
        get{
          return this.list[this.index];
        }
      }
      public bool MoveNext(){
        return ++this.index < this.list.count;
      }
      public void Reset(){
        this.index = -1;
      }
    }
  }
  public sealed class SerializedTypeNameList
  {
    private Metadata.SerializedTypeName[]/*!*/ elements = new Metadata.SerializedTypeName[4];
    private int count = 0;
    public SerializedTypeNameList()
    {
      this.elements = new Metadata.SerializedTypeName[4];
      //^ base();
    }
    public SerializedTypeNameList(int capacity)
    {
      this.elements = new Metadata.SerializedTypeName[capacity];
      //^ base();
    }
    public SerializedTypeNameList(params Metadata.SerializedTypeName[] elements)
    {
      if (elements == null) elements = new Metadata.SerializedTypeName[0];
      this.elements = elements;
      this.count = elements.Length;
      //^ base();
    }
    public void Add(Metadata.SerializedTypeName element)
    {
      int n = this.elements.Length;
      int i = this.count++;
      if (i == n)
      {
        int m = n * 2; if (m < 4) m = 4;
        Metadata.SerializedTypeName[] newElements = new Metadata.SerializedTypeName[m];
        for (int j = 0; j < n; j++) newElements[j] = elements[j];
        this.elements = newElements;
      }
      this.elements[i] = element;
    }
    public int Count
    {
      get { return this.count; }
    }
    [Obsolete("Use Count property instead.")]
    public int Length
    {
      get { return this.count; }
    }
    public Metadata.SerializedTypeName this[int index]
    {
      get
      {
        return this.elements[index];
      }
      set
      {
        this.elements[index] = value;
      }
    }
    public Enumerator GetEnumerator()
    {
      return new Enumerator(this);
    }
    public struct Enumerator
    {
      private int index;
      private readonly SerializedTypeNameList/*!*/ list;
      public Enumerator(SerializedTypeNameList/*!*/ list)
      {
        this.index = -1;
        this.list = list;
      }
      public Metadata.SerializedTypeName Current
      {
        get
        {
          return this.list[this.index];
        }
      }
      public bool MoveNext()
      {
        return ++this.index < this.list.count;
      }
      public void Reset()
      {
        this.index = -1;
      }
    }
  }
}
