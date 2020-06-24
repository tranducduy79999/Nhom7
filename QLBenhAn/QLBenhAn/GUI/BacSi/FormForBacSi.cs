using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.BacSi
{
    public partial class FormForBacSi : Form
    {
        public FormForBacSi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormBacSi_QuanLyBenhAn frm = new FormBacSi_QuanLyBenhAn() { TopLevel = false};
            

            panelBacSi.Controls.Add(frm);
            frm.Show();
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormBacSi_QuanLyBenhNhan frm1 = new FormBacSi_QuanLyBenhNhan() { TopLevel = false };


            panelBacSi.Controls.Add(frm1);
            frm1.Show();
        }
    }  
}
