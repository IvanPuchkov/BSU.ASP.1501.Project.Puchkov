using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MVC.Infrastructure;
using MVC.Infrastructure.Mappers;
using MVC.Models;

namespace MVC.Controllers
{   using ICustomAuthentication;
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICustomAuthentication _customAuthentication;
        private readonly IBidService _bidService;
        private readonly ILotService _lotService;
        private const int ItemsPerPage = 3;
        public UsersController(ICustomAuthentication authentication, IUserService userService,IBidService bidService,ILotService lotService)
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

        public ActionResult Details(int? id)
        {
            if(id==null) 
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var user=_userService.GetUserEntity(id.Value);
            if (user==null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            bool isAdminOrModerator = false;
            if (UserViewModel != null)
                if (_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                    isAdminOrModerator = true;
            ViewBag.AdminOrModerator = isAdminOrModerator;
            if (Request.IsAjaxRequest())
                return PartialView("DetailsPartialView", user.ToUserViewModel());
            return View("Details",user.ToUserViewModel());
        }

        private void BanUser(UserEntity user)
        {
            user.UserBanned = true;
            _userService.UpdateUser(user);
            _bidService.DeleteAllBidsByUser(user.Id);
            _lotService.CloseAllActiveLotsCreatedByUser(user);
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
                if (Request.IsAjaxRequest())
                    return Details(id.Value);
                return RedirectToAction("Details",new {id=id.Value});
            }
            if (Request.IsAjaxRequest())
            {
                BanUser(user);
                return Details(id.Value);
            }
            int a=5;
            return View("Ban", user.ToUserViewModel());
        }

        [HttpPost]
        public ActionResult Ban(UserViewModel user)
        {
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var userEntity = _userService.GetUserEntity(user.Id);
            BanUser(userEntity);
            return RedirectToAction("Index");
        }

        public ActionResult GetLots(int? id,int? page)
        {
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user=_userService.GetUserEntity(id.Value);
            if(user==null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var lots=_lotService.GetAllLotsCreatedByUserId(id.Value);
            int actualPage = page ?? 1;
            var pager = PagerViewModelCreator<LotViewModel>.GetPagerViewModel(lots.Select(x => x.ToLotViewModel()),
                actualPage, ItemsPerPage);
            ViewBag.Id = id.Value;
            return PartialView(pager);
        }

        public ActionResult GetBids(int? id,int? page)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = _userService.GetUserEntity(id.Value);
            if (user == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var bids = _bidService.GetAllBidsByUserId(id.Value);
            bool isAdminOrModerator = false;
            if(UserViewModel!=null)
                if (_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                    isAdminOrModerator = true;
            ViewBag.AdminOrModerator = isAdminOrModerator;
            int actualPage = page ?? 1;
            var pager =
                PagerViewModelCreator<BidViewModel>.GetPagerViewModel(
                    bids.OrderByDescending(x => x.Placed).Select(x => x.ToBidViewModel()),
                    actualPage, ItemsPerPage);
            ViewBag.Id = id.Value;
            return PartialView(pager);
        }
    }
}