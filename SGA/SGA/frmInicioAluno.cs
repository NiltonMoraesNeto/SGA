using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SGA
{
    public partial class frmInicioAluno : Form
    {

        MySqlConnection conMySQL = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=sga;User Id=root;password=''");
        MySqlCommand cmdMySQL = new MySqlCommand();
        MySqlDataReader reader;

        public frmInicioAluno()
        {
            InitializeComponent();
        }

        private void frmInicioAluno_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
            label4.Text = DateTime.Now.ToLongDateString();
        }

        private void btnCodAluno_Click(object sender, EventArgs e)
        {
            //if (txtCodAluno.Text != "")
            //{
            //    cmdMySQL.CommandText = "SELECT * FROM `sga`.`treino` WHERE IdAluno ='" + txtCodAluno.Text + "'";

            //    cmdMySQL.ExecuteNonQuery();
            //    cmdMySQL.Dispose();

            //}

            this.Hide();
            Form f = new Treino();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        
    }
}
