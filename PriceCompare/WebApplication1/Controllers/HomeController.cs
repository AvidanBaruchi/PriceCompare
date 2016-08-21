using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PricesEntitiesModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var db = new PricesEntitiesModel.PricesContext("DefaultConnection");

            var chain = new Chain();

            chain.Name = "abc";
            db.Chains.Add(chain);
            //db.Chains.Add(new Chain());
            //db.SaveChanges();
            return View();
        }
    }
}
