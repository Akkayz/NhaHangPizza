using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class ComboController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Combo
        public ActionResult Index()
        {
            // Retrieve the list of beverages from the database
            List<Combo> danhSachComBo = db.Comboes.ToList();

            ViewBag.DanhSachComBo = danhSachComBo;

            return View();
        }
    }
}