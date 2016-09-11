//Created by: Rong Fan
//Email:rong.fan1031@gmail.com
//2016-9-10


using MVC5Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVC5Products.Controllers
{
    public class ProductController : Controller
    {
        static readonly IProductRepository repository = new ProductRepository();

        // GET: Product
        public ActionResult Index()
        {
            return View(repository.GetAll().ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Product product = repository.Get(id);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                //TODO: Add insert logic here              
                if (ModelState.IsValid)//whether input data is valid
                {
                    repository.Add(product);
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = repository.Get(id);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                    repository.Update(product);// = product;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = repository.Get(id);
            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                Product p2= repository.Get(id);
                if (ModelState.IsValid)
                {
                    repository.Remove(id);
                    return RedirectToAction("Index");
                }
                //Product product = repository.Get(id);
                return View(p2);
            }
            catch
            {
                return View();
            }
        }
    }
}
