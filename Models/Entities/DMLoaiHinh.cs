using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMLoaiHinh
    {
        public DMLoaiHinh()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenLoaiHinh { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

    }
}
