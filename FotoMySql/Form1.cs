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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadastro cad = new frmCadastro();
            cad.Show();
        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            string stringConexao = "Server=localhost;Database=teste;Uid=root;Pwd=12345678;";
            MySqlConnection conexao = new MySqlConnection(stringConexao);
            MySqlCommand comando = new MySqlCommand("SELECT * FROM cadastro ORDER BY RAND() LIMIT 1",conexao);
            MySqlDataReader reader;
            try
            {
                conexao.Open();
                reader = comando.ExecuteReader();
                while (reader.Read()) 
                {
                    string nome = reader.GetString("nome");
                    txtNome.Text = nome.ToString();
                    byte[] imagem = (byte[])(reader["fotos"]);
                    if (imagem == null) 
                    {
                        pictureBox1.Image = null;
                    }
                    else 
                    {
                        MemoryStream memoryStream = new MemoryStream(imagem);
                        pictureBox1.Image = System.Drawing.Image.FromStream(memoryStream);
                    }
                    MessageBox.Show("Parabens " + nome.ToString() + "!");
                }
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
