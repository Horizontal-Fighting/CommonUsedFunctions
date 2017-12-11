using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;

namespace GetWebContent
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        CurrencyExchangeRateHtmlFetcher currencyExchangeRateHtmlFetcher;
        private void button1_Click(object sender, EventArgs e)
        {        
            currencyExchangeRateHtmlFetcher = new CurrencyExchangeRateHtmlFetcher();
            currencyExchangeRateHtmlFetcher.SrcHtmlUrl = tb_url.Text.Trim();
            currencyExchangeRateHtmlFetcher.CurrencyExchangeFetched += CurrencyExchangeRateHtmlFetcher_CurrencyExchangeFetched;
            currencyExchangeRateHtmlFetcher.BeginFetchCurrencyExchangeRate();
        }

        private void CurrencyExchangeRateHtmlFetcher_CurrencyExchangeFetched(object sender, EventArgs e)
        {
            dataGridView1.DataSource = currencyExchangeRateHtmlFetcher.DataTable;
            //GetRate(tb_html.Text);
            MessageBox.Show("OK", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region 提取URL地址
        //    publicstaticvoidMain(string[]args)
        //{　　　　　
        //  stringtext="I'vefoundthisamazingURLathttp://www.sohu.com,andthenfindftp://ftp.sohu.comisbetter.";
        //  stringpattern=@"b(S+)://(S+)b";　//匹配URL的模式
        //  MatchCollectionmc=Regex.Matches(text,pattern);//满足pattern的匹配集合
        //  Console.WriteLine("文本中包含的URL地址有：");
        //  foreach(Matchmatchinmc)
        //  {
        //    Console.WriteLine(match.ToString());
        //  }
        //  Console.Read();
        //}
        ///*
        //*运行后输出如下：
        //*文本中包含的URL地址有：
        //*http://www.sohu.com
        //*ftp://ftp.sohu.com
        //*/
        #endregion

    }
}
