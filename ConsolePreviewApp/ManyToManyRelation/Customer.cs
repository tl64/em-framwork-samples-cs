using System.Collections.Generic;

namespace ManyToManyRelation
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Product> Products { get; set; }

        public Customer()
        {
            Products = new List<Product>();
        }
    }
}
