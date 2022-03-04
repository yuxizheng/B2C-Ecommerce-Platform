using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BikiniBottomMall
{
    public partial class ProductsManagement : Form
    {
        Product p = new Product();
        Cart c = new Cart();
        OrderManagement om = new OrderManagement();
        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;
        public ProductsManagement()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string keyword = txtSearch1.Text;
            string sql = "select * from products where businessid=@businessid and name like '%" + keyword + "%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        private void backtosign_Click(object sender, EventArgs e)
        {
            SignUp signupwindow = new SignUp();
            signupwindow.Show();
            this.Close();
        }

        private void Search_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }


        public void Clear()
        {
            txtinventory.Text = "";
            txtSearch2.Text = "";
        }

        private void Update_Click(object sender, EventArgs e)
        {
            p.ProductID = txtproductid.Text;

            int x;
            Int32.TryParse(txtinventory.Text, out x);
            p.inventory = x;

            bool success = p.Update(p);

            if (success == true)
            {
                MessageBox.Show("info Updated!");
                DataTable dt2 = p.Select_Bussiness();
                dataGridView3.DataSource = dt2;
                Clear();
            }
            else
            {
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch2.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlCommand cmd = new SqlCommand ("SELECT o.OrderDate, o.OrderID, od.DetailID, p.name, o.Address " +
                    "FROM Orders o,Orderdetail od, Products p " +
                    "where p.ProductID = od.ProductID and od.OrderID = o.OrderID and o.BusinessID=@businessid " +
                    "and o.orderID like '%" + keyword + "%'", conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            conn.Open();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        

        private void Top3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
          
            string sql = "select t.* from (select sum(quantity) as total_sales_amount,p.productid,typeid,name,p.unitprice,inventory, rank() over( order by sum(quantity) desc) as rank_num " +
                        "from Products p, Orderdetail od, Orders o " +
                        "where p.BusinessID = @businessid and od.OrderID = o.OrderID and o.BusinessID = @businessid and od.productid = p.productid " +
                        "group by p.productid, p.typeid, p.name, p.unitprice, p.inventory) t where rank_num<4";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            conn.Open();
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            Clear();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        public void show_orders()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string sql = "select * from orders where businessid = @businessid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        public void show_products()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string sql = "select * from products where businessid = @businessid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        public void show_rank()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string sql = "exec Business_top3 @businessid=@businessid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        public void show_annual_sales()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string sql = "exec Business_amout_year @year=2021,@businessid = @businessid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@businessid", Globals.BusinessID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            string annual_sales = dt.Rows[0][0].ToString();
            txtsales.Text = annual_sales;
        }
        private void ProductsManagement_Load(object sender, EventArgs e)
        {
            show_orders();
            show_products();
            show_rank();
            show_annual_sales();
        }

        private void dataGridView3_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            txtproductid.Text = dataGridView3.Rows[rowindex].Cells[0].Value.ToString();
            p.ProductID = txtproductid.Text;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
