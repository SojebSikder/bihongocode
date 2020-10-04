using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.IO;
using bihongoPlugin;

namespace testPlugin
{
    public class testPlugin
    {
        public class Plugin : StandardIO
        {
            #region IPlugin Members

            public string GetEditorText { get; set; }

            public string Name
            {
                get
                {
                    return "Dev Plugin";
                }
            }

            public string[] position
            {

                get
                {
                    return new string[] {
                        MenuPosition.Toolbar.ToString()
                    };
                }
            }

            public string[] EventRegister
            {
                get
                {
                    return new string[] {
                        "Do",
                        "work"
                    };
                }
            }

            public void work()
            {
                MessageBox.Show(GetEditorText);
            }
            public void Do()
            {
                MessageBox.Show("im do");
            }



            public void TextValue(string test)
            {
                GetEditorText = test;

            }

            
            public void Export()
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(sfd.FileName, FileMode.CreateNew))
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(GetEditorText);

                    }
                }

            }
             


            public void Start()
            {

                Form fm = new Form();
                fm.Text = Name;

                //Button
                Button btn = new Button();
                btn.Height = 23;
                btn.Width = 100;
                btn.Location = new Point(10, 10);
                btn.Text = "Set Name";
                btn.Name = Name;
                btn.Font = new Font("Georgia", 9);
                btn.Click += new EventHandler(btn_Click);
                //End Button

                fm.Controls.Add(btn);
                fm.Show();
            }


            public void btn_Click(object sender, EventArgs e)
            {
                MessageBox.Show("txt: " + GetEditorText, "title");
            }

            #endregion
        }
    }
}
