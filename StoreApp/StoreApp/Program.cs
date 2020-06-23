using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StoreApp.Library.Model;
using StoreApp.Library.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                new Product("p1", 100),
                new Product("p2", 100),
                new Product("p3", 100)

            };
            List<Product> p2 = new List<Product>
            {
                new Product("p1", 100),
                new Product("p2", 100),
                new Product("p3", 100)

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
            
            string filePath = "../../../../OrderHistory.xml";


            //act
            GenerateOrderHistory(oh, filePath);

            OrderHistory dataFromJson = GetInitialData(filePath); ;
            Console.WriteLine(dataFromJson.GetOrderHistory().ToList()[0].Products[0].Name);
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


        private static Location SetLocation(List<Order> oh)
        {
            //if the order histor is empty, initialize new location inventories
            Location currentLoc = null;
            Location loc1 = null;
            Location loc2 = null;
            Location loc3 = null;
            if (oh == null)
            {
                loc1 = new Location("Store 1", 1);
                loc2 = new Location("Store 2", 2);
                loc3 = new Location("Store 3", 3);
            }
            
            //create a menu to select which store 
            string menu = "Select store location:\n"
                        + "1. Store 1\n"
                        + "2. Store 2\n"
                        + "3. Store 3\n"
                        + "4. Go back";

            Console.WriteLine(menu);
            int selection = Console.Read();
            while (selection != 4)
            {
                if (selection == 1 || selection == 2 || selection == 3)
                {   
                    //if the order history is empty just use the previously initialized locations
                    if (oh == null)
                    {
                        switch(selection)
                        {
                            case 1:
                                currentLoc = loc1;
                                break;
                            case 2:
                                currentLoc = loc2;
                                break;
                            case 3:
                                currentLoc = loc3;
                                break;
                        }
                        
                    }
                    //else check for an order in the order histor for a store ID that matches 
                    //the user selection in and continue to update that store's inventory
                    else
                    {
                        //foreach(var order in oh)
                        //{
                        //    if(order.StoreLocation.LocationID == selection)
                        //    {
                        //        currentLoc = order.StoreLocation;
                        //        break;
                        //    }
                        //}
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Selection. Please type 1-4.");
                }
                Console.WriteLine(menu);
                selection = Console.Read();
            }
            return currentLoc;
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
            //Generate a new file and output the order history after converting to json format
            
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
            catch(FileNotFoundException)
            {
                Console.WriteLine("No data exists. Must first create a data file.");
                return new OrderHistory(new List<Order>());
            }
            
        }

        public static string ConvertToJson(Object obj)
        { 
            //This converts the Order History repo to a json format to write to a file
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
