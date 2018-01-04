using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DatabaseConnectionProvider
{
    public class SqlDatabaseConnector
    {
        public readonly MySqlConnection Connector;

        public SqlDatabaseConnector()
        {
            Connector = new MySqlConnection
            {
                ConnectionString =
                "Server=psymysql;" +
                "Port=3306;" +
                "Uid=root;" +
                "Pwd=usbw;"
            };       
        }
    }
}
