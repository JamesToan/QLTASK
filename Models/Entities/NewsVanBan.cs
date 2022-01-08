using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NewsVanBan
    {
        [Key]
        public int Id { get; set; }
        public Guid? UID { get; set; }
        public int? ChuyenMucId { get; set; }
        public int? LinhVucId { get; set; }
        [MaxLength(50)]
        public string LoaiVanBanId { get; set; }
        [MaxLength(50)]
        public string SoKyHieu { get; set; }
        [MaxLength(500)]
        public string TrichYeu { get; set; }
        public DateTime? NgayThang { get; set; }
        [MaxLength(500)]
        public string CoQuanBanHanh { get; set; }
        [MaxLength(500)]
        public string VanBan { get; set; }
        public int? NguoiTaoId { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? NguoiDuyetId { get; set; }
        public DateTime? NgayDuyet { get; set; }

        [ForeignKey("ChuyenMucId")]
        public NewsChuyenMuc ChuyenMuc { get; set; }
        [ForeignKey("LinhVucId")]
        public DMLinhVuc LinhVuc { get; set; }
        [ForeignKey("LoaiVanBanId")]
        public DMLoaiVanBan LoaiVanBan { get; set; }
        [ForeignKey("NguoiTaoId")]
        public User NguoiTao { get; set; }
        [ForeignKey("NguoiDuyetId")]
        public User NguoiDuyet { get; set; }
    }
}