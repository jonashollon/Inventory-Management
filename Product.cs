using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ConsoleApp8
{
    class Product
    {
        public string productName { get; set; }
        public decimal productPurchasePrice { get; set; }
        public string productPurchasePlace { get; set; }
        public DateTime productPurchaseDate { get; set; }
        public DateTime? productExpirationDate { get; set; }
        public static List<Product>? productList = new List<Product>();
        public static int SKUCounter { get; set; }
        public int SKU { get; set; }
        public Product()
        {

        }
        public static void Start()
        {
            var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";


        }

        public static void AddProduct()
        {
            string userAnswer;
            Product product = new Product();
            var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                productList.Add(new Product());
            }
            SKUCounter = productList.Max(s => s.SKU);
            SKUCounter++;
            product.SKU = SKUCounter;



            Console.WriteLine("What is the name of the product?");
            string productName = Console.ReadLine();
            product.productName = productName;

            Console.WriteLine("What was the purchase price?");
            decimal productPurchasePrice = 0.00m;
            while (!decimal.TryParse(Console.ReadLine(), out productPurchasePrice))
                Console.WriteLine("Invalid input - please try again with a valid purchase price.");
            product.productPurchasePrice = productPurchasePrice;

            Console.WriteLine("Where was the product purchased?");
            string productPurchasePlace = Console.ReadLine();
            product.productPurchasePlace = productPurchasePlace;

            DateTime purchaseDatePlaceholder = DateTime.Now;

            Console.WriteLine("When was the product purchased (MM/DD/YYYY)?");
            while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out purchaseDatePlaceholder))
                Console.WriteLine("Please try again while typing the date in the proper format");

            product.productPurchaseDate = purchaseDatePlaceholder;

            //DateTime productPurchaseDate = DateTime.Parse(Console.ReadLine());
            while (true)
            {
                Console.WriteLine("Does the product have an expiration date (Y/N)");
                string expiryAnswer = Console.ReadLine();
                string expiryAnswerLower = expiryAnswer.ToLower();

                switch (expiryAnswerLower)
                {
                    case "n":
                        productList.Add(product);
                        inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                        File.WriteAllText(jsonFilePath, inventoryJson);

                        Console.WriteLine($"{productName} has been added.");

                            while (true)
                            {
                                Console.WriteLine(@"You can now:
                                1. Add another product
                                2. Return to main menu
                                3. Exit the program
                                Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
                                {
                                    case "1":
                                        Product.AddProduct();
                                        break;
                                    case "2":
                                        Program.Main();
                                        break;
                                    case "3":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                        break;
                                }
                            }
                        break;
                    case "y":
                        DateTime expirationDatePlaceholder = DateTime.Now;

                        Console.WriteLine("What is the expiration date (MM/DD/YYYY)?");
                        while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out expirationDatePlaceholder))
                            Console.WriteLine("Please try again while typing the date in the proper format");

                        product.productExpirationDate = expirationDatePlaceholder;

                        productList.Add(product);

                        inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                        File.WriteAllText(jsonFilePath, inventoryJson);

                        Console.WriteLine($"{productName} has been added.");
                        
                        while (true)
                        {
                            Console.WriteLine(@"You can now:
                                1. Add another product
                                2. Return to main menu
                                3. Exit the program
                                Which would you like to do?");
                            userAnswer = Console.ReadLine();

                            switch (userAnswer)
                            {
                                case "1":
                                    Product.AddProduct();
                                    break;
                                case "2":
                                    Program.Main();
                                    break;
                                case "3":
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                    break;
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("It would seem that you've entered an input that was incorrect. Please enter \"y\" or \"n\" ");
                        break;
                }
            }

        }

            public static void RemoveProductSKU()
            {
                int SKUToRemove;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    productList.Add(new Product());
                }
                Console.WriteLine("Which SKU would you like to remove?");
                while (!int.TryParse(Console.ReadLine(), out SKUToRemove))
                    Console.WriteLine("That was an incorrect input. Please try again.");
                var removeProductSKU = productList.Single(p => p.SKU == SKUToRemove);
                if (removeProductSKU != null)
                    productList.Remove(removeProductSKU);


                Console.WriteLine($"{SKUToRemove} has been successfully removed from the library!");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);



            }
            public static void RemoveProductName()
            {
                string nameToRemove;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    productList.Add(new Product());
                }
                Console.WriteLine("Which product would you like to remove?");
                nameToRemove = Console.ReadLine();
                var removeProductSKU = productList.Single(p => p.productName == nameToRemove);
                if (removeProductSKU != null)
                    productList.Remove(removeProductSKU);

                //DOO SOMETHINNNGGGG ABOUTTT NULLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

                Console.WriteLine($"{nameToRemove} has been successfully removed from the library!");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);
            }

            public static void SearchProductName()
            {
                string searchedName;
                string userAnswer;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    Product.productList.Add(new Product());
                }

                Console.WriteLine("What is the name of the product you're searching for? (Note: cannot distinguish between upper and lower case)");
                searchedName = Console.ReadLine();
                searchedName.ToLower();

                List<Product> searchProductList = new List<Product>(from product in productList
                                                                    where product.productName.ToLower().Contains(searchedName)
                                                                    orderby product.SKU ascending
                                                                    select product);

                if (searchProductList.Count > 0)
                {
                    foreach (Product product in searchProductList)
                        Console.WriteLine(
                            @$"Name:            {product.productName}
                        Price:              {product.productPurchasePrice}
                        Place:              {product.productPurchasePlace}
                        Purchase Date:      {product.productPurchaseDate}
                        Expiration Date:    {product.productExpirationDate}
                        SKU:                {product.SKU}");
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine($"There doesn't seem to be any products named {searchedName}.");
                        Console.WriteLine(@"You can now:
                    1. Restart your search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                        userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Product.SearchProductName();
                                break;
                            case "2":
                                Program.Main();
                                break;
                            case "3":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                break;
                        }
                    }
                }
            }
            public static void SearchProductSKU()
            {
                int searchedSKU;
                string userAnswer;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    productList.Add(new Product());
                }
                Console.WriteLine("What is the SKU of the product you are searching for?");
                while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                    Console.WriteLine("That was an incorrect input. Please try again.");

                Product displyProductBySKU = productList.SingleOrDefault(p => p.SKU == searchedSKU);

                if (displyProductBySKU != null)
                {
                    Console.WriteLine(
                        @$"Name:            {displyProductBySKU.productName}
                    Price:              {displyProductBySKU.productPurchasePrice}
                    Place:              {displyProductBySKU.productPurchasePlace}
                    Purchase Date:      {displyProductBySKU.productPurchaseDate}
                    Expiration Date:    {displyProductBySKU.productExpirationDate}
                    SKU:                {displyProductBySKU.SKU}");

                    while (true)
                    {
                        Console.WriteLine(@"You can now:
                    1. Do another search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                        userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Product.SearchProductSKU();
                                break;
                            case "2":
                                Program.Main();
                                break;
                            case "3":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                break;

                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine($"There doesn't seem to be any products with a SKU of {searchedSKU}.");
                        Console.WriteLine(@"You can now:
                    1. Restart your search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                        userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Product.SearchProductSKU();
                                break;
                            case "2":
                                Program.Main();
                                break;
                            case "3":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                break;
                        }
                    }
                }
            }
            public static void SearchProductPurchasePlace()
            {
                string searchedPurchasePlace;
                string userAnswer;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    Product.productList.Add(new Product());
                }

                Console.WriteLine("What is the purchase place of the product(s) you're searching for? (Note: cannot distinguish between upper and lower case)");
                searchedPurchasePlace = Console.ReadLine();
                searchedPurchasePlace.ToLower();

                List<Product> searchProductList = new List<Product>(from product in productList
                                                                    where product.productPurchasePlace.ToLower().Contains(searchedPurchasePlace)
                                                                    orderby product.SKU ascending
                                                                    select product);
                if (searchProductList.Count > 0)
                {
                    foreach (Product product in searchProductList)
                        Console.WriteLine(
                     @$"Name:            {product.productName}
                Price:              {product.productPurchasePrice}
                Place:              {product.productPurchasePlace}
                Purchase Date:      {product.productPurchaseDate}
                Expiration Date:    {product.productExpirationDate}
                SKU:                {product.SKU}"); ;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine($"There doesn't seem to be any products that were purchased at {searchedPurchasePlace}.");
                        Console.WriteLine(@"You can now:
                    1. Restart your search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                        userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Product.SearchProductPurchasePlace();
                                break;
                            case "2":
                                Program.Main();
                                break;
                            case "3":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                break;
                        }
                    }
                }
            }

            public static void SearchProductExpirationDate()
            {
                DateTime searchedExpirationDate;
                string userAnswer;
                var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                var inventoryJson = File.ReadAllText(jsonFilePath);
                if (File.Exists(jsonFilePath))
                    Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                else
                {
                    // fixes pesky null exception
                    Product.productList.Add(new Product());
                }

                Console.WriteLine("What is the expiration date of the product(s) you're searching for (MM/DD/YYYY)?)");
                while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out searchedExpirationDate))
                    Console.WriteLine("Please try again while typing the date in the proper format");

                List<Product> searchProductList = new List<Product>(from product in productList
                                                                    where product.productExpirationDate.Equals(searchedExpirationDate)
                                                                    orderby product.SKU ascending
                                                                    select product);
                if (searchProductList.Count > 0)
                {
                    foreach (Product product in searchProductList)
                        Console.WriteLine(
                     @$"Name:            {product.productName}
                Price:              {product.productPurchasePrice}
                Place:              {product.productPurchasePlace}
                Purchase Date:      {product.productPurchaseDate}
                Expiration Date:    {product.productExpirationDate}
                SKU:                {product.SKU}"); ;
                }
                else
                {
                    while (true)
                    {
                        Console.WriteLine($"There doesn't seem to be any products with the expiration date of {searchedExpirationDate}.");
                        Console.WriteLine(@"You can now:
                    1. Restart your search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                        userAnswer = Console.ReadLine();

                        switch (userAnswer)
                        {
                            case "1":
                                Product.SearchProductExpirationDate();
                                break;
                            case "2":
                                Program.Main();
                                break;
                            case "3":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                                break;
                        }
                    }
                }
            }

            public static void ExpirationSearch()
        {
            int searchedExpiration;
            string userAnswer;
            var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("Please enter how many days from today's date you would like to search for products with an expiration date of.");
            while (!int.TryParse(Console.ReadLine(), out searchedExpiration))
                Console.WriteLine("That was an incorrect input. Please try again.");

            List<Product> searchProductList = new List<Product>(from product in productList
                                                                where (product.productExpirationDate != null) && (product.productExpirationDate - DateTime.Now).Value.TotalDays < searchedExpiration 
                                                                orderby product.SKU ascending
                                                                select product);

            if (searchProductList.Count > 0)
            {
                foreach (Product product in searchProductList)
                    Console.WriteLine(
                        @$"Name:            {product.productName}
                        Price:              {product.productPurchasePrice}
                        Place:              {product.productPurchasePlace}
                        Purchase Date:      {product.productPurchaseDate}
                        Expiration Date:    {product.productExpirationDate}
                        SKU:                {product.SKU}");
            }
            else
            {
                while (true)
                {
                    Console.WriteLine($"There doesn't seem to be any products with an expiration date within {searchedExpiration} days.");
                    Console.WriteLine(@"You can now:
                    1. Restart your search
                    2. Return to main menu
                    3. Exit the program
                    Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
                    {
                        case "1":
                            Product.ExpirationSearch();
                            break;
                        case "2":
                            Program.Main();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                            break;
                    }
                }
            }
        }

        public static void ProductEdit()
            {
                Console.WriteLine("edit");
            }
        }
    }
