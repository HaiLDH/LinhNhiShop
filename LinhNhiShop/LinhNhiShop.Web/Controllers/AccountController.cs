using BotDetect.Web.Mvc;
using LinhNhiShop.Common;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Web.App_Start;
using LinhNhiShop.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LinhNhiShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindAsync(loginViewModel.UserName, loginViewModel.Password);
                if (user != null)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = loginViewModel.RememberMe
                    };
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error Authentication", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegisterCaptcha", "Mã xác nhận không đúng!")]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var emailUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (emailUser != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại.");
                    return View(registerViewModel);
                }
                var userNameUser = await _userManager.FindByNameAsync(registerViewModel.UserName);
                if (userNameUser != null)
                {
                    ModelState.AddModelError("userName", "UserName đã tồn tại.");
                    return View(registerViewModel);
                }
                if (!registerViewModel.Password.Equals(registerViewModel.ConfirmPassword))
                {
                    ModelState.AddModelError("confirmPassword", "Xác nhận mật khẩu không đúng.");
                    return View(registerViewModel);
                }
                var newUser = new ApplicationUser()
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = registerViewModel.FullName,
                    PhoneNumber = registerViewModel.PhoneNumber,
                    Address = registerViewModel.Address
                };
                await _userManager.CreateAsync(newUser, registerViewModel.Password);

                var adminUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (adminUser != null)
                    await _userManager.AddToRolesAsync(adminUser.Id, new string[] { "User" });

                string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/newuser_template.html"));
                content = content.Replace("{{UserName}}", adminUser.UserName);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");

                MailHelper.SendMail(adminUser.Email, "Thông báo LinhNhi Shop", content);

                ViewData["SuccessMsg"] = "Đăng ký thành công";
            }

            return View();
        }


        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}