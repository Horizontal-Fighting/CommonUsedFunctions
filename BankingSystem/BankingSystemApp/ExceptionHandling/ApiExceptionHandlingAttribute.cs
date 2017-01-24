using BankingSystemApp.ExceptionHandling;
using BankingSystemApp.Log;
using NuGet.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace BankingSystemApp
{
    public class ApiExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// global error handler
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            //self defined error 

            //log exception
            RecordLog recordLog = RecordLogProvider.GetInstance(Common.logFile);
            recordLog.WriteLogFile(context.Exception.ToString());

            if (context.Exception is NotImplementedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    //RETURN JSON Error
                    Content = new StringContent(JsonHelper.ToJson(new ErrorModel(HttpStatusCode.NotImplemented, context.Exception.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "NotImplementedException"
                });
            }
            else if (context.Exception is TimeoutException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.RequestTimeout)
                {
                    //return json object
                    Content = new StringContent(JsonHelper.ToJson(new ErrorModel(HttpStatusCode.RequestTimeout, context.Exception.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "TimeoutException"
                });
            }
            //.....can not find error type， return server error: 500
            else
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    //return json object
                    Content = new StringContent(JsonHelper.ToJson(new ErrorModel(HttpStatusCode.InternalServerError,  context.Exception.Message)), Encoding.UTF8, "application/json"),
                    ReasonPhrase = "InternalServerErrorException"
                });
            }

        }
    }
}