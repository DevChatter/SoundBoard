using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevChatter.SoundBoard.Infra;
using System.Windows.Input;
using GlobalHotKey;


namespace DevChatter.SoundBoard.WinForms
{
    static class Program
    {
        private static NotifyIcon _notifyIcon;
        private static HotKeyManager _hotKeyManager = new HotKeyManager();
        private static HotKey _numpad0Hotkey;

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

            _numpad0Hotkey = _hotKeyManager.Register(Key.NumPad0, ModifierKeys.None);

            _hotKeyManager.KeyPressed += HotKeyManagerOnKeyPressed;

            _notifyIcon.ContextMenu.MenuItems.AddRange(new[]
            {
                menuItemConfigure,
                menuItemExit
            });

            Application.Run();

            Application.ApplicationExit += ApplicationOnApplicationExit;
        }

        private static void HotKeyManagerOnKeyPressed(object o, KeyPressedEventArgs e)
        {
            if (e.HotKey.Key == _numpad0Hotkey.Key)
            {
                var audioPlayer = new AudioPlayer();
                audioPlayer.PlayAudioTrack(@"over9000.mp3");
            }
        }

        private static void ApplicationOnApplicationExit(object o, EventArgs eventArgs)
        {
            _hotKeyManager.Dispose();
            _notifyIcon.Dispose();
        }

        private static void MenuItemExitOnClick(object sender, EventArgs eventArgs)
        {
            Application.Exit();
        }
    }
}
