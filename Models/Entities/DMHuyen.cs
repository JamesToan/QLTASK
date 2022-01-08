using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;


namespace coreWeb.Models.Entities
{
    public class DMHuyen
    {
        public DMHuyen()
        {
            this.Xa = new List<DMXa>();
        }
        [Key]
        [MaxLength(5)]
        public string Id { get; set; }
        [MaxLength(250)]
        public string TenHuyen { get; set; }
        public string TinhId { get; set; }
        public int? ThuTu { get; set; }
        public bool HieuLuc { get; set; }
        
        [JsonIgnore]
        [ForeignKey("TinhId")]
        public DMTinh Tinh { get; set; }
        public List<DMXa> Xa { get; set; }
    }
}
