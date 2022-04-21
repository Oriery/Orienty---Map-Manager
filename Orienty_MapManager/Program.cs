using System;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    static class Program
    {
        public static Form1 form;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1();
            Application.Run(form);
        }
    }
}
