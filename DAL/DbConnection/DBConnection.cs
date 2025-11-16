using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DbConnect
{
    public class DBConnection
    {
        private readonly string connectionString = AppConstants.DBConnectDocker;
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
