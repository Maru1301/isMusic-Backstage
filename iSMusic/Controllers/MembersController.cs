using isMusic.Models.DTOs;
using iSMusic.Models.DTOs;
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

namespace iSMusic.Controllers
{
	public class MembersController : Controller
	{
		private MemberService memberService;
		private IMemberRepository repo;
		public MembersController()
		{
			repo = new MemberRepository();
			this.memberService = new MemberService(repo);
		}

		// GET: Member
		public ActionResult Index()
		{
			var data = memberService.GetAll();
			return View(data);
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
