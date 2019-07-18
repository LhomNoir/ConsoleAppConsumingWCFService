using MyWcfService.Business.Interfaces;
using MyWcfService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Business
{
    //class BusinessAccessLayer
    //{
    //}
    public class BusinessAccessLayer
    {
        public string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //private Employee employee;
        private Person person;
        private List<Person> persons = null;
        //private List<Employee> employeeList = null;


        /// <summary>
        /// --- ///!!!!\\\ ---
        /// </summary>
        public void ZSDED()
        {
            #region --   --
            //string columns = string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
            //string values = string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(c => string.Format("@{0}", c.ColumnName)));
            //String sqlCommandInsert = string.Format("INSERT INTO dbo.RAW_DATA({0}) VALUES ({1})", columns, values);

            //using (var con = new SqlConnection("ConnectionString"))
            //using (var cmd = new SqlCommand(sqlCommandInsert, con))
            //{
            //    con.Open();
            //    foreach (DataRow row in dataTable.Rows)
            //    {
            //        cmd.Parameters.Clear();
            //        foreach (DataColumn col in dataTable.Columns)
            //            cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col]);
            //        int inserted = cmd.ExecuteNonQuery();
            //    }
            //}
            #endregion
        }


        #region -- Generics  --
        //// params object[] args
        //public List<Student> SelectAll(string sp, List<string> param)
        public List<Person> SelectAll(string sp, List<string> param)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                persons = new List<Person>();
                sqlcon.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                #region MyRegion
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            for (int i = 0; i < param.Count; i++)
                            {
                                person = new Person
                                {
                                    Id = Convert.ToInt32(dr[param[0]]),
                                    Name = dr[param[1]] as string,
                                    Age = Convert.ToInt32(dr[param[2]])
                                };
                            }
                            persons.Add(person);
                        }
                        return persons;
                    }
                    else
                        return null;
                }
                #endregion
                //return null;
            }
        }

        #endregion
        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public List<Person> SelectAllStudents(string sp)
        {
            using (SqlConnection sqlcon = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                sqlcon.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        List<Person> lstStudents = new List<Person>();

                        while (dr.Read())
                        {
                            person = new Person
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Name = dr["Name"] as string,
                                Age = Convert.ToInt32(dr["Age"])
                            };

                            lstStudents.Add(person);
                        }

                        return lstStudents;
                    }
                    else
                        return null;
                }
            }
        }
                
        /// <summary>
        /// --  --
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public int Insert(string procedureName, Person emp)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conStr))
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = procedureName;

                    sqlcmd.Parameters.AddWithValue("@Name", emp.Name);
                    sqlcmd.Parameters.AddWithValue("@Age", emp.Age);

                    sqlcmd.Connection = sqlcon;
                    sqlcon.Open();
                    int count = sqlcmd.ExecuteNonQuery();

                    if (count > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public int Update(string procedureName, Person emp)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conStr))
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = procedureName;

                    sqlcmd.Parameters.AddWithValue("@Name", emp.Name);
                    sqlcmd.Parameters.AddWithValue("@Age", emp.Age);
                    sqlcmd.Parameters.AddWithValue("@Id", emp.Id);

                    sqlcmd.Connection = sqlcon;
                    sqlcon.Open();
                    int count = sqlcmd.ExecuteNonQuery();

                    if (count > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// --  --
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        public int Delete(string procedureName, Person emp)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conStr))
                using (SqlCommand sqlcmd = new SqlCommand())
                {
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.CommandText = procedureName;

                    sqlcmd.Parameters.AddWithValue("@Id", emp.Id);

                    sqlcmd.Connection = sqlcon;
                    sqlcon.Open();
                    int count = sqlcmd.ExecuteNonQuery();

                    if (count > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// --   --
        /// </summary>
        /// <param name="std"></param>
        /// <returns></returns>
        public int Add(string procedureName, Person std)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(conStr))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = std.Name;
                    cmd.Parameters.Add("@Age", SqlDbType.Int).Value = std.Age;
                    cmd.Connection = sqlcon;

                    sqlcon.Open();
                    int count = cmd.ExecuteNonQuery();

                    if (count > 0)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        #region MyRegion
        /// <summary>
        /// --  --
        /// </summary>
        /// <param name="sp"></param>
        public void RetournData(string sp)
        {
            using (SqlConnection sqlConnection1 = new SqlConnection(conStr))
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlDataReader reader;

                cmd.CommandText = sp;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                reader = cmd.ExecuteReader();
                // Data is accessible through the DataReader object here.
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nom = reader[1] as string;
                        string gender = reader[2] as string;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
            }
        }

        /// <summary>
        /// --    --
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        public DataTable ExecuteProc(string procedureName, Object[] parameterList, bool isSP) // throws SystemException
        {
            DataTable outputDataTable;
            string conString = @"Server=.\\SQLEXPRESS01;Database=TestDB;Trusted_Connection=Yes";

            using (SqlConnection sqlConnection = new SqlConnection(conString))
            using (SqlCommand sqlCommand = new SqlCommand(procedureName, sqlConnection))
            {
                // -   -   
                sqlCommand.CommandType = isSP ? CommandType.StoredProcedure : CommandType.Text;

                if (parameterList != null)
                {
                    for (int i = 0; i < parameterList.Length; i = i + 2)
                    {
                        string parameterName = parameterList[i].ToString();
                        object parameterValue = parameterList[i + 1];

                        sqlCommand.Parameters.Add(new SqlParameter(parameterName, parameterValue));
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataSet outputDataSet = new DataSet();
                try
                {
                    sqlDataAdapter.Fill(outputDataSet, "resultset");
                }
                catch (SystemException systemException)
                {
                    // The source table is invalid.
                    throw systemException; // to be handled as appropriate by calling function
                }

                outputDataTable = outputDataSet.Tables["resultset"];
            }

            return outputDataTable;
        }
        #endregion





        #region --   Bruno  --
        #region --  Insert  --
        private static Dictionary<int, SqlCommand> openedCmd;
        private static Dictionary<int, SqlConnection> openedConn;
        private static IFormatProvider formatProvider;

        /// <summary>
        /// --  --
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery000000(string statement, params object[] args)
            => ExecuteNonQuery000000(string.Format(formatProvider, statement, args));

        /// <summary>
        /// --  --
        /// </summary>
        /// <param name="statement"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string statement, params object[] args)
        {
            return ExecuteNonQuery(string.Format(formatProvider, statement, args));
        }

        public int ExecuteNonQuery(string statement)
        {
            SqlConnection sqlConn = null; SqlCommand sqlCmd = null; int result;
            try
            {
                sqlConn = GetNewOpenedConnection();
                sqlCmd = new SqlCommand(statement, sqlConn);
                sqlCmd.CommandTimeout = 60;
                result = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FreeMemory(sqlCmd, sqlConn);
            }
            return result;
        }

        static public void FreeMemory(SqlCommand sqlCmd, SqlConnection sqlConn)
        {
            sqlCmd.Dispose();
            sqlConn.Close();
            sqlConn.Dispose();
        }
        #endregion

        // -------------------------------------------------------

        #region - Constants -
        /// <summary>
        /// Time before terminating the attempt to execute a command.
        /// </summary>
        private const int COMMAND_TIMEOUT_QUERY = 300;      //-> 300 seconds (5 minutes)
        private const int COMMAND_TIMEOUT_PROCEDURE = 1200;  //-> 1200 seconds (10 minutes)
        #endregion

        #region - Generic functions -
        public SqlDataReader ExecuteReader(string statement)
        {
            SqlConnection sqlConn = null; SqlCommand sqlCmd = null; SqlDataReader result;
            try
            {
                sqlConn = GetNewOpenedConnection();
                sqlCmd = new SqlCommand(statement, sqlConn);
                //sqlCmd.CommandTimeout = 60;
                result = sqlCmd.ExecuteReader();
                // - Saving opened connection and command -
                openedCmd.Add(result.GetHashCode(), sqlCmd);
                openedConn.Add(result.GetHashCode(), sqlConn);
            }
            catch (Exception e)
            {
                FreeMemory(sqlCmd, sqlConn);
                throw e;
            }
            return result;
        }

        public object ExecuteScalar(string query, params object[] args)
        {
            return ExecuteScalar(string.Format(query, args));
        }

        public object ExecuteScalar(string statement)
        {
            SqlConnection sqlConn = null; SqlCommand sqlCmd = null; object result;
            try
            {
                sqlConn = GetNewOpenedConnection();
                sqlCmd = new SqlCommand(statement, sqlConn);
                sqlCmd.CommandTimeout = 60;
                result = sqlCmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                FreeMemory(sqlCmd, sqlConn);
            }
            return result;
        }

        public int ExecuteNonQueryLong(string statement, params object[] args)
        {
            // - Build SQL connection -
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                // - Open connection -
                sqlConnection.Open();

                // - Build SQL command -
                using (SqlCommand sqlCommand = new SqlCommand(string.Format(statement, args), sqlConnection))
                {
                    // - Set command timeout -
                    sqlCommand.CommandTimeout = 0;  //-> 0 means infinite

                    // - Run long command -
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }
        #endregion


        #region --  BRUNO Select data from SQL server Datatable  --
        public DataTable GetDataTable(string query, params object[] args)
        {
            return GetDataTable(string.Format(formatProvider, query, args));
        }

        public DataTable GetDataTable(string query)
        {
            // - Init -
            DataTable dataTable = new DataTable();

            //SqlDataReader sDR = ExecuteReader(query);
            SqlConnection sqlConn = null; SqlCommand sqlCmd = null;
            sqlConn = GetNewOpenedConnection();
            sqlCmd = new SqlCommand(query, sqlConn);
            //sqlCmd.CommandTimeout = 60;
            SqlDataReader sDR = sqlCmd.ExecuteReader();

            // - Building datatable -
            for (int i = 0; i < sDR.FieldCount; i++)
            {
                dataTable.Columns.Add(new DataColumn(sDR.GetName(i), SQLType.GetType(sDR.GetDataTypeName(i))));
            }

            // - Start loading data (remove all index, constraints...) -
            dataTable.BeginLoadData();

            // - Fill datatable -
            dataTable.Load(sDR, LoadOption.OverwriteChanges);

            // - Stop loading data (Re-Apply all index, constraints...) -
            dataTable.EndLoadData();

            // - Prevent datatable storing several versions of rows -
            dataTable.AcceptChanges();

            //// - Free memory -
            //CloseOpenedQuery(sDR);

            // - Return result -
            return dataTable;
        }

        public DataTable GetSingleResult(string cmdText)
        {
            return GetSingleResult(cmdText, 0);
        }

        public DataTable GetSingleResult(string cmdText, int minimumCapacity)
        {
            // - Build SQL Connection -
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                // - Open SQL Connection -
                sqlConnection.Open();

                // - Build SQL command -
                using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                {
                    // - Set command timeout -
                    sqlCommand.CommandTimeout = COMMAND_TIMEOUT_QUERY;

                    // - Run stored procedure -
                    using (SqlDataReader sdReader = sqlCommand.ExecuteReader(CommandBehavior.SingleResult))
                    {
                        // - Retrieve field count -
                        int fieldCount = sdReader.FieldCount;

                        // - Building datatable -
                        DataTable dataTable = new DataTable();
                        if (minimumCapacity > 0) dataTable.MinimumCapacity = minimumCapacity;

                        // - Fill columns -
                        for (int i = 0; i < fieldCount; i++)
                            dataTable.Columns.Add(new DataColumn(sdReader.GetName(i), sdReader.GetFieldType(i)));

                        // - Read each line -
                        while (sdReader.Read())
                        {
                            DataRow dRow = dataTable.NewRow();
                            for (int i = 0; i < fieldCount; i++)
                            {
                                dRow[i] = sdReader[i];
                            }
                            dataTable.Rows.Add(dRow);
                        }

                        // - Return data result set -
                        return dataTable;
                    }
                }
            }
        }
        #endregion
        // -------------------------------------------------------
        #region - Connection -
        public SqlConnection GetNewConnection()
        {
            return new SqlConnection(conStr);
        }

        public SqlConnection GetNewOpenedConnection()
        {
            SqlConnection sqlConn = new SqlConnection(conStr);
            sqlConn.Open();
            return sqlConn;
        }
        #endregion

        #region - Static Functions -
        public void CloseOpenedQuery(SqlDataReader sDR)
        {
            // - Init -
            int hashCode = sDR.GetHashCode();

            // - Close SqlDataReader -
            sDR.Close();
            sDR.Dispose();

            // - Get and close SqlCommand -
            if (openedCmd.ContainsKey(hashCode))
            {
                SqlCommand sqlCmd = openedCmd[hashCode];
                openedCmd.Remove(hashCode);
                sqlCmd.Dispose();
            }

            // - Get and close SqlConnection -
            if (openedConn.ContainsKey(hashCode))
            {
                SqlConnection sqlConn = openedConn[hashCode];
                openedConn.Remove(hashCode);
                sqlConn.Close();
                sqlConn.Dispose();
            }
        }
        #endregion


        #region - Static method Close / FreeMemory -
        /// <summary>
        /// Clear all data and call "DataTable.Dispose()" method (E2MKI)
        /// </summary>
        /// <param name="dataTable"></param>
        static public void FreeMemory(DataTable dataTable)
        {
            if (dataTable != null)
            {
                dataTable.Clear();
                dataTable.Dispose();
                dataTable = null;
            }
        }
        static public void FreeMemory(SqlConnection sqlConn)
        {
            sqlConn.Close();
            sqlConn.Dispose();
        }
        static public void FreeMemory(SqlCommand sqlCmd)
        {
            sqlCmd.Dispose();
        }
        static public void FreeMemory(SqlCommand sqlCmd, SqlDataReader sRdr)
        {
            sqlCmd.Dispose();
            sRdr.Close();
            sRdr.Dispose();
        }
        static public void FreeMemory(SqlCommand sqlCmd, SqlDataReader sRdr, SqlConnection sqlConn)
        {
            sqlCmd.Dispose();
            sRdr.Close();
            sRdr.Dispose();
            sqlConn.Close();
            sqlConn.Dispose();
        }
        #endregion

        #endregion

        #region ----  !!!!!!!!!!!!!!!!!!!!!!!!!  -----
        /// <summary>
        /// Insert, Update and Delete in the database through this method
        /// </summary>
        /// <param name="sql">The SQL Query or the name of the Stored Procedure</param>
        /// <param name="parameters">The values which you have to insert, update, or delete</param>
        /// <param name="isProcedure">If the first parameter "sql" is any name of the stored procedure then it must be true</param>
        /// <returns>True for successful execution, otherwise False</returns>
        public bool InsertUpdateDelete(string sql, bool isProcedure, Dictionary<string, object> parameters = null, int keyIdentity = 0)
        {
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    if (isProcedure)
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                    else
                        sqlCommand.CommandType = CommandType.Text;

                    if (parameters != null)
                    {
                        // Adding parameters using Dictionary...
                        foreach (KeyValuePair<string, object> parameter in parameters)
                            sqlCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));

                        //SQLParam.AddParameter(sqlCommand, parameter.Key, parameter.Value);
                    }

                    #region MyRegion
                    //for (int i = 0; i < competitorList.Count; i++)
                    //{
                    //    cmd.Parameters["@CompetitorID"].Value = competitorList[i];
                    //    cmd.Parameters["@ProductCode"].Value = productCodeList[i];
                    //    cmd.Parameters["@ProductName"].Value = productNameList[i];
                    //    // etc
                    //    int rowsAffected = cmd.ExecuteNonQuery();
                    //}
                    #endregion

                    if (sqlCommand.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool GetdDATA()
        {
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            using (SqlCommand cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PS_SelectAllEmployeeList";
                sqlConnection.Open();
                //return (int)cmd.ExecuteScalar();
                Person employee;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        var employeeList = new List<Person>();
                        while (reader.Read())
                        {
                            employee = new Person()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Age = int.Parse(reader["Age"].ToString()),
                            };
                            employeeList.Add(employee);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
        }

        //public bool GetdDATA()
        public bool GetSingleResultMM(string sp)
        {
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            using (SqlCommand cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                sqlConnection.Open();
                int res = (int)cmd.ExecuteScalar();
                if (res > 0)
                {
                    return true;
                }
                else
                    return false;
            }
        }


        //public bool DoSQLQuery(string statement, params object[] args)
        //{
        //    //return ExecuteNonQuery(string.Format(formatProvider, statement, args));
        //    //string preparedCommand = @"Insert into [MM_EmployeeDapper] (Name, Position, Age, Salary)  Values(@Name, @Position, @Age, @Salary)";


        //    using (SqlConnection sqlConnection = new SqlConnection(conStr)) //if (varConnection != null) {
        //    using (var sqlQuery = new SqlCommand(statement, sqlConnection))
        //    {

        //        sqlQuery.Parameters.AddWithValue("@Name", "Name");
        //        sqlQuery.Parameters.AddWithValue("@Position", "varStopaOdniesienia");
        //        //sqlQuery.Parameters.Add("@data");
        //        sqlQuery.Parameters.AddWithValue("@Age", 33);
        //        sqlQuery.Parameters.AddWithValue("@Salary", 43680);

        //        sqlConnection.Open();
        //        //sqlQuery.Prepare();
        //        if (sqlQuery.ExecuteNonQuery() > 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}

        public bool pobierzBenchmarkKolejny()
        {
            #region MyRegion 
            List<string> values = new List<string>() { "@Name", "@Position", "@Age", "@Salary" };
            #endregion

            //const string preparedCommand = @"SELECT [dbo].[ufn_BenchmarkKolejny](@varPortfelID, @data, @varBenchmarkPoprzedni,  @varStopaOdniesienia) AS 'Benchmark'";
            string preparedCommand = @"Insert into [MM_EmployeeDapper] (Name, Position, Age, Salary)  Values(@Name, @Position, @Age, @Salary)";


            using (SqlConnection sqlConnection = new SqlConnection(conStr)) //if (varConnection != null) {
            using (var sqlQuery = new SqlCommand(preparedCommand, sqlConnection))
            {

                sqlQuery.Parameters.AddWithValue("@Name", "Name");
                sqlQuery.Parameters.AddWithValue("@Position", "varStopaOdniesienia");
                //sqlQuery.Parameters.Add("@data");
                sqlQuery.Parameters.AddWithValue("@Age", 33);
                sqlQuery.Parameters.AddWithValue("@Salary", 43680);

                sqlConnection.Open();
                //sqlQuery.Prepare();
                if (sqlQuery.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// -- !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! --
        /// </summary>
        /// <param name="table"></param>
        public void DoInsertUpdate(string table)
        {
            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                //using (var command = new SqlCommand())
                //{
                //    command.CommandText =
                //        "INSERT INTO " + table.Title + " ("
                //      + string.Join(", ", table.Headers)
                //      + ") VALUES ("
                //      + string.Join(", ", table.Headers.Select(x => "@" + x))
                //      + ");";
                //    command.Connection = sqlConnection;

                //    foreach (var header in table.Headers)
                //    {
                //        /*
                //             Add all parameters as strings. One could choose to infer the
                //             data types by inspecting the first N rows or by using some sort
                //             of specification to map the types from A to B.
                //         */
                //        command.Parameters.Add("@" + header, typeof(string));
                //    }

                //    foreach (var row in table.RowData)
                //    {
                //        for (var i = 0; i < table.Headers.Count(); i++)
                //        {
                //            if (!string.IsNullOrEmpty(row.ElementAt(i)))
                //            {
                //                command.Parameters["@" + table.Headers.ElementAt(i)].Value = row.ElementAt(i);
                //            }
                //            else
                //            {
                //                command.Parameters["@" + table.Headers.ElementAt(i)].Value = DBNull.Value;
                //            }
                //        }

                //        command.ExecuteNonQuery();
                //    }
                //}
            }
        }
        #endregion

        #region ----- !!!!!!!!!!!!!  A essayer  !!!!!!!!!!!!  --
        private static string ConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private SqlConnection Conn = new SqlConnection(ConnString);

        public void ExecuteStoredProcedure(string procedureName)
        {
            SqlConnection sqlConnObj = new SqlConnection(ConnString);

            SqlCommand sqlCmd = new SqlCommand(procedureName, sqlConnObj);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlConnObj.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnObj.Close();
        }

        public void ExecuteStoredProcedure(string procedureName, object model)
        {
            var parameters = GenerateSQLParameters(model);
            SqlConnection sqlConnObj = new SqlConnection(ConnString);

            SqlCommand sqlCmd = new SqlCommand(procedureName, sqlConnObj);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            foreach (var param in parameters)
            {
                sqlCmd.Parameters.Add(param);
            }

            sqlConnObj.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConnObj.Close();
        }

        private List<SqlParameter> GenerateSQLParameters(object model)
        {
            var paramList = new List<SqlParameter>();
            Type modelType = model.GetType();
            var properties = modelType.GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(model) == null)
                {
                    paramList.Add(new SqlParameter(property.Name, DBNull.Value));
                }
                else
                {
                    paramList.Add(new SqlParameter(property.Name, property.GetValue(model)));
                }
            }
            return paramList;
        }
        #endregion

        #region -- ****  Generic GetData From DataBase Method **** --
        // --  GENERIC METHOD TO CONVERT DATATABLE TO LIST  --
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
        //// --  CALL THAT METHOD  --
        //List<Student> studentDetails = new List<Student>();
        //studentDetails = ConvertDataTable<Student>(dt);  


        // --  WITH LIN LOOP  --
        public static List<Person> ConvertDataTableToListLoop(DataTable dt)
        {
            List<Person> studentList = new List<Person>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                studentList.Add(new Person()
                {
                    Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                    Name = dt.Rows[i]["Name"].ToString(),
                    Age = Convert.ToInt32(dt.Rows[i]["Age"])
                });
            }
            return studentList;
        }

        //// --  WITH LIN QUERY  --
        public static List<Person> ConvertDataTableToListLink(DataTable dt)
        {
            List<Person> studentList = new List<Person>();
            studentList = (from DataRow dr in dt.Rows
                           select new Person()
                           {
                               Id = Convert.ToInt32(dr["Id"]),
                               Name = dr["Name"].ToString(),
                               Age = Convert.ToInt32(dr["Age"])
                           }).ToList();

            return studentList;
        }

        //public void StudentListUsingLink()
        //{
        //    //  DataTable dt = new DataTable("Branches");  
        //    DataTable dt = new DataTable("Student");
        //    dt.Columns.Add("StudentId", typeof(Int32));
        //    dt.Columns.Add("StudentName", typeof(string));
        //    dt.Columns.Add("Address", typeof(string));
        //    dt.Columns.Add("MobileNo", typeof(string));
        //    //Data  
        //    dt.Rows.Add(1, "Manish", "Hyderabad", "0000000000");
        //    dt.Rows.Add(2, "Venkat", "Hyderabad", "111111111");
        //    dt.Rows.Add(3, "Namit", "Pune", "1222222222");
        //    dt.Rows.Add(4, "Abhinav", "Bhagalpur", "3333333333");

        //    List<Student> studentList = new List<Student>();
        //    studentList = (from DataRow dr in dt.Rows
        //                   select new Student()
        //                   {
        //                       StudentId = Convert.ToInt32(dr["StudentId"]),
        //                       StudentName = dr["StudentName"].ToString(),
        //                       Address = dr["Address"].ToString(),
        //                       MobileNo = dr["MobileNo"].ToString()
        //                   }).ToList();

        //}

        #region GetListFromDataTableRevised<T>
        /// <summary>
        /// Get List of type T from Datatable.
        /// </summary>
        /// <typeparam name="T">Custom Class.</typeparam>
        /// <param name="dataTable">Datatable that is to be converted into list of type T.</param>
        /// <returns>List of T type.</returns>
        public static List<T> GetListFromDataTableRevised<T>(DataTable dataTable)
        {
            List<T> lst = null;

            try
            {
                //Obtains the type of the generic class.
                Type entType = typeof(T);

                if (dataTable != null && entType != null)
                {
                    PropertyInfo propertyInfo = null; //To hold propery information of type T.
                    object defaultInstance = null; //To hold object of type T.
                    IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

                    //Create a new list of type T.
                    lst = new List<T>();

                    //Each row of table is mapped with individual item of List<T>.
                    foreach (DataRow row in dataTable.Rows)
                    {
                        //Create a new instance of type T to set property value.
                        defaultInstance = Activator.CreateInstance(entType);

                        if (defaultInstance != null)
                        {
                            //Each column is mapped with particular property of type T.
                            foreach (DataColumn dc in dataTable.Columns)
                            {
                                //Get property of respective datacolumn of class T.
                                propertyInfo = entType.GetProperty(dc.ColumnName);

                                //Get the value of property from row data.
                                object columnvalue = row[dc.ColumnName];

                                if (propertyInfo != null &&
                                    columnvalue != null &&
                                    columnvalue != DBNull.Value &&
                                    propertyInfo.CanWrite)
                                {
                                    #region Check if Types are same
                                    if (propertyInfo.PropertyType.FullName == columnvalue.GetType().FullName)
                                    {
                                        //Set value of particular property value in default instance of T.
                                        propertyInfo.SetValue(defaultInstance, columnvalue, null);
                                    }
                                    #endregion

                                    #region Check if Nullable then underlaying types are same
                                    else if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null
                                             && Nullable.GetUnderlyingType(propertyInfo.PropertyType).FullName == columnvalue.GetType().FullName)
                                    {
                                        //Set value of particular property value in default instance of T.
                                        propertyInfo.SetValue(defaultInstance, columnvalue, null);
                                    }
                                    #endregion

                                    #region Check if Date Time or Nullable DateTime Then try to parse date
                                    else if (propertyInfo.PropertyType == typeof(DateTime) ||
                                                           (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null &&
                                                            Nullable.GetUnderlyingType(propertyInfo.PropertyType) == typeof(DateTime)))
                                    {
                                        if (columnvalue.ToString() != "")
                                        {
                                            DateTime convertedDateTime;

                                            if (DateTime.TryParse(columnvalue.ToString(), culture, DateTimeStyles.AssumeLocal, out convertedDateTime))
                                            {
                                                propertyInfo.SetValue(defaultInstance, convertedDateTime, null);
                                            }
                                            else
                                            {
                                                throw new Exception(string.Format("Error while Converting to DateTime. PropertyName: {0} ColumnName: {1}", propertyInfo.Name, dc.ColumnName));
                                            }
                                        }
                                    }
                                    #endregion

                                    #region If Types are not same then try to change type
                                    else
                                    {
                                        try
                                        {
                                            //For Nullable 
                                            if (Nullable.GetUnderlyingType(propertyInfo.PropertyType) != null)
                                            {
                                                var convertedValue = Convert.ChangeType(columnvalue, Nullable.GetUnderlyingType(propertyInfo.PropertyType), culture);
                                                propertyInfo.SetValue(defaultInstance, convertedValue, null);
                                            }
                                            //For Simple
                                            else
                                            {
                                                var convertedValue = Convert.ChangeType(columnvalue, propertyInfo.PropertyType, culture);
                                                propertyInfo.SetValue(defaultInstance, convertedValue, null);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            // ex.LogAndMailException(string.Format("Error while Converting Data Type from the sheet. PropertyName: {0} ColumnName: {1}", propertyInfo.Name, dc.ColumnName));
                                            //handle your exception 
                                            throw ex;
                                        }
                                    }
                                    #endregion
                                }
                            }

                            //Now, create a class of the same type of T from default instance to add in list<T>.
                            T rtClass = (T)defaultInstance;

                            //Add the instance to List<T>.
                            lst.Add(rtClass);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return the final list of type T.
            return lst;
        }
        #endregion
        #endregion
    }

    public static class SQLParam
    {
        //public static void AddParameter(this SqlCommand command, string parameterName, object parameterValue, SqlDbType parameterSqlType)
        public static void AddParameter(this SqlCommand command, string parameterName, object parameterValue)
        {
            if (!parameterName.StartsWith("@"))
            {
                parameterName = "@" + parameterName;
            }
            command.Parameters.Add(new SqlParameter(parameterName, parameterValue));
        }

    }

    #region MyRegion
    public class SQLType
    {
        private static readonly Dictionary<string, Type> dbTypeDic;
        private static readonly Dictionary<string, string> defaultSizeDic;

        static SQLType()
        {
            // - SQL to C# type dictionary -
            dbTypeDic = new Dictionary<string, Type>();

            // - Guid -
            dbTypeDic.Add("uniqueidentifier", typeof(Guid));

            // - Boolean -
            dbTypeDic.Add("bit", typeof(bool));

            // - Integer -
            dbTypeDic.Add("tinyint", typeof(byte));
            dbTypeDic.Add("smallint", typeof(short));
            dbTypeDic.Add("int", typeof(int));
            dbTypeDic.Add("bigint", typeof(long));

            // - Decimal -
            dbTypeDic.Add("real", typeof(float));
            dbTypeDic.Add("float", typeof(double));
            dbTypeDic.Add("decimal", typeof(decimal));
            dbTypeDic.Add("numeric", typeof(decimal));

            // - Text -
            dbTypeDic.Add("text", typeof(string));
            dbTypeDic.Add("char", typeof(string));
            dbTypeDic.Add("nchar", typeof(string));
            dbTypeDic.Add("varchar", typeof(string));
            dbTypeDic.Add("nvarchar", typeof(string));

            // - Date -
            dbTypeDic.Add("datetime", typeof(DateTime));
            dbTypeDic.Add("smalldatetime", typeof(DateTime));

            // - Binary -
            dbTypeDic.Add("image", typeof(byte[]));
            dbTypeDic.Add("binary", typeof(byte[]));
            dbTypeDic.Add("varbinary", typeof(byte[]));

            // - C# type to SQL default size -
            defaultSizeDic = new Dictionary<string, string>();
            defaultSizeDic.Add("decimal", "9,2");
            defaultSizeDic.Add("varchar", "255");
            defaultSizeDic.Add("nvarchar", "255");
        }

        static public Type GetType(string sqlType)
        {
            return dbTypeDic[sqlType];
        }

        #region - Get SQLType -
        static public string GetSQLType(Type type)
        {
            string sqlTypeFound = null;
            foreach (string sqlType in dbTypeDic.Keys)
            {
                if (dbTypeDic[sqlType] == type)
                {
                    sqlTypeFound = sqlType;
                    break;
                }
            }
            return sqlTypeFound;
        }
        #endregion

        #region - Get default size type -
        static public string GetDefaultSize(string sqlType)
        {
            return defaultSizeDic[sqlType];
        }
        #endregion

        #region - Set Decimal As Single - (Workaround)
        public static void SetDecimalAsSingle()
        {
            Type singleType = typeof(float);
            dbTypeDic["real"] = singleType;
            dbTypeDic["float"] = singleType;
            dbTypeDic["decimal"] = singleType;
            dbTypeDic["numeric"] = singleType;
        }
        #endregion
    }
    #endregion



    /// <summary>
    /// --------------------- !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! -------------------------
    /// </summary>
    public static class UtilitiesSQL
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private static SqlConnection connection = new SqlConnection(conStr);

        public static List<KeyValuePair<string, SqlDbType>> dbTypeList = new List<KeyValuePair<string, SqlDbType>> {
            new KeyValuePair<string, SqlDbType>("int", SqlDbType.Int),
            new KeyValuePair<string, SqlDbType>("varchar", SqlDbType.VarChar),
            new KeyValuePair<string, SqlDbType>("bit", SqlDbType.Bit),
            new KeyValuePair<string, SqlDbType>("datetime", SqlDbType.DateTime),
            new KeyValuePair<string, SqlDbType>("decimal", SqlDbType.Decimal),
            new KeyValuePair<string, SqlDbType>("image", SqlDbType.Image)
            // Add any or all of the SqlDbTypes into this list.
        };

        public static SqlDbType FindInDbTypeList(string NativeDbType)
        {
            foreach (KeyValuePair<string, SqlDbType> _DbListItem in dbTypeList)
            {
                if (_DbListItem.Key == NativeDbType) return _DbListItem.Value;
            }

            return SqlDbType.Variant; // If all the possible choices are in the List, this is not good.
        }

        public static DataSet GetColumsAndTypes(string Table)
        {
            DataSet _DataSet;

            SqlCommand _Command = new SqlCommand("SELECT syscolumns.name, syscolumns.length, systypes.name FROM syscolumns " +
                "INNER JOIN systypes ON syscolumns.xtype = systypes.xtype " +
                "WHERE syscolumns.id = object_id('" + Table + "')", connection);

            SqlDataAdapter _Adapter = new SqlDataAdapter(_Command);
            _Adapter.Fill(_DataSet = new DataSet(), Table);

            return _DataSet;
        }

        public static SqlParameter[] GetParameters(string Table)
        {
            SqlParameter[] _Parameters = new SqlParameter[0];
            DataSet _DataSet = GetColumsAndTypes(Table);

            foreach (DataRow _Row in _DataSet.Tables[0].Rows)
            {
                Array.Resize(ref _Parameters, _Parameters.Length + 1);
                _Parameters[_Parameters.Length - 1] = new SqlParameter("@" + _Row[0].ToString(), FindInDbTypeList(_Row[2].ToString()),
                    Convert.ToInt32(_Row[1]), _Row[0].ToString());
            }

            return _Parameters;
        }

        public static SqlCommand CreateInsertCommand(string Table, string CommandString, string IdentityColumn, bool IsStoredProcedure)
        {
            SqlCommand _Command = new SqlCommand(CommandString, connection);
            //if (IsStoredProcedure) _Command.CommandType = CommandType.StoredProcedure;
            _Command.CommandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
            _Command.Parameters.AddRange(GetParameters(Table));
            _Command.Parameters["@" + IdentityColumn].Direction = ParameterDirection.Output;

            return _Command;
        }

        public static SqlCommand CreateUpdateCommand(string Table, string CommandString, bool IsStoredProcedure)
        {
            SqlCommand _Command = new SqlCommand(CommandString, connection);
            //if (IsStoredProcedure) _Command.CommandType = CommandType.StoredProcedure;
            _Command.CommandType = IsStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
            _Command.Parameters.AddRange(GetParameters(Table));
            //_Command.Parameters.AddRange(GetParameters(Table));

            return _Command;
        }

        public static SqlCommand CreateDeleteCommand(string Table, string IdentityColumn)
        {
            SqlCommand _Command = new SqlCommand("DELETE FROM " + Table + " WHERE " + IdentityColumn + " = @" + IdentityColumn, connection);
            _Command.Parameters.Add(new SqlParameter("@" + IdentityColumn, SqlDbType.Int, sizeof(int), IdentityColumn));

            return _Command;
        }
    }

    #region --- !!!!!!!!!!!!  ---
    //public class GenericDataModel
    //{
    //    public GenericDataModel(string connectionString)
    //    {
    //        this.connectionString = connectionString;
    //    }

    //    /// <summary>
    //    /// Connection string for the database
    //    /// </summary>
    //    private readonly String connectionString;

    //    /// <summary>
    //    /// Calls a stored procedure with a single table as the parameter
    //    /// </summary>
    //    /// <param name="storedProcedureName">Name of the stored procedure to call (ie integration.UpsertTestOrderTrackingNum)</param>
    //    /// <param name="parameterName">Name of the parameter (ie "@TestOrderTrackingNumObjects")</param>
    //    /// <param name="sprocParamObjects">Parameter for the sproc</param>
    //    /// <param name="tableParamTypeName">name of the table valued parameter.  (ie. integration.TestOrderTrackingNumTableType)</param>
    //    /// <param name="connection">The connection to use.  This is optional and is there to allow transactions.</param>
    //    public void ExecuteTableParamedProcedure<T>(string storedProcedureName, string parameterName, string tableParamTypeName, IEnumerable<T> sprocParamObjects, SqlConnection connection = null)
    //    {
    //        SqlConnection adHocConnection = null;
    //        if (connection == null)
    //        {
    //            connection = new SqlConnection(connectionString);
    //            connection.Open();
    //            adHocConnection = connection;
    //        }

    //        using (adHocConnection)
    //        {
    //            using (SqlCommand command = connection.CreateCommand())
    //            {
    //                command.CommandText = storedProcedureName;
    //                command.CommandType = CommandType.StoredProcedure;

    //                SqlParameter parameter = command.Parameters.AddWithValue(parameterName, CreateDataTable(sprocParamObjects));
    //                parameter.SqlDbType = SqlDbType.Structured;
    //                parameter.TypeName = tableParamTypeName;

    //                command.ExecuteNonQuery();
    //            }
    //        }

    //    }


    //    /// <summary>
    //    /// Calls a list of sprocs in a transaction.
    //    /// Example Usage: CallSprocsInTransaction(connection=>model.SprocToCall(paramObjects, connection), connection=>model.Sproc2ToCall(param2Objects, connection...);
    //    /// </summary>
    //    /// <param name="sprocsToCall">List of sprocs to call.</param>
    //    public void CallSprocsInTransaction(params Action<SqlConnection>[] sprocsToCall)
    //    {
    //        // Create a new connection that will run the transaction
    //        using (SqlConnection connection = new SqlConnection(connectionString))
    //        {
    //            // Create a transaction to wrap our calls in
    //            var transaction = connection.BeginTransaction();
    //            try
    //            {
    //                // Call each sproc that was passed in.
    //                foreach (var action in sprocsToCall)
    //                {
    //                    // We send the connection to the action so that it will all take place on the same connection.
    //                    // If we don't then if we do a rollback, the rollback will be for a connection that did not run the sprocs.
    //                    action(connection);
    //                }
    //            }
    //            catch (Exception e)
    //            {
    //                // If we failed then roll back.
    //                // The idea here is that the caller wants all the sprocs to succeed or none of them.
    //                transaction.Rollback();
    //                throw;
    //            }
    //            // If everything was good, then commit our calls.
    //            transaction.Commit();
    //        }
    //    }



    //    /// <summary>
    //    /// Create the data table to be sent up to SQL Server
    //    /// </summary>
    //    /// <typeparam name="T">Type of object to be created</typeparam>
    //    /// <param name="sprocParamObjects">The data to be sent in the table param to SQL Server</param>
    //    /// <returns></returns>
    //    private static DataTable CreateDataTable<T>(IEnumerable<T> sprocParamObjects)
    //    {
    //        DataTable table = new DataTable();

    //        Type type = typeof(T);
    //        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

    //        foreach (PropertyInfo property in properties)
    //        {
    //            table.Columns.Add(property.Name, property.PropertyType);
    //        }

    //        foreach (var sprocParamObject in sprocParamObjects)
    //        {
    //            var propertyValues = new List<object>();
    //            foreach (PropertyInfo property in properties)
    //            {
    //                propertyValues.Add(property.GetValue(sprocParamObject, null));
    //            }
    //            table.Rows.Add(propertyValues.ToArray());

    //            Console.WriteLine(table);
    //        }
    //        return table;
    //    }


    //}
    #endregion

    #region --  *******************************************************************************************************  --
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        public string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = null;
            //using (var context = new Entities())
            //{
            //    IQueryable<T> dbQuery = context.Set<T>();

            //    //Apply eager loading
            //    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            //        dbQuery = dbQuery.Include<T, object>(navigationProperty);

            //    list = dbQuery
            //        .AsNoTracking()
            //        .ToList<T>();
            //}

            using (SqlConnection sqlConnection = new SqlConnection(conStr))
            {
                //System.Linq.IQueryable<T> dbQuery = context.Set<T>();

                ////Apply eager loading
                //foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                //    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                //list = dbQuery.AsNoTracking().ToList<T>();
            }
            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = null;
            //using (var context = new Entities())
            //{
            //    IQueryable<T> dbQuery = context.Set<T>();

            //    //Apply eager loading
            //    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            //        dbQuery = dbQuery.Include<T, object>(navigationProperty);

            //    list = dbQuery
            //        .AsNoTracking()
            //        .Where(where)
            //        .ToList<T>();
            //}
            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            //using (var context = new Entities())
            //{
            //    IQueryable<T> dbQuery = context.Set<T>();

            //    //Apply eager loading
            //    foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            //        dbQuery = dbQuery.Include<T, object>(navigationProperty);

            //    item = dbQuery
            //        .AsNoTracking() //Don't track any changes for the selected item
            //        .FirstOrDefault(where); //Apply where clause
            //}
            return item;
        }

        /* rest of code omitted */
    }

    /// <summary>
    /// --   --
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        //void Add(params T[] items);
        //void Update(params T[] items);
        //void Remove(params T[] items);
    }

    #endregion

    #region --  ******************************************************************  --
    /* Entity classes implementing IEntity */
    public partial class Department //: IEntity
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        //public EntityState EntityState { get; set; }
    }


    public partial class Employee //: IEntity
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual Department Department { get; set; }

        //public EntityState EntityState { get; set; }
    }

    public interface IBusinessLayer
    {
        IList<Department> GetAllDepartments();
        Department GetDepartmentByName(string departmentName);
        void AddDepartment(params Department[] departments);
        void UpdateDepartment(params Department[] departments);
        void RemoveDepartment(params Department[] departments);

        IList<Employee> GetEmployeesByDepartmentName(string departmentName);
        void AddEmployee(Employee employee);
        void UpdateEmploee(Employee employee);
        void RemoveEmployee(Employee employee);
    }

    public class BusinessLayer : IBusinessLayer
    {
        public void AddDepartment(params Department[] departments)
        {
            throw new NotImplementedException();
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IList<Department> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        public Department GetDepartmentByName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IList<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public void RemoveDepartment(params Department[] departments)
        {
            throw new NotImplementedException();
        }

        public void RemoveEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(params Department[] departments)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmploee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}

public class ConsumeIRepository : IRepository<Person>
{
    public void Add(Person entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Person entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetAll()
    {
        throw new NotImplementedException();
    }

    public Person GetByID(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Person entity)
    {
        throw new NotImplementedException();
    }
}
