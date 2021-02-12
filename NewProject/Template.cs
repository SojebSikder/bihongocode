using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using bihongoPlugin;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace NewProject
{
    public class Template : StandardIO
    {
        //Get editor text
        public string GetEditorText { get; set; }
        //Get Editor File address where file have been saved
        public string FileAddress { get; set; }
        //Get Editor file extension
        public string FileExtension { get; set; }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Description
        {
            get
            {
                return "Project Chooser";
            }
        }

        public string Name
        {
            get { return "New Project"; }
        }

        public void Start()
        {
            initGui();
        }

        public string[] position
        {
            get
            {
                return new string[]
                { 
                    MenuPosition.File.ToString()
                };
            }
        }

        public void initGui()
        {
            Form fm = new Form();
            fm.Text = Name;
            // 
            // Form2
            // 
            fm.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            fm.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            fm.ClientSize = new System.Drawing.Size(445, 423);
            //fm.Controls.Add(browser);
            fm.Name = "Form2";
            fm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            fm.Text = "New Project";
            fm.ResumeLayout(false);

            //End browser
            fm.Show();
        }
    }
}
