using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SGA
{
    public partial class Form1 : Form
    {

        MySqlConnection con = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=sga;User Id=root;password=''");
        int i;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from usuarios where Login='"+txtLogin.Text+"' and Senha='"+txtSenha.Text+"'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            i = Convert.ToInt32(dt.Rows.Count.ToString());


            if (i == 0)
            {
                MessageBox.Show("Usuário ou Senha Incorretos", "Login", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }


            else
            {
                MessageBox.Show("Olá!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                this.Hide();
                Form f = new Inicio();
                f.Closed += (s, args) => this.Close();
                f.Show();
            }

            con.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new frmInicioAluno();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
