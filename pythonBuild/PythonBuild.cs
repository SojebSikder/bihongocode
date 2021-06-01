using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bihongoPlugin;
using System.Diagnostics;
using System.Windows.Forms;

namespace pythonBuild
{
    public class PythonBuild : StandardIO
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
            get { return "Python Build"; }
        }
        // Define plugin version
        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        // Defin plugin description
        public string Description
        {
            get
            {
                return "Build Python Code";
            }
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

        //This method will be start first
        public void Init()
        {
            //MessageBox.Show("Initiated... "+tes);
        }
        //this is start when plugin will clicked
        public void Start()
        {
            //MessageBox.Show(tes);
            buildPython(FileAddress);
        }

        //functions


        public void buildPython(string arg)
        {
            if (arg != null)
            {
                Process.Start("python",  "-m compileall "+arg);
            }
            else
            {
                MessageBox.Show("Save Python file first");
            }

        }
        //end functions
    }
}
