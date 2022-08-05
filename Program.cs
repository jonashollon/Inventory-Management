using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ConsoleApp8
{
    internal class Program
    {
        public static void Main()
        {
            Product.Start();

            Console.WriteLine("What would you like to do?");
            Console.WriteLine(@"Choose from the following list of options (1, 2, 3, 4, 5, or 6):
                1. Add a product
                2. Remove a product
                3. Search for a product
                4. Check for products nearing expiration date
                5. Edit a product's attributes
                6. Exit");

            string userResponse = Console.ReadLine();
            switch (userResponse)
            {
                case "1":
                    Product.AddProduct();
                    break;
                case "2":
                    string userAnswerRemove;
                    do
                    {
                        Console.WriteLine(@"Which product would you like to remove?
                    You can make a selection by: 
                    1. SKU 
                    2. Product name.
                    Please select 1 or 2");
                            userAnswerRemove=Console.ReadLine();
                    }
                    while (userAnswerRemove != "1" && userAnswerRemove != "2");
                    if (userAnswerRemove == "1")
                    {
                        Product.RemoveProductSKU();
                    }
                    else if (userAnswerRemove == "2")
                    {
                        Product.RemoveProductName();
                    }
                    break;
                case "3":
                    string userAnswerSearch;
                    do
                    {
                        Console.WriteLine(@"With which product property would you like to search for the product(s)?
                        You can make a selection by:
                        1. Product Name
                        2. Product SKU
                        3. Purchase place of Product
                        4. Expiration date of Product");
                        userAnswerSearch = Console.ReadLine();
                    }
                    while(userAnswerSearch != "1" && userAnswerSearch != "2" && userAnswerSearch != "3" && userAnswerSearch != "4");
                    switch (userAnswerSearch)
                    {
                        case "1":
                            Product.SearchProductName();
                            break;
                        case "2":
                            Product.SearchProductSKU();
                            break;
                        case "3":
                            Product.SearchProductPurchasePlace();
                            break;
                        case "4":
                            Product.SearchProductExpirationDate();
                            break;
                    }
                    break;
                case "4":
                    Product.ExpirationSearch();
                    break;
                case "5":
                    string userAnswerEdit;
                    do
                    {
                        Console.WriteLine(@"With which product property would you like to search for the product(s)?
                        You can make a selection by:
                        1. Product Name
                        2. Product SKU
                        3. Purchase place of Product
                        4. Expiration date of Product");
                        userAnswerEdit = Console.ReadLine();
                    }
                    while (userAnswerEdit != "1" && userAnswerEdit != "2" && userAnswerEdit != "3" && userAnswerEdit != "4" && userAnswerEdit != "5");
                    switch (userAnswerEdit)
                    {
                        case "1":
                            Product.EditProductName();
                            break;
                        case "2":
                            Product.EditProductPrice();
                            break;
                        case "3":
                            Product.EditProductPurchasePlace();
                            break;
                        case "4":
                            Product.EditProductPurchaseDate();
                            break;
                        case "5":
                            Product.EditProductExpirationDate();
                            break;
                    }
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("That was an incorrect input. Please try again.");
                    Main();
                    break;
            }
        }
    }
}
