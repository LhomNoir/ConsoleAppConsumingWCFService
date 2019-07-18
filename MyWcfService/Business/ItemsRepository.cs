using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Business
{
    public class ItemsRepository
    {
        // -- https://www.outcoldman.com/en/archive/2010/08/28/tsql-passing-array-list-set-to-stored-procedure-ms-sql-server/

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="sqlConnection"></param>
        /// <returns></returns>
        public static DataTable FindItems(List<int> categories, string sqlConnection)
        {
            DataTable tbCategories = new DataTable("FilterCategory");
            tbCategories.Columns.Add("Id", typeof(int));
            categories.ForEach(x => tbCategories.Rows.Add(x));

            using (SqlConnection connection = new SqlConnection(sqlConnection))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string tableName = string.Format("tempdb..#{0}", tbCategories.TableName);

                        CreateTableOnSqlServer(connection, transaction, tbCategories, tableName);
                        CopyDataToSqlServer(connection, transaction, tbCategories, tableName);

                        DataTable result = new DataTable();
                        using (SqlCommand command = new SqlCommand("FindItems", connection, transaction)
                        { CommandType = CommandType.StoredProcedure })
                        {
                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                            {
                                dataAdapter.Fill(result);
                            }
                        }
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        private static void CopyDataToSqlServer(SqlConnection connection, SqlTransaction transaction, DataTable table,
                                                string tableName)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction)
            {
                DestinationTableName = tableName
            })
            {
                bulkCopy.WriteToServer(table);
            }
        }

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="table"></param>
        /// <param name="tableName"></param>
        private static void CreateTableOnSqlServer(SqlConnection connection, SqlTransaction transaction, 
            DataTable table, string tableName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("create table {0}(", tableName);
            foreach (DataColumn column in table.Columns)
            {
                sb.AppendFormat("{0} {1} {2}",
                                table.Columns.IndexOf(column) == 0 ? string.Empty : ",",
                                column.ColumnName, GetSqlType(column.DataType));
            }
            sb.Append(")");

            using (SqlCommand command = new SqlCommand(sb.ToString(), connection, transaction))
            {
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetSqlType(Type type)
        {
            if (type == typeof(string))
                return string.Format("{0}(max)", SqlDbType.VarChar);
            else if (type == typeof(int))
                return SqlDbType.Int.ToString();
            else if (type == typeof(bool))
                return SqlDbType.Bit.ToString();
            else if (type == typeof(DateTime))
                return SqlDbType.DateTime.ToString();
            else if (type == typeof(Single))
                return SqlDbType.Float.ToString();
            else throw new NotImplementedException();
        }
    }

    /*
     * 
     * if object_id('FindItems') is not null 
    drop proc FindItems
go
 
set ansi_nulls on
go 
set quoted_identifier on
go
 
create proc FindItems
as
begin
    if object_id('tempdb..#FilterCategory') is null 
    begin
        raiserror('#FilterCategory(id int) should be created', 16, 1)
        return
    end
    
    select i.Name as ItemName, f.Name as FirmName, c.Name as CategoryName 
    from Item i
        inner join Firm f on i.FirmId = f.FirmId
        inner join Category c on i.CategoryId = c.CategoryId
        inner join #FilterCategory cf on c.CategoryId = cf.Id
end 



    /--------------------------------------------------------------------------------------/
     * List<int> categories = new List<int>() { 1, 2, 3 };
 
DataTable tbCategories = new DataTable("FilterCategory");
tbCategories.Columns.Add("Id", typeof(int));
categories.ForEach(x => tbCategories.Rows.Add(x));
 
DataTable table = new DataTable();
using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=TableParameters;Integrated Security=SSPI;"))
{
  connection.Open();
  using (SqlCommand command = new SqlCommand("FindItems", connection) { CommandType = CommandType.StoredProcedure })
  {
    command.Parameters.AddWithValue("@categories", tbCategories);
    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
    {
      dataAdapter.Fill(table);
    }
  }
}
     * */
}
