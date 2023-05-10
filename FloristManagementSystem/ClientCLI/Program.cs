using ClientCLI;
using UtilityLibrary;
using MySql.Data.MySqlClient;
// See https://aka.ms/new-console-template for more information


// Define menus
Menus[MenuID.MainMenu] = new Menu("Main Menu");
Menus[MenuID.MainMenuAuthenticated] = new Menu("Main Menu");;
Menus[MenuID.Login] = new LoginMenu();
Menus[MenuID.Register] = new RegisterMenu();

Program.MainMenu = Menus[MenuID.MainMenu];
Menus[MenuID.MainMenu].options.Add(Menus[MenuID.Login]);
Menus[MenuID.MainMenu].options.Add(Menus[MenuID.Register]);

Menus[MenuID.Order] = new Menu("Order");
Menus[MenuID.OrderStandardBouquet] = new Menu("Order standard bouquet");
Menus[MenuID.OrderFlowerArrangement] = new Menu("Order flower arrangement");
Menus[MenuID.Order].options.Add(Menus[MenuID.OrderStandardBouquet]);
Menus[MenuID.Order].options.Add(Menus[MenuID.OrderFlowerArrangement]);

Menus[MenuID.MainMenuAuthenticated].options.Add(Menus[MenuID.Order]);
Menus[MenuID.MainMenuAuthenticated].options.Add(new Menu("History"));
Menus[MenuID.MainMenuAuthenticated].options.Add(new Menu("Fidelity status"));


MySQLUtil.StartConnection("localhost", 3306, "florist", "florist_user", "password");

Menus[MenuID.MainMenu].Show();

static partial class Program
{
    public static Menu MainMenu;
    public static Dictionary<MenuID, Menu> Menus = new Dictionary<MenuID, Menu>();
}

public enum MenuID
{
    MainMenu,
    Login,
    Register,
    MainMenuAuthenticated,
    Order,
    OrderFlowerArrangement,
    OrderStandardBouquet
}