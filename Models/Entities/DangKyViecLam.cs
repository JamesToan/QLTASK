using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class DangKyViecLam
    {
        [Key]
        public int Id { get; set; }
        public int? ThiTruongId { get; set; }
        public int? NguoiLaoDongId { get; set; }
        [MaxLength(50)]
        public string HoTen { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string SoDienThoai { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public int? TinhTrangId { get; set; }

        [ForeignKey("ThiTruongId")]
        public DMThiTruong ThiTruong { get; set; }
        [ForeignKey("NguoiLaoDongId")]
        public NguoiLaoDong NguoiLaoDong { get; set; }
        [ForeignKey("TinhTrangId")]
        public DMTinhTrangDangKy TinhTrang { get; set; }
    }
}