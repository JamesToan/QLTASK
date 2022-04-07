using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class NhanSu
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string TenNhanSu { get; set; }

        public int NguoiTaoId { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public int? UnitId { get; set; }

        public int? DichVuId { get; set; }

        public int? AdminDichVuId { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("NguoiTaoId")]
        public User Users { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

        [ForeignKey("DichVuId")]
        public DichVu DichVu { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("AdminDichVuId")]
        public DichVu AdminDichVu { get; set; }
    }
}
