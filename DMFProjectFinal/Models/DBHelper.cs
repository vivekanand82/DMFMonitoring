using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DMFProjectFinal.Models
{
    public class DBHelper
    {
        public static string connectionstring = string.Empty;
        static DBHelper()
        {
            try
            {
                connectionstring = ConfigurationManager.ConnectionStrings["constr"].ToString();
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public int ExecuteNonQuery(string CommandText, params SqlParameter[] parameters)
        {
            int k = 0;
            try
            {
                using (var con = new SqlConnection(connectionstring))
                using (var cmd = new SqlCommand(CommandText, con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    con.Open();
                    k = cmd.ExecuteNonQuery();

                }
                return k;
            }
            catch
            {
                return k;
            }
        }
        public static DataSet ExecuteQuery(string commandText, params SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (var con = new SqlConnection(connectionstring))
                using (var cmd = new SqlCommand(commandText, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Code");
                dt.Columns.Add("Remark");
                DataRow dr = dt.NewRow();
                dr["Code"] = "0";
                dr["Remark"] = ex.Message;
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);

            }
            return ds;
        }
    }
}