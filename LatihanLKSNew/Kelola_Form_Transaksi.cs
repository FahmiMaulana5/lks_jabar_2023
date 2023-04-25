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
    public partial class Kelola_Form_Transaksi : Form
    {
        DateTime time = DateTime.Now;
        ClassFunction f = new ClassFunction();
        int i = 0;
        SqlConnection conn = new SqlConnection(ClassFunction.ds);
        string kode = "";
        string nama = "";
        public static int IdTr = 0;

        void getBarang()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select kode_barang, nama_barang from tbl_barang", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                kode = reader["kode_barang"].ToString();
                nama = reader["nama_barang"].ToString();
                cbMenu.Items.Add(kode + "-" + nama);
            }

            reader.Close();
            conn.Close();
        }

        string GetHarga()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select harga_satuan from tbl_barang where kode_barang like '" + cbMenu.Text.Split('-')[0].Trim() + "%'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string harga_satuan = "";

            if (reader.Read())
            {
                harga_satuan = reader["harga_satuan"].ToString();
            }

            conn.Close();
            reader.Close();

            return harga_satuan;
        }

        void getId()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select max(id_transaksi) as id from tbl_transaksi", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int result = 0;
                int.TryParse(reader["id"].ToString(), out result);

                IdTr = result + 1;
            }

            reader.Close();
            conn.Close();
        }

        void addTroli()
        {
            string kode = cbMenu.Text.Split('-')[0].Trim();
            string nama = cbMenu.Text.Split('-')[1].Trim();
            string harga = txtHarga.Text;
            string quantitas = txtQuantitas.Text;
            string total = txtTotal.Text;

            dataGridView1.Rows.Add();
            dataGridView1.Rows[i].Cells[0].Value = IdTr;
            dataGridView1.Rows[i].Cells[1].Value = kode;
            dataGridView1.Rows[i].Cells[2].Value = nama;
            dataGridView1.Rows[i].Cells[3].Value = harga;
            dataGridView1.Rows[i].Cells[4].Value = quantitas;
            dataGridView1.Rows[i].Cells[5].Value = total;

            i++;
            IdTr++;

        }

        void addDb()
        {
            try
            {
                conn.Open();
                foreach(DataGridViewRow dr in dataGridView1.Rows)
                {
                    if (dr.Cells[0].Value != null || dr.Cells[1].Value != null || dr.Cells[2].Value != null || dr.Cells[3].Value != null || dr.Cells[4].Value != null || dr.Cells[5].Value != null)
                    {
                        string no = dr.Cells[0].Value.ToString();
                        string total = dr.Cells[5].Value.ToString();
                        string kode = dr.Cells[1].Value.ToString();

                        f.cmd("insert into tbl_transaksi (no_transaksi, tgl_transaksi, total_bayar, id_user, id_barang) values ('" + no + "', getdate(), '" + total + "', '" + ClassFunction.id_user + "', (select id_barang from tbl_barang where kode_barang = '" + kode + "'))");
                    }
                    
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally 
            {
                conn.Close();
            }
        }

        void inVoice()
        {
            DataSet1 ds = new DataSet1();
            DataTable dt = new DataTable();

            dt = ds.Tables["DataTable1"];

            for (int i = 0; i < dataGridView1.Rows.Count - 1 ; i++)
            {
                dt.Rows.Add(
                    dataGridView1.Rows[i].Cells[0].Value,
                    dataGridView1.Rows[i].Cells[1].Value,
                    dataGridView1.Rows[i].Cells[2].Value,
                    dataGridView1.Rows[i].Cells[3].Value,
                    dataGridView1.Rows[i].Cells[4].Value,
                    dataGridView1.Rows[i].Cells[5].Value
                );
            }

            Invoice_Form invoice = new Invoice_Form();

            invoice.reportViewer1.LocalReport.ReportPath = "D:\\dev\\PBO\\LatihanLKSNew\\LatihanLKSNew\\Report1.rdlc";
            invoice.reportViewer1.LocalReport.DataSources.Clear();
            invoice.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("myDataSet1", dt));

            invoice.Show();
            invoice.reportViewer1.Refresh();
            
        }

        void reFresh()
        {
            cbMenu.Text = string.Empty;
            txtHarga.Text = string.Empty;
            txtQuantitas.Text = string.Empty;
            txtTotal.Text = string.Empty;
        }

        public Kelola_Form_Transaksi()
        {
            InitializeComponent();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            f.cmd("insert into tbl_log (waktu, aktivitas, id_user) values (current_timestamp, 'Logout', '" + ClassFunction.id_user + "')");
            this.Close();
            new Form1().Show();
        }

        private void Kelola_Form_Transaksi_Load(object sender, EventArgs e)
        {
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            lblUsername.Text = ClassFunction.username;
            lblDatetime.Text = time.ToString();
            
            getBarang();
            getId();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {
            double a;
            double b;

            Double.TryParse(txtHarga.Text, out a);
            Double.TryParse(txtQuantitas.Text, out b);

            double result = a * b;

            txtTotal.Text = result.ToString();
            lblTotalHarga.Text = "Rp." + " " + result.ToString();
        }

        private void txtBayar_TextChanged(object sender, EventArgs e)
        {
            double a;
            double b;

            Double.TryParse(txtBayar.Text, out a);
            Double.TryParse(txtTotal.Text, out b);

            double result = a - b;

            lblKembali.Text = "Rp." + " " + result.ToString();
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            txtBayar.Text = string.Empty;
            lblTotalHarga.Text= "Rp. 0";
            lblKembali.Text = "Rp. 0";
        }

        private void cbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHarga.Text = GetHarga();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                addTroli();
                reFresh();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                i = 0;
                reFresh();
            }
        }

        private void Kelola_Form_Transaksi_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                addDb();
                dataGridView1.Rows.Clear();
                i = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DialogResult d;
            d = MessageBox.Show("Apakah anda yakin?", "Pertanyaan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                inVoice();
            }
        }

        private void txtQuantitas_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.validationText(sender, e);
        }

        private void txtBayar_KeyPress(object sender, KeyPressEventArgs e)
        {
            f.validationText(sender, e);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                if(e.Value != null)
                {
                    string tran = e.Value.ToString();
                    if(tran.Length == 1)
                    {
                        tran = "00" + tran;
                    }else if(tran.Length == 2)
                    {
                        tran = "0" + tran;
                    }

                    e.Value = "TR" + tran;
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
