using bihongoCode.Properties;
using CodeCompletion_CSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bihongoPlugin;
using SimplePlugin;

using System.Diagnostics;
using System.Threading;
using System.Reflection;
using bihongoCode.library;


namespace bihongoCode
{
    public partial class Form1 : Form
    {

        #region variables

        /// <summary>
        /// Controls
        /// </summary>
        TabPage tp;

        Color backColor = Color.FromArgb(40, 41, 35); //#282923 //0, 64, 128
        Color foreColor = Color.White;

        //Info
        ComponentResourceManager resources = new ComponentResourceManager(typeof(Form1));
        //File Info
        public string FileAddress;
        public string FileExtension = ".txt";
        public string pluginName;

        // Add set of extension with keywords
        public Dictionary<string, string> KeywordWithExt = new Dictionary<string, string>();

        string paths = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        bool isArg = false;

        /// <summary>
        /// Plugin event component
        /// </summary>
        public enum PluginComponent
        {   
            /// <summary>
            /// Used for toolstrip
            /// </summary>
            ToolStripButton,
            /// <summary>
            /// Used for tollsript item
            /// </summary>
            ToolStripMenuItem
        }
        #endregion

        #region function

        /// <summary>
        /// Load all plugins
        /// </summary>
        public void LoadPlugin()
        {
            //.....................Plugin..........System..........Code...........
            //_StandardIOPlugins = new Dictionary<string, StandardIO>();
            //ICollection<StandardIO> StandardIOPlugins = PluginLoader.LoadDevPlugins("Plugins");


            int btnPosition = 400;
            foreach (var item in PluginUtility.PluginList())
            {
                btnPosition = btnPosition + 10;

                PluginUtility._StandardIOPlugins.Add(item.Name, item);


                for (int i = 0; i < item.position.Length; i++)
                {
                    //For Toolbar
                    ToolStripButton toolstripbtn = new ToolStripButton();
                    toolstripbtn.Text = item.Name;
                    toolstripbtn.Name = item.Name + btnPosition;
                    toolstripbtn.Click += toolstrip_Click;

                    toolstripbtn.ForeColor = Color.White;
                    toolstripbtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
                    toolstripbtn.Image = ((Image)(resources.GetObject("toolStripButton1.Image")));
                    toolstripbtn.ImageTransparentColor = System.Drawing.Color.Magenta;
                    toolstripbtn.Size = new System.Drawing.Size(23, 22);
                    //End Toolbar


                    //For Menu Item
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.ForeColor = Color.Black;
                    menuItem.Text = item.Name;
                    menuItem.Name = item.Name + btnPosition;
                    menuItem.Click += menuItem_Click;
                    //End Menu Item

                    //for (int i = 0; i < item.position.Length; i++)
                    //  {


                    switch (item.position[i])
                    {
                        case "File":
                            fileToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Edit":
                            editToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Format":
                            formatToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Tools":
                            toolsToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Settings":
                            settingsToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Help":
                            helpToolStripMenuItem.DropDownItems.Add(menuItem);
                            break;
                        case "Toolbar":
                            toolStrip1.Items.AddRange(new ToolStripItem[] { toolstripbtn });
                            break;

                    }

                    // }

                }
            }
        }
        /// <summary>
        /// load plugin init Method
        /// </summary>
        public void loadInit()
        {
            //loading library for name
            string[] dllFileNames = Directory.GetFiles("plugins", "*.dll");
            foreach (string dllFile in dllFileNames)
            {
                AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                Assembly assembly = Assembly.Load(an);
                foreach (var t in assembly.GetTypes())
                {
                    pluginName = t.Name;

                    InitPlugin();
                    SetKeywordWithExt();
                    
                   
                }
            }
            //ending
        }

        /// <summary>
        /// Set language specific syntax
        /// </summary>
        public void SetKeywordWithExt()
        {
            if (PluginUtility._StandardIOPlugins.ContainsKey(pluginName))
            {
                StandardIO item = PluginUtility._StandardIOPlugins[pluginName];
                if (PluginUtility.getPlugin_Property(item, "SetExt") != null)
                {
                    var extProperty = PluginUtility.getPlugin_Property(item, "SetExt");

                    //spliting string with separator
                    char[] separatorExtSet = { '|' };
                    string[] allExt = extProperty.Split(separatorExtSet, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var ext in allExt)
                    {
                        char[] separatorExt = { '@' };
                        string[] ExtSet = ext.Split(separatorExt, StringSplitOptions.RemoveEmptyEntries);
                        KeywordWithExt.Add(ExtSet[0], ExtSet[1]);

                    }

                }


            }
        }
        /// <summary>
        /// Invoke plugin initPlugin Method
        /// </summary>
        public void InitPlugin()
        {
            //  Button b = sender as Button;

            //....Dev Plugin...toolstrip code.
            string dkey = pluginName;
            if (PluginUtility._StandardIOPlugins.ContainsKey(dkey))
            {

                StandardIO dplugin = PluginUtility._StandardIOPlugins[dkey];
                //dplugin.Start();

                dynamic devType = dplugin.GetType();
                dynamic dev = Activator.CreateInstance(devType);


                dynamic methodStart = devType.GetMethod("Init");
                methodStart.Invoke(dev, new object[] { });


                dynamic actionProperty = devType.GetProperty("command");
                if (actionProperty != null)
                {
                    Dictionary<string, Action<string>> actionVal = actionProperty.GetValue(dev);
                    foreach (var action in actionVal)
                    {
                        //action.Value.DynamicInvoke("");
                        char[] separator = { ' '};
                        string[] actionArray = action.Key.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        if (actionArray[0] == "create")
                        {
                            if (actionArray[1] == "menu")
                            {
                                ToolStripMenuItem topStripMenuItem = new ToolStripMenuItem();
                              
                                topStripMenuItem.Name = "topToolStripMenuItem";
                                //newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
                                newToolStripMenuItem.Text = actionArray[2];
                                newToolStripMenuItem.Click += new EventHandler(this.newToolStripMenuItem_Click);

                                menuStrip.Items.AddRange(new ToolStripItem[] {
                                    topStripMenuItem

                                });
                                Console.WriteLine(actionArray[2]);
                            }

                        }
                    }
                }

                //MessageBox.Show(eventval);
            }
            //...End dev...


        }
        //end plugin helper method

        public void LoadSyntax()
        {
            //initializing syntax
            string[] xmlFileNames = Directory.GetFiles("keywords", "*.xml");
            foreach (var item in xmlFileNames)
            {
                ToolStripMenuItem syntax = new ToolStripMenuItem();
                FileInfo fileInfo = new FileInfo(item);

                syntax.Text = fileInfo.Name;
                syntax.Click += syntax_Click;
                syntaxToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { syntax });
            }
        }



        //library function
        /// <summary>
        /// Create new tab page
        /// </summary>
        public void newpage(string name = "New Document")
        {
            tp = new TabPage(name);
            CCRichTextBoxXML rtb = new CCRichTextBoxXML();

            rtb.HideSelection = false;
            rtb.AcceptsTab = true;
            rtb.SelectionFont = new Font("Arial", 12, FontStyle.Bold);
            rtb.ContextMenuStrip = contexTab;
            rtb.ForeColor = (Color)Settings.Default["forecolor"];
            rtb.BackColor = (Color)Settings.Default["backcolor"]; // Color.CadetBlue;
            rtb.Dock = DockStyle.Fill;


            tp.Controls.Add(rtb);
            tabControl1.TabPages.Add(tp);

            tabControl1.SelectTab(tp);
            // setting droping text file
            GetRichTextBox().AllowDrop = true;
            GetRichTextBox().DragDrop += new DragEventHandler(GetRichTextBox_DragDrop);
            //end that
            GetRichTextBox().KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
            GetRichTextBox().KeyPress += new KeyPressEventHandler(richTextBox1_KeyPress);

            //for linenumbers
            GetRichTextBox().SelectionChanged += new EventHandler(richTextBox1_SelectionChanged);
            GetRichTextBox().VScroll += new EventHandler(richTextBox1_VScroll);
            GetRichTextBox().TextChanged += new EventHandler(richTextBox1_TextChanged);
            GetRichTextBox().FontChanged += new EventHandler(richTextBox1_FontChanged);
        }
        void GetRichTextBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileNames = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (fileNames != null)
            {
                foreach (string name in fileNames)
                {
                    try
                    {
                        FileInfo fi = new FileInfo(name);
                        newpage(fi.Name);
                        GetRichTextBox().AppendText(File.ReadAllText(name));
                        FileAddress = name;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = GetRichTextBox().GetPositionFromCharIndex(GetRichTextBox().SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            }
        }
        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            LineNumberTextBox.Text = "";
            AddLineNumbers();
            LineNumberTextBox.Invalidate();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (GetRichTextBox().Text == "")
            {
                AddLineNumbers();
            }
        }
        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
            LineNumberTextBox.Font = GetRichTextBox().Font;
            GetRichTextBox().Select();
            AddLineNumbers();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();
        }
        private void LineNumberTextBox_MouseDown(object sender, MouseEventArgs e)
        {
            GetRichTextBox().Select();
            LineNumberTextBox.DeselectAll();
        }
        // end line numbers
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            String s = e.KeyChar.ToString();
            int sel = GetRichTextBox().SelectionStart;

            // if (checkBox1.Checked == true){
            switch (s)
            {

                case "(":
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, "()");
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + 1;
                    break;

                case "{":
                    String t = "{}";
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, t);
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + t.Length - 1;
                    isCurslyBracesKeyPressed = true;
                    break;

                case "<":
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, "<>");
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + 1;
                    break;

                case "\"":
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, "\"\"");
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + 1;
                    break;

                case "'":
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, "''");
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + 1;
                    break;
            }
            // }
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            int sel = GetRichTextBox().SelectionStart;
            if (e.KeyCode == Keys.Enter)
            {
                if (isCurslyBracesKeyPressed == true)
                {
                    GetRichTextBox().Text = GetRichTextBox().Text.Insert(sel, "\n  \n");
                    e.Handled = true;
                    GetRichTextBox().SelectionStart = sel + " ".Length;
                    isCurslyBracesKeyPressed = false;
                }
            }
        }
        public CCRichTextBoxXML GetRichTextBox()
        {
            //CCRichTextBox rtb = new CCRichTextBox();
            CCRichTextBoxXML rtb = new CCRichTextBoxXML();
            //RichTextBox rtb = null;
            TabPage tp = tabControl1.SelectedTab;


            if (tp != null)
            {
                rtb = tp.Controls[0] as CCRichTextBoxXML;
            }

            return rtb;
        }
        /*  public RichTextBox GetRichTextBox()
          {
              RichTextBox rtb = null;
              TabPage tp = tabControl1.SelectedTab;


              if (tp != null)
              {
                  rtb = tp.Controls[0] as RichTextBox;
              }

              return rtb;
          }*/
        //end library function

        //Code for Line Numbers
        public int getWidth()
        {
            int w = 25;
            //get totallines of richtextbox1
            int line = GetRichTextBox().Lines.Length;

            if (line <= 00)
            {
                w = 20 + (int)GetRichTextBox().Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)GetRichTextBox().Font.Size;
            }
            else
            {
                w = 50 + (int)GetRichTextBox().Font.Size;
            }
            return w;
        }

        public void AddLineNumbers()
        {
            //Create & set Point pt to (0,0)
            Point pt = new Point(0, 0);
            //get First Index & First Line from richTextBox1
            int First_Index = GetRichTextBox().GetCharIndexFromPosition(pt);
            int First_Line = GetRichTextBox().GetLineFromCharIndex(First_Index);
            // set X & Y coordinates of Point pt to ClientRectangle Width Height respectively
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;
            // get Last Index & Last Line from richTextBox1
            int Last_Index = GetRichTextBox().GetCharIndexFromPosition(pt);
            int Last_Line = GetRichTextBox().GetLineFromCharIndex(Last_Index);
            //set center alignment to LineNumberTextBox
            LineNumberTextBox.SelectionAlignment = HorizontalAlignment.Center;
            // set LineNumberTextBox text to null & width to getWidthfunction value
            LineNumberTextBox.Text = "";
            LineNumberTextBox.Width = getWidth();
            // now add each line number to LineNumberTextBox upto last line
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                LineNumberTextBox.Text += i + 1 + "\n";
            }
        }

        //End Code for LineNumbers
        #endregion

        public Form1()
        {
            InitializeComponent();
            //load plugin
            LoadPlugin();
            //loadInit();
        }

        public void LoadFile(string openWith)
        {
            FileAddress = openWith;
            string strfilename = openWith;
            FileInfo fileInfo = new FileInfo(strfilename);
            FileExtension = fileInfo.Extension;

            newpage(fileInfo.Name);
            changeExt(FileExtension);
            string filetext = File.ReadAllText(strfilename);
            GetRichTextBox().Text = filetext;

            isArg = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FileAssoc.SetAssociation(".ucs", "UCS_Editor_File", Application.ExecutablePath, "UCS File"); 

            //Create new tabpage with richtextbox
            if (isArg == false)
            {
                newpage();
            }

            //GetRichTextBox().keywordUrl = "\\keywords\\c_sharp.xml";
            GetRichTextBox().Focus();

            if (WordWrapBool == true)
            {
                wordWrapToolStripMenuItem.Checked = true;
                GetRichTextBox().WordWrap = true;
                WordWrapBool = false;
            }

            //initializing syntax
            LoadSyntax();
            //end syntax

            //loading file and folders
            //string[] dirs = Directory.GetFiles("keywords", "*.*");
            string[] dirs = Directory.GetDirectories("keywords", "*.*", SearchOption.AllDirectories);
            int i = 0;
            foreach (var dir in dirs)
            {
                i = i + 30;
                Button btn = new Button();
                FileInfo fileInfo = new FileInfo(dir);
                btn.Text = fileInfo.Name;
                btn.ForeColor = Color.White;
                btn.Location = new Point(10, i);
                panel.Panel1.Controls.AddRange(new Button[] { btn });

            }
            //end loading

            //init line numnbers
            LineNumberTextBox.Font = GetRichTextBox().Font;
            GetRichTextBox().Select();
            AddLineNumbers();

            

            //invoke plugin init method
            loadInit();

            // Add extension with keywords
            KeywordWithExt.Add(".php", "php.xml");
            KeywordWithExt.Add(".cs", "cs.xml");
            KeywordWithExt.Add(".html", "html.xml");
            KeywordWithExt.Add(".js", "js.xml");
            KeywordWithExt.Add(".py", "py.xml");

            toolstatus.Text = "Ready";
            SetAfterExtChange();
        }

        /// <summary>
        /// After changing language do some task
        /// </summary>
        public void SetAfterExtChange()
        {
            // Change lang status
            string input = GetRichTextBox().keywordUrl;
            if (input != null)
            {
                int index = input.IndexOf(".xml");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                    toolStripStatusLang.Text = input;
                }
            }
            else
            {
                toolStripStatusLang.Text = "";
            }
        }

        /// <summary>
        /// Execute something after file saved
        /// </summary>
        public void ExecuteAfterFileSaved()
        {

        }


        /// <summary>
        /// Used for plugin event
        /// </summary>
        public void Plugin_Event(object sender, EventArgs e, PluginComponent component)
        {
            /**
             * Toolstrip Section
             */
            if(component == PluginComponent.ToolStripButton)
            {
                ToolStripButton toolstripbtn = sender as ToolStripButton;
            
                if (toolstripbtn != null)
                {
                    //....Dev Plugin...toolstrip code.
                    string dkey = toolstripbtn.Text.ToString();
                    Plugin_Common(dkey);
                }
                //End ToolStrip Section
            }
            else if (component == PluginComponent.ToolStripMenuItem)
            {
                ToolStripMenuItem toolstripbtn = sender as ToolStripMenuItem;

                if (toolstripbtn != null)
                {
                    //....Dev Plugin...toolstrip code.
                    string dkey = toolstripbtn.Text.ToString();
                    Plugin_Common(dkey);
                }
                //End ToolStrip Section
            }


        }

        /// <summary>
        /// Plugin Common Code
        /// </summary>
        public void Plugin_Common(string dkey)
        {
            if (PluginUtility._StandardIOPlugins.ContainsKey(dkey))
            {
                //get GetRichTextBox() value
                string editorValue = GetRichTextBox().Text;

                StandardIO dplugin = PluginUtility._StandardIOPlugins[dkey];
                //dplugin.Start();

                dynamic devType = dplugin.GetType();
                dynamic dev = Activator.CreateInstance(devType);

                dynamic property = devType.GetProperty("GetEditorText");
                if(property != null){
                    property.SetValue(dev, editorValue);
                }
                

                dynamic eventProperty = devType.GetProperty("GetEditorText");
                if(eventProperty != null){
                    dynamic eventval = eventProperty.GetValue(dev);
                }
                

                //setting file extension
                dynamic extension = devType.GetProperty("FileExtension");
                if (extension != null)
                {
                    extension.SetValue(dev, FileExtension);
                }

                //dynamic ext = extension.GetValue(dev);
                //setting file address
                if (String.IsNullOrEmpty(FileAddress))
                {
                }
                else
                {
                    try
                    {
                        string getileurl = FileAddress.ToString();
                        dynamic fileurl = devType.GetProperty("FileAddress");

                        if (fileurl != null)
                        {
                            fileurl.SetValue(dev, getileurl);
                        }
                        //dynamic Getfileurl = fileurl.GetValue(dev);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }



                dynamic methodStart = devType.GetMethod("Start");
                methodStart.Invoke(dev, new object[] { });

                // invoke custom method
                dynamic actionProperty = devType.GetProperty("command");
                if (actionProperty != null)
                {
                    Dictionary <string, Action <string>> actionVal = actionProperty.GetValue(dev);
                    foreach (var action in actionVal)
                    {
                       action.Value.DynamicInvoke("");
                    }
                }


                //MessageBox.Show(eventval);
            }
        }
        
        /**
         * ToolStrip Event Click
         */
        void toolstrip_Click(object sender, EventArgs e)
        {
            Plugin_Event(sender, e, PluginComponent.ToolStripButton);
        }

        /**
         * MenuItem Event Clicks
         */
        void menuItem_Click(object sender, EventArgs e)
        {
            Plugin_Event(sender, e, PluginComponent.ToolStripMenuItem);
        }

        public static Boolean isCurslyBracesKeyPressed = false;


        #region menu
        void syntax_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem c = sender as ToolStripMenuItem;

            if (c != null)
            {
                string input = c.Text;
                int index = input.IndexOf(".xml");
                if (index > 0)
                {
                    input = input.Substring(0, index);
                }

                input = input.Insert(0, ".");

                if (KeywordWithExt.ContainsKey(input))
                {
                    GetRichTextBox().keywordUrl = KeywordWithExt[input].ToString();
                    SetAfterExtChange();
                }
                
            }

        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newpage();
        }

        // Change file extension with keywords
        public void changeExt(string ext)
        {

            if(KeywordWithExt.ContainsKey(ext)){
                GetRichTextBox().keywordUrl = KeywordWithExt[ext].ToString();
                SetAfterExtChange();
            }

            
         /*   if (ext == ".php")
            {
                GetRichTextBox().keywordUrl = "\\keywords\\php.xml";
            }
            else if (ext == ".cs")
            {
                GetRichTextBox().keywordUrl = "\\keywords\\c_sharp.xml";
            }
            else if (ext == ".html")
            {
                GetRichTextBox().keywordUrl = "\\keywords\\html.xml";
            }
            else if (ext == ".js")
            {
                GetRichTextBox().keywordUrl = "\\keywords\\javascript.xml";
            }
            else if (ext == ".py")
            {
                GetRichTextBox().keywordUrl = "\\keywords\\python.xml";
            } 
          * */

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            Stream myStream;
            ofd.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = ofd.OpenFile()) != null)
                {
                    FileAddress = ofd.FileName;
                    string strfilename = ofd.FileName;
                    FileInfo fileInfo = new FileInfo(strfilename);
                    FileExtension = fileInfo.Extension;

                    newpage(fileInfo.Name);
                    changeExt(FileExtension);
                    string filetext = File.ReadAllText(strfilename);
                    GetRichTextBox().Text = filetext;
                }
                myStream.Close();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
         /*   if (FileAddress != null)
            {
                using (Stream s = File.Open(FileAddress, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(GetRichTextBox().Text);

                    FileInfo fileInfo = new FileInfo(FileAddress);
                    FileExtension = fileInfo.Extension;
                    changeExt(FileExtension);
                }
            }
            else
            {
                Stream s;
                StreamWriter sw;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (s = File.Open(sfd.FileName, FileMode.Create))
                    using (sw = new StreamWriter(s))
                    {
                        FileAddress = sfd.FileName;
                        sw.Write(GetRichTextBox().Text);

                        FileInfo fileInfo = new FileInfo(sfd.FileName);
                        FileExtension = fileInfo.Extension;
                        changeExt(FileExtension);
                    }
                    sw.Close();
                    s.Close();

                }
            }
          * */
        }


        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(sfd.FileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    FileAddress = sfd.FileName;
                    sw.Write(GetRichTextBox().Text);

                    FileInfo fileInfo = new FileInfo(sfd.FileName);
                    FileExtension = fileInfo.Extension;
                    changeExt(FileExtension);

                    // Set tab name
                    tp.Text = fileInfo.Name;

                    ExecuteAfterFileSaved();
                }
            }
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectAll();
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Clear();
        }

        private void clearClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        bool WordWrapBool = true;
        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WordWrapBool == false)
            {

                wordWrapToolStripMenuItem.Checked = false;
                GetRichTextBox().WordWrap = false;

                WordWrapBool = true;
            }
            else if (WordWrapBool == true)
            {

                wordWrapToolStripMenuItem.Checked = true;
                GetRichTextBox().WordWrap = true;

                WordWrapBool = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = GetRichTextBox().SelectionFont;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                GetRichTextBox().SelectionFont = fd.Font;

                Settings.Default["font"] = fd.Font;
                Settings.Default.Save();
            }
        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cr = new ColorDialog();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                GetRichTextBox().BackColor = cr.Color;

                Settings.Default["backcolor"] = cr.Color;
                Settings.Default.Save();
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cr = new ColorDialog();
            if (cr.ShowDialog() == DialogResult.OK)
            {
                GetRichTextBox().ForeColor = cr.Color;

                Settings.Default["forecolor"] = cr.Color;
                Settings.Default.Save();
            }
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().BackColor = backColor;
            GetRichTextBox().ForeColor = foreColor;


            Settings.Default["font"] = new Font("Microsoft Sans Serif", 16);

            Settings.Default.Reset();
            Settings.Default.Save();
        }

        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newpage();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectAll();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Clear();
        }

        private void toolSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && toolSearchText.TextLength > 0)
            {
                int index = 0; string temp = GetRichTextBox().Text; GetRichTextBox().Text = ""; GetRichTextBox().Text = temp;
                while (index < GetRichTextBox().Text.LastIndexOf(toolSearchText.Text))
                {
                    GetRichTextBox().Find(toolSearchText.Text, index, GetRichTextBox().TextLength, RichTextBoxFinds.None);
                    GetRichTextBox().SelectionBackColor = Color.Red;
                    index = GetRichTextBox().Text.IndexOf(toolSearchText.Text, index) + 1;
                }
            }
            else if (toolSearchText.TextLength == 0)
            {
                GetRichTextBox().SelectionBackColor = Color.Teal;
            }
        }


        private void newKeywordsDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FileInfo fileInfo = new FileInfo("keywords\\keywords.xml");
            string filetext = File.ReadAllText("keywords\\keywords.xml");

            newpage(fileInfo.Name);
            GetRichTextBox().Text = filetext;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutBox = new About();
            aboutBox.ShowDialog();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Replcae rp = new Replcae(GetRichTextBox());
            rp.Show();
        }

        private void findToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Find fd = new Find(GetRichTextBox());
            fd.Show();
        }

        private void gotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Goto go = new Goto(GetRichTextBox());
            go.Show();
        }


        private void pluginListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PluginListForm().Show();
        }

        #endregion





    }
}
