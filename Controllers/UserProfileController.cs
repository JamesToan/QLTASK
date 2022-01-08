using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using coreWeb.Models;
using coreWeb.Models.Entities;
using coreWeb.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coreWeb.Controllers
{
    public class UserProfileController : Controller
    {

        private ApplicationDbContext _context;
        private static List<DMTinh> _tinhs { get; set; }
        private static List<DMHuyen> _huyens { get; set; }
        private static List<DMXa> _xas { get; set; }


        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


        public ActionResult Employees()
        {

            int? userID = HttpContext.Session.GetInt32("UserId");
            
            if (userID !=0)
            {
                //var dmTinh = _context.DMTinh.ToList();
                //ViewBag.DMTinh = dmTinh;

                //var dmHuyen = _context.DMHuyen.ToList();
                //ViewBag.DMHuyen = dmHuyen;

                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var nguoilaodong = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.NguoiLaoDong = nguoilaodong;
                ViewBag.Role = user.RoleId;

                var timviec = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == nguoilaodong.Id).FirstOrDefault();
                ViewBag.TimViec = timviec;
                if (timviec != null)
                {
                    var dmNganhNghe = _context.DMNganhNghe.Where(p => p.Id == timviec.NganhNgheId).FirstOrDefault();
                    ViewBag.DMNganhNghe = dmNganhNghe;

                    if (timviec.VanBangId != null)
                    {
                        var vanbang = _context.DMVanBang.Where(p => p.Id == timviec.VanBangId).FirstOrDefault();
                        ViewBag.VanBang = vanbang;
                    }


                    if (timviec.TinHocId != null)
                    {
                        var tinhoc = _context.DMTinHoc.Where(p => p.Id == timviec.TinHocId).FirstOrDefault();
                        ViewBag.TinHoc = tinhoc;
                    }


                    if (timviec.NgoaiNguId != null)
                    {
                        var ngoaingu = _context.DMNgoaiNgu.Where(p => p.Id == timviec.NgoaiNguId).FirstOrDefault();
                        ViewBag.NgoaiNgu = ngoaingu;
                    }

                    
                }

                //var gioitinh = _context.DMGioiTinh.Where(p => p.Id == nguoilaodong.GioiTinhId).FirstOrDefault();
                //ViewBag.DMGioiTinh = gioitinh;
                
                var vitricongviec = _context.DMViTriCongViec.ToList();
                ViewBag.ViTriCongViec = vitricongviec;

                var dmlinhvuc = _context.DMLinhVuc.ToList();
                ViewBag.DMLinhVuc = dmlinhvuc;

                HttpContext.Session.SetInt32("NLDID", nguoilaodong.Id);
            }


            return View();
        }

        public ActionResult Employers()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID !=0)
            {
                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                

                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();

                ViewBag.User = user;
                ViewBag.Role = user.RoleId;

                var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.DoanhNghiep = doanhnghiep;

                var gioitinh = _context.DMGioiTinh.ToList();
                ViewBag.DMGioiTinh = gioitinh;

                var tinh = _context.DMTinh.Where(p => p.Id == doanhnghiep.DiaChiTinhId).FirstOrDefault();
                ViewBag.Tinh = tinh;

                var huyen = _context.DMHuyen.Where(p => p.Id == doanhnghiep.DiaChiHuyenId).FirstOrDefault();
                ViewBag.Huyen = huyen;

                var tuyendung = _context.HoSoTuyenDung.Where(p => p.DoanhNghiepId == doanhnghiep.Id).ToList();
                ViewBag.TuyenDung = tuyendung;

                var loaihinh = _context.DMLoaiHinh.Where(p => p.Id == doanhnghiep.LoaiHinhId).FirstOrDefault();
                ViewBag.LoaiHinh = loaihinh;

                var nganhkinhte = _context.DMNganhKinhTe.Where(p => p.Id == doanhnghiep.NganhKinhTeId).FirstOrDefault();
                ViewBag.NganhKT = nganhkinhte;

                var cumcongnghiep = _context.DMKhuCumCN.Where(p => p.Id == doanhnghiep.KhuCumCNId).FirstOrDefault();
                ViewBag.KCN = cumcongnghiep;

                var thoigianlamviec = _context.DMThoiGianLamViec.ToList();
                ViewBag.TGLamViec = thoigianlamviec;

                var vitricongviec = _context.DMViTriCongViec.ToList();
                ViewBag.ViTriCongViec = vitricongviec;

                var dmlinhvuc = _context.DMLinhVuc.ToList();
                ViewBag.DMLinhVuc = dmlinhvuc;

                HttpContext.Session.SetInt32("DNID", doanhnghiep.Id);
            }


            return View();
        }

        public ActionResult EmployeesCreate()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;
                ViewBag.Role = user.RoleId;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                var dmXa = _context.DMXa.ToList();
                ViewBag.DMXa = dmXa;

            }
            
            return View();
        }

        public ActionResult EmployersCreate()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            if (userID != 0)
            {
                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;
                ViewBag.Role = user.RoleId;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                var dmXa = _context.DMXa.ToList();
                ViewBag.DMXa = dmXa;

                var loaihinh = _context.DMLoaiHinh.ToList();
                ViewBag.LoaiHinh = loaihinh;

                var nganhkinhte = _context.DMNganhKinhTe.ToList();
                ViewBag.NganhKT = nganhkinhte;

                var cumcongnghiep = _context.DMKhuCumCN.ToList();
                ViewBag.KCN = cumcongnghiep;
            }
            

            return View();
        }

        public ActionResult EditDoanhNghiep()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;
                ViewBag.Role = user.RoleId;

                var doanhnghiep = _context.DoanhNghiep.Include(e => e.DiaChiHuyen).Include(e => e.DiaChiTinh).Include(e => e.LoaiHinh).Include(e => e.NganhKinhTe)
                    .Include(e => e.KhuCumCN).Include(e => e.LoaiHinh).Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.DoanhNghiepnow = doanhnghiep;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                var dmXa = _context.DMXa.ToList();
                ViewBag.DMXa = dmXa;

                var loaihinh = _context.DMLoaiHinh.ToList();
                ViewBag.LoaiHinh = loaihinh;

                var nganhkinhte = _context.DMNganhKinhTe.ToList();
                ViewBag.NganhKT = nganhkinhte;

                var cumcongnghiep = _context.DMKhuCumCN.ToList();
                ViewBag.KCN = cumcongnghiep;
            }
            

            return View();
        }

        public ActionResult HSTDCreateView()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.DoanhNghiep = doanhnghiep;

                var nganhnghe = _context.DMNganhNghe.ToList();
                ViewBag.NganhNghe = nganhnghe;

                var vitri = _context.DMViTriCongViec.ToList();
                ViewBag.ViTri = vitri;

                var gioitinh = _context.DMGioiTinh.ToList();
                ViewBag.GioiTinh = gioitinh;

                var mucluong = _context.DMMucLuong.ToList();
                ViewBag.MucLuong = mucluong;

                var thoigian = _context.DMThoiGianLamViec.ToList();
                ViewBag.ThoiGian = thoigian;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

            }

            return View();
        }

        public ActionResult HSTDEditView( int hstdid)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.DoanhNghiep = doanhnghiep;

                var hstd = _context.HoSoTuyenDung.Include(e => e.NganhNghe).Include(e => e.ViTriCongViec).Include(e => e.GioiTinh).Include(e => e.MucLuong)
                    .Include(e => e.ThoiGianLamViec).Include(e => e.NoiLamViecTinh).Include(e => e.NoiLamViecHuyen).Where(p => p.DoanhNghiepId == doanhnghiep.Id && p.Id == hstdid).FirstOrDefault();
                ViewBag.HSTD = hstd;

                var nganhnghe = _context.DMNganhNghe.ToList();
                ViewBag.NganhNghe = nganhnghe;


                var vitri = _context.DMViTriCongViec.ToList();
                ViewBag.ViTri = vitri;

                var gioitinh = _context.DMGioiTinh.ToList();
                ViewBag.GioiTinh = gioitinh;

                var mucluong = _context.DMMucLuong.ToList();
                ViewBag.MucLuong = mucluong;

                var thoigian = _context.DMThoiGianLamViec.ToList();
                ViewBag.ThoiGian = thoigian;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;
            }

            return View();
        }

        public ActionResult AddNewNLD(string HoTen, string Email, string Phone, DateTime NgaySinh, string DiaChi, int GioiTinh, string SoDinhDanh, string tinh, string huyen, string xa, DateTime ngaycap, string noicap)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;
            try
            {
                NguoiLaoDong ngld = new NguoiLaoDong();

                ngld.HoTen = HoTen;
                ngld.Email = Email;
                ngld.SoDienThoai = Phone;
                ngld.DiaChi = DiaChi;
                ngld.GioiTinhId = GioiTinh;
                ngld.NgaySinh = NgaySinh;
                ngld.SoDinhDanh = SoDinhDanh;
                ngld.DiaChiHuyenId = huyen;
                ngld.DiaChiTinhId = tinh;
                ngld.DiaChiXaId = xa;
                ngld.UserId = userID;
                
                _context.Add(ngld);
                _context.SaveChangesAsync();
                
            }
            catch (Exception)
            {

                throw;
            }
            return RedirectToAction("Employees");
        }

        public ActionResult EditProfileEmployees()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.NLD = ngld;

                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var timviec = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == ngld.Id).FirstOrDefault();
                ViewBag.TimViec = timviec;

                var tinh = _context.DMTinh.Where(p => p.Id == ngld.DiaChiTinhId).FirstOrDefault();
                ViewBag.Tinh = tinh;

                var huyen = _context.DMHuyen.Where(p => p.Id == ngld.DiaChiHuyenId).FirstOrDefault();
                ViewBag.Huyen = huyen;

                var xa = _context.DMXa.Where(p => p.Id == ngld.DiaChiXaId).FirstOrDefault();
                ViewBag.Xa = xa;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                var dmXa = _context.DMXa.ToList();
                ViewBag.DMXa = dmXa;

                //ViewBag.TinhList = new SelectList(GetTinhList(), "Id", "TenTinh");
                ViewBag.Role = user.RoleId;
            }
            

            return View(); 
        }

        public ActionResult EditProfileNLD(string HoTen, string Email, string Phone, DateTime NgaySinh, string DiaChi, int GioiTinh, string SoDinhDanh, string tinh, string huyen, string xa)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var ngld = _context.NguoiLaoDong.Where(p => p.UserId == user.Id).FirstOrDefault();

            if (ngld != null)
            {
                ngld.HoTen = HoTen;
                ngld.Email = Email;
                ngld.SoDienThoai = Phone;
                ngld.NgaySinh = NgaySinh;
                ngld.DiaChi = DiaChi;
                ngld.GioiTinhId = GioiTinh;
                ngld.SoDinhDanh = SoDinhDanh;
                ngld.DiaChiTinhId = tinh;
                ngld.DiaChiHuyenId = huyen;
                ngld.DiaChiXaId = xa;

            }
            _context.Update(ngld);
           

            user.FullName = HoTen;
            _context.Update(user);
            _context.SaveChangesAsync();

            return RedirectToAction("Employees");
        }

        public ActionResult EmployeesCV()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.NLD = ngld;

                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var tinh = _context.DMTinh.Where(p => p.Id == ngld.DiaChiTinhId).FirstOrDefault();
                ViewBag.Tinh = tinh;

                var huyen = _context.DMHuyen.Where(p => p.Id == ngld.DiaChiHuyenId).FirstOrDefault();
                ViewBag.Huyen = huyen;

                var xa = _context.DMXa.Where(p => p.Id == ngld.DiaChiXaId).FirstOrDefault();
                ViewBag.Xa = xa;

                //var dmTinh = _context.DMTinh.ToList();
                //ViewBag.DMTinh = dmTinh;

                //var dmHuyen = _context.DMHuyen.ToList();
                //ViewBag.DMHuyen = dmHuyen;

                //var dmXa = _context.DMXa.ToList();
                //ViewBag.DMXa = dmXa;

                var vitricongviec = _context.DMViTriCongViec.ToList();
                ViewBag.ViTriCongViec = vitricongviec;

                var dmNganhNghe = _context.DMNganhNghe.ToList();
                ViewBag.DMNganhNghe = dmNganhNghe;

                var mucluong = _context.DMMucLuong.ToList();
                ViewBag.MucLuong = mucluong;

                var vanhoa = _context.DMVanHoa.ToList();
                ViewBag.VanHoa = vanhoa;

                var ngoaingu = _context.DMNgoaiNgu.ToList();
                ViewBag.NgoaiNgu = ngoaingu;

                var tinhoc = _context.DMTinHoc.ToList();
                ViewBag.TinHoc = tinhoc;

                var vanbang = _context.DMVanBang.ToList();
                ViewBag.VanBang = vanbang;

                ViewBag.Role = user.RoleId;
            }
            

            return View();
        }

        public ActionResult EmployeesCVEdit()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            if (userID != 0)
            {
                var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
                ViewBag.NLD = ngld;

                var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
                ViewBag.User = user;

                var timviec = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == ngld.Id).FirstOrDefault();
                ViewBag.TimViec = timviec;

                var tinh = _context.DMTinh.Where(p => p.Id == ngld.DiaChiTinhId).FirstOrDefault();
                ViewBag.Tinh = tinh;

                var huyen = _context.DMHuyen.Where(p => p.Id == ngld.DiaChiHuyenId).FirstOrDefault();
                ViewBag.Huyen = huyen;

                var xa = _context.DMXa.Where(p => p.Id == ngld.DiaChiXaId).FirstOrDefault();
                ViewBag.Xa = xa;


                var vitricongviecht = _context.DMViTriCongViec.Where(p => p.Id == timviec.ViTriCongViecId).FirstOrDefault();
                ViewBag.ViTriNow = vitricongviecht;

                var dmNganhNghenow = _context.DMNganhNghe.Where(p => p.Id == timviec.NganhNgheId).FirstOrDefault();
                ViewBag.NganhNgheNow = dmNganhNghenow;

                var mucluongnow = _context.DMMucLuong.Where(p => p.Id == timviec.MucLuongId).FirstOrDefault();
                ViewBag.MucLuongNow = mucluongnow;

                var vanhoanow = _context.DMVanHoa.Where(p => p.Id == timviec.VanHoaId).FirstOrDefault();
                ViewBag.VanHoaNow = vanhoanow;

                var ngoaingunow = _context.DMNgoaiNgu.Where(p => p.Id == timviec.NgoaiNguId).FirstOrDefault();
                ViewBag.NgoaiNguNow = ngoaingunow;

                var tinhocnow = _context.DMTinHoc.Where(p => p.Id == timviec.TinHocId).FirstOrDefault();
                ViewBag.TinHocNow = tinhocnow;

                var vanbangnow = _context.DMVanBang.Where(p => p.Id == timviec.VanBangId).FirstOrDefault();
                ViewBag.VanBangNow = vanbangnow;

                var dmTinh = _context.DMTinh.ToList();
                ViewBag.DMTinh = dmTinh;

                var dmHuyen = _context.DMHuyen.ToList();
                ViewBag.DMHuyen = dmHuyen;

                var dmXa = _context.DMXa.ToList();
                ViewBag.DMXa = dmXa;

                var vitricongviec = _context.DMViTriCongViec.ToList();
                ViewBag.ViTriCongViec = vitricongviec;

                var dmNganhNghe = _context.DMNganhNghe.ToList();
                ViewBag.DMNganhNghe = dmNganhNghe;

                var mucluong = _context.DMMucLuong.ToList();
                ViewBag.MucLuong = mucluong;

                var vanhoa = _context.DMVanHoa.ToList();
                ViewBag.VanHoa = vanhoa;

                var ngoaingu = _context.DMNgoaiNgu.ToList();
                ViewBag.NgoaiNgu = ngoaingu;

                var tinhoc = _context.DMTinHoc.ToList();
                ViewBag.TinHoc = tinhoc;

                var vanbang = _context.DMVanBang.ToList();
                ViewBag.VanBang = vanbang;

                ViewBag.Role = user.RoleId;
            }
            

            return View();
        }


        public ActionResult NLDCVADD( string Hoten, string tcv, int vitri , int nganhnghe, string kinhnghiem, string kynang, string uudiem, string khuyetdiem, int mucluong, int vanhoa, int? tinhoc, int? ngoaingu, int? vanbang, string tinh, string huyen)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();

            var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();

            HoSoTimViec timviec = new HoSoTimViec()
            {
                NguoiLaoDongId = ngld.Id,
                ViTriCongViecId = vitri,
                TenCongViec = tcv,
                NganhNgheId = nganhnghe,
                MucLuongId = mucluong,
                NoiLamViecTinhId = tinh,
                NoiLamViecHuyenId = huyen,
                VanHoaId = vanhoa,
                NgoaiNguId = ngoaingu,
                TinHocId = tinhoc,
                VanBangId = vanbang,
                KinhNghiem = kinhnghiem,
                KyNang = kynang,
                UuDiem = uudiem,
                KhuyetDiem = khuyetdiem,
                NgayTao = DateTime.Now
            };

            _context.Add(timviec);
            _context.SaveChanges();
            
            return RedirectToAction("Employees");
        }

        public ActionResult NLDCVEDIT( string Hoten, string tcv, int vitri, int nganhnghe, string kinhnghiem, string kynang, string uudiem, string khuyetdiem, int mucluong, int? vanhoa, int? tinhoc, int? ngoaingu, int? vanbang, string tinh, string huyen)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();

            var ngld = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();

            var timviec = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == ngld.Id).FirstOrDefault();

           
                timviec.NguoiLaoDongId = ngld.Id;
                timviec.ViTriCongViecId = vitri;
                timviec.TenCongViec = tcv;
                timviec.NganhNgheId = nganhnghe;
                timviec.MucLuongId = mucluong;
                timviec.NoiLamViecTinhId = tinh;
                timviec.NoiLamViecHuyenId = huyen;
                timviec.VanHoaId = vanhoa;
                timviec.NgoaiNguId = ngoaingu;
                timviec.TinHocId = tinhoc;
                timviec.VanBangId = vanbang;
                timviec.KinhNghiem = kinhnghiem;
                timviec.KyNang = kynang;
                timviec.UuDiem = uudiem;
                timviec.KhuyetDiem = khuyetdiem;
                timviec.NgayCapNhat = DateTime.Now;

                
                _context.Update(timviec);
                _context.SaveChanges();
            


            return RedirectToAction("Employees");
        }

        public ActionResult AddNewDN(string dnname, string msthue, int loaihinh, string nkte, string kdsx, int? tsld, int namhd, string phone, string email, string website, string DiaChi, int kcn, string huyen, string tinh)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == user.Id).FirstOrDefault();
            if (doanhnghiep == null)
            {
                try
                {

                    DoanhNghiep dnn = new DoanhNghiep();
                    dnn.UserId = userID;
                    dnn.TenDoanhNghiep = dnname;
                    dnn.MaSoThue = msthue;
                    dnn.LoaiHinhId = loaihinh;
                    dnn.NganhKinhTeId = nkte;
                    dnn.KinhDoanhSanXuat = kdsx;
                    dnn.TongSoLaoDong = tsld;
                    dnn.NamHoatDong = namhd;
                    dnn.SoDienThoai = phone;
                    dnn.Email = email;
                    dnn.TongSoLaoDong = tsld;
                    dnn.Website = website;
                    dnn.DiaChi = DiaChi;
                    dnn.KhuCumCNId = kcn;
                    dnn.DiaChiTinhId = tinh;
                    dnn.DiaChiHuyenId = huyen;

                    _context.Add(dnn);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {

                }

                
            }

            return RedirectToAction("Employers");

        }

        public ActionResult EditDN( string dnname, string msthue, int loaihinh, string nkte, string kdsx, int? tsld, int namhd, string phone, string email, string website, string DiaChi, int kcn, string huyen, string tinh)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;
            
            var doanhnghhiep = _context.DoanhNghiep.Where(p => p.UserId == user.Id).FirstOrDefault();
            doanhnghhiep.UserId = userID;
            doanhnghhiep.TenDoanhNghiep = dnname;
            doanhnghhiep.MaSoThue = msthue;
            doanhnghhiep.LoaiHinhId = loaihinh;
            doanhnghhiep.NganhKinhTeId = nkte;
            doanhnghhiep.KinhDoanhSanXuat = kdsx;
            doanhnghhiep.NamHoatDong = namhd;
            doanhnghhiep.SoDienThoai = phone;
            doanhnghhiep.Email = email;
            doanhnghhiep.TongSoLaoDong = tsld;
            doanhnghhiep.Website = website;
            doanhnghhiep.KhuCumCNId = kcn;
            doanhnghhiep.DiaChi = DiaChi;
            doanhnghhiep.DiaChiTinhId = tinh;
            doanhnghhiep.DiaChiHuyenId = huyen;


            _context.Update(doanhnghhiep);
            _context.SaveChanges();

            return RedirectToAction("Employers");
        }

        public ActionResult CreateHSTD(string tcviec, int nganhnghe, int vtcv, int soluong, int gioitinh, int tuoitu, int tuoiden, int mucluong, string nlviec, string tinh, string huyen, int? thoigian, DateTime hannop, string mota, string kinhnghiem, string yeucau, string phucloi /*[FromForm] HoSoTuyenDung hoSo*/)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
            ViewBag.DoanhNghiep = doanhnghiep;
            //HoSoTuyenDung hstd4 = new HoSoTuyenDung()
            //{
            //    DoanhNghiepId = doanhnghiep.Id,
            //    TenCongViec = hoSo.TenCongViec,
            //    NganhNgheId = hoSo.NganhNgheId,
            //    ViTriCongViecId = hoSo.ViTriCongViecId,
            //    SoLuong = hoSo.SoLuong,
            //    GioiTinhId = hoSo.GioiTinhId,
            //    TuoiTu = hoSo.TuoiTu,
            //    TuoiDen = hoSo.TuoiDen,
            //    MucLuongId = hoSo.MucLuongId,
            //    NoiLamViec = hoSo.NoiLamViec,
            //    NoiLamViecTinhId = hoSo.NoiLamViecTinhId,
            //    NoiLamViecHuyenId = hoSo.NoiLamViecHuyenId,
            //    ThoiGianLamViecId = hoSo.ThoiGianLamViecId,
            //    HanNop = hoSo.HanNop,
            //    MoTaCongViec = hoSo.MoTaCongViec,
            //    KinhNghiem = hoSo.KinhNghiem,
            //    YeuCauCongViec = hoSo.YeuCauCongViec,
            //    PhucLoi = hoSo.PhucLoi,
            //    NgayTao = DateTime.Now

            //};

            HoSoTuyenDung hstd4 = new HoSoTuyenDung()
            {
                DoanhNghiepId = doanhnghiep.Id,
                TenCongViec = tcviec,
                NganhNgheId = nganhnghe,
                ViTriCongViecId = vtcv,
                SoLuong = soluong,
                GioiTinhId = gioitinh,
                TuoiTu = tuoitu,
                TuoiDen = tuoiden,
                MucLuongId = mucluong,
                NoiLamViec = nlviec,
                NoiLamViecTinhId = tinh,
                NoiLamViecHuyenId = huyen,
                ThoiGianLamViecId = thoigian,
                HanNop = hannop,
                MoTaCongViec = mota,
                KinhNghiem = kinhnghiem,
                YeuCauCongViec = yeucau,
                PhucLoi = phucloi,
                NgayTao = DateTime.Now

            };
            _context.Add(hstd4);
                _context.SaveChanges();
           
            
           
            return RedirectToAction("Employers");
        }

        public ActionResult EditHSTD(int hstdid, string tcviec, int nganhnghe, int vtcv, int soluong, int gioitinh, int tuoitu, int tuoiden, int mucluong, string nlviec, string tinh, string huyen, int thoigian, DateTime hannop, string mota, string kinhnghiem, string yeucau, string phucloi)
        {

            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
            ViewBag.DoanhNghiep = doanhnghiep;

           
                var hstd4 = _context.HoSoTuyenDung.Where(p => p.Id == hstdid && p.DoanhNghiepId == doanhnghiep.Id).FirstOrDefault();


                hstd4.TenCongViec = tcviec;
                hstd4.NganhNgheId = nganhnghe;
                hstd4.ViTriCongViecId = vtcv;
                hstd4.SoLuong = soluong;
                hstd4.GioiTinhId = gioitinh;
                hstd4.TuoiTu = tuoitu;
                hstd4.TuoiDen = tuoiden;
                hstd4.MucLuongId = mucluong;
                hstd4.NoiLamViec = nlviec;
                hstd4.NoiLamViecTinhId = tinh;
                hstd4.NoiLamViecHuyenId = huyen;
                hstd4.ThoiGianLamViecId = thoigian;
                hstd4.HanNop = hannop;
                hstd4.KinhNghiem = kinhnghiem;
                hstd4.YeuCauCongViec = yeucau;
                hstd4.NgayCapNhat = DateTime.Now;
                hstd4.MoTaCongViec = mota;
                hstd4.PhucLoi = phucloi;

                _context.Update(hstd4);
                _context.SaveChanges();
           
            return RedirectToAction("Employers");
        }

        public JsonResult GetAllProvinceByCountryId()
        {

            var data = _context.DMTinh.Where(p => p.Id != "").OrderBy(x => x.Id).ToList();
            
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public JsonResult GetAllDistrictByProvinceId(string id)
        {
            var data = _context.DMHuyen.Where(p => p.TinhId == id).ToList();
            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }
        public JsonResult GetAllWardByDistrictId(string id)
        {
            var data = _context.DMXa.Where(p => p.HuyenId == id).ToList();

            return Json(data, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public ActionResult ChangePasswordEmployees()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var nguoilaodong = _context.NguoiLaoDong.Where(p => p.UserId == userID).FirstOrDefault();
            ViewBag.NguoiLaoDong = nguoilaodong;

            var timviec = _context.HoSoTimViec.Where(p => p.NguoiLaoDongId == nguoilaodong.Id).FirstOrDefault();
            ViewBag.TimViec = timviec;
            return View();
        }

        public ActionResult ChangePasswordEmployer()
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            ViewBag.User = user;

            var doanhnghiep = _context.DoanhNghiep.Where(p => p.UserId == userID).FirstOrDefault();
            ViewBag.DoanhNghiep = doanhnghiep;
            return View();
        }

        public JsonResult checkPassword(string passnew)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");
            var user = _context.User.Where(p => p.Id == userID).FirstOrDefault();
            string pass = "";
            bool check;
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                string input = passnew;
                var hash = sha1.ComputeHash(Encoding.Unicode.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2")); // can be "x2" if you want lowercase
                }
                pass = sb.ToString();
            }
           
            if (user.Password == pass)
            {
                check = true;
                return Json(check, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                check = false;
                return Json(check, new Newtonsoft.Json.JsonSerializerSettings());
            }
            
        }

        public ActionResult ChangePass(string passold, string passnew)
        {
            int? userID = HttpContext.Session.GetInt32("UserId");

            

            var objUser = new UserService(_context);
            var result = objUser.ChangePass((int)userID, passold, passnew);
            if (result == 1)
            {
                return RedirectToAction("Employees");
            }
            else
            {
                return RedirectToAction("ChangePasswordEmployees");
            }

        }
    }
}
