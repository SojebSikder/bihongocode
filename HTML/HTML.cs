using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bihongoPlugin;
using System.Windows.Forms;
using System.Drawing;

namespace HTML
{
    public class HTML : StandardIO
    {
        //Get editor text
        public string GetEditorText { get; set; }
        //Get Editor File address where file have been saved
        public string FileAddress { get; set; }
        //Get Editor file extension
        public string FileExtension { get; set; }
        


        public string Name
        {
            get { return "HTML"; }
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
                return "Html Viewer";
            }
        }

        public string SetExt
        {
            get
            {
                return ".css@css.xml|.jsx@jsx.xml";
            }
        }
        /// <summary>
        /// The control appears on File menu.
        /// </summary>
        public string[] position
        {
            get
            {
                return new string[]{
                    MenuPosition.Toolbar.ToString(),
                };
            }
        }

        /*
        public string[] command
        {
            get
            {
                return new string[]{
                    "create menu python",
                    "create menu HTML",
                };
            }
        }
        */

        public Dictionary<string, Action<string>> command
        {
            get
            {
                return new Dictionary<string, Action<string>>()
                {
                    { "create menu Python py", python },
                    { "create menu C# csharp", csharp },
                    { "create menu CMD bihongophp", bphp },

                };
            }

        }



        public static void python(string sender = "null")
        {
            MessageBox.Show("Hello From Python");
        }

        public static void csharp(string sender = "null")
        {
            MessageBox.Show("Hello From csharp");
        }

        public static void bphp(string sender = "null")
        {
            MessageBox.Show("Hello From bihongophp");
        }




        //This method will be start first
        public void Init()
        {
           /* SetExt.Add(".css", "css.xml");
            SetExt.Add(".jsx", "jsx.xml"); */
   
        }

        public void Start()
        {
            Form fm = new Form();
            fm.Text = Name;

            //browser
            WebBrowser browser = new WebBrowser();
            // 
            // webBrowser1
            // 
            browser.Dock = System.Windows.Forms.DockStyle.Fill;
            browser.Location = new System.Drawing.Point(0, 0);
            browser.MinimumSize = new System.Drawing.Size(20, 20);
            browser.Name = "webBrowser1";
            browser.Size = new System.Drawing.Size(445, 423);
            browser.TabIndex = 0;
            browser.DocumentText = GetEditorText;
            // 
            // Form2
            // 
            fm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            fm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            fm.ClientSize = new System.Drawing.Size(445, 423);
            fm.Controls.Add(browser);
            fm.Name = "Form2";
            fm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            fm.Text = "Html Viewer";
            fm.ResumeLayout(false);

            //End browser
            fm.Show();
        }



    }
}
