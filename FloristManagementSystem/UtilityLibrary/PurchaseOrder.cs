using System;
namespace UtilityLibrary
{
	public class PurchaseOrder
	{
        public int Id { get; set; }
        public string DeliveryAddress { get; set; }
        public string Message { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderState { get; set; }
        public int ClientId { get; set; }
        public int ArrangementId { get; set; }
        public string BouquetName { get; set; }

        public PurchaseOrder(int id, string deliveryAddress, string message, DateTime deliveryDate, DateTime orderDate, string orderState, int clientId, int arrangementId, string bouquetName)
        {
            Id = id;
            DeliveryAddress = deliveryAddress;
            Message = message;
            DeliveryDate = deliveryDate;
            OrderDate = orderDate;
            OrderState = orderState;
            ClientId = clientId;
            ArrangementId = arrangementId;
            BouquetName = bouquetName;
        }
    }
}

