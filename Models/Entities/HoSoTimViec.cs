using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class HoSoTimViec
    {
        [Key]
        public int Id { get; set; }
        public Guid? UID { get; set; }
        public int? NguoiLaoDongId { get; set; }
        public int? ViTriCongViecId { get; set; }
        [MaxLength(500)]
        public string TenCongViec { get; set; }
        public int? NganhNgheId { get; set; }
        public int? LoaiHinhId { get; set; }
        public int? MucLuongId { get; set; }
        public int? MucLuongTu { get; set; }
        public int? MucLuongDen { get; set; }
        [MaxLength(200)]
        public string NoiLamViec { get; set; }
        [MaxLength(5)]
        public string NoiLamViecTinhId { get; set; }
        [MaxLength(5)]
        public string NoiLamViecHuyenId { get; set; }
        public int? ThoiGianLamViecId { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public int? VanHoaId { get; set; }
        public int? VanBangId { get; set; }
        [MaxLength(500)]
        public string ChuyenNganh { get; set; }
        [MaxLength(500)]
        public string KinhNghiem { get; set; }
        public int? NgoaiNguId { get; set; }
        public int? TinHocId { get; set; }
        [MaxLength]
        public string KyNang { get; set; }
        [MaxLength]
        public string UuDiem { get; set; }
        [MaxLength]
        public string KhuyetDiem { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public bool? XacThuc { get; set; }

        [ForeignKey("NguoiLaoDongId")]
        public NguoiLaoDong NguoiLaoDong { get; set; }
        [ForeignKey("ViTriCongViecId")]
        public DMViTriCongViec ViTriCongViec { get; set; }
        [ForeignKey("NganhNgheId")]
        public DMNganhNghe NganhNghe { get; set; }
        [ForeignKey("LoaiHinhId")]
        public DMLoaiHinh LoaiHinh { get; set; }
        [ForeignKey("MucLuongId")]
        public DMMucLuong MucLuong { get; set; }
        [ForeignKey("NoiLamViecTinhId")]
        public DMTinh NoiLamViecTinh { get; set; }
        [ForeignKey("NoiLamViecHuyenId")]
        public DMHuyen NoiLamViecHuyen { get; set; }
        [ForeignKey("ThoiGianLamViecId")]
        public DMThoiGianLamViec ThoiGianLamViec { get; set; }
        [ForeignKey("VanBangId")]
        public DMVanBang VanBang { get; set; }
        [ForeignKey("VanHoaId")]
        public DMVanHoa VanHoa { get; set; }
        [ForeignKey("NgoaiNguId")]
        public DMNgoaiNgu NgoaiNgu { get; set; }
        [ForeignKey("TinHocId")]
        public DMTinHoc TinHoc { get; set; }
    }
}
