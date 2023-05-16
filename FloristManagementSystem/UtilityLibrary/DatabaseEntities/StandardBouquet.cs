using System;
namespace UtilityLibrary
{
	public class StandardBouquet
	{
        public string Name;
        public string Description;
        public float Price;
		public string Category;
		

        public StandardBouquet(string name, string description, float price, string category)
		{
			this.Name = name;
            this.Description = description;
            this.Price = price;
			this.Category = category;
		}
	}
}

