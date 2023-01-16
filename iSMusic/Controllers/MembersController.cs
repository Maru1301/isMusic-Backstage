using isMusic.Models.DTOs;
using iSMusic.Models.DTOs;
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
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using X.PagedList;

namespace iSMusic.Controllers
{
	public class MembersController : Controller
	{
		private AppDbContext db = new AppDbContext();
		private MemberService memberService;
		private IMemberRepository repo;
		public MembersController()
		{
			repo = new MemberRepository();
			this.memberService = new MemberService(repo);
		}

		// GET: Member
		//public ActionResult Index()
		//{
		//	var data = memberService.GetAll();
		//	return View(data);
		//}
		public ActionResult Index(int? Id, string Account, int pageNumber = 1)
		{
			pageNumber = pageNumber > 0 ? pageNumber : 1;

			// 將篩選條件放在ViewBag,稍後在 view page取回
			//ViewBag.Categories = GetCategories(categoryId);
			ViewBag.Account = Account;
			ViewBag.CategoryId = Id;

			//ViewBag.QueryString = $"CategoryId={categoryId.ToString()}&ProductName={HttpUtility.UrlEncode(productName)}";

			IPagedList<Member> pagedData = GetPagedProducts(Id, Account, pageNumber);

			return View(pagedData);
		}
		
		private IPagedList<Member> GetPagedProducts(int? Id, string Account, int pageNumber)
		{
			//var db = new AppDbContext();
			int pageSize = 3;

			var query = db.Members.Include("Avatar").OrderBy(x => x.id);

			return query.ToPagedList(pageNumber, pageSize);
		}
		// GET: Members/Register
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Register(RegisterVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var service = new MemberService(repo);

			(bool IsSuccess, string ErrorMessage) response =
				service.CreateNewMember(model.ToRequestDto());

			if (response.IsSuccess)
			{
				// 建檔成功 redirect to confirm page
				return View("RegisterConfirm");
			}
			else
			{
				ModelState.AddModelError(string.Empty, response.ErrorMessage);
				return View(model);
			}
		}

		public ActionResult EditProfile(int id)
		{
			//string currentUserAccount = User.Identity.Name;

			MemberDTO entity = repo.GetById(id);
			EditProfileVM model = entity.ToEditProfileVM();

			return View(model);
		}

		[HttpPost]
		public ActionResult EditProfile(EditProfileVM model)
		{
			//string currentUserAccount = User.Identity.Name;

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			//UpdateProfileDTO request = model.ToDTO(currentUserAccount);
			try
			{
				memberService.UpdateProfile(model.ToEditProfileDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid == true)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(model);
			}
		}

		public ActionResult DeleteAccount(int Id)
		{
			MemberDTO entity = repo.GetById(Id);
			EditProfileVM model = entity.ToEditProfileVM();

			return View(model);
		}

		[HttpPost]
		public ActionResult DeleteAccount(EditProfileVM model)
		{
			if (ModelState.IsValid == false)
			{
				return View(model);
			}
			try
			{
				memberService.DeleteAccount(model.ToEditProfileDTO());
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			}

			if (ModelState.IsValid == true)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return View(model);
			}
		}
	}
}
