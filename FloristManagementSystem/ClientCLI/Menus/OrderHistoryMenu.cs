using System;
using UtilityLibrary;

namespace ClientCLI.Menus
{
    public class OrderHistoryMenu : Menu
    {
        public OrderHistoryMenu() : base("History")
        {
        }

        void PrintMenu(List<PurchaseOrder> orders)
        {
            Console.Clear();

            Console.WriteLine(name);
            Console.WriteLine(0 + ". Quit history");
            int i = 1;
            foreach (PurchaseOrder order in orders)
            {
                Console.WriteLine(i + ". " + "Order n°" + order.Id + " - " + order.OrderDate + " - " + order.OrderState);
                Console.WriteLine("\t" + "Delivery date: " + order.DeliveryDate.ToString());
                Console.WriteLine("\t" + "Delivery address: " + order.DeliveryAddress);
                i++;
            }
        }

        public override Menu Show()
        {
            List<PurchaseOrder> orders = MySQLUtil.GetPurchaseOrders(clientId: MySQLUtil.ConnectedClientId);

            while (true)
            {
                PrintMenu(orders);
                int? selectedOption = null;

                while (selectedOption is null)
                {
                    Console.WriteLine("\nSelect option:");
                    try
                    {
                        selectedOption = int.Parse(Console.ReadLine());
                        if (selectedOption > orders.Count || selectedOption < 0)
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
                    PrintMenu(orders);
                    Console.WriteLine("\nIncorrect option!");
                }

                if (selectedOption == 0)
                {
                    return Program.MainMenu;
                }

                PurchaseOrder selectedOrder = orders[(int)selectedOption - 1];

                new OrderDetailsMenu(selectedOrder).Show();

                return Program.MainMenu;
            }
        }
    }
}

