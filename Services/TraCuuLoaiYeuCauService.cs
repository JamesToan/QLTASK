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
    public class TraCuuLoaiYeuCauService
    {
        private readonly ApplicationDbContext _context;

        public TraCuuLoaiYeuCauService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TraCuuLoaiYeuCau> tracuuLoai(int? DonViId,string TuNgay, string DenNgay)
        {
            var adoConnStr = _context.Database.GetDbConnection().ConnectionString;
            SqlConnection connection = new SqlConnection(adoConnStr);

            List<TraCuuLoaiYeuCau> ds = new List<TraCuuLoaiYeuCau>();
            string selectProcedure = "[dbo].[TraCuuLoaiYeuCau]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if(DonViId != 0)
            {
                selectCommand.Parameters.AddWithValue("@DonViId", DonViId);
            }
            else if (DonViId == 0)
            {
                selectCommand.Parameters.AddWithValue("@DonViId", DBNull.Value);
            }
            if (TuNgay != null)
            {
                selectCommand.Parameters.AddWithValue("@TuNgay", TuNgay);
            }
            else if (TuNgay == null)
            {
                selectCommand.Parameters.AddWithValue("@TuNgay", DBNull.Value);
            }

            if (DenNgay != null)
            {
                selectCommand.Parameters.AddWithValue("@DenNgay", DenNgay);
            }
            else if (DenNgay == null)
            {
                selectCommand.Parameters.AddWithValue("@DenNgay", DBNull.Value);
            }

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                    ds = dt.ToList<TraCuuLoaiYeuCau>();
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return new List<TraCuuLoaiYeuCau>();
            }
            finally
            {
                connection.Close();
            }

            return ds;
        }
    }
}
