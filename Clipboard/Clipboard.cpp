

#include "Includes.h"

Hwnd _HANDLE;


#define write_debug false
 

int main()
{
	int data = 0;

	HWND console_hwnd = GetConsoleWindow();
	cout << "Console: " << console_hwnd << endl;


#if write_debug
	debugptr(*OpenClipboard, "OpenClipboard", (int)console_hwnd);
	{
		debugptr(*IsClipboardFormatAvailable, "IsClipboardFormatAvailable");
		debugptr(*GetClipboardData, "GetClipboardData");


		debugptr(*GetClipboardOwner, "GetClipboardOwner");
	}
	debugptr(*CloseClipboard, "CloseClipboard", (int)console_hwnd);
#endif // write_debug



	if (OpenClipboard(console_hwnd)) {
		Hwnd hwnd_clp = GetClipboardData(CUR_CF);
		if (hwnd_clp != nullptr) {
			char* pszText = static_cast<char*>(GlobalLock(hwnd_clp));
			if (pszText != nullptr) {
				cout << pszText << endl;
			}
		}
	}
	CloseClipboard();



	cin >> data;
}

