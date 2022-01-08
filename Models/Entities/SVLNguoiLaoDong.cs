using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class SVLNguoiLaoDong
    {
        [Key]
        public int Id { get; set; }
        public int? SanViecLamId { get; set; }
        public int? NguoiLaoDongId { get; set; }
        public int? DoanhNghiepId { get; set; }
        public int? HoSoTuyenDungId { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public bool? XacNhanDangKy { get; set; }
        public bool? XacNhanTrungTuyen { get; set; }

        [ForeignKey("SanViecLamId")]
        public SanViecLam SanViecLam { get; set; }
        [ForeignKey("NguoiLaoDongId")]
        public NguoiLaoDong NguoiLaoDong { get; set; }
        [ForeignKey("DoanhNghiepId")]
        public DoanhNghiep DoanhNghiep { get; set; }
        [ForeignKey("HoSoTuyenDungId")]
        public HoSoTuyenDung HoSoTuyenDung { get; set; }
    }
}
