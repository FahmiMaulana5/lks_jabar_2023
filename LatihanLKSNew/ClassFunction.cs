using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LatihanLKSNew
{
    internal class ClassFunction
    {
        public static string ds = "Data Source=DESKTOP-10VNTUJ;Initial Catalog=db_foodxyz;Integrated Security=True";
        SqlConnection conn = new SqlConnection(ds);
        private Form activateForm;
        public static string username = "";
        public static string id_user = "";

        public void openChildForm(Form childForm, Panel panel, object btnSender)
        {
            if(activateForm != null)
            {
                activateForm.Close();
            }

            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel.Controls.Add(childForm);
            panel.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();
        }

        public void cmd(string query)
        {
            try
            {
                if(conn.State == ConnectionState.Closed) conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conn.Close();
            }
        }

        public void showDg(string query, DataGridView dg)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                sda.Fill(dt);

                dg.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public void validationText(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
