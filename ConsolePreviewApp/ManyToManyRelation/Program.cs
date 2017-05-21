using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManyToManyRelation
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StoreEntity>());

            using (var db = new StoreEntity())
            {
                var product1 = new Product { Name = "Computer", Price = 1000 };
                var product2 = new Product { Name = "Chair", Price = 10 };
                var product3 = new Product { Name = "Conditioner", Price = 550 };
                db.Products.AddRange(new List<Product> { product1, product2, product3 });

                var customer1 = new Customer { FirstName = "Aram", LastName = "Zhamkochyan" };
                var customer2 = new Customer { FirstName = "Arman", LastName = "Zhamkochyan" };
                var customer3 = new Customer { FirstName = "Tigran", LastName = "Sargsyan" };
                db.Customers.AddRange(new List<Customer> { customer1, customer2, customer3 });

                customer1.Products.Add(product1);
                customer1.Products.Add(product2);

                customer2.Products.Add(product1);
                customer2.Products.Add(product2);
                customer2.Products.Add(product3);

                product1.Customers.Add(customer1);
                product1.Customers.Add(customer2);

                product2.Customers.Add(customer1);
                product2.Customers.Add(customer2);

                product3.Customers.Add(customer2);

                db.SaveChanges();

                foreach (Customer customer in db.Customers)
                {
                    Console.WriteLine($"{customer.FirstName} {customer.LastName}");
                    foreach (Product product in customer.Products)
                    {
                        Console.WriteLine($"\t{product.Name} - price: {product.Price}");
                    }
                }

                Console.WriteLine(db.Database.Connection.ConnectionString);
                //var average = db.Database.SqlQuery<Product>($"[dbo].[Function]({1},{2})");
                //Console.WriteLine(average);
                //var products = db.Database.SqlQuery<Product>("select * from Products");
                //foreach (Product product in products)
                //{
                //    Console.WriteLine($"{product.Name} {product.Price}");
                //}
            }

            Console.ReadKey();
        }

        private static async Task DoWork()
        {
            
        }
    }
}
