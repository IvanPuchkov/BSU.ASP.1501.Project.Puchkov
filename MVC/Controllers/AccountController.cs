using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MVC.Infrastructure.Mappers;
using MVC.Models;

namespace MVC.Controllers
{
    using ICustomAuthentication;
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthentication _customAuthentication;

        public AccountController(ICustomAuthentication authentication,IUserService userService):base(authentication)
        {
            _customAuthentication = authentication;
            _userService = userService;
        }

        public ActionResult Register()
        {
            if (UserViewModel != null)
                return RedirectToAction("Index", "Home");
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.CreateUser(register.ToUserEntity());
                }
                catch (DbUpdateException e)
                {
                    var ex = e.InnerException.InnerException as SqlException;
                    if (ex?.ErrorCode == -2146232060)
                    {
                        ModelState.AddModelError(string.Empty, "User with specified Display name or Email already exists!");
                        return View(register);
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(register);
        }

        public ActionResult Login()
        {
            if (UserViewModel != null)
                return RedirectToAction("Index", "Home");
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                result = _customAuthentication.LoginWithDisplayName(System.Web.HttpContext.Current, login.Email, login.Password,login.RememberMe);
                if(result)
                    return RedirectToAction("Index","Home");
                result = _customAuthentication.LoginWithEmail(System.Web.HttpContext.Current, login.Email,
                    login.Password,login.RememberMe);
                if(result)
                    return RedirectToAction("Index", "Home");
                ModelState.AddModelError(string.Empty,"Can't login with specified information");
            }
            return View(login);
        }

        public ActionResult Logout()
        {
            _customAuthentication.LogOut(System.Web.HttpContext.Current);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserLoginHeaderInfo()
        {
            if (!ControllerContext.IsChildAction)
                return RedirectToAction("Index", "Home");
            return PartialView(UserViewModel);
        }
    }
}