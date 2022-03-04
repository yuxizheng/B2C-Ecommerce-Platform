using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BikiniBottomMall
{
    public partial class SignUp : Form
    {
        static string myconnstrng = ConfigurationManager.ConnectionStrings["connectionstrings"].ConnectionString;

        SignUpIn s = new SignUpIn();

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // get the value from the input fields
            s.UserID = Globals.RandomString(8);
            s.UserName = txtusername.Text;
            s.Gender = txtgender.Text;
            s.Birthday = txtbirthday.Text;
            s.Tel = txttel.Text;
            s.UserPwd = txtpassword.Text;
            s.Province = txtaddress.Text;

            // insert data into database using the method
            bool success = s.Insert(s);
            if (success == true)
            {
                // send a message
                MessageBox.Show("Congrats! Your ID is "+s.UserID);
                Clear();
            }
            else
            {
                // fail
                MessageBox.Show("Sorry! Some error occur! Please contact us!");
            }
        }

        private void signin_Click(object sender, EventArgs e)
        {
            
            string userID = txtsigninuserid.Text;
            string password = txtsigninpassword.Text;

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            try
            {
                conn.Open();
                string sql = "SELECT * FROM Users where UserID like @ID and UserPwd like @pass";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", userID);
                cmd.Parameters.AddWithValue("@pass", password);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("User not existed or wrong password.");
                    txtsigninuserid.Text = "";
                    txtsigninpassword.Text = "";
                }
                else
                {
                    Globals.UserID = userID;
                    Globals.UserPwd = password;
                    Globals.UserName =dt.Rows[0][1].ToString();
                    Mall m = new Mall();
                    m.Show();
                    this.Hide();
                }
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public void Clear()
        {
            txtUserID.Text = "";
            txtusername.Text = "";
            txtaddress.Text = "";
            txtgender.Text = "";
            txtbirthday.Text = "";
            txtpassword.Text = "";
            txttel.Text = "";
        }


        
        private void signIn2_Click(object sender, EventArgs e)
        {
            string businessID = txtSignInBusinessId.Text;
            string businessPwd = txtSignInBusinessPwd.Text;

            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            try
            {
                conn.Open();
                string sql = "SELECT * FROM Businesses where BusinessID like @BID and BusinessPwd like @Bpass";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BID", businessID);
                cmd.Parameters.AddWithValue("@Bpass", businessPwd);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Business not existed or wrong password.");
                    txtSignInBusinessId.Text = "";
                    txtSignInBusinessPwd.Text = "";
                }
                else
                {
                    Globals.BusinessID = businessID;
                    Globals.BusinessPwd = businessPwd;
                    ProductsManagement p = new ProductsManagement();
                    p.Show();
                    this.Hide();
                }
            }
            catch (Exception es)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        private void saSignIn_Click(object sender, EventArgs e)
        {
            string said = txtsaID.Text;
            string sapwd = txtsaPwd.Text;
            
            try
            {
                

                if (Globals.saPwd == txtsaPwd.Text && Globals.saID == txtsaID.Text)
                {
                    Globals.saID = said;
                    Globals.saPwd = sapwd;
                    UserManagement u = new UserManagement();
                    u.Show();
                    this.Hide();
                }
                else
                {
                    
                    MessageBox.Show("sa not existed or wrong password.");
                    txtsaID.Text = "";
                    txtsaPwd.Text = "";
                }
            }
            catch (Exception es)
            {

            }
            finally
            {
                
            }
        }



        

        
    }
}
