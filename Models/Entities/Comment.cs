using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Models.Entities
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }

        [MaxLength]
        public string Comments { get; set; }

        public int? UserId { get; set; }

        public int? YeuCauId { get; set; }

        public DateTime? NgayComment { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("YeuCauId")]
        public YeuCau YeuCau { get; set; }
    }
}
