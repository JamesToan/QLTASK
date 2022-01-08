using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class DangKyHocNghe
    {
        [Key]
        public int Id { get; set; }
        public int? DaoTaoNgheId { get; set; }
        public int? NguoiLaoDongId { get; set; }
        [MaxLength(50)]
        public string HoTen { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string SoDienThoai { get; set; }
        public DateTime? NgayDangKy { get; set; }
        public int? TinhTrangId { get; set; }

        [ForeignKey("DaoTaoNgheId")]
        public DaoTaoNghe DaoTaoNghe { get; set; }
        [ForeignKey("NguoiLaoDongId")]
        public NguoiLaoDong NguoiLaoDong { get; set; }
        [ForeignKey("TinhTrangId")]
        public DMTinhTrangDangKy TinhTrang { get; set; }
    }
}