using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using MVC.Infrastructure.Mappers;
using MVC.Models;

namespace MVC.Controllers
{   using ICustomAuthentication;
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthentication _customAuthentication;
        private readonly IBidService _bidService;
        private readonly ILotService _lotService;
        public UserController(ICustomAuthentication authentication, IUserService userService,IBidService bidService,ILotService lotService)
            :base(authentication)
        {
            _customAuthentication = authentication;
            _userService = userService;
            _bidService = bidService;
            _lotService = lotService;
        }
        public ActionResult Index()
        {
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var users = _userService.GetAllUserEntities().Select(x => x.ToUserViewModel());
            return View(users);
        }

        public ActionResult Ban(int? id, bool ban=true)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var user = _userService.GetUserEntity(id.Value);
            if (user==null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            if (!ban)
            {
                user.UserBanned = false;
                _userService.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user.ToUserViewModel());
        }

        [HttpPost]
        public ActionResult Ban(UserViewModel user)
        {
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var userEntity=_userService.GetUserEntity(user.Id);
            userEntity.UserBanned = true;
            _userService.UpdateUser(userEntity);
            _bidService.DeleteAllBidsByUser(userEntity.Id);
            _lotService.CloseAllActiveLotsCreatedByUser(userEntity);
            return RedirectToAction("Index");
        }
    }
}