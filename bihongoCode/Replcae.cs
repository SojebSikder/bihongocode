using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bihongoCode
{
    public partial class Replcae : Form
    {
        RichTextBox texteditor;
        public Replcae(RichTextBox tx)
        {
            InitializeComponent();
            texteditor = tx;
        }

        private void Replcae_Load(object sender, EventArgs e)
        {
            btnReplaceAll.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text == "" || txtReplace.Text == "")
            {
                btnReplaceAll.Enabled = false;
            }
            else
            {
                btnReplaceAll.Enabled = true;
            }
        }

        private void txtReplace_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text == "" || txtReplace.Text == "")
            {
                btnReplaceAll.Enabled = false;
            }
            else
            {
                btnReplaceAll.Enabled = true;
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            String findtext = txtFind.Text;
            String replacetext = txtReplace.Text;
            if (findtext != "" && replacetext != "")
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Text = texteditor.Text;
                rtb.Text = rtb.Text.Replace(findtext, replacetext);
                texteditor.Text = rtb.Text;
            }
        }

    }
}
