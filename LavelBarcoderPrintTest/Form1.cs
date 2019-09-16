using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavelBarcoderPrintTest
{
    public partial class Form1 : Form
    {
        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            //プリンタ一覧取得
            foreach (string s in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(s);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //プリンタが選択されているか
            if (string.IsNullOrEmpty(comboBox1.Text.Trim()))
            {
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = comboBox1.Text;
            //PrintPageイベントハンドラーを登録　pd_PrintPageはメソッド名
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //デバイスコンテキスト取得
            //e.Graphics.PageUnit = GraphicsUnit.Inch;
            IntPtr hdc = e.Graphics.GetHdc();

            //バーコードの印字 バーコードの場合は9番目の引数を0にする必要あり
            IntPtr mFont = CreateFont(200, 0, 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, "Code 128");
            SelectObject(hdc, mFont);
            String BarcodeData = "9999999999";
            TextOut(hdc, 5, 305, BarcodeData, BarcodeData.Length);
            DeleteObject(mFont);

            String stringFont = "Meiryo UI";

            //文字列1
            IntPtr mFont1 = CreateFont(100, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont1);
            String BarcodeData1 = "文字列１";
            TextOut(hdc, 5, 5, BarcodeData1, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData1));
            DeleteObject(mFont1);
            //文字列2
            IntPtr mFont2 = CreateFont(100, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont2);
            String BarcodeData2 = "文字列２";
            TextOut(hdc, 300, 5, BarcodeData2, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData2));
            DeleteObject(mFont2);

            //文字列3
            IntPtr mFont3 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont3);
            String BarcodeData3 = "文字列３";
            TextOut(hdc, 5, 105, BarcodeData3, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData3));
            DeleteObject(mFont3);
            //文字列4
            IntPtr mFont4 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont4);
            String BarcodeData4 = "文字列４";
            TextOut(hdc, 300, 105, BarcodeData4, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData4));
            DeleteObject(mFont4);

            //文字列5
            IntPtr mFont5 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont5);
            String BarcodeData5 = "文字列5";
            TextOut(hdc, 5, 165, BarcodeData5, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData5));
            DeleteObject(mFont5);
            //文字列6
            IntPtr mFont6 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont6);
            String BarcodeData6 = "文字列6";
            TextOut(hdc, 300, 165, BarcodeData6, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData6));
            DeleteObject(mFont6);

            //文字列7
            IntPtr mFont7 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont7);
            String BarcodeData7 = "文字列7";
            TextOut(hdc, 5, 225, BarcodeData7, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData7));
            DeleteObject(mFont7);
            //文字列8
            IntPtr mFont8 = CreateFont(60, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFont);
            SelectObject(hdc, mFont8);
            String BarcodeData8 = "文字列8";
            TextOut(hdc, 300, 225, BarcodeData8, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData8));
            DeleteObject(mFont8);

            //デバイスコンテキストの開放　これは必ず必要
            e.Graphics.ReleaseHdc(hdc);
        }
    }
}
