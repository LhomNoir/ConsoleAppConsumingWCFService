using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Business.NewsBusiness
{
    public abstract class BaseDao //: IBaseDao
    {
        public DbProviderFactory Factory { get { return _factory; } }

        public IDbCommand Command { get; private set; }

        private readonly DbProviderFactory _factory;

        protected BaseDao(string providerName)
            : this(DbProviderFactories.GetFactory(providerName))
        {
        }

        protected BaseDao(DbProviderFactory factory)
        {
            _factory = factory;
        }

        //public void ReadAll(Action<DynamicDataReader> readerActionBlock)
        //{
        //    using (var dynamicReader = new DynamicDataReader(Command.ExecuteReader()))
        //    {
        //        while (dynamicReader.Read())
        //        {
        //            readerActionBlock(dynamicReader);
        //        }
        //    }
        //}

        public void SetSqlText(string sql)
        {
            Command.CommandText = sql;
        }

        public void AddParameter(string parameterName, object parameterValue)
        {
            var parameter = Command.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;

            Command.Parameters.Add(parameter);
        }

        public void ExecuteNonQuery()
        {
            int rowsAffected = Command.ExecuteNonQuery();
            if (rowsAffected < 1)
            {
                throw new Exception("No Rows Affected");
            }
        }

        internal void SafelyUse(Action<BaseDao> commandBlock)
        {
            UsingConnection((con) =>
            {
                using (Command = con.CreateCommand())
                {
                    Command.CommandTimeout = 5 * 60;
                    Command.CommandType = CommandType.Text;
                    try
                    {
                        commandBlock(this);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("An exception occured when Creating a command for a database", ex);
                    }
                }
            });
        }

        private void UsingConnection(Action<IDbConnection> connectionBlock)
        {
            try
            {
                var connectionstringBuilder = Factory.CreateConnectionStringBuilder();
                AdjustConnectionString(connectionstringBuilder);
                using (var connection = Factory.CreateConnection())
                {
                    if (connection == null) throw new Exception(string.Format("No connection could be made with the connection string {0}", connectionstringBuilder.ConnectionString));
                    connection.ConnectionString = connectionstringBuilder.ConnectionString;
                    connection.Open();
                    connectionBlock(connection);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An exception was made when creating a connection to a database", ex);
            }
        }

        protected abstract void AdjustConnectionString(DbConnectionStringBuilder connectionStringBuilder);
    }

    #region --   --
    internal class IbmDb2Dao : BaseDao
    {
        private readonly string _databaseName;
        private readonly string _serverName;

        internal IbmDb2Dao(string serverName, string databaseName)
            : base("IBM.Data.DB2")
        {
            _serverName = serverName;
            _databaseName = databaseName;
        }

        protected override void AdjustConnectionString(DbConnectionStringBuilder connectionStringBuilder)
        {
            connectionStringBuilder.Add("Server", _serverName);
            connectionStringBuilder.Add("Database", _databaseName);
            connectionStringBuilder.Add("CurrentSchema", "userid");
        }
    }

    internal class MsSqlDao : BaseDao
    {
        private readonly string _databaseName;
        private readonly string _serverName;

        internal MsSqlDao(string serverName, string databaseName)
            : base("System.Data.SqlClient")
        {
            _serverName = serverName;
            _databaseName = databaseName;
        }

        protected override void AdjustConnectionString(DbConnectionStringBuilder connectionStringBuilder)
        {
            connectionStringBuilder.Add("Data Source", _serverName);
            connectionStringBuilder.Add("Initial Catalog", _databaseName);
            connectionStringBuilder.Add("Integrated Security", "SSPI");
        }
    }
    #endregion
}
