using System;

namespace UtilityLibrary
{
    public class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Availability { get; set; }

        public Item(string name, int price, string type, string availability)
        {
            Name = name;
            Price = price;
            Type = type;
            Availability = availability;
        }
    }
}
