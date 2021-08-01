using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Windows.Forms;
using System.Drawing;
using System.IO;
using bihongoPlugin;
using System.Diagnostics;

namespace python
{
    public class Python : StandardIO
    {
        //Get editor text
        public string GetEditorText { get; set; }
        //Get Editor File address where file have been saved
        public string FileAddress { get; set; }
        //Get Editor file extension
        public string FileExtension { get; set; }

        //specify plugin name
        public string Name
        {
            get { return "Python"; }
        }

        //Specify plugin position
        public string[] position
        {
            get
            {
                return new string[]{
                    MenuPosition.Toolbar.ToString()
                };
            }
        }

        //This method will be start first before window created
        public void Init()
        {
            //MessageBox.Show("Initiated... ");
        }
        //this is start when plugin will clicked
        public void Start()
        {
            runPython(FileAddress);
        }

        public Dictionary<string, Action<string>> command
        {
            get
            {
                return new Dictionary<string, Action<string>>()
                {
                    // syntex {"create menu <Label> <unique-name>", method}
                    { "create menu RunPy rpy", run },
                    { "create menu BuildPy bpy", buildPython },
                };
            }

        }

        //functions
        public void runPython(string arg)
        {
            if (arg != null)
            {
                Process.Start("python", arg);
            }
            else
            {
                MessageBox.Show("Save Python file first");
            }

        }

        public void run(string sender = "null")
        {
            string arg = FileAddress;
            if (arg != null)
            {
                Process.Start("python", arg);
            }
            else
            {
                MessageBox.Show("Save Python file first");
            }
        }

        public void buildPython(string sender = "null")
        {
            string arg = FileAddress;
            if (arg != null)
            {
                Process.Start("python", "-m compileall " + arg);
            }
            else
            {
                MessageBox.Show("Save Python file first");
            }
        }
        //end functions


    }
}
