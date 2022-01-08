using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMNganhKinhTe
    {
        public DMNganhKinhTe()
        {
        }

        [Key]
        [MaxLength(50)]
        public string Id { get; set; }
        [MaxLength(200)]
        public string TenNganhKinhTe { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
