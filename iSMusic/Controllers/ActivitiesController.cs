using iSMusic.Models.Infrastructures.Extensions;
using isMusic.Models.DTOs;
using isMusic.Models.ViewModels;
using isMusic.Services;
using isMusic.Services.Interfaces;
using isMusic.ViewModels;
using iSMusic.Models.EFModels;
using System;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using isMusic.Infrastructures.Repositories;
using isMusic.Infrastructures.Extensions;
using X.PagedList;

namespace isMusic.Controllers
{
    public class ActivitiesController : Controller
    {
        private ActivityService activityService;

        //這個要換到REPO
        private AppDbContext db = new AppDbContext();

        public ActivitiesController()
        {
            var db = new AppDbContext();
            IActivityRepository repo = new ActivityRepository(db);
            this.activityService = new ActivityService(repo);

        }
        // GET: Activities
        public ActionResult Index()
        {
            //if not specific then default to page 1
            //var pageNumber = page ?? 1;
            //int pageSize = 5;

            var data = activityService.Search(null, null).Select(x => x.ToActivityVM()).ToList();

            //var result = data.ToPagedList(pageNumber, pageSize);

            return View(data);
        }


        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.activityType = new SelectList(db.ActivityTypes, "id", "typeName").Prepend(new SelectListItem { Value = string.Empty, Text = "請選擇" });
            ViewBag.checkedById = new SelectList(db.Admins, "id", "adminAccount", "admin");
            ViewBag.activityOrganizerId = new SelectList(db.Members, "id", "memberAccount", "iSMusic");
            return View();
        }

        // POST: Activities/Create
        [HttpPost]
        public ActionResult Create(ActivityCreateVM model)
        {
            try
            {
                ViewBag.activityType = new SelectList(db.ActivityTypes, "id", "typeName");
                ViewBag.checkedById = new SelectList(db.Admins, "id", "adminAccount");
                ViewBag.activityOrganizerId = new SelectList(db.Members, "id", "memberAccount");
				string coverPath = Server.MapPath("/Uploads/Covers");
				activityService.Create(coverPath,model.ToActivityCreateDTO());
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

        // GET: Activities/Edit/1
        public ActionResult Edit(int id)
        {
            var data = activityService.GetById(id);
            ViewBag.activityType = new SelectList(db.ActivityTypes, "id", "typeName");
            ViewBag.checkedBy = new SelectList(db.Admins, "id", "adminAccount", data.admin);
            ViewBag.activityOrganizer = new SelectList(db.Members, "id", "memberAccount", data.member);
            
            return View(data.ToActivityEditVM());
        }

        // POST: Activities/Edit/1
        [HttpPost]
        public ActionResult Edit(ActivityEditVM model)
        {
            try
            {
                ViewBag.activityTypeId = new SelectList(db.ActivityTypes, "id", "typeName");
                ViewBag.checkedById = new SelectList(db.Admins, "id", "adminAccount");
                ViewBag.activityOrganizerId = new SelectList(db.Members, "id", "memberAccount");
                activityService.Edit(model.ToActivityEditDTO());
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

        // GET: Activities/Delete/1
        public ActionResult Delete(int id)
        {
            var data = activityService.GetById(id);
            return View(data.ToActivityEditVM());
        }

        // POST: Activities/Delete/1
        [HttpPost]
        public ActionResult Delete(ActivityEditVM model)
        {
            try
            {
                activityService.Delete(model.id);
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