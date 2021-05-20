using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FotoMySql
{
    public partial class frmCadastro : Form
    {
        public frmCadastro()
        {
            InitializeComponent();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|AllFiles(*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK) 
            {
                string foto = dialog.FileName.ToString();
                txtImagem.Text = foto;
                pictureBox1.ImageLocation = foto;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            byte[] imagemByte = null;
            FileStream fileStream = new FileStream(this.txtImagem.Text,FileMode.Open,FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            imagemByte = binaryReader.ReadBytes((int)fileStream.Length);
            string sintax = "INSERT INTO cadastro (nome,fotos) VALUES (@nome,@fotos)";
            string stringConexao = "Server=localhost;Database=teste;Uid=root;Pwd=12345678;";
            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand(sintax, conexao);
            MySqlDataReader reader;
            try
            {
                conexao.Open();
                comando.Parameters.Add(new MySqlParameter("@nome", txtNome.Text));
                comando.Parameters.Add(new MySqlParameter("@fotos", imagemByte));
                reader = comando.ExecuteReader();
                MessageBox.Show("jogador salvo");
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }
            finally 
            {
                conexao.Close();
            }
        }
    }
}
