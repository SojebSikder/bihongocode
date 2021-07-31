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

namespace packagemanager
{
    public class Pm : StandardIO
    {
        //Get Editor File address where file have been saved
        public string FileAddress { get; set; }

        private TextBox txtInput;
        private Button btnExe;
        private Label label1;
        private Label label2;

        public string Name
        {
            get
            {
                return "Package Manager";
            }
        }

        public string[] position
        {
            get
            {
                return new string[] {
                    MenuPosition.Tools.ToString()
                };
            }
        }


        public void Start()
        {
            Form form1 = new Form();
            form1.ShowIcon = false;
            form1.StartPosition = FormStartPosition.CenterScreen;

            txtInput = new TextBox();
            btnExe = new Button();
            label1 = new Label();
            label2 = new Label();
            form1.SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.Font = new Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txtInput.Location = new Point(131, 59);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(392, 22);
            txtInput.TabIndex = 0;
            txtInput.KeyUp += new KeyEventHandler(this.txtInput_KeyUp);
            // 
            // btnExe
            // 
            btnExe.FlatStyle = FlatStyle.Flat;
            btnExe.Location = new Point(247, 100);
            btnExe.Name = "btnExe";
            btnExe.Size = new Size(92, 25);
            btnExe.TabIndex = 1;
            btnExe.Text = "Execute";
            btnExe.UseVisualStyleBackColor = true;
            btnExe.Click += new EventHandler(this.btnExe_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new Point(193, 9);
            label1.Name = "label1";
            label1.Size = new Size(187, 25);
            label1.TabIndex = 2;
            label1.Text = "Package Manager";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(24, 62);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(104, 13);
            label2.TabIndex = 3;
            label2.Text = "Write Argument here";
            // 
            // cmd
            // 
            form1.AutoScaleDimensions = new SizeF(6F, 13F);
            form1.AutoScaleMode = AutoScaleMode.Font;
            form1.ClientSize = new Size(549, 150);
            form1.Controls.Add(label2);
            form1.Controls.Add(label1);
            form1.Controls.Add(btnExe);
            form1.Controls.Add(txtInput);
            form1.Name = "cmd";
            form1.Text = "cmd";
            form1.ResumeLayout(false);
            form1.PerformLayout();

            form1.ShowDialog();
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            runCommand();
        }



        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                runCommand();

            }
        }

        public void runCommand()
        {

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.WorkingDirectory = @"C:\";
            startInfo.Arguments = "/c "+ txtInput.Text;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            txtInput.Text = "";

            //if (File.Exists("lib\\bihongocmd\\phpcmd.exe"))
            //{
            //    Process.Start("lib\\bihongocmd\\phpcmd.exe", FileAddress+"\\"+txtInput.Text);
            //    txtInput.Text = "";
            //}
            //else
            //{
            //    MessageBox.Show("Package Manager not found :)");
            //}
        }


    }
}
