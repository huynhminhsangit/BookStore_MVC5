using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCBookStore.Models
{
    public class GioHang
    {
        // Tao doi tuong data chứa dữ liệu từ model dbQLBanSach đã tạo
        QLBANSACHEntities data = new QLBANSACHEntities();

        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public Double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public Double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        // Khởi tạo giỏ hàng theo MaSach được truyền vào với số lượng mặc định là 1
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMaSach);
            sTenSach = sach.Tensach;
            sAnhBia = sach.Anhbia;
            dDonGia = double.Parse(sach.Giaban.ToString());
            iSoLuong = 1;
        }
    }
}