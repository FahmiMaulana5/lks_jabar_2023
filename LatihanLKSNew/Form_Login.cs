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
    public partial class Form1 : Form
    {
        ClassFunction f = new ClassFunction();
        SqlConnection conn = new SqlConnection(ClassFunction.ds);

        void login()
        {
            try
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select id_user, tipe_user, nama from tbl_user where username = '"+txtUsername.Text+"' and password = '"+txtPassword.Text+"'", conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ClassFunction.username = dr["nama"].ToString();
                        ClassFunction.id_user = dr["id_user"].ToString();

                        f.cmd("insert into tbl_log(waktu, aktivitas, id_user) values (current_timestamp, 'Login', '" + ClassFunction.id_user+"')");

                        MessageBox.Show("login sukses !", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (dr["tipe_user"].ToString() == "Admin")
                        {
                            this.Hide();
                            new Form_Admin().Show();
                        }
                        else if (dr["tipe_user"].ToString() == "Gudang")
                        {
                            this.Hide();
                            new Kelola_Form_Barang().Show();
                        }else if (dr["tipe_user"].ToString() == "Kasir")
                        {
                            this.Hide();
                            new Kelola_Form_Transaksi().Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("username atau password yang anda masukkan tidak sesuai !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }finally 
            { 
                conn.Close();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Semua kolom harus di isi!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                login();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            txtUsername.Text=string.Empty;
            txtPassword.Text=string.Empty;
        }
    }
}
