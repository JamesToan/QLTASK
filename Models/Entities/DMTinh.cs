using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class DMTinh
    {
        public DMTinh()
        {
            this.Huyen = new List<DMHuyen>();
        }
        [Key]
        [MaxLength(5)]
        public string Id { get; set; }
        [MaxLength(250)]
        public string TenTinh { get; set; }
        public int? ThuTu { get; set; }
        public bool HieuLuc { get; set; }
        public List<DMHuyen> Huyen { get; set; }
    }
}
