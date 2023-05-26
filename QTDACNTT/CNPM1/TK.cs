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
    public partial class TK : Form
    {
        public string conect_string = "server=" + @"DESKTOP-QV932RK\MSSQLSERVER01"
                                     + ";database=" + "QuanLyHS"
                                     + ";integrated security=true";
        SqlConnection con = new SqlConnection();
        SqlDataAdapter adapter;
        SqlCommand cmd;
        int row_id = 0;
        public TK()
        {
            InitializeComponent();
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
        public void ExportFile(DataTable dataTable, string sheetName, string title)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần Tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã học sinh";

            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Mã môn học";

            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Điểm hệ số 1";
            cl3.ColumnWidth = 12.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Điểm hệ số 2";

            cl4.ColumnWidth = 10.5;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Điểm thi";

            cl5.ColumnWidth = 20.5;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Hạnh kiểm";

            cl6.ColumnWidth = 18.5;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Tên lớp";

            cl7.ColumnWidth = 14.5;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "Tên học sinh";

            cl8.ColumnWidth = 14.5; 


            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 6;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng theo datatable

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dataTable.Rows.Count - 2;

            int columnEnd = dataTable.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa 

            //Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            //Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            //Căn giữa cả bảng 
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }
        private void btn_xuatecel_Click(object sender, EventArgs e)
        {
            DataTable dateTable = new DataTable();

            DataColumn col1 = new DataColumn("maHS");
            DataColumn col2 = new DataColumn("maMH");
            DataColumn col3 = new DataColumn("diemheso1");
            DataColumn col4 = new DataColumn("diemheso2");
            DataColumn col5 = new DataColumn("diemhocky");
            DataColumn col6 = new DataColumn("hanhkiem");
            DataColumn col7= new DataColumn("malop");
            DataColumn col8 = new DataColumn("tenhs");

            dateTable.Columns.Add(col1);
            dateTable.Columns.Add(col2);
            dateTable.Columns.Add(col3);
            dateTable.Columns.Add(col4);
            dateTable.Columns.Add(col5);
            dateTable.Columns.Add(col6);
            dateTable.Columns.Add(col7);
            dateTable.Columns.Add(col8);

            foreach (DataGridViewRow dtgvRow in gv_dsdiem.Rows)
            {
                DataRow dtrow = dateTable.NewRow();

                dtrow[0] = dtgvRow.Cells[0].Value;
                dtrow[1] = dtgvRow.Cells[1].Value;
                dtrow[2] = dtgvRow.Cells[2].Value;
                dtrow[3] = dtgvRow.Cells[3].Value;
                dtrow[4] = dtgvRow.Cells[4].Value;
                dtrow[5] = dtgvRow.Cells[5].Value;
                dtrow[6] = dtgvRow.Cells[6].Value;
                dtrow[7] = dtgvRow.Cells[7].Value;

                dateTable.Rows.Add(dtrow);

            }
            ExportFile(dateTable, "Danh sach", "Danh sách điểm");
        }

        private void TK_Load(object sender, EventArgs e)
        {
            
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dg == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            con.ConnectionString = conect_string;
            Dislay_diem();
        }

        private void btn_backMenu_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            this.Hide();
            mn.ShowDialog();
        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {
           
        }
    }
}
