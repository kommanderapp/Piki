using System;
using System.Threading;
using System.Windows.Forms;

namespace Piki.Tray
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Guid mutexName = Guid.NewGuid();

            if (!Mutex.TryOpenExisting(mutexName.ToString(), out Mutex mutex))
            {
                mutex = new Mutex(false, mutexName.ToString());
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new TrayApplicationContext());
                mutex.Close();
            }
        }
    }
}