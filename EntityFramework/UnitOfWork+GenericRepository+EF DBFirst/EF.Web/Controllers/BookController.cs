using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EF.Data;
using EF.Model;
using EF.Service;
using System;

namespace EF.Web.Controllers
{
    public class BookController : Controller
    {
        private IMappingService mappingService;
        public BookController(IMappingService _mappingService)
        {
            mappingService = _mappingService;
        }

        public ActionResult Index()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var bookRepository = unitOfWork.Repository<T_Book>().GetAll();
                var currencyTypeRepository = unitOfWork.Repository<T_CurrencyType>().GetAll();

                // Join表查询
                var query = from bo in bookRepository
                            join cu in currencyTypeRepository on bo.CurrencyTypeID equals cu.CurrencyTypeID
                            where cu.CurrencyTypeID == 1
                            select new Book {
                                ISBN = bo.ISBN,
                                Price = bo.Price == null ? 0:(decimal)bo.Price,
                                CurrencyType = (CurrencyType)cu.CurrencyTypeID,
                                Remark = bo.Remark,
                                Title = bo.Title,
                                Author = bo.Author,
                                Published = bo.Published == null ? new DateTime(): (DateTime)bo.Published                             
                            };
                var books = new List<Book>();
                books = query.ToList();
                books[0].ISBN = "12345678900";


                // Update and Automapper
                var t_book = mappingService.Map<Book,T_Book>(books[0]);
                unitOfWork.Repository<T_Book>().Update(t_book);

                //del using ID
                unitOfWork.Repository<T_Book>().Delete(t_book.ISBN);

                // unitOfWork.Repository<T_Book>().Update();
                return View(books);
            }
        }

        //public ActionResult CreateEditBook(int? id)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        Book model = new Book();
        //        if (id.HasValue)
        //        {
        //            var bookRepository = unitOfWork.Repository<Book>();
        //            model = bookRepository.GetById(id.Value);
        //        }
        //        return View(model);
        //    }
        //}

        //[HttpPost]
        //public ActionResult CreateEditBook(Book model)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        var bookRepository = unitOfWork.Repository<Book>();
        //        //model.CurrencyType = CurrencyType.USD;
        //        //if (model.ISBN == 0)
        //        //{
        //            model.AuditModel.CreatedAt = System.DateTime.Now;
        //            model.AuditModel.UpdatedAt = System.DateTime.Now;
        //            model.AuditModel.CreatedBy = Request.UserHostAddress;
        //            model.AuditModel.UpdatedBy = Request.UserHostAddress;
        //            bookRepository.Insert(model);
        //        //}
        //        //else
        //        //{
        //        //    var editModel = bookRepository.GetById(model.ISBN);
        //        //    editModel.Title = model.Title;
        //        //    editModel.Author = model.Author;
        //        //    editModel.ISBN = model.ISBN;
        //        //    editModel.Published = model.Published;
        //        //    editModel.AuditModel.UpdatedAt = System.DateTime.Now;
        //        //    //editModel.IP = Request.UserHostAddress;
        //        //    bookRepository.Update(editModel);
        //        //}

        //        //if (model.Id > 0)
        //        //{
        //            return RedirectToAction("Index");
        //        //}
        //        return View(model);
        //    }
        //}

        //public ActionResult DeleteBook(int id)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        var bookRepository = unitOfWork.Repository<Book>();
        //        Book model = bookRepository.GetById(id);
        //        return View(model);
        //    }
        //}

        //[HttpPost,ActionName("DeleteBook")]
        //public ActionResult ConfirmDeleteBook(int id)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        var bookRepository = unitOfWork.Repository<Book>();
        //        Book model = bookRepository.GetById(id);
        //        bookRepository.Delete(model);
        //        return RedirectToAction("Index");
        //    }
        //}

        //public ActionResult DetailBook(int id)
        //{
        //    using (UnitOfWork unitOfWork = new UnitOfWork())
        //    {
        //        var bookRepository = unitOfWork.Repository<Book>();
        //        Book model = bookRepository.GetById(id);
        //        return View(model);
        //    }
        //}

        protected override void Dispose(bool disposing)
        {
            //unitOfWork.Dispose();
            base.Dispose(disposing);
        }


    }
}
