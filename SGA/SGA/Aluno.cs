using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Drawing.Imaging;
using AForge.Video.DirectShow;
using SGA.Model;

namespace SGA
{
    public partial class Aluno : Form
    {
        MySqlConnection conMySQL = new MySqlConnection(@"Data Source=localhost;port=3306;Initial Catalog=sga;User Id=root;password=''");
        MySqlCommand cmdMySQL = new MySqlCommand();
        MySqlDataReader reader;

        string status;

        public DirectX.Capture.Filter Camera;
        public DirectX.Capture.Capture CaptureInfo;
        public DirectX.Capture.Filters CamContainer;
        AForge.Video.DirectShow.VideoCaptureDevice videoSource;
        Image capturaImagem;
        public string caminhoImagemSalva = null;
        string diretorioImagem = @"c:\Users\Marina\Desktop\NILTON\Sistemas\Clientes\SGA\SGA\Img\";

        //PictureBox pbAux;

        //private Pessoa pessoa = new Pessoa();
        //public int dedo = 1;

        //public BiometricSample bioSample1 { get; set; }
        //public BiometricSample bioSample2 { get; set; }
        //public BiometricSample bioSample3 { get; set; }
        //public BiometricTemplate bioTemplate { get; set; }


        public Aluno()
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

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                string xml = "http://cep.republicavirtual.com.br/web_cep.php?cep=@cep&formato=xml".Replace("@cep", txtCEP.Text);

                ds.ReadXml(xml);

                txtRua.Text = ds.Tables[0].Rows[0]["logradouro"].ToString();
                txtBairro.Text = ds.Tables[0].Rows[0]["bairro"].ToString();
                txtCidade.Text = ds.Tables[0].Rows[0]["cidade"].ToString();
                txtEstado.Text = ds.Tables[0].Rows[0]["uf"].ToString();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "CEP não encontrado");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string date = txtDataNascimento.Text;
            var dt = DateTime.Parse(date).ToString("yyyy-MM-dd HH:mm:ss");

            string date2 = txtDataPagamento.Text;
            var dt2 = DateTime.Parse(date2).ToString("yyyy-MM-dd HH:mm:ss");

            try
            {
                if (txtNomeAluno.Text == "")
                {
                    MessageBox.Show("Informe o nome do Aluno.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtNomeAluno.Focus();
                }
                else if (txtEmail.Text == "")
                {
                    MessageBox.Show("Informe o email do Aluno.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtEmail.Focus();
                }
                else
                {
                    if (status == "novo")
                    {

                      

                        cmdMySQL.CommandText = "INSERT INTO aluno(NomeAluno,Email,CPF,Rg,DataNascimento,Telefone1, " +
                        "Telefone2,Cep,Rua,Numero,Bairro,Cidade,Estado,Complemento,DataVencimento,Foto) " +
                        "VALUES('" + txtNomeAluno.Text + "', '" + txtEmail.Text + "', '" + txtCPF.Text + "', " +
                        "'" + txtRG.Text + "', '" + dt + "', '" + txtTelefone1.Text + "', " +
                        "'" + txtTelefone2.Text + "', '" + txtCEP.Text + "', '" + txtRua.Text + "', " +
                        "'" + txtNumero.Text + "', '" + txtBairro.Text + "', '" + txtCidade.Text + "', " +
                        "'" + txtEstado.Text + "', '" + txtComplemento.Text + "', '" + dt2 + "', '" + txtNomeAluno.Text + ".jpg" + "')";

                        cmdMySQL.ExecuteNonQuery();
                        cmdMySQL.Dispose();

                        MessageBox.Show("Aluno salvo com sucesso.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (status == "editar")
                    {

                        cmdMySQL.CommandText = "UPDATE aluno SET NomeAluno='" + txtNomeAluno.Text + "', Email='" + txtEmail.Text +
                            "', CPF='" + txtCPF.Text + "', Rg='" + txtRG.Text + "', DataNascimento='" + dt +
                             "', Telefone1='" + txtTelefone1.Text + "', Telefone2='" + txtTelefone2.Text + "', Cep='" + txtCEP.Text +
                             "', Rua='" + txtRua.Text + "', Numero='" + txtNumero.Text + "', Bairro='" + txtBairro.Text +
                             "', Cidade='" + txtCidade.Text + "', Estado='" + txtEstado.Text + "', Complemento='" + txtComplemento.Text +
                             "', DataVencimento='" + dt2+
                            "' WHERE IdAluno='"+listaAlunos.Items[listaAlunos.FocusedItem.Index].Text+"'";
                        cmdMySQL.ExecuteNonQuery();


                        MessageBox.Show("Registro atualizado com sucesso.", "Atualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    carregaVars();
                    btnLimpar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeAluno.Text = "";
            txtEmail.Text = "";
            txtCPF.Text = "";
            txtRG.Text = "";
            txtTelefone1.Text = "";
            txtTelefone2.Text = "";
            txtCEP.Text = "";
            txtRua.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtEstado.Text = "";
            txtComplemento.Text = "";
            txtDataNascimento.Text = "";
            txtDataPagamento.Text = "";
            txtNomeAluno.Focus();
            status = "novo";
        }

        private void Aluno_Load(object sender, EventArgs e)
        {
            try
            {
                conMySQL.Open();
                status = "novo";
                carregaVars();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //CamContainer = new DirectX.Capture.Filters();
            //try
            //{
            //    int no_of_cam = CamContainer.VideoInputDevices.Count;

            //    for (int i = 0; i < no_of_cam; i++)
            //    {
            //        try
            //        {
            //            // obtém o dispositivo de entrada do vídeo
            //            Camera = CamContainer.VideoInputDevices[i];

            //            // inicializa a Captura usando o dispositivo
            //            CaptureInfo = new DirectX.Capture.Capture(Camera, null);

            //            // Define a janela de visualização do vídeo
            //            CaptureInfo.PreviewWindow = this.picWebCam;

            //            // Capturando o tratamento de evento
            //            CaptureInfo.FrameCaptureComplete += AtualizaImagem;

            //            // Captura o frame do dispositivo
            //            CaptureInfo.CaptureFrame();

            //            // Se o dispositivo foi encontrado e inicializado então sai sem checar o resto
            //            break;
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(this, ex.Message);
            //}


        }

        void carregaVars()
        {
            try
            {
                listaAlunos.Items.Clear();
                if (txtNomeAlunoPesquisar.Text == "")
                {
                    cmdMySQL.CommandText = "SELECT * FROM aluno ORDER BY NomeAluno ASC";
                }
                else
                {
                    cmdMySQL.CommandText = "SELECT * FROM aluno WHERE NomeAluno LIKE '" + txtNomeAlunoPesquisar.Text + "%' ORDER BY NomeAluno ASC";
                }
                reader = cmdMySQL.ExecuteReader();
                while (reader.Read())
                {
                    ListViewItem list = new ListViewItem(reader[0].ToString());
                    list.SubItems.Add(reader[1].ToString());
                    list.SubItems.Add(reader[2].ToString());
                    list.SubItems.Add(reader[3].ToString());
                    list.SubItems.Add(reader[4].ToString());
                    list.SubItems.Add(reader[5].ToString().Replace("00:00:00", ""));
                    list.SubItems.Add(reader[6].ToString());
                    list.SubItems.Add(reader[7].ToString());
                    list.SubItems.Add(reader[8].ToString());
                    list.SubItems.Add(reader[9].ToString());
                    list.SubItems.Add(reader[10].ToString());
                    list.SubItems.Add(reader[11].ToString());
                    list.SubItems.Add(reader[12].ToString());
                    list.SubItems.Add(reader[13].ToString());
                    list.SubItems.Add(reader[14].ToString());
                    list.SubItems.Add(reader[15].ToString().Replace("00:00:00", ""));
                    list.SubItems.Add(reader[16].ToString());
                    listaAlunos.Items.AddRange(new ListViewItem[] { list });
                }
                reader.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtNomeAlunoPesquisar.Text = "";
            carregaVars();
        }

        private void listaAlunos_Click(object sender, EventArgs e)
        {
            try
            {
                txtNomeAluno.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[1].Text;
                txtEmail.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[2].Text;
                txtCPF.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[3].Text;
                txtRG.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[4].Text;
                txtDataNascimento.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[5].Text;
                txtTelefone1.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[6].Text;
                txtTelefone2.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[7].Text;
                txtCEP.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[8].Text;
                txtRua.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[9].Text;
                txtNumero.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[10].Text;
                txtBairro.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[11].Text;
                txtCidade.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[12].Text;
                txtEstado.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[13].Text;
                txtComplemento.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[14].Text;
                txtDataPagamento.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[15].Text;
                //fotoPerfil.ImageLocation = diretorioImagem + listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[16];
                fotoPerfil.ImageLocation = diretorioImagem + txtNomeAluno.Text + ".jpg";
                //fotoPerfil.ImageLocation = diretorioImagem + "Antonio Alves.jpg";
                //fotoPerfil.ImageLocation = "Img\\" + listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[1].Text + "jpg";
                //fotoPerfil.Text = listaAlunos.Items[listaAlunos.FocusedItem.Index].SubItems[16].Text;
                status = "editar";
            }
            catch (Exception) { MessageBox.Show("Não existem registros na lista.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void txtNomeAlunoPesquisar_TextChanged(object sender, EventArgs e)
        {
            carregaVars();
        }
        //public void AtualizaImagem(PictureBox frame)
        //{
        //    try
        //    {
        //        capturaImagem = frame.Image;
        //        this.picImagem.Image = capturaImagem;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Erro " + ex.Message);
        //    }
        //}

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    CaptureInfo.CaptureFrame();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro " + ex.Message);
            //}
            Apagar();
        }

        private void btnSalvarFoto_Click(object sender, EventArgs e)
        {

            TakePhoto();
            //try
            //{
            //    //C:\Users\Marina\Desktop\NILTON\Sistemas\Clientes\SGA\SGA\Img
            //    caminhoImagemSalva = @"c:\Users\Marina\Desktop\NILTON\Sistemas\Clientes\SGA\SGA\Img\" + txtNomeAluno.Text + ".jpg";
            //    picImagem.Image.Save(caminhoImagemSalva, ImageFormat.Jpeg);
            //    MessageBox.Show("Imagem salva com sucesso");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Erro " + ex.Message);
            //}
        }

        private void TakePhoto()
        {
            try
            {
                //webCamPictureBox.Image.Save("snapshot.png", System.Drawing.Imaging.ImageFormat.Png);
                btnSalvarFoto.Enabled = false;
                btnCapturar.Enabled = true;

                //webCamPictureBox.ImageLocation = "snapshot.png";

                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }


                picWebCam.Image.Save(diretorioImagem + txtNomeAluno.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Png);


            }
            catch (Exception ex)
            {
            }
        }

        private void Apagar()
        {
            try
            {
                btnSalvarFoto.Enabled = true;
                btnCapturar.Enabled = false;

                AForge.Video.DirectShow.FilterInfoCollection videosources = new AForge.Video.DirectShow.FilterInfoCollection(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

                if (videosources != null)
                {
                    videoSource = new AForge.Video.DirectShow.VideoCaptureDevice(videosources[0].MonikerString);
                    videoSource.NewFrame += (s, e) => picWebCam.Image = (Bitmap)e.Frame.Clone();

                    videoSource.SetCameraProperty(
                     CameraControlProperty.Focus,
                    500,
                    CameraControlFlags.Manual);
                    videoSource.Start();

                }

            }
            catch (Exception ex)
            {
            }
        }

        private void pbFinger_Click(object sender, EventArgs e)
        {
            
        }       
        private void pbFinger2_Click(object sender, EventArgs e)
        {
            
        }
        private void pbFinger3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
