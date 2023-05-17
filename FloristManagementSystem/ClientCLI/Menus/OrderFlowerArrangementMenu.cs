using System;
using UtilityLibrary;

namespace ClientCLI.Menus
{
	public class OrderFlowerArrangementMenu : Menu
	{

		public OrderFlowerArrangementMenu() : base("Order a flower arrangement")
        {
		}

        void PrintMenu(List<Item> items, Dictionary<Item, int> selectedItems)
        {
            Console.Clear();

            Console.WriteLine(name);

            if (selectedItems.Count > 0)
            {
                Console.WriteLine("\nSelected items / Quantity:");

                foreach (Item item in selectedItems.Keys)
                {
                    Console.WriteLine(" • " + item.Name + ": " + selectedItems[item]);
                }
            }

            Console.WriteLine("\nAdd items:");
            Console.WriteLine(0 + ". " + "Cancel order");
            int i = 1;
            foreach (Item item in items)
            {
                Console.WriteLine(i + ". " + item.Name + " ("+item.Price+"€)" + ": " + (item.Type == "f" ? "Flower" : "Accessory"));
                i++;
            }
        }

        public override Menu Show()
        {
            Dictionary<Item, int> selectedItems = new Dictionary<Item, int>();

            List<Item> items = MySQLUtil.GetItems();


            while (true)
            {
                PrintMenu(items, selectedItems);
                int? selectedOption = null;

                while (selectedOption is null)
                {
                    Console.WriteLine("\nSelect option (<ENTER> to validate):");
                    try
                    {
                        string input = Console.ReadLine();
                        if (input == "")
                        {
                            selectedOption = -1;
                            break;
                        }
                        selectedOption = int.Parse(input);
                        if (selectedOption > items.Count || selectedOption < 0)
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
                    PrintMenu(items, selectedItems);
                    Console.WriteLine("\nIncorrect option!");
                }

                if (selectedOption == 0)
                {
                    return Program.MainMenu;
                }

                if (selectedOption != -1)
                {
                    Item selectedItem = items[(int)selectedOption - 1];

                    string quantityString;
                    int quantity = 0;
                    if (!RequestString("Quantity:", out quantityString, x => (int.TryParse(x, out quantity) == true, "Incorrect quantity")))
                        continue;
                    if (!selectedItems.Keys.Contains(selectedItem))
                    {
                        selectedItems[selectedItem] = 0;
                    }
                    selectedItems[selectedItem] += quantity;

                    if (selectedItems[selectedItem] <= 0)
                    {
                        selectedItems.Remove(selectedItem);
                    }
                }
                else if (selectedItems.Values.Count > 0)
                {
                    // Continue order with selected items
                    break;
                }

            }
            
            string deliveryAddress;
            if (!RequestString("Delivery address:", out deliveryAddress))
                return Program.MainMenu;
            string messageForDesigner;
            if (!RequestString("Message for the bouquet designer (add details if you want designer to customize your arrangement):", out messageForDesigner))
                return Program.MainMenu;
            string priceString;
            int price = 0;
            if (!RequestString("Your budget:", out priceString, x => (int.TryParse(x, out price) == true && price >= 0, "Incorrect budget")))
                return Program.MainMenu;
            string deliveryDate;
            if (!RequestString("Delivery date (AAAA-MM-DD):", out deliveryDate, x => MySQLUtil.CheckDate(x, DateTime.Now)))
                return Program.MainMenu;

            string shopIdString;
            if (!RequestString("Enter shop id:", out shopIdString,
                x =>
                {
                    int y;
                    if (!int.TryParse(x, out y) || y < 1 || y > 11)
                    {
                        return (false, "Wrong shop id (should be between 1 and 11)");
                    }
                    return (true, "");

                }))
                return Program.MainMenu;
            int shopId = int.Parse(shopIdString);

            string confirm;
            if (!RequestString("Confirm order (y/n):", out confirm, inputValidator: x => (x.ToLower() == "y" || x.ToLower() == "n", "Incorrect option")))
                return Program.MainMenu;




            if (confirm.ToLower() == "y")
            {
                bool result = MySQLUtil.OrderFlowerArrangement(selectedItems, deliveryAddress, messageForDesigner, deliveryDate, shopId, price);
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

