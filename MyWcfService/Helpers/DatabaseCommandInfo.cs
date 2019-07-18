using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Helpers
{
    public class DatabaseCommandInfo
    {
        public string StoredProcName { get; private set; }
        public SqlParameter[] Parameters { get; private set; }
        public string[] TableNames { get; private set; }
        public LoadOption Option { get; private set; }
        public CommandType CommandType { get; set; }

        public DatabaseCommandInfo(string storeProcName, SqlParameter[] spParams)
        {
            StoredProcName = storeProcName;
            Parameters = spParams;
            CommandType = CommandType.StoredProcedure;
        }


        public DatabaseCommandInfo(string storeProcName, SqlParameter[] spParams, string[] tableNames)
        {
            StoredProcName = storeProcName;
            Parameters = spParams;
            TableNames = tableNames;
            Option = LoadOption.OverwriteChanges;
            CommandType = CommandType.StoredProcedure;
        }
    }
}
