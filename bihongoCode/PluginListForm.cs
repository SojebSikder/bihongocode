using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using bihongoCode.library;
using bihongoPlugin;
using SimplePlugin;
namespace bihongoCode
{
    public partial class PluginListForm : Form
    {
        public PluginListForm()
        {
            InitializeComponent();
        }


        private void PluginListForm_Load(object sender, EventArgs e)
        {
            var i = 0;
            foreach (var item in PluginUtility.PluginList())
            {
                i++;
                PluginUtility._StandardIOPlugins.Add(item.Name, item);
                richTextBox1.Text += i+"." + item.Name + " - " + PluginUtility.getPlugin_Property(item, "Version") + " " + PluginUtility.getPlugin_Property(item, "Description") + "\n";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
