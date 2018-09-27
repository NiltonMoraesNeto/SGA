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
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SGA
{
    public partial class Treino : Form
    {

        private string _strConn = (@"Data Source=localhost;port=3306;Initial Catalog=sga;User Id=root;password=''");
        MySqlCommand objComand = null;
        MySqlDataReader reader;

        MySqlConnection objConect = null;


        public Treino()
        {
            InitializeComponent();
            //cmdMySQL.Connection = conMySQL;
        }

               
        private void button1_Click(object sender, EventArgs e)
        {
            listaGrid();
        }



        //void carregaVars()
        //{

        //    InitializeComponent();

        //    var motorForm = new frmInicioAluno();
        //    var potencia = motorForm.txtCodAluno.Text;

        //    try
        //    {
        //        listaAlunosTreino.Items.Clear();
        //        if (potencia == "")
        //        {
        //            cmdMySQL.CommandText = "SELECT * FROM treino";
        //        }
        //        else
        //        {
        //            cmdMySQL.CommandText = "SELECT * FROM treino WHERE IdAluno = " + potencia;
        //        }
        //        reader = cmdMySQL.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            ListViewItem list = new ListViewItem(reader[0].ToString());
        //            list.SubItems.Add(reader[1].ToString());
        //            list.SubItems.Add(reader[2].ToString());
        //            list.SubItems.Add(reader[3].ToString());
        //            list.SubItems.Add(reader[4].ToString());
        //            list.SubItems.Add(reader[5].ToString());
        //            list.SubItems.Add(reader[6].ToString());
        //            list.SubItems.Add(reader[7].ToString());
        //            list.SubItems.Add(reader[8].ToString());
        //            list.SubItems.Add(reader[9].ToString());
                    
        //            listaAlunosTreino.Items.AddRange(new ListViewItem[] { list });
        //        }
        //        reader.Close();
        //    }
        //    catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        //}

        private void Treino_Load(object sender, EventArgs e)
        {
            //listaGrid();
        }

        


        public void listaGrid()
        {
            //conMySQL.Open();
            //cmdMySQL.CommandText = "SELECT * FROM treino";

            string sql = "SELECT * FROM treino";

            objConect = new MySqlConnection(_strConn);
            objComand = new MySqlCommand(sql,objConect);


            try
            {
                MySqlDataAdapter my = new MySqlDataAdapter(objComand);

                DataTable dtLista = new DataTable();

                my.Fill(dtLista);

                dataGridView1.DataSource = dtLista;


            }
            catch
            {

                MessageBox.Show("Deu erro");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Inicio();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
