using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;


namespace coreWeb.Models.Entities
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        [StringLength(128)]
        [Display(Name = "Tên đơn vị")]
        public string UnitName { get; set; }
        public int? ParentId { get; set; }
        [JsonIgnore]
        [ForeignKey("ParentId")]
        public virtual Unit Parent { get; set; }
        [StringLength(50)]
        public string LinhVuc { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Unit> children { get; set; }
        public Unit()
        {
            children = new List<Unit>();
        }
    }
}
