using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class LoaiYeuCauNew
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string TenLYC { get; set; }
        
        public int DichVuId { get; set; }

        [ForeignKey("DichVuId")]
        public DichVu DichVu { get; set; }

    }
}
