using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePreviewApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DoStudentWork().Wait();

            Console.ReadKey();
        }

        private static async Task DoStudentWork()
        {
            var shopdb = new ShopDBEntities("name=ShopDBEntities");

            //shopdb.Students.Add(new Student
            //{
            //    FName = "Marek",
            //    LName = "Hamsik",
            //    Email = "hamsik@napoli.it",
            //    Phone = "093880880"
            //});
            //await shopdb.SaveChangesAsync();

            var vivaHolders = await shopdb.Students.Where(s => s.Phone.StartsWith("093") || s.Phone.StartsWith("077")).ToListAsync();

            foreach (var student in vivaHolders)
            {
                Console.Write($"{student.FName} {student.LName} - {student.Phone}\n");
            }
        }
    }
}
