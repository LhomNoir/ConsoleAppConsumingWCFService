using MyWcfService.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string connectionString;

        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataSet GetDataSet(DatabaseCommandInfo data)
        {
            var ds = new DataSet();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = GetSqlCommand(data, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        ds.Load(rdr, data.Option, data.TableNames);
                    }
                    cmd.Parameters.Clear();
                }
            }
            return ds;
        }

        public DataTable GetDataTable(DatabaseCommandInfo data)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = GetSqlCommand(data, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                    }
                    cmd.Parameters.Clear();
                }
            }
            return dt;
        }

        public void ExecuteNonQuery(DatabaseCommandInfo data)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(data.StoredProcName, con))
                {
                    cmd.CommandType = data.CommandType;
                    cmd.Parameters.AddRange(data.Parameters);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
        }

        private SqlCommand GetSqlCommand(DatabaseCommandInfo data, SqlConnection sqlConnection)
        {
            var cmd = new SqlCommand(data.StoredProcName, sqlConnection)
            {
                CommandType = data.CommandType
            };

            if (data.Parameters != null)
                cmd.Parameters.AddRange(data.Parameters);

            return cmd;
        }

    }
}
