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

namespace QuanLyThuVien
{
    public partial class QuanLySach : Form
    {
        //khai bao
        //string dauvao;

        public QuanLySach()
        {
            InitializeComponent();
        }

        private void load_data()
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlDataAdapter da = new SqlDataAdapter("select * from Sach",con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void load_text()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
        }
        // bat loi
        public bool ktNhap()
        {
            bool ok = false;
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlCommand cmd = new SqlCommand("select Masach from Sach",con);
          //  SqlDataReader  = cmd.ExecuteReader();

            return ok;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            /* SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
             SqlCommand cmd = new SqlCommand("insert into Sach values('" + txtMaSach.Text + "','" + txtTenSach.Text + "','" + Convert.ToDouble(txtDonGia.Text) + "','" + Convert.ToInt16(txtSoLuong.Text) + "')", con) ;
             con.Open();
             int ret =  cmd.ExecuteNonQuery();
             if (ret == 1)
             {
                 MessageBox.Show("Thêm thành công");
             }
            */
            

            try
            {
                SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
                SqlCommand cmd = new SqlCommand("insert into Sach values('" + txtMaSach.Text + "','" + txtTenSach.Text + "','" + Convert.ToDouble(txtDonGia.Text) + "','" + Convert.ToInt16(txtSoLuong.Text) + "')", con);
                con.Open();
                int ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    MessageBox.Show("Thêm thành công");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(Exception))
                {
                    if(ex.Message.Contains("PRIMARY KEY"))
                    {
                        MessageBox.Show("Mã sách bị trùng mời bạn nhập lại");
                    }
                    else
                    
                        throw ex;
                    
                }
                //MessageBox.Show("Mã sách bị trùng mời bạn nhập lại");
            }

            //con.Close();
            load_data();
            load_text();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlCommand cmd = new SqlCommand("update Sach set Tensach='" + txtTenSach.Text + "',Dongia='" + Convert.ToDouble(txtDonGia.Text) + "', Soluong = '" + Convert.ToInt16(txtSoLuong.Text) + "' where Masach = '" + txtMaSach.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
            {
                MessageBox.Show("Cập nhật thành công");
            }
            con.Close();


            load_data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlCommand cmd = new SqlCommand("delete from Sach where Masach='" + txtMaSach.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
            {
                MessageBox.Show("Xóa thành công");
            }
            con.Close();
            load_data();
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlDataAdapter da = new SqlDataAdapter("select * from Sach where Tensach LIKE '%"+txtTimKiem.Text+"%'", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSach.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSach.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDonGia.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtMaSach_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text))
            {
                e.Cancel = true;
                txtMaSach.Focus();
                errorProvider1.SetError(txtMaSach, "Hãy nhập mã sách");
            } else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMaSach, null);
            }
        }
    }
}
