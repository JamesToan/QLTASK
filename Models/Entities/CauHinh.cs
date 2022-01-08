using System;
using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class CauHinh
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string MaCauHinh { get; set; }
        
        [MaxLength(500)]
        public string TenCauHinh { get; set; }
        [MaxLength(50)]
        public string GiaTri { get; set; }
        
        [MaxLength(500)]
        public string GhiChu { get; set; }
        public DateTime? NgayCapNhat { get; set; }
    }
}
