// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"
#include <cstdio>
#include <iostream>
#include <shellapi.h>
#include <string>
#include <winuser.h>
#include <Windows.h>
MSG msg;
POINT point;
std::string message;
#define DLLEXPORT extern "C" __declspec(dllexport)


#pragma region error
DLLEXPORT const char* GetLastErrorAsString()
{
	//Get the error message ID, if any.
	DWORD errorMessageID = ::GetLastError();
	if (errorMessageID == 0) {
		return std::string().c_str(); //No error message has been recorded
	}

	LPSTR messageBuffer = nullptr;

	//Ask Win32 to give us the string version of that message ID.
	//The parameters we pass in, tell Win32 to create the buffer that holds the message for us (because we don't yet know how long the message string will be).
	size_t size = FormatMessageA(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
		NULL, errorMessageID, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&messageBuffer, 0, NULL);

	//Copy the error message into a std::string.
	message = (messageBuffer, size);

	//Free the Win32's string's buffer.
	LocalFree(messageBuffer);

	return message.c_str();
}
#pragma endregion


#pragma region HotKey
DLLEXPORT bool HotKey_Register(HWND hwd, int id_register, int vk)
{
	bool status = RegisterHotKey(hwd, id_register, MOD_ALT | MOD_NOREPEAT, vk);
	std::string error = std::system_category().message(::GetLastError());
	std::cout << "Status reg: " << status << std::endl;
	std::cout << error << std::endl;
	return status;
}

DLLEXPORT bool HotKey_Unregister(HWND hwd, int id_register)
{
	return UnregisterHotKey(NULL, id_register);
}
#pragma endregion


#pragma region Screens
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors
//	WINUSERAPI
//	BOOL
//	WINAPI
//	EnumDisplayMonitors(
//	_In_opt_ HDC hdc,
//	_In_opt_ LPCRECT lprcClip,
//	_In_ MONITORENUMPROC lpfnEnum,
//	_In_ LPARAM dwData);

DLLEXPORT BOOL WINAPI w_EnumDisplayMonitors(_In_ MONITORENUMPROC lpfnEnum, _In_ LPARAM dwData)
{
	return EnumDisplayMonitors(0, 0, lpfnEnum, dwData);
}
#pragma endregion



#pragma region Console
DLLEXPORT void _AllocConsole()
{

	if (AllocConsole())
	{
		FILE* fDummy;
		freopen_s(&fDummy, "CONOUT$", "w", stdout);

		std::cout.clear();
		setlocale(LC_ALL, "Russian");
	}
}

DLLEXPORT bool _FreeConsole()
{

	return FreeConsole();
}
#pragma endregion






DLLEXPORT int _GetMessage()
{
	GetMessage(&msg, NULL, 0, 0);
	return msg.message;
}
DLLEXPORT int _GetCursorPosX()
{
	if (GetCursorPos(&point)) {
		return point.x;
	}
	return 0;
}

BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

