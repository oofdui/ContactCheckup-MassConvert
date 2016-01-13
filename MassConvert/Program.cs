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
            var isAutoMassConvert = false;
            if (args.Length > 0)
            {
                for(int i = 0; i < args.Length; i++)
                {
                    if (args[i].ToLower().Trim() == "auto")
                    {
                        isAutoMassConvert = true;
                    }
                    else
                    {
                        var clsTempData = new clsTempData();
                        clsTempData.Username = args[i];
                    }
                }
            }
            if (isAutoMassConvert)
            {
                Application.Run(new AutoMassConvert());
            }
            else
            { 
                Application.Run(new MDIMassConvert());
            }
        }
    }
}