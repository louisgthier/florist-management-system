namespace ManagerGUI
{
    using System.Diagnostics.Eventing.Reader;
    using UtilityLibrary;

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
                    // Cr�ation de la deuxi�me page
                    ManagerMenu form2 = new ManagerMenu();
                    form2.Show();
                    this.Hide();
                }
                else
                {

                    MessageBox.Show("Vous n'�tes pas le g�rant !");

                }
            }
            else
            {
                MessageBox.Show("Identifiants incorrects !");
            }
        }
    }
}