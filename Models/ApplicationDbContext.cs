using Microsoft.EntityFrameworkCore;

using AutoAPI;

using coreWeb.Models.Entities;


namespace coreWeb.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //identity
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionRole> PermissionRole { get; set; }

        //DanhMuc
        [AutoAPIEntity(Route = "CauHinh", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<CauHinh> CauHinh { get; set; }
        //DMTinh
        [AutoAPIEntity(Route = "DMTinh", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMTinh> DMTinh { get; set; }

        //DMHuyen
        [AutoAPIEntity(Route = "DMHuyen", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMHuyen> DMHuyen { get; set; }

        //DMXa
        [AutoAPIEntity(Route = "DMXa", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMXa> DMXa { get; set; }

        //DMKhuCumCN
        [AutoAPIEntity(Route = "DMKhuCumCN", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMKhuCumCN> DMKhuCumCN { get; set; }

        //DMGioiTinh
        [AutoAPIEntity(Route = "DMGioiTinh", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMGioiTinh> DMGioiTinh { get; set; }

        //DMLinhVuc
        [AutoAPIEntity(Route = "DMLinhVuc", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMLinhVuc> DMLinhVuc { get; set; }

        //DMLoaiHinh
        [AutoAPIEntity(Route = "DMLoaiHinh", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMLoaiHinh> DMLoaiHinh { get; set; }

        //DMLoaiHinhDaoTao
        [AutoAPIEntity(Route = "DMLoaiHinhDaoTao", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMLoaiHinhDaoTao> DMLoaiHinhDaoTao { get; set; }

        //DMLoaiVanBan
        [AutoAPIEntity(Route = "DMLoaiVanBan", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMLoaiVanBan> DMLoaiVanBan { get; set; }

        //DMNganhNghe
        [AutoAPIEntity(Route = "DMNganhNghe", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMNganhNghe> DMNganhNghe { get; set; }

        //DMNganhKinhTe
        [AutoAPIEntity(Route = "DMNganhKinhTe", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMNganhKinhTe> DMNganhKinhTe { get; set; }

        //DMNgoaiNgu
        [AutoAPIEntity(Route = "DMNgoaiNgu", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMNgoaiNgu> DMNgoaiNgu { get; set; }

        //DMMucLuong
        [AutoAPIEntity(Route = "DMMucLuong", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMMucLuong> DMMucLuong { get; set; }

        //DMThoiGianLamViec
        [AutoAPIEntity(Route = "DMThoiGianLamViec", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMThoiGianLamViec> DMThoiGianLamViec { get; set; }

        //DMTinHoc
        [AutoAPIEntity(Route = "DMTinHoc", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMTinHoc> DMTinHoc { get; set; }

        //DMTinhTrang
        [AutoAPIEntity(Route = "DMTinhTrang", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMTinhTrang> DMTinhTrang { get; set; }

        //DMVanHoa
        [AutoAPIEntity(Route = "DMVanHoa", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMVanHoa> DMVanHoa { get; set; }

        //DMVanBang
        [AutoAPIEntity(Route = "DMVanBang", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMVanBang> DMVanBang { get; set; }

        //DMViTriCongViec
        [AutoAPIEntity(Route = "DMViTriCongViec", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMViTriCongViec> DMViTriCongViec { get; set; }

        //DMThiTruong
        [AutoAPIEntity(Route = "DMThiTruong", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMThiTruong> DMThiTruong { get; set; }

        //DMTinhTrangDangKy
        [AutoAPIEntity(Route = "DMTinhTrangDangKy", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMTinhTrangDangKy> DMTinhTrangDangKy { get; set; }
        //DMLoaiSan
        [AutoAPIEntity(Route = "DMLoaiSan", POSTPolicy = "Admin", PUTPolicy = "Admin", DELETEPolicy = "Admin", ExposePagedResult = true)]
        public DbSet<DMLoaiSan> DMLoaiSan { get; set; }
        //News
        //NewsMenu
        public DbSet<NewsMenu> NewsMenu { get; set; }
        //NewsBanner
        public DbSet<NewsBanner> NewsBanner { get; set; }

        //NewsBanTin
        public DbSet<NewsBanTin> NewsBanTin { get; set; }

        //NewsHoiDap
        public DbSet<NewsHoiDap> NewsHoiDap { get; set; }

        //NewsChuyenMuc
        [AutoAPIEntity(Route = "NewsChuyenMuc", ExposePagedResult = true)]
        public DbSet<NewsChuyenMuc> NewsChuyenMuc { get; set; }

        //NewsVanBan
        public DbSet<NewsVanBan> NewsVanBan { get; set; }

        //NewsVideo
        public DbSet<NewsVideo> NewsVideo { get; set; }

        //DaoTaoNghe
        public DbSet<DaoTaoNghe> DaoTaoNghe { get; set; }

        //DangKyHocNghe
        public DbSet<DangKyHocNghe> DangKyHocNghe { get; set; }

        //DangKyViecLam
        public DbSet<DangKyViecLam> DangKyViecLam { get; set; }

        //DoanhNghiep
        public DbSet<DoanhNghiep> DoanhNghiep { get; set; }

        //HoSoTimViec
        public DbSet<HoSoTimViec> HoSoTimViec { get; set; }

        //HoSoTuyenDung
        public DbSet<HoSoTuyenDung> HoSoTuyenDung { get; set; }

        //NguoiLaoDong
        public DbSet<NguoiLaoDong> NguoiLaoDong { get; set; }

        //SanViecLam
        public DbSet<SanViecLam> SanViecLam { get; set; }

        //SVLDoanhNghiep
        public DbSet<SVLDoanhNghiep> SVLDoanhNghiep { get; set; }

        //SVLNguoiLaoDong
        public DbSet<SVLNguoiLaoDong> SVLNguoiLaoDong { get; set; }

    }
}
