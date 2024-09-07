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

tagRECT RECTThis;


// #define LP (LPCWSTR)
#define DLLEXPORT extern "C" __declspec(dllexport)

DLLEXPORT int _GetCursorPosX();

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


//DLLEXPORT tagRECT WINAPI DisplayMonitor()
//{
//
//	 
//
//	EnumDisplayMonitors(0, 0, MonitorEnum, 0);
//
//	 
//	return RECTThis;
//}

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



#pragma region Shell



//SHSTDAPI_(HINSTANCE) ShellExecuteW(
//	_In_opt_ HWND hwnd,
//	_In_opt_ LPCWSTR lpOperation,
//	_In_ LPCWSTR lpFile,
//	_In_opt_ LPCWSTR lpParameters,
//	_In_opt_ LPCWSTR lpDirectory,
//	_In_ INT nShowCmd
//)
// 
//  L"find"
DLLEXPORT int ShellExplorer(HWND hwnd, const char* lpOperation , const char* lpFile)
{
	 
	return (int)ShellExecute(hwnd, (LPCWSTR)lpOperation, (LPCWSTR)lpFile, NULL, NULL, 0);
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

#pragma region Hide
//DLLEXPORT int MessageBoxShowTopMost( const char*  lpText , const char* lpCaption)
//{
//
//
//	return MessageBox(
//		NULL,
//		(LPCTSTR)lpText,
//		(LPCTSTR)lpCaption,
//		MB_OK | MB_ICONWARNING | MB_TOPMOST
//	);
//
//}
#pragma endregion



#pragma region DLlMain
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

#pragma endregion

