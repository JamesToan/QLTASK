using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class TapTin
    {
        [StringLength(128)]
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
