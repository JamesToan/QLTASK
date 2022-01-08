using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class NewsMenu
    {
        public NewsMenu()
        {
        }

        [Key]
        public int Id { get; set; }
        public int ChuyenMucId { get; set; }
        public int? MenuChaId { get; set; }
        [MaxLength(50)]
        public string TieuDe { get; set; }
        [MaxLength(50)]
        public string Icon { get; set; }
        [MaxLength(500)]
        public string Link { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

        [ForeignKey("ChuyenMucId")]
        public NewsChuyenMuc ChuyenMuc { get; set; }
        [ForeignKey("MenuChaId")]
        public NewsMenu MenuCha { get; set; }
    }
}
