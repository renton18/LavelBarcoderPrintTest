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

            //縦文字枠線設定
            tbH11.Text = (int.Parse(tbX10.Text) - 2).ToString();
            tbY11.Text = (int.Parse(tbY10.Text) + 10).ToString();
            tbX11.Text = (int.Parse(tbH10.Text) + int.Parse(tbH11.Text) + int.Parse(tbX10.Text) + 2).ToString();
            tbW11.Text = (int.Parse(tbY10.Text) - int.Parse(tbW10.Text) * 6).ToString();

            //stringList.Clear();
            //stringList.Add(tbStr1.Text);
            //stringList.Add(tbStr2.Text);
            //stringList.Add(tbStr3.Text);
            //stringList.Add(tbStr4.Text);
            //stringList.Add(tbStr5.Text);
            //stringList.Add(tbStr6.Text);
            //stringList.Add(tbStr7.Text);
            //stringList.Add(tbStr8.Text);
            //stringList.Add(tbStr9.Text);
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode");
            dt.Columns.Add("str1");
            dt.Columns.Add("str2");
            dt.Columns.Add("str3");
            dt.Columns.Add("str4");
            dt.Columns.Add("str5");
            dt.Columns.Add("str6");
            dt.Columns.Add("str7");
            dt.Columns.Add("str8");
            dt.Columns.Add("str9");
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
            dt.Rows.Add(row);
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
            attributeList.AddRange(new int[] { int.Parse(tbH10.Text), int.Parse(tbW10.Text), int.Parse(tbX10.Text), int.Parse(tbY10.Text) });
            attributeList.AddRange(new int[] { int.Parse(tbH11.Text), int.Parse(tbW11.Text), int.Parse(tbX11.Text), int.Parse(tbY11.Text) });

            B_EV4D_GH17_R b_ev4D = new B_EV4D_GH17_R("", dt, "NW-7", stringList, attributeList);
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

        private void cbPattern_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).Text == "A")
            {
                tbX4.Text = "15";
                tbX5.Text = "220";
                tbX6.Text = "15";
                tbX7.Text = "220";
                tbX8.Text = "15";
                tbX9.Text = "220";

                tbStr10.Enabled = false;
                tbH10.Enabled = false;
                tbW10.Enabled = false;
                tbX10.Enabled = false;
                tbY10.Enabled = false;
                tbH11.Enabled = false;
                tbW11.Enabled = false;
                tbX11.Enabled = false;
                tbY11.Enabled = false;
            }
            else if (((ComboBox)sender).Text == "B")
            {
                tbX4.Text = "60";
                tbX5.Text = "260";
                tbX6.Text = "60";
                tbX7.Text = "260";
                tbX8.Text = "60";
                tbX9.Text = "260";

                tbStr10.Enabled = true;
                tbH10.Enabled = true;
                tbW10.Enabled = true;
                tbX10.Enabled = true;
                tbY10.Enabled = true;
                tbH11.Enabled = true;
                tbW11.Enabled = true;
                tbX11.Enabled = true;
                tbY11.Enabled = true;
            }
        }
    }
}
