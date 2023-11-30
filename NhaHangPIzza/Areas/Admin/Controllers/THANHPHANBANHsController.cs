using NhaHangPIzza.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace NhaHangPIzza.Areas.Admin.Controllers
{
    public class THANHPHANBANHsController : Controller
    {
        private readonly QLNHAHANG_PIZZAEntities db = new QLNHAHANG_PIZZAEntities();

        // GET: Admin/THANHPHANBANHs
        public ActionResult Index()
        {
            return View(db.THANHPHANBANHs.ToList());
        }

        // GET: Admin/THANHPHANBANHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHPHANBANH tHANHPHANBANH = db.THANHPHANBANHs.Find(id);
            if (tHANHPHANBANH == null)
            {
                return HttpNotFound();
            }
            return View(tHANHPHANBANH);
        }

        // GET: Admin/THANHPHANBANHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/THANHPHANBANHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin/THANHPHANBANHs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdThanhPhan,TenThanhPhan,Giatien")] THANHPHANBANH tHANHPHANBANH)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem tên thành phần bánh đã tồn tại hay chưa
                if (IsThanhPhanBanhExist(tHANHPHANBANH.TenThanhPhan))
                {
                    ViewBag.Message = "Thành phần bánh đã tồn tại. Vui lòng nhập lại.";
                    return View(tHANHPHANBANH); // Trở lại view Create để hiển thị thông báo và giữ lại dữ liệu người dùng đã nhập
                }
                else
                {
                    // Thêm thành phần bánh vào cơ sở dữ liệu
                    db.THANHPHANBANHs.Add(tHANHPHANBANH);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(tHANHPHANBANH);
        }

        // GET: Admin/THANHPHANBANHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHPHANBANH tHANHPHANBANH = db.THANHPHANBANHs.Find(id);
            if (tHANHPHANBANH == null)
            {
                return HttpNotFound();
            }
            return View(tHANHPHANBANH);
        }

        // POST: Admin/THANHPHANBANHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdThanhPhan,TenThanhPhan,Giatien")] THANHPHANBANH tHANHPHANBANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHANHPHANBANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tHANHPHANBANH);
        }

        // GET: Admin/THANHPHANBANHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THANHPHANBANH tHANHPHANBANH = db.THANHPHANBANHs.Find(id);
            if (tHANHPHANBANH == null)
            {
                return HttpNotFound();
            }
            return View(tHANHPHANBANH);
        }

        // POST: Admin/THANHPHANBANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THANHPHANBANH tHANHPHANBANH = db.THANHPHANBANHs.Find(id);
            db.THANHPHANBANHs.Remove(tHANHPHANBANH);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IsThanhPhanBanhExist(string tenThanhPhan)
        {
            // Chuyển tên thành phần bánh về chữ thường để so sánh không phân biệt chữ hoa chữ thường
            string lowercaseTenThanhPhan = tenThanhPhan.ToLower();

            // Kiểm tra xem có thành phần bánh nào có tên giống với tên được truyền vào không
            return db.THANHPHANBANHs.Any(x => x.TenThanhPhan.ToLower() == lowercaseTenThanhPhan);
        }
    }
}