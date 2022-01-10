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

        [ForeignKey("NguoiTaoId")]
        public User User { get; set; }
    }
}