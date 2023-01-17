using isMusic.Models.ViewModels;
using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
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
using System.Web.Services.Description;

namespace iSMusic.Controllers
{
	public class AdminsController : Controller
	{
		private AppDbContext db = new AppDbContext();
		public AdminRepository adminRepository;
		public AdminService adminService;
		public AdminsController()
		{
			var db = new AppDbContext();
			IAdminRepository repo = new AdminRepository(db);
			adminRepository = new AdminRepository(db);
			this.adminService = new AdminService(repo);
		}

		// GET: Admin
		public ActionResult Index()
		{
			var data = adminService.Search(null, null).Select(x => x.ToAdminIndexVM()).ToList();
			return View(data);
		}

		public ActionResult Create()
		{
			ViewBag.department = new SelectList(db.Departments, "id", "departmentName");
			return View();
		}

		[HttpPost]
		public ActionResult Create(AdminCreateVM model)
		{
			try
			{
				ViewBag.department = new SelectList(db.Departments, "id", "departmentName").Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });

				adminService.adminCreate(model.ToAdminCreateDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "新增失敗," + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public ActionResult Edit(string adminAccount)
		{
			ViewBag.departments = new SelectList(db.Departments, "id", "departmentName");

			var data = adminService.GetByAccount(adminAccount);
			return View(data.ToAdminEditVM());
		}

		[HttpPost]
		public ActionResult Edit(AdminEditVM model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}
			try
			{
				ViewBag.departments = new SelectList(db.Departments, "id", "departmentName");
				adminService.Edit(model.ToAdminEditDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "更新失敗," + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		public ActionResult Delete(string adminAccount)
		{
			var data = adminService.GetByAccount(adminAccount);
			return View(data.ToAdminEditVM());
		}

		[HttpPost]
		public ActionResult Delete(AdminEditVM model)
		{
			//adminService.Delete(model.adminAccount);			       
			try
			{
				adminService.Delete(model.ToAdminEditDTO());
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "刪除失敗," + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}