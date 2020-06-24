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
    public partial class FormBacSi_QuanLyBenhAn : Form
    {
        public FormBacSi_QuanLyBenhAn()
        {
            InitializeComponent();
        }
        BUS_tbl_BacSi bus_bacsi = new BUS_tbl_BacSi();
        BUS_tbl_BenhAn bus_benhan  = new BUS_tbl_BenhAn();

        private void FormBacSi_QuanLyBenhAn_Load(object sender, EventArgs e)
        {
            cb_BacSi.DataSource = bus_bacsi.SelectAll();
            cb_BacSi.DisplayMember = "TenBacSi";
            cb_BacSi.ValueMember = "MaBacSi";
            dgv_BenhAn.DataSource = bus_benhan.SelectAll();
            cb_TimKiemTheo.SelectedIndex = 0;
        }
        private void clear()
        {
            txt_CCCD.Text = "" ;
            txt_MaBenhAn.Text = "";
            dtp_NgayVaoVien.Text = DateTime.Today.ToString() ;
            dtp_NgayRaVien.Text = DateTime.Today.ToString();
            txt_ChanDoanLucVaoVien.Text = "";
            txt_ChanDoanLucRaVien.Text = "";
            txt_PhuongAnDieuTri.Text = "";
           
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            DTO_tbl_BenhAn bn = new DTO_tbl_BenhAn();
            try
            {
                int result = DateTime.Compare(dtp_NgayVaoVien.Value, dtp_NgayRaVien.Value);
                if (txt_CCCD.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập căn cước công dân");
                }
                else if (txt_MaBenhAn.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập mã bệnh án");
                }
                else if (txt_ChanDoanLucVaoVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc vào viện");

                }
                else if (txt_ChanDoanLucRaVien.Text.Trim() == "")
                {
                    throw new Exception("Vui lòng nhập chuẩn đoán lúc ra viện");

                }
                //MessageBox.Show(result.ToString());
                else if (result > 0)
                {
                    throw new Exception("Vui lòng chọn đúng ngày ra vào viện");
                }



                bn.CCCD = txt_CCCD.Text;
                bn.MaBenhAn = txt_MaBenhAn.Text;
                bn.NgayVaoVien = dtp_NgayVaoVien.Value;
                bn.NgayRaVien = dtp_NgayRaVien.Value;
                bn.ChanDoanLucVaoVien = txt_ChanDoanLucVaoVien.Text;
                bn.ChanDoanLucRaVien = txt_ChanDoanLucRaVien.Text;
                bn.PhuongAnDieuTri = txt_PhuongAnDieuTri.Text;
                bn.MaBacSi = cb_BacSi.SelectedValue.ToString();
                bus_benhan.InsertBenhAn(bn);
                clear();
                FormBacSi_QuanLyBenhAn_Load(sender, e);
            }
            catch(SqlException ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Tồn tại bệnh án", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            DTO_tbl_BenhAn bn = new DTO_tbl_BenhAn();
            bn.CCCD = txt_CCCD.Text;
            bn.MaBenhAn = txt_MaBenhAn.Text;
            bn.NgayVaoVien = dtp_NgayVaoVien.Value;
            bn.NgayRaVien = dtp_NgayRaVien.Value;
            bn.ChanDoanLucVaoVien = txt_ChanDoanLucVaoVien.Text;
            bn.ChanDoanLucRaVien = txt_ChanDoanLucRaVien.Text;
            bn.PhuongAnDieuTri = txt_PhuongAnDieuTri.Text;
            bn.MaBacSi = cb_BacSi.SelectedValue.ToString();
            bus_benhan.UpdateBenhAn(bn);
            FormBacSi_QuanLyBenhAn_Load( sender,  e);
        }
        int row;

        private void dgv_BenhAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            txt_CCCD.Text = dgv_BenhAn.Rows[row].Cells[1].Value.ToString();
            txt_MaBenhAn.Text = dgv_BenhAn.Rows[row].Cells[0].Value.ToString();
            dtp_NgayVaoVien.Text = dgv_BenhAn.Rows[row].Cells[2].Value.ToString();
            dtp_NgayRaVien.Text = dgv_BenhAn.Rows[row].Cells[3].Value.ToString();
            txt_ChanDoanLucVaoVien.Text = dgv_BenhAn.Rows[row].Cells[4].Value.ToString();
            txt_ChanDoanLucRaVien.Text = dgv_BenhAn.Rows[row].Cells[5].Value.ToString();
            txt_PhuongAnDieuTri.Text = dgv_BenhAn.Rows[row].Cells[6].Value.ToString();
            cb_BacSi.SelectedValue = dgv_BenhAn.Rows[row].Cells[7].Value.ToString();
            txt_MaBenhAn.Enabled = false;
                
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            bus_benhan.DeleteBenhAn(dgv_BenhAn.Rows[row].Cells[0].Value.ToString());
            FormBacSi_QuanLyBenhAn_Load(sender, e);

        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            clear();
            txt_MaBenhAn.Enabled = true;


        }

        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            String key = txt_TimKiem.Text;
            if(cb_TimKiemTheo.SelectedItem.ToString() == "CCCD")
            {
                if (key != null)
                {
                    dgv_BenhAn.DataSource =  bus_benhan.FindBenhAnByCCCD(key);
                }
            }
            else if(cb_TimKiemTheo.Text == "Mã bác sĩ")
            {
                if (key != null)
                {
                    dgv_BenhAn.DataSource = bus_benhan.FindBenhAnByMaBacSi(key);
                }
            }
            else if(cb_TimKiemTheo.SelectedItem.ToString() == "Mã bệnh án")
            {
                if (key != null)
                {
                    dgv_BenhAn.DataSource =  bus_benhan.FindBenhAnByMaBenhAn(key);
                }
            }
            
        }
    }
}
    