using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Helpers.Interfaces
{
    public interface IDatabaseHelper
    {
        void ExecuteNonQuery(DatabaseCommandInfo data);
        DataSet GetDataSet(DatabaseCommandInfo data);
        DataTable GetDataTable(DatabaseCommandInfo data);
    }
}
