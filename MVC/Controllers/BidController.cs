using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MVC.Models;

namespace MVC.Controllers
{
    using ICustomAuthentication;
    using Infrastructure.Mappers;

    public class BidController : BaseController
    {
        private readonly IBidService _bidService;
        private readonly ICustomAuthentication _customAuthentication;
        private readonly ILotService _lotService;

        public BidController(ICustomAuthentication authentication, ILotService lotService, IBidService bidService) : base(authentication)
        {
            _customAuthentication = authentication;
            _bidService = bidService;
            _lotService = lotService;
        }

        // GET: Bid
        public ActionResult Index()
        {
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var bids = _bidService.GetAllBids().Select(x => x.ToBidViewModel());
            return View(bids);
        }

        public ActionResult Create(int? id)
        {
            if(UserViewModel==null)
                return new HttpStatusCodeResult(401);
            if(id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lot = _lotService.GetLotEntity(id.Value);
            if (lot.CreatedByUserId == UserViewModel.Id)
            {
                TempData["error"] = "You cannot bid on you own lots!";
                return RedirectToAction("Details","Lot",new {id=lot.Id});
            }
            var vm = new BidViewModel
            {
                LotId = lot.Id,
                Lot = lot.ToLotViewModel()
            };
            return View(vm);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BidViewModel bid)
        {
            if(UserViewModel==null)
                return new HttpStatusCodeResult(401);
            if(UserViewModel.Banned)
                return new HttpStatusCodeResult(403);
            if (ModelState.IsValid)
            {
                bid.LotId = bid.Id;
                var lot = _lotService.GetLotEntity(bid.LotId);
                if(lot==null)
                    return new HttpStatusCodeResult(400);
                if ((lot.LotClosed) || (lot.LotEnded))
                {
                    ModelState.AddModelError(string.Empty,"You can't place bids on closed or ended lot");
                    return View(bid);
                }
                if (bid.Amount < lot.MinimalBet)
                {
                    ModelState.AddModelError(string.Empty, "Your bid's amount can't be less than Minimal Bet Amount");
                    return View(bid);
                }
                bid.Placed=DateTime.Now;
                bid.UserId = UserViewModel.Id;
                var bidEntity = bid.ToBidEntity();
                try
                {
                    _bidService.AddBid(bidEntity);
                }
                catch (BllBidTooLowException)
                {
                    ModelState.AddModelError(string.Empty, "Your bid's amount too low. (There is already a bid with higher amount");
                    return View(bid);
                }
                return RedirectToAction("Details", "Lot", new { id = lot.Id });
            }
            return View(bid);
        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return RedirectToAction("Index");
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var bid = _bidService.GetBidById(id.Value);
            if (bid == null)
                return HttpNotFound();
            return View(bid.ToBidViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
             var bid = _bidService.GetBidById(id);
            _bidService.DeleteBid(bid);
            return RedirectToAction("Index");
        }
    }
}