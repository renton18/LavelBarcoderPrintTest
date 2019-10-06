using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace LavelBarcoderPrintTest
{
    public class B_EV4D_GH17_R
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, bool fdwItalic, bool fdwUnderline, bool fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static System.IntPtr SelectObject(System.IntPtr hObject, System.IntPtr hFont);
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private extern static int TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private extern static bool DeleteObject(System.IntPtr hObject);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        int pageCount = 0;
        string printerName = "";
        DataTable dt = new DataTable();
        Bitmap canvas;
        List<int> attributeList = new List<int>();
        List<string> stringList = new List<string>();
        string barcode;

        public B_EV4D_GH17_R(string printerName, DataTable dt, string barcode, List<string> stringList, List<int> attributeList)
        {
            //dt.Columns.Add("id");
            //DataRow row = dt.NewRow();
            //row["id"] = 4;
            //dt.Rows.Add(row);
            //DataRow row2 = dt.NewRow();
            //row2["id"] = 6;
            //dt.Rows.Add(row2);
            //DataRow row3 = dt.NewRow();
            //row3["id"] = 7;
            //dt.Rows.Add(row3);
            this.attributeList = attributeList;
            this.stringList = stringList;

            //印字サイズ、X軸、Y軸
            //attributeList.AddRange(new int[] { 100, 5, 295 });
            //attributeList.AddRange(new int[] { 100, 5, 5 });
            //attributeList.AddRange(new int[] { 100, 300, 5 });
            //attributeList.AddRange(new int[] { 60, 5, 105 });
            //attributeList.AddRange(new int[] { 60, 300, 105 });
            //attributeList.AddRange(new int[] { 60, 5, 165 });
            //attributeList.AddRange(new int[] { 60, 300, 165 });
            //attributeList.AddRange(new int[] { 60, 5, 225 });
            //attributeList.AddRange(new int[] { 60, 30, 225 });

            this.printerName = printerName;
            this.dt = dt;
            this.barcode = barcode;
        }

        #region 印刷イメージを取得する
        public Bitmap GetPrintImage(PictureBox pictureBox)
        {
            canvas = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(canvas);
            IntPtr hdc = g.GetHdc();
            PrintImage(hdc);
            //デバイスコンテキストの開放
            g.ReleaseHdc(hdc);
            return canvas;
        }
        #endregion

        #region 印刷処理
        public void Print()
        {
            PrintDocument pd = new PrintDocument();
            if (printerName != "")
            {
                pd.PrinterSettings.PrinterName = printerName;
            }
            else
            {
                ////ポップアップを表示させない
                pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                pd.PrinterSettings.PrintFileName = "test.xps";
                pd.DefaultPageSettings.PrinterSettings.PrintToFile = true;
                pd.PrintController = new StandardPrintController();
            }
            pd.PrintPage += new PrintPageEventHandler(this.PdPrintPage);
            pd.Print();
        }
        private void PdPrintPage(object sender, PrintPageEventArgs e)
        {
            //デバイスコンテキスト取得
            IntPtr hdc = e.Graphics.GetHdc();
            PrintImage(hdc);
            pageCount++;
            e.HasMorePages = true;
            //デバイスコンテキストの開放
            e.Graphics.ReleaseHdc(hdc);
            if (dt.Rows.Count <= pageCount)
            {
                //次のページがないことを通知する
                e.HasMorePages = false;
            }
        }
        #endregion

        #region 印刷イメージ作成メソッド
        /// <summary>
        /// 印刷イメージ作成メソッド
        /// </summary>
        /// <param name="hdc"></param>
        private void PrintImage(IntPtr hdc)
        {
            //バーコードの印字 バーコードの場合は9番目の引数を0にする必要あり
            IntPtr mFont = CreateFont(attributeList[0], attributeList[1], 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, barcode);
            SelectObject(hdc, mFont);
            String BarcodeData = stringList[0];
            TextOut(hdc, attributeList[2], attributeList[3], BarcodeData, BarcodeData.Length);
            DeleteObject(mFont);

            String stringFontName = "Meiryo UI";

            //文字列1
            IntPtr mFont1 = CreateFont(attributeList[4], attributeList[5], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont1);
            String printString1 = stringList[1];
            TextOut(hdc, attributeList[6], attributeList[7], printString1, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString1));
            DeleteObject(mFont1);
            //文字列2
            IntPtr mFont2 = CreateFont(attributeList[8], attributeList[9], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont2);
            String printString2 = stringList[2];
            TextOut(hdc, attributeList[10], attributeList[11], printString2, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString2));
            DeleteObject(mFont2);

            //文字列3
            IntPtr mFont3 = CreateFont(attributeList[12], attributeList[13], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont3);
            String printString3 = stringList[3];
            TextOut(hdc, attributeList[14], attributeList[15], printString3, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString3));
            DeleteObject(mFont3);
            //文字列4
            IntPtr mFont4 = CreateFont(attributeList[16], attributeList[17], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont4);
            String printString4 = stringList[4];
            TextOut(hdc, attributeList[18], attributeList[19], printString4, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString4));
            DeleteObject(mFont4);

            //文字列5
            IntPtr mFont5 = CreateFont(attributeList[20], attributeList[21], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont5);
            String printString5 = stringList[5];
            TextOut(hdc, attributeList[22], attributeList[23], printString5, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString5));
            DeleteObject(mFont5);
            //文字列6
            IntPtr mFont6 = CreateFont(attributeList[24], attributeList[25], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont6);
            String printString6 = stringList[6];
            TextOut(hdc, attributeList[26], attributeList[27], printString6, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString6));
            DeleteObject(mFont6);

            //文字列7
            IntPtr mFont7 = CreateFont(attributeList[28], attributeList[29], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont7);
            String printString7 = stringList[7];
            TextOut(hdc, attributeList[30], attributeList[31], printString7, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString7));
            DeleteObject(mFont7);
            //文字列8
            IntPtr mFont8 = CreateFont(attributeList[32], attributeList[33], 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringFontName);
            SelectObject(hdc, mFont8);
            String printString8 = stringList[8];
            TextOut(hdc, attributeList[34], attributeList[35], printString8, Encoding.GetEncoding("Shift_JIS").GetByteCount(printString8));
            DeleteObject(mFont8);
        }
        #endregion
    }
}
