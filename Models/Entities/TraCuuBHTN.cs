using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace coreWeb.Models.Entities
{
    public class TraCuuBHTN
    {
        [Key]
        [StringLength(10)]
        public string Id { get; set; }

        [StringLength(10)]
        public string SoBNhan { get; set; }
        
        [StringLength(200)]
        public string HoTen { get; set; }

        [StringLength(10)]
        public string NgaySinh { get; set; }

        [StringLength(20)]
        public string cmnd { get; set; }

        [StringLength(20)]
        public string SoBHXH { get; set; }

        public DateTime NgayNopHs { get; set; }

        public DateTime NgayNhanKQ { get; set; }

        public string SoQD { get; set; }

        public DateTime NgayQD { get; set; }

        public int SoThangHuong { get; set; }

        public decimal MucHuong { get; set; }
    }
}
