using System;
using UtilityLibrary;
using System.Threading;

namespace ClientCLI.Menus
{
	public class RegisterMenu: Menu
	{
		public RegisterMenu(): base("Register")
		{ }

		public override Menu Show()
		{

            string email;
            if (!RequestString("Enter email:", out email, inputValidator: MySQLUtil.CheckEmailAvailable))
                return Program.MainMenu;

            string password;
            string passwordConfirmation = null;

            do
            {
                if (!RequestString((passwordConfirmation != null ? "Passwords were not the same\n" : "") + "Enter a new password for " + email + ":", out password, inputValidator: x => MySQLUtil.CheckLength(x, 8)))
                    return Program.MainMenu;
                if (!RequestString("Confirm password:", out passwordConfirmation))
                    return Program.MainMenu;
            } while (passwordConfirmation != password);

            string prenom;
            if (!RequestString("Enter your first name:", out prenom))
                return Program.MainMenu;

            string nom;
            if (!RequestString("Enter your last name:", out nom))
                return Program.MainMenu;

            string numTel;
            if (!RequestString("Enter your phone number:", out numTel))
                return Program.MainMenu;

            string address;
            if (!RequestString("Enter your adresse:", out address))
                return Program.MainMenu;

            string numCb;
            if (!RequestString("Enter your credit card number:", out numCb))
                return Program.MainMenu;

            MySQLUtil.RegisterClient(email, password, nom, prenom, numTel, address, numCb);

            Thread.Sleep(2000);

            return Program.MainMenu; ;

		}
	}
}

