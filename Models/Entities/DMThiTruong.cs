using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMThiTruong
    {
        public DMThiTruong()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenThiTruong { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
