using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace BankingSystemApp.ExceptionHandling
{
    public class ErrorModel
    {
        public ErrorModel(HttpStatusCode _statusCode, string _content)
        {
            StatusCode = _statusCode;
            Content = _content;
        }


        public HttpStatusCode StatusCode { get; set; }
        public string Content { get; set; }
    }
}