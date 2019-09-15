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
            IntPtr hdc = e.Graphics.GetHdc();

            //バーコードの印字
            String BarFontName = "Code 128";
            int BarFontSize = 200;
            //バーコードの場合は9番目の引数を0にする必要あり
            IntPtr mFont = CreateFont(BarFontSize, 0, 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, BarFontName);
            SelectObject(hdc, mFont);
            String BarcodeData = "9999999999";
            int xPos = 100;
            int yPos = 150; 
            TextOut(hdc, xPos, yPos, BarcodeData, BarcodeData.Length);
            DeleteObject(mFont);

            //文字列の印字
            String BarFontName2 = "Meiryo UI";
            int BarFontSize2 = 50;
            IntPtr mFont2 = CreateFont(BarFontSize2, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, BarFontName2);
            SelectObject(hdc, mFont2);
            String BarcodeData2 = "商品名：○○○";
            int xPos2 = 200;
            int yPos2 = 360;
            TextOut(hdc, xPos2, yPos2, BarcodeData2, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData2));
            DeleteObject(mFont2);

            //文字列の印字
            String BarFontName3 = "Meiryo UI";
            int BarFontSize3 = 100;
            //文字列の場合は9番目の引数を128にする必要あり
            IntPtr mFont3 = CreateFont(BarFontSize3, 0, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, BarFontName3);
            SelectObject(hdc, mFont3);
            String BarcodeData3 = "No:12345678";
            int xPos3 = 10;
            int yPos3 = 10;
            TextOut(hdc, xPos3, yPos3, BarcodeData3, Encoding.GetEncoding("Shift_JIS").GetByteCount(BarcodeData3));
            DeleteObject(mFont3);

            //デバイスコンテキストの開放　これは必ず必要
            e.Graphics.ReleaseHdc(hdc);
        }
    }
}
