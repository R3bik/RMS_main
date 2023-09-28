using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS
{
    internal class MainClass
    {
        public static readonly string con_string = "Data Source = laptop-tj9evupb; Initial Catalog = RMS;Persist Security Info-True;User Id = sa; Password = 123;";
        public static SqlConnection con = new SqlConnection(con_string);

        O references
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid=false;
            string qry =@"Select * from users where username = '"

            return isValid;
         }
    }
}
