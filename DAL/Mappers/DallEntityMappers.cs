using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class DallEntityMappers
    {
        public static Bid ToOrmBid(this DalBid bid)
        {
            return new Bid
            {
                Id = bid.Id,
                Amount = bid.Amount,
                LotId = bid.LotId,
                Placed = bid.Placed,
                UserId = bid.UserId
            };

        }


        public static DalBid ToDalBid(this Bid bid)
        {
            return new DalBid
            {
                Id = bid.Id,
                Amount = bid.Amount,
                LotId = bid.LotId,
                Placed = bid.Placed,
                UserId = bid.UserId,
                Lot=bid.Lot.ToDalLot(),
                User=bid.UserLogin.ToDalUser()
            };
        }

        public static Lot ToOrmLot(this DalLot lot)
        {
            return new Lot
            {
                Id = lot.Id,
                BuyOutBet = lot.BuyOutBet,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                LotClosed = lot.LotClosed,
                LotEnded = lot.LotEnded,
                LotDescription = lot.LotDescription,
                Name = lot.Name,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                MinimalBet = lot.MinimalBet

            };

        }


        public static DalLot ToDalLot(this Lot lot)
        {
            return new DalLot
            {
                Id = lot.Id,
                BuyOutBet = lot.BuyOutBet,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                LotClosed = lot.LotClosed,
                LotEnded = lot.LotEnded,
                LotDescription = lot.LotDescription,
                Name = lot.Name,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                MinimalBet = lot.MinimalBet,
                CreatedByUser = lot.UserLogin.ToDalUser()
            };
        }

        public static Role ToOrmRole(this DalRole role)
        {
            return new Role
            {
                Id = role.Id,
                UserRoleName = role.UserRoleName
            };

        }


        public static DalRole ToDalRole(this Role role)
        {
            return new DalRole
            {
                Id = role.Id,
                UserRoleName = role.UserRoleName
            };
        }

        public static UserLogin ToOrmUser(this DalUser user)
        {
            return new UserLogin
            {
                Id = user.Id,
                UserDisplayName = user.UserDisplayName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserBanned = user.UserBanned
            };
        }

        public static DalUser ToDalUser(this UserLogin user)
        {
            return new DalUser
            {
                Id = user.Id,
                UserDisplayName = user.UserDisplayName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserBanned = user.UserBanned
            };
        }

        public static UserRole ToOrmUserRole(this DalUserRole role)
        {
            return new UserRole
            {
                Id = role.Id,
                UserID = role.UserId,
                RoleID = role.UserRoleId
            };

        }


        public static DalUserRole ToDalUserRole(this UserRole role)
        {
            return new DalUserRole
            {
                Id = role.Id,
                UserId = role.UserID,
                UserRoleId = role.RoleID

            };
        }
    }
}
