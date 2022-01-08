using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Microsoft.AspNetCore.Http;

using coreWeb.Helps;


namespace coreWeb.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Route { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
    }

    public class PermissionRole
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        public int PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
        public bool IsActive { get; set; }
    }
    public class UserClaim
    {
        public int UserId { get; set; }
        //public int GroupId { get; set; }
        public int RoleId { get; set; }


        public UserClaim()
        {
        }

        public UserClaim(HttpContext context)
        {
            var currentUser = context.User;

            if (currentUser.Claims.Count() == 0) return;
            string strData = currentUser.Claims.FirstOrDefault(c => c.Type == "Data").Value;
            var dUser = new DataUser(strData);
            UserId = dUser.UserId;
            //GroupId = dUser.GroupId;
            RoleId = dUser.RoleId;
        }
    }
    public class DataUser
    {
        public int UserId { get; set; }
        //public int GroupId { get; set; }
        public int RoleId { get; set; }

        public DataUser(string txt)
        {
            string decodeString = Encryption.Decrypt(txt);

            string[] arrString = decodeString.Split("^");
            UserId = Convert.ToInt32(arrString[0]);
            RoleId = Convert.ToInt32(arrString[1]);
        }
    }

}
