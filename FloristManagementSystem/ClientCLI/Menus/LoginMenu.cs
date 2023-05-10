using System;
using UtilityLibrary;
namespace ClientCLI
{
	public class LoginMenu: Menu
	{
		public LoginMenu() : base("Login")
		{ }

		public override void Show(string message=null)
		{

			string email = RequestString("Enter email:", inputValidator: MySQLUtil.CheckEmailExists);

            string password = RequestString("Enter password:");

			if (MySQLUtil.LoginAsUser("localhost", 3306, "florist", email, password))
			{
				Program.MainMenu = Program.Menus[MenuID.MainMenuAuthenticated];
				Program.MainMenu.name = "Main Menu - " + email;
                Program.MainMenu.Show();
            }
			else
			{
                Program.MainMenu.Show(message: "Wrong credentials");
            }
			

		}
	}
}

