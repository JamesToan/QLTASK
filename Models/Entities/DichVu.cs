using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class DichVu
    {

        public DichVu()
        {
            this.DonVi = new List<DonViYeuCau>();
            this.LoaiYeuCauNew = new List<LoaiYeuCauNew>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string TenDichVu { get; set; }
        
        public bool? IsActive { get; set; }

        public List<DonViYeuCau> DonVi { get; set; }

        public List<LoaiYeuCauNew>  LoaiYeuCauNew { get; set; }
    }
}
