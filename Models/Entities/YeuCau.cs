using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class YeuCau
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string TenYeuCau { get; set; }

        [MaxLength]
        public string NoiDung { get; set; }

        public DateTime? ThoiHan { get; set; }

        [MaxLength]
        public string JiraDaGui { get; set; }

        public int? StatusId { get; set; }
        public int? StateId { get; set; }
        public int? NhanSuId { get; set; }

        [MaxLength(500)]
        public string DonVi { get; set; }

        public int? DichVuId { get; set; }

        public int NguoiTaoId { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }

        [ForeignKey("StateId")]
        public States States { get; set; }

        [ForeignKey("NhanSuId")]
        public NhanSu NhanSu { get; set; }

        [ForeignKey("DichVuId")]
        public DichVu DichVu { get; set; }

        [ForeignKey("NguoiTaoId")]
        public User User { get; set; }
    }
}
