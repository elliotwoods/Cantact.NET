using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Cantact
{
	class Program
	{
		static unsafe void Main(string[] args)
		{
			Console.WriteLine("Initializing driver");
			var handle = NativeFunctions.cantact_init();


			Console.WriteLine("Opening ");
			if(NativeFunctions.cantact_open(handle) != 0)
			{
				throw (new Exception("Failed to open driver"));
			}

			Console.WriteLine("Getting channel count:");
			var channelCount = NativeFunctions.cantact_get_channel_count(handle);
			Console.WriteLine(channelCount);

			Console.WriteLine("Closing (just following how it works in BusMaster)");
			NativeFunctions.cantact_close(handle);

			CantactFrame frame = new CantactFrame();
			{
				frame.channel = 0;
				frame.id = 1 << 19;

				{
					frame.data = new byte[8];
					var binaryWriter = new BinaryWriter(new MemoryStream(frame.data));
					binaryWriter.Write((byte)1);
					binaryWriter.Write((UInt16)1);
					binaryWriter.Write((Int32)1);
				}
			
				frame.dlc = 7;
				frame.ext = 1;
				frame.fd = 0;
				frame.rtr = 0;
			}

			Console.WriteLine("Setting bit rate");
			NativeFunctions.cantact_open(handle);
			NativeFunctions.cantact_set_enabled(handle, 0, 1);
			if(NativeFunctions.cantact_set_bitrate(handle, 0, 500000) < 0)
			{
				throw (new Exception("Failed to set bitrate"));
			}
			NativeFunctions.cantact_close(handle);

			Console.WriteLine("Starting");

			NativeFunctions.cantact_open(handle);
			if (NativeFunctions.cantact_start(handle) != 0) {
				throw (new Exception("Failed to start device"));
			}

			Console.WriteLine("Transmitting");
			Marshal.SizeOf(typeof(CantactFrame));
			//var result = NativeFunctions.cantact_transmit(handle, frame);
			//Console.WriteLine(result);

			NativeFunctions.cantact_stop(handle);
			NativeFunctions.cantact_close(handle);
			NativeFunctions.cantact_deinit(handle);
		}
	}
}
