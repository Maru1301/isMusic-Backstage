using iSMusic.Models.EFModels;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace iSMusic.Filters
{
	//authorization always comes after authentication
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		private readonly string[] allowedroles;

		private readonly string superUser = "1";
		public CustomAuthorizeAttribute(params string[] roles)
		{
			this.allowedroles = roles;
		}
		protected override bool AuthorizeCore(HttpContextBase httpContext)
		{
			List<string> userRoles = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',').Where(x => x.Length != 0).ToList();

			if (userRoles.Contains(superUser)) return true;

			bool authorize = false;

			if(userRoles.Any(r=> allowedroles.Contains(r))) authorize = true;

			return authorize;
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
		{
			filterContext.Result = new RedirectToRouteResult(
			   new RouteValueDictionary
			   {
					{ "controller", "Home" },
					{ "action", "UnAuthorized" }
			   });
		}
	}
}