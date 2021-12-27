using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Media;
using System.Threading;

namespace tablet
{
    public partial class Form1 : Form
    {
        Erro e = new Erro();
        public Form1()
        {
            InitializeComponent();
            runAlerta();
        }
        public void runAlerta()
        {
            bool verificar = false;
            var nome = Environment.MachineName;
            string query = "INSERT INTO alerta(`TT`,`status`,`relatorio`, `usuario`) VALUES (@nome,'1','','')";

            //------CONFIGURAÇÃO PARA CONECTAR AO BANCO DE DADOS----- //

            string MySQLConnectionString = "datasource=ipdobancodedados;port=3306;username=usuariobancodedados;password=senhadobanco;database=bancodedados;Connect Timeout=2";

            MySqlConnection databaseConnection = new MySqlConnection(MySQLConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.Parameters.AddWithValue("@nome", nome);
            commandDatabase.CommandTimeout = 2;


            //-----------CONFIGURAÇÃO DE TIMEOUT-----------//

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    databaseConnection.Open();
                    commandDatabase.ExecuteNonQuery();
                    verificar = true;
                    break;
                }
                catch
                {
                    Thread.Sleep(3000);
                    continue;
                }
            }
            if(verificar == false)
            {
                SystemSounds.Exclamation.Play();
                e.ShowDialog();
                Environment.Exit(0);
            }
        }

    }
}
