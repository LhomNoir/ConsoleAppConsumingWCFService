using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyWcfService.Models;
using MyWcfService;
using MyWcfService.NewBusiness;
using System.Reflection;
using MyWcfService.Helpers;
using MyWcfService.Helpers.SqlHelpers;

namespace MyWcfService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1, IDisposable
    {
        bool _isDisposed;

        // --   --
        private static string ConnectionString = null;
        //@"Data Source=DESKTOP-AJN25BM\SQLEXPRESS;Initial Catalog=WCF;Integrated Security=True;Asynchronous Processing=True;MultipleActiveResultSets=True;Connect Timeout=300;Encrypt=True;TrustServerCertificate=True";

        #region -- SQL --
        SqlConnection sqlConnection;
        SqlCommand sqlCommand;

        SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public bool IsDisposed => throw new NotImplementedException();

        void BuildDBconnection()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = @"DESKTOP-AJN25BM\SQLEXPRESS";
            sqlConnectionStringBuilder.InitialCatalog = "WCF";
            sqlConnectionStringBuilder.Encrypt = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            sqlConnectionStringBuilder.ConnectTimeout = 300;
            sqlConnectionStringBuilder.AsynchronousProcessing = true;
            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.IntegratedSecurity = true;

            sqlConnection = new SqlConnection(sqlConnectionStringBuilder.ToString());
            sqlCommand = sqlConnection.CreateCommand();
        }

        void BuildConnectionString()
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = @"DESKTOP-AJN25BM\SQLEXPRESS";
            sqlConnectionStringBuilder.InitialCatalog = "WCF";
            sqlConnectionStringBuilder.Encrypt = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            sqlConnectionStringBuilder.ConnectTimeout = 300;
            sqlConnectionStringBuilder.AsynchronousProcessing = true;
            sqlConnectionStringBuilder.MultipleActiveResultSets = true;
            sqlConnectionStringBuilder.IntegratedSecurity = true;
        }

        public Service1()
        {
            BuildConnectionString();

            BuildDBconnection();

            ConnectionString = sqlConnectionStringBuilder.ToString();
        }
        #endregion

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public IEnumerable<Person> GetAllPersons()
        {
            //Manage();

            GetPersonList();

            List<Person> peoples = new List<Person>();
            peoples.Clear();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string sqlSelectQuery = "SELECT DISTINCT Id, Name, Age FROM TPerson1";

                using (SqlCommand sqlCommand = new SqlCommand(sqlSelectQuery, sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.Text;

                    if (sqlConnection.State != ConnectionState.Open)
                        sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        peoples.Add(new Person()
                        {
                            Id = sqlDataReader[0] != null ? Convert.ToInt32(sqlDataReader[0]) : 0,
                            Name = sqlDataReader[1].ToString(),
                            Age = Convert.ToInt32(sqlDataReader[2])
                        });
                    }
                }
            }
            return peoples;
        }

        public List<Person> GetPersonList()
        {
            List<Person> personlist = new List<Person>();

            string sqlSelectQuery = "SELECT DISTINCT Id, Name, Age FROM TPerson1";

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            using (SqlCommand sqlCommand = new SqlCommand(sqlSelectQuery, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.Text;
                SqlDataAdapter sd = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();

                sqlConnection.Open();
                sd.Fill(dt);

                #region -- Convert DataTable to List using Generic Method --
                var peoples = ConvertDataClass.ConvertDataTable<Person>(dt);
                #endregion

                foreach (DataRow dr in dt.Rows)
                {
                    personlist.Add(
                        new Person
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Name = Convert.ToString(dr["Name"]),
                            Age = Convert.ToInt32(dr["Age"])
                        });
                }
                return personlist;
            }
        }

        public IList<Person> GetPersons()
        {
            Person newPerson;
            List<Person> persons = new List<Person>();
            persons.Clear();
            try
            {
                sqlCommand.CommandText = "SELECT DISTINCT Id, Name, Age FROM TPerson1";
                sqlCommand.CommandType = CommandType.Text;

                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    persons.Add(new Person()
                    {
                        Id = sqlDataReader[0] != null ? Convert.ToInt32(sqlDataReader[0]) : 0,
                        Name = sqlDataReader[1].ToString(),
                        Age = Convert.ToInt32(sqlDataReader[2])
                    });
                }
                return persons;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public int InsertPerson(Person person)
        {
            try
            {
                sqlCommand.CommandText = "INSERT INTO TPerson1 VALUES(@Name, @Age)";

                sqlCommand.Parameters.AddWithValue("Name", person.Name);
                sqlCommand.Parameters.AddWithValue("Age", person.Age);

                #region --   --
                //sqlCommand.Parameters.Add("@UserName", System.Data.SqlDbType.VarChar);
                //sqlCommand.Parameters.Add("@Date", System.Data.SqlDbType.DateTime);
                //sqlCommand.Parameters.Add("@TableName", System.Data.SqlDbType.VarChar);
                //sqlCommand.Parameters.Add("@ColumnName", System.Data.SqlDbType.VarChar);
                #endregion

                sqlCommand.CommandType = CommandType.Text;
                sqlConnection.Open();

                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public Person GetPerson(Person person)
        {
            Person newPerson = new Person();
            try
            {
                sqlCommand.CommandText = "SELECT DISTINCT Id, Name, Age FROM TPerson1 WHERE Id = @Id";

                sqlCommand.Parameters.AddWithValue("Id", person.Id);

                sqlCommand.CommandType = CommandType.Text;
                sqlConnection.Open();

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    newPerson.Id = sqlDataReader[0] != null ? Convert.ToInt32(sqlDataReader[0]) : 0;
                    newPerson.Name = sqlDataReader[1].ToString();
                    newPerson.Age = Convert.ToInt32(sqlDataReader[2]);
                }

                return person;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public int UpdatePerson(Person person)
        {
            try
            {
                var result = 0;
                var findPerson = GetPersons().FirstOrDefault(p => p.Id == person.Id);

                var res = HasAlready(findPerson.Name);

                if (findPerson != null)
                {
                    sqlCommand.CommandText = "UPDATE TPerson1 SET Name = @Name, Age = @Age WHERE Id = @Id";

                    sqlCommand.Parameters.AddWithValue("Id", person.Id);
                    sqlCommand.Parameters.AddWithValue("Name", person.Name);
                    sqlCommand.Parameters.AddWithValue("Age", person.Age);

                    sqlCommand.CommandType = CommandType.Text;
                    sqlConnection.Open();

                    result = sqlCommand.ExecuteNonQuery();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public int DeltePerson(Person person)
        {
            try
            {
                sqlCommand.CommandText = "DELETE TPerson1 WHERE Id = @Id";

                sqlCommand.Parameters.AddWithValue("Id", person.Id);

                sqlCommand.CommandType = System.Data.CommandType.Text;
                sqlConnection.Open();

                return sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        #region --   --
        public bool HasAlready(string name)
        {
            try
            {
                using (SqlConnection slqConnection = new SqlConnection(ConnectionString))
                {
                    TextWithParams();
                    ;

                    string sqlQuery = "SELECT count(*) FROM TPerson1 WHERE Name = @name";
                    SqlCommand sql = new SqlCommand(sqlQuery, slqConnection);
                    sql.Parameters.Add("@name", SqlDbType.VarChar);
                    sql.Parameters["@name"].Value = name.Trim();

                    slqConnection.Open();
                    int val = (Int32)sql.ExecuteScalar();

                    #region ---   ---
                    //int[] ages = { 33, 35, 22 };
                    //var cmd = new SqlCommand("SELECT * FROM TPerson1 WHERE Age IN ({Age})", slqConnection);
                    //cmd.AddArrayParameters("Age", new int[] { 0, 03, 305 });
                    //int val  = (Int32)cmd.ExecuteScalar();
                    #endregion

                    if (val > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        #endregion

        #region ---  ---
        public void TextWithParams()
        {
            int[] items = { 33, 35, 22 };
            try
            {

                var parameters = new string[items.Length];
                var cmd = new SqlCommand();
                for (int i = 0; i < items.Length; i++)
                {
                    parameters[i] = string.Format("@Age{0}", i);
                    cmd.Parameters.AddWithValue(parameters[i], items[i]);
                }

                cmd.CommandText = string.Format("SELECT * from TPerson1 WHERE Age IN ({0})", string.Join(", ", parameters));
                cmd.Connection = new SqlConnection(ConnectionString);
                cmd.Connection.Open();
                int val = (Int32)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region --   --
        private void PageLoad()
        {
            DataSet ds = new DataSet();
            String strAppend = "";
            String strNames = "";
            int index = 1;
            String paramName = "";
            String[] strArrayNames;
            strNames = "John,Rohan,Krist,Bronk,Peter";
            strArrayNames = strNames.Split(',');
            SqlCommand cmd = new SqlCommand();
            foreach (String item in strArrayNames)
            {
                paramName = "@idParam" + index;
                cmd.Parameters.AddWithValue(paramName, item); //Making individual parameters for every name  
                strAppend += paramName + ",";
                index += 1;
            }
            strAppend = strAppend.ToString().Remove(strAppend.LastIndexOf(","), 1); //Remove the last comma  
            cmd.CommandText = "select * from tblemployee where ename in(" + strAppend + ")";
            //ds = RetrieveSqlData(cmd);
        }

        private void AZERTY()
        {
            string query = "UPDATE [guitarBrands] SET type = @type, name = @name, image = @image WHERE id = @id";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                     // open the connection, execute, etc
                     List<SqlParameter> p = new List<SqlParameter>();
                     p.Add(new SqlParameter("@type", "newType.Text"));
                     p.Add(new SqlParameter("@name", "newName.Text"));
                     p.Add(new SqlParameter("@image", "newImage.Text"));
                     p.Add(new SqlParameter("@id", "id"));

                     connection.Open();
                     //GetExample(command, p.ToArray());
                     command.ExecuteNonQuery();
                     command.Parameters.Clear();
                }
                catch
                {
                     // log and handle exception(s)
                }
            }
        }

        
        #endregion

        #region -- New Business --
        private void Manage()
        {
            Exception exError = null;
            
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                Person_DataMapper user_mapper = new Person_DataMapper(connection);
                List<Person> users = user_mapper.Select(out exError);

                if (exError != null)
                {
                    //do something with the users list
                }
                else
                    //Report and handle the exception
                    Console.WriteLine("Unable to get list of users, " + exError.Message);
            }
        }
        #endregion

        #region -- Dispose Ressources --
        //protected override void Dispose(bool disposing)
        //{
        //    //if (!_isDisposed)
        //    //{
        //    //    if (disposing)
        //    //    {
        //    //        // free other managed objects that implement
        //    //        // IDisposable only
        //    //    }
        //    //    // release any unmanaged objects
        //    //    // set object references to null
        //    //    _isDisposed = true;
        //    //}
        //    //base.Dispose(disposing);
        //}

        public void Dispose()
        {
            // 
        }

        void IDisposable.Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only
                }
                // release any unmanaged objects
                // set object references to null
                _isDisposed = true;
            }
            //base.Dispose(disposing);
        }
        #endregion

    }
}
