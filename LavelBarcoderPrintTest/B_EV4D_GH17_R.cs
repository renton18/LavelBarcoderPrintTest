using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace LavelBarcoderPrintTest
{
    public class B_EV4D_GH17_R
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern System.IntPtr CreateFont(int nHeight, int nWidth, int nEscapement, int nOrientation, int fnWeight, bool fdwItalic, bool fdwUnderline, bool fdwStrikeOut, int fdwCharSet, int fdwOutputPrecision, int fdwClipPrecision, int fdwQuality, int fdwPitchAndFamily, string lpszFace);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern System.IntPtr SelectObject(System.IntPtr hObject, System.IntPtr hFont);
        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        static extern int TextOut(IntPtr hdc, int nXStart, int nYStart, string lpString, int cbString);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern bool DeleteObject(System.IntPtr hObject);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateSolidBrush(uint crColor);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern bool FillRgn(IntPtr hdc, IntPtr hrgn, IntPtr hbr);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern int SetTextColor(IntPtr hdc, int crColor);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern int SetBkColor(IntPtr hdc, int crColor);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        int pageCount = 0;
        string printerName = "";
        DataTable dt = new DataTable();
        Bitmap canvas;
        List<string[]> stringList = new List<string[]>();
        List<string[]> frameList = new List<string[]>();
        List<string[]> hyoudaiList = new List<string[]>();

        #region コンストラクタ
        public B_EV4D_GH17_R(string printerName, DataTable dt, List<string[]> stringList, List<string[]> frameList, List<string[]> hyoudaiList)
        {
            this.frameList = frameList;
            this.stringList = stringList;
            this.hyoudaiList = hyoudaiList;
            this.printerName = printerName;
            this.dt = dt;
        }
        #endregion

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
            //データテーブルにデータがない場合
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("印刷するデータがありません。");
                return;
            }

            pageCount = 0;
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = printerName;
            //プリンタが設定されてない場合はXPSファイルとして出力する
            if (pd.PrinterSettings.PrinterName == "")
            {
                pd.PrinterSettings.PrinterName = "Microsoft XPS Document Writer";
                pd.PrinterSettings.PrintFileName = "test.xps";
                //ポップアップを表示させない
                pd.DefaultPageSettings.PrinterSettings.PrintToFile = true;
                pd.PrintController = new StandardPrintController();
                Process.Start(Application.StartupPath);
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
            //縦文字囲い
            Rectangle(hdc, int.Parse(frameList[0][0]), int.Parse(frameList[0][1]), int.Parse(frameList[0][2]), int.Parse(frameList[0][3]));
            //文字列、バーコード、縦文字
            var colCount = 0;
            foreach (var item in stringList)
            {
                SetString(hdc, item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7]);
                //本番時
                //SetString(hdc, item[0], item[1], item[2], item[3], dt.Rows[pageCount][colCount].ToString()., item[5], item[6], item[7]);
                colCount++;
            }
            colCount = 0;
            foreach (var item in hyoudaiList)
            {
                SetString(hdc, item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7]);
                colCount++;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //バーコード
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //バーコードの印字 バーコードの場合は9番目の引数を0にする必要あり
            //String barString = dt.Rows[pageCount][0].ToString();
            //IntPtr barFont1 = CreateFont(stringList[0], stringList[1], 0, 0, 0, false, false, false, 0, 0, 0, 0, 0, stringList[0][2]);
            //SelectObject(hdc, barFont1);
            //TextOut(hdc, stringList[2], stringList[3], barString, barString.Length);
            //DeleteObject(barFont1);
            //IntPtr barFont11 = CreateFont(stringList[0] / 5, stringList[1] / 2, 0, 0, 0, false, false, false, 128, 0, 0, 0, 0, stringList[0][1]);
            //SelectObject(hdc, barFont11);
            //TextOut(hdc, stringList[2] + stringList[0], stringList[3] + stringList[1] * 2, barString, Encoding.GetEncoding("Shift_JIS").GetByteCount(barString));
            //DeleteObject(barFont11);
            //SetString(hdc, stringList[0][0], stringList[0][1], stringList[0][2], stringList[0][3], dt.Rows[pageCount][0].ToString(), stringList[0][4], stringList[0][5], stringList[0][6]);
            //SetString(hdc, stringList[1][0], stringList[1][1], stringList[1][2], stringList[1][3], dt.Rows[pageCount][1].ToString(), stringList[1][4], stringList[1][5], stringList[1][6]);
            //SetString(hdc, stringList[2][0], stringList[2][1], stringList[2][2], stringList[2][3], dt.Rows[pageCount][2].ToString(), stringList[2][4], stringList[2][5], stringList[2][6]);
            //SetString(hdc, stringList[3][0], stringList[3][1], stringList[3][2], stringList[3][3], dt.Rows[pageCount][3].ToString(), stringList[3][4], stringList[3][5], stringList[3][6]);
            //SetString(hdc, stringList[4][0], stringList[4][1], stringList[4][2], stringList[4][3], dt.Rows[pageCount][4].ToString(), stringList[4][4], stringList[4][5], stringList[4][6]);
            //SetString(hdc, stringList[5][0], stringList[5][1], stringList[5][2], stringList[5][3], dt.Rows[pageCount][5].ToString(), stringList[5][4], stringList[5][5], stringList[5][6]);
            //SetString(hdc, stringList[6][0], stringList[6][1], stringList[6][2], stringList[6][3], dt.Rows[pageCount][6].ToString(), stringList[6][4], stringList[6][5], stringList[6][6]);
            //SetString(hdc, stringList[7][0], stringList[7][1], stringList[7][2], stringList[7][3], dt.Rows[pageCount][7].ToString(), stringList[7][4], stringList[7][5], stringList[7][6]);
            //SetString(hdc, stringList[8][0], stringList[8][1], stringList[8][2], stringList[8][3], dt.Rows[pageCount][8].ToString(), stringList[8][4], stringList[8][5], stringList[8][6]);
            //SetString(hdc, stringList[9][0], stringList[9][1], stringList[9][2], stringList[9][3], dt.Rows[pageCount][9].ToString(), stringList[9][4], stringList[9][5], stringList[9][6]);

        }

        //barcorde=0  バーコード
        //barcorde=128  文字列（デフォルト）
        //verticalString=900 縦文字
        //verticalString=0 横文字（デフォルト）
        private void SetString(IntPtr hdc, string h, string w, string x, string y, string data, string dataFont, string barcorde, string verticalStrng)
        {
            if (h != "" || w != "" || x != "" || y != "")
            {
                IntPtr mFont = CreateFont(int.Parse(h), int.Parse(w), 0, 0, 0, false, false, false, int.Parse(barcorde), 0, 0, 0, 0, dataFont);
                SelectObject(hdc, mFont);
                TextOut(hdc, int.Parse(x), int.Parse(y), data, Encoding.GetEncoding("Shift_JIS").GetByteCount(data));
                DeleteObject(mFont);
            }
        }
        #endregion
    }
}
