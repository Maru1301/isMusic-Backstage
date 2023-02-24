using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures;
using iSMusic.Models.ViewModels;

namespace iSMusic.Controllers
{
    public class ArtistsController : Controller
    {
		private IArtistRepository repository;
		private ArtistService service;

		public ArtistsController()
		{
			repository = new ArtistRepository();
			service = new ArtistService(repository);
		}

		// GET: Artists
		public ActionResult Index(int pageNumber = 1)
		{
			// prepare the list for isband
			var isBandList = new List<SelectListItem>() 
			{ 
				new SelectListItem{ Text = "全部"},
				new SelectListItem{ Text = "單人", Value= "False"},
				new SelectListItem{ Text = "樂團", Value= "True"},
			};
			ViewBag.IsBandList = isBandList;

			var criteria = PrepareCriteria();
			ViewBag.Criteria = criteria;

			IQueryable<Artist> query = service.GetQuery();
			query = criteria.ApplyCriteria(query);

			// 處理分頁功能
			int pageSize = 3;

			int totalRecords = query.Count();

			var paginationInfo = new PaginationInfo(totalRecords, pageSize, pageNumber);
			ViewBag.Pagination = paginationInfo;

			IQueryable<Artist> data = query.OrderBy(t => t.artistName);

			var list = paginationInfo.GetPagedData(data).ToList();

			return View(list);
		}

		private Criteria PrepareCriteria()
		{
			var criteria = new Criteria { PerformerName = Request["PerformerName"] };
			criteria.isBand = null;
			if (bool.TryParse(Request.Params.Get(0), out bool value))
			{
				criteria.isBand = value;
			}

			return criteria;
		}

		public class Criteria
		{
			public bool? isBand { get; set; }
			public string PerformerName { get; set; }

			public string GetQueryString()
			{
				return $"isBand={HttpUtility.UrlEncode(isBand.ToString())}&PerformerName={HttpUtility.UrlEncode(PerformerName)}";
			}

			public IQueryable<Artist> ApplyCriteria(IQueryable<Artist> query)
			{
				if (isBand.HasValue)
				{
					query = query.Where(q => q.isBand == isBand);
				}

				if (!string.IsNullOrWhiteSpace(PerformerName))
				{
					query = query.Where(t => t.artistName.Contains(PerformerName));
				}

				return query;
			}

		}

		// GET: Artists/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Artists/Create
		[HttpPost]
		public ActionResult Create(ArtistVM model)
		{
			try
			{
                string coverPath = Server.MapPath("/Uploads/Covers");

                service.Create(model.ToDTO(), coverPath);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "新增失敗," + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		// GET: Artists/Edit/5
		public ActionResult Edit(int id)
		{
			var data = service.FindById(id);
			data.artistPicPath = "/Uploads/Covers/" + data.artistPicPath;

			return View(data.ToVM());
		}

		// POST: Artists/Edit/5
		[HttpPost]
		public ActionResult Edit(ArtistVM model)
		{
			try
			{
                string coverPath = Server.MapPath("/Uploads/Covers");

                service.Edit(model.ToDTO(), coverPath);
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

		// GET: Artists/Delete/5
		public ActionResult Delete(int id)
		{
			var data = service.FindById(id);

			return View(data);
		}

		// POST: Artists/Delete/5
		[HttpPost]
		public ActionResult Delete(ArtistDTO model)
		{
			try
			{
				// TODO: Add update logic here
				service.Delete(model);
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "刪除失敗," + ex.Message);
			}

			return RedirectToAction("Index");
		}
	}
}