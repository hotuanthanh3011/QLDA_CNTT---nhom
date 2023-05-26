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
    public partial class QuanLyuser : Form
    {
        public string conect_string = "server=" + @"TRQUAN"
                                     + ";database=" + "QuanLyHS"
                                     + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adapter;
        SqlCommand cmd;
        int row_id = 0;
        public QuanLyuser()
        {
            InitializeComponent();
        }

        private void QuanLyuser_Load(object sender, EventArgs e)
        {
            Dislay_user();
        }
        private void Dislay_user()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT * FROM nguoidung", con);
                adapter.Fill(dt);
                gv_quanliuser.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void Clear_diem()
        {
            tb_taikhoan.Text = "";
            tb_matkhau.Text = "";
            
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (tb_taikhoan.Text != "" && tb_matkhau.Text != "");
                try
                {
                    cmd = new SqlCommand("INSERT INTO nguoidung(taikhoan,matkhau,admin_role) VALUES(@tk,@mk,@admin)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@tk", tb_taikhoan.Text);
                    cmd.Parameters.AddWithValue("@mk", tb_matkhau.Text);
                    cmd.Parameters.AddWithValue("@admin", tb_admin.Text);
                    Dislay_user();
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Insert thành công vào bảng Điểm");
                    
                    Clear_diem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                }
            }
     
        }
    }


