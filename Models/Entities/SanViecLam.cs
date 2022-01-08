using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class SanViecLam
    {
        public SanViecLam()
        {
            this.DoanhNghiepThamGia = new List<SVLDoanhNghiep>();
            this.NguoiLaoDongThamGia = new List<SVLNguoiLaoDong>();
        }

        [Key]
        public int Id { get; set; }
        public Guid UID { get; set; }
        [MaxLength(50)]
        public string MaSanViecLam { get; set; }
        [MaxLength(500)]
        public string TenSanViecLam { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        [MaxLength(500)]
        public string NoiToChuc { get; set; }
        public int? LoaiSanId { get; set; }
        public int? NguoiTaoId { get; set; }
        public DateTime? NgayTao { get; set; }
        public int? NguoiDuyetId { get; set; }
        public DateTime? NgayDuyet { get; set; }
        [ForeignKey("LoaiSanId")]
        public DMLoaiSan LoaiSan { get; set; }

        [ForeignKey("NguoiTaoId")]
        public User NguoiTao { get; set; }
        [ForeignKey("NguoiDuyetId")]
        public User NguoiDuyet { get; set; }
        public List<SVLDoanhNghiep> DoanhNghiepThamGia { get; set; }
        public List<SVLNguoiLaoDong> NguoiLaoDongThamGia { get; set; }
    }
}
