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

        [Key]
        public int Id { get; set; }

        [MaxLength(500)]
        public string TenDichVu { get; set; }

        public bool? IsActive { get; set; }
    }
}
