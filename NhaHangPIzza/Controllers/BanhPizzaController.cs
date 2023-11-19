using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class BanhPizzaController : Controller
    {
        // GET: BanhPizza
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BanhPizza()
        {
            return View();
        }
    }
}