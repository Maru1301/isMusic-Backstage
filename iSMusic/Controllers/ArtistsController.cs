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

			var paginationInfo = new PaginationInfo_110(totalRecords, pageSize, pageNumber);
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

		public class PaginationInfo_110
		{
			public PaginationInfo_110(int totalRecords, int pageSize, int pageNumber)
			{
				TotalRecords = totalRecords < 0 ? 0 : totalRecords;
				PageSize = pageSize < 1 ? 1 : pageSize;
				PageNumber = pageNumber < 1 ? 1 : pageNumber;
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

			//public IQueryable<TownShip> GetPagedData(IQueryable<TownShip> query)
			//{
			// int recordStartIndex = (PageNumber - 1) * PageSize;

			// return query.Skip(recordStartIndex).Take(PageSize);
			//}
			public IQueryable<T> GetPagedData<T>(IQueryable<T> query)
			{
				int recordStartIndex = (PageNumber - 1) * PageSize;

				return query.Skip(recordStartIndex).Take(PageSize);
			}

			public int PageItemPrevNumber => (PageBarStartNumber <= 1) ? 1 : PageBarStartNumber - 1;

			public int PageBarItemCount => PageBarStartNumber + PageItemCount > Pages
				? Pages - PageBarStartNumber + 1
				: PageItemCount;
			public int PageItemNextNumber => (PageBarStartNumber + PageItemCount >= Pages) ? Pages : PageBarStartNumber + PageItemCount;

		}
	}

	public static class Paged_110Ext
	{
		public static MvcHtmlString RenderPager(this ArtistsController.PaginationInfo_110 pagedInfo, Func<int, string> urlGenerator)
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