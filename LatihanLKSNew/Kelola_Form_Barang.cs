using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LatihanLKSNew
{
    public partial class Kelola_Form_Barang : Form
    {
        ClassFunction f = new ClassFunction();
        SqlConnection conn = new SqlConnection(ClassFunction.ds);
        DateTime tgl;
        string tglS;

        public Kelola_Form_Barang()
        {
            InitializeComponent();
        }

        void reFresh()
        {
            txtKode.Text = string.Empty;
            txtNama.Text = string.Empty;
            txtHarga.Text = string.Empty;
            txtJumlah.Text = string.Empty;
            dtTanggal.Text = string.Empty;
            cbSatuan.Text = string.Empty;
            f.showDg("select * from tbl_barang", dgBarang);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(d == DialogResult.Yes)
            {
                f.cmd("insert into tbl_barang (kode_barang, nama_barang, expired_date, jumlah_barang, satuan, harga_satuan) values ('"+txtKode.Text+"', '"+txtNama.Text+"', '"+tglS+"', '"+txtJumlah.Text+"', '"+cbSatuan.Text+"', '"+txtHarga.Text+"')");
                MessageBox.Show("tambah data sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reFresh();
            }
        }

        private void Kelola_Form_Barang_Load(object sender, EventArgs e)
        {
            dgBarang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            f.showDg("select * from tbl_barang", dgBarang);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            f.cmd("insert into tbl_log (waktu, aktivitas, id_user) values (current_timestamp, 'Logout', '" + ClassFunction.id_user + "')");
            this.Close();
            new Form1().Show();
        }

        private void txtCari_TextChanged(object sender, EventArgs e)
        {
            f.showDg("select * from tbl_barang where nama_barang like '"+txtCari.Text+"%'", dgBarang);
        }

        private void dtTanggal_ValueChanged(object sender, EventArgs e)
        {
            tgl = dtTanggal.Value;
            tglS = tgl.ToString("yyyy-MM-dd");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                if(txtKode.Text == "" || txtNama.Text == "" ||  txtHarga.Text == "" || txtJumlah.Text == "" || dtTanggal.Text == "" || cbSatuan.Text == "")
                {
                    MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    f.cmd("update tbl_barang set nama_barang = '"+txtNama.Text+"', expired_date = '"+tglS+"', jumlah_barang = '"+txtJumlah.Text+"', satuan = '"+cbSatuan.Text+"', harga_satuan = '"+txtHarga.Text+"' where kode_barang = '"+txtKode.Text+"'");
                    reFresh();
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                if (txtKode.Text == "" || txtNama.Text == "" || txtHarga.Text == "" || txtJumlah.Text == "" || dtTanggal.Text == "" || cbSatuan.Text == "")
                {
                    MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    f.cmd("delete from tbl_barang where kode_barang = '"+txtKode.Text+"'");
                    reFresh();
                }
            }
        }

        private void txtJumlah_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.validationText(sender, e);
        }

        private void txtHarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.validationText(sender, e);
        }

        private void dgBarang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgBarang.Columns[e.ColumnIndex].Name == "id_barang")
            {
                if(e.Value != null)
                {
                    string idBrg = e.Value.ToString();
                    if(idBrg.Length == 1)
                    {
                        idBrg = "0" + idBrg;
                    }

                    e.Value = "BRG" + idBrg;
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = this.dgBarang.Rows[e.RowIndex];

            txtKode.Text = dr.Cells[1].Value.ToString();
            txtNama.Text = dr.Cells[2].Value.ToString();
            dtTanggal.Text = dr.Cells[3].Value.ToString();
            txtJumlah.Text = dr.Cells[4].Value.ToString();
            cbSatuan.Text = dr.Cells[5].Value.ToString();
            txtHarga.Text = dr.Cells[6].Value.ToString();
        }
    }
}
