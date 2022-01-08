using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NguoiLaoDong
    {
        public NguoiLaoDong()
        {
            this.HoSoTimViecs = new List<HoSoTimViec>();
        }

        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        [MaxLength(50)]
        public string HoTen { get; set; }
        public int? GioiTinhId { get; set; }
        public DateTime? NgaySinh { get; set; }
        [MaxLength(20)]
        public string SoDinhDanh { get; set; }
        public DateTime? NgayCap { get; set; }
        [MaxLength(200)]
        public string NoiCap { get; set; }
        [MaxLength(50)]
        public string MaKhachHang { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string SoDienThoai { get; set; }
        [MaxLength(500)]
        public string DiaChi { get; set; }
        [MaxLength(5)]
        public string DiaChiTinhId { get; set; }
        [MaxLength(5)]
        public string DiaChiHuyenId { get; set; }
        [MaxLength(5)]
        public string DiaChiXaId { get; set; }
        public bool? XacThuc { get; set; }
        public int? TinhTrangId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GioiTinhId")]
        public DMGioiTinh GioiTinh { get; set; }
        [ForeignKey("DiaChiTinhId")]
        public DMTinh DiaChiTinh { get; set; }
        [ForeignKey("DiaChiHuyenId")]
        public DMHuyen DiaChiHuyen { get; set; }
        [ForeignKey("TinhTrangId")]
        public DMTinhTrang TinhTrang { get; set; }
        public List<HoSoTimViec> HoSoTimViecs { get; set; }
    }
}
