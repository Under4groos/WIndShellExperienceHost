// Registry.cpp : Этот файл содержит функцию "main". Здесь начинается и заканчивается выполнение программы.
//

#include "Includes.h"



//RegOpenKeyA(
//    _In_ HKEY hKey,
//    _In_opt_ LPCSTR lpSubKey,
//    _Out_ PHKEY phkResult
//);

HKEY Handler = 0;
int c_ = 0;
int main()
{
	LSTATUS reg_ = RegOpenKeyA(HKEY_LOCAL_MACHINE, "asd", &Handler);
	{
		if (reg_ != ERROR_SUCCESS) {
			std::cout << reg_ << endl;
		}


	}
	RegCloseKey(Handler);
	cin >> c_;
}

