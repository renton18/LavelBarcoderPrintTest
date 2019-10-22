using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LavelBarcoderPrintTest
{

    static class COMMON
    {
        public static string mode = "";
        //接続文字列
        public static string NIBSConnecttion = "";
        public static string TRUSTConnection = "";
        public static string OTHERConnection = "";

        //印刷先プリンタ
        public static string ListPrinter = "";
        public static string EnvelopePrinter = "";
        public static string LavelPrinter = "";

        [DllImport("KERNEL32.DLL")]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        #region INIファイルから設定を取得する
        /// <summary>
        ///　INIファイルから設定を取得する
        /// </summary>
        public static Boolean SetConfig()
        {
#if DEBUG == true
            mode = "テスト環境";
            //iniファイルからプリンタ名を取得する
            var iniFile = Application.StartupPath + @"\\" + "Settings.ini";

            //プリンタ設定を取得する
            StringBuilder sb1 = new StringBuilder(1024);
            StringBuilder sb2 = new StringBuilder(1024);
            StringBuilder sb3 = new StringBuilder(1024);
            GetPrivateProfileString("ListPrinter", "Name", "NG", sb1, Convert.ToUInt32(sb1.Capacity), iniFile);
            GetPrivateProfileString("EnvelopePrinter", "Name", "NG", sb2, Convert.ToUInt32(sb2.Capacity), iniFile);
            GetPrivateProfileString("LavelPrinter", "Name", "NG", sb3, Convert.ToUInt32(sb3.Capacity), iniFile);
            if (sb1.ToString() == "NG" || sb2.ToString() == "NG" || sb3.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした：" + Environment.NewLine + iniFile);
                return false;
            }
            COMMON.ListPrinter = sb1.ToString();
            COMMON.EnvelopePrinter = sb2.ToString();
            COMMON.LavelPrinter = sb3.ToString();

            //ネットワーク設定取得する
            StringBuilder sb4 = new StringBuilder(1024);
            GetPrivateProfileString("NIBS_TEST", "Name", "NG", sb4, Convert.ToUInt32(sb4.Capacity), iniFile);
            if (sb4.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした" + Environment.NewLine + iniFile + Environment.NewLine + "項目名： NIBS_TEST");
                return false;
            }
            COMMON.NIBSConnecttion = sb4.ToString();

            StringBuilder sb5 = new StringBuilder(1024);
            GetPrivateProfileString("TRUST_TEST", "Name", "NG", sb5, Convert.ToUInt32(sb5.Capacity), iniFile);
            if (sb5.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした" + Environment.NewLine + iniFile + Environment.NewLine + "項目名： TRUST_TEST");
                return false;
            }
            COMMON.TRUSTConnection = sb5.ToString();

#else
            mode = "本番";
            //iniファイルからプリンタ名を取得する
            var iniFile = Directory.GetParent(Application.StartupPath) + @"\" + "Settings.ini";

            //プリンタ設定を取得する
            StringBuilder sb1 = new StringBuilder(1024);
            StringBuilder sb2 = new StringBuilder(1024);
            StringBuilder sb3 = new StringBuilder(1024);
            GetPrivateProfileString("ListPrinter", "Name", "NG", sb1, Convert.ToUInt32(sb1.Capacity), iniFile);
            GetPrivateProfileString("EnvelopePrinter", "Name", "NG", sb2, Convert.ToUInt32(sb2.Capacity), iniFile);
            GetPrivateProfileString("LavelPrinter", "Name", "NG", sb3, Convert.ToUInt32(sb3.Capacity), iniFile);
            if (sb1.ToString() == "NG" || sb2.ToString() == "NG" || sb3.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした：" + iniFile);
                return false;
            }
            COMMON.ListPrinter = sb1.ToString();
            COMMON.EnvelopePrinter = sb2.ToString();
            COMMON.LavelPrinter = sb3.ToString();

            //ネットワーク設定を取得する
            StringBuilder sb4 = new StringBuilder(1024);
            GetPrivateProfileString("NIBS", "Name", "NG", sb4, Convert.ToUInt32(sb4.Capacity), iniFile);
            if (sb4.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした" + Environment.NewLine + iniFile + "項目名： NIBS");
                return false;
            }
            COMMON.NIBSConnecttion = sb4.ToString();

            StringBuilder sb5 = new StringBuilder(1024);
            GetPrivateProfileString("TRUST", "Name", "NG", sb5, Convert.ToUInt32(sb5.Capacity), iniFile);
            if (sb5.ToString() == "NG")
            {
                MessageBox.Show("INIファイルが読み取れませんでした" + Environment.NewLine + iniFile + "項目名： TRUST");
                return false;
            }
            COMMON.TRUSTConnection = sb5.ToString();

#endif

            return true;
        }
        #endregion

        #region テキストのログファイルを出力する
        /// <summary>
        ///　テキストのログファイルを出力する
        /// </summary>
        public static void OutputLogFile(string message)
        {
            File.AppendAllText("ErrorLog.txt", DateTime.Now.ToString("yyyy/MM/dd (dddd) hh時mm分ss秒") + " " + message + Environment.NewLine);
        }
        #endregion

        #region フォームの設定する
        /// <summary>
        ///　フォームの設定する
        /// </summary>
        public static void SetForm(Form form)
        {
            //ステータスバーを表示する
            form.Text = form.Text + "   ホスト名 = " + Dns.GetHostName();
            form.ForeColor = COMMON.mode == "テスト環境" ? Color.Brown : SystemColors.Control;
        }
        #endregion

    }
}
