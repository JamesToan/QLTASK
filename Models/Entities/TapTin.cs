using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace coreWeb.Models.Entities
{
    public class TapTin 
    {
        [StringLength(128)]
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
