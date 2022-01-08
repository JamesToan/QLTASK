using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMTinhTrangDangKy
    {
        public DMTinhTrangDangKy()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string MaTinhTrang { get; set; }
        [MaxLength(200)]
        public string TenTinhTrang { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
