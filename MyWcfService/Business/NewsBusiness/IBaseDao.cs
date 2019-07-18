using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWcfService.Business.NewsBusiness
{
    public interface IBaseDao
    {
        void AddParameter(string parameterName, object parameterValue);

        void ExecuteNonQuery();

        //void ReadAll(Action<DynamicDataReader> readerActionBlock);

        void SetSqlText(string sql);
    }


}
