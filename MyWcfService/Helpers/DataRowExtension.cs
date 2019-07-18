using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Helpers
{
    public static class DataRowExtension
    {
        public static T GetValue<T>(this DataRow row, string columnName)
        {
            if (row != null && row.Table.Columns.Count > 0 && row[columnName] != DBNull.Value)
            {
                return (T)Convert.ChangeType(row[columnName], typeof(T));
            }
            return default(T);
        }

        public static T? GetNullableValue<T>(this DataRow row, string columnName) where T : struct
        {
            if (DBNull.Value.Equals(row[columnName]))
            {
                return null;
            }
            return (T)Convert.ChangeType(row[columnName], typeof(T));
        }
    }
}
