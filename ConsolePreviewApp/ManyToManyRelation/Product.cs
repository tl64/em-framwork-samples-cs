using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyToManyRelation
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public ICollection<Customer> Customers { get; set; } //navigation property

        public Product()
        {
            Customers = new List<Customer>();
        }
    }
}
