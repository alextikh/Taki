#include <stdio.h>
#include <stdlib.h>
#include <ws2tcpip.h>
#include <WinSock2.h>
#include <Windows.h>
#include <iostream>
#include <string>
#include <thread>
#include <vector>
#include "status_code.h"
#include "Card.h"
#include "Room.h"
#include "Manager.h"
#include "User.h"

#pragma comment (lib, "Ws2_32.lib")

#define PORT "10113"

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::thread;
using std::vector;

int main()
{
	Manager manager;
	vector<thread> threads;
	WSADATA info;
	int ret = WSAStartup(MAKEWORD(2, 0), &info) != 0;
	if (ret != 0)
	{
		cout << "WSAStartup failed with error code " << ret << endl;
		system("PAUSE");
		return 0;
	}

	addrinfo hints, *result = nullptr;

	ZeroMemory(&hints, sizeof(hints));
	hints.ai_family = AF_INET;
	hints.ai_socktype = SOCK_STREAM;
	hints.ai_protocol = IPPROTO_TCP;
	hints.ai_flags = AI_PASSIVE;

	// Resolve the server address and port
	ret = getaddrinfo(nullptr, PORT, &hints, &result);
	if (ret != 0)
	{
		cout << "getaddrinfo failed with error code:" << ret << endl;
		WSACleanup();
		cin.ignore();
		return 1;
	}

	SOCKET sock = socket(result->ai_family, result->ai_socktype, result->ai_protocol), client_sock;
	if (sock == INVALID_SOCKET)
	{
		cout << "socket failed with error code " << WSAGetLastError() << endl;
		freeaddrinfo(result);
		WSACleanup();
		cin.ignore();
		return 0;
	}

	if (bind(sock, result->ai_addr, (int)result->ai_addrlen) == SOCKET_ERROR)
	{
		cout << "bind failed with error code " << WSAGetLastError() << endl;
		closesocket(sock);
		freeaddrinfo(result);
		WSACleanup();
		cin.ignore();
		return 0;
	}
	if (listen(sock, SOMAXCONN) == SOCKET_ERROR)
	{
		cout << "listed failed with error code " << WSAGetLastError() << endl;
		closesocket(sock);
		freeaddrinfo(result);
		WSACleanup();
		cin.ignore();
		return 0;
	}

	while (true)
	{
		if ((client_sock = accept(sock, nullptr, nullptr)) == INVALID_SOCKET)
		{
			cout << "accept failed with error code " << WSAGetLastError() << endl;
			closesocket(sock);
			freeaddrinfo(result);
			WSACleanup();
			cin.ignore();
			return 0;
		}
		
		threads.push_back(thread(&Manager::client_requests_thread, &manager, client_sock));
		threads.back().join();
	}

	closesocket(sock);
	freeaddrinfo(result);
	WSACleanup();
	cin.ignore();
	return 0;
}