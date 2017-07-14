using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace Identiy.Asp.Net.Demo.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<IdentityUser> UserManager => 
            HttpContext
            .GetOwinContext()
            .Get<UserManager<IdentityUser>>();

        public SignInManager<IdentityUser, string> SignInManager =>
            HttpContext
            .GetOwinContext()
            .Get<SignInManager<IdentityUser,string>>();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var signInStatus = await SignInManager.PasswordSignInAsync(model.UserName,model.Password,true,true);

            switch (signInStatus)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index","Home");
                default:
                    ModelState.AddModelError("", "Invalid Credentials");
                    return View(model);
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            //check whether user exist before create
            //var identifyUser = await UserManager.FindByNameAsync(model.UserName);
            //if (identifyUser != null)
            //{
            //    //if user exist, return it to Home, Index
            //    return RedirectToAction("Index","Home");
            //}

            var identityResult =  await UserManager.CreateAsync(new IdentityUser(model.UserName), model.Password);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            string firstError = identityResult.Errors.FirstOrDefault();
            ModelState.AddModelError("", firstError);
            return View(model);
        }

    }

    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
