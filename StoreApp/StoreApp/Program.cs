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
            Console.WriteLine("running");
            var dataList = new List<Order>();
            List<Product> p = new List<Product>
            {
                new Product("x", 100),
                new Product("y", 100),
                new Product("z", 100)

            };
            List<Product> p2 = new List<Product>
            {
                new Product("x", 100),
                new Product("y", 100),
                new Product("z", 100)

            };
            Customer c = new Customer("noah", "funtanilla");
            Order o = new Order
            {
                Products = p,
                CurrentCustomer = c
            };
            Order o2 = new Order
            {
                Products = p2,
                CurrentCustomer = c
            };
            dataList.Add(o);
            dataList.Add(o2);

            OrderHistory oh = new OrderHistory(dataList);

            List<Location> stores = new List<Location>
            {
                new Location
                {
                    Name = "store1",
                    LocationID = 1,
                    Inventory =
                    {
                        { new Product("a", 10.00), 100 },
                        { new Product("b", 20.00), 200 },
                        { new Product("c", 30.00), 300 }
                    }
                },
                new Location
                {
                    Name = "store2",
                    LocationID = 2,
                    Inventory =
                    {
                        { new Product("e", 10.00), 100 },
                        { new Product("f", 20.00), 200 },
                        { new Product("g", 30.00), 300 }
                    }
                }
            };
            StoreRepo storeRepo = new StoreRepo(stores);

            string orderHistoryPath = "../../../../OrderHistory.xml";
            string storesDataPath = "../../../../StoresData.xml";

         
            GenerateOrderHistory(oh, orderHistoryPath);
            GenerateStores(storeRepo, storesDataPath);

            OrderHistory dataFromOrdersXml = GetInitialData(orderHistoryPath);
            StoreRepo dataFromStoresXml = GetInitialStoreData(storesDataPath);
            Console.WriteLine(dataFromOrdersXml.GetOrderHistory().ToList()[0].Products[0].Name);
            dataFromStoresXml.CheckInventory(1);
            //List<Order> orderHistory = GetInitialData();


            //Customer customer;

            //int selection = 1;

            //while (selection != 5)
            //{
            //    Console.WriteLine(GetMenu() + "\nPlease enter an option: ");
            //    selection = Console.Read();
            //    if (selection == 1 || selection == 2 || selection == 3 || selection == 4)
            //    {
            //        switch(selection)
            //        {
            //            case 1:  //add a new customer
            //                customer = RegisterCustomer(orderHistory);

            //                break;
            //            case 2: //place an order

            //                break;
            //            case 3://Search customers
            //                break;
            //            case 4://Print order history
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("Invalid Option. Please type 1-5");
            //    }
            //}
            //Place orders to store locations for customers



            //search customers by name

            //display details of an order

            //display all order history of a store location

            //display all order history of a customer


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
