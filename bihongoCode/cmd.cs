using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.IO;

namespace bihongoCode
{
    public partial class cmd : Form
    {
        public cmd()
        {
            InitializeComponent();
        }

        private void btnExe_Click(object sender, EventArgs e)
        {
            if (File.Exists("lib\\bihongocmd\\phpcmd.exe"))
            {
                Process.Start("lib\\bihongocmd\\phpcmd.exe", txtInput.Text);
                txtInput.Text = "";
            }
            else
            {
                MessageBox.Show("Package Manager not found :)");
            }
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter){
                if (File.Exists("lib\\bihongocmd\\phpcmd.exe"))
                {
                    Process.Start("lib\\bihongocmd\\phpcmd.exe", txtInput.Text);
                    txtInput.Text = "";
                }
                else
                {
                    MessageBox.Show("Package Manager not found :)");
                }
            }
        }

        private void cmd_Load(object sender, EventArgs e)
        {

        }
    }
}
