using coreWeb.Models;
using coreWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static coreWeb.Startup;
using coreWeb.Helpers;
namespace coreWeb.Services
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _context;
        private string _connectionString;
        public PermissionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public PermissionService()
        {
            _connectionString = MyOption.MyConnection;
        }
        public Permission GetPermission(int UserId, string Path)
        {
            var adoConnStr = _connectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);

            string selectProcedure = "[dbo].[PermissionGetbyRoute]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@UserId", UserId);
            selectCommand.Parameters.AddWithValue("@Path", Path);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return new Permission();
            }
            finally
            {
                connection.Close();
            }
            var obj = dt.ToList<Permission>();
            return obj.FirstOrDefault();
        }
    }
}
