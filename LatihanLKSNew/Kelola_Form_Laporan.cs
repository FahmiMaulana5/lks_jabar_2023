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
    public partial class Kelola_Form_Laporan : Form
    {
        ClassFunction f = new ClassFunction();
        SqlConnection conn = new SqlConnection(ClassFunction.ds);
        DateTime tgl1;
        string tglS1;
        DateTime tgl2;
        string tglS2;

        public Kelola_Form_Laporan()
        {
            InitializeComponent();
        }

        private void Kelola_Form_Laporan_Load(object sender, EventArgs e)
        {
            f.showDg("select a.id_transaksi, a.tgl_transaksi, a.total_bayar, b.nama from tbl_transaksi a join tbl_user b on a.id_user = b.id_user", dgTransaksi);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select format(tgl_transaksi, 'dd/MM/yyyy') as tgl_transaksi, total_bayar from tbl_transaksi order by tgl_transaksi", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            chart1.DataSource = dt;

            chart1.Series[0].XValueMember = "tgl_transaksi";
            chart1.Series[0].YValueMembers = "total_bayar";

            chart1.DataBind();
            conn.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            f.showDg("select a.id_transaksi, a.tgl_transaksi, a.total_bayar, b.nama from tbl_transaksi a join tbl_user b on a.id_user = b.id_user where a.tgl_transaksi between '"+tglS1+"' and '"+tglS2+"'", dgTransaksi);
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select format(tgl_transaksi, 'dd/MM/yyyy') as tgl_transaksi, total_bayar from tbl_transaksi where tgl_transaksi between '"+tglS1+"' and '"+tglS2+"' order by tgl_transaksi", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            chart1.DataSource = dt;

            chart1.Series[0].XValueMember = "tgl_transaksi";
            chart1.Series[0].YValueMembers = "total_bayar";

            chart1.DataBind();
            conn.Close();
        }

        private void dtTanggal1_ValueChanged(object sender, EventArgs e)
        {
            tgl1 = dtTanggal1.Value;
            tglS1 = tgl1.ToString("yyyy-MM-dd");
        }

        private void dtTanggal2_ValueChanged(object sender, EventArgs e)
        {
            tgl2 = dtTanggal2.Value;
            tglS2 = tgl2.ToString("yyyy-MM-dd");
        }
    }
}
