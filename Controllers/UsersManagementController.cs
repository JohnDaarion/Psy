using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DatabaseConnectionProvider.Controllers
{
    [Route("user")]
    public class UsersManagementController : Controller
    {
        [HttpGet]
        [Route("ResetDataBase")]
        public string GetResetDataBase()
        {
            try
            {
                var database = new SqlDatabaseConnector();
                database.Connector.Open();
                string resetDatabaseQuery = "CREATE DATABASE IF NOT EXISTS Swierzaki; USE Swierzaki; DROP TABLE IF EXISTS Persons; CREATE TABLE Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); INSERT INTO Persons VALUES(null, 'Muffy', 'ala@wp.pl', 'AAA'); INSERT INTO Persons VALUES(null, 'Admin', 'kot@wp.pl', 'AAA');";
                var data = new DataSet();
                var recreate = new MySqlCommand(resetDatabaseQuery, database.Connector).ExecuteNonQuery();

                database.Connector.Close();
            }
            catch (Exception e)
            {
                return "Baza wywaliła";
            }

            return "Zresetowano bazę danych";
        }

        [HttpGet]
        [Route("CheckUserPassword")]
        public bool CheckUserPassword(string user, string password)
        {
            var database = new SqlDatabaseConnector();
            database.Connector.Open();
            string checkPasswordQuery = $"USE Swierzaki; CREATE TABLE IF NOT EXISTS Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); select PersonID from Persons where Nick = '{user}' and Password = '{password}'";
            var cmd = new MySqlCommand(checkPasswordQuery, database.Connector);
            MySqlDataReader rdr = cmd.ExecuteReader();

            string buff = null;
            while (rdr.Read())
            {
                buff = rdr[rdr.GetName(0)].ToString();
            }

            rdr.Close();
            database.Connector.Close();

            return !String.IsNullOrEmpty(buff);
        }

        [HttpGet]
        [Route("AddUser")]
        public bool AddUser(string user, string email, string password)
        {
            var database = new SqlDatabaseConnector();
            database.Connector.Open();
            string addUserQuery = $"USE Swierzaki; CREATE TABLE IF NOT EXISTS Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); INSERT INTO Persons VALUES(null, '{user}', '{email}', '{password}')";
            var data = new DataSet();
            var recreate = new MySqlCommand(addUserQuery, database.Connector).ExecuteNonQuery();
            database.Connector.Close();

            return true;
        }
    }
}
