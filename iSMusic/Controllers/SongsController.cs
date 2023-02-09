using iSMusic.Models.DTOs;
using iSMusic.Models.EFModels;
using iSMusic.Models.Entities;
using iSMusic.Models.Infrastructures;
using iSMusic.Models.Infrastructures.Extensions;
using iSMusic.Models.Infrastructures.Repositories;
using iSMusic.Models.Services;
using iSMusic.Models.Services.Interfaces;
using iSMusic.Models.ViewModels;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static iSMusic.Controllers.AlbumsController;
using static iSMusic.Controllers.ArtistsController;

namespace iSMusic.Controllers
{
	public class SongsController : Controller
	{
		private ISongRepository repository;

		public SongsController()
		{
			repository = new SongRepository();
		}
		// GET: Songs
		public ActionResult Index(SongCriteria criteria, string columnName, string direction, int pageNumber = 1)
		{
			var service = new SongService(repository);

			ViewBag.Criteria = criteria;
			
			var sortInfo = new SortInfo(columnName, direction);
			sortInfo.UrlTemplate = "/Songs/Index?{0}" + "&" + criteria.GetQueryString();
			ViewBag.SortInfo = sortInfo;

			var list = service.Search(criteria, sortInfo, pageNumber, out Models.Infrastructures.PaginationInfo paginationInfo).ToList();

			// 處理分頁功能
			ViewBag.Pagination = paginationInfo;

			ViewBag.SongGenreList = GetGenreList();
			ViewBag.LanguageList = GetLanguageList();
			ViewBag.StatusList = new List<SelectListItem>()
			{
				new SelectListItem { Text = "請選擇", Value = ""},
				new SelectListItem { Text = "已上架", Value = "True"},
				new SelectListItem { Text = "已下架", Value = "False"}
			};

			return View(list.Select(item=> item.ToIndexVM()));
		}

		// launch a song
		// PUT: Songs/LaunchSong
		[HttpPost]
		public JsonResult LaunchSong(List<SongIdInfo> info)
		{
			var service = new SongService(repository);
			
			try
			{
				return Json(service.LaunchSong(info.Select(i=> i.Id).ToList()));
			}
			catch(Exception ex)
			{
				return Json(ex.Message);
			}
		}

		// recall a song
		public JsonResult RecallSong(List<SongIdInfo> info)
		{
			var service = new SongService(repository);

			try
			{
				return Json(service.RecallSong(info.Select(i => i.Id).ToList()));
			}
			catch (Exception ex)
			{
				return Json(ex.Message);
			}
		}

		// GET: Songs/Create
		public ActionResult Create()
		{
			ViewBag.ArtistList = GetArtistList();
			ViewBag.GenreList = GetGenreList();
			ViewBag.LanguageList = GetLanguageList();

			return View();
		}

		// POST: Songs/Create
		[HttpPost]
		public ActionResult Create(SongCreateVM model)
		{
			var service = new SongService(repository);
			try
			{
				string coverPath = Server.MapPath("/Uploads/Covers");
				string songPath = Server.MapPath("/Uploads/Songs");

				service.AddNewSong(coverPath, songPath, model.ToDTO());
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
			ViewBag.GenreList = GetGenreList();
			ViewBag.LanguageList = GetLanguageList();

			return View(model);
		}

		private List<SelectListItem> GetArtistList()
		{
			var artists = new ArtistRepository().FindAll();

			var artistList = new List<SelectListItem>()
		{
			new SelectListItem{Text = "請選擇", Value = "0"}
		};
			for (int i = 0; i < artists.Count; i++)
			{
				artistList.Add(new SelectListItem { Text = artists[i].artistName, Value = artists[i].id.ToString() });
			}

			return artistList;
		}

		private List<SelectListItem> GetGenreList()
		{
			var genres = new SongGenreRepository().GetAll().ToList();

			var list = new List<SelectListItem>();
			var genreList = new List<SelectListItem>()
			{
				new SelectListItem{Text = "請選擇", Value = ""}
			};
			for (int i = 0; i < genres.Count; i++)
			{
				genreList.Add(new SelectListItem { Text = genres[i].genreName, Value = genres[i].id.ToString() });
			}

			return genreList;
		}

		private List<SelectListItem> GetLanguageList()
		{
			return new List<SelectListItem>()
		{
			new SelectListItem { Text = "請選擇", Value = ""},
			new SelectListItem { Text = "English" , Value= "English"},
			new SelectListItem { Text = "Español", Value= "Español" },
			new SelectListItem { Text = "中文", Value= "中文" },
			new SelectListItem { Text = "Français", Value= "Français" },
			new SelectListItem { Text = "Deutsch", Value= "Deutsch" },
			new SelectListItem { Text = "日本語", Value= "日本語" },
			new SelectListItem { Text = "Português", Value= "Português" },
		};
		}

		// GET: Songs/Edit/5
		public ActionResult Edit(int id)
		{
			SongDTO data = null;
			try
			{
				data = repository.FindById(id);

				data.songCoverPath = "/Uploads/Covers/" + data.songCoverPath;
				data.songPath = "\\Uploads\\Songs\\" + data.songPath;

				ViewBag.ArtistList = GetArtistList();
				ViewBag.GenreList = GetGenreList();
				ViewBag.LanguageList = GetLanguageList();
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid)
			{
				return View(data.ToEditVM());
			}

			return RedirectToAction("Index"); ;
		}

		// POST: Songs/Edit/5
		[HttpPost]
		public ActionResult Edit(SongEditVM model)
		{
			
			var service = new SongService(repository);
			try
			{
				string coverPath = Server.MapPath("/Uploads/Covers");
				string songPath = Server.MapPath("/Uploads/Songs");

				service.EditSong(coverPath, songPath, model.ToDTO());
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
			ViewBag.GenreList = GetGenreList();
			ViewBag.LanguageList = GetLanguageList();

			return View(model);
		}

		// GET: Songs/Delete/5
		public ActionResult Delete(int id)
		{
			var data = repository.FindById(id);

			return View(data.ToDeleteVM());
		}

		// POST: Songs/Delete/5
		[HttpPost]
		public ActionResult Delete(SongDeleteVM model)
		{			
			var service = new SongService(repository);
			try
			{
				// TODO: Add delete logic here
				service.DeleteSong(model.ToDTO());

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

		//GET: Songs/GetSongList/artistId
		public JsonResult GetSongList(int artistId)
		{
			var service = new SongService(repository);
			var data = service.GetSongList(artistId);
			return Json(data, JsonRequestBehavior.AllowGet);
		}

		public class SongCriteria
		{
			public bool? Status { get; set; }

			public int? SongGenreId { get; set; }

			public string Language { get; set; }

			public string input { get; set; }

			public string GetQueryString()
			{
				return $"Status={Status}&SongGenreId={SongGenreId}&Language={HttpUtility.UrlEncode(Language)}&input={HttpUtility.UrlEncode(input)}";
			}

			public IQueryable<Song> ApplyCriteria(IQueryable<Song> query)
			{
				if (Status.HasValue)
				{
					query = query.Where(q => q.status == Status);
				}

				if (SongGenreId > 0)
				{
					query = query.Where(q => q.SongGenre.id == SongGenreId);
				}

				if (!string.IsNullOrEmpty(Language))
				{
					query = query.Where(t => t.language == Language);
				}

				if (!string.IsNullOrWhiteSpace(input))
				{
					query = query.Where(t => t.songName.Contains(input));
				}

				return query;
			}

		}

		public class SortInfo : BaseSortInfo<Song>
		{
			public override string[] ColumnNames { get=> new string[] { "songName", "SongGenreName", "Language", "released", "duration"}; }

			public SortInfo(string columnName, string direction) : base(columnName, direction, "songName")
			{

			}

			public override IQueryable<Song> ApplySort(IQueryable<Song> data)
			{
				bool result = Enum.TryParse(this.ColumnName, out EnumColumn enumColumnName);
				if (!result)
				{
					enumColumnName = EnumColumn.songName;
				}

				switch (enumColumnName)
				{
					case EnumColumn.songName:
						return (IsAsc())
							? data.OrderBy(t => t.songName).ThenBy(t => t.released)
							: data.OrderByDescending(t => t.songName).ThenBy(t => t.released);

					case EnumColumn.SongGenreName:
						return (IsAsc())
							? data.OrderBy(t => t.SongGenre.genreName).ThenByDescending(t => t.released)
							: data.OrderByDescending(t => t.SongGenre.genreName).ThenByDescending(t => t.released);

					case EnumColumn.released:
						return (IsAsc())
							? data.OrderBy(t => t.released).ThenBy(t => t.songName)
							: data.OrderByDescending(t => t.released).ThenBy(t => t.songName);

					case EnumColumn.Language:
						return (IsAsc())
							? data.OrderBy(t => t.language)
							: data.OrderByDescending(t => t.language);

					case EnumColumn.duration:
						return (IsAsc())
							? data.OrderBy(t => t.duration)
							: data.OrderByDescending(t => t.duration);

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
			songName, SongGenreName, Language, released, duration
		}

		public class SongIdInfo
		{
			public int Id { get; set; }
		}
	}
}