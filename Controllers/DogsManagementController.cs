using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DatabaseConnectionProvider.Controllers
{
    [Route("dogs")]
    public class DogsManagementController
    {
        [HttpGet]
        [Route("ResetDataBase")]
        public string GetResetDataBase()
        {
            var database = new SqlDatabaseConnector();
            database.Connector.Open();
            string resetDatabaseQuery = "CREATE DATABASE IF NOT EXISTS Swierzaki; USE Swierzaki; CREATE TABLE IF NOT EXISTS Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); DROP TABLE IF EXISTS Dogs; CREATE TABLE Dogs(DogID int NOT NULL PRIMARY KEY AUTO_INCREMENT, OwnerID int, Name varchar(255), Race varchar(255), Notes varchar(255)); ALTER TABLE Dogs ADD FOREIGN KEY(OwnerID) REFERENCES Persons(PersonID); INSERT INTO Dogs VALUES(null, 1, 'Burek', 'Mieszaniec', 'AAA'); INSERT INTO Dogs VALUES(null, 2, 'Mialek', 'Quint', 'BBB'); ";
            var data = new DataSet();
            var recreate = new MySqlCommand(resetDatabaseQuery, database.Connector).ExecuteNonQuery();

            database.Connector.Close();

            return "Zresetowano bazę danych";
        }

        [HttpGet]
        [Route("SelectAllDogsOfUser")]
        public string CheckUserPassword(string user)
        {
            var database = new SqlDatabaseConnector();
            database.Connector.Open();
            string checkPasswordQuery = $"USE Swierzaki; CREATE TABLE IF NOT EXISTS Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); CREATE TABLE IF NOT EXISTS Dogs(DogID int NOT NULL PRIMARY KEY AUTO_INCREMENT, OwnerID int, Name varchar(255), Race varchar(255), Notes varchar(255)); ALTER TABLE Dogs ADD FOREIGN KEY(OwnerID) REFERENCES Persons(PersonID); SELECT * FROM Dogs WHERE OwnerID IN(SELECT PersonID FROM Persons WHERE Nick = '{user}')";
            var cmd = new MySqlCommand(checkPasswordQuery, database.Connector);
            MySqlDataReader rdr = cmd.ExecuteReader();

            var buff = new List<object>();
            while (rdr.Read())
            {
                buff.Add(new {
                    DogId = rdr["DogID"],
                    OwnerId = rdr["OwnerID"],
                    Name = rdr["Name"],
                    Race = rdr["Race"],
                    Notes = rdr["Notes"]
                });
            }

            var returnString = JsonConvert.SerializeObject(buff);

            rdr.Close();
            database.Connector.Close();

            return returnString;
        }

        [HttpGet]
        [Route("AddDog")]
        public bool AddDog(int ownerId ,string dogName, string dogRace, string notes)
        {
            var database = new SqlDatabaseConnector();
            database.Connector.Open();
            string addUserQuery = $"USE Swierzaki; CREATE TABLE IF NOT EXISTS Persons (PersonID int NOT NULL PRIMARY KEY AUTO_INCREMENT, Nick varchar(255), Email varchar(255), Password varchar(255) ); CREATE TABLE IF NOT EXISTS Dogs(DogID int NOT NULL PRIMARY KEY AUTO_INCREMENT, OwnerID int, Name varchar(255), Race varchar(255), Notes varchar(255)); ALTER TABLE Dogs ADD FOREIGN KEY(OwnerID) REFERENCES Persons(PersonID); INSERT INTO Dogs VALUES(null, '{ownerId}','{dogName}', '{dogRace}', '{notes}')";
            var data = new DataSet();
            var recreate = new MySqlCommand(addUserQuery, database.Connector).ExecuteNonQuery();
            database.Connector.Close();

            return true;
        }
    }
}
