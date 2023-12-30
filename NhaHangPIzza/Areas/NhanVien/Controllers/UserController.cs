using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhaHangPIzza.Areas.NhanVien.Controllers
{
    public class UserController : Controller
    {
        // GET: NhanVien/User
        public ActionResult Index()
        {
            return View();
        }
    }
}