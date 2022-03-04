using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BikiniBottomMall
{
    class OrderManagement
    {
        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public string BusinessID { get; set; }
        public int amount { get; set; }
        public string OrderDate { get; set; }
        public string comment { get; set; }
        public string DetailID { get; set; }
        public string CommentID { get; set; }
        public int rate { get; set; }

        public DataTable Select()
        {
            UserID = Globals.UserID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "SELECT o.OrderDate,o.OrderID,od.DetailID,p.name,o.Address FROM Orders o,Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID=@userid";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", UserID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable Select_order_sa()
        {
            UserID = Globals.UserID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "SELECT o.OrderDate,o.OrderID,od.DetailID,p.name,o.Address FROM Orders o,Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable rank_Select()
        {
            UserID = Globals.UserID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select name,BusinessID,count(name) as frequency,rank() over(order by count(name) desc) as rank from (SELECT p.name, o.BusinessID FROM Orders o, Orderdetail od, Products p where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.UserID = @userid) as a group by name,BusinessID";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", UserID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public DataTable Select2()
        {
            BusinessID = Globals.BusinessID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "SELECT o.OrderDate,o.OrderID,od.DetailID,p.name,o.Address " +
                    "FROM Orders o,Orderdetail od, Products p " +
                    "where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.BusinessID=@businessid";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@businessid", BusinessID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable rank_Select2()
        {
            BusinessID = Globals.BusinessID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select name,BusinessID,count(name) as sales number,rank() over(order by count(name) desc) as rank " +
                    "from (SELECT p.name, o.BusinessID " +
                    "FROM Orders o, Orderdetail od, Products p " +
                    "where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.BusinessID = @businessid) " +
                    "as a group by name,BusinessID";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@businessid", BusinessID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public bool comment_Insert()
        {
            UserID = Globals.UserID;
            string commentid = Globals.RandomString(12);
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // 3: sql query to insert
                string sql = "insert into Comments(CommentID,DetailID,comment,date,rate) values(@commentid,@detailid,@comment,Convert(varchar(20), getdate(),101),@rate)";
                // open connection
                conn.Open();
                // sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create parameters to add data
                cmd.Parameters.AddWithValue("@commentid", commentid);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.Parameters.AddWithValue("@detailid", DetailID);
                cmd.Parameters.AddWithValue("@rate", rate);

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

        public DataTable comment_Select()
        {
            UserID = Globals.UserID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select c.* from Comments c,Orderdetail od, Orders o where od.OrderID=o.OrderID and o.UserID=@userid and c.DetailID=od.DetailID";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", UserID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable comment_Select_sa()
        {
            UserID = Globals.UserID;
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select c.* from Comments c,Orderdetail od, Orders o where od.OrderID=o.OrderID and c.DetailID=od.DetailID";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userid", UserID);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public DataTable show_product_comment(string productid)
        {
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select c.date,c.rate,c.comment,o.UserID " +
                    "from Comments c,Orderdetail od, Orders o " +
                    "where od.OrderID=o.OrderID and c.DetailID=od.DetailID and od.ProductID=@productid";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productid", productid);
                // create sql dataadapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        public bool Delete_Comment(string commentid)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "delete from Comments where CommentID=@commentid";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@commentid", commentid);
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
