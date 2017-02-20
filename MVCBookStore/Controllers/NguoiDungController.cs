using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBookStore.Models;

namespace MVCBookStore.Controllers
{
    public class NguoiDungController : Controller
    {

        QLBANSACHEntities data = new QLBANSACHEntities();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }

        // Sign Up - Đăng ký
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        // Hàm Post nhận dữ liệu từ trang đăng ký và thực hiện việc tạo mới dữ liệu
        [HttpPost]
        public ActionResult Signup(FormCollection collection, KHACHHANG kh)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var fullName = collection["HoTenKH"];
            var userName = collection["TenDangNhap"];
            var passWord = collection["MatKhau"];
            var rePassWord = collection["MatKhauNL"];
            var birthDay = string.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var phoneNumber = collection["DienThoai"];
            var email = collection["Email"];
            var address = collection["DiaChi"];
            // Kiểm tra giá trị nhập vào
            if (String.IsNullOrEmpty(fullName))
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            else if (String.IsNullOrEmpty(userName))
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            else if (String.IsNullOrEmpty(passWord))
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            else if (String.IsNullOrEmpty(rePassWord))
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            else if (String.IsNullOrEmpty(phoneNumber))
                ViewData["Loi5"] = "Phải nhập số điện thoại";
            else if (String.IsNullOrEmpty(email))
                ViewData["Loi6"] = "Phải nhập email, không được để trống";
            else
            {
                // Gán giá trị cho đối tượng tạo mới
                kh.HoTen = fullName;
                kh.Taikhoan = userName;
                kh.Matkhau = passWord;
                kh.Email = email;
                kh.DiachiKH = address;
                kh.DienthoaiKH = phoneNumber;
                kh.Ngaysinh = DateTime.Parse(birthDay);
                // Insert Data
                data.KHACHHANGs.Add(kh);
                data.SaveChanges();

                return RedirectToAction("Signup");

            }
            return this.Signup();
        }

        // Sign In - Đăng nhập
        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }
        // Hàm Post nhận dữ liệu từ trang đăng nhập và thực hiện việc tạo mới phiên làm việc
        [HttpPost]
        public ActionResult Signin(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var userName = collection["TenDangNhap"];
            var passWord = collection["MatKhau"];
            if (String.IsNullOrEmpty(userName))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(passWord))
            {
                ViewData["Loi2"] = "Chưa nhập mật khẩu";
            }
            else
            {
                // Gán giá trị cho đối tượng tạo mới (kh)
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == userName && n.Matkhau == passWord);
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đã đăng nhập thành công!";
                    Session["TaiKhoan"] = kh;
                    ViewBag.tenkh = kh.HoTen;
                    return RedirectToAction("Index", "BookStore");
                    
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
            return View();
        }

        public ActionResult showNguoiDung()
        {
            KHACHHANG kh = new KHACHHANG() ;
            if (Session["TaiKhoan"] != null) {
                kh = (KHACHHANG)Session["TaiKhoan"];
                ViewBag.TENKH = kh.HoTen;
            }
            return PartialView();
        }
    }
}