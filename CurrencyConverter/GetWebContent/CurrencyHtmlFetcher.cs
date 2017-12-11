using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetWebContent
{
    /// <summary>
    /// 使用本类，可以从中国银行获取货币汇率
    /// </summary>
    public class CurrencyExchangeRateHtmlFetcher
    {
        DataTable _dataTable;// = new DataTable();
        private string _srcHtmlUrl,_pageSrcCode;
        /// <summary>
        /// 数据结果
        /// </summary>
        public DataTable DataTable
        {
            get
            {
                return _dataTable;
            }
        }

        public string SrcHtmlUrl
        {
            get
            {
                return _srcHtmlUrl;
            }

            set
            {
                _srcHtmlUrl = value;
            }
        }

        public event EventHandler CurrencyExchangeFetched;

        public CurrencyExchangeRateHtmlFetcher()
        {
            _dataTable = new DataTable();
            SrcHtmlUrl = "http://www.boc.cn/sourcedb/whpj/enindex.html";
        }


        public void BeginFetchCurrencyExchangeRate()
        {
            using (WebClient wc = new WebClient())
            {
                wc.Credentials = CredentialCache.DefaultCredentials;
                Encoding enc = Encoding.GetEncoding("UTF-8"); // 如果是乱码就改成 UTF-8 / GB2312       
                                                              //方法三(异步)：
                wc.DownloadStringCompleted += Wc_DownloadStringCompleted;
                wc.DownloadStringAsync(new Uri(_srcHtmlUrl));
            }               
        }

        public Task<DataTable> FetchCurrencyExchangeRateAsync()
        {
            return  Task<DataTable>.Run(() =>
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Credentials = CredentialCache.DefaultCredentials;
                    Encoding enc = Encoding.GetEncoding("UTF-8"); // 如果是乱码就改成 UTF-8 / GB2312                                                              
                    string htmlSrcCode = wc.DownloadString(new Uri(_srcHtmlUrl));
                    _dataTable = ParseHtmlSrcCodeToRate(htmlSrcCode);
                    return _dataTable;
                }
            });
        }

        private void Wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            _pageSrcCode = e.Result;
            ParseSrcHtmlCode();
        }


        private void ParseSrcHtmlCode()
        {
            _dataTable = ParseHtmlSrcCodeToRate(_pageSrcCode);
            CurrencyExchangeFetched(this,new EventArgs());
        }


        private DataTable ParseHtmlSrcCodeToRate(string strHtml)
        {
            DataTable dt = new DataTable();
            DataColumn col1 = new DataColumn("Currency Name", typeof(string));
            DataColumn col2 = new DataColumn("Buying Rate", typeof(string));
            DataColumn col3 = new DataColumn("Cash Buying Rate", typeof(string));
            DataColumn col4 = new DataColumn("Selling Rate", typeof(string));
            DataColumn col5 = new DataColumn("Cash Selling Rate", typeof(string));
            DataColumn col6 = new DataColumn("Middle Rate", typeof(string));
            DataColumn col7 = new DataColumn("Pub Time", typeof(string));

            dt.Columns.Add(col1);
            dt.Columns.Add(col2);
            dt.Columns.Add(col3);
            dt.Columns.Add(col4);
            dt.Columns.Add(col5);
            dt.Columns.Add(col6);
            dt.Columns.Add(col7);


            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(strHtml);

            doc.OptionOutputAsXml = true;
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode(".//table[tr/th=\"Currency Name\"]");
            if (node == null)
            {
                throw new Exception ("没有找到有th为Currency Name列的table");
            }
            HtmlAgilityPack.HtmlNodeCollection trNodeList = node.SelectNodes("tr[td]");

            foreach (HtmlAgilityPack.HtmlNode trNode in trNodeList)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 7; j++)
                {
                    HtmlAgilityPack.HtmlNodeCollection tdNodeList = trNode.SelectNodes("td");
                    dr[j] = tdNodeList[j].InnerText.Replace("&nbsp;", " "); ;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
