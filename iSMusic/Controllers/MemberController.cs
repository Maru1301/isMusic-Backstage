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
    public class MemberController : Controller
    {
		private MemberService memberService;
		private IMemberRepository repo;
		public MemberController()
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
	}
}