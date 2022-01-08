using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMLoaiVanBan
    {
        public DMLoaiVanBan()
        {
        }

        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(500)]
        public string TenLoaiVanBan { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
