using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMMucLuong
    {
        public DMMucLuong()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenMucLuong { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

    }
}
