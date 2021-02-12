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

        public void SetKeywordByExt()
        {
            var data = new Dictionary<String,String>();
            data.Add(".py", "\\keywords\\python.xml");
        }

        //This method will be start first
        public void Init()
        {
            MessageBox.Show("Initiated... ");
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


        public string[] position
        {
            get {
                return new string[]{
                    MenuPosition.Toolbar.ToString()
                };
            }
        }
    }
}
