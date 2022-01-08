using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMLoaiSan
    {
        public DMLoaiSan()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenLoaiSan{ get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

    }
}
