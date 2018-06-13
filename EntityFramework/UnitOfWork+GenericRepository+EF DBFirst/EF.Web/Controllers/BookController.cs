using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EF.Data;
using EF.Model;

namespace EF.Web.Controllers
{
    public class BookController : Controller
    {
        public BookController()
        {

        }

        public ActionResult Index()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                //var accountRepository = unitOfWork.Repository<Account>();

                //PersonalAccount personalAccount = new PersonalAccount();
                //personalAccount.CashAccount = new CashAccount();
                //unitOfWork.Repository<PersonalAccount>().Insert(personalAccount);
                //unitOfWork.Save();

                //var mainAccount = unitOfWork
                //.Repository<MainAccount>()
                //.Get(null,null, "CashAccount,CashAccount.SingleCurrencyCashAccounts")
                //.FirstOrDefault();

                //var result = accountRepository.GetById(1);
                // (x=>x.AccountType==AccountType.Main);//(personalAccount);

                var bookRepository = unitOfWork.Repository<T_Book>();
                IEnumerable<Book> books = null;// = bookRepository.Table.ToList();
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
