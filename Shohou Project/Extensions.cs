using System;
using Microsoft.Xna.Framework;

namespace Ark.Xna {
    public static class Extensions {
        public static void RemoveBorder(this Game game) {
            var handle = game.Window.Handle;
            if (handle != IntPtr.Zero) {
#if Windows 
                ((System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(handle)).FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
#endif
            }
        }
    }
}