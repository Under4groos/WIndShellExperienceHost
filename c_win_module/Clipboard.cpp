#include "Includes.h"



DLLEXPORT bool clipboard_set_data(HWND Hwnd, const char* output) {
	if (!OpenClipboard(Hwnd))
		return false;

	const size_t len = strlen(output) + 1;
	HGLOBAL hMem = GlobalAlloc(GMEM_MOVEABLE, len);
	memcpy(GlobalLock(hMem), output, len);
	GlobalUnlock(hMem);

	EmptyClipboard();
	SetClipboardData(CF_TEXT, hMem);

	CloseClipboard();

	return true;
}