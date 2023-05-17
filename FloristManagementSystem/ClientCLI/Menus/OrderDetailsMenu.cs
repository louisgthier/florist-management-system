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

            if (order.BouquetName is not null)
            {
                Console.WriteLine("Bouquet standard: " + order.BouquetName);
            }
            else
            {
                //MySQLUtil.Get
                //foreach ()
            }

            Console.WriteLine();
            RequestString("<Esc> to exit", out string _, clearConsole: false, showName: false);

            return null;
        }

    }
}