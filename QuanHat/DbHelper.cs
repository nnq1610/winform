using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuanHat
{
    class DbHelper
    {
        private string connectionString = @"Server=Immagorst\MSSQLSERVER01;Database=QuanLyKaraoke;Trusted_Connection=True;";

        // ExecuteQuery with optional parameters (returns a DataTable)
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        //adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // ExecuteNonQuery with parameters (used for INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
