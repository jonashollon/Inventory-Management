using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp8
{
    class Product
    {
        public string productName { get; set; }
        public decimal productPurchasePrice { get; set; }
        public string productPurchasePlace { get; set; }
        public DateTime productPurchaseDate { get; set; }
        public DateTime? productExpirationDate { get; set; }
        public static List<Product> ProductList = new List<Product>();
        public static int SKUCounter { get; set; }
        public int SKU { get; set; }
        public Product()
        {

        }

        public static void AddProduct()
        {
            
            
            Product product = new Product();
           
            product.SKU = SKUCounter;
            SKUCounter++;


            Console.WriteLine("What is the name of the product?");
            string productName = Console.ReadLine();
            product.productName = productName;

            Console.WriteLine("What was the purchase price?");
            decimal productPurchasePrice =0.00m;
            while (!decimal.TryParse(Console.ReadLine(), out productPurchasePrice))
                Console.WriteLine("Invalid input - please try again with a valid purchase price.");
            product.productPurchasePrice = productPurchasePrice;
            
            Console.WriteLine("Where was the product purchased?");
            string productPurchasePlace = Console.ReadLine();
            product.productPurchasePlace = productPurchasePlace;
            
            DateTime purchaseDatePlaceholder = DateTime.Now;

            Console.WriteLine("When was the product purchased (MM/DD/YYYY)?");
            while (!DateTime.TryParse(Console.ReadLine(), out purchaseDatePlaceholder))
                Console.WriteLine("Please try again while typing the date in the proper format");

            product.productPurchaseDate = purchaseDatePlaceholder;

            //DateTime productPurchaseDate = DateTime.Parse(Console.ReadLine());
            
            Console.WriteLine("Does the product have an expiration date (Y/N)");
            string expiryAnswer = Console.ReadLine();
            string expiryAnswerLower = expiryAnswer.ToLower();
            while (expiryAnswerLower != "y")
                if (expiryAnswerLower == "n")
                {
                    //serialize to JSON
                    ProductList.Add(product);

                    Console.WriteLine($"{productName} has been added.");

                    var inventoryJson = JsonConvert.SerializeObject(ProductList);

                    File.WriteAllText(@"inventory.json", inventoryJson);
                }
                else
                {
                    Console.WriteLine("That doesn't seem to be the correct input, please enter Y for yes or N for no.");
                    Console.WriteLine("Does the product have an expiration date (Y/N)");
                    expiryAnswer = Console.ReadLine();
                };
            {
                DateTime expirationDatePlaceholder = DateTime.Now;

                Console.WriteLine("What is the expiration date (MM/DD/YYYY)?");
                while (!DateTime.TryParse(Console.ReadLine(), out expirationDatePlaceholder))
                    Console.WriteLine("Please try again while typing the date in the proper format");

                product.productExpirationDate = expirationDatePlaceholder;

                ProductList.Add(product);

                Console.WriteLine($"{productName} has been added.");

                var inventoryJson = JsonConvert.SerializeObject(ProductList);

                File.WriteAllText(@"inventory.json", inventoryJson);

            }



        }

        
    }
} 
