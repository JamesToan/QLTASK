using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace coreWeb.Models.Entities
{
    public class DaoTaoNghe
    {
        public DaoTaoNghe()
        {
            this.DangKyHocNghe = new List<DangKyHocNghe>();
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(200)]
        public string TenKhoaHoc { get; set; }
        public int? LoaiHinhDaoTaoId { get; set; }
        [MaxLength(200)]
        public string ThoiGianDaoTao { get; set; }
        [MaxLength(200)]
        public string HocPhi { get; set; }
        [MaxLength]
        public string MoTaKhoaHoc { get; set; }
        [MaxLength]
        public string YeuCauKhoaHoc { get; set; }
        public int? ThuTu { get; set; }
        public bool? HieuLuc { get; set; }
        [ForeignKey("LoaiHinhDaoTaoId")]
        public DMLoaiHinhDaoTao LoaiHinhDaoTao { get; set; }
        public List<DangKyHocNghe> DangKyHocNghe { get; set; }
    }
}