using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMViTriCongViec
    {
        public DMViTriCongViec()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string TenViTriCongViec { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
