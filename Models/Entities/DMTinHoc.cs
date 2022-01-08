using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMTinHoc
    {
        public DMTinHoc()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenChungChi { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
