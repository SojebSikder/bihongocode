﻿using System;
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

        public string tes { get; set; }

        //specify plugin name
        public string Name
        {
            get { return "Python"; }
        }

        //Specify plugin position
        public string[] position
        {
            get { 
                return new string[]{
                    MenuPosition.Toolbar.ToString()
                };
            }
        }

        //This method will be start first
        public void Init()
        {
            tes = "Hello i am working";
            //MessageBox.Show("Initiated... "+tes);
        }
        //this is start when plugin will clicked
        public void Start()
        {
            //MessageBox.Show(tes);
            runPython(FileAddress);
        }

        //functions
        public void runPython(string arg)
        {
            if(arg != null){
                Process.Start("python", arg);
            }
            else
            {
                MessageBox.Show("Save Python file first");
            }
            
        }
        //end functions

        
    }
}
