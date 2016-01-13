using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BLL.Interface.Entities;

namespace ICustomAuthentication
{
    public interface ICustomAuthentication
    {
        void LogOut(HttpContext context);
        bool LoginWithDisplayName(HttpContext context, string displayName, string password);
        bool LoginWithEmail(HttpContext context, string email, string password);
        UserEntity CheckAuthentication(HttpRequestBase request);
        bool CheckUserInRoles(UserEntity user, string roles);
    }
}
