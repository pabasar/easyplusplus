using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Easypp3
{
    public partial class EasyPlusPlus : Form
    {
        public EasyPlusPlus()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
        }

        public int line = 0;
        public int column = 0;

        string setPath = "";
        string cppName = "";
        string directory = "";
        string extName = "";
        string output = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            rchTxtLine.Font = rchTxtBx.Font;
            rchTxtBx.Select();
            AddLineNumbers();
        }

        private void CheckKeyWord(string word, Color color, int startIndex)
        {
            if (this.rchTxtBx.Text.Contains(word))
            {
                int index = -1;
                int selectStart = this.rchTxtBx.SelectionStart;

                while ((index = this.rchTxtBx.Text.IndexOf(word, (index + 1))) != -1)
                {
                    this.rchTxtBx.Select((index + startIndex), word.Length);
                    this.rchTxtBx.SelectionColor = color;
                    this.rchTxtBx.Select(selectStart, 0);
                    this.rchTxtBx.SelectionColor = Color.WhiteSmoke;
                }
            }
        }

        public int getWidth()
        {
            int w = 25;
            int line = rchTxtBx.Lines.Length;

            if (line <= 99)
            {
                w = 20 + (int)rchTxtBx.Font.Size;
            }
            else if (line <= 999)
            {
                w = 30 + (int)rchTxtBx.Font.Size;
            }
            else
            {
                w = 50 + (int)rchTxtBx.Font.Size;
            }

            return w;
        }

        public void AddLineNumbers()
        {
            
            Point pt = new Point(0, 0);

            int First_Index = rchTxtBx.GetCharIndexFromPosition(pt);
            int First_Line = rchTxtBx.GetLineFromCharIndex(First_Index);
           
            pt.X = ClientRectangle.Width;
            pt.Y = ClientRectangle.Height;

            int Last_Index = rchTxtBx.GetCharIndexFromPosition(pt);
            int Last_Line = rchTxtBx.GetLineFromCharIndex(Last_Index);

            rchTxtLine.SelectionAlignment = HorizontalAlignment.Center;

            rchTxtLine.Text = "";
            rchTxtLine.Width = getWidth();
                
            for (int i = First_Line; i <= Last_Line + 2; i++)
            {
                rchTxtLine.Text += i + 1 + "\n";
            }
        }
       

        private void openToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private static bool MAXIMIZED = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (MAXIMIZED)
            {
                WindowState = FormWindowState.Normal;
                MAXIMIZED = false;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
                MAXIMIZED = true;
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCls_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Save changes?", "File Modified", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Exclamation);
            if (result1 == DialogResult.Yes)
            {
                btnSave.PerformClick();
                Application.Exit();
            }
            else if (result1 == DialogResult.No)
            {
                Application.Exit();
            }
            else if (result1 == DialogResult.Cancel)
            {
                
            }
            
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            rchTxtBx.Clear();
            txtOutput.Clear();
            tabControl1.SelectedTab.Text = "Untitled";
        }

        private void btnOpn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "C++ File(*.cpp)|*.cpp";
            openFile.Title = "Open C++ Project";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                rchTxtBx.Clear();
                using (StreamReader sr = new StreamReader(openFile.FileName))
                {
                    FileInfo fi = new FileInfo(openFile.FileName);
                    cppName = fi.Name;
                    rchTxtBx.Text = sr.ReadToEnd();
                    sr.Close();
                }
                tabControl1.SelectedTab.Text = cppName;
            }
            directory = Path.GetDirectoryName(openFile.FileName);
            extName = Path.GetFileNameWithoutExtension(openFile.FileName);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as";
            savefile.Filter = "C++ File(*.cpp)|*.cpp";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                FileInfo fi = new FileInfo(savefile.FileName);
                cppName = fi.Name;
                txtoutput.Write(rchTxtBx.Text);
                txtoutput.Close();               
            }
            tabControl1.SelectedTab.Text = cppName;
            directory = Path.GetDirectoryName(savefile.FileName);
            extName = Path.GetFileNameWithoutExtension(savefile.FileName);
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            rchTxtBx.Cut();
        }

        private void btnCpy_Click(object sender, EventArgs e)
        {
            rchTxtBx.Copy();
        }

        private void btnSA_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Save file as";
            savefile.Filter = "C++ File(*.cpp)|*.cpp";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtoutput = new StreamWriter(savefile.FileName);
                FileInfo fi = new FileInfo(savefile.FileName);
                cppName = fi.Name;
                txtoutput.Write(rchTxtBx.Text);
                txtoutput.Close();
            }
            tabControl1.SelectedTab.Text = cppName;
            directory = Path.GetDirectoryName(savefile.FileName);
            extName = Path.GetFileNameWithoutExtension(savefile.FileName);
        }

        private void btnPst_Click(object sender, EventArgs e)
        {
            rchTxtBx.Paste();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            rchTxtBx.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            rchTxtBx.Redo();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public Color SandyBrown { get; set; }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            line = 1 + rchTxtBx.GetLineFromCharIndex(rchTxtBx.GetFirstCharIndexOfCurrentLine());
            column = 1 + rchTxtBx.SelectionStart - rchTxtBx.GetFirstCharIndexOfCurrentLine();
            lblLine.Text = "Line: "+line.ToString();
            lblClmn.Text = "Column: " + column.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        

        private void EasyPlusPlus_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void rchTxtBx_TextChanged_1(object sender, EventArgs e)
        {
            if (rchTxtBx.Text == "")
            {
                AddLineNumbers();
            }  

            this.CheckKeyWord("public", Color.CornflowerBlue, 0);
            this.CheckKeyWord("private", Color.CornflowerBlue, 0);
            this.CheckKeyWord("protected", Color.CornflowerBlue, 0);
            this.CheckKeyWord("using", Color.CornflowerBlue, 0);
            this.CheckKeyWord("new", Color.CornflowerBlue, 0);
            this.CheckKeyWord("this", Color.CornflowerBlue, 0);
            this.CheckKeyWord("if", Color.CornflowerBlue, 0);
            this.CheckKeyWord("else if", Color.CornflowerBlue, 0);
            this.CheckKeyWord("else", Color.CornflowerBlue, 0);
            this.CheckKeyWord("switch", Color.CornflowerBlue, 0);
            this.CheckKeyWord("case", Color.CornflowerBlue, 0);
            this.CheckKeyWord("break", Color.CornflowerBlue, 0);
            this.CheckKeyWord("do", Color.CornflowerBlue, 0);
            this.CheckKeyWord("while", Color.CornflowerBlue, 0);
            this.CheckKeyWord("void", Color.CornflowerBlue, 0);
            this.CheckKeyWord("true", Color.CornflowerBlue, 0);
            this.CheckKeyWord("false", Color.CornflowerBlue, 0);
            this.CheckKeyWord("#include", Color.Aqua, 0);
            this.CheckKeyWord("int", Color.GreenYellow, 0);
            this.CheckKeyWord("string", Color.GreenYellow, 0);
            this.CheckKeyWord("double", Color.GreenYellow, 0);
            this.CheckKeyWord("boolean", Color.GreenYellow, 0);
            this.CheckKeyWord("long", Color.GreenYellow, 0);
            this.CheckKeyWord("short", Color.GreenYellow, 0);
            this.CheckKeyWord("float", Color.GreenYellow, 0);
            this.CheckKeyWord("namespace", Color.Magenta, 0);
            this.CheckKeyWord("class", Color.LightSeaGreen, 0);
        }

        private void rchTxtBx_KeyDown_1(object sender, KeyEventArgs e)
        {
            {
                if (e.Control && e.KeyCode.ToString() == "N")
                {
                    btnNew.PerformClick();
                }

                if (e.Control && e.KeyCode.ToString() == "S")
                {
                    btnSave.PerformClick();
                }

                if (e.Control && e.KeyCode.ToString() == "O")
                {
                    btnOpn.PerformClick();
                }

                if (e.Control && e.KeyCode.ToString() == "C")
                {
                    btnCpy.PerformClick();
                }

                if (e.Control && e.KeyCode.ToString() == "X")
                {
                    btnCut.PerformClick();
                }

                if (e.Control && e.KeyCode.ToString() == "V")
                {
                    btnPst.PerformClick();
                }

            }
        }

        private void EasyPlusPlus_Resize(object sender, EventArgs e)
        {
            AddLineNumbers();    
        }

        private void rchTxtBx_SelectionChanged(object sender, EventArgs e)
        {
            Point pt = rchTxtBx.GetPositionFromCharIndex(rchTxtBx.SelectionStart);
            if (pt.X == 1)
            {
                AddLineNumbers();
            } 
        }

        private void rchTxtBx_VScroll(object sender, EventArgs e)
        {
            rchTxtLine.Text = "";
            AddLineNumbers();
            rchTxtLine.Invalidate(); 
        }

        private void rchTxtBx_FontChanged(object sender, EventArgs e)
        {
            rchTxtLine.Font = rchTxtBx.Font;
            rchTxtBx.Select();
            AddLineNumbers();
        }

        private void rchTxtLine_MouseDown(object sender, MouseEventArgs e)
        {
            rchTxtBx.Select();
            rchTxtLine.DeselectAll();  
        }

        private void btnBld_Click(object sender, EventArgs e)
        {
            Process build = new Process();
            build.StartInfo.FileName = "cmd.exe";
            build.StartInfo.RedirectStandardInput = true;
            build.StartInfo.RedirectStandardOutput = true;
            build.StartInfo.CreateNoWindow = true;
            build.StartInfo.UseShellExecute = false;
            build.Start();

            build.StandardInput.WriteLine("cd "+directory);
            build.StandardInput.WriteLine("g++ -o "+extName+".exe "+cppName);
        }

        private void btnRn_Click(object sender, EventArgs e)
        {
            Process p = new Process();

            p.StartInfo.WorkingDirectory = directory;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = directory+"\\"+extName+".exe";
            p.Start();

            try
            {
                p.StandardInput.WriteLine(extName + ".exe");
                output = p.StandardOutput.ReadToEnd().ToString();
                p.WaitForExit();
                txtOutput.Text = output;
            }catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblClmn_Click(object sender, EventArgs e)
        {

        }

        private void btnSettings_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Choose MinGw installation directory", "Set Path", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if (folder.ShowDialog() == DialogResult.OK)
            {
                setPath = folder.SelectedPath;
            }

            ProcessStartInfo pathInfo;
            Process path;

            pathInfo = new ProcessStartInfo("cmd.exe", "/c" + "set path=" + setPath);
            pathInfo.CreateNoWindow = false;
            pathInfo.UseShellExecute = false;
            pathInfo.RedirectStandardInput = true;
            pathInfo.RedirectStandardOutput = true;

            path = Process.Start(pathInfo);
        }

        

        
    }
}
