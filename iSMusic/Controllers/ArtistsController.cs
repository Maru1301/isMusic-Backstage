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
		public ActionResult Index()
		{
			var data = service.FindAll();

			return View(data);
		}

		// GET: Artists/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Artists/Create
		[HttpPost]
		public ActionResult Create(Artist model)
		{
			try
			{
				service.Create(model.ToDTO());
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

			return View(data);
		}

		// POST: Artists/Edit/5
		[HttpPost]
		public ActionResult Edit(Artist model)
		{
			try
			{
				service.Edit(model.ToDTO());
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

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}