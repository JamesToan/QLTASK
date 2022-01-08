using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace coreWeb.Models.Entities
{
    public class Role 
    {
        public Role()
        {
            this.Users = new List<User>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UId { get; set; }

        [StringLength(128)]
        public string RoleName { get; set; }
        public bool IsActive { get; set; }

        [JsonIgnore]
        public virtual List<User> Users { get; set; }
    }
}
