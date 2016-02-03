using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MVC.Infrastructure.Mappers;
using MVC.Models;

namespace MVC.Controllers
{ 
    using ICustomAuthentication;

    public class LotController : BaseController
    {
        private readonly ILotService _lotService;
        private readonly IBidService _bidService;
        private readonly ICustomAuthentication _customAuthentication;
        private const int ItemsPerPage = 3;
        public LotController(ICustomAuthentication authentication, ILotService lotService,IBidService bidService) : base(authentication)
        {
            _customAuthentication = authentication;
            _bidService = bidService;
            _lotService = lotService;
        }

        public ActionResult Details(int? id)
        {

            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var lot = _lotService.GetLotEntity(id.Value);
            if (lot == null)
                return HttpNotFound();
            var lotVM = lot.ToLotViewModel();
            ViewBag.IsAdminOrModerator = false;
            ViewBag.SamePerson = false;
            if ((UserViewModel == null) || (UserViewModel.Banned))
            {
                ViewBag.AutorizedAndNotBanned = false;
            }
            else
            {

                ViewBag.AutorizedAndNotBanned = true;
                if (lotVM.CreatedByUserId==UserViewModel.Id)
                    ViewBag.SamePerson = true;
                ViewBag.IsAdminOrModerator = _customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(),
                    "Admin,Moderator");
            }
            
            lotVM.LatestBids = _bidService.GetLatestBidsForLot(id.Value).Select(x => x.ToBidViewModel());
            ViewBag.TopBid = 0;
            if (lotVM.LatestBids.Any())
                ViewBag.TopBid = lotVM.LatestBids.First().Amount;
            return View(lotVM);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult LotList(int? page,LotSearchViewModel search)
        {
            int actualPage = page ?? 1;
            ViewBag.IsAdminOrModerator = false;
            IEnumerable<LotEntity> lots;
            if ((UserViewModel != null))
            {
                ViewBag.IsAdminOrModerator = _customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(),
                    "Admin,Moderator");
            }
            if (string.IsNullOrEmpty(search?.SearchString))
            {
                lots= _lotService.GetAllLots();
            }
            else
            {
                if (search.SearchInName)
                    lots = _lotService.GetLotsContainingStringInName(search.SearchString);
                else
                {
                    lots = _lotService.GetLotsContainingStringInDescription(search.SearchString);
                }
            }
            var pager=new CustomPagerViewModel<LotViewModel>();
            pager.CurrentPage = actualPage;
            int count = lots.Count();
            int numberOfPages = count/ItemsPerPage;
            pager.NumberOfPages = count%ItemsPerPage==0?numberOfPages:numberOfPages+1;
            pager.Data=lots.Skip((actualPage - 1) * ItemsPerPage).Take(ItemsPerPage).Select(x => x.ToLotViewModel());
            ViewBag.SearchString = search?.SearchString;
            ViewBag.SearchInName = search?.SearchInName ?? true;
            return PartialView(pager);
        }

        public ActionResult Index()
        {
            ViewBag.IsAdminOrModerator = false;
            if ((UserViewModel == null) || (UserViewModel.Banned))
                ViewBag.AutorizedAndNotBanned = false;
            else
            {
                ViewBag.AutorizedAndNotBanned = true;
                ViewBag.IsAdminOrModerator = _customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(),
                    "Admin,Moderator");
            }
            return View(new LotSearchViewModel());
        }

        

        private WebImage GetDefaultImage(bool isPreview)
        {
            WebImage wi = new WebImage("~/App_Data/img/no-image.png");
            if (isPreview)
                wi.Resize(100, 100, true, true);
            else
            {
                wi.Resize(300, 300, true, true);
            }
            return wi;
        }

        public FileResult GetImage(int? lotId, bool? isPreview)
        {
            WebImage wi;
            bool preview = true;
            if (isPreview.HasValue)
                preview = isPreview.Value;
            if (lotId.HasValue)
            {
                LotViewModel lot = _lotService.GetLotEntity(lotId.Value).ToLotViewModel();
                if (lot.LotPicture != null)
                    wi = new WebImage(preview ? lot.LotPicturePreview : lot.LotPicture);
                else
                    wi = GetDefaultImage(preview);
            }
            else
                wi = GetDefaultImage(preview);
            return File(wi.GetBytes(), "image/" + wi.ImageFormat, wi.FileName);
        }

        public ActionResult Create()
        {
            if ((UserViewModel == null) || (UserViewModel.Banned))
                return RedirectToAction("Index");
            return View(new LotViewModel() {EndDate = DateTime.Now});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LotViewModel lot)
        {

            if (ModelState.IsValid)
            {
                if (lot.EndDate < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "End Date cannot come before Now");
                    return View(lot);
                }
                if (lot.MinimalBid < 0.01m)
                {
                    ModelState.AddModelError(string.Empty, "Starting bid can't be less than 0.1");
                    return View(lot);
                }
                if ((lot.BuyOutBid != null) && (lot.BuyOutBid.Value <= lot.MinimalBid))
                {
                    ModelState.AddModelError(string.Empty, "Buyout bid can't be less than or equal to starting bid!");
                    return View(lot);
                }
                lot.CreatedByUserId = UserViewModel.Id;
                WebImage wi = WebImage.GetImageFromRequest();
                if (wi != null)
                {
                    wi.Resize(300, 300, true, true);
                    lot.LotPicture = wi.GetBytes();
                    wi.Resize(100, 100, true, true);
                    lot.LotPicturePreview = wi.GetBytes();
                }
                _lotService.CreateLot(lot.ToLotEntity());
                return RedirectToAction("Index");
            }
            return View(lot);

        }

        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return RedirectToAction("Index");
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id.Value);
            if (lot == null)
                return HttpNotFound();
            return View(lot.ToLotViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id);
            _lotService.DeleteLot(lot);
            return RedirectToAction("Index");
        }

        public ActionResult Close(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return RedirectToAction("Index");
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id.Value);
            if (lot == null)
                return HttpNotFound();
            return View(lot.ToLotViewModel());
        }

        [HttpPost, ActionName("Close")]
        [ValidateAntiForgeryToken]
        public ActionResult CloseConfirmed(int id)
        {
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id);
            lot.LotClosed = true;
            _lotService.UpdateLot(lot);
            return RedirectToAction("Details", new { id = lot.Id });
        }

        public ActionResult End(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return RedirectToAction("Index");
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id.Value);
            if (lot == null)
                return HttpNotFound();
            return View(lot.ToLotViewModel());
        }

        [HttpPost, ActionName("End")]
        [ValidateAntiForgeryToken]
        public ActionResult EndConfirmed(int id)
        {
            if (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator"))
                return new HttpStatusCodeResult(403);
            var lot = _lotService.GetLotEntity(id);
            lot.LotEnded = true;
            _lotService.UpdateLot(lot);
            return RedirectToAction("Details",new {id=lot.Id});
        }

        public ActionResult Update(int? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            var lot = _lotService.GetLotEntity(id.Value);
            if ((UserViewModel.Id != lot.CreatedByUserId) &&
                (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator")))
            return new HttpStatusCodeResult(403);
            ViewBag.HasImage = lot.LotPicture != null;
            return View(lot.ToLotViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LotViewModel lot)
        {
            if (UserViewModel == null)
                return new HttpStatusCodeResult(401);
            if ((UserViewModel.Id != lot.CreatedByUserId) &&
                (!_customAuthentication.CheckUserInRoles(UserViewModel.ToUserEntity(), "Admin,Moderator")))
                return new HttpStatusCodeResult(403);
            var oldLot = _lotService.GetLotEntity(lot.Id);
            ViewBag.HasImage = oldLot.LotPicture != null;
            lot.CreatedByUserId = oldLot.CreatedByUserId;
            if (ModelState.IsValid)
            {
                if (lot.EndDate < DateTime.Now)
                { 
                        ModelState.AddModelError(string.Empty, "End Date cannot come before Now");
                        return View(lot);
                }
                    if (lot.MinimalBid < 0.01m)
                    {
                        ModelState.AddModelError(string.Empty, "Starting bid can't be less than 0.1");
                        return View(lot);
                    }
                    if ((lot.BuyOutBid != null) && (lot.BuyOutBid.Value <= lot.MinimalBid))
                    {
                        ModelState.AddModelError(string.Empty, "Buyout bid can't be less than or equal to starting bid!");
                        return View(lot);
                    }
                    WebImage wi = WebImage.GetImageFromRequest();
                    var r = wi;
                    if (wi != null)
                    {
                        wi.Resize(300, 300, true, true);
                        lot.LotPicture = wi.GetBytes();
                        wi.Resize(100, 100, true, true);
                        lot.LotPicturePreview = wi.GetBytes();
                    }
                if ((wi == null) && (!lot.DeletePicture))
                {

                    lot.LotPicture = oldLot.LotPicture;
                    lot.LotPicturePreview = oldLot.LotPicturePreview;
                }

                _lotService.UpdateLot(lot.ToLotEntity());
                return RedirectToAction("Details", new {id = lot.Id});
            }
            return View(lot);
        }
    }

}