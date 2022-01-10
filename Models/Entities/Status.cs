using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Entities
{
    public class Status
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(150)]
        public string StatusName { get; set; }
    }
}
