using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMVanHoa
    {
        public DMVanHoa()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenVanHoa { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}
