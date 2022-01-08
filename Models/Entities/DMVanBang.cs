using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMVanBang
    {
        public DMVanBang()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenVanBang { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
