using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BikiniBottomMall
{
    class Product
    {
        public string ProductID { get; set; }
        public string BusinessID { get; set; }
        public string TypeID { get; set; }
        public string name { get; set; }
        public int unitprice { get; set; }
        public string introduction { get; set; }
        public string image { get; set; }
        public int inventory { get; set; }

        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;

        public DataTable Select_Bussiness()
        {
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select * from products where BusinessID=@businessid";
                // create cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
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

        //Selecting data from database
        public DataTable Select()
        {
            // 1: database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                // 2: writing SQL query
                string sql = "select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID";
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


        //Insert Data into Database
        public bool Insert(Product p)
        {
            // 1: create a default false return
            bool isSuccess = false;
            // 2: connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                // 3: sql query to insert
                string sql = "insert into Products(ProductID,BusinessID,TypeID,name,unitprice,introduction,image,inventory) " +
                    "values(@ProductID,@BusinessID,@TypeID,@name,@unitprice,@introduction,@image,@inventory)";
                // open connection
                conn.Open();
                // sql command
                SqlCommand cmd = new SqlCommand(sql, conn);

                // create parameters to add data
                cmd.Parameters.AddWithValue("@inventory", p.inventory);
                cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
                cmd.Parameters.AddWithValue("@BusinessID", p.BusinessID);
                cmd.Parameters.AddWithValue("@TypeID", p.TypeID);
                cmd.Parameters.AddWithValue("@name", p.name);
                cmd.Parameters.AddWithValue("@unitprice", p.unitprice);
                cmd.Parameters.AddWithValue("@introduction", p.introduction);
                cmd.Parameters.AddWithValue("@image", p.image);

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

        public bool Update(Product p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "update products set inventory=@inventory where ProductID=@ProductID";

                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@inventory", p.inventory);
                cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
                
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
        public bool Delete(Product p)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                string sql = "delete from Users where ProductID=@ProductID, BusinessID=@BusinessID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductID", p.ProductID);
                cmd.Parameters.AddWithValue("@BusinessID", p.BusinessID);
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
