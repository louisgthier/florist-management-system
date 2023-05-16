using System;
using UtilityLibrary;

namespace ClientCLI.Menus
{
	public class OrderStandardBouquetMenu: Menu
	{
		public OrderStandardBouquetMenu() : base("Order a standard bouquet")
        {
		}

        void PrintMenu(List<StandardBouquet> bouquets)
        {
            Console.Clear();

            Console.WriteLine(name);
            Console.WriteLine(0 + ". " + "Cancel order");
            int i = 1;
            foreach (StandardBouquet bouquet in bouquets)
            {
                Console.WriteLine(i + ". " + bouquet.Name + " ("+bouquet.Price+"€)" + ": " + bouquet.Description);
                i++;
            }
        }

        public override Menu Show()
        {

            List<StandardBouquet> bouquets = MySQLUtil.GetStandardBouquets();
            
            PrintMenu(bouquets);
            int? selectedOption = null;

            while (selectedOption is null)
            {
                Console.WriteLine("\nSelect option:");
                try
                {
                    selectedOption = int.Parse(Console.ReadLine());
                    if (selectedOption > bouquets.Count || selectedOption < 0)
                    {
                        selectedOption = null;
                    }
                    else
                    {
                        break;
                    }
                }
                catch (System.FormatException)
                {

                }
                PrintMenu(bouquets);
                Console.WriteLine("\nIncorrect option!");
            }

            if (selectedOption == 0)
            {
                return Program.MainMenu;
            }

            StandardBouquet selectedBouquet = bouquets[(int)selectedOption - 1];

            string deliveryAddress;
            if (!RequestString("Delivery address:", out deliveryAddress))
                return Program.MainMenu;
            string messageForDesigner;
            if (!RequestString("Message for the bouquet designer:", out messageForDesigner))
                return Program.MainMenu;
            string deliveryDate;
            if (!RequestString("Delivery date (AAAA-MM-DD):", out deliveryDate, x => MySQLUtil.CheckDate(x,DateTime.Now)))
                return Program.MainMenu;

            string confirm;
            if (!RequestString("Confirm order (Y/N):", out confirm, inputValidator: x => (x.ToLower() == "y" || x.ToLower() == "n", "Incorrect option")))
                return Program.MainMenu;

            

            if (confirm.ToLower() == "y")
            {
                bool result = MySQLUtil.OrderStandardBouquet(selectedBouquet, deliveryAddress, messageForDesigner, deliveryDate);
                if (result)
                    Console.WriteLine("You order has been sent to the shop");
                else
                    Console.WriteLine("There was an error with your order");
            }
            else
            {
                Console.WriteLine("You order has been cancelled");
            }
            Thread.Sleep(2000);
            return Program.MainMenu;
            

        }
    }
}

