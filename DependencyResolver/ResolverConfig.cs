using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<AuctionDatabaseEntities>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IBidService>().To<BidService>();
            kernel.Bind<ILotService>().To<LotService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IUserRoleSevice>().To<UserRoleService>();


            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IBidRepository>().To<BidRepository>();
            kernel.Bind<ILotRepository>().To<LotRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            kernel.Bind<ICustomAuthentication.ICustomAuthentication>().To<CustomAuthentication>();
        }
    }
}
