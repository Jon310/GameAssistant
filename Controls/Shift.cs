using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GameAssistant.Controls
{
    public class Shift
    {
        #region Shift
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);

        private const byte KEYBDEVENTF_SHIFTVIRTUAL = 0x10;
        private const byte KEYBDEVENTF_SHIFTSCANCODE = 0x2A;
        private const int KEYEVENTF_KEYUP = 2;
        private const int KEYEVENTF_KEYDOWN = 0;

        public static void ShiftUp()
        {
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYEVENTF_KEYUP, 0);
        }

        public static void ShiftDown()
        {
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYEVENTF_KEYDOWN, 0);
        }
        #endregion
    }
}
