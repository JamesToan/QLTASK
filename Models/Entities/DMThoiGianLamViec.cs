using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMThoiGianLamViec
    {
        public DMThoiGianLamViec()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenThoiGianLamViec { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
