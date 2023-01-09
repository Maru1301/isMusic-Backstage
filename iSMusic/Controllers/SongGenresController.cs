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

			ViewBag.Pagination = new PaginationInfo(totalRecords, pageSize, pageNumber);

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

		public class PaginationInfo
		{
			public PaginationInfo(int totalRecords, int pageSize, int pageNumber)
			{
				TotalRecords = totalRecords;
				PageSize = pageSize;
				PageNumber = pageNumber;
			}

			public int TotalRecords { get; set; }
			public int PageSize { get; set; }
			public int PageNumber { get; set; }

			public int Pages => (int)Math.Ceiling((double)TotalRecords / PageSize);

			public int PageItemCount => 5;

			public int PageBarStartNumber
			{
				get
				{
					int startNumber = PageNumber - ((int)Math.Floor((double)this.PageItemCount / 2));
					return startNumber < 1 ? 1 : startNumber;
				}
			}

			public int PageItemPrevNumber => (PageBarStartNumber <= 1) ? 1 : PageBarStartNumber - 1;

			public int PageBarItemCount => PageBarStartNumber + PageItemCount > Pages
				? Pages - PageBarStartNumber + 1
				: PageItemCount;
			public int PageItemNextNumber => (PageBarStartNumber + PageItemCount >= Pages) ? Pages : PageBarStartNumber + PageItemCount;
		}
	}

	public static class Paged_3Ext
	{
		public static MvcHtmlString RenderPager(this SongGenresController.PaginationInfo pagedInfo,
			Func<int, string> urlGenerator)
		{
			string result = @"
			<nav aria-label=""Page navigation"">
    <ul class=""pagination"">";

			if (pagedInfo.PageNumber >= 1)
			{
				string prevUrl = urlGenerator(pagedInfo.PageItemPrevNumber);
				result += $@"<li>
                <a href=""{prevUrl}"" aria-label=""Previous"">
                    <span aria-hidden=""true"">&laquo;</span>
                </a>
            </li>";
			}

			for (int i = 0; i < pagedInfo.PageBarItemCount; i++)
			{
				int currentPageNumber = pagedInfo.PageBarStartNumber + i;
				string url = urlGenerator(currentPageNumber);

				string className = pagedInfo.PageBarStartNumber + i == pagedInfo.PageNumber ? "active" : "";

				result += $@"
            <li class=""{className}""><a href=""{url}"">{currentPageNumber}</a></li>";
			}

			if (pagedInfo.PageNumber < pagedInfo.Pages)
			{
				string nextUrl = urlGenerator(pagedInfo.PageItemNextNumber);
				result += $@"
            <li>
                <a href=""{nextUrl}"" aria-label=""Next"">
                    <span aria-hidden=""true"">&raquo;</span>
                </a>
            </li>";
			}

			result += @"
                </ul>
            </nav>";

			return new MvcHtmlString(result);
		}
	}
}