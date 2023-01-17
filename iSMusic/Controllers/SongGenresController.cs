using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSMusic.Controllers
{
    public class SongGenresController : Controller
    {
		private SongGenreRepository repository;
		private SongGenreService service;

		public SongGenresController()
		{
			repository = new SongGenreRepository();
			service = new SongGenreService(repository);
		}

		// GET: SongGenres
		public ActionResult Index(int pageNumber = 1, string genreName = "")
		{
			ViewBag.GenreName = genreName;

			pageNumber = pageNumber < 1 ? 1 : pageNumber;

			int pageSize = 10;

			int recordStartIndex = (pageNumber - 1) * pageSize;

			int totalRecords = repository.GetTotalRecordsNum();

			ViewBag.Pagination = new iSMusic.Models.Infrastructures.PaginationInfo(totalRecords, pageSize, pageNumber);

			var sizeList = new List<SelectListItem>
			{
				new SelectListItem { Text = "5", Value = "5" },
				new SelectListItem { Text = "10", Value = "10" },
				new SelectListItem { Text = "25", Value = "25" },
			};

			ViewBag.PageSizeList = sizeList;

			var data = service.Index(pageSize, recordStartIndex, genreName);

			return View(data);
		}

		// GET: SongGenres/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SongGenres/Create
		[HttpPost]
		public ActionResult Create(SongGenre model)
		{
			try
			{
				service.Create(model.ToDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "新增失敗:" + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		// GET: SongGenres/Edit/5
		public ActionResult Edit(int id)
		{
			var data = service.Edit(id);

			return View(data);
		}

		// POST: SongGenres/Edit/5
		[HttpPost]
		public ActionResult Edit(SongGenre model)
		{
			try
			{
				service.Edit(model.ToDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "更新失敗:" + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}

		// GET: SongGenres/Delete/5
		public ActionResult Delete(int id)
		{
			var data = service.Delete(id);

			return View(data);
		}

		// POST: SongGenres/Delete/5
		[HttpPost]
		public ActionResult Delete(SongGenre model)
		{
			foreach (var key in ModelState.Keys) ModelState[key].Errors.Clear();
			try
			{
				service.Delete(model.ToDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, "刪除失敗:" + ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}