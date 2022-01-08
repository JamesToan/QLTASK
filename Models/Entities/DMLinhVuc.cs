using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMLinhVuc
    {
        public DMLinhVuc()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string TenLinhVuc { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

    }
}
