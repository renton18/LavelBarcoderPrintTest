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

        List<string[]> frameList = new List<string[]>();
        List<string[]> stringList = new List<string[]>();

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
            B_EV4D_GH17_R b_ev4D = new B_EV4D_GH17_R(cbPrinter.Text, new DataTable(), stringList, frameList);
            pictureBox1.Image = b_ev4D.GetPrintImage(pictureBox1);
            b_ev4D.Print();
        }

        private void btnHyouji_Click(object sender, EventArgs e)
        {
            //if (sender.GetType().Equals(typeof(ComboBox)))
            //{

            //    if (((ComboBox)sender).Text == "")
            //    {
            //        return;
            //    }
            //}
            if (sender.GetType().Equals(typeof(TextBox)))
            {
                if (((TextBox)sender).Text == "")
                {
                    return;
                }
            }

            //サンプル値
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode1");
            dt.Columns.Add("str1");
            dt.Columns.Add("str2");
            dt.Columns.Add("str3");
            dt.Columns.Add("str4");
            dt.Columns.Add("str5");
            dt.Columns.Add("str6");
            dt.Columns.Add("str7");
            dt.Columns.Add("str8");
            dt.Columns.Add("str9");
            dt.Columns.Add("Barcode2");
            DataRow row = dt.NewRow();
            row[0] = tbStr1.Text;
            row[1] = tbStr2.Text;
            row[2] = tbStr3.Text;
            row[3] = tbStr4.Text;
            row[4] = tbStr5.Text;
            row[5] = tbStr6.Text;
            row[6] = tbStr7.Text;
            row[7] = tbStr8.Text;
            row[8] = tbStr9.Text;
            row[9] = tbStr10.Text;
            row[10] = tbStr12.Text;
            dt.Rows.Add(row);

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // コピペ対象コード
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            stringList.Clear();
            //文字列
            if (tbStr1.Text.Trim() != "") stringList.Add(new string[] { tbH1.Text, tbW1.Text, tbX1.Text, tbY1.Text, tbStr1.Text, comboBox1.Text, "128", "0" });
            if (tbStr2.Text.Trim() != "") stringList.Add(new string[] { tbH2.Text, tbW2.Text, tbX2.Text, tbY2.Text, tbStr2.Text, comboBox2.Text, "128", "0" });
            if (tbStr3.Text.Trim() != "") stringList.Add(new string[] { tbH3.Text, tbW3.Text, tbX3.Text, tbY3.Text, tbStr3.Text, comboBox3.Text, "128", "0" });
            if (tbStr4.Text.Trim() != "") stringList.Add(new string[] { tbH4.Text, tbW4.Text, tbX4.Text, tbY4.Text, tbStr4.Text, comboBox4.Text, "128", "0" });
            if (tbStr5.Text.Trim() != "") stringList.Add(new string[] { tbH5.Text, tbW5.Text, tbX5.Text, tbY5.Text, tbStr5.Text, comboBox5.Text, "128", "0" });
            if (tbStr6.Text.Trim() != "") stringList.Add(new string[] { tbH6.Text, tbW6.Text, tbX6.Text, tbY6.Text, tbStr6.Text, comboBox6.Text, "128", "0" });
            if (tbStr7.Text.Trim() != "") stringList.Add(new string[] { tbH7.Text, tbW7.Text, tbX7.Text, tbY7.Text, tbStr7.Text, comboBox7.Text, "128", "0" });
            if (tbStr8.Text.Trim() != "") stringList.Add(new string[] { tbH8.Text, tbW8.Text, tbX8.Text, tbY8.Text, tbStr8.Text, comboBox8.Text, "128", "0" });
            if (tbStr9.Text.Trim() != "") stringList.Add(new string[] { tbH9.Text, tbW9.Text, tbX9.Text, tbY9.Text, tbStr9.Text, comboBox9.Text, "128", "0" });
            if (tbStr10.Text.Trim() != "") stringList.Add(new string[] { tbH10.Text, tbW10.Text, tbX10.Text, tbY10.Text, tbStr10.Text, comboBox10.Text, "128", "0" });
            if (tbStr11.Text.Trim() != "") stringList.Add(new string[] { tbH11.Text, tbW11.Text, tbX11.Text, tbY11.Text, tbStr11.Text, comboBox11.Text, "128", "0" });
            if (tbStr12.Text.Trim() != "") stringList.Add(new string[] { tbH12.Text, tbW12.Text, tbX12.Text, tbY12.Text, tbStr12.Text, comboBox12.Text, "128", "0" });
            if (tbStr13.Text.Trim() != "") stringList.Add(new string[] { tbH13.Text, tbW13.Text, tbX13.Text, tbY13.Text, tbStr13.Text, comboBox13.Text, "128", "0" });
            if (tbStr14.Text.Trim() != "") stringList.Add(new string[] { tbH14.Text, tbW14.Text, tbX14.Text, tbY14.Text, tbStr14.Text, comboBox14.Text, "128", "0" });
            if (tbStr15.Text.Trim() != "") stringList.Add(new string[] { tbH15.Text, tbW15.Text, tbX15.Text, tbY15.Text, tbStr15.Text, comboBox15.Text, "128", "0" });
            if (tbStr16.Text.Trim() != "") stringList.Add(new string[] { tbH16.Text, tbW16.Text, tbX16.Text, tbY16.Text, tbStr16.Text, comboBox16.Text, "128", "0" });
            if (tbStr17.Text.Trim() != "") stringList.Add(new string[] { tbH17.Text, tbW17.Text, tbX17.Text, tbY17.Text, tbStr17.Text, comboBox17.Text, "128", "0" });
            if (tbStr18.Text.Trim() != "") stringList.Add(new string[] { tbH18.Text, tbW18.Text, tbX18.Text, tbY18.Text, tbStr18.Text, comboBox18.Text, "128", "0" });
            if (tbStr19.Text.Trim() != "") stringList.Add(new string[] { tbH19.Text, tbW19.Text, tbX19.Text, tbY19.Text, tbStr19.Text, comboBox19.Text, "128", "0" });
            if (tbStr20.Text.Trim() != "") stringList.Add(new string[] { tbH20.Text, tbW20.Text, tbX20.Text, tbY20.Text, tbStr20.Text, comboBox20.Text, "128", "0" });
            //バーコード
            if (tbStr21.Text.Trim() != "") stringList.Add(new string[] { tbH21.Text, tbW21.Text, tbX21.Text, tbY21.Text, tbStr21.Text, comboBox21.Text, "0", "0" });
            if (tbStr22.Text.Trim() != "") stringList.Add(new string[] { tbH22.Text, tbW22.Text, tbX22.Text, tbY22.Text, tbStr22.Text, comboBox22.Text, "128", "0" });
            if (tbStr23.Text.Trim() != "") stringList.Add(new string[] { tbH23.Text, tbW23.Text, tbX23.Text, tbY23.Text, tbStr23.Text, comboBox23.Text, "0", "0" });
            if (tbStr24.Text.Trim() != "") stringList.Add(new string[] { tbH24.Text, tbW24.Text, tbX24.Text, tbY24.Text, tbStr24.Text, comboBox24.Text, "128", "0" });
            //枠あり文字
            if (tbStr25.Text.Trim() != "") stringList.Add(new string[] { tbH25.Text, tbW25.Text, tbX25.Text, tbY25.Text, tbStr25.Text, comboBox25.Text, "128",
                cbVertival.Checked == true ? "900" : "0"});
            frameList.Clear();
            if (tbStr25.Text.Trim() != "") frameList.Add(new string[] { tbH26.Text, tbW26.Text, tbX26.Text, tbY26.Text });


            // コピペ対象コード　ここまで
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            B_EV4D_GH17_R b_ev4D = new B_EV4D_GH17_R("", dt, stringList, frameList);
            pictureBox1.Image = b_ev4D.GetPrintImage(pictureBox1);
        }

        #region ソースコピー
        private void btnCopySource_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"List<string[]> stringList = new List<string[]>();" + Environment.NewLine);
            //sb.Append("stringList.Clear();" + Environment.NewLine);
            for (int i = 0; i < stringList.Count; i++)
            {
                sb.Append(@"stringList.Add(new string[] {""" + stringList[i][0] + @""", """ + stringList[i][1] + @""", """ + stringList[2] + @""", """ + stringList[i][3] + @""", """  + stringList[i][5] + @""", """ + stringList[i][6] + @""", """ + stringList[i][7] + @""" " + "});" + Environment.NewLine);
            }

            sb.Append(Environment.NewLine);
            sb.Append(@"List<string[]> frameList = new List<string[]>();" + Environment.NewLine);
            //sb.Append("frameList.Clear();" + Environment.NewLine);
            for (int i = 0; i < frameList.Count; i++)
            {
                sb.Append(@"frameList.Add(new string[] {""" + frameList[i][0] + @""", """ + frameList[i][1] + @""", """ + frameList[i][2] + @""", """ + frameList[i][3] + @""" " + "});" + Environment.NewLine);
            }

            //クリップボードにコピーする
            Clipboard.SetText(sb.ToString());
        }
        #endregion

        #region パターンチェンジ
        private void cbPattern_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "A")
            {

            }
        }
        #endregion
    }
}
