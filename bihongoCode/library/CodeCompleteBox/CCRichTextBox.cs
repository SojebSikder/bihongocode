using System;
using System.Drawing;
using System.Windows.Forms;
namespace CodeCompletion_CSharp
{

    public class CCRichTextBox : RichTextBox
    {

        //*********************************************************************
        //   declare ListBox object CodeCompleteBox
        //*********************************************************************
        public ListBox CodeCompleteBox = new ListBox();

        //*********************************************************************
        //   declare ToolTipControlPanel object ToolTipControl
        //*********************************************************************
        public ToolTipControlPanel ToolTipControl = new ToolTipControlPanel();


        //*********************************************************************
        //  declare static string EnteredKey variable
        //*********************************************************************
        public static String EnteredKey = "";

        public static Boolean isClassCreated = false;

        public static Boolean isDataTypeDeclared = false;

        public static Boolean isAutoCompleteBrackets = false;

        public static Boolean isToolTipControlAdded = false;

        // declare backcolor & forecolor variables
        public static Color backcolor = SystemColors.Window;
        public static Color forecolor = Color.Black;

        public CCRichTextBox()
        {

            //*********************************************************************
            //  set FixedSingle Border style to CodeCompleteBox
            //*********************************************************************
            CodeCompleteBox.BorderStyle = BorderStyle.Fixed3D;

            //***************************************************************************************
            //  Add KeyDown,KeyPress & MouseClick events to CodeCompleteBox
            //***************************************************************************************
            CodeCompleteBox.KeyDown += new KeyEventHandler(CodeCompleteBox_KeyDown);
            CodeCompleteBox.KeyUp += new KeyEventHandler(CodeCompleteBox_KeyUp);
            CodeCompleteBox.KeyPress += new KeyPressEventHandler(CodeCompleteBox_KeyPress);
            CodeCompleteBox.MouseClick += new MouseEventHandler(CodeCompleteBox_MouseClick);
        }


        //**************************************************************************
        // get & set backcolor & forecolor to CodeCompleteBox
        //**************************************************************************
        public Color CodeCompleBackColor
        {
            get { return backcolor; }
            set { backcolor = value; CodeCompleteBox.BackColor = value; Invalidate(); }
        }

        public Color CodeCompleForeColor
        {
            get { return forecolor; }
            set { forecolor = value; CodeCompleteBox.ForeColor = value; Invalidate(); }
        }



        //**********************************************************************************************
        //  getXYPoints() function that returns (x,y) co-ordinates taken from CCRichTextBox
        //  to set Location of CodeCompleteBox on CCRichTextBox
        //**********************************************************************************************
        public Point getXYPoints()
        {
            //get current caret position point from CCRichTextBox
            Point pt = this.GetPositionFromCharIndex(this.SelectionStart);
            // increase the Y co-ordinate size by 10 & Font size of CCRichTextBox
            pt.Y = pt.Y + (int)this.Font.Size + 10;

            //  check Y co-ordinate value is greater than CCRichTextBox Height - CodeCompleteBox
            //   for add CodeCompleteBox at the Bottom of CCRichTextBox
            if (pt.Y > this.Height - CodeCompleteBox.Height)
            {
                pt.Y = pt.Y - CodeCompleteBox.Height - (int)this.Font.Size - 10;
            }

            return pt;
        }


        //*********************************************************************************************************************
        //  getWidth() function returns specific width depending on the length of items in CodeCompleteBox
        //*********************************************************************************************************************
        public int getWidth()
        {
            int width = 100;

            foreach (String item in CodeCompleteBox.Items)
            {
                if (item.Length <= 5)
                {
                    width = 160;
                }
                else if (item.Length <= 10)
                {
                    width = 200;
                }
                else if (item.Length <= 20)
                {
                    width = width + item.Length * 2;
                }
                else
                {
                    width = width + item.Length * 4;
                }
            }

            return width;
        }


        //*********************************************************************************************************************
        //  getHeight() function returns specific height depending on the number of items in CodeCompleteBox
        //*********************************************************************************************************************
        public int getHeight()
        {
            int height = 10;

            //  get Font size of CCRichTextBox
            int fontsize = (int)this.Font.Size;

            //  get number of items added to CodeCompleteBox
            int count = CodeCompleteBox.Items.Count;


            //   increase the height of CodeCompleteBox if added items count is 0,1,2,3,4,5
            switch (count)
            {
                case 0: height = fontsize;
                    break;
                case 1: height += 10 + fontsize;
                    break;
                case 2: height += 20 + fontsize;
                    break;
                case 3: height += 30 + fontsize;
                    break;
                case 4: height += 40 + fontsize;
                    break;
                case 5: height += 50 + fontsize;
                    break;
                case 6: height += 60 + fontsize;
                    break;
                case 7: height += 70 + fontsize;
                    break;
                case 8: height += 80 + fontsize;
                    break;
                case 9: height += 90 + fontsize;
                    break;
                case 10: height += 100 + fontsize;
                    break;
                case 11: height += 110 + fontsize;
                    break;
                case 12: height += 120 + fontsize;
                    break;
                case 13: height += 130 + fontsize;
                    break;
                case 14: height += 140 + fontsize;
                    break;
                case 15: height += 150 + fontsize;
                    break;
                default: height += 200 + fontsize;
                    break;

            }

            return height;
        }





        //**********************************************************************************************
        //  declare list of String keywordslist and adding keywords or any syntax to it
        //**********************************************************************************************
        public String[] keywordslist = {
"bool", 
"break", 
"case", 
"catch", 
"char", 
"class", 
"const", 
"continue", 
"default", 
"do", 
"double",
"else", 
"enum",  
"false", 
"float", 
"for", 
"goto", 
"if",  
"int", 
"long", 
"namespace", 
"new", 
"private", 
"protected", 
"public",  
"return", 
"short",  
"sizeof", 
"static",  
"struct", 
"switch", 
"this", 
"throw",
"true", 
"try", 
"typedof",
"using",
"virtual",
"void", 
"while",
"Form",
"Button",
"CheckBox",
"CheckedListBox",
"ColorDialog",
"ComboBox",
"ContextMenuStrip",
"DataGridView",
"DataSet",
"DataTimePicker",
"DirectoryEntry",
"DirectorySearcher",
"DomainUpDown",
"FlowLayoutPanel",
"FolderBrowserDialog",
"FontDialog",
"GroupBox",
"HelpProvider",
"HScrollBar",
"ImageList",
"Label",
"LinkLabel",
"ListBox",
"ListView",
"MaskedTextBox",
"MenuStrip",
"MessageQueue",
"MonthCalendar",
"NotifyIcon",
"NumericUpDown",
"OpenFileDialog",
"PageSetupDialog",
"Panel",
"PerformanceCounter",
"PictureBox",
"PrintDialog",
"PrintDocument",
"PrintPreviewControl",
"PrintPreviewDialog",
"Process",
"ProgressBar",
"PropertyGrid",
"RadioButton",
"RichTextBox",
"SaveFileDialog",
"SerialPort",
"ServiceController",
"SplitContainer",
"Splitter",
"StatusStrip",
"TabControl",
"TableLayoutPanel",
"TextBox",
"Timer",
"ToolStrip",
"ToolStripContainer",
"ToolTip",
"TrackBar",
"TreeView",
"VScrollBar",
"WebBrowser"
        };


        public String[] classeslist =
       {
           "Form",
           "Panel",
           "Button",
           "CheckBox"
       };


        public String[] datatypes =
       {
          "byte",
          "short",
          "ushort",
          "int",
          "uint",
          "long",
          "ulong",
          "float",
          "double",
          "decimal",
          "char",
          "bool"
       };



        public String[] KeywordsList
        {
            get { return keywordslist; }
            set { keywordslist = value; Invalidate(); }
        }


        public String[] ClassesList
        {
            get { return classeslist; }
            set { classeslist = value; Invalidate(); }
        }

        public String[] DataTypesList
        {
            get { return datatypes; }
            set { datatypes = value; Invalidate(); }
        }

        public Boolean AutoCompleteBrackets
        {
            get { return isAutoCompleteBrackets; }
            set { isAutoCompleteBrackets = value; Invalidate(); }
        }



        //****************************************************************************
        //  function of ProcessDeclaredClasses()
        //  if EnteredKey text is equal to class name defined in classeslist
        // then set isClassCreated to true
        //****************************************************************************
        public void ProcessDeclaredClasses(String input)
        {
            foreach (String item in classeslist)
            {
                if (item == input)
                {
                    isClassCreated = true;
                }
            }
        }


        //****************************************************************************
        //  function of ProcessDeclaredDataTypes()
        //  if EnteredKey text is equal to data type name defined in datatypes
        // then set isDataTypeDeclared to true
        //****************************************************************************
        public void ProcessDeclaredDataTypes(String input)
        {
            foreach (String item in datatypes)
            {
                if (item == input)
                {
                    isDataTypeDeclared = true;
                }
            }
        }



        //*******************************************************************************************************************
        //  ProcessToolTips() function
        //  match selected item with keywords list item and set width & height & change text of label
        //*******************************************************************************************************************
        public void ProcessToolTips(String input)
        {
            switch (input)
            {
                case "bool":
                    ToolTipControl.ToolTipText = "struct System.Boolean\nRepresents a Boolean value";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 35;
                    break;

                case "char":
                    ToolTipControl.ToolTipText = "struct System.Char\nRepresents a Unicode character";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 35;
                    break;

                case "class":
                    ToolTipControl.ToolTipText = "class\nCollections of datas and functions\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "do":
                    ToolTipControl.ToolTipText = "do\nCode snippet for do....while loop\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "double":
                    ToolTipControl.ToolTipText = "struct System.Double\nRepresents a double-precision floating-point number";
                    ToolTipControl.Width = 260;
                    ToolTipControl.Height = 35;
                    break;

                case "else":
                    ToolTipControl.ToolTipText = "else\nCode snippet for else statement\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "enum":
                    ToolTipControl.ToolTipText = "enum\nCode snippet for enum statement\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "float":
                    ToolTipControl.ToolTipText = "struct System.Single\nRepresents a single-precision floating-point number";
                    ToolTipControl.Width = 260;
                    ToolTipControl.Height = 35;
                    break;

                case "for":
                    ToolTipControl.ToolTipText = "for\nCode snippet for 'for' loop\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "if":
                    ToolTipControl.ToolTipText = "if\nCode snippet for if statement\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "int":
                    ToolTipControl.ToolTipText = "struct System.Int32\nRepresents a 32-bit signed integer";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 35;
                    break;

                case "long":
                    ToolTipControl.ToolTipText = "struct System.Int64\nRepresents a 64-bit signed integer";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 35;
                    break;

                case "short":
                    ToolTipControl.ToolTipText = "struct System.Int16\nRepresents a 16-bit signed integer";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 35;
                    break;

                case "struct":
                    ToolTipControl.ToolTipText = "struct\nCode snippet for struct\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "switch":
                    ToolTipControl.ToolTipText = "switch\nCode snippet for switch statement\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "try":
                    ToolTipControl.ToolTipText = "try\nCode snippet for try catch\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 150;
                    ToolTipControl.Height = 60;
                    break;

                case "using":
                    ToolTipControl.ToolTipText = "using\nCode snippet for using statement\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;

                case "while":
                    ToolTipControl.ToolTipText = "while\nCode snippet for while loop\n\nPress TAB twice to insert snippet";
                    ToolTipControl.Width = 180;
                    ToolTipControl.Height = 60;
                    break;


                case "Form":
                    ToolTipControl.ToolTipText = "class System.Windows.Forms.Form\nRepresents the windows or dailog box to display user interfaces";
                    ToolTipControl.Width = 320;
                    ToolTipControl.Height = 35;
                    break;

                case "Button":
                    ToolTipControl.ToolTipText = "class System.Windows.Forms.Button\nRepresents a windows button control";
                    ToolTipControl.Width = 200;
                    ToolTipControl.Height = 35;
                    break;



                default:
                    if (isToolTipControlAdded)
                    {
                        ToolTipControl.Visible = false;
                    }
                    break;

            }
        }




        // insert code into CCRichTextBox at selection start position 
        // when Tab key is down
        // e.g if user pressed a key 'Tab' twice on 'for' selected item in
        // CodeCompleteBox then insert for loop
        public void InsertSyntax(String text)
        {
            if (isCodeCompleteBoxAdded)
            {
                if (EnteredKey != "")
                {
                    if (EnteredKey.Length == 1)
                    {
                        int sel = this.SelectionStart;
                        text = text.Remove(0, 1);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else if (EnteredKey.Length == 2)
                    {
                        int sel = this.SelectionStart;
                        text = text.Remove(0, 2);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else if (EnteredKey.Length == 3)
                    {
                        int sel = this.SelectionStart;
                        text = text.Remove(0, 3);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else
                    {
                        int sel = this.SelectionStart;
                        if (text.Contains(EnteredKey))
                        {
                            text = text.Replace(EnteredKey, "");
                        }
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                }
            }
        }



        //**************************************************************
        // InsertingCodeSnippetCodes() function
        //**************************************************************
        public void InsertingCodeSnippetCodes()
        {
            if (isCodeCompleteBoxAdded)
            {
                RichTextBox rtb = new RichTextBox();

                switch (CodeCompleteBox.SelectedItem.ToString())
                {
                    case "class":
                        rtb.Text = "";
                        rtb.Text = "class MyClass\n{\n                   \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "do":
                        rtb.Text = "";
                        rtb.Text = "do  {\n               \n}while(true);";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "else":
                        rtb.Text = "";
                        rtb.Text = "else  {\n                \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "enum":
                        rtb.Text = "";
                        rtb.Text = "enum MyEnums  {\n             \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "for":
                        rtb.Text = "";
                        rtb.Text = "for( )\n{\n             \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "if":
                        rtb.Text = "";
                        rtb.Text = "if(true) \n{\n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "struct":
                        rtb.Text = "";
                        rtb.Text = "struct MyStruct  {\n             \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "switch":
                        rtb.Text = "";
                        rtb.Text = "switch( ) \n{ \n           default : break;     \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "try":
                        rtb.Text = "";
                        rtb.Text = "try\n{\n                   \n} \n catch(Exception e) \n{ \n              throw;          \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "using":
                        rtb.Text = "";
                        rtb.Text = "using( )\n{\n                        \n}";
                        this.InsertSyntax(rtb.Text);
                        break;

                    case "while":
                        rtb.Text = "";
                        rtb.Text = "while(true)\n{\n                   \n}";
                        this.InsertSyntax(rtb.Text);
                        break;
                }
            }
        }

        //***********************************************************************************
        //  declare static Boolean isCodeCompleteBoxAdded variable to check
        //  for CodeCompleteBox is added to CCRichTextBox or not
        //***********************************************************************************
        public static Boolean isCodeCompleteBoxAdded = false;




        //*******************************************************************************
        //  ProcessAutoCompleteBrackets() function
        //  function to automatically complete brackets like ( , [ , { , < , ' , "
        //********************************************************************************
        public void ProcessAutoCompleteBrackets(KeyPressEventArgs e)
        {
            String s = e.KeyChar.ToString();
            int sel = this.SelectionStart;
            switch (s)
            {
                case "(": this.Text = this.Text.Insert(sel, "()");
                    e.Handled = true;
                    this.SelectionStart = sel + 1;
                    break;

                case "{":
                    String t = "{\n                 \n}";
                    this.Text = this.Text.Insert(sel, t);
                    e.Handled = true;
                    this.SelectionStart = sel + t.Length - 6;
                    break;

                case "[": this.Text = this.Text.Insert(sel, "[]");
                    e.Handled = true;
                    this.SelectionStart = sel + 1;
                    break;

                case "<": this.Text = this.Text.Insert(sel, "<>");
                    e.Handled = true;
                    this.SelectionStart = sel + 1;
                    break;

                case "\"": this.Text = this.Text.Insert(sel, "\"\"");
                    e.Handled = true;
                    this.SelectionStart = sel + 1;
                    break;

                case "'": this.Text = this.Text.Insert(sel, "''");
                    e.Handled = true;
                    this.SelectionStart = sel + 1;
                    break;
            }
        }



        //****************************************************************************
        //   ProcessCodeCompletion() function
        //****************************************************************************
        public void ProcessCodeCompletionAction(String key)
        {
            EnteredKey = "";

            // concat the key & EnteredKey postfix
            EnteredKey = EnteredKey + key;

            char ch;

            //check pressed key on CCRichTextBox is lower case alphabet or not
            for (ch = 'a'; ch <= 'z'; ch++)
            {
                if (key == ch.ToString())
                {
                    // Clear the CodeCompleteBox Items 
                    CodeCompleteBox.Items.Clear();
                    //add each item to CodeCompleteBox
                    foreach (String item in keywordslist)
                    {
                        //check item is starts with EnteredKey or not
                        if (item.StartsWith(EnteredKey))
                        {
                            CodeCompleteBox.Items.Add(item);
                        }
                    }


                    //  read each item from CodeCompleteBox to set SelectedItem
                    foreach (String item in keywordslist)
                    {
                        if (item.StartsWith(EnteredKey))
                        {
                            CodeCompleteBox.SelectedItem = item;

                            //  set Default cursor to CodeCompleteBox
                            CodeCompleteBox.Cursor = Cursors.Default;

                            //  set Size to CodeCompleteBox
                            // width=this.getWidth() & height=this.getHeight()+(int)this.Font.Size
                            CodeCompleteBox.Size = new System.Drawing.Size(this.getWidth(), this.getHeight() + (int)this.Font.Size);

                            //  set Location to CodeCompleteBox by calling getXYPoints() function
                            CodeCompleteBox.Location = this.getXYPoints();

                            //  adding controls of CodeCompleteBox to CCRichTextBox
                            this.Controls.Add(CodeCompleteBox);

                            //  set Focus to CodeCompleteBox
                            CodeCompleteBox.Focus();

                            //  set isCodeCompleteBoxAdded to true
                            isCodeCompleteBoxAdded = true;


                            // set location to ToolTipControl
                            ToolTipControl.Location = new Point(CodeCompleteBox.Location.X + CodeCompleteBox.Width, CodeCompleteBox.Location.Y);

                            // call ProcessToolTips() function
                            this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());

                            // add ToolTipControl to CCRichTextBox
                            this.Controls.Add(ToolTipControl);

                            isToolTipControlAdded = true;

                            break;
                        }

                        else
                        {
                            isCodeCompleteBoxAdded = false;
                        }
                    }
                }

                // check pressed key character is upper case letter or not
                else if (key == ch.ToString().ToUpper())
                {
                    // Clear the CodeCompleteBox Items 
                    CodeCompleteBox.Items.Clear();
                    //add each item to CodeCompleteBox
                    foreach (String item in keywordslist)
                    {
                        //check item is starts with EnteredKey or not
                        if (item.StartsWith(EnteredKey))
                        {
                            CodeCompleteBox.Items.Add(item);
                        }
                    }


                    //  read each item from CodeCompleteBox to set SelectedItem
                    foreach (String item in keywordslist)
                    {
                        if (item.StartsWith(EnteredKey))
                        {
                            CodeCompleteBox.SelectedItem = item;

                            //  set Default cursor to CodeCompleteBox
                            CodeCompleteBox.Cursor = Cursors.Default;

                            //  set Size to CodeCompleteBox
                            // width=this.getWidth() & height=this.getHeight()+(int)this.Font.Size
                            CodeCompleteBox.Size = new System.Drawing.Size(this.getWidth(), this.getHeight() + (int)this.Font.Size);

                            //  set Location to CodeCompleteBox by calling getXYPoints() function
                            CodeCompleteBox.Location = this.getXYPoints();

                            //  adding controls of CodeCompleteBox to CCRichTextBox
                            this.Controls.Add(CodeCompleteBox);

                            //  set Focus to CodeCompleteBox
                            CodeCompleteBox.Focus();

                            //  set isCodeCompleteBoxAdded to true
                            isCodeCompleteBoxAdded = true;

                            // set location to ToolTipControl
                            ToolTipControl.Location = new Point(CodeCompleteBox.Location.X + CodeCompleteBox.Width, CodeCompleteBox.Location.Y);

                            // call ProcessToolTips() function
                            this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());

                            // add ToolTipControl to CCRichTextBox
                            this.Controls.Add(ToolTipControl);

                            isToolTipControlAdded = true;

                            break;
                        }

                        else
                        {
                            isCodeCompleteBoxAdded = false;
                        }
                    }
                }
            }
        }



        //*************************************************************************************
        //  Text Changed event of CCRichTextBox
        //  if text is null then remove CodeCompleteBox from CCRichTextBox
        //*****************************************************************************************
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            // remove CodeCompleteBox when text is null in CCRichTextBox
            // before remove check CodeCompleteBox is added to CCRichTextBox or not
            if (this.Text == "")
            {
                if (isCodeCompleteBoxAdded)
                {
                    this.Controls.Remove(CodeCompleteBox);
                    EnteredKey = "";

                    if (isToolTipControlAdded)
                    {
                        this.Controls.Remove(ToolTipControl);
                    }
                }
            }
        }



        //***********************************************************************************
        // KeyDown event of CCRichTextBox
        //**********************************************************************************
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.KeyCode)
            {
                // if Space key is down then remove CodeCompleteBox control from CCRichTextBox
                case Keys.Space:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }
                    }
                    break;

                // if Enter key is down then remove CodeCompleteBox control from CCRichTextBox
                case Keys.Enter:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }
                    }
                    break;

                // if Escape key is down then remove CodeCompleteBox control from CCRichTextBox
                case Keys.Escape:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }
                    }
                    break;

                // if Back key is down then remove CodeCompleteBox control from CCRichTextBox
                case Keys.Back:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }
                    }
                    break;
            }
        }


        //******************************************************************************
        //  Key Press event of CCRichTextBox
        //*****************************************************************************
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // check isAutoCompleteBrackets is true then call ProcessAutoCompleteBrackets() function
            if (isAutoCompleteBrackets)
            {
                ProcessAutoCompleteBrackets(e);
            }


            String key = e.KeyChar.ToString();

            // as you know how class object is declared
            // e.g Form f=new Form();
            // if you selected the Form text from CodeCompleteBox then it must be completed with
            // semicolon or declare the object of that class using new keyword by specifying = sign
            // same as data types

            if (isClassCreated && (key == "=" || key == ";"))
            {

                ProcessCodeCompletionAction(key);

                isClassCreated = false;
            }
            else if (isClassCreated && key != "=")
            { }
            else if (isDataTypeDeclared && (key == ";" || key == "{" || key == "}" || key == "(" || key == ")"))
            {

                ProcessCodeCompletionAction(key);


                isDataTypeDeclared = false;
            }
            else if (isDataTypeDeclared && key != ";")
            { }
            else
            {

                ProcessCodeCompletionAction(key);
            }
        }


        //*****************************************************************************************
        //  Mouse Click event of CCRichTextBox
        //****************************************************************************************
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // when mouse click on CCRichTextBox then remove CodeCompleteBox from CCRichTextBox

            if (isCodeCompleteBoxAdded)
            {
                this.Controls.Remove(CodeCompleteBox);
                EnteredKey = "";

                if (isToolTipControlAdded)
                {
                    this.Controls.Remove(ToolTipControl);
                }
            }
        }


        //*****************************************************************************************
        //  VScroll event of CCRichTextBox
        //****************************************************************************************
        protected override void OnVScroll(EventArgs e)
        {
            base.OnVScroll(e);

            // remove CodeCompleteBox when VScroll is changed in CCRichTextBox
            // before remove check CodeCompleteBox is added to CCRichTextBox or not
            if (isCodeCompleteBoxAdded)
            {
                this.Controls.Remove(CodeCompleteBox);
                EnteredKey = "";

                if (isToolTipControlAdded)
                {
                    this.Controls.Remove(ToolTipControl);
                }
            }
        }



        //*********************************************************************
        //  CodeCompleteBox KeyDown events function
        //*********************************************************************
        private void CodeCompleteBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // if Space key is down when CodeCompleteBox is added to the CCRichTextBox
                //  then insert SelectedItem from CodeCompleteBox to this at SelectionStart location
                //  also inserting a single space because space bar key is down

                case Keys.Space:
                    if (isCodeCompleteBoxAdded)
                    {
                        if (CodeCompleteBox.SelectedItem.ToString().StartsWith(EnteredKey))
                        {
                            if (EnteredKey != "")
                            {
                                // before inserting a selected item first check EnteredKey length
                                // if it is 1 then remove first character of selected item from CodeCompleteBox
                                // if it is 2 then remove first 2 characters of selected item from CodeCompleteBox
                                // if it is 3 then remove first 3 characters
                                // if it is greater than 3 then replace EnteredKey with null/"" in selected item text
                                // this all arrangement is important because characters keywords added to CodeCompleteBox could be same
                                if (EnteredKey.Length == 1)
                                {
                                    int sel = this.SelectionStart;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 1);
                                    this.Text = this.Text.Insert(sel, text + " ");
                                    this.SelectionStart = sel + (text + " ").Length;
                                    this.Controls.Remove(CodeCompleteBox);

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        this.Controls.Remove(ToolTipControl);
                                    }

                                }
                                else if (EnteredKey.Length == 2)
                                {
                                    int sel = this.SelectionStart;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 2);
                                    this.Text = this.Text.Insert(sel, text + " ");
                                    this.SelectionStart = sel + (text + " ").Length;
                                    this.Controls.Remove(CodeCompleteBox);

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        this.Controls.Remove(ToolTipControl);
                                    }

                                }
                                else if (EnteredKey.Length == 3)
                                {
                                    int sel = this.SelectionStart;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    text = text.Remove(0, 3);
                                    this.Text = this.Text.Insert(sel, text + " ");
                                    this.SelectionStart = sel + (text + " ").Length;
                                    this.Controls.Remove(CodeCompleteBox);

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        this.Controls.Remove(ToolTipControl);
                                    }

                                }
                                else
                                {
                                    int sel = this.SelectionStart;
                                    String text = CodeCompleteBox.SelectedItem.ToString();
                                    if (text.Contains(EnteredKey))
                                    {
                                        text = text.Replace(EnteredKey, "");
                                    }
                                    this.Text = this.Text.Insert(sel, text + " ");
                                    this.SelectionStart = sel + (text + " ").Length;
                                    this.Controls.Remove(CodeCompleteBox);

                                    this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                    this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                    if (isToolTipControlAdded)
                                    {
                                        this.Controls.Remove(ToolTipControl);
                                    }

                                }
                            }
                        }
                        else
                        {
                            int sel = this.SelectionStart;
                            this.Text = this.Text.Insert(sel, " ");
                            this.SelectionStart = sel + " ".Length;
                        }
                    }
                    break;


                // if Enter key is down when CodeCompleteBox is added to the CCRichTextBox
                //  then insert SelectedItem from CodeCompleteBox to this at SelectionStart location
                // same all procedure as used when Space key is down only without inserting a single space here

                case Keys.Enter:
                    if (isCodeCompleteBoxAdded)
                    {
                        if (EnteredKey != "")
                        {
                            if (EnteredKey.Length == 1)
                            {
                                int sel = this.SelectionStart;
                                String text = CodeCompleteBox.SelectedItem.ToString();
                                text = text.Remove(0, 1);
                                this.Text = this.Text.Insert(sel, text);
                                this.SelectionStart = sel + text.Length;
                                this.Controls.Remove(CodeCompleteBox);

                                this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                if (isToolTipControlAdded)
                                {
                                    this.Controls.Remove(ToolTipControl);
                                }

                            }
                            else if (EnteredKey.Length == 2)
                            {
                                int sel = this.SelectionStart;
                                String text = CodeCompleteBox.SelectedItem.ToString();
                                text = text.Remove(0, 2);
                                this.Text = this.Text.Insert(sel, text);
                                this.SelectionStart = sel + text.Length;
                                this.Controls.Remove(CodeCompleteBox);

                                this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                if (isToolTipControlAdded)
                                {
                                    this.Controls.Remove(ToolTipControl);
                                }

                            }
                            else if (EnteredKey.Length == 3)
                            {
                                int sel = this.SelectionStart;
                                String text = CodeCompleteBox.SelectedItem.ToString();
                                text = text.Remove(0, 3);
                                this.Text = this.Text.Insert(sel, text);
                                this.SelectionStart = sel + text.Length;
                                this.Controls.Remove(CodeCompleteBox);

                                this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                if (isToolTipControlAdded)
                                {
                                    this.Controls.Remove(ToolTipControl);
                                }

                            }
                            else
                            {
                                int sel = this.SelectionStart;
                                String text = CodeCompleteBox.SelectedItem.ToString();
                                if (text.Contains(EnteredKey))
                                {
                                    text = text.Replace(EnteredKey, "");
                                }
                                this.Text = this.Text.Insert(sel, text);
                                this.SelectionStart = sel + text.Length;
                                this.Controls.Remove(CodeCompleteBox);

                                this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                                this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                                if (isToolTipControlAdded)
                                {
                                    this.Controls.Remove(ToolTipControl);
                                }

                            }
                        }
                    }
                    break;

                // if Left key is down then remove CodeCompleteBox from this
                case Keys.Left:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    break;

                // if Right key is down then remove CodeCompleteBox from this
                case Keys.Right:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    break;
            }
        }



        //*********************************************************************
        //  CodeCompleteBox KeyUp events function
        //*********************************************************************
        private void CodeCompleteBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    if (isCodeCompleteBoxAdded)
                    {
                        ToolTipControl.Visible = true;
                        this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());
                    }
                    break;


                case Keys.Tab:
                    if (isCodeCompleteBoxAdded)
                    {
                        this.InsertingCodeSnippetCodes();
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    break;
            }
        }


        //*********************************************************************
        //  CodeCompleteBox KeyPress events function
        //*********************************************************************
        private void CodeCompleteBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            String str = e.KeyChar.ToString();

            // in this event we must insert pressed key to this because Focus is on CodeCompleteBox

            // first check pressed key is not Space,Enter,Escape & Back
            // Space=32, Enter=13, Escape=27, Back=8
            if (Convert.ToInt32(e.KeyChar) != 13 && Convert.ToInt32(e.KeyChar) != 32 && Convert.ToInt32(e.KeyChar) != 27 && Convert.ToInt32(e.KeyChar) != 8)
            {
                if (isCodeCompleteBoxAdded)
                {
                    // insert pressed key to CCRichTextBox at SelectionStart position
                    int sel = this.SelectionStart;
                    this.Text = this.Text.Insert(sel, str);
                    this.SelectionStart = sel + 1;
                    e.Handled = true;

                    // concat the EnteredKey and pressed key on CodeCompleteBox
                    EnteredKey = EnteredKey + str;

                    // search item in CodeCompleteBox which starts with EnteredKey and set it to selected
                    foreach (String item in CodeCompleteBox.Items)
                    {
                        if (item.StartsWith(EnteredKey))
                        {
                            CodeCompleteBox.SelectedItem = item;

                            ToolTipControl.Visible = true;
                            this.ProcessToolTips(CodeCompleteBox.SelectedItem.ToString());
                            break;
                        }
                    }
                }
            }

            // if pressed key is Back then set focus to CCRichTextBox
            else if (Convert.ToInt32(e.KeyChar) == 8)
            {
                this.Focus();
            }

              // if pressed key is not Back then remove CodeCompleteBox from CCRichTextBox
            else if (Convert.ToInt32(e.KeyChar) != 8)
            {
                if (isCodeCompleteBoxAdded)
                {
                    this.Controls.Remove(CodeCompleteBox);
                    EnteredKey = "";

                    if (isToolTipControlAdded)
                    {
                        this.Controls.Remove(ToolTipControl);
                    }

                }
            }



            //  check pressed key on CodeCompleteBox is special character or not
            //   if it is a special character then remove CodeCompleteBox from CCRichTextBox
            switch (str)
            {
                case "~":
                case "`":
                case "!":
                case "@":
                case "#":
                case "$":
                case "%":
                case "^":
                case "&":
                case "*":
                case "-":
                case "_":
                case "+":
                case "=":
                case "(":
                case ")":
                case "[":
                case "]":
                case "{":
                case "}":
                case ":":
                case ";":
                case "\"":
                case "'":
                case "|":
                case "\\":
                case "<":
                case ">":
                case ",":
                case ".":
                case "/":
                case "?":
                    if (isCodeCompleteBoxAdded)
                    {
                        this.Controls.Remove(CodeCompleteBox);
                        EnteredKey = "";

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    break;

            }
        }



        //*********************************************************************
        //  CodeCompleteBox MouseClick events function
        //*********************************************************************
        private void CodeCompleteBox_MouseClick(object sender, MouseEventArgs e)
        {
            // insert selected item text in CCRichTextBox at SelectionStart position when mouse clicks on CodeCompleteBox
            //  action is same as Enter key is Down

            if (isCodeCompleteBoxAdded)
            {
                if (EnteredKey != "")
                {
                    if (EnteredKey.Length == 1)
                    {
                        int sel = this.SelectionStart;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 1);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else if (EnteredKey.Length == 2)
                    {
                        int sel = this.SelectionStart;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 2);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else if (EnteredKey.Length == 3)
                    {
                        int sel = this.SelectionStart;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        text = text.Remove(0, 3);
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                    else
                    {
                        int sel = this.SelectionStart;
                        String text = CodeCompleteBox.SelectedItem.ToString();
                        if (text.Contains(EnteredKey))
                        {
                            text = text.Replace(EnteredKey, "");
                        }
                        this.Text = this.Text.Insert(sel, text);
                        this.SelectionStart = sel + text.Length;
                        this.Controls.Remove(CodeCompleteBox);

                        this.ProcessDeclaredClasses(CodeCompleteBox.SelectedItem.ToString());
                        this.ProcessDeclaredDataTypes(CodeCompleteBox.SelectedItem.ToString());

                        if (isToolTipControlAdded)
                        {
                            this.Controls.Remove(ToolTipControl);
                        }

                    }
                }
            }
        }



    }
}
