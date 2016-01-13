using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using BLL.Interface.Entities;


namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalBid ToDalBid(this BidEntity bid)
        {
            return new DalBid
            {
                Amount = bid.Amount,
                Id = bid.Id,
                LotId = bid.LotId,
                Placed = bid.Placed,
                UserId = bid.UserId
            };
        }

        public static BidEntity ToBllBidEntity(this DalBid bid)
        {
            return new BidEntity
            {
                Amount = bid.Amount,
                Id = bid.Id,
                LotId = bid.LotId,
                Placed = bid.Placed,
                UserId = bid.UserId,
                Lot=bid.Lot.ToBllLotEntity(),
                User=bid.User.ToBllUserEntity()
            };
        }

        public static LotEntity ToBllLotEntity(this DalLot lot)
        {
            return new LotEntity
            {
                BuyOutBet = lot.BuyOutBet,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                Id = lot.Id,
                LotClosed = lot.LotClosed,
                LotDescription = lot.LotDescription,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                MinimalBet = lot.MinimalBet,
                Name = lot.Name,
                LotEnded = lot.LotEnded,
                CreatedByUser = lot.CreatedByUser.ToBllUserEntity()
            };
        }

        public static DalLot ToDalLot(this LotEntity lot)
        {
            return new DalLot
            {
                BuyOutBet = lot.BuyOutBet,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                Id = lot.Id,
                LotClosed = lot.LotClosed,
                LotDescription = lot.LotDescription,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                MinimalBet = lot.MinimalBet,
                Name = lot.Name,
                LotEnded = lot.LotEnded
            };
        }

        public static DalUser ToDalUser(this UserEntity user)
        {
            return new DalUser
            {
                Id = user.Id,
                UserDisplayName = user.UserDisplayName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserBanned =user.UserBanned
    };
        }

        public static UserEntity ToBllUserEntity(this DalUser user)
        {
            return new UserEntity
            {
                Id = user.Id,
                UserDisplayName = user.UserDisplayName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserBanned = user.UserBanned
            };
        }

        public static DalRole ToDalRole(this RoleEntity userRole)
        {
            return new DalRole
            {
                UserRoleName = userRole.UserRoleName,
                Id = userRole.Id
            };
        }

        public static RoleEntity ToBllRoleEntity(this DalRole userRole)
        {
            return new RoleEntity
            {
                UserRoleName = userRole.UserRoleName,
                Id = userRole.Id
            };
        }

        public static UserRoleEntity ToBllUserRoleEntity(this DalUserRole userRoles)
        {
            return new UserRoleEntity
            {
                UserId = userRoles.UserId,
                Id = userRoles.Id,
                UserRoleId = userRoles.UserRoleId
            };
        }

        public static DalUserRole ToDalUserRole(this UserRoleEntity userRole)
        {
            return new DalUserRole
            {
                UserId = userRole.UserId,
                Id = userRole.Id,
                UserRoleId = userRole.UserRoleId
            };
        }
    }
}
