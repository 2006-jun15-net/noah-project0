using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoreApp.Library.Model;
using StoreApp.Library.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace StoreApp.App
{
    public class Program
    {

        static void Main(string[] args)
        {
            //Get Home Menu
            StoreRepo sr = new StoreRepo(new List<Location>());
            string storeDataPath = "../../../../StoreRepo.xml";
            char selection = GetHomeMenu();
            while(selection != 'q')
            {
                if(selection != 'a' && selection != 'r' && selection != 'p' && selection != 'g' && selection != 'l' && selection != 'q' )
                {
                    Console.WriteLine("Invalid Input. Please type a, r, p, g, l, or q.");
                    selection = GetHomeMenu();
                }
                else
                {
                    switch(selection)
                    {
                        case 'a':
                            //Go to a menu that steps you through creating a new store
                            string storeSetup =
                                "Store Setup:\n" +
                                "a: add new store\n" +
                                "e: edit existing store\n" +
                                "d: delete existing store\n" +
                                "b: go back\n";
                            Console.WriteLine(storeSetup);
                            char input = Char.ToLower(Char.Parse(Console.ReadLine()));
                            while(input != 'b')
                            {
                                if(input != 'a' && input != 'e' && input != 'd')
                                {
                                    Console.WriteLine("Invalid Input. Please type a, e, d, or b.");
                                    input = Char.ToLower(Char.Parse(Console.ReadLine()));
                                }
                                else
                                {
                                    switch(input)
                                    {
                                        case 'a':
                                            Location newStore = CreateNewStore();
                                            sr.AddStore(newStore);
                                            GenerateStores(sr, storeDataPath);
                                            Console.WriteLine("New store added to Store Repository!");

                                            break;
                                        case 'e':

                                            break;
                                        case 'd':

                                            break;

                                    }
                                }
                            }
                            
                            break;
                        case 'r':
                            //Register a new customers

                            break;
                        case 'p':
                            //Place an order

                            break;
                        case 'g':
                            //Get info and display it to the user

                            break;
                        case 'l':
                            //Load existing data

                            break;

                    }
                }
            }


        }

        private static Location CreateNewStore()
        {
            Dictionary<Product, int> inventory = new Dictionary<Product, int>();
            Console.WriteLine("Enter store name: ");
            string storeName = Console.ReadLine();
            char input = 'a';
            while (input != 'f')
            {
                 input = ProductSetupMenu();
                if (input != 'a' && input != 'e' && input != 'f')
                {
                    Console.WriteLine("Invalid Input. Please type a, e, or f.");
                    input = ProductSetupMenu();
                }
                else
                {
                    switch(input)
                    {
                        case 'a':
                            try
                            {
                                Product p = CreateNewProduct(inventory);
                                Console.WriteLine("Enter quantity of products: ");
                                int qty = Int32.Parse(Console.ReadLine());
                                inventory.Add(p, qty);
                                Console.WriteLine("Product Created!");
                                Console.WriteLine();
                                
                            }
                            catch(ArgumentException ae)
                            {
                                Console.WriteLine(ae.Message);
                            }
                            
                            break;

                        case 'e':
                            Console.WriteLine("Enter product name you want to change:");
                            string name = Console.ReadLine();
                            char choice;
                            if (inventory.Keys.Any(p => p.Name == name))
                            {
                                Product p = inventory.Keys.First(p => p.Name == name);
                                Console.WriteLine("Change product name (press y for yes, press any key for no): ");
                                choice = Char.ToLower(Char.Parse(Console.ReadLine()));
                                if(choice == 'y')
                                {
                                    Console.WriteLine("Enter new name:");
                                    p.Name = Console.ReadLine();
                                    Console.WriteLine($"Name was changed to {p.Name}.");
                                }
                                else { Console.WriteLine("Name was not changed."); }
                                Console.WriteLine("Change Price?(y for yes, any key for no):");
                                choice = Char.ToLower(Char.Parse(Console.ReadLine()));
                                if (choice == 'y')
                                {
                                    Console.WriteLine("Enter Price:");
                                    p.Price = Int32.Parse(Console.ReadLine());
                                    Console.WriteLine($"Price was changed to {p.Price}.");
                                }
                                else { Console.WriteLine("Price was not changed"); }
                                Console.WriteLine("Enter quantity of products:");
                                int qty = Int32.Parse(Console.ReadLine());

                                inventory.Remove(p);
                                inventory.Add(p, qty);
                                
                            }
                            else
                            {
                                Console.WriteLine($"Product with name, {name} doesn't exist.");
                            }
                            
                            break;
                    }
                }
                
                
            }
            return new Location(storeName, inventory);
        }

        private static Product CreateNewProduct(in Dictionary<Product, int> inv)
        {
            Console.WriteLine("Enter product name: ");
            string pName = Console.ReadLine();
            if (inv.Keys.Any(p => p.Name == pName))
            {
                throw new ArgumentException($"Product Name:{pName} already exists.");
            }
            Console.WriteLine("Enter price for the product: ");
            double price = Double.Parse(Console.ReadLine());
            return new Product(pName, price);
            
        }

        private static char ProductSetupMenu()
        {
            string productSetup =
                "What does the store sell?\n" +
                "a: add new product\n" +
                "e: edit products\n" +
                "f: finish";
            Console.WriteLine(productSetup);
            
            Console.WriteLine("Enter choice: ");
            char choice = Char.ToLower(Char.Parse(Console.ReadLine()));
            return choice;
        }

        private static char GetHomeMenu()
        {
            //Menu options include: 
            //  1. Create new store
            //  2. Register new customers
            //  3. Place orders
            //  4. Display current status, e.g. inventory, customer info, etc.
            //  5. load data
            
            string homeMenu =
                "Home Menu:\n" +
                "a. Add a new store\n" +
                "r. Register as a new Customer\n" +
                "p. Place an order\n" +
                "g. Get info (order history, store inventories, etc.)\n" +
                "l. Load existing store and/or order history\n" +
                "q. quit\n";
            Console.WriteLine(homeMenu);
            return Char.ToLower(Char.Parse(Console.ReadLine()));
           
        }

        private static Customer RegisterCustomer(List<Order> oh)
        {
            //Get first and last name of customer
            Console.WriteLine("Enter your first name: ");
            string fn = Console.ReadLine();
            while(fn != null)
            {
                Console.WriteLine("Must enter a name.");
                Console.WriteLine("Enter your first name: ");
                fn = Console.ReadLine();
            }
            Console.WriteLine("Enter your last name: ");
            string ln = Console.ReadLine();
            while (ln != null)
            {
                Console.WriteLine("Must enter a name.");
                Console.WriteLine("Enter your first name: ");
                ln = Console.ReadLine();
            }
            //check to see if first and last name of each existing customer does not match new customer 
            foreach(var order in oh)
            {
                if(fn == order.CurrentCustomer.FirstName && ln == order.CurrentCustomer.LastName)
                {
                    Console.WriteLine("This name already exists.");
                    return order.CurrentCustomer;
                }
            }
           
            return new Customer(fn, ln);
        }

        private static string GetMenu()
        {
            return "Store Options\n"
                        + "1. Register as a new customer\n"
                        + "2. Place an order\n"
                        + "3. Search customers\n"
                        + "4. Print Order History\n"
                        + "5. Quit";
        }

        public static void GenerateOrderHistory(OrderHistory oh, string filePath)
        {
            //Generate a new file and output the order history to xml format
            
            var serializer = new XmlSerializer(typeof(List<Order>));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, oh.GetOrderHistory().ToList());
            }
            
        }
        public static OrderHistory GetInitialData(string filePath)
        {
            List<Order> oh;
            var serializer = new XmlSerializer(typeof(List<Order>));
            //Try to read in a json file and assign it to a list of orders (aka an order history)
            //but if none exists just return a null order history
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    oh = (List<Order>)serializer.Deserialize(stream);
                }
                return new OrderHistory(oh);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No data exists. Must first create a data file.");
                return new OrderHistory(new List<Order>());
            }

        }
        public static void GenerateStores(StoreRepo allStores, string filePath)
        {
            var serializer = new DataContractSerializer(typeof(List<Location>));

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.WriteObject(stream, allStores.GetAllStores().ToList());
            }

        }
        
        public static StoreRepo GetInitialStoreData(string filePath)
        {
            List<Location> stores;
            
            var serializer = new DataContractSerializer(typeof(List<Location>));
            //Try to read in a json file and assign it to a list of orders (aka an order history)
            //but if none exists just return a null order history
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stores = (List<Location>)serializer.ReadObject(stream);
                }
                return new StoreRepo(stores);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No data exists. Must first create a data file.");
                return new StoreRepo(new List<Location>());
            }
        }
        

        public static string ConvertToJson(Object obj)
        { 
            //This converts the Order History repo to a json format to write to a file
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
