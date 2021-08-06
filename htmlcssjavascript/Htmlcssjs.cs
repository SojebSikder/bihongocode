using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using bihongoPlugin;

namespace htmlcssjavascript
{
    public class Htmlcssjs : StandardIO
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
            get { return "Htmlcssjavascript"; }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }
        public string SetExt
        {
            get
            {
                return ".html@html.xml|.css@css.xml|.js@js.xml";
            }
        }

        // Defin plugin description
        public string Description
        {
            get
            {
                return "Extension for html css js";
            }
        }

        //Specify plugin position
        public string[] position
        {
            get
            {
                return new string[]{};
            }
        }

        //This method will be start first before window created
        public void Init()
        {
        }
        //this is start when plugin will clicked
        public void Start()
        {
            MessageBox.Show("Hello World");
        }

        //public Dictionary<string, Action<string>> command
        //{
        //    get
        //    {
        //        return new Dictionary<string, Action<string>>()
        //        {
        //            // syntex {"create menu <Label> <unique-name>", method}
        //            { "create menu RunPy rpy", run },
        //            { "create menu BuildPy bpy", buildPython },
        //        };
        //    }

        //}
    }
}
