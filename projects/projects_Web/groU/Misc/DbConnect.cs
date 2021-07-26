using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace groU
{
    public class DbConnect
    {
        SqlConnection db = null;
        public SqlConnection Db { get => db; set => db = value; }

        public DbConnect()
        {
            try
            {
                Db = new SqlConnection(@"Server = 161.22.42.238,54321;
                                        Database = groU;
                                        User = sa;
                                        Password = 123456789Net3;");
                Db.Open();
            }
            catch (Exception e) { }
        }


        //Funciones exclusivas para leer de la base de datos
        public SqlDataReader CreateQuery(string query)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(query, this.Db);
                SqlDataReader reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception e) { return null; }
        }
    }
}