using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BikiniBottomMall
{
    public partial class UserCenter : Form
    {
        OrderManagement om = new OrderManagement();

        public UserCenter()
        {
            InitializeComponent();
        }

        private void UserCenter_Load(object sender, EventArgs e)
        {
            textBox1.Text = Globals.UserName;
            DataTable dt = om.Select();
            dataGridView1.DataSource = dt;
            DataTable dt2 = om.comment_Select();
            dataGridView2.DataSource = dt2;
            DataTable dt3 = om.rank_Select();
            dataGridView3.DataSource = dt3;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            om.comment = textBox3.Text;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            txtod.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            om.DetailID = txtod.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x;
            Int32.TryParse(comborate.Text, out x);
            om.rate = x;
            bool success = om.comment_Insert();
            if (success == true)
            {
                // send a message
                MessageBox.Show("Successfully commented!");
                DataTable dt2 = om.comment_Select();
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
            txtod.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView2_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            om.CommentID = dataGridView2.Rows[rowindex].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool success = om.Delete_Comment(om.CommentID);
            if (success == true)
            {
                // send a message
                MessageBox.Show("Comment deleted!");
                DataTable dt2 = om.comment_Select();
                dataGridView2.DataSource = dt2;
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Mall mallwindow = new Mall();
            mallwindow.Show();
            this.Close();
        }

        private void txtod_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
