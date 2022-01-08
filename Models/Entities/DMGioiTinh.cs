using System.ComponentModel.DataAnnotations;


namespace coreWeb.Models.Entities
{
    public class DMGioiTinh
    {
        public DMGioiTinh()
        {
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string TenGioiTinh { get; set; }
        [MaxLength(200)]
        public string DanhXung { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
    }
}