using System;
using UtilityLibrary;
using System.Threading;

namespace ClientCLI
{
	public class RegisterMenu: Menu
	{
		public RegisterMenu(): base("Register")
		{ }

		public override async void Show(string message=null)
		{

			string email = RequestString("Enter email:", inputValidator: MySQLUtil.CheckEmailAvailable);

            string password;
            string passwordConfirmation = null;

            do
            {
                password = RequestString((passwordConfirmation != null ? "Passwords were not the same\n" : "") + "Enter a new password for " + email + ":", inputValidator: x => MySQLUtil.CheckLength(x, 8));
                passwordConfirmation = RequestString("Confirm password:");
            } while (passwordConfirmation != password);

            string prenom = RequestString("Enter your first name:");

            string nom = RequestString("Enter your last name:");

            string numTel = RequestString("Enter your phone number:");

            string adresse = RequestString("Enter your adresse:");

			string numCb = RequestString("Enter your credit card number:");

            MySQLUtil.RegisterClient(email, password, nom, prenom, numTel, adresse, numCb);

            Thread.Sleep(2000);

            Program.MainMenu.Show();

		}
	}
}

