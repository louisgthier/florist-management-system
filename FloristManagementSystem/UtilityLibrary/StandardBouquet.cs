using System;
namespace UtilityLibrary
{
	public class StandardBouquet
	{
        public string Name;
        public float Price;
		public string Category;
		public string Description;

        public StandardBouquet(string name, float price, string category, string description)
		{
			this.Name = name;
			this.Price = price;
			this.Category = category;
			this.Description = description;
		}
	}
}

