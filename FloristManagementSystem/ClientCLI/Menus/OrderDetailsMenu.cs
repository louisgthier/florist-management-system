using System;
using UtilityLibrary;

namespace ClientCLI.Menus
{
	public class OrderDetailsMenu : Menu
	{
        PurchaseOrder order;
		public OrderDetailsMenu(PurchaseOrder order) : base("Order details")
        {
            this.order = order;
		}


        public override Menu Show()
        {

            Console.Clear();

            Console.WriteLine(name + "\n");
            Console.WriteLine("Order n°"+order.Id);

            Console.WriteLine("Order date: " +order.OrderDate);
            Console.WriteLine("Order status: " + order.OrderState);
            Console.WriteLine("Delivery date: " + order.DeliveryDate.ToString());
            Console.WriteLine("Delivery address: " + order.DeliveryAddress);
            Console.WriteLine("Designer message: " + order.Message);
            Console.WriteLine("Shop id: " + order.ShopId);

            if (order.BouquetName is not null)
            {
                Console.WriteLine("Bouquet standard: " + order.BouquetName);
            }
            else
            {
                Console.WriteLine("Arrangement items:");
                List<(Item, int)> items = MySQLUtil.GetItemsOfArrangement((int)order.ArrangementId);
                foreach ((Item, int) item in items)
                {
                    Console.WriteLine(" • " + item.Item1.Name + " (" + item.Item1.Price + "$): " + item.Item2.ToString());
                }
            }

            Console.WriteLine();
            RequestString("<Esc> to exit", out string _, clearConsole: false, showName: false);

            return null;
        }

    }
}