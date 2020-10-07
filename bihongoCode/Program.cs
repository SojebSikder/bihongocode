using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bihongoCode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new Form1();

            // Add these lines:
            // ----------------------------------------------
            string[] args = Environment.GetCommandLineArgs();
            if (args.Count() >= 2)
            {
                mainForm.LoadFile(args[1]);
            }
                
            // ----------------------------------------------

            Application.Run(mainForm);
        }
    }
}
