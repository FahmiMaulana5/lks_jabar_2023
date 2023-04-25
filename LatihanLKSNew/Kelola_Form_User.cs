using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LatihanLKSNew
{
    public partial class Kelola_Form_User : Form
    {
        ClassFunction f = new ClassFunction();

        public Kelola_Form_User()
        {
            InitializeComponent();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        void fresh()
        {
            cbTipeUser.Text = string.Empty;
            txtNama.Text = string.Empty;
            txtTelepon.Text = string.Empty;
            txtAlamat.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            f.showDg("select * from tbl_user", dgUser);
        }

        private void Kelola_Form_User_Load(object sender, EventArgs e)
        {
            f.showDg("select * from tbl_user", dgUser);
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                f.cmd("insert into tbl_user (tipe_user, nama, telpon, alamat, username, password) values ('"+cbTipeUser.Text+"', '"+txtNama.Text+"', '"+txtTelepon.Text+"', '"+txtAlamat.Text+"', '"+txtUsername.Text+"', '"+txtPassword.Text+"')");
                fresh();
                MessageBox.Show("tambah data sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                if (cbTipeUser.Text == "" || txtNama.Text == "" || txtTelepon.Text == "" || txtAlamat.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    f.cmd("update tbl_user set tipe_user = '"+cbTipeUser.Text+"', nama = '"+txtNama.Text+"', telpon = '"+txtTelepon.Text+"', alamat = '"+txtAlamat.Text+"', password = '"+txtPassword.Text+"' where username = '"+txtUsername.Text+"'");
                    fresh();
                    MessageBox.Show("edit data sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                if (cbTipeUser.Text == "" || txtNama.Text == "" || txtTelepon.Text == "" || txtAlamat.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    f.cmd("delete from tbl_user where username = '"+txtUsername.Text+"'");
                    fresh();
                    MessageBox.Show("hapus data sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            f.showDg("select * from tbl_user where nama like '"+txtCari.Text+"%'", dgUser);
        }

        private void dgUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dgUser.Rows[e.RowIndex];

            cbTipeUser.Text = dr.Cells[1].Value.ToString();
            txtNama.Text = dr.Cells[2].Value.ToString();
            txtAlamat.Text = dr.Cells[3].Value.ToString();
            txtTelepon.Text = dr.Cells[4].Value.ToString();
            txtUsername.Text = dr.Cells[5].Value.ToString();
            txtPassword.Text = dr.Cells[6].Value.ToString();
        }
    }
}
