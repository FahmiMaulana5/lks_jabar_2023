using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace LatihanLKSNew
{
    public partial class Form_Admin : Form
    {
        ClassFunction f = new ClassFunction();
        DateTime date;
        string dateS;
        public Form_Admin()
        {
            InitializeComponent();
        }

        private void btnKelolaUser_Click(object sender, EventArgs e)
        {
            f.openChildForm(new Kelola_Form_User(), panel2, sender);
        }

        private void btnKelolaLaporan_Click(object sender, EventArgs e)
        {
            f.openChildForm(new Kelola_Form_Laporan(), panel2, sender);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            f.cmd("insert into tbl_log (waktu, aktivitas, id_user) values (current_timestamp, 'Logout', '" + ClassFunction.id_user + "')");
            this.Close();
            new Form1().Show();
        }

        private void Form_Admin_Load(object sender, EventArgs e)
        {
            f.showDg("select a.id_log as ID_Log, b.username as Username, format(a.waktu, 'dd/MM/yyyy HH:mm') as waktu, a.aktivitas as Aktivitas from tbl_log a join tbl_user b on a.id_user = b.id_user", dgLog);
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            f.showDg("select a.id_log as ID_Log, b.username as Username, format(a.waktu, 'dd/MM/yyyy HH:mm') as waktu, a.aktivitas as Aktivitas from tbl_log a join tbl_user b on a.id_user = b.id_user where convert(date, a.waktu) like '"+dateS+"%'", dgLog);
        }

        private void dtTanggal_ValueChanged(object sender, EventArgs e)
        {
            date = dtTanggal.Value;
            dateS = date.ToString("yyyy-MM-dd");
        }
    }
}
