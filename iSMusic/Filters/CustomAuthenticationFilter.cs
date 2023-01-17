using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace iSMusic.Filters
{
	//authentication always comes before authorization
	public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserName"])))
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}
		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
			{
				//Redirecting the user to the Login View of Account Controller  
				filterContext.Result = new RedirectToRouteResult(
				new RouteValueDictionary
				{
					 { "controller", "Account" },
					 { "action", "Login" }
				});
			}
		}
	}
}