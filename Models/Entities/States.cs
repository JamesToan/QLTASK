using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace coreWeb.Models.Entities
{
    public class States
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string StateName { get; set; }
    }
}
