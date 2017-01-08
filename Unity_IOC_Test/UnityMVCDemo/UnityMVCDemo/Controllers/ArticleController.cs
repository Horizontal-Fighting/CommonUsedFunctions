using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnityMVCDemo.Models;

namespace UnityMVCDemo.Controllers
{
    public class ArticleController : Controller
    {
        readonly IArticleRepository repository;

        //构造器注入
        public ArticleController(IArticleRepository repository)
        {
            this.repository = repository;
        }

        // GET: Article
        public ActionResult Index()
        {
            var data = repository.GetAll();
            return View(data);
        }
    }
}