using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookStore.Models;

namespace MVCBookStore.Controllers
{
    public class BookStoreController : Controller
    {
        // GET: BookStore
        public ActionResult Index()
        {
            // Get 5 new book
            var newBook = getNewBook(5);

            return View(newBook.ToList());
        }

        // Create new subject contains all data from database
        QLBANSACHEntities data = new QLBANSACHEntities(); // gọi đối tượng context đó muốn đổi tên thì vào web config đổi cũng được

        private List<SACH> getNewBook(int count)
        {
            // Sort by NgayCapNhat, get count first line
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // Get ChuDe
        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        // Get NhaXuatBan
        public ActionResult NhaXuatBan()
        {
            var nhaXuatBan = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaXuatBan);
        }

        // Get Product follow ChuDe
        public ActionResult SPTheoChuDe(int id)
        {
            var sachTheoChuDe = from sach in data.SACHes where sach.MaCD == id select sach;
            return View(sachTheoChuDe);
        }

        // Get Product follow NhaXuatBan
        public ActionResult SPTheoNXB(int id)
        {
            var sachTheoNXB = from sach in data.SACHes where sach.MaNXB == id select sach;
            return View(sachTheoNXB);
        }

        // Get Details Data for items
        public ActionResult Details(int id)
        {
            var chiTietSach = from sach in data.SACHes where sach.Masach == id select sach;
            return View(chiTietSach.Single());
        }
    }
}