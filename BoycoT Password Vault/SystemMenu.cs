using System;
using System.Runtime.InteropServices;

namespace BoycoT_Password_Vault
{
    static class SystemMenu
    {
        internal const Int32 WM_SYSCOMMAND = 0x112;
        internal const Int32 MF_BYPOSITION = 0x400;
        internal const Int32 MF_SEPARATOR = 0x800;
        internal const Int32 SHOW_SETTINGS_DIALOG = 1000;
        internal const Int32 EXPORT_TO_CSV = 1001;
        internal const Int32 ABOUT_THIS_PROGRAM = 1002;

        [DllImport("user32.dll")]
        internal static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);
    }
}
