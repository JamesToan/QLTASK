using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class HoSoTuyenDung
    {
        public HoSoTuyenDung()
        {
        }

        [Key]
        public int Id { get; set; }
        public Guid? UID { get; set; }
        public int? DoanhNghiepId { get; set; }
        public int? NganhNgheId { get; set; }
        public int? ViTriCongViecId { get; set; }
        [MaxLength(500)]
        public string TenCongViec { get; set; }
        public int? SoLuong { get; set; }
        public int? GioiTinhId { get; set; }
        public int? TuoiTu { get; set; }
        public int? TuoiDen { get; set; }
        public int? MucLuongId { get; set; }
        public int? MucLuongTu { get; set; }
        public int? MucLuongDen { get; set; }
        [MaxLength(500)]
        public string NoiLamViec { get; set; }
        [MaxLength(5)]
        public string NoiLamViecTinhId { get; set; }
        [MaxLength(5)]
        public string NoiLamViecHuyenId { get; set; }
        public int? ThoiGianLamViecId { get; set; }
        public DateTime? HanNop { get; set; }
        [MaxLength]
        public string MoTaCongViec { get; set; }
        [MaxLength]
        public string KinhNghiem { get; set; }
        [MaxLength]
        public string YeuCauCongViec { get; set; }
        [MaxLength]
        public string PhucLoi { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public bool? XacThuc { get; set; }

        [ForeignKey("DoanhNghiepId")]
        public DoanhNghiep DoanhNghiep { get; set; }
        [ForeignKey("NganhNgheId")]
        public DMNganhNghe NganhNghe { get; set; }
        [ForeignKey("ViTriCongViecId")]
        public DMViTriCongViec ViTriCongViec { get; set; }
        [ForeignKey("GioiTinhId")]
        public DMGioiTinh GioiTinh { get; set; }
        [ForeignKey("MucLuongId")]
        public DMMucLuong MucLuong { get; set; }
        [ForeignKey("NoiLamViecTinhId")]
        public DMTinh NoiLamViecTinh { get; set; }
        [ForeignKey("NoiLamViecHuyenId")]
        public DMHuyen NoiLamViecHuyen { get; set; }
        [ForeignKey("ThoiGianLamViecId")]
        public DMThoiGianLamViec ThoiGianLamViec { get; set; }
    }
}