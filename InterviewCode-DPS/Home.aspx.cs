using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Interview1
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string userName="vtrax$16";
            string pwd = "test16";
            //doto no bind now
            string url1 = "http://localhost/DelTraxShopRS/DelTraxShopRS.svc/logintraxid/vtrax$16/test16/0/0";
            string resultStr1 = Global.get(url1);
            Console.WriteLine(resultStr1);
            Label3.Text = resultStr1;

            //todo: get shop_id, time consuming operation
            string shop_id = GetShopId(resultStr1);
            Label6.Text = shop_id;

            //acquire data from shop_id
            string url2 = "http://localhost/DelTraxShopRS/DelTraxShopRS.svc/dealers/16309/0";
            string resultStr2 = Global.get(url2);
            //Label4.Text=resultStr2;

            
            //parse dealers info
            var dealers = ParseDealerInfo(resultStr2);

            //bind data to table
            BindDataToTable(dealers);

        }

        private List<Dealer> ParseDealerInfo(string jsonStr)
        {
            JObject mJObj = JObject.Parse(jsonStr);
            string tmp1 = (string)mJObj["GetShopDealersResult"];
            mJObj = JObject.Parse(tmp1);
            var tmp2 = (JArray)mJObj["Data"];
            List<Dealer> dealers = new List<Dealer>();
            foreach (var tmp3 in tmp2)
            {
                Console.WriteLine(tmp3.ToString());
                dealers.Add(GetDealer(tmp3.ToString()));
            }
            return dealers;
        }

        private void BindDataToTable(List<Dealer> dealers)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dealer_id");
            dt.Columns.Add("dealer");
            dt.Columns.Add("ship_vendor_id");
            dt.Columns.Add("ems_make_id");

            foreach (var tmpDealer in dealers)
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = tmpDealer.dealer_id;
                NewRow[1] = tmpDealer.dealer;
                NewRow[2] = tmpDealer.ship_vendor_id;
                NewRow[3] = tmpDealer.ems_make_id;// tmpdeal.Text;
                dt.Rows.Add(NewRow); 
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();
            //GridView1.DataSource = dt;
            //GridViewl.DataBind();
        }

        private Dealer GetDealer(string jsonStr)
        {
            Dealer resDealer = new Dealer();
            JObject mJObj = JObject.Parse(jsonStr);
            resDealer.dealer_id = (int)mJObj["dealer_id"];
            resDealer.dealer = mJObj["dealer"].ToString();
            resDealer.ship_vendor_id = (int)mJObj["ship_vendor_id"];
            resDealer.ems_make_id = (int)mJObj["ems_make_id"];
            return resDealer;
        }

        private string GetShopId(string jsonStr)
        {
            JObject mJObj = JObject.Parse(jsonStr);
            string tmp1 = (string)mJObj["VerifyLoginTraxIdResult"];
            mJObj = JObject.Parse(tmp1);
            var tmp2 = mJObj["Data"][0];
            var tmp3 = tmp2["shop_id"];
            return (string)tmp3;
        }

      
    }
}