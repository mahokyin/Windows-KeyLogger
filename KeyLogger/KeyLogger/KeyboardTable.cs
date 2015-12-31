using System;
using System.Collections;
using System.Windows.Forms;

// https://msdn.microsoft.com/en-us/library/aa243025(v=vs.60).aspx

public class KeyboardTable
{
    private static Hashtable keyTable = new Hashtable();
    private static Hashtable keyShiftTable = new Hashtable();

    private static bool initFlag = false;
    private static bool initShiftFlag = false;

    public static Hashtable getInstance()
    {
        if (!initFlag)
            initialize();
        return keyTable;
    }

    public static Hashtable getShiftInstance()
    {
        if (!initShiftFlag)
            initializeShift();
        return keyShiftTable;
    }

    private static void initialize()
    {
        //Special keys
        keyTable.Add(32, "[SPACE]"); //[SPACE]
        keyTable.Add(13, "[ENTER]"); // [Return / enter]
        keyTable.Add(9, "[TAB]"); // [TAB]
        keyTable.Add(8, "[Backspace]");
        keyTable.Add(27, "[ESC]");
        keyTable.Add(37, "[LEFT]");
        keyTable.Add(39, "[RIGHT]");
        keyTable.Add(38, "[UP]");
        keyTable.Add(40, "[DOWN]");
        keyTable.Add(35, "[END]");
        keyTable.Add(36, "[HOME]");
        keyTable.Add(20, "[CapsLock]");
        keyTable.Add(144, "[NumLock]");

        keyTable.Add(112, "[F1]");
        keyTable.Add(113, "[F2]");
        keyTable.Add(114, "[F3]");
        keyTable.Add(115, "[F4]");
        keyTable.Add(116, "[F5]");
        keyTable.Add(117, "[F6]");
        keyTable.Add(118, "[F7]");
        keyTable.Add(119, "[F8]");
        keyTable.Add(120, "[F9]");
        keyTable.Add(121, "[F10]");
        keyTable.Add(122, "[F11]");
        keyTable.Add(123, "[F12]");

        // Numpad keys
        keyTable.Add(96, "0");
        keyTable.Add(97, "1");
        keyTable.Add(98, "2");
        keyTable.Add(99, "3");
        keyTable.Add(100, "4");
        keyTable.Add(101, "5");
        keyTable.Add(102, "6");
        keyTable.Add(103, "7");
        keyTable.Add(104, "8");
        keyTable.Add(105, "9");
        keyTable.Add(106, "*");
        keyTable.Add(107, "+");
        keyTable.Add(108, "[ENTER]"); //enter
        keyTable.Add(109, "-");
        keyTable.Add(110, ".");
        keyTable.Add(111, "/");
        
        //Number keys
        keyTable.Add(48, "0");
        keyTable.Add(49, "1");
        keyTable.Add(50, "2");
        keyTable.Add(51, "3");
        keyTable.Add(52, "4");
        keyTable.Add(53, "5");
        keyTable.Add(54, "6");
        keyTable.Add(55, "7");
        keyTable.Add(56, "8");
        keyTable.Add(57, "9");

        //Character keys
        keyTable.Add(65, "a");
        keyTable.Add(66, "b");
        keyTable.Add(67, "c");
        keyTable.Add(68, "d");
        keyTable.Add(69, "e");
        keyTable.Add(70, "f");
        keyTable.Add(71, "g");
        keyTable.Add(72, "h");
        keyTable.Add(73, "i");
        keyTable.Add(74, "j");
        keyTable.Add(75, "k");
        keyTable.Add(76, "l");
        keyTable.Add(77, "m");
        keyTable.Add(78, "n");
        keyTable.Add(79, "o");
        keyTable.Add(80, "p");
        keyTable.Add(81, "q");
        keyTable.Add(82, "r");
        keyTable.Add(83, "s");
        keyTable.Add(84, "t");
        keyTable.Add(85, "u");
        keyTable.Add(86, "v");
        keyTable.Add(87, "w");
        keyTable.Add(88, "x");
        keyTable.Add(89, "y");
        keyTable.Add(90, "z");

        initFlag = true;
    }

    private static void initializeShift()
    {
        //Number keys
        keyShiftTable.Add(48, ")");
        keyShiftTable.Add(49, "!");
        keyShiftTable.Add(50, "@");
        keyShiftTable.Add(51, "#");
        keyShiftTable.Add(52, "$");
        keyShiftTable.Add(53, "%");
        keyShiftTable.Add(54, "^");
        keyShiftTable.Add(55, "&");
        keyShiftTable.Add(56, "*");
        keyShiftTable.Add(57, "(");

        keyShiftTable.Add(65, "A");
        keyShiftTable.Add(66, "B");
        keyShiftTable.Add(67, "C");
        keyShiftTable.Add(68, "D");
        keyShiftTable.Add(69, "E");
        keyShiftTable.Add(70, "F");
        keyShiftTable.Add(71, "G");
        keyShiftTable.Add(72, "H");
        keyShiftTable.Add(73, "I");
        keyShiftTable.Add(74, "J");
        keyShiftTable.Add(75, "K");
        keyShiftTable.Add(76, "L");
        keyShiftTable.Add(77, "M");
        keyShiftTable.Add(78, "N");
        keyShiftTable.Add(79, "O");
        keyShiftTable.Add(80, "P");
        keyShiftTable.Add(81, "Q");
        keyShiftTable.Add(82, "R");
        keyShiftTable.Add(83, "S");
        keyShiftTable.Add(84, "T");
        keyShiftTable.Add(85, "U");
        keyShiftTable.Add(86, "V");
        keyShiftTable.Add(87, "W");
        keyShiftTable.Add(88, "X");
        keyShiftTable.Add(89, "Y");
        keyShiftTable.Add(90, "Z");

        initShiftFlag = true;
    }
}
