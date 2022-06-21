using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Avigma.Repository.Lib
{
    public class Avigma_connectionString
    {
        public static string connectionString;

        public static SqlConnection GetConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Conn_dBcon"].ToString();

            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
       
    }
}