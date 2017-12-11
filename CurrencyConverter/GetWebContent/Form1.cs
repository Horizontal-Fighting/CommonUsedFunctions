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

        private void button1_Click(object sender, EventArgs e)
        {
           
            tb_html.Text = GetWebContent(tb_url.Text.Trim()).Trim();
            GetRate(tb_html.Text);
            MessageBox.Show("OK", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        ///<summary> 实时汇率换算(人民币to外汇) </summary>   
        /// <param name="name">外汇名称,如:英镑、港币、美元、日元、欧元</param>    
        /// <param name="money">人民币价格</param>   
        /// <returns>返回实际外汇价格</returns>    
        //public Decimal GetRMB(string name, decimal money)
        //{
        //    if (name != "人民币")   
        //    {   
        //        string wsrcString = GetWebContent("http://www.boc.cn/cn/common/whpj.html");//远程获取中行最新汇率   
        //        wsrcString = wsrcString.Substring(wsrcString.IndexOf(name), wsrcString.Length - wsrcString.IndexOf(name));   
        //        string regexStr = "<td bgcolor=\"#f7f7f7\" style="\" mce_style="\"border-bottom: #bcbbbb 1px dashed;\">(?<key>.*?)</td>";   
        //        Regex r = new Regex(regexStr, RegexOptions.None);   
        //        Match mc = r.Match(wsrcString);   
        //        money = 100 / decimal.Parse(mc.Groups["key"].Value) * money;   
        //        return Math.Round(Convert.ToDecimal(money), 2);   
        //    }   
        //    else  
        //    {   
        //        return money;   
        //    }   
        //}

        /// <summary> 获取远程HTML内容</summary>   
        /// <param name="url">远程网页地址URL</param>   
        /// <returns>成功返回远程HTML的代码内容</returns>   
        private string GetWebContent(string strUrl)
        {
            string str = "";
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = CredentialCache.DefaultCredentials;
                Encoding enc = Encoding.GetEncoding("UTF-8");// 如果是乱码就改成 UTF-8 / GB2312            

                //方法一:
                //Byte[] buffer = wc.DownloadData(strUrl);// 从资源下载数据并返回字节数组
                //str = enc.GetString(buffer);// 输出字符串(HTML代码)

                //方法二:
                Stream res = wc.OpenRead(strUrl);//以流的形式打开URL
                //Stream res = new FileStream(@"E:\ExchangeRate\enindex.html", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(res, enc);//以指定的编码方式读取数据流
                str = sr.ReadToEnd();//输出(HTML代码)
                res.Close();

                wc.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return str;
        }

        #region 产生汇率表
        //private void GetRate(string strHtml)
        //{
            
        //    try
        //    {
        //        //string regexStr = "<th width=\"86\" bgcolor=\"#EFEFEF\">(?<key>?)</th>";
        //        //Regex r = new Regex(regexStr, RegexOptions.None);
        //        //MatchCollection mc=r.Matches(strHtml);
        //        //string str = "";
        //        //for (int i = 0; i < mc.Count; i++)
        //        //{
        //        //    str += mc[i].Value.ToString() + ";";
        //        //}
        //        try
        //        {
        //            dataGridView1.Rows.Clear();
        //            dataGridView1.Columns.Clear();
        //        }
        //        catch { }
        //        string str1 = "<th width=\"75\" bgcolor=\"#EFEFEF\">";
        //        while (strHtml.Contains(str1))
        //        {
        //            string strName = strHtml.Substring(strHtml.IndexOf(str1) + str1.Length, strHtml.IndexOf("</th>") - strHtml.IndexOf(str1) - str1.Length);
        //            DataGridViewTextBoxColumn Column = new DataGridViewTextBoxColumn();
        //            Column.Width = 100;
        //            Column.HeaderText = strName;
        //            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column });
        //            strHtml = strHtml.Substring(strHtml.IndexOf("</th>") + "</th>".Length).Trim();
        //        }

        //        str1 = "<tr align=\"center\">";
        //        strHtml=strHtml.Substring(strHtml.IndexOf(str1));
        //        while (strHtml.Contains(str1))
        //        {
        //            string strData = strHtml.Substring(strHtml.IndexOf(str1) + str1.Length, strHtml.IndexOf("</tr>") - strHtml.IndexOf(str1) - str1.Length);
        //            dataGridView1.Rows.Add(1);
        //            string str2 = "<td bgcolor=\"#FFFFFF\">";
        //            int i = 0;
        //            while (strData.Contains(str2))
        //            {
        //                string strDD = strData.Substring(strData.IndexOf(str2) + str2.Length, strData.IndexOf("</td>") - strData.IndexOf(str2) - str2.Length);
        //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = strDD.Replace(@"\r\n", "");
        //                strData = strData.Substring(strData.IndexOf("</td>") + "</td>".Length).Trim();
        //                i++;
        //            }
        //            strHtml = strHtml.Substring(strHtml.IndexOf("</tr>") + "</tr>".Length).Trim();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //       // MessageBox.Show(ex.Message, "Get Rate Err", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        private void GetRate(string strHtml)
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
                MessageBox.Show("没有找到有th为Currency Name列的table", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HtmlAgilityPack.HtmlNodeCollection trNodeList = node.SelectNodes("tr[td]");
   
            foreach(HtmlAgilityPack.HtmlNode trNode in trNodeList)
            {              
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < 7; j++) 
                    {
                        HtmlAgilityPack.HtmlNodeCollection tdNodeList = trNode.SelectNodes("td");
                        dr[j] = tdNodeList[j].InnerText.Replace("&nbsp;", " "); ;
                    }
                    dt.Rows.Add(dr);
            }
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dt;
    

        }

     
        #endregion

        private void tb_url_TextChanged(object sender, EventArgs e)
        {

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
