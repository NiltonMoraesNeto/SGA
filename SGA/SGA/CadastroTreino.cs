using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGA
{
    public partial class CadastroTreino : Form
    {

        MySqlConnection conMySQL = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=sga;User Id=root;password=''");
        MySqlCommand cmdMySQL = new MySqlCommand();
        MySqlDataReader reader;


        public CadastroTreino()
        {
            InitializeComponent();
            cmdMySQL.Connection = conMySQL;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Inicio();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void CadastroTreino_Load(object sender, EventArgs e)
        {
            conMySQL.Open();
            cmdMySQL = conMySQL.CreateCommand();
            cmdMySQL.CommandType = CommandType.Text;
            cmdMySQL.CommandText = "SELECT * from aluno";
            cmdMySQL.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmdMySQL);
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["NomeAluno"].ToString());
            }

            conMySQL.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var a = comboBox1.ValueMember;


            conMySQL.Open();
            cmdMySQL.CommandText = "INSERT INTO treino(IdAluno,Descricao1,Qtd1,Descricao2,Qtd2,Descricao3, " +
           "Qtd3,Descricao4,Qtd4) " +
           "VALUES('" + comboBox1.ValueMember + "', '" + txtDescricao1.Text + "', '" + txtQtd1.Text + "', " +
           "'" + txtDescricao2.Text + "', '" + txtQtd2.Text + "', '" + txtDescricao3.Text + "', " +
           "'" + txtQtd3.Text + "', '" + txtDescricao4.Text + "', '" + txtQtd4.Text + "')";

            cmdMySQL.ExecuteNonQuery();
            cmdMySQL.Dispose();



            MessageBox.Show("TREINO salvo com sucesso.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}