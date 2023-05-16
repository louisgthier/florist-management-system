using System;
using UtilityLibrary;
namespace ClientCLI.Menus
{
	public class LoginMenu: Menu
	{
		public LoginMenu() : base("Login")
		{ }

		public override Menu Show()
		{

			string email;
			if (!RequestString("Enter email:", out email, inputValidator: MySQLUtil.CheckEmailExists))
				return Program.MainMenu;

			string password;
			if (!RequestString("Enter password:", out password))
				return Program.MainMenu;

			if (MySQLUtil.LoginAsUser("localhost", 3306, "florist", email, password))
			{
				Program.MainMenu = Program.Menus[MenuID.MainMenuAuthenticated];
				Program.MainMenu.name = "Main Menu - " + email;
				return Program.MainMenu;
            }
			else
			{
				Program.MainMenu.message = "Wrong credentials";
                return Program.MainMenu;
            }
			

		}
	}
}

