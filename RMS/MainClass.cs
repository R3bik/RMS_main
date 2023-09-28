using System;
using System.Collections;
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
            bool isValid = false;
            string qry = @"Select * from users where username = '" + user + "' and upass= '" + pass + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            }
            /* bool isValid = false;
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
             }*/




            return isValid;
        }

        //create properties for username
        public static string user;

        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        //Method for CRUD operation

        public static int SQl(string qry, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;

                foreach (DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) { con.Open(); }
                res = cmd.ExecuteNonQuery();
                if (con.State == ConnectionState.Open) { con.Close(); }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
            return res;
        }
        //For loading from database
        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < lb.Items.Count; i++)
                {
                    string colNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                    gv.Columns[colNam1].DataPropertyName = dt.Columns[i].ToString();
                }
                gv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }
    }
}
