using NhaHangPIzza.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Controllers
{
    public class NuocGiaiKhatController : Controller
    {
        // GET: NuocGiaiKhat
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        public ActionResult Index()
        {
            // Retrieve the list of beverages from the database
            List<NuocUong> danhSachNuocUong = db.NuocUongs.ToList();

            ViewBag.DanhSachNuocUong = danhSachNuocUong;

            return View();
        }
    }
}