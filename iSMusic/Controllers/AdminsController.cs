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
using System.Security.AccessControl;
using System.Runtime.Remoting.Contexts;
using System.Security.Claims;
using iSMusic.Filters;

namespace AdminManagement.Controllers
{
	
	public class AdminsController : Controller
	{
		public IAdminRepository repository;

		private int departmentId = 5;

		public AdminsController()
		{
			repository = new AdminRepository();
		}

		// GET: Admins
		[Authorize]
		public ActionResult Index()
		{
			var service = new AdminService(repository);

			if (CheckPermission(departmentId) == false) return RedirectToAction("Index", "Home");

			var data = service.Search();
			var list = data.Select(x => x.ToVM());

			return View(list);
		}

		public ActionResult DepartmentIndex()
		{
			var service = new AdminService(repository);

			string[] roles = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',').Where(x => x.Length != 0).ToArray();

			var data = service.Search();
			var list = new List<AdminIndexVM>();

			var departmentIds = roles.Where(r => r[1] == '3').Select(r => int.Parse(r.Substring(0, 1)));

			foreach (int departmentId in departmentIds)
			{
				var tempList = data.Where(d => d.departmentId == departmentId).Select(x => x.ToVM()).ToList();

				list = list.Concat(tempList).ToList();
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
			var dto = repository.GetById(id);

			//check permission level or the owner
			if (QualifiedPermission(dto.adminAccount) == false) return RedirectToAction("Index", "Admins");

			var data = dto.ToEditVM();

			ViewBag.departmentId = new SelectList(GetDepartments(), "id", "departmentName", dto.departmentId);

			return View(data);
		}

		private bool QualifiedPermission(string account)
		{
			var authTicket = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);

			var roles = authTicket.UserData.Split(',').Where(d=> d.Length> 0);

			if (roles.Contains("1")) return true;

			var permissions = roles.Where(r => r.Length > 1).Select(r => r[1]);

			var departments = roles.Where(r => r.Length > 1).Select(r => r[0]);

			return (authTicket.Name == account || permissions.Contains('3') || departments.Contains('5'));
		}

		// POST: Admins/Edit/5
		[HttpPost]
		public ActionResult Edit(AdminEditVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var service = new AdminService(repository);

			(bool IsSuccess, string ErrorMessage) response = service.EditAdmin(model.ToRequestDTO());

			if (response.IsSuccess)
			{
				// 建檔成功 redirect to confirm page
				return RedirectToAction("Index");
			}
			else
			{
				ModelState.AddModelError(string.Empty, response.ErrorMessage);

			}

			ViewBag.departmentId = new SelectList(GetDepartments(), "id", "departmentName", model.Id);

			return View(model);
		}

		// GET: Admins/Delete/5
		public ActionResult Delete(int id)
		{
			//superuser check
			List<int> roles = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Name).UserData.Split(',').Where(x => x.Length != 0).Select(r => int.Parse(r)).ToList();
			if (roles.Contains(1) == false) return RedirectToAction("Index", "Admins");

			var service = new AdminService(repository);

			//only superuser can execute delete action
			var data = service.ShowDelete(id);

			return View(data.ToDelVM());
		}

		// POST: Admins/Delete/5
		[HttpPost]
		public ActionResult Delete(AdminDelVM model)
		{
			var service = new AdminService(repository);
			try
			{
				// TODO: Add delete logic here
				service.Delete(model.ToRequestDTO());

				return RedirectToAction("Index");
			}
			catch(Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public ActionResult Details()
		{
			var account = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
			var data = repository.GetByAccount(account);

			return View(data.ToDetailVM());
		}

		public static bool CheckPermission(int departmentId)
		{
			List<int> roles = FormsAuthentication.Decrypt(System.Web.HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData.Split(',').Where(x => x.Length != 0).Select(r => int.Parse(r)).ToList();

			int superUser = 1;

			//if there exist a superUser in the roles, return true
			if (roles.Contains(superUser)) return true;

			//if the departmentId from the User involved the request departmentId, return true
			if (roles.Select(r => r / 10).Contains(departmentId)) return true;

			return false;
		}
	}
}
