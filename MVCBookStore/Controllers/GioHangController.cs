using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookStore.Models;

namespace MVCBookStore.Controllers
{
    public class GioHangController : Controller
    {
        // Tạo đối tượng data chứa dữ liệu từ model QLBanSach đã tạo
        QLBANSACHEntities data = new QLBANSACHEntities();

        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        // Lấy giỏ hàng 
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                // Nếu giỏ hàng chưa tồn tại thì khởi tạo listGioHang
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        // Thêm hàng vào giỏ
        public ActionResult ThemGioHang (int iMaSach, string strURL)
        {
            // Lấy ra Session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra sách này tồn tại trong Session["GioHang"] chưa?
            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if(sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        // Tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            return iTongSoLuong;
        }

        // Tính tổng tiền
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
                iTongTien = lstGioHang.Sum(n => n.dThanhTien);
            return iTongTien;
        }

        // Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
                return RedirectToAction("Index", "BookStore");
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }

        // Hiển thị giỏ hàng
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        // Xóa sản phẩm khỏi giỏ hàng
        public ActionResult XoaGioHang(int iMaSp)
        {
            // Lấy giỏ hàng từ Session
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra sản phẩm đã có trong Session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSp);
            // Nếu đã tồn tại thì cho sửa số lượng
            if(sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == iMaSp);
                return RedirectToAction("GioHang");
            }
            if (lstGioHang.Count == 0)
                return RedirectToAction("Index", "BookStore");
            return RedirectToAction("GioHang");
        }

        // Cập nhật giỏ hàng
        //[HttpPost]
        public ActionResult CapNhatGioHang(int iMaSp, FormCollection collection)
        {
            // Lấy giỏ hàng từ Session 
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra sản phẩm đã có trong Session["GioHang"]
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSp);
            // Nếu tồn tại thì cho sửa SoLuong
            if(sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(collection["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        // Xóa tất cả sản phẩm khỏi giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            // Lấy giỏ hàng từ Session["GioHang"]
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "BookStore");
        }
    }
}