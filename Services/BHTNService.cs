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
    
    public class BHTNService
    {
        
        private readonly ApplicationDbContext _context;


        public BHTNService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TraCuuBHTN TraCuu(string sobnhan ,  string cmnd , string sobhxh)
        {
            
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);
            var bhtn = new TraCuuBHTN();

            string selectProcedure = "[dbo].[TraCuuBHTN]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sobnhan != null)
            {
                selectCommand.Parameters.AddWithValue("@SoBienNhan", sobnhan);
               
                
            }
            else if(sobnhan == null)
            {
                selectCommand.Parameters.AddWithValue("@SoBienNhan", DBNull.Value);
            }

            if (cmnd != null)
            {
                
                selectCommand.Parameters.AddWithValue("@CMND", cmnd);
               
            }
            else if(cmnd == null)
            {
                selectCommand.Parameters.AddWithValue("@CMND", DBNull.Value);
            }

            if (sobhxh != null)
            {
                selectCommand.Parameters.AddWithValue("@SoBHXH", sobhxh);
            }
            else if(sobhxh == null)
            {
                selectCommand.Parameters.AddWithValue("@SoBHXH", DBNull.Value);
            }
            
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    bhtn = dt.ToList<TraCuuBHTN>().FirstOrDefault();
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return new TraCuuBHTN();
            }
            finally
            {
                connection.Close();
            }

            return bhtn;
        }
    }
}
