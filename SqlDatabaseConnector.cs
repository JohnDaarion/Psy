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
                "Server=localhost;" +
                "Port=3307;" +
                "Uid=root;" +
                "Pwd=usbw;"
            };       
        }
    }
}
