using iSMusic.Models.EFModels;
using iSMusic.Models.Infrastructures;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static iSMusic.Controllers.ArtistsController;

namespace iSMusic.Controllers
{
	public class AlbumsController : Controller
	{
		private IAlbumRepository repository;

		public int PageSize { get; set; }

		public AlbumsController()
		{
			repository = new AlbumRepository();
			this.PageSize = 10;
		}

		// GET: Albums
		public ActionResult Index(AlbumCriteria criteria, string columnName, string direction, int pageNumber = 1)
		{
			IQueryable<AlbumIndexVM> query = repository.GetQuery();

			// 處理篩選功能
			//var criteria = PrepareCriteria();
			ViewBag.Criteria = criteria;
			query = criteria.ApplyCriteria(query);

			// 加入排序
			var sortInfo = new SortInfo(columnName, direction);
			sortInfo.UrlTemplate = "/Albums/Index?{0}" + "&" + criteria.GetQueryString();
			ViewBag.SortInfo = sortInfo;

			query = sortInfo.ApplySort(query);

			// 處理分頁功能
			int totalRecords = query.Count();

			var paginationInfo = new Models.Infrastructures.PaginationInfo(totalRecords, this.PageSize, pageNumber);
			ViewBag.Pagination = paginationInfo;

			var typeList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty},
				new SelectListItem{Text = "專輯", Value = "1"},
				new SelectListItem{Text = "EP", Value = "2"},
				new SelectListItem{Text = "單曲", Value = "3"}
			};
			ViewBag.TypeList = typeList;

			var list = paginationInfo.GetPagedData(query).ToList();

			return View(list);
		}

		// GET: Albums/Create
		public ActionResult Create()
		{
			ViewBag.ArtistList = GetArtistList();

			var songList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty}
			};
			ViewBag.SongList = songList;

			var typeList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty},
				new SelectListItem{Text = "專輯", Value = "1"},
				new SelectListItem{Text = "EP", Value = "2"},
				new SelectListItem{Text = "單曲", Value = "3"}
			};
			ViewBag.TypeList = typeList;

			ViewBag.albumGenreId = new SelectList(new AppDbContext().SongGenres, "id", "genreName").Prepend(new SelectListItem() { Text="請選擇", Value=string.Empty});

			return View();
		}

		// POST: Albums/Create
		[HttpPost]
		public ActionResult Create(AlbumCreateVM model)
		{
			var service = new AlbumService(repository);
			try
			{
				string coverPath = Server.MapPath("/Uploads/Covers");

				service.AddNewAlbum(coverPath, model.ToDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			ViewBag.ArtistList = GetArtistList();

			var songList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty}
			};
			ViewBag.SongList = songList;

			var typeList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty},
				new SelectListItem{Text = "專輯", Value = "1"},
				new SelectListItem{Text = "EP", Value = "2"},
				new SelectListItem{Text = "單曲", Value = "3"}
			};
			ViewBag.TypeList = typeList;

			ViewBag.albumGenreId = new SelectList(new AppDbContext().SongGenres, "id", "genreName").Prepend(new SelectListItem() { Text = "請選擇", Value = string.Empty });

			return View(model);
		}

		// GET: Albums/Edit/id
		public ActionResult Edit(int id)
		{
			var data = repository.FindById(id);
			data.albumCoverPath = "/Uploads/Covers/" + data.albumCoverPath;

			ViewBag.ArtistList = GetArtistList();

			var songList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty}
			};
			ViewBag.SongList = songList;

			ViewBag.typeId = new SelectList(new AppDbContext().AlbumTypes, "id", "typeName", data.typeId);
			ViewBag.albumGenreId = new SelectList(new AppDbContext().SongGenres, "id", "genreName", data.albumGenreId);

			return View(data);
		}

		// POST: Songs/Edit/id
		[HttpPost]
		public ActionResult Edit(AlbumEditVM model)
		{
			var service = new AlbumService(repository);
			try
			{
				string coverPath = Server.MapPath("/Uploads/Covers");

				service.UpdateAlbum(coverPath, model.ToDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			ViewBag.ArtistList = GetArtistList();

			var songList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty}
			};

			ViewBag.typeId = new SelectList(new AppDbContext().AlbumTypes, "id", "typeName", model.typeId);
			ViewBag.albumGenreId = new SelectList(new AppDbContext().SongGenres, "id", "genreName", model.albumGenreId);

			ViewBag.SongList = songList;

			return View(model);
		}

		public ActionResult Delete(int id)
		{
			var data = repository.FindById(id);

			return View(data);
		}

		[HttpPost]
		public ActionResult Delete(AlbumEditVM model)
		{
			var service = new AlbumService(repository);
			try
			{
				// TODO: Add delete logic here
				service.DeleteAlbum(model.id);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			return View(model);
		}

		private List<SelectListItem> GetArtistList()
		{
			var artists = new ArtistRepository().FindAll();
			var artistList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = string.Empty}
			};
			for (int i = 0; i < artists.Count; i++)
			{
				artistList.Add(new SelectListItem { Text = artists[i].artistName, Value = artists[i].id.ToString() });
			}

			return artistList;
		}

		//private AlbumCriteria PrepareCriteria()
		//{
		//	var criteria = new AlbumCriteria { input = Request["input"] };
		//	criteria.TypeId = null;
		//	if (int.TryParse(Request["albumTypeId"], out int value))
		//	{
		//		criteria.TypeId = value;
		//	}

		//	return criteria;
		//}

		public class AlbumCriteria
		{
			public int? AlbumTypeId { get; set; }
			public string input { get; set; }

			public string GetQueryString()
			{
				return $"TypeId={AlbumTypeId}&input={HttpUtility.UrlEncode(input)}";
			}

			public IQueryable<AlbumIndexVM> ApplyCriteria(IQueryable<AlbumIndexVM> query)
			{
				if (AlbumTypeId > 0)
				{
					query = query.Where(t => t.albumTypeId == AlbumTypeId);
				}

				if (!string.IsNullOrWhiteSpace(input))
				{
					query = query.Where(t => t.albumName.Contains(input));
				}

				return query;
			}

		}

		public class SortInfo : BaseSortInfo<AlbumIndexVM>
		{
			public override string[] ColumnNames { get => new string[] { "albumName", "released", "mainArtistName" }; }

			public SortInfo(string columnName, string direction) : base(columnName, direction, "albumName")
			{

			}

			public override IQueryable<AlbumIndexVM> ApplySort(IQueryable<AlbumIndexVM> data)
			{
				bool result = Enum.TryParse(this.ColumnName, out EnumColumn enumColumnName);
				if (!result)
				{
					enumColumnName = EnumColumn.albumName;
				}

				switch (enumColumnName)
				{
					case EnumColumn.albumName:
						return (IsAsc())
							? data.OrderBy(t => t.albumName).ThenBy(t => t.released)
							: data.OrderByDescending(t => t.albumName).ThenBy(t => t.released);

					case EnumColumn.released:
						return (IsAsc())
							? data.OrderBy(t => t.released).ThenBy(t => t.albumName)
							: data.OrderByDescending(t => t.released).ThenBy(t => t.albumName);

					case EnumColumn.mainArtistName:
						return (IsAsc())
							? data.OrderBy(t => t.mainArtistName)
							: data.OrderByDescending(t => t.mainArtistName);
				}

				return data;
			}
			public MvcHtmlString RenderItem(EnumColumn column)
			{
				return base.RenderItem(column.ToString());
			}
		}
		public enum EnumColumn
		{
			albumName, released, mainArtistName
		}
	}
}