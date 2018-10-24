using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using MySql.Data;
using System.Collections;
using System.Configuration;

namespace WebApplication1
{

    public class PersonPersistence
    {
        private readonly string myConnectionString = ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        public long savePerson(Person personToSave)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    String sqlString = "INSERT INTO tblpersonnel (FirstName, LastName, PayRate, StartDate, EndDate) VALUES ('" + personToSave.FirstName + "','" + personToSave.LastName + "'," + personToSave.PayRate + ",'" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                    cmd.ExecuteNonQuery();
                    long id = cmd.LastInsertedId;
                    return id;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw ex;
            }
        }

        public Person getPerson(long ID)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    Person p = new Person();
                    MySql.Data.MySqlClient.MySqlDataReader Reader = null;

                    string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        p.ID = Reader.GetInt32(0);
                        p.FirstName = Reader.GetString(1);
                        p.LastName = Reader.GetString(2);
                        p.PayRate = Reader.GetFloat(3);
                        p.StartDate = Reader.GetDateTime(4);
                        p.EndDate = Reader.GetDateTime(5);

                        return p;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw ex;
            }
        }

        public ArrayList getPersons()
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    ArrayList persons = new ArrayList();

                    MySql.Data.MySqlClient.MySqlDataReader Reader = null;

                    string sqlString = "SELECT * FROM tblpersonnel";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    Reader = cmd.ExecuteReader();
                    while (Reader.Read())
                    {
                        Person p = new Person();
                        p.ID = Reader.GetInt32(0);
                        p.FirstName = Reader.GetString(1);
                        p.LastName = Reader.GetString(2);
                        p.PayRate = Reader.GetFloat(3);
                        p.StartDate = Reader.GetDateTime(4);
                        p.EndDate = Reader.GetDateTime(5);

                        persons.Add(p);
                    }
                    return persons;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw ex;
            }
        }

        public bool deletePerson(long ID)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    Person p = new Person();
                    MySql.Data.MySqlClient.MySqlDataReader Reader = null;

                    string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        Reader.Close();

                        sqlString = "DELETE FROM tblpersonnel WHERE ID = " + ID.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw ex;
            }
        }

        public bool updatePerson(long ID, Person personToSave)
        {
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(myConnectionString))
                {
                    conn.Open();
                    MySql.Data.MySqlClient.MySqlDataReader Reader = null;

                    string sqlString = "SELECT * FROM tblpersonnel WHERE ID = " + ID.ToString();
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

                    Reader = cmd.ExecuteReader();
                    if (Reader.Read())
                    {
                        Reader.Close();

                        sqlString = "UPDATE tblpersonnel SET FirstName='" + personToSave.FirstName + "', LastName ='" + personToSave.LastName + "' , PayRate =" + personToSave.PayRate + " , StartDate='" + personToSave.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + "', EndDate='" + personToSave.EndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID = " + ID.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                throw ex;
            }
        }

    }

}