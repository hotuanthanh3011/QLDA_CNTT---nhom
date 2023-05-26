using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CNPM1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                string connect = "server= " + @"DESKTOP-QV932RK\MSSQLSERVER01"
                                + ";database= " + "QuanLyHS"
                                + ";integrated security= true";
                con.ConnectionString = connect;
                string tk = tb_user.Text;
                string mk = tb_password.Text;
                string sql = "SELECT * FROM Nguoidung WHERE nguoidung= '" + tk + "'AND matkhau='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0 )
                {
                    var admin_role = dt.Rows[0].Field<string>("admin_role");
                    if (admin_role == "y")
                    {
                        MessageBox.Show("Đăng nhập thành công với quyền Giáo viên");
                        Menu mn = new Menu();
                        this.Hide();
                        mn.ShowDialog();

                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập với quyền học sinh");
                        TK thongke = new TK();
                        this.Hide();
                        thongke.ShowDialog();
                    }

                }
                   
                else if(tb_user.Text != "" && tb_password.Text != "")
                {
                    MessageBox.Show("Nhập sai mật khẩu vui lòng nhập lại");
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập Tên đăng nhập và mật khẩu");
                }    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
