using System;
using System.Runtime.InteropServices;

namespace Cantact
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CantactFrame
	{
		public byte channel;
		public UInt32 id;

		public byte dlc;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] data;

		public byte ext;
		public byte fd;
		public byte loopback;
		public byte rtr;
	}

	public class NativeFunctions
	{
		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static IntPtr cantact_init();


		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_deinit(IntPtr handle);


		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_open(IntPtr handle);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_close(IntPtr handle);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_start(IntPtr handle);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_stop(IntPtr handle);


		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_transmit(IntPtr handle, CantactFrame cantactFrame);


		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_set_bitrate(IntPtr handle, byte channel, UInt32 bitrate);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_set_enabled(IntPtr handle, byte channel, byte enabled);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_set_monitor(IntPtr handle, byte channel, byte enabled);

		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_set_hw_loopback(IntPtr handle, byte channel, byte enabled);


		[DllImport("cantact.dll", CallingConvention = CallingConvention.StdCall)]
		public extern static Int32 cantact_get_channel_count(IntPtr handle);
	}
}
