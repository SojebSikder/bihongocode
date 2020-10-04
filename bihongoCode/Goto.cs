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
    public partial class Goto : Form
    {
        RichTextBox texteditor;
        public Goto(RichTextBox tx)
        {
            InitializeComponent();
            texteditor = tx;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Goto_Load(object sender, EventArgs e)
        {
            RichTextBox rtb = new RichTextBox();
            rtb.Text = texteditor.Text;
            int lines = rtb.Lines.Length;
            label1.Text = "Enter Line Number (1-" + lines.ToString() + ") :";

            //int sel = texteditor.ActiveTextAreaControl.TextArea.Caret.Line + 1;
            int sel = texteditor.SelectionStart + 1;
            txtLine.Text = sel.ToString();
        }

        private void btnGoto_Click(object sender, EventArgs e)
        {
            int line = Int32.Parse(txtLine.Text);
            //texteditor.ActiveTextAreaControl.TextArea.Caret.Line = line - 1;
            //texteditor.ScrollToCaret();
           // texteditor.Lines = new string[] { line.ToString()};
            this.Close();
        }

        private void txtLine_TextChanged(object sender, EventArgs e)
        {
            int sel;
            RichTextBox rtb = new RichTextBox();
            rtb.Text = texteditor.Text;
            int lines = rtb.Lines.Length;

            if (txtLine.Text == "")
            {
                btnGoto.Enabled = false;
            }
            else if (!int.TryParse(txtLine.Text, out sel))
            {
                btnGoto.Enabled = false;
            }
            else if (Int32.Parse(txtLine.Text) > rtb.Lines.Length)
            {
                btnGoto.Enabled = false;
            }
            else if (txtLine.Text == "0")
            {
                btnGoto.Enabled = false;
            }
            else
            {
                btnGoto.Enabled = true;
            }
        }
    }
}
