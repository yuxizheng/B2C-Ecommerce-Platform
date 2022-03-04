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
    public partial class UserManagement : Form
    {
        OrderManagement om = new OrderManagement();

        static string myconnstrng = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;
        public UserManagement()
        {
            InitializeComponent();
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

        private void button2_Click(object sender, EventArgs e)
        {
            bool success = om.Delete_Comment(om.CommentID);
            if (success == true)
            {
                // send a message
                MessageBox.Show("Comment deleted!");
                SqlConnection conn = new SqlConnection(myconnstrng);
                string sql = "(select * from comments)";
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView2.DataSource = dt;
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
            om.CommentID = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
        }

        private void All_Click(object sender, EventArgs e)
        {
            
        }
        
        public void province_detail()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            string sql = "exec Province_detail";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView3.DataSource = dt;
        }

        public void show_comment()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            string sql = "(select * from comments)";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        public void show_user_most()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            string sql = "exec User_purchasemost";
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridUserBuy.DataSource = dt;
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            show_comment();
            province_detail();
            show_user_most();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_RowHeaderMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            txtcommentid.Text = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
            om.CommentID = txtcommentid.Text;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
