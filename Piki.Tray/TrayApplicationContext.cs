using Gma.System.MouseKeyHook;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Piki.Tray
{
    public class TrayApplicationContext : ApplicationContext
    {
        private static readonly string s_defaultTooltip = "PIKI";
        private static readonly string s_iconFileName = "pik.ico";
        private readonly IKeyboardMouseEvents _GlobalHook;

        private IContainer _components;
        private NotifyIcon _notifyIcon;

        public TrayApplicationContext()
        {
            InitializeContext();
            _GlobalHook = Hook.GlobalEvents();
            _GlobalHook.KeyDown += _GlobalHook_KeyDown;
        }

        private void _GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.PrintScreen) MessageBox.Show(e.KeyCode + "Pressed");
        }

        private void InitializeContext()
        {
            _components = new Container();
            _notifyIcon = new NotifyIcon(_components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(s_iconFileName),
                Text = s_defaultTooltip,
                Visible = true
            };
            _notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            _notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
            _notifyIcon.MouseUp += NotifyIcon_MouseUp;
        }

        private void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon)
                    .GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(_notifyIcon, null);
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExitThreadCore()
        {
            _notifyIcon.Visible = false; 
            base.ExitThreadCore();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _components != null) { _components.Dispose(); }
        }
    }
}
