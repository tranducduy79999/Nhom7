using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using System.Data.SqlClient;

namespace GUI.BacSi
{
    public partial class FormBacSi_QuanLyBenhNhan : Form
    {
        public FormBacSi_QuanLyBenhNhan()
        {
            InitializeComponent();
            
        }
        private void ClearALL()
        {
            txt_CCCD.Text = string.Empty;
            txt_HoTen.Text = string.Empty;
            txt_SDT.Text = string.Empty;
            dtp_NgaySinh.Value = DateTime.Today;
            txt_MaBHYT.Text = string.Empty;
            txt_MatKhau.Text = string.Empty;
            txt_DiaChi.Text = string.Empty;
            rdb_Nam.Checked = true;
            
        }
        BUS_tbl_BenhNhan bus_benhnhan = new BUS_tbl_BenhNhan();

        private void dgv_BenhNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormBacSi_QuanLyBenhNhan_Load(object sender, EventArgs e)
        {
            dgv_BenhNhan.DataSource = bus_benhnhan.SelectAllFromBenhNhan();
            cb_TimKiemTheo.SelectedItem = "Họ tên";
            FormBorderStyle = FormBorderStyle.None;
            ClearALL();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                DTO_tbl_BenhNhan bn = new DTO_tbl_BenhNhan();
                if(txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                else if(txt_HoTen.Text.Trim() =="")
                {
                    throw new Exception("Vui lòng nhập họ tên bệnh nhân");
                }     
                else if(txt_DiaChi.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa chỉ bệnh nhân");
                }
                else if (txt_MaBHYT.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập địa mã bảo hiểm y tế");
                }
                else if(txt_MatKhau.Text.Trim() =="")
                {
                    throw new Exception("Vui lòng nhập mật khẩu");
                }
                else if(txt_SDT.Text.Trim() =="")
                {
                    throw new Exception("Vui lòng nhập số điện thoại");
                }
                bn.HoTen = txt_HoTen.Text;
                bn.MatKhau = txt_MatKhau.Text;
                bn.NgaySinh = dtp_NgaySinh.Value;
                bn.SDT = txt_SDT.Text;
                bn.CCCD = txt_CCCD.Text;
                bn.DiaChi = txt_DiaChi.Text;
                bn.MaBHYT = txt_MaBHYT.Text;
                if (rdb_Nam.Checked)
                {
                    bn.GioiTinh = "Nam";
                }
                else
                {
                    bn.GioiTinh = "Nữ";

                }              
                bus_benhnhan.InsertBenhNhan(bn);
                FormBacSi_QuanLyBenhNhan_Load(sender, e);
            }
            catch(SqlException er)
            {
                if (er.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    MessageBox.Show("Tồn tại bệnh án vui lòng kiểm tra lại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }
            
           
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            DTO_tbl_BenhNhan bn = new DTO_tbl_BenhNhan();
            bn.CCCD = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();
            bn.HoTen = txt_HoTen.Text;
            bn.MaBHYT = txt_MaBHYT.Text;
            bn.MatKhau = txt_MatKhau.Text;
            bn.SDT = txt_MatKhau.Text;
            bn.NgaySinh = dtp_NgaySinh.Value;
            bn.DiaChi = txt_DiaChi.Text;
            if (rdb_Nam.Checked)
            {
                bn.GioiTinh = "Nam";
            }
            else
            {
                bn.GioiTinh = "Nữ";

            }
            bus_benhnhan.UpdateBenhNhan(bn);
            FormBacSi_QuanLyBenhNhan_Load(sender, e);

        }
        int row;
        private void dgv_BenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            if(row >=0)
            {
                txt_CCCD.Enabled = false;
                txt_CCCD.Text = dgv_BenhNhan.Rows[row].Cells[0].Value.ToString();
                txt_MatKhau.Text = dgv_BenhNhan.Rows[row].Cells[1].Value.ToString();
                txt_HoTen.Text = dgv_BenhNhan.Rows[row].Cells[2].Value.ToString();
                if(dgv_BenhNhan.Rows[row].Cells[3].Value.ToString() == "Nam")
                {
                    rdb_Nam.Checked = true;
                }
                else
                {
                    rdb_Nu.Checked = true;
                }
                dtp_NgaySinh.Text = dgv_BenhNhan.Rows[row].Cells[4].Value.ToString();
                txt_MaBHYT.Text = dgv_BenhNhan.Rows[row].Cells[5].Value.ToString();
                txt_DiaChi.Text = dgv_BenhNhan.Rows[row].Cells[6].Value.ToString();
                txt_SDT.Text = dgv_BenhNhan.Rows[row].Cells[7].Value.ToString();
            }

           
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            ClearALL();
            txt_CCCD.Enabled = true;
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            try
            {
                bus_benhnhan.DeleteBenhNhan(dgv_BenhNhan.Rows[row].Cells[0].Value.ToString());
                FormBacSi_QuanLyBenhNhan_Load(sender, e);
            }
            catch (SqlException er)
            {
                if (er.Message.Contains("DELETE statement conflicted with the REFERENCE constraint"))
                {
                    MessageBox.Show("Tồn tại bệnh án vui lòng kiểm tra lại", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }




        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            String key = txt_TimKiem.Text;
            if(cb_TimKiemTheo.SelectedItem.ToString() == "Họ tên")
            {
                if(key != null)
                {
                    dgv_BenhNhan.DataSource =bus_benhnhan.FindBenhNhanByName(key);
                    ClearALL();
                }    
            }
            else if(cb_TimKiemTheo.SelectedItem.ToString() == "Căn cước công dân")
            {
                if (key != null)
                {
                    dgv_BenhNhan.DataSource = bus_benhnhan.FindBenhNhanByCCCD(key);
                    ClearALL();
                }
            }
            else if(cb_TimKiemTheo.SelectedItem.ToString() == "Địa chỉ")
            {
                if (key != null)
                {
                    dgv_BenhNhan.DataSource = bus_benhnhan.FindBenhNhanByDiaChi(key);
                    ClearALL();
                }
            }
        }

        private void btn_XemBenhAn_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
