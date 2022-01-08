using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMKhuCumCN
    {
        public DMKhuCumCN()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string TenKhuCumCN { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }

    }
}
