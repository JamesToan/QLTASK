using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

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

        public int TrangThaiId { get; set; }

        public bool? IsActive { get; set; }
    }
}
