using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EF.Core.Data;
using EF.Data;

namespace EF.Web.Controllers
{
    public class BookController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<Book> bookRepository;
        private Repository<Account> accountRepository;
        private Repository<SingleCurrencyCashAccount> singleCurrencyCashAccountRepository;
        private Repository<CashAccount> cashAccountRepository;

        public BookController()
        {
            bookRepository = unitOfWork.Repository<Book>();     
            singleCurrencyCashAccountRepository = unitOfWork.Repository<SingleCurrencyCashAccount>();
            cashAccountRepository = unitOfWork.Repository<CashAccount>();
            accountRepository = unitOfWork.Repository<Account>();
        }

        public ActionResult Index()
        {
            Account personalAccount = new PersonalAccount();
            personalAccount.CashAccount = new CashAccount();
            personalAccount.HistoricalCommisions = new Commisions();
            accountRepository.Insert(personalAccount);
            //var result = accountRepository.GetById(1);
            // (x=>x.AccountType==AccountType.Main);//(personalAccount);
            //unitOfWork.Save();
            IEnumerable<Book> books = bookRepository.Table.ToList();
            unitOfWork.Dispose();
            return View(books);
        }

        public ActionResult CreateEditBook(int? id)
        {
            Book model = new Book();
            if (id.HasValue)
            {
                model = bookRepository.GetById(id.Value);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateEditBook(Book model)
        {
            model.CurrencyType = CurrencyType.USD;
            if (model.Id == 0)
            {
                model.CreatedAt = System.DateTime.Now;
                model.UpdatedAt = System.DateTime.Now;
                model.CreatedBy = Request.UserHostAddress;
                model.UpdatedBy = Request.UserHostAddress;
                bookRepository.Insert(model);
            }
            else
            {
                var editModel = bookRepository.GetById(model.Id);
                editModel.Title = model.Title;
                editModel.Author = model.Author;
                editModel.ISBN = model.ISBN;
                editModel.Published = model.Published;
                editModel.UpdatedAt = System.DateTime.Now;
                //editModel.IP = Request.UserHostAddress;
                bookRepository.Update(editModel);
            }

            if (model.Id > 0)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult DeleteBook(int id)
        {
            Book model = bookRepository.GetById(id);
            return View(model);
        }

        [HttpPost,ActionName("DeleteBook")]
        public ActionResult ConfirmDeleteBook(int id)
        {
            Book model = bookRepository.GetById(id);
            bookRepository.Delete(model);
            return RedirectToAction("Index");
        }

        public ActionResult DetailBook(int id)
        {
            Book model = bookRepository.GetById(id);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
