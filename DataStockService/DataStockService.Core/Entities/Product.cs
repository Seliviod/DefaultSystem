namespace DataStockService.Core.Entities
{
    public class Product : Entity
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
