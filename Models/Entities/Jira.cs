using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coreWeb.Models.Entities
{
    public class Jira
    {

        [Key]
        public int Id { get; set; }

        [MaxLength]
        public string TenJira { get; set; }

        [MaxLength]
        public string LinkJira { get; set; }

        public int YeuCauId { get; set; }

        public bool? IsActive { get; set; }

        [MaxLength]
        public string Comment { get; set; }

        [ForeignKey("YeuCauId")]
        public YeuCau YeuCau { get; set; }
    }
}
