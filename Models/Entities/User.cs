using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Newtonsoft.Json;

namespace coreWeb.Models.Entities
{
    public class User 
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UId { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string JiraAcount { get; set; }
        [StringLength(250)]
        public string JiraPass { get; set; }
        public DateTime DateCreated { get; set; }
        public bool isActive { get; set; }

        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public DateTime userdatecreate { get; set; }

        public DateTime userlastlogin { get; set; }

        public string userlastip { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class AddUserModel
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string UID { get; set; }

        [Required(ErrorMessage = "Username bắt buộc nhập")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username phải từ 5 đến 20 ký tự")]
       
        public string UserName { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [StringLength(250)]
        
        public string PasswordConform { get; set; }

        [StringLength(250, ErrorMessage = "Name đầy đủ không quá 50 ký tự")]
        [Display(Name = "Name đầy đủ")]
        public string FullName { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime DateCreated { get; set; }
        public bool isActive { get; set; }

        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}
