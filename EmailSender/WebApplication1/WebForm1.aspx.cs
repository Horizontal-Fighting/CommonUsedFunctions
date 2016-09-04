//Created by: Rong Fan
//Email:rong.fan1031@gmail.com
//2016-9-4
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Send_Click(object sender, EventArgs e)
        {
            Email email = new Email();
            email.mailFrom = "发送人的邮箱地址";
            email.mailPwd = "发送人邮箱的密码";
            email.mailSubject = "邮件主题";
            email.mailBody = "邮件内容";
            email.isbodyHtml = true;    //是否是HTML
            email.host = "smtp.126.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
            email.mailToArray = new string[] { "******@qq.com","12345678@qq.com"};//接收者邮件集合
            email.mailCcArray = new string[] { "******@qq.com" };//抄送者邮件集合
            if (email.Send())
            {
                Response.Write("<script type='text/javascript'>alert('发送成功！');history.go(-1)</script>");//发送成功则提示返回当前页面；

            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('发送失败！');history.go(-1)</script>");
            }
        }

    }
}