using System;
using System.Data.SqlClient;

namespace CarHub
{
    class DataConnection
    {
        public static string Seller { get; set; }
        public static SqlConnection DataCon { get; set; }
        public static void ConnectionDB(string ip, string dbName, string user, string password)
        {
            string connectionString = "Server=" + ip + "; Database=" + dbName + "; User ID =" + user + "; Password = " + password + ";";
            DataCon = new SqlConnection(connectionString);
            DataCon.Open();
            Seller = user;
        }
        public static void ConnectionDB(string ip, string dbName)
        {
            string connectionStrong = "Server=" + ip + ";Database=" + dbName + ";Trusted_Connection=True;";
            DataCon = new SqlConnection(connectionStrong);
            DataCon.Open();
            //Seller = Environment.UserName;
            Seller = FormLogin.username;
        }

        //static DataConnection()
        //{
        //    string connectionString = "Your Connection String Here";
        //    DataCon = new SqlConnection(connectionString);
        //    DataCon.Open();
        //}
    }
}
