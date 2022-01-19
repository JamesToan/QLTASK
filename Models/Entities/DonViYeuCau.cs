using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class DonViYeuCau
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string TenDonViYeuCau { get; set; }

        public int DichVuId { get; set; }

        
        [ForeignKey("DichVuId")]
        public DichVu DichVu { get; set; }


    }
}
