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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void imgCadastrarCliente_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Aluno();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form f = new CadastroTreino();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form f = new Form1();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}
