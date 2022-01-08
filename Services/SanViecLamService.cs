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
    public class SanViecLamService
    {
        private readonly ApplicationDbContext _context;


        public SanViecLamService(ApplicationDbContext context)
        {
            _context = context;
        }


        public SanViecLam GetViecLam(int loai)
        {
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);
            var sanvl = new SanViecLam();
            string selectProcedure = "[dbo].[GetSanViecLam]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;

            selectCommand.Parameters.AddWithValue("@loai", loai);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    sanvl = dt.ToList<SanViecLam>().FirstOrDefault();
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return new SanViecLam();
            }
            finally
            {
                connection.Close();
            }

            return sanvl;
        }
    }
}
