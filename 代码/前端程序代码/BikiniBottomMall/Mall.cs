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
    public partial class Mall : Form
    {
        Product p = new Product();
        Cart c = new Cart();
        OrderManagement om = new OrderManagement();
        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;

        public Mall()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Mall_Load(object sender, EventArgs e)
        {
            //show user id
            displayuserid.Text = Globals.UserName;

            //set userin into cart
            c.UserID = Globals.UserID;
            //set quantity default value
            c.ProductQuantity = 1;

            // load data on data gridview
            DataTable dt = p.Select();
            DataTable dt2 = c.Select();
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt2;
            
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtsearch.Text;
            string shopname = txtbus.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda = new SqlDataAdapter("select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname from products p,Businesses b where p.BusinessID = b.BusinessID and name like '%"+keyword+ "%' and b.ShopName like '%" + shopname + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void displayuserid_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void backtosign_Click(object sender, EventArgs e)
        {
            SignUp signupwindow = new SignUp();
            signupwindow.Show();
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            txtproductid.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            txtunitprice.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
            c.ProductID = txtproductid.Text;
            Get_average_rate();
            DataTable commenttable = om.show_product_comment(txtproductid.Text);
            dataGridView3.DataSource = commenttable;
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            // get the value from the input fields in click
            int x;
            Int32.TryParse(txtquantity.Text, out x);
            c.ProductQuantity = x;

            // insert data into database using the method
            bool success = c.Insert(c);
            if (success == true)
            {
                // send a message
                MessageBox.Show("New item added!");
                DataTable dt2 = c.Select();
                dataGridView2.DataSource = dt2;
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        public void Clear()
        {
            txtproductid.Text = "";
            txtquantity.Text = "";
            txtunitprice.Text = "";
        }


        private void btn_Click(object sender, EventArgs e)
        {
            // insert data into database using the method
            int x;
            Int32.TryParse(txtquantity.Text, out x);
            c.ProductQuantity = x;
            bool success = c.Update(c);
            if (success == true)
            {
                // send a message
                MessageBox.Show("info Updated!");
                DataTable dt2 = c.Select();
                dataGridView2.DataSource = dt2;
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;

            c.CartID = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();

            txtproductid.Text = dataGridView2.Rows[rowindex].Cells[2].Value.ToString();
            
            txtunitprice.Text = dataGridView2.Rows[rowindex].Cells[3].Value.ToString();
            
            c.ProductID = txtproductid.Text;

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            bool success = c.Delete(c);
            if (success == true)
            {
                // send a message
                MessageBox.Show("Item deleted!");
                DataTable dt2 = c.Select();
                dataGridView2.DataSource = dt2;
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void btncheckout_Click(object sender, EventArgs e)
        {
            c.Address = txtaddr.Text;
            bool success = c.Checkout(c);
            if (success == true)
            {
                // send a message
                MessageBox.Show("Successfully checkOut!");
                DataTable dt2 = c.Select();
                dataGridView2.DataSource = dt2;
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void btncheap5_Click(object sender, EventArgs e)
        {
            string keyword = txtsearch.Text;
            string shopname = txtbus.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            //string sql2 = "(select ProductID, BusinessID, TypeID, name, unitprice, introduction, image, rank() over(order by unitprice) as rank_num from Products where name like '%" + keyword + "%')";
            string sql2 = "(select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname,rank() over(order by p.unitprice) as rank_num " +
                "from products p,Businesses b " +
                "where p.BusinessID = b.BusinessID and name like '%" + keyword + "%' and b.ShopName like '%" + shopname + "%')";
            string sql = "select t.* from" + sql2 + "t where rank_num < 6";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnusercenter_Click(object sender, EventArgs e)
        {
            UserCenter usercenterwindow = new UserCenter();
            usercenterwindow.Show();
            this.Close();
        }

        private void txtunitprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbus_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtsearch.Text;
            string shopname = txtbus.Text;
            SqlConnection conn = new SqlConnection(myconnstrng);
            SqlDataAdapter sda = new SqlDataAdapter("select p.productid, p.name,p.unitprice,p.introduction,p.inventory,p.image,b.shopname " +
                "from products p,Businesses b " +
                "where p.BusinessID = b.BusinessID and name like '%" + keyword + "%' and b.ShopName like '%" + shopname + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtproductid_TextChanged(object sender, EventArgs e)
        {

        }

        public void Get_average_rate()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            conn.Open();

            string sql = "select avg(rate) from Comments c,Orderdetail od where c.DetailID=od.DetailID and od.ProductID=@productid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@productid", c.ProductID);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            string averagerate = dt.Rows[0][0].ToString();
            txtrate.Text = averagerate;
        }
    }
}
