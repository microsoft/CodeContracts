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


#if !SILVERLIGHT

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Diagnostics.Contracts;


namespace System.Runtime.Serialization.Formatters.Binary
{
  // Summary:
  //     Serializes and deserializes an object, or an entire graph of connected objects,
  //     in binary format.
  //[ComVisible(true)]
  public class BinaryFormatter // : IRemotingFormatter, IFormatter
  {
    // Summary:
    //     Initializes a new instance of the System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    //     class with default values.
    //public BinaryFormatter();
    //
    // Summary:
    //     Initializes a new instance of the System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    //     class with a given surrogate selector and streaming context.
    //
    // Parameters:
    //   selector:
    //     The System.Runtime.Serialization.ISurrogateSelector to use. Can be null.
    //
    //   context:
    //     The source and destination for the serialized data.
    //public BinaryFormatter(ISurrogateSelector selector, StreamingContext context);

    // Summary:
    //     Gets or sets the behavior of the deserializer with regards to finding and
    //     loading assemblies.
    //
    // Returns:
    //     One of the System.Runtime.Serialization.Formatters.FormatterAssemblyStyle
    //     values that specifies the deserializer behavior.
    //public FormatterAssemblyStyle AssemblyFormat { get; set; }
    //
    // Summary:
    //     Gets or sets an object of type System.Runtime.Serialization.SerializationBinder
    //     that controls the binding of a serialized object to a type.
    //
    // Returns:
    //     The serialization binder to use with this formatter.
    //public SerializationBinder Binder { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Runtime.Serialization.StreamingContext for this formatter.
    //
    // Returns:
    //     The streaming context to use with this formatter.
    //public StreamingContext Context { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Runtime.Serialization.Formatters.TypeFilterLevel
    //     of automatic deserialization the System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
    //     performs.
    //
    // Returns:
    //     The System.Runtime.Serialization.Formatters.TypeFilterLevel that represents
    //     the current automatic deserialization level.
    //  public TypeFilterLevel FilterLevel { get; set; }
    //
    // Summary:
    //     Gets or sets a System.Runtime.Serialization.ISurrogateSelector that controls
    //     type substitution during serialization and deserialization.
    //
    // Returns:
    //     The surrogate selector to use with this formatter.
    //public ISurrogateSelector SurrogateSelector { get; set; }
    //
    // Summary:
    //     Gets or sets the format in which type descriptions are laid out in the serialized
    //     stream.
    //
    // Returns:
    //     The style of type layouts to use.
    //public FormatterTypeStyle TypeFormat { get; set; }

    // Summary:
    //     Deserializes the specified stream into an object graph.
    //
    // Parameters:
    //   serializationStream:
    //     The stream from which to deserialize the object graph.
    //
    // Returns:
    //     The top (root) of the object graph.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The serializationStream supports seeking, but its length is 0.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    virtual public object Deserialize(Stream serializationStream)
    {
      Contract.Requires(serializationStream != null);
      Contract.Requires(!serializationStream.CanSeek || serializationStream.Length > 0);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Deserializes the specified stream into an object graph. The provided System.Runtime.Remoting.Messaging.HeaderHandler
    //     handles any headers in that stream.
    //
    // Parameters:
    //   serializationStream:
    //     The stream from which to deserialize the object graph.
    //
    //   handler:
    //     The System.Runtime.Remoting.Messaging.HeaderHandler that handles any headers
    //     in the serializationStream. Can be null.
    //
    // Returns:
    //     The deserialized object or the top object (root) of the object graph.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The serializationStream supports seeking, but its length is 0.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    virtual public object Deserialize(Stream serializationStream, HeaderHandler handler)
    {
      Contract.Requires(serializationStream != null);
      Contract.Requires(!serializationStream.CanSeek || serializationStream.Length > 0);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Deserializes a response to a remote method call from the provided System.IO.Stream.
    //
    // Parameters:
    //   serializationStream:
    //     The stream from which to deserialize the object graph.
    //
    //   handler:
    //     The System.Runtime.Remoting.Messaging.HeaderHandler that handles any headers
    //     in the serializationStream. Can be null.
    //
    //   methodCallMessage:
    //     The System.Runtime.Remoting.Messaging.IMethodCallMessage that contains details
    //     about where the call came from.
    //
    // Returns:
    //     The deserialized response to the remote method call.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The serializationStream supports seeking, but its length is 0.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      Contract.Requires(serializationStream != null);
      Contract.Requires(!serializationStream.CanSeek || serializationStream.Length > 0);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Serializes the object, or graph of objects with the specified top (root),
    //     to the given stream.
    //
    // Parameters:
    //   serializationStream:
    //     The stream to which the graph is to be serialized.
    //
    //   graph:
    //     The object at the root of the graph to serialize.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null. -or- The graph is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     An error has occurred during serialization, such as if an object in the graph
    //     parameter is not marked as serializable.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    virtual public void Serialize(Stream serializationStream, object graph)
    {
      Contract.Requires(serializationStream != null);
      Contract.Requires(graph != null);
    }
    //
    // Summary:
    //     Serializes the object, or graph of objects with the specified top (root),
    //     to the given stream attaching the provided headers.
    //
    // Parameters:
    //   serializationStream:
    //     The stream to which the object is to be serialized.
    //
    //   graph:
    //     The object at the root of the graph to serialize.
    //
    //   headers:
    //     Remoting headers to include in the serialization. Can be null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     An error has occurred during serialization, such as if an object in the graph
    //     parameter is not marked as serializable.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    virtual public void Serialize(Stream serializationStream, object graph, Header[] headers)
    {
      Contract.Requires(serializationStream != null);
    }
    //
    // Summary:
    //     Deserializes the specified stream into an object graph. The provided System.Runtime.Remoting.Messaging.HeaderHandler
    //     handles any headers in that stream.
    //
    // Parameters:
    //   serializationStream:
    //     The stream from which to deserialize the object graph.
    //
    //   handler:
    //     The System.Runtime.Remoting.Messaging.HeaderHandler that handles any headers
    //     in the serializationStream. Can be null.
    //
    // Returns:
    //     The deserialized object or the top object (root) of the object graph.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The serializationStream supports seeking, but its length is 0.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //[ComVisible(false)]
    public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
    {
      Contract.Requires(serializationStream != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Deserializes a response to a remote method call from the provided System.IO.Stream.
    //
    // Parameters:
    //   serializationStream:
    //     The stream from which to deserialize the object graph.
    //
    //   handler:
    //     The System.Runtime.Remoting.Messaging.HeaderHandler that handles any headers
    //     in the serializationStream. Can be null.
    //
    //   methodCallMessage:
    //     The System.Runtime.Remoting.Messaging.IMethodCallMessage that contains details
    //     about where the call came from.
    //
    // Returns:
    //     The deserialized response to the remote method call.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     The serializationStream is null.
    //
    //   System.Runtime.Serialization.SerializationException:
    //     The serializationStream supports seeking, but its length is 0.
    //
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    //[ComVisible(false)]
    public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      Contract.Requires(serializationStream != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
  }
}
#endif