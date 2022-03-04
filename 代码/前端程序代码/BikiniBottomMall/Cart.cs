using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BikiniBottomMall
{
    class Cart
    {
        public string CartID { get; set; }
        public string UserID { get; set; }
        public string ProductID { get; set; }
        public string Address { get; set; }
        public int ProductQuantity { get; set; }

        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;


        public bool Checkout(Cart c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {

                string sql = "exec check_out_final @userid,@address";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@address", c.Address);
                cmd.Parameters.AddWithValue("@userid", c.UserID);
                conn.Open();
                
                int rows = cmd.ExecuteNonQuery();
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

        //Selecting data from database
        public DataTable Select()
        {
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select * from Cart where UserID=@userid";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid",UserID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //Insert Data into Database
        public bool Insert(Cart c)
        {
            c.CartID = Globals.RandomString(12);
            // 1: create a default false return
            bool isSuccess = false;
            // 2: connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // 3: sql query to insert
                string sql = "insert into Cart(CartID,UserID,ProductID,ProductQuantity) values(@cartid,@userid,@productid,@productquantity)";
                // open connection
                conn.Open();
                // sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create parameters to add data
                cmd.Parameters.AddWithValue("@cartid", c.CartID);
                cmd.Parameters.AddWithValue("@userid", c.UserID);
                cmd.Parameters.AddWithValue("@productid", c.ProductID);
                cmd.Parameters.AddWithValue("@productquantity", c.ProductQuantity);

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

        public bool Update(Cart c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "update Cart set ProductQuantity=@productquantity where Cartid=@CartID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cartid", c.CartID);
                cmd.Parameters.AddWithValue("@userid", c.UserID);
                cmd.Parameters.AddWithValue("@productid", c.ProductID);
                cmd.Parameters.AddWithValue("@productquantity", c.ProductQuantity);
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
        public bool Delete(Cart c)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "delete from Cart where CartID=@CartID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CartID", c.CartID);
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
