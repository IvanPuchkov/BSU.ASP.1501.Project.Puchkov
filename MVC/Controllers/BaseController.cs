using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using MVC.Infrastructure.Mappers;
using MVC.Models;

namespace MVC.Controllers
{
    using ICustomAuthentication;
    public class BaseController : Controller
    {
        private readonly ICustomAuthentication _customAuthentication;
        protected UserViewModel UserViewModel { get; private set; }

        public BaseController(ICustomAuthentication customAuthentication)
        {
            _customAuthentication = customAuthentication;
        }
        
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            base.OnActionExecuting(ctx);
            UserViewModel = _customAuthentication.CheckAuthentication(Request)?.ToUserViewModel();
        }
    }
}