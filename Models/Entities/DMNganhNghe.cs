using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMNganhNghe
    {
        public DMNganhNghe()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string MaNganhNghe { get; set; }
        [MaxLength(500)]
        public string TenNganhNghe { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
