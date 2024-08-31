// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"
#include <cstdio>
#include <iostream>
#include <shellapi.h>
#include <string>
MSG msg;
POINT point;

#define DLLEXPORT extern "C" __declspec(dllexport)




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

