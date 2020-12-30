using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AboutVintageApiService.DBUtils
{
    public class Utils
    {
        private const string ConnectionString = "";
        private static SqlConnection _sqlConnection;
        public static SqlConnection GetConnection()
        {
            return _sqlConnection == null ? new SqlConnection(ConnectionString) : _sqlConnection;
        }
    }
}
