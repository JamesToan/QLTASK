using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class DoanhNghiep
    {
        public DoanhNghiep()
        {
            this.HoSoTuyenDungs = new List<HoSoTuyenDung>();
        }

        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [MaxLength(200)]
        public string TenDoanhNghiep { get; set; }
        [MaxLength(50)]
        public string MaSoThue { get; set; }
        public int? LoaiHinhId { get; set; }
        public string NganhKinhTeId { get; set; }
        [MaxLength(500)]
        public string KinhDoanhSanXuat { get; set; }
        public int? TongSoLaoDong { get; set; }
        public int? NamHoatDong { get; set; }
        [MaxLength(50)]
        public string MaKhachHang { get; set; }
        [MaxLength(200)]
        public string SoDienThoai { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string Website { get; set; }
        [MaxLength(200)]
        public string Logo { get; set; }
        public int? KhuCumCNId { get; set; }
        [MaxLength(500)]
        public string DiaChi { get; set; }
        [MaxLength(5)]
        public string DiaChiTinhId { get; set; }
        [MaxLength(5)]
        public string DiaChiHuyenId { get; set; }

        public bool? XacThuc { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("LoaiHinhId")]
        public DMLoaiHinh LoaiHinh { get; set; }
        [ForeignKey("NganhKinhTeId")]
        public DMNganhKinhTe NganhKinhTe { get; set; }
        [ForeignKey("KhuCumCNId")]
        public DMKhuCumCN KhuCumCN { get; set; }
        [ForeignKey("DiaChiTinhId")]
        public DMTinh DiaChiTinh { get; set; }
        [ForeignKey("DiaChiHuyenId")]
        public DMHuyen DiaChiHuyen { get; set; }
        public List<HoSoTuyenDung> HoSoTuyenDungs { get; set; }
    }
}
