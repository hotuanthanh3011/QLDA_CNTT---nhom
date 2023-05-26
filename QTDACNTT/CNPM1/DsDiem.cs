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
    public partial class DsDiem : Form
    {
        public string conect_string = "server=" + @"DESKTOP-QV932RK\MSSQLSERVER01"
                                     + ";database=" + "QuanLyHS"
                                     + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adapter;
        SqlCommand cmd;
        int row_id = 0;
        public DsDiem()
        {
            InitializeComponent();
        }
        private void Clear_diem()
        {
            tb_mahs.Text = "";
            tb_mamh.Text = "";
            tb_hs1.Text = "";
            tb_hs2.Text = "";
            tb_hocki.Text = "";
            tb_hanhkiem.Text = "";
            tb_tenhs.Text = "";
            tb_malop.Text = "";
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            if (tb_mahs.Text != "" && tb_mamh.Text != "" && tb_hs1.Text != "" && tb_hs2.Text != "" && tb_hocki.Text != "" && tb_hanhkiem.Text != "" && tb_tenhs.Text != "" && tb_malop.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("INSERT INTO diemhanhkiemhocsinh(maHS,maMH,diemheso1,diemheso2,diemthi,hanhkiem,malop,tenhs) VALUES(@mahs,@mamh,@hs1,@hs2,@hocki,@hanhkiem,@malop,@tenhs)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@mahs", tb_mahs.Text);
                    cmd.Parameters.AddWithValue("@mamh", tb_mamh.Text);
                    cmd.Parameters.AddWithValue("@hs1", tb_hs1.Text);
                    cmd.Parameters.AddWithValue("@hs2", tb_hs2.Text);
                    cmd.Parameters.AddWithValue("@hocki", tb_hocki.Text);
                    cmd.Parameters.AddWithValue("@hanhkiem", tb_hanhkiem.Text);
                    cmd.Parameters.AddWithValue("@malop", tb_malop.Text);
                    cmd.Parameters.AddWithValue("@tenhs", tb_tenhs.Text);
                    cmd.ExecuteReader();
                    con.Close();
                    MessageBox.Show("Insert thành công vào bảng Điểm");
                    Dislay_diem();
                    Clear_diem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thêm thông tin");
            }
        }

        private void DsDiem_Load(object sender, EventArgs e)
        {
            con.ConnectionString = conect_string;
            Dislay_diem();
        }
        private void Dislay_diem()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                adapter = new SqlDataAdapter("SELECT * FROM diemhanhkiemhocsinh", con);
                adapter.Fill(dt);
                gv_dsdiem.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
       

        private void gv_dsdiem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row_id = e.RowIndex;
            tb_mahs.Text = gv_dsdiem.Rows[row_id].Cells[0].Value.ToString().Trim();
            tb_mamh.Text = gv_dsdiem.Rows[row_id].Cells[1].Value.ToString().Trim();
            tb_hs1.Text = gv_dsdiem.Rows[row_id].Cells[2].Value.ToString().Trim();
            tb_hs2.Text = gv_dsdiem.Rows[row_id].Cells[3].Value.ToString().Trim();
            tb_hocki.Text = gv_dsdiem.Rows[row_id].Cells[4].Value.ToString().Trim();
            tb_hanhkiem.Text = gv_dsdiem.Rows[row_id].Cells[5].Value.ToString().Trim();
            tb_tenhs.Text = gv_dsdiem.Rows[row_id].Cells[6].Value.ToString().Trim();
            tb_malop.Text = gv_dsdiem.Rows[row_id].Cells[7].Value.ToString().Trim();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (tb_mahs.Text != "" && tb_mamh.Text != "" && tb_hs1.Text != "" && tb_hs2.Text != "" && tb_hocki.Text != "" && tb_hanhkiem.Text != "" && tb_tenhs.Text != "" && tb_malop.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("UPDATE diemhanhkiemhocsinh SET maMH = @mamh, diemheso1 = @hs1, diemheso2 = @hs2 , diemthi = @hocki, hanhkiem = @hanhkiem, tenhs = @tenhs, malop = @malop WHERE maHS = @mahs ", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@mahs", tb_mahs.Text);
                    cmd.Parameters.AddWithValue("@mamh", tb_mamh.Text);
                    cmd.Parameters.AddWithValue("@hs1", tb_hs1.Text);
                    cmd.Parameters.AddWithValue("@hs2", tb_hs2.Text);
                    cmd.Parameters.AddWithValue("@hocki", tb_hocki.Text);
                    cmd.Parameters.AddWithValue("@hanhkiem", tb_hanhkiem.Text);
                    cmd.Parameters.AddWithValue("@malop", tb_malop.Text);
                    cmd.Parameters.AddWithValue("@tenhs", tb_tenhs.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Update thành công vào bảng Điểm");
                    }
                    con.Close();
                    Dislay_diem();
                    Clear_diem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thêm thông tin");
            }
        }

        private void btn_del_Click(object sender, EventArgs e)
        {

            if (tb_mahs.Text != "" && tb_mamh.Text != "" && tb_hs1.Text != "" && tb_hs2.Text != "" && tb_hocki.Text != "" && tb_hanhkiem.Text != "" && tb_tenhs.Text != "" && tb_malop.Text != "")
            {
                try
                {
                    cmd = new SqlCommand("DELETE diemhanhkiemhocsinh WHERE maMH = @mamh AND diemheso1 = @hs1 AND diemheso2 = @hs2 AND diemthi = @hocki AND hanhkiem = @hanhkiem AND tenhs = @tenhs AND malop = @malop AND maHS = @mahs ", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@mahs", tb_mahs.Text);
                    cmd.Parameters.AddWithValue("@mamh", tb_mamh.Text);
                    cmd.Parameters.AddWithValue("@hs1", tb_hs1.Text);
                    cmd.Parameters.AddWithValue("@hs2", tb_hs2.Text);
                    cmd.Parameters.AddWithValue("@hocki", tb_hocki.Text);
                    cmd.Parameters.AddWithValue("@hanhkiem", tb_hanhkiem.Text);
                    cmd.Parameters.AddWithValue("@malop", tb_malop.Text);
                    cmd.Parameters.AddWithValue("@tenhs", tb_tenhs.Text);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        MessageBox.Show("Delete thành công vào bảng Điểm");
                    }
                    else
                    {
                        MessageBox.Show("Không có điểm bị xóa");
                    }
                    con.Close();
                    Dislay_diem();
                    Clear_diem();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng thêm thông tin");
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            if (tb_timkiem.Text != "")
            {
                con.ConnectionString = conect_string;
                try
                {
                    con.Open();
                    string mahs = tb_timkiem.Text;
                    DataTable dt = new DataTable();
                    adapter = new SqlDataAdapter("SELECT * FROM diemhanhkiemhocsinh WHERE mahs= '" + mahs + "'", con);
                    adapter.Fill(dt);
                    gv_dsdiem.DataSource = dt;
                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } 
            else
            {
                MessageBox.Show("Vui lòng thêm thông tin tìm kiếm");
            }
        }

        private void btn_tinhdiem_Click(object sender, EventArgs e)
        {
            string Strhs1 = "", Strhs2 = "", Strhk = "", Strpheptoan = "";
            float hs1 = 0, hs2 = 0, hk = 0, ketqua = 0;

            Strhs1 = tb_hs1.Text;
            Strhs2 = tb_hs2.Text;
            Strhk = tb_hocki.Text;
            Strpheptoan = cboPheptoan.Text;
            if (string.IsNullOrEmpty(Strpheptoan))
            {
                MessageBox.Show("Bạn cần phải chọn phép toán");

                cboPheptoan.Focus();

                return;
            }

            hs1 = float.Parse(Strhs1);
            hs2 = float.Parse(Strhs2);
            hk = float.Parse(Strhk);

            switch (Strpheptoan)
            {
                case "Học kì":
                    ketqua = (hs1 + hs2 * 2 + hk * 3) / 5;
                    break;
                case "cả năm":
                    ketqua = ((hs1 + hs2 * 2 + hk * 3) / 5 * 2) / 2;
                    break;
            }
            tb_tbhocki.Text = ketqua.ToString();
        }

        private void btn_cancel1_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            this.Hide();
            mn.ShowDialog();
        }
    }
}
