using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BikiniBottomMall
{
    class SignUpIn
    {
        //Data carrier
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Tel { get; set; }
        public string UserPwd { get; set; }
        public string Province { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;

        //Selecting data from database
        public DataTable Select()
        {
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "SELECT * FROM Users";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Insert Data into Database
        public bool Insert(SignUpIn s)
        {
            // 1: create a default false return
            bool isSuccess = false;
            // 2: connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // 3: sql query to insert
                string sql = "insert into Users (UserID,UserName,gender,birthday,tel,Userpwd,province) values(@UserID,@UserName,@gender,@birthday,@tel,@Userpwd,@province)";
                // open connection
                conn.Open();
                // sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create parameters to add data
                cmd.Parameters.AddWithValue("@UserId", s.UserID);
                cmd.Parameters.AddWithValue("@UserName", s.UserName);
                cmd.Parameters.AddWithValue("@gender", s.Gender);
                cmd.Parameters.AddWithValue("@birthday", s.Birthday);
                cmd.Parameters.AddWithValue("@tel", s.Tel);
                cmd.Parameters.AddWithValue("@UserPwd", s.UserPwd);
                cmd.Parameters.AddWithValue("@province", s.Province);
                
                int rows = cmd.ExecuteNonQuery();
                // if successful, the value of rows will be greater than zero
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        public bool Update(SignUpIn s)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "update users set UserName=@UserName,gender=@gender,birthday=@birthday,tel=@tel,Userpwd=@Userpwd,province=@province where UserID=@userid";
               
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@UserId", s.UserID);
                cmd.Parameters.AddWithValue("@UserName", s.UserName);
                cmd.Parameters.AddWithValue("@gender", s.Gender);
                cmd.Parameters.AddWithValue("@birthday", s.Birthday);
                cmd.Parameters.AddWithValue("@tel", s.Tel);
                cmd.Parameters.AddWithValue("@UserPwd", s.UserPwd);
                cmd.Parameters.AddWithValue("@province", s.Province);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if successful, the value of rows will be greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        public bool Delete(SignUpIn s)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "delete from Users where UserID=@UserID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@UserId", s.UserID);
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if successful, the value of rows will be greater than zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
