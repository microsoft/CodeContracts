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
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Net.NetworkInformation {

  // Summary:
  //     Specifies the Internet Protocol versions that are supported by a network
  //     interface.
  public enum NetworkInterfaceComponent
  {
    // Summary:
    //     Internet Protocol version 4.
    IPv4 = 0,
    //
    // Summary:
    //     Internet Protocol version 6.
    IPv6 = 1,
  }

  // Summary:
  //     Specifies the operational state of a network interface.
  public enum OperationalStatus
  {
    // Summary:
    //     The network interface is up; it can transmit data packets.
    Up = 1,
    //
    // Summary:
    //     The network interface is unable to transmit data packets.
    Down = 2,
    //
    // Summary:
    //     The network interface is running tests.
    Testing = 3,
    //
    // Summary:
    //     The network interface status is not known.
    Unknown = 4,
    //
    // Summary:
    //     The network interface is not in a condition to transmit data packets; it
    //     is waiting for an external event.
    Dormant = 5,
    //
    // Summary:
    //     The network interface is unable to transmit data packets because of a missing
    //     component, typically a hardware component.
    NotPresent = 6,
    //
    // Summary:
    //     The network interface is unable to transmit data packets because it runs
    //     on top of one or more other interfaces, and at least one of these "lower
    //     layer" interfaces is down.
    LowerLayerDown = 7,
  }

  // Summary:
  //     Specifies types of network interfaces.
  public enum NetworkInterfaceType
  {
    // Summary:
    //     The interface type is not known.
    Unknown = 1,
    //
    // Summary:
    //     The network interface uses an Ethernet connection. Ethernet is defined in
    //     IEEE standard 802.3.
    Ethernet = 6,
    //
    // Summary:
    //     The network interface uses a Token-Ring connection. Token-Ring is defined
    //     in IEEE standard 802.5.
    TokenRing = 9,
    //
    // Summary:
    //     The network interface uses a Fiber Distributed Data Interface (FDDI) connection.
    //     FDDI is a set of standards for data transmission on fiber optic lines in
    //     a local area network.
    Fddi = 15,
    //
    // Summary:
    //     The network interface uses a basic rate interface Integrated Services Digital
    //     Network (ISDN) connection. ISDN is a set of standards for data transmission
    //     over telephone lines.
    BasicIsdn = 20,
    //
    // Summary:
    //     The network interface uses a primary rate interface Integrated Services Digital
    //     Network (ISDN) connection. ISDN is a set of standards for data transmission
    //     over telephone lines.
    PrimaryIsdn = 21,
    //
    // Summary:
    //     The network interface uses a Point-To-Point protocol (PPP) connection. PPP
    //     is a protocol for data transmission using a serial device.
    Ppp = 23,
    //
    // Summary:
    //     The network interface is a loopback adapter. Such interfaces are used primarily
    //     for testing; no traffic is sent.
    Loopback = 24,
    //
    // Summary:
    //     The network interface uses an Ethernet 3 megabit/second connection. This
    //     version of Ethernet is defined in IETF RFC 895.
    Ethernet3Megabit = 26,
    //
    // Summary:
    //     The network interface uses a Serial Line Internet Protocol (SLIP) connection.
    //     SLIP is defined in IETF RFC 1055.
    Slip = 28,
    //
    // Summary:
    //     The network interface uses asynchronous transfer mode (ATM) for data transmission.
    Atm = 37,
    //
    // Summary:
    //     The network interface uses a modem.
    GenericModem = 48,
    //
    // Summary:
    //     The network interface uses a Fast Ethernet connection. Fast Ethernet provides
    //     a data rate of 100 megabits per second, known as 100BaseT.
    FastEthernetT = 62,
    //
    // Summary:
    //     The network interface uses a connection configured for ISDN and the X.25
    //     protocol. X.25 allows computers on public networks to communicate using an
    //     intermediary computer.
    Isdn = 63,
    //
    // Summary:
    //     The network interface uses a Fast Ethernet connection over optical fiber.
    //     This type of connection is also known as 100BaseFX.
    FastEthernetFx = 69,
    //
    // Summary:
    //     The network interface uses a wireless LAN connection (IEEE 802.11 standard).
    Wireless80211 = 71,
    //
    // Summary:
    //     The network interface uses an Asymmetric Digital Subscriber Line (ADSL).
    AsymmetricDsl = 94,
    //
    // Summary:
    //     The network interface uses a Rate Adaptive Digital Subscriber Line (RADSL).
    RateAdaptDsl = 95,
    //
    // Summary:
    //     The network interface uses a Symmetric Digital Subscriber Line (SDSL).
    SymmetricDsl = 96,
    //
    // Summary:
    //     The network interface uses a Very High Data Rate Digital Subscriber Line
    //     (VDSL).
    VeryHighSpeedDsl = 97,
    //
    // Summary:
    //     The network interface uses Internet Protocol (IP) in combination with asynchronous
    //     transfer mode (ATM) for data transmission.
    IPOverAtm = 114,
    //
    // Summary:
    //     The network interface uses a gigabit Ethernet connection.
    GigabitEthernet = 117,
    //
    // Summary:
    //     The network interface uses a tunnel connection.
    Tunnel = 131,
    //
    // Summary:
    //     The network interface uses a Multirate Digital Subscriber Line.
    MultiRateSymmetricDsl = 143,
    //
    // Summary:
    //     The network interface uses a High Performance Serial Bus.
    HighPerformanceSerialBus = 144,
  }

  // Summary:
  //     Provides configuration and statistical information for a network interface.
  public abstract class NetworkInterface {
    // Summary:
    //     Initializes a new instance of the System.Net.NetworkInformation.NetworkInterface
    //     class.
    extern protected NetworkInterface();

    // Summary:
    //     Gets the description of the interface.
    //
    // Returns:
    //     A System.String that describes this interface.
    public abstract string Description { get; }
    //
    // Summary:
    //     Gets the identifier of the network adapter.
    //
    // Returns:
    //     A System.String that contains the identifier.
    public abstract string Id { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the network interface
    //     is set to only receive data packets.
    //
    // Returns:
    //     true if the interface only receives network traffic; otherwise, false.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract bool IsReceiveOnly { get; }
    //
    // Summary:
    //     Gets the index of the loopback interface.
    //
    // Returns:
    //     A System.Int32 that contains the index for the loopback interface.
    extern public static int LoopbackInterfaceIndex { get; }
    //
    // Summary:
    //     Gets the name of the network adapter.
    //
    // Returns:
    //     A System.String that contains the adapter name.
    public abstract string Name { get; }
    //
    // Summary:
    //     Gets the interface type.
    //
    // Returns:
    //     An System.Net.NetworkInformation.NetworkInterfaceType value that specifies
    //     the network interface type.
    public abstract NetworkInterfaceType NetworkInterfaceType { get; }
    //
    // Summary:
    //     Gets the current operational state of the network connection.
    //
    // Returns:
    //     One of the System.Net.NetworkInformation.OperationalStatus values.
    public abstract OperationalStatus OperationalStatus { get; }
    //
    // Summary:
    //     Gets the speed of the network interface.
    //
    // Returns:
    //     A System.Int64 value that specifies the speed in bits per second.
    public abstract long Speed { get; }
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the network interface
    //     is enabled to receive multicast packets.
    //
    // Returns:
    //     true if the interface receives multicast packets; otherwise, false.
    //
    // Exceptions:
    //   System.PlatformNotSupportedException:
    //     This property is not valid on computers running operating systems earlier
    //     than Windows XP.
    public abstract bool SupportsMulticast { get; }

    // Summary:
    //     Returns objects that describe the network interfaces on the local computer.
    //
    // Returns:
    //     A System.Net.NetworkInformation.NetworkInterface array that contains objects
    //     that describe the available network interfaces, or an empty array if no interfaces
    //     are detected.
    //
    // Exceptions:
    //   System.Net.NetworkInformation.NetworkInformationException:
    //     A Windows system function call failed.
    [Pure]
    public static NetworkInterface[] GetAllNetworkInterfaces()
    {
        Contract.Ensures(Contract.Result<NetworkInterface[]>() != null);
        return default(NetworkInterface[]);
    }
    //
    // Summary:
    //     Returns an object that describes the configuration of this network interface.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPInterfaceProperties object that describes
    //     this network interface.
    [Pure]
    public abstract IPInterfaceProperties GetIPProperties();
    //
    // Summary:
    //     Gets the IPv4 statistics.
    //
    // Returns:
    //     An System.Net.NetworkInformation.IPv4InterfaceStatistics object.
    [Pure]
    public abstract IPv4InterfaceStatistics GetIPv4Statistics();
    //
    // Summary:
    //     Indicates whether any network connection is available.
    //
    // Returns:
    //     true if a network connection is available; otherwise, false.
    [Pure]
    extern public static bool GetIsNetworkAvailable();
    //
    // Summary:
    //     Returns the Media Access Control (MAC) address for this adapter.
    //
    // Returns:
    //     A System.Net.NetworkInformation.PhysicalAddress object that contains the
    //     physical address.
    [Pure]
    public abstract PhysicalAddress GetPhysicalAddress();
    //
    // Summary:
    //     Gets a System.Boolean value that indicates whether the interface supports
    //     the specified protocol.
    //
    // Parameters:
    //   networkInterfaceComponent:
    //     A System.Net.NetworkInformation.NetworkInterfaceComponent value.
    //
    // Returns:
    //     true if the specified protocol is supported; otherwise, false.
    [Pure]
    public abstract bool Supports(NetworkInterfaceComponent networkInterfaceComponent);
  }
}

#endif