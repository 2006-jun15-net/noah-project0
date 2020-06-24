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
using System.Xml.Serialization;

namespace StoreApp.App
{
    public class Program
    {

        static void Main(string[] args)
        {
            //Get Home Menu
            char selection = GetHomeMenu();
            while(selection != 'q')
            {
                if(selection != 'a' || selection != 'r' || selection != 'p' || selection != 'g' || selection != 'l' || selection != 'q' )
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
                            CreateStoreMenu();
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

        private static void CreateStoreMenu()
        {
            string storeMenu =
                "Create a new store:\n" +
                "";
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
            return Char.Parse(Console.ReadLine());
           
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
