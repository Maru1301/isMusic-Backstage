using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using isMusic.Models.ViewModels;
using AdminManagement.Models.Repositories;
using iSMusic.Infrastructures.Extensions;

namespace AdminManagement.Controllers
{
	[Authorize]
	public class AdminsController : Controller
	{
		public IAdminRepository repository;

		public AdminsController()
		{
			repository = new AdminRepository();
		}

		// GET: Admins
		[Authorize]
		public ActionResult Index()
		{
			var service = new AdminService(repository);

			string[] roles = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',').Where(x => x.Length != 0).ToArray();

			var data = service.Search();
			var list = new List<AdminIndexVM>();

			if (roles.Contains("1"))
			{
				list = data.Select(x => x.ToVM()).ToList();
			}
			else
			{
				var departmentIds = roles.Where(r => r[1] == '3').Select(r => int.Parse(r.Substring(0, 1)));
				foreach (int departmentId in departmentIds)
				{
					var tempList = data.Where(d => d.departmentId == departmentId).Select(x => x.ToVM()).ToList();

					list = list.Concat(tempList).ToList();
				}
			}

			return View(list);
		}

		// GET: Admins/Create
		public ActionResult Create()
		{
			ViewBag.departmentId = new SelectList(GetDepartments(), "id", "departmentName").Prepend(new SelectListItem { Text = "請選擇" });

			return View();
		}

		private IEnumerable<Department> GetDepartments()
		{
			string[] roles = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',').Where(x => x.Length != 0).ToArray();

			var departmentList = new Department().GetAll();

			// not a superuser, but an PermissionEditor
			if (roles.Contains("1") == false && roles.Contains("52"))
			{
				departmentList = departmentList.Where(l => l.id != 5);
			}
			// not a superuser, but a department Admin
			else if (roles.Contains("1") == false)
			{
				var departmentIds = roles.Where(r => r[1] == '3').Select(r => int.Parse(r.Substring(0, 1)));

				departmentList = departmentList.Where(l => departmentIds.Contains(l.id));
			}

			return departmentList;
		}

		// POST: Admins/Create
		[HttpPost]
		public ActionResult Create(AdminCreateVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var service = new AdminService(repository);

			(bool IsSuccess, string ErrorMessage) response = service.CreateNewAdmin(model.ToRequestDTO());

			if (response.IsSuccess)
			{
				// 建檔成功 redirect to confirm page
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, response.ErrorMessage);

			}

			ViewBag.departmentId = new SelectList(GetDepartments(), "id", "departmentName").Prepend(new SelectListItem { Text = "請選擇" });

			return View(model);
		}

		[HttpGet]
		public JsonResult GenerateAccount()
		{
			return Json(new AdminService(repository).GetNewAccount(), JsonRequestBehavior.AllowGet);
		}

		[AllowAnonymous]
		public ActionResult Login()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public ActionResult Login(LoginVM model)
		{
			var service = new AdminService(repository);
			(bool IsSuccess, string ErrorMessage) response = service.Login(model.Account, model.Password);
			if (response.IsSuccess)
			{
				// 記住登入成功的會員
				var rememberMe = false;

				string returnUrl = ProcessLogin(model.Account, rememberMe, out HttpCookie cookie);

				Response.Cookies.Add(cookie);

				return Redirect(returnUrl);
			}

			ModelState.AddModelError(string.Empty, response.ErrorMessage);

			return this.View(model);
		}

		private string ProcessLogin(string account, bool rememberMe, out HttpCookie cookie)
		{
			var adminRole = repository.Load(account).ToDTO();
			string roles = String.Empty;

			foreach (int roleId in adminRole.RoleIdList)
			{
				roles += roleId.ToString() + ",";
			}

			// 建立一張認證票
			FormsAuthenticationTicket ticket =
				new FormsAuthenticationTicket(
					1,          // 版本別, 沒特別用處
					account,
					DateTime.Now,   // 發行日
					DateTime.Now.AddDays(2), // 到期日
					rememberMe,     // 是否續存
					roles,          // userdata
					"/" // cookie位置
				);


			// 將它加密
			string value = FormsAuthentication.Encrypt(ticket);

			// 存入cookie
			cookie = new HttpCookie(FormsAuthentication.FormsCookieName, value);

			// 取得return url
			string url = FormsAuthentication.GetRedirectUrl(account, true); //第二個引數沒有用處

			return url;
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();

			return RedirectToAction("Index", "Home");
		}

		// GET: Admins/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Admins/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Admins/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Admins/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
