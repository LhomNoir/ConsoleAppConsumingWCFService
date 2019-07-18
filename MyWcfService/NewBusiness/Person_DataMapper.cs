using MyWcfService.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.NewBusiness
{
    public class Person_DataMapper : DataMapper<Person>
    {
        public Person_DataMapper(SqlConnection connection) 
            : base(connection)
        {

        }

        public override bool Create(Person instance, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int ID, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(Person instance, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override Person Read(int ID, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override Person Read(Person instance, out Exception exError)
        {
            throw new NotImplementedException();
        }

        public override List<Person> Select(out Exception exError)
        {
            List<Person> returnValue = new List<Person>();
            exError = null;
            Person newPerson = null;
            returnValue.Clear();

            try
            {
                if (this.Connection.State != ConnectionState.Open)
                    this.Connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT DISTINCT Id, Name, Age FROM TPerson1", (SqlConnection)this.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //newPerson = new Person()
                            //{
                            //    Id = reader[0] != null ? Convert.ToInt32(reader[0]) : 0,
                            //    Name = reader.GetString(1),
                            //    Age = reader.GetInt32(2)
                            //};

                            returnValue.Add(new Person()
                            {
                                Id = reader[0] != null ? Convert.ToInt32(reader[0]) : 0,
                                Name = reader.GetString(1),
                                Age = reader.GetInt32(2)
                            });
                        }
                    }
                }
            }
            catch (InvalidOperationException invalid)
            {
                exError = invalid;
            }
            catch (Exception ex)
            {
                exError = ex;
            }

            return returnValue;
        }

        public override bool Update(Person instance, out Exception exError)
        {
            throw new NotImplementedException();
        }
    }
}
