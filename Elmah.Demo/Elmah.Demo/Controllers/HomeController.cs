using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Elmah.Demo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //throw new ArgumentNullException();
            return View();
        }

        [HttpPost]
        public ActionResult GenerateError(string error)
        {
            throw new ApplicationException(error);
        }

    }
}