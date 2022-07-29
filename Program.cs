using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ConsoleApp8
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine(@"Choose from the following list of options (1,2,3,4):
                1. Add a product
                2. Remove a product
                3. Search for a product
                4. Check for products nearing expiration date");

            string userResponse = Console.ReadLine();
            switch (userResponse)
            {
                case "1":
                    Product.AddProduct();
                    break;
                case "2":
                    Console.WriteLine(@"Which product would you like to remove?
                    You can make a selection by (1)Product name or (2)SKU.
                    Please select 1 or 2");
                    break;
                //case "3":
                //    Product.SearchProduct();
                //    break;
                //case "4":
                //    Product.ExpirationSearch();
                //    break;
            }
        }
    }
}
