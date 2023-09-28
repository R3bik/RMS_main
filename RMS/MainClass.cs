using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RMS
{
     class MainClass
    {
        public static readonly string con_string = "Data Source=laptop-tj9evupb;Initial Catalog=RMS;Integrated Security=True;";

        public static SqlConnection con = new SqlConnection(con_string);

        
        public static bool IsValidUser(string user, string pass)
        {
            /* bool isValid=false;
             string qry = @"Select * from users where username = '" + user + "' and upass= '" + pass + "'";
             SqlCommand cmd = new SqlCommand(qry, con);
             DataTable dt = new DataTable();
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             da.Fill(dt);

             if (dt.Rows.Count>0)
             {
                 isValid = true;
             }*/
            bool isValid = false;
            string qry = "SELECT COUNT(*) FROM users WHERE username = @Username AND upass = @Password";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Username", user);
            cmd.Parameters.AddWithValue("@Password", pass);

            try
            {
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                isValid = (count > 0);
            }
            catch (SqlException sqlEx)
            {
                // Handle SQL-specific exceptions.
                // You can log the exception details or display an error message to the user.
                // Here, we'll just display a message box with a generic error message.
                MessageBox.Show("An SQL error occurred: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Handle other general exceptions (not specific to SQL).
                // You can log the exception details or display an error message to the user.
                // Here, we'll just display a message box with a generic error message.
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }




            return isValid;
         }
    }
}
