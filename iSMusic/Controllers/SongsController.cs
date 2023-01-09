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
		public ActionResult Index()
		{
			var service = new SongService(repository);
			var data = service.Index();

			return View(data);
		}

		// launch a song
		public ActionResult LaunchSong(int songId)
		{
			return Index();
		}

		// recall a song
		public ActionResult RecallSong(int songId)
		{
			return Index();
		}

		// GET: Songs/Create
		public ActionResult Create()
		{
			var artists = new ArtistRepository().FindAll();
			var genres = new SongGenreRepository().GetAll().ToList();

			var artistList = new List<SelectListItem>()
		{
			new SelectListItem{Text = "請選擇", Value = string.Empty}
		};
			for (int i = 0; i < artists.Count; i++)
			{
				artistList.Add(new SelectListItem { Text = artists[i].artistName, Value = artists[i].id.ToString() });
			}

			ViewBag.ArtistList = artistList;

			var genreList = new List<SelectListItem>()
		{
			new SelectListItem{Text = "請選擇", Value = string.Empty}
		};
			for (int i = 0; i < genres.Count; i++)
			{
				genreList.Add(new SelectListItem { Text = genres[i].genreName, Value = genres[i].id.ToString() });
			}

			ViewBag.GenreList = genreList;

			var languageList = new List<SelectListItem>()
		{
			new SelectListItem{Text = "---請選擇---", Value = string.Empty},
			new SelectListItem { Text = "English", Value = "English" },
			new SelectListItem { Text = "Español", Value = "Español" },
			new SelectListItem { Text = "中文", Value = "中文" },
			new SelectListItem { Text = "Français", Value = "Français" },
			new SelectListItem { Text = "Deutsch", Value = "Deutsch" },
			new SelectListItem { Text = "日本語", Value = "日本語" },
			new SelectListItem { Text = "Português", Value = "Português" },
		};

			ViewBag.LanguageList = languageList;

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
			new SelectListItem{Text = "請選擇", Value = "0"}
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
			new SelectListItem{Text = "---請選擇---"},
			new SelectListItem { Text = "No Lyric" },
			new SelectListItem { Text = "English" },
			new SelectListItem { Text = "Español" },
			new SelectListItem { Text = "中文" },
			new SelectListItem { Text = "Français" },
			new SelectListItem { Text = "Deutsch" },
			new SelectListItem { Text = "日本語" },
			new SelectListItem { Text = "Português" },
		};
		}

		// GET: Songs/Edit/5
		public ActionResult Edit(int id)
		{
			SongEditVM data = null;
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
				return View(data);
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

			return View(data);
		}

		// POST: Songs/Delete/5
		[HttpPost]
		public ActionResult Delete(SongEditVM model)
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
	}
}