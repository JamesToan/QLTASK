using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NewsHoiDap
    {
        [Key]
        public int Id { get; set; }
        public Guid? UID { get; set; }
        public int? LinhVucId { get; set; }
        [MaxLength(500)]
        public string TieuDe { get; set; }
        [MaxLength]
        public string NoiDung { get; set; }
        public int? NguoiHoiId { get; set; }
        [MaxLength(50)]
        public string HoTen { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string SoDienThoai { get; set; }
        public DateTime? NgayHoi { get; set; }
        [MaxLength]
        public string TraLoi { get; set; }
        [MaxLength(500)]
        public string VanBan { get; set; }
        public int? NguoiTraLoiId { get; set; }
        public DateTime? NgayTraLoi { get; set; }
        public int? NguoiDuyetId { get; set; }
        public DateTime? NgayDuyet { get; set; }

        [ForeignKey("LinhVucId")]
        public DMLinhVuc LinhVuc { get; set; }
        [ForeignKey("NguoiHoiId")]
        public User NguoiHoi { get; set; }
        [ForeignKey("NguoiTraLoiId")]
        public User NguoiTraLoi { get; set; }
        [ForeignKey("NguoiDuyetId")]
        public User NguoiDuyet { get; set; }
    }
}
