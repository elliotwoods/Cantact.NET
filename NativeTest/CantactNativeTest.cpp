// CantactNativeTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "cantact.h"
#include <chrono>
#include <thread>

void __cdecl rxCallback(struct CantactFrame* frame)
{
	std::cout << "Rx : " << std::endl;
}

int main()
{
	auto handle = cantact_init();
	if (cantact_open(handle) != 0) {
		std::cerr << "Couldn't open!" << std::endl;
	}

	std::cout << "Getting channel count : ";
	std::cout << cantact_get_channel_count(handle) << std::endl;

	std::cout << "Closing again\n";
	cantact_close(handle);

	CantactFrame frame;
	{
		frame.channel = 0;
		frame.id = 1 << 19;

		{
			auto data = frame.data;
			data[1] = 1;
			data[2] = 1;
			data[3] = 0;
			data[4] = 1;
			data[5] = 0;
			data[6] = 0;
			data[7] = 0;
			data[8] = 0;
		}

		frame.dlc = 7;
		frame.ext = 0;
		frame.fd = 0;
		frame.rtr = 0;
	}

	std::cout << "Setting bit rate" << std::endl;
	cantact_open(handle);
	cantact_set_enabled(handle, 0, 1); 
	if (cantact_set_bitrate(handle, 0, 500000) < 0) {
		std::cerr << "Failed to set bit rate" << std::endl;
	}
	cantact_close(handle);

	std::cout << "Starting" << std::endl;

	cantact_open(handle);
	cantact_set_rx_callback(handle, &rxCallback);
	if (cantact_start(handle) != 0) {
		std::cerr << "Failed to start device" << std::endl;
	}

	std::cout << "Transmitting" << std::endl;
	if (cantact_transmit(handle, frame) != 0) {
		std::cerr << "Transmit failed" << std::endl;
	}

	std::cout << "Receiving for 10 seconds" << std::endl;
	for (auto i = 0; i < 10; i++) {
		std::cout << i << ", ";
		std::this_thread::sleep_for(std::chrono::seconds(1));
	}

	cantact_stop(handle);
	cantact_close(handle);
	cantact_deinit(handle);
}