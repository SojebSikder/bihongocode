using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bihongoPlugin;
using System.Windows.Forms;

namespace testTheme
{
    public class Theme : StandardIO
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
            get { return "TestTheme"; }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        // Define plugin description
        public string Description
        {
            get
            {
                return "Test Theme";
            }
        }

        //Specify plugin position
        public string[] position
        {
            get
            {
                return new string[] { };
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

    }
}
