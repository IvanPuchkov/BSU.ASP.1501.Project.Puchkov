using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Entities;
using MVC.Models;

namespace MVC.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static BidEntity ToBidEntity(this BidViewModel bid)
        {
            return new BidEntity
            {
                Amount = bid.Amount,
                LotId = bid.LotId,
                Placed = bid.Placed,
                UserId = bid.UserId
            };
        }
        public static BidViewModel ToBidViewModel(this BidEntity bid)
        {
            return new BidViewModel
            {
                Amount = bid.Amount,
                Id = bid.Id,
                Lot = bid.Lot.ToLotViewModel(),
                LotId = bid.LotId,
                Placed = bid.Placed,
                User = bid.User.ToUserViewModel(),
                UserId = bid.UserId
            };
        }

        public static UserEntity ToUserEntity(this RegisterViewModel register)
        {
            return new UserEntity
            {
                UserDisplayName = register.DisplayName,
                UserEmail = register.Email,
                UserPassword = register.Password
            };
        }

        public static UserEntity ToUserEntity(this UserViewModel user)
        {
            return new UserEntity
            {
                UserBanned = user.Banned,
                UserDisplayName = user.DisplayName,
                UserEmail = user.Email,
                Id=user.Id
            };
        }

        public static UserViewModel ToUserViewModel(this UserEntity user)
        {
            return new UserViewModel
            {
                DisplayName = user.UserDisplayName,
                Email = user.UserEmail,
                Banned = user.UserBanned,
                Id = user.Id
            };
        }

        public static LotViewModel ToLotViewModel(this LotEntity lot)
        {
            return new LotViewModel
            {
                BuyOutBid = lot.BuyOutBet,
                CreatedByUser = lot.CreatedByUser.ToUserViewModel(),
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                Id = lot.Id,
                LotClosed = lot.LotClosed,
                LotEnded = lot.LotEnded,
                LotDescription = lot.LotDescription,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                Name=lot.Name,
                MinimalBid = lot.MinimalBet
            };
        }

        public static LotEntity ToLotEntity(this LotViewModel lot)
        {
            return new LotEntity
            {
                BuyOutBet = lot.BuyOutBid,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                Id = lot.Id,
                LotClosed = lot.LotClosed,
                LotEnded = lot.LotEnded,
                LotDescription = lot.LotDescription,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                Name = lot.Name,
                MinimalBet = lot.MinimalBid
            };
        }
    }
}