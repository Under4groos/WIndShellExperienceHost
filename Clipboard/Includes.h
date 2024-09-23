#pragma once
#include <iostream>
#include <Windows.h>
using namespace std;


#define CUR_CF CF_TEXT
#define Hwnd HANDLE



typedef int (*func)(int);
void debugptr(Hwnd hwnd, const char* arrname, int arg = 0) {

	func _hwnd = (func)hwnd;


	int res_ = arg == 0 ? _hwnd(CUR_CF) : _hwnd(arg);
	cout << arrname << ": Adr: " << hwnd << " Result: " << res_ << endl;
}