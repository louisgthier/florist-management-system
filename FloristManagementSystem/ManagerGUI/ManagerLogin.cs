namespace ManagerGUI
{   using MySql.Data.MySqlClient;
    using System.ComponentModel.Design.Serialization;
    using System.Diagnostics.Eventing.Reader;
    using UtilityLibrary;
    MySQLConnection connect = new MySQLConnection("localhost", 3306, "florist", 'root', 'root');



    public partial class ManagerLogin : Form
    {
        public ManagerLogin()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MySQLUtil.LoginAsUser("localhost", 3306, "florist", textBox1.Text, textBox2.Text))
            {
                if (textBox1.Text == "root")
                {
                    // Création de la deuxième page
                    ManagerMenu form2 = new ManagerMenu();
                    form2.Show();
                    this.Hide();
                }
                else
                {

                    MessageBox.Show("Vous n'êtes pas le gérant !");

                }
            }
            else
            {
                MessageBox.Show("Identifiants incorrects !");
            }
        }
        public void AffichageClient() 
        {
        }
    }
}