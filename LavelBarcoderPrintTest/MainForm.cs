using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace LavelBarcoderPrintTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, bool fdwItalic,
bool fdwUnderline, bool fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr SelectObject(System.IntPtr hObject, System.IntPtr hFont);
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private extern static int TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static bool DeleteObject(System.IntPtr hObject);

        List<int> attributeList = new List<int>();
        List<string> stringList = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            //プリンタ一覧取得
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                cbPrinter.Items.Add(s);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            B_EV4D_GH17_R b_ev4D = new B_EV4D_GH17_R(cbPrinter.Text, new DataTable(), "NW-7", stringList, attributeList);
            pictureBox1.Image = b_ev4D.GetPrintImage(pictureBox1);
            b_ev4D.Print();
        }

        private void btnHyouji_Click(object sender, EventArgs e)
        {
            if (!sender.GetType().Equals(typeof(Button)))
            {
                if (((TextBox)sender).Text == "")
                {
                    return;
                }
            }
            stringList.Clear();
            stringList.Add(tbStr1.Text);
            stringList.Add(tbStr2.Text);
            stringList.Add(tbStr3.Text);
            stringList.Add(tbStr4.Text);
            stringList.Add(tbStr5.Text);
            stringList.Add(tbStr6.Text);
            stringList.Add(tbStr7.Text);
            stringList.Add(tbStr8.Text);
            stringList.Add(tbStr9.Text);
            attributeList.Clear();
            attributeList.AddRange(new int[] { int.Parse(tbH1.Text), int.Parse(tbW1.Text), int.Parse(tbX1.Text), int.Parse(tbY1.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH2.Text), int.Parse(tbW2.Text), int.Parse(tbX2.Text), int.Parse(tbY2.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH3.Text), int.Parse(tbW3.Text), int.Parse(tbX3.Text), int.Parse(tbY3.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH4.Text), int.Parse(tbW4.Text), int.Parse(tbX4.Text), int.Parse(tbY4.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH5.Text), int.Parse(tbW5.Text), int.Parse(tbX5.Text), int.Parse(tbY5.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH6.Text), int.Parse(tbW6.Text), int.Parse(tbX6.Text), int.Parse(tbY6.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH7.Text), int.Parse(tbW7.Text), int.Parse(tbX7.Text), int.Parse(tbY7.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH8.Text), int.Parse(tbW8.Text), int.Parse(tbX8.Text), int.Parse(tbY8.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH9.Text), int.Parse(tbW9.Text), int.Parse(tbX9.Text), int.Parse(tbY9.Text) });

            B_EV4D_GH17_R b_ev4D = new B_EV4D_GH17_R("", new DataTable(), "NW-7", stringList, attributeList);
            pictureBox1.Image = b_ev4D.GetPrintImage(pictureBox1);
        }

        private void btnCopySource_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("stringList.Clear();" + Environment.NewLine);
            stringList.ForEach(a => sb.Append(@"stringList.Add(""" + a + @""");" + Environment.NewLine));
            sb.Append("attributeList.Clear();" + Environment.NewLine);
            for (int i = 0; i < attributeList.Count; i = i + 4)
            {
                sb.Append("attributeList.AddRange(new int[] {" + attributeList[i + 0] + "," + attributeList[i + 1] + "," + attributeList[i + 2] + "," + attributeList[i + 3] + "});" + Environment.NewLine);
            }
            //クリップボードにコピーする
            Clipboard.SetText(sb.ToString());

        }
    }
}
