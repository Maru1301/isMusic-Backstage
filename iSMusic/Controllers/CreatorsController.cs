using iSMusic.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iSMusic.Controllers
{
    public class CreatorsController : Controller
    {
        private AppDbContext db;

        public CreatorsController()
        {
            db = new AppDbContext();
        }

        // GET: Creators
        public ActionResult Index()
        {
            var data = db.Creators.ToList();

            return View(data);
        }

        public ActionResult AddNewCreator()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var data = db.Creators.Find(id);

            return View(data);
        }
    }
}