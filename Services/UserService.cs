using coreWeb.Helpers;
using coreWeb.Models;
using coreWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace coreWeb.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetListUser()
        {
            var result = _context.User.ToList();
            return result;
        }

        public int Add(string UserName, string Password, string FullName, int RoleId, int? UnitId)
        {
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);
            string insertProcedure = "[dbo].[UserInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;

            insertCommand.Parameters.AddWithValue("@UserName", UserName);
            insertCommand.Parameters.AddWithValue("@Password", Password);
            insertCommand.Parameters.AddWithValue("@FullName", FullName);
            insertCommand.Parameters.AddWithValue("@RoleId", RoleId);
            if (UnitId != null)
            {
                insertCommand.Parameters.AddWithValue("@UnitId", UnitId);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UnitId", DBNull.Value);
            }

            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public User Login(string UserName, string Password)
        {
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);

            var objDM = new User();

            string selectProcedure = "[dbo].[UserLogin]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@UserName", UserName);
            selectCommand.Parameters.AddWithValue("@Password", Password);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    objDM = dt.ToList<User>().FirstOrDefault();
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return new User();
            }
            finally
            {
                connection.Close();
            }
            return objDM;
        }

        public int ChangePass(int UserId, string PasswordOld, string PasswordNew)
        {
           var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
           SqlConnection connection = new SqlConnection(adoConnStr);

           string updateProcedure = "[dbo].[UserChangePass]";
           SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
           updateCommand.CommandType = CommandType.StoredProcedure;


           updateCommand.Parameters.AddWithValue("@UserId", UserId);
           updateCommand.Parameters.AddWithValue("@PasswordOld", PasswordOld);
           updateCommand.Parameters.AddWithValue("@PasswordNew", PasswordNew);

           updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
           updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
           try
           {
               connection.Open();
               updateCommand.ExecuteNonQuery();
               int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
               if (count > 0)
               {
                   return 1;
               }
               else
               {
                   return 0;
               }
           }
           catch (SqlException)
           {
               return 0;
           }
           finally
           {
               connection.Close();
           }
        }

        public int ResetPass(int UserId)
        {
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);

            string updateProcedure = "[dbo].[UserResetPass]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;


            updateCommand.Parameters.AddWithValue("@UserId", UserId);


            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
