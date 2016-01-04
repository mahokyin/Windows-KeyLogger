using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class KeyboardStatus
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern short GetKeyState(int keyCode);

    private static bool initFlag = false;
    private static bool shift = false;
    private static bool capsLock;
    private static bool numLock;
    private static bool scrollLock;
    private static int keyCode;

    public static void initilize()
    {
        initFlag = true;
        capsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        numLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        scrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;

        string capsStatus = "CapsLock: " + capsLock;
        string numStatus = "NumLock: " + numLock;
        string scrollStatus = "ScrollLock: " + scrollLock;
        TextPrinter.printLine(capsStatus);
        TextPrinter.printLine(numStatus);
        TextPrinter.printLine(scrollStatus);
        TextPrinter.printLine();
    }

    public static void setShiftFlag(bool status)
    {
        shift = status;
    }

    public static bool getShiftFlag()
    {
        return shift;
    }

    public static void setCapsFlag(bool status)
    {
        capsLock = status;
    }

    public static bool getCapsFlag()
    {
        return capsLock;
    }

    public static void setKeyCode(int code)
    {
        //CapsLock check
        if (code == 20 && capsLock == false) capsLock = true;
        else if (code == 20 && capsLock == true) capsLock = false;
        //NumLock check
        else if (code == 144 && numLock == true) numLock = false;
        else if (code == 144 && numLock == false) numLock = true;

        keyCode = code;
    }

    public static int getKeyCode()
    {
        return keyCode;
    }
}