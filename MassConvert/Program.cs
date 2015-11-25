using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MassConvert
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            if (args.Length > 0)
            {
                var clsTempData = new clsTempData();
                clsTempData.Username = args[0];
            }
            Application.Run(new MDIMassConvert());
        }
    }
}
