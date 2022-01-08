using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NewsBanner
    {
        [Key]
        public int Id { get; set; }
        public Guid? UID { get; set; }
        public int? ChuyenMucId { get; set; }
        [MaxLength(50)]
        public string TieuDe { get; set; }
        [MaxLength(500)]
        public string HinhAnh { get; set; }
        [MaxLength(500)]
        public string Link { get; set; }
        [MaxLength(50)]
        public string Target { get; set; }
        public int? NguoiTaoId { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? NguoiDuyetId { get; set; }
        public DateTime? NgayDuyet { get; set; }

        [ForeignKey("ChuyenMucId")]
        public NewsChuyenMuc ChuyenMuc { get; set; }
        [ForeignKey("NguoiTaoId")]
        public User NguoiTao { get; set; }
        [ForeignKey("NguoiDuyetId")]
        public User NguoiDuyet { get; set; }
    }
}
