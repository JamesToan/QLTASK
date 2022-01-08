using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NewsChuyenMuc
    {
        public NewsChuyenMuc()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string TenChuyenMuc { get; set; }
        [MaxLength(50)]
        public string LoaiChuyenMuc { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}