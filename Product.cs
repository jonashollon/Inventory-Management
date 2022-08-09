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
        
        public static void AddProduct()
        {
            string userAnswer;
            Product product = new Product();
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            string inventoryJson;

            if (File.Exists(jsonFilePath))
            {
                inventoryJson = File.ReadAllText(jsonFilePath);
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);
                
            }
            else
            {
                // fixes pesky null exception
                productList.Add(new Product());

                inventoryJson = JsonConvert.SerializeObject(productList);

                File.WriteAllText(jsonFilePath, inventoryJson);

                //FileStream fileStream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read);
                //static FileStream Create(string path);
                //string inventoryJson = JsonConvert.SerializeObject(product);
                //inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                //File.WriteAllText(jsonFilePath, inventoryJson);

                //File.create???
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
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, or 3.");
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
                                    Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, or 3.");
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
                string userAnswer;
                var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                    Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");
                var removeProductSKU = productList.Single(p => p.SKU == SKUToRemove);
            if (removeProductSKU != null)
            {
                productList.Remove(removeProductSKU);

                Console.WriteLine($"The prouduct with the SKU of {SKUToRemove} has been successfully removed from the database!");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                while (true)
                {
                    Console.WriteLine(@"You can now:
                        1. Restart your search by SKU for a product to remove.
                        2. Search by name for a product to remove.
                        3. Return to main menu
                        4. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
                    {
                        case "1":
                            Product.RemoveProductSKU();
                            break;
                        case "2":
                            Product.RemoveProductName();
                            break;
                        case "3":
                            Program.Main();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, or 4.");
                            break;
                    }
                }
            }

            else
            {
                while (true)
                {
                    Console.WriteLine($"There doesn't seem to be any products with a SKU of {SKUToRemove}.");
                    Console.WriteLine(@"You can now:
                        1. Restart your search by SKU for a product to remove.
                        2. Search by name for a product to remove.
                        3. Return to main menu
                        4. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
                    {
                        case "1":
                            Product.RemoveProductSKU();
                            break;
                        case "2":
                            Product.RemoveProductName();
                            break;
                        case "3":
                            Program.Main();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, or 4.");
                            break;
                    }
                }
            }
        }



        public static void RemoveProductName()
        {
            string nameToRemove;
            string userAnswer;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                productList.Add(new Product());
            }

            Console.WriteLine("What is the name of the product you would like to remove?");
            nameToRemove = Console.ReadLine();
            var removeProductSKU = productList.Single(p => p.productName == nameToRemove);

            if (removeProductSKU != null)
            {
                productList.Remove(removeProductSKU);

                Console.WriteLine($"{nameToRemove} has been successfully removed from the database!");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                while (true)
                {
                    Console.WriteLine(@"You can now:
                        1. Restart your search by name for a product to remove.
                        2. Search by SKU for a product to remove.
                        3. Return to main menu
                        4. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
                    {
                        case "1":
                            Product.RemoveProductName();
                            break;
                        case "2":
                            Product.RemoveProductSKU();
                            break;
                        case "3":
                            Program.Main();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, or 4.");
                            break;
                    }
                }    
            }
            else
            {
                while (true)
                {
                    Console.WriteLine($"There doesn't seem to be any products with the name \"{nameToRemove}\".");
                    Console.WriteLine(@"You can now:
                        1. Restart your search by name for a product to remove.
                        2. Search by SKU for a product to remove.
                        3. Return to main menu
                        4. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
                    {
                        case "1":
                            Product.RemoveProductName();
                            break;
                        case "2":
                            Product.RemoveProductSKU();
                            break;
                        case "3":
                            Program.Main();
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, or 4.");
                            break;
                    }
                }
            }
        }
                    
            
            public static void SearchProductName()
                    {
                        string searchedName;
                        string userAnswer;
                        var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                                                                            where product.productName != null && product.productName.ToLower().Equals(searchedName)
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

                while (true)
                {
                    Console.WriteLine(@"You can now:
                        1. Search for a product by name
                        2. Search for a product by SKU
                        3. Search for a product by purchase place
                        4. Search for a product by expiration date
                        5. Return to main menu
                        6. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "5":
                            Program.Main();
                            break;
                        case "6":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                            break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine($"There doesn't seem to be any products named {searchedName}.");
                    Console.WriteLine(@"You can now:
                        1. Search for a product by name
                        2. Search for a product by SKU
                        3. Search for a product by purchase place
                        4. Search for a product by expiration date
                        5. Return to main menu
                        6. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "5":
                            Program.Main();
                            break;
                        case "6":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                            break;
                    }
                }
            }
                    }
                    public static void SearchProductSKU()
                    {
                        int searchedSKU;
                        string userAnswer;
                        var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                            Console.WriteLine("That was an incorrect input. Please try again. Enter the SKU (it's a number).");

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
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                            userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
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
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                    }
                            }
                        }
                    }
                    public static void SearchProductPurchasePlace()
                    {
                        string searchedPurchasePlace;
                        string userAnswer;
                        var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                                                                            where product.productPurchasePlace != null && product.productPurchasePlace.ToLower().Equals(searchedPurchasePlace)
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
                            
                            while(true)
                            {        
                                Console.WriteLine(@"You can now:
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                    userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
        
                        else
                        {
                            while (true)
                            {
                                Console.WriteLine($"There doesn't seem to be any products that were purchased at {searchedPurchasePlace}.");
                                Console.WriteLine(@"You can now:
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
                    }

                    public static void SearchProductExpirationDate()
                    {
                        DateTime searchedExpirationDate;
                        string userAnswer;
                        var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                                                                            where product.productExpirationDate != null && product.productExpirationDate.Equals(searchedExpirationDate)
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

                            while (true)
                            {
                                Console.WriteLine(@"You can now:
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            while (true)
                            {
                                Console.WriteLine($"There doesn't seem to be any products with the expiration date of {searchedExpirationDate}.");
                                Console.WriteLine(@"You can now:
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
                    }

                    public static void ExpirationSearch()
                    {
                        int searchedExpiration;
                        string userAnswer;
                        var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
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
                                Console.WriteLine($@"Name:            {product.productName}
                                Price:              {product.productPurchasePrice}
                                Place:              {product.productPurchasePlace}
                                Purchase Date:      {product.productPurchaseDate}
                                Expiration Date:    {product.productExpirationDate}
                                SKU:                {product.SKU}");

                            while (true)
                            {
                                Console.WriteLine(@"You can now:
                                    1. Search for products within a certain expiration date range                                                    
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
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
                        else
                        {
                            while (true)
                            {
                                Console.WriteLine($"There doesn't seem to be any products with an expiration date within {searchedExpiration} days.");
                                Console.WriteLine(@"You can now:
                                    1. Search for a product by name
                                    2. Search for a product by SKU
                                    3. Search for a product by purchase place
                                    4. Search for a product by expiration date
                                    5. Return to main menu
                                    6. Exit the program
                                    Which would you like to do?");
                                userAnswer = Console.ReadLine();

                                switch (userAnswer)
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
                                    case "5":
                                        Program.Main();
                                        break;
                                    case "6":
                                        Environment.Exit(0);
                                        break;
                                    default:
                                        Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, or 6.");
                                        break;
                                }
                            }
                        }
                    }
                    
                    // All of this is old code before I decided to switch each attribute edit into their own methods. Saving it just in case.    

                    //public static void ProductEdit()
                    //{
                    //    int searchedSKU;
                    //    string attributeAnswer;
                    //    var jsonFilePath = @"C:\Users\hollo\source\repos\ConsoleApp8\inventory.json";
                    //    var inventoryJson = File.ReadAllText(jsonFilePath);
                    //    if (File.Exists(jsonFilePath))
                    //        Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

                    //    else
                    //    {
                    //        // fixes pesky null exception
                    //        Product.productList.Add(new Product());
                    //    }

                    //    Console.WriteLine("What is the SKU of the product you're searching for?)");
                    //    while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                    //        Console.WriteLine("That was an incorrect input. Please try again.");

                    //    while (true)
                    //    {
                    //        Console.WriteLine(@"Which attribute of this product would you like to edit?
                    //        1. Product name
                    //        2. Product price
                    //        3. Product's place of purchase
                    //        4. Product's purchase date
                    //        5. Product's expiration date");

                    //        attributeAnswer = Console.ReadLine();

                    //        switch (attributeAnswer)
                    //        {
                    //            case "1":
                    //                string nameSelection;
                    //                string userAnswer;
                    //                Console.WriteLine("What would you like to change the name to?");
                    //                nameSelection = Console.ReadLine();

                    //                var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
                    //                if (obj != null)
                    //                {
                    //                    obj.productName = nameSelection;

                    //                    Console.WriteLine($"The product with the SKU of {searchedSKU} has had its name changed to {nameSelection}");

                    //                    inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                    //                    File.WriteAllText(jsonFilePath, inventoryJson);
                                    
                    //                }
                    //                else
                    //                {
                    //                    while (true)
                    //                    {
                    //                        Console.WriteLine($"There doesn't seem to be any products with a SKU of {searchedSKU}.");
                    //                        Console.WriteLine(@"You can now:
                    //                        1. Try to edit a product with a different SKU
                    //                        2. Return to main menu
                    //                        3. Exit the program
                    //                        Which would you like to do?");
                    //                        userAnswer = Console.ReadLine();

                    //                        switch (userAnswer)
                    //                        {
                    //                            case "1":
                    //                                Product.ProductEdit();
                    //                                break;
                    //                            case "2":
                    //                                Program.Main();
                    //                                break;
                    //                            case "3":
                    //                                Environment.Exit(0);
                    //                                break;
                    //                            default:
                    //                                Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                    //                                break;
                    //                        }
                    //                    }
                    //                }
                    //                break;
                    //            case "2":
                    //                string priceSelection;
                    //                Console.WriteLine("What would you like to change the price to?");
                    //                break;
                    //            case "3":
                    //                string purchasePlaceSelection;
                    //                Console.WriteLine("What would you like to change the palce of purchase to?");
                    //                break;
                    //            case "4":
                    //                DateTime purchaseDateSelection;
                    //                Console.WriteLine("What would you like to change the purchase date to?");
                    //                break;
                    //            case "5":
                    //                DateTime expirationDateSelection;
                    //                Console.WriteLine("What would you like to change the expiration date to?");
                    //                break;

                    //        }
                    //    }



                    //}
        public static void EditProductName()
        {
            int searchedSKU;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("What is the SKU of the product you're trying to edit the name of?");
            while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");

            string nameSelection;
            string userAnswer;
            Console.WriteLine("What would you like to change the name to?");
            nameSelection = Console.ReadLine();

            var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
            if (obj != null)
            {
                obj.productName = nameSelection;

                Console.WriteLine($"The product with the SKU of {searchedSKU} has had its name changed to {nameSelection}");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                string userAnswer2;
                while(true)
                { 
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine(@"You can now:
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer2 = Console.ReadLine();

                    switch (userAnswer2)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
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
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
                            break;
                    }
                }
            }


        }
        public static void EditProductPrice()
        {
            int searchedSKU;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("What is the SKU of the product you're trying to edit the price of?");
            while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");

            decimal priceSelection;
            string userAnswer;
            Console.WriteLine("What would you like to change the price to?");

            while (!decimal.TryParse(Console.ReadLine(), out priceSelection))
                Console.WriteLine("Invalid input - please try again with a valid purchase price.");

            var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
            if (obj != null)
            {
                obj.productPurchasePrice = priceSelection;

                Console.WriteLine($"The product with the SKU of {searchedSKU} has had its price changed to {priceSelection}");
                
                //HEYYY YOU MIGHT NEED TO CHANGE NUMBER OF DECIMAL PLACES DISPLAYED!!!!!!!!#!$$%#^$%&%^*&^%&*^&)*_%^&_$%_^$%^_$%^$%$




                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                string userAnswer2;
                while (true)
                { 
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine(@"You can now:
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer2 = Console.ReadLine();

                    switch (userAnswer2)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
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
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
                            break;
                    }
                }
            }


        }

        public static void EditProductPurchasePlace()

        {
            int searchedSKU;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("What is the SKU of the product you're trying to edit the purchase place of?");
            while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");

            string purchasePlaceSelection;
            string userAnswer;
            Console.WriteLine("What would you like to change the purchase place to?");
            purchasePlaceSelection = Console.ReadLine();

            var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
            if (obj != null)
            {
                obj.productPurchasePlace = purchasePlaceSelection;

                Console.WriteLine($"The product with the SKU of {searchedSKU} has had its purchase place changed to {purchasePlaceSelection}");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                string userAnswer2;
                while(true)
                {
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine(@"You can now:
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer2 = Console.ReadLine();

                    switch (userAnswer2)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again.  Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
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
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
                            break;
                    }
                }
            }


        }

        public static void EditProductPurchaseDate()

        {
            int searchedSKU;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("What is the SKU of the product you're trying to edit the purchase date of?)");
            while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");

            DateTime purchaseDateSelection;
            string userAnswer;
            Console.WriteLine("What would you like to change the purchase date to (MM/DD/YYYY)?");
            while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out purchaseDateSelection))
                Console.WriteLine("Please try again while typing the date in the proper format");

            var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
            if (obj != null)
            {
                obj.productPurchaseDate = purchaseDateSelection;

                Console.WriteLine($"The product with the SKU of {searchedSKU} has had its purchase date changed to {purchaseDateSelection}");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                string userAnswer2;
                while (true)
                {
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine(@"You can now:
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer2 = Console.ReadLine();

                    switch (userAnswer2)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again. Please try again. Enter 1, 2, 3, 4, 5, 6, or 7.");
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
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                            break;
                    }
                }
            }


        }

        public static void EditProductExpirationDate()

        {
            int searchedSKU;
            var jsonFilePath = Path.Combine(Environment.CurrentDirectory, @"inventoryJson2.json");
            var inventoryJson = File.ReadAllText(jsonFilePath);
            if (File.Exists(jsonFilePath))
                Product.productList = JsonConvert.DeserializeObject<List<Product>>(inventoryJson);

            else
            {
                // fixes pesky null exception
                Product.productList.Add(new Product());
            }

            Console.WriteLine("What is the SKU of the product you're trying to edit the expiration date of?)");
            while (!int.TryParse(Console.ReadLine(), out searchedSKU))
                Console.WriteLine("That was an incorrect input. Please try again. Please enter the SKU (it's a number).");

            DateTime expirationDateSelection;
            string userAnswer;
            Console.WriteLine("What would you like to change the expiration date to (MM/DD/YYYY)?");
            while (!DateTime.TryParseExact(Console.ReadLine(), "MM/dd/yyyy", null, DateTimeStyles.None, out expirationDateSelection))
                Console.WriteLine("Please try again while typing the date in the proper format");

            var obj = productList.FirstOrDefault(x => x.SKU == searchedSKU);
            if (obj != null)
            {
                obj.productExpirationDate = expirationDateSelection;

                Console.WriteLine($"The product with the SKU of {searchedSKU} has had its expiration date changed to {expirationDateSelection}");

                inventoryJson = JsonConvert.SerializeObject(productList, Formatting.Indented);
                File.WriteAllText(jsonFilePath, inventoryJson);

                string userAnswer2;
                while(true)
                {
                    Console.WriteLine("What would you like to do next?");
                    Console.WriteLine(@"You can now:
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer2 = Console.ReadLine();

                    switch (userAnswer2)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
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
                        1. Edit another product's name
                        2. Edit another product's price
                        3. Edit another product's purchase place
                        4. Edit another product's purchase date
                        5. Edit another product's expiration date
                        6. Return to main menu
                        7. Exit the program
                        Which would you like to do?");
                    userAnswer = Console.ReadLine();

                    switch (userAnswer)
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
                        case "6":
                            Program.Main();
                            break;
                        case "7":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("You seem to have entered an invalid input. Please try again.");
                            break;
                    }
                }
            }


        }
    }
}
