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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Microsoft.Research.DataStructures
{
  public interface IVerySimpleLineWriter
  {
    // The attribute is needed for services inheriting this interface
    // The attribute means that the service calling WriteLine does not wait for WriteLine to complete
    [OperationContract(IsOneWay = true)] 
    void WriteLine(string value);
  }

  internal static class VerySimpleLineWriter
  {
    internal abstract class BaseToTextWriter : TextWriter
    {
      protected abstract StringBuilder Buffer { get; }
      protected abstract IWithEncoding AsWithEncoding { get; }
      protected abstract void InternalFlush();

      #region TextWriter base cases
      public override void Write(char value)
      {
        this.Buffer.Append(value);
      }
      public override void Write(char[] value, int index, int count)
      {
        this.Buffer.Append(value, index, count);
      }
      public override void Write(string value)
      {
        this.Buffer.Append(value);
      }
      #endregion
      protected string GetAndEmptyBufferContent()
      {
        var res = this.Buffer.ToString();
        this.Buffer.Clear();
        return res;
      }
      public override void WriteLine(string value)
      {
        // TextWriter implementation calls Write(char[], int, int) instead of Write + WriteLine
        this.Write(value);
        this.WriteLine();
      }
      public override void Flush()
      {
        if (this.Buffer.Length > 0)
          this.InternalFlush();
      }
      public override void Close()
      {
        this.Flush();
      }
      public override Encoding Encoding
      {
        get
        {
          var withEncoding = this.AsWithEncoding;
          if (withEncoding != null)
            return withEncoding.Encoding;
          return Encoding.Default;
        }
      }
    }

    internal abstract class NormalBaseToTextWriter : BaseToTextWriter
    {
      protected abstract void WriteStringLine(string value);
      private readonly StringBuilder buffer = new StringBuilder();
      protected override StringBuilder Buffer { get { return this.buffer; } }
      public override void WriteLine()
      {
        this.WriteStringLine(this.GetAndEmptyBufferContent());
      }
      protected override void InternalFlush()
      {
        this.WriteLine(); // this is adding an extra NewLine
      }
    }

    internal abstract class MultithreadedBaseToTextWriter : BaseToTextWriter
    {
      private readonly ThreadLocal<StringBuilder> buffer = new ThreadLocal<StringBuilder>(() => new StringBuilder());
      protected override StringBuilder Buffer { get { return this.buffer.Value; } }
    }

    internal class ToTextWriter : NormalBaseToTextWriter, IFromVerySimpleLineWriter
    {
      protected readonly IVerySimpleLineWriter lineWriter;

      public ToTextWriter(IVerySimpleLineWriter lineWriter)
      {
        this.lineWriter = lineWriter;
      }

      protected override void WriteStringLine(string value)
      {
        this.lineWriter.WriteLine(value);
      }

      protected override IWithEncoding AsWithEncoding
      {
        get { return this.lineWriter as IWithEncoding; }
      }

      IVerySimpleLineWriter IFromVerySimpleLineWriter.OriginalLineWriter { get { return this.lineWriter; } }
    }

    internal class ToTextWriterWithEncoding : ToTextWriter
    {
      protected readonly Encoding encoding;

      public ToTextWriterWithEncoding(IVerySimpleLineWriter lineWriter, string encodingWebName)
        : base(lineWriter)
      {
        this.encoding = Encoding.GetEncoding(encodingWebName);
      }

      public override Encoding Encoding
      {
        get { return this.encoding; }
      }
    }

    internal class FromTextWriter : IVerySimpleLineWriterWithEncoding, IFromTextWriter
    {
      protected readonly TextWriter textWriter;

      public FromTextWriter(TextWriter textWriter)
      {
        Contract.Requires(textWriter != null);

        this.textWriter = textWriter;
      }

      virtual public void WriteLine(string value)
      {
        this.textWriter.WriteLine(value);
      }

      public Encoding Encoding { get { return this.textWriter.Encoding; } }

      public static implicit operator FromTextWriter(TextWriter textWriter)
      {
        return new FromTextWriter(textWriter);
      }

      TextWriter IFromTextWriter.OriginalTextWriter { get { return this.textWriter; } }

      virtual public void Dispose()
      {
        // Do nothing
      }
    }

    public class FromTextWriterWithBuffer : FromTextWriter, IDisposable
    {
      private readonly NonBlockingTextWriter tw;
      public FromTextWriterWithBuffer(TextWriter inner)
        : base(inner)
      {
        this.tw = new NonBlockingTextWriter(inner);
      }

      public override void WriteLine(string value)
      {
        this.tw.WriteLine(value);
      }

      public override void Dispose()
      {
      }
    }

    // TODO: add the opportune interface it implements
    public class NonBlockingTextWriter
    {
      private readonly BlockingCollection<string> toWrite = new BlockingCollection<string>();
      private readonly TextWriter Where;

      public NonBlockingTextWriter(TextWriter where)
      {
        this.Where = where;

        var thread = new Thread(
          () =>
          {
            while (true)
            {
              where.WriteLine(toWrite.Take());
            }
          });
        thread.IsBackground = true;
        thread.Start();
      }

      public void WriteLine(string value)
      {
        toWrite.Add(value);
      }
    }
  }

  [ContractClass(typeof(ISimpleLineWriterContracts))]
  public interface ISimpleLineWriter
  {
    void WriteLine(string format, params object[] args);
  }

  [ContractClassFor(typeof(ISimpleLineWriter))]
  abstract class ISimpleLineWriterContracts : ISimpleLineWriter
  {
    public void WriteLine(string format, params object[] args)
    {
      Contract.Requires(format != null);
      Contract.Requires(args != null);

      throw new NotImplementedException();
    }
  }

  public static partial class SimpleLineWriter
  {
    internal class ToTextWriter : VerySimpleLineWriter.NormalBaseToTextWriter, IFromSimpleLineWriter
    {
      protected readonly ISimpleLineWriter lineWriter;

      public ToTextWriter(ISimpleLineWriter lineWriter)
      {
        this.lineWriter = lineWriter;
      }

      protected override void WriteStringLine(string value)
      {
        this.lineWriter.WriteLine("{0}", value);
      }

      protected override IWithEncoding AsWithEncoding
      {
        get { return this.lineWriter as IWithEncoding; }
      }

      ISimpleLineWriter IFromSimpleLineWriter.OriginalLineWriter { get { return this.lineWriter; } }
    }

    internal class ToMultithreadedTextWriter : VerySimpleLineWriter.MultithreadedBaseToTextWriter
    {
      protected readonly ISimpleLineWriter lineWriter;

      public ToMultithreadedTextWriter(ISimpleLineWriter lineWriter)
      {
        this.lineWriter = lineWriter;
      }
      protected override void InternalFlush()
      {
        this.lineWriter.WriteLine(this.GetAndEmptyBufferContent());
      }
      protected override IWithEncoding AsWithEncoding
      {
        get { return this.lineWriter as IWithEncoding; }
      }
    }

    public class FromTextWriter : ISimpleLineWriterWithEncoding, IFromTextWriter
    {
      protected readonly TextWriter textWriter;

      public FromTextWriter(TextWriter textWriter)
      {
        Contract.Requires(textWriter != null);

        this.textWriter = textWriter;
      }

      public virtual void WriteLine(string value, params object[] arg)
      {
        this.textWriter.WriteLine(value, arg);
      }

      public Encoding Encoding { get { return this.textWriter.Encoding; } }

      public static implicit operator FromTextWriter(TextWriter textWriter)
      {
        return new FromTextWriter(textWriter);
      }

      TextWriter IFromTextWriter.OriginalTextWriter { get { return this.textWriter; } }
    }
  }

  // In case of double conversions, these interface allow unboxing instead of double boxing

  internal interface IFromTextWriter
  {
    TextWriter OriginalTextWriter { get; }
  }

  internal interface IFromVerySimpleLineWriter
  {
    IVerySimpleLineWriter OriginalLineWriter { get; }
  }

  internal interface IFromSimpleLineWriter
  {
    ISimpleLineWriter OriginalLineWriter { get; }
  }

  public interface IWithEncoding
  {
    Encoding Encoding { get; }
  }

  public interface IVerySimpleLineWriterWithEncoding : IVerySimpleLineWriter, IWithEncoding, IDisposable { }
  public interface ISimpleLineWriterWithEncoding : ISimpleLineWriter, IWithEncoding { }

  public static class VerySimpleLineWriterExtensions
  {
    public static void WriteLine(this IVerySimpleLineWriter writer)
    {
      writer.WriteLine((string)null);
    }

    public static void WriteLine(this IVerySimpleLineWriter writer, string format, params object[] args)
    {
      writer.WriteLine(String.Format(format, args));
    }

    public static TextWriter AsTextWriter(this IVerySimpleLineWriter writer)
    {
      var writerAsFromTextWriter = writer as IFromTextWriter;
      if (writerAsFromTextWriter != null)
        return writerAsFromTextWriter.OriginalTextWriter;
      return new VerySimpleLineWriter.ToTextWriter(writer);
    }

    public static TextWriter AsTextWriter(this IVerySimpleLineWriter writer, string encodingWebName)
    {
      var writerAsFromTextWriter = writer as IFromTextWriter;
      if (writerAsFromTextWriter != null)
      {
        if (writerAsFromTextWriter.OriginalTextWriter.Encoding.WebName != encodingWebName)
          throw new ArgumentException("Encoding name different from original TextWriter encoding");
        return writerAsFromTextWriter.OriginalTextWriter;
      }
      return new VerySimpleLineWriter.ToTextWriterWithEncoding(writer, encodingWebName);
    }
  }

  public static class SimpleLineWriterExtensions
  {
    public static void WriteLine(this ISimpleLineWriter writer)
    {
      writer.WriteLine("");
    }

    public static void WriteLine(this ISimpleLineWriter writer, string value)
    {
      writer.WriteLine("{0}", value);
    }

    public static void WriteLine(this ISimpleLineWriter writer, object value)
    {
      writer.WriteLine("{0}", value);
    }

    public static TextWriter AsTextWriter(this ISimpleLineWriter writer)
    {
      var writerAsFromTextWriter = writer as IFromTextWriter;
      if (writerAsFromTextWriter != null)
        return writerAsFromTextWriter.OriginalTextWriter;
      return new SimpleLineWriter.ToTextWriter(writer);
    }

    public static TextWriter AsMultithreadedTextWriter(this ISimpleLineWriter writer)
    {
      return new SimpleLineWriter.ToMultithreadedTextWriter(writer);
    }
  }

  public static class TextWriterExtensions
  {
    public static IVerySimpleLineWriterWithEncoding AsLineWriter(this TextWriter textWriter)
    {
      var asFromVerySimpleLineWriter = textWriter as IFromVerySimpleLineWriter;
      if (asFromVerySimpleLineWriter != null)
      {
        var asWithEncoding = asFromVerySimpleLineWriter.OriginalLineWriter as IVerySimpleLineWriterWithEncoding;
        if (asWithEncoding != null)
          return asWithEncoding;
      }
      // Original from mehdi:
      //     return new VerySimpleLineWriter.FromTextWriter(textWriter);

      return new VerySimpleLineWriter.FromTextWriterWithBuffer(textWriter);
    }

    public static ISimpleLineWriterWithEncoding AsSimpleLineWriter(this TextWriter textWriter)
    {
      var asFromSimpleLineWriter = textWriter as IFromSimpleLineWriter;
      if (asFromSimpleLineWriter != null)
      {
        var asWithEncoding = asFromSimpleLineWriter.OriginalLineWriter as ISimpleLineWriterWithEncoding;
        if (asWithEncoding != null)
          return asWithEncoding;
      }
      return new SimpleLineWriter.FromTextWriter(textWriter);
    }
  }

  public static class BufferedTextWriter
  {
    public static TextWriter Create()
    {
      Contract.Ensures(Contract.Result<TextWriter>() != null);
      return new InternalBufferedTextWriter();
    }
    internal class InternalBufferedTextWriter : VerySimpleLineWriter.BaseToTextWriter
    {
      private readonly StringBuilder buffer = new StringBuilder();
      protected override StringBuilder Buffer { get { return this.buffer; } }
      protected override IWithEncoding AsWithEncoding { get { return null; } }
      public override string ToString()
      {
        return this.GetAndEmptyBufferContent();
      }
      protected override void InternalFlush() { }
    }
  }

}
