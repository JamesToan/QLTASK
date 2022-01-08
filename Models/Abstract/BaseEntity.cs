using System;
using System.ComponentModel.DataAnnotations;

namespace coreWeb.Models.Abstract
{
    public class BaseEntity : IBaseEntity, ISortable, ISoftDelete, IDateTracking
    {
        public BaseEntity()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public int Id { get; set; }

        [Display(Name = "Thứ tự hiển thị")]
        public int? DisplayOrder { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Ngày sửa")]
        public DateTime DateModified { get; set; }

        [Display(Name = "Ngày xóa")]
        public DateTime? DateDeleted { get; set; }

    }
}
