using System;
using System.Web;
using System.Web.Security;
using BLL.Interface.Entities;
using BLL.Interface.Services;

namespace Authentication
{
    using ICustomAuthentication;
    public class CustomAuthentication: ICustomAuthentication
    {
        private const string CookieName = "__AUTH_COOKIE";

        private readonly IUserService _userService;
        private readonly IUserRoleSevice _userRoleSevice;

        public CustomAuthentication(IUserService userService,IUserRoleSevice userRoleSevice)
        {
            _userService = userService;
            _userRoleSevice = userRoleSevice;
        }

        private void CreateCookie(HttpContext context, UserEntity user, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                1,
                user.UserEmail,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                isPersistent,
                string.Empty,
                FormsAuthentication.FormsCookiePath);


            var encTicket = FormsAuthentication.Encrypt(ticket);

            var authCookie = new HttpCookie(CookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.AddDays(1)
            };
            context.Response.Cookies.Set(authCookie);
        }

        public void LogOut(HttpContext context)
        {
            var httpCookie = context.Response.Cookies[CookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }



        public bool LoginWithDisplayName(HttpContext context, string displayName, string password, bool isPersistent)
        {
            UserEntity user = _userService.GetUserEntityByDisplayNameAndPassword(displayName, password);
            if (user == null)
                return false;
            CreateCookie(context, user,isPersistent);
            return true;
        }


        public bool LoginWithEmail(HttpContext context, string email, string password, bool isPersistent)
        {
            UserEntity user = _userService.GetUserEntityByEmailAndPassword(email, password);
            if (user == null)
                return false;
            CreateCookie(context, user,isPersistent);
            return true;
        }

        public UserEntity CheckAuthentication(HttpRequestBase request)
        {
            UserEntity userEntity = null;
            HttpCookie authCookie = request.Cookies.Get(CookieName);
            if (!string.IsNullOrEmpty(authCookie?.Value))
            {
                if (authCookie.Expires > DateTime.Now)
                {
                    authCookie.Value = string.Empty;
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                }
                else
                {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (ticket != null)
                    {
                        if (ticket.Expired)
                        {
                            authCookie.Value = string.Empty;
                            authCookie.Expires = DateTime.Now.AddDays(-1);
                        }
                        else
                        {
                            userEntity = _userService.GetUserEntityByEmail(ticket.Name);
                        }
                    }
                }
            }
            return userEntity;
        }

        public bool CheckUserInRoles(UserEntity user,string roles)
        {
            if (user == null)
                return false;
            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            bool roleMatched = false;
            var userRoles = _userRoleSevice.GetUserRoles(user.Id);
            foreach (var stringRole in rolesArray)
            {
                foreach (var userRoleEntity in userRoles)
                {
                    if (stringRole.Equals(userRoleEntity.UserRoleName, StringComparison.InvariantCultureIgnoreCase))
                        roleMatched = true;
                }
            }
            return roleMatched;
        }
    }

    
}
