using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;


namespace coreWeb.Models.Entities
{
    public class DMXa
    {
        [Key]
        [MaxLength(5)]
        public string Id { get; set; }
        [MaxLength(250)]
        public string TenXa { get; set; }
        public string HuyenId { get; set; }
        public int? ThuTu { get; set; }
        public bool HieuLuc { get; set; }
        
        [JsonIgnore]
        [ForeignKey("HuyenId")]
        public DMHuyen Huyen { get; set; }

    }
}
