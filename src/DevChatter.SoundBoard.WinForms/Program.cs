using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevChatter.SoundBoard.WinForms
{
    static class Program
    {
        private static NotifyIcon _notifyIcon;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon("devchatter.ico"),
                Visible = true,
                ContextMenu = new ContextMenu()
            };


            var menuItemExit = new MenuItem { Text = @"Exit" };
            menuItemExit.Click += MenuItemExitOnClick;

            var menuItemConfigure = new MenuItem { Text = @"Configure" };
            menuItemConfigure.Click += (sender, args) =>
            {
                _notifyIcon.ShowBalloonTip(0, "Configure Menu", "You Clicked Configure", ToolTipIcon.None);
            };

            _notifyIcon.ContextMenu.MenuItems.AddRange(new[] { menuItemConfigure, menuItemExit });

            Application.Run();

            Application.ApplicationExit += ApplicationOnApplicationExit;
        }

        private static void ApplicationOnApplicationExit(object o, EventArgs eventArgs)
        {
            _notifyIcon.Dispose();
        }

        private static void MenuItemExitOnClick(object sender, EventArgs eventArgs)
        {
            Application.Exit();
        }
    }
}
