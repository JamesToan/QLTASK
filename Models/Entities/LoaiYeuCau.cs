using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class LoaiYeuCau
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string TenLoaiYeuCau { get; set; }

        public bool? isActive { get; set; }

        public int Order { get; set; }
    }
}
