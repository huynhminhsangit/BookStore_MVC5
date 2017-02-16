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
        // Create new subject contains all data from database
        QLBANSACHEntities data = new QLBANSACHEntities();

        private List<SACH> getNewBook (int count)
        {
            // Sort by NgayCapNhat, get count first line
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // GET: BookStore
        public ActionResult Index()
        {
            // Get 5 new book
            var newBook = getNewBook(5);

            return View();
        }
    }
}