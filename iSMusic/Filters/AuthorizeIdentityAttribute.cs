using iSMusic.Models.EFModels;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
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
		public CustomAuthorizeAttribute(params string[] roles)
		{
			this.allowedroles = roles;
		}
		//protected override bool AuthorizeCore(HttpContextBase httpContext)
		//{
		//	var userRole = FormsAuthentication.Decrypt(httpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData;
		//	bool authorize = false;
		//	var userId = Convert.ToString(httpContext.Session["UserId"]);
		//	if (!string.IsNullOrEmpty(userId))
		//		using (var context = new AppDbContext())
		//		{
		//			var userRole = (from a in context.Admins
		//							join m in context.Admin_Role_Metadata on a.id equals m.adminId
		//							join r in context.Roles on m.roleId equals r.id
		//							where a.id == userId
		//							select new
		//							{
		//								r.Name
		//							}).FirstOrDefault();
		//			foreach (var role in allowedroles)
		//			{
		//				if (role == userRole.Name) return true;
		//			}
		//		}


		//	return authorize;
		//}

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