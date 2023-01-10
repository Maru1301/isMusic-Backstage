using iSMusic.Models.EFModels;
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
		IAlbumRepository repository;

		public AlbumsController()
		{
			repository = new AlbumRepository();
		}

		// GET: Albums
		public ActionResult Index(AlbumCriteria criteria, string columnName, string direction, int pageNumber = 1)
		{
			IQueryable<Album> query = repository.GetQuery();

			// 處理篩選功能
			ViewBag.Criteria = criteria;
			query = criteria.ApplyCriteria(query);

			// 加入排序
			var sortInfo = new SortInfo(columnName, direction);
			sortInfo.UrlTemplate = "/P111/Index01?{0}" + "&" + criteria.GetQueryString();
			ViewBag.SortInfo = sortInfo;

			query = sortInfo.ApplySort(query);
			// 處理分頁功能

			var service = new AlbumService(repository);
			var data = service.Index();

			return View(data);
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

			return View(model);
		}

		// GET: Albums/Edit/id
		public ActionResult Edit(int id)
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

			var data = repository.FindById(id);
			data.albumCoverPath = "/Uploads/Covers/" + data.albumCoverPath;

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

		public class AlbumCriteria
		{
			public int TypeId { get; set; }
			public string input { get; set; }

			public string GetQueryString()
			{
				return $"TypeId={TypeId}&input={HttpUtility.UrlEncode(input)}";
			}

			public IQueryable<Album> ApplyCriteria(IQueryable<Album> query)
			{
				if (TypeId > 0)
				{
					query = query.Where(t => t.albumTypeId == TypeId);
				}

				if (!string.IsNullOrWhiteSpace(input))
				{
					query = query.Where(t => t.albumName.Contains(input));
				}

				return query;
			}

		}

		public class SortInfo
		{
			public SortInfo(string columnName, string direction) : base(columnName, direction, "CityDisplayOrder")
			{

			}

			public override IQueryable<Album> ApplySort(IQueryable<Album> data)
			{
				bool result = Enum.TryParse(this.ColumnName, out EnumColumn enumColumnName);
				if (!result)
				{
					enumColumnName = EnumColumn.CityDisplayOrder;
				}

				switch (enumColumnName)
				{
					case EnumColumn.CityDisplayOrder:
						return (IsAsc())
							? data.OrderBy(t => t.City.DisplayOrder).ThenBy(t => t.DisplayOrder)
							: data.OrderByDescending(t => t.City.DisplayOrder).ThenBy(t => t.DisplayOrder);

					case EnumColumn.TownshipDisplayOrder:
						return (IsAsc())
							? data.OrderBy(t => t.City.DisplayOrder).ThenBy(t => t.DisplayOrder)
							: data.OrderBy(t => t.City.DisplayOrder).ThenByDescending(t => t.DisplayOrder);

					case EnumColumn.TownShipName:
						return (IsAsc())
							? data.OrderBy(t => t.TownShipName)
							: data.OrderByDescending(t => t.TownShipName);

					case EnumColumn.CityName:
						return (IsAsc())
							? data.OrderBy(t => t.City.CityName).ThenBy(t => t.DisplayOrder)
							: data.OrderByDescending(t => t.City.CityName).ThenBy(t => t.DisplayOrder);
				}

				return data;
			}
		}
		public enum EnumColumn
		{
			CityName, CityDisplayOrder, TownShipName, TownshipDisplayOrder
		}
	}
}