using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class QuanLyDichVu
    {

        [Key]
        public int Id { get; set; }

        public int? DichVuId { get; set; }

        public int? NhanSuId { get; set; }

        public int? UnitId { get; set; }

        [ForeignKey("DichVuId")]
        public DichVu DichVu { get; set; }

        [ForeignKey("NhanSuId")]
        public NhanSu NhanSu { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

    }
}
