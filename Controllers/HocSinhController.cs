using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HocSinhController : Controller
    {
        private HocSinhDB db=new HocSinhDB();
        public ActionResult Xemdanhsach(string searchString)
        {
            var hocsinh = db.HocSinhs.Select(p => p);
            if (!string.IsNullOrEmpty(searchString))
            {
                hocsinh =hocsinh.Where(x=>x.hoten.Contains(searchString));
            }

            return View(hocsinh.ToList());
        }



        public ActionResult Themdulieu()
        {
            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop");
            return View();
        }

        [HttpPost]
        public ActionResult Themdulieu(HocSinh hs)
        {
            try
            {
                hs.anhduthi = "";
                var path = Request.Files["imageFile"];
                if (path.ContentLength > 0 && path != null)
                {
                    string fileName = System.IO.Path.GetFileName(path.FileName);
                    string upload = Server.MapPath("~/Images/" + fileName);
                    path.SaveAs(upload);
                    hs.anhduthi = fileName;
                }
                db.HocSinhs.Add(hs);
                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }
            catch (Exception ex)
            {
                return View(hs);
            }
        }


        public ActionResult XoaDuLieu(string id)
        {
            HocSinh hs= db.HocSinhs.Find(id);
            return View(hs);
        }



        [HttpPost, ActionName("XoaDuLieu")]
        public ActionResult DeleteConfirm(string id)
        {
            HocSinh hs = db.HocSinhs.Find(id);
            db.HocSinhs.Remove(hs);
            db.SaveChanges();
            return RedirectToAction("Xemdanhsach");
        }


        public ActionResult ThongKe()
        {
            var thongKe = db.HocSinhs
        .GroupBy(hs => hs.LopHoc)
        .Select(group => new ThongKeViewModel
        {
            LopID = group.Key.malop,
            LopTen = group.Key.tenlop, // Thay thế tên lớp từ trường `tenlop` của `LopHoc`
            SoHocSinh = group.Count()
        })
        .OrderBy(x => x.LopID) // Hoặc x.TenLop nếu bạn muốn sắp xếp theo tên lớp
        .ToList();

            return View(thongKe);
        }

        public ActionResult Edit(string id)
        {
            HocSinh hs = db.HocSinhs.Find(id);
            if (hs == null)
            {
                return HttpNotFound();
            }

            ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hs.malop);
            return View(hs);
        }

        [HttpPost]
        public ActionResult Edit(HocSinh hs)
        {
            try
            {
                // Xử lý hình ảnh
                var path = Request.Files["imageFile"];
                if (path.ContentLength > 0 && path != null)
                {
                    string fileName = System.IO.Path.GetFileName(path.FileName);
                    string upload = Server.MapPath("~/Images/" + fileName);
                    path.SaveAs(upload);
                    hs.anhduthi = fileName;
                }

                db.Entry(hs).State = System.Data.Entity.EntityState.Modified;

                ViewBag.malop = new SelectList(db.LopHocs, "malop", "tenlop", hs.malop);

                db.SaveChanges();
                return RedirectToAction("Xemdanhsach");
            }
            catch (Exception ex)
            {
                return View(hs);
            }
        }




    }
}