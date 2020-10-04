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
    public partial class Find : Form
    {
        RichTextBox texteditor;
        public Find(RichTextBox tx)
        {
            InitializeComponent();
            texteditor = tx;
        }

        RichTextBox rtb = new RichTextBox();
        RichTextBox richTextBox1 = new RichTextBox();
        RichTextBox richTextBox2 = new RichTextBox();

        //**************************************************************************************************************
        //     GetLines()
        //**************************************************************************************************************
        void GetLines()
        {
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            int i;
            rtb.Text = texteditor.Text;
            String s = txtFind.Text;
            String[] lines = rtb.Lines;
            for (i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(s))
                {
                    int b = i + 1;
                    richTextBox1.Text = richTextBox1.Text.Insert(0, "" + b.ToString() + "\n");
                }
            }
            String[] lines2 = richTextBox1.Lines;
            for (int j = 0; j < lines2.Length; j++)
            {
                if (lines2[j] == "") { }
                else
                {
                    richTextBox2.Text = richTextBox2.Text.Insert(0, "" + lines2[j] + "\n");
                }
            }
        }

        static int a = 0;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            a = 0;
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            a = 0;
            label2.Text = "";
            if (txtFind.Text == "")
            {
                btnFind.Enabled = false;
            }
            else
            {
                btnFind.Enabled = true;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFind.Text == "")
            {
                btnFind.Enabled = false;
            }
            else
            {
                btnFind.Enabled = true;
                try
                {
                    GetLines();
                    String[] lines = richTextBox2.Lines;
                    if (lines[a] == "")
                    {
                    }
                    else
                    {
                        int line = Convert.ToInt32(lines[a]);
                        //texteditor.ActiveTextAreaControl.TextArea.Caret.Line = line - 1;
                        label2.Text = "Found at Line No : " + (line).ToString();
                        a = a + 1;
                    }
                }
                catch { }
            }
        }

        private void Find_FormClosing(object sender, FormClosingEventArgs e)
        {
            a = 0;
        }
    }
}
