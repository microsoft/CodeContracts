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
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;

namespace System.Net
{
  // Summary:
  //     Provides connection management for HTTP connections.
  public class ServicePoint
  {
    // Summary:
    //     Gets the Uniform Resource Identifier (URI) of the server that this System.Net.ServicePoint
    //     object connects to.
    //
    // Returns:
    //     An instance of the System.Uri class that contains the URI of the Internet
    //     server that this System.Net.ServicePoint object connects to.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     The System.Net.ServicePoint is in host mode.
    public Uri Address
    {
      get
      {
        Contract.Ensures(Contract.Result<Uri>() != null);
        return default(Uri);
      }
    }
    //
    // Summary:
    //     Specifies the delegate to associate a local System.Net.IPEndPoint with a
    //     System.Net.ServicePoint.
    //
    // Returns:
    //     A delegate that forces a System.Net.ServicePoint to use a particular local
    //     Internet Protocol (IP) address and port number. The default value is null.
#if false
    public BindIPEndPoint BindIPEndPointDelegate { get; set; }
#endif
    //
    // Summary:
    //     Gets the certificate received for this System.Net.ServicePoint object.
    //
    // Returns:
    //     An instance of the System.Security.Cryptography.X509Certificates.X509Certificate
    //     class that contains the security certificate received for this System.Net.ServicePoint
    //     object.
    extern public X509Certificate Certificate { get; }
    //
    // Summary:
    //     Gets the last client certificate sent to the server.
    //
    // Returns:
    //     An System.Security.Cryptography.X509Certificates.X509Certificate object that
    //     contains the public values of the last client certificate sent to the server.
    extern public X509Certificate ClientCertificate { get; }
    //
    // Summary:
    //     Gets or sets the number of milliseconds after which an active System.Net.ServicePoint
    //     connection is closed.
    //
    // Returns:
    //     A System.Int32 that specifies the number of milliseconds that an active System.Net.ServicePoint
    //     connection remains open. The default is -1, which allows an active System.Net.ServicePoint
    //     connection to stay connected indefinitely. Set this property to 0 to force
    //     System.Net.ServicePoint connections to close after servicing a request.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value specified for a set operation is a negative number less than -1.
    public int ConnectionLeaseTimeout
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= -1);
        return default(int);
      }
      set
      {
        Contract.Requires(value >= -1);
      }
    }
    //
    // Summary:
    //     Gets or sets the maximum number of connections allowed on this System.Net.ServicePoint
    //     object.
    //
    // Returns:
    //     The maximum number of connections allowed on this System.Net.ServicePoint
    //     object.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The connection limit is equal to or less than 0.
    public int ConnectionLimit
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() > 0);
        return default(int);
      }
      set
      {
        Contract.Requires(value > 0);
      }
    }
    //
    // Summary:
    //     Gets the connection name.
    //
    // Returns:
    //     A System.String that represents the connection name.
    extern public string ConnectionName { get; }
    //
    // Summary:
    //     Gets the number of open connections associated with this System.Net.ServicePoint
    //     object.
    //
    // Returns:
    //     The number of open connections associated with this System.Net.ServicePoint
    //     object.
    public int CurrentConnections
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
    //
    // Summary:
    //     Gets or sets a System.Boolean value that determines whether 100-Continue
    //     behavior is used.
    //
    // Returns:
    //     true to expect 100-Continue responses for POST requests; otherwise, false.
    //     The default value is true.
    extern public bool Expect100Continue { get; set; }
    //
    // Summary:
    //     Gets the date and time that the System.Net.ServicePoint object was last connected
    //     to a host.
    //
    // Returns:
    //     A System.DateTime object that contains the date and time at which the System.Net.ServicePoint
    //     object was last connected.
    extern public DateTime IdleSince { get; }
    //
    // Summary:
    //     Gets or sets the amount of time a connection associated with the System.Net.ServicePoint
    //     object can remain idle before the connection is closed.
    //
    // Returns:
    //     The length of time, in milliseconds, that a connection associated with the
    //     System.Net.ServicePoint object can remain idle before it is closed and reused
    //     for another connection.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     System.Net.ServicePoint.MaxIdleTime is set to less than System.Threading.Timeout.Infinite
    //     or greater than System.Int32.MaxValue.
    extern public int MaxIdleTime { get; set; }
    //
    // Summary:
    //     Gets the version of the HTTP protocol that the System.Net.ServicePoint object
    //     uses.
    //
    // Returns:
    //     A System.Version object that contains the HTTP protocol version that the
    //     System.Net.ServicePoint object uses.
    public virtual Version ProtocolVersion
    {
      get
      {
        Contract.Ensures(Contract.Result<Version>() != null);
        return default(Version);
      }
    }
    //
    // Summary:
    //     Gets or sets the size of the receiving buffer for the socket used by this
    //     System.Net.ServicePoint.
    //
    // Returns:
    //     A System.Int32 that contains the size, in bytes, of the receive buffer. The
    //     default is 8192.
    //
    // Exceptions:
    //   System.ArgumentOutOfRangeException:
    //     The value specified for a set operation is greater than System.Int32.MaxValue.
    extern public int ReceiveBufferSize { get; set; }
    //
    // Summary:
    //     Indicates whether the System.Net.ServicePoint object supports pipelined connections.
    //
    // Returns:
    //     true if the System.Net.ServicePoint object supports pipelined connections;
    //     otherwise, false.
    extern public bool SupportsPipelining { get; }
    //
    // Summary:
    //     Gets or sets a System.Boolean value that determines whether the Nagle algorithm
    //     is used on connections managed by this System.Net.ServicePoint object.
    //
    // Returns:
    //     true to use the Nagle algorithm; otherwise, false. The default value is true.
    extern public bool UseNagleAlgorithm { get; set; }

    // Summary:
    //     Removes the specified connection group from this System.Net.ServicePoint
    //     object.
    //
    // Parameters:
    //   connectionGroupName:
    //     The name of the connection group that contains the connections to close and
    //     remove from this service point.
    //
    // Returns:
    //     A System.Boolean value that indicates whether the connection group was closed.
    extern public bool CloseConnectionGroup(string connectionGroupName);

    extern public void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval);
  }
}

#endif