using Newtonsoft.Json;
using StoreApp.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Common;
using System.Dynamic;
using System.IO;
using System.Transactions;

namespace StoreApp.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orderHistory = GetInitialData();
            
            
            Customer customer;
            
            int selection = 1;

            while (selection != 5)
            {
                Console.WriteLine(GetMenu() + "\nPlease enter an option: ");
                selection = Console.Read();
                if (selection == 1 || selection == 2 || selection == 3 || selection == 4)
                {
                    switch(selection)
                    {
                        case 1:  //add a new customer
                            customer = RegisterCustomer(orderHistory);

                            break;
                        case 2: //place an order
                            PlaceOrder(orderHistory);
                        case 3://Search customers

                        case 4://Print order history

                    }
                }
                else
                {
                    Console.WriteLine("Invalid Option. Please type 1-5");
                }
            }
            //Place orders to store locations for customers

           

            //search customers by name

            //display details of an order

            //display all order history of a store location

            //display all order history of a customer


        }

        private static void PlaceOrder(List<Order> oh)
        {
            var currentLocation = SetLocation(oh);
            if(currentLocation == null)
            {
                Console.WriteLine("No location was set. "
                                + "Cannot place order without setting a location.");
                return;
            }
            else
            {
                //get the inventory product names from the store location loc
                Dictionary<Product, int>.KeyCollection products = currentLocation.Inventory.Keys;
                string productNames = "";
                int count = 0;
                foreach (Product p in products)
                {
                    count++;
                    productNames += $"{count}. {p.Name}\n";
                }
                string menu = "What would you like to add to your order?\n" + productNames;


            }
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
                            case 2:
                                currentLoc = loc2;
                            case 3:
                                currentLoc = loc3;
                        }
                        
                    }
                    //else check for an order in the order histor for a store ID that matches 
                    //the user selection in and continue to update that store's inventory
                    else
                    {
                        foreach(var order in oh)
                        {
                            if(order.StoreLocation.LocationID == selection)
                            {
                                currentLoc = order.StoreLocation;
                                break;
                            }
                        }
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
                if(fn == order.customer.FirstName && ln == order.customer.LastName)
                {
                    Console.WriteLine("This name already exists.");
                    return order.customer;
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

        public static string GenerateOrderHistory(List<Order> OrderHistory)
        {
            //Generate a new file and output the order history after converting to json format
            string filePath = "../../../OrderHistory.json";
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    return ConvertToJson(OrderHistory);
                }
                
            }
            catch (IOException e)
            {
                Console.WriteLine($"Error writing file: {e.Message}");
                return "";
            }
            
        }
        public static List<Order> GetInitialData()
        {
            //Try to read in a json file and assign it to a list of orders (aka an order history)
            //but if none exists just return a null order history
            try
            {
                string filePath = $"../../../OrderHistory.json";
                string initialData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Order>>(initialData);
               
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("No data exists. Must first create a data file.");
                return new List<Order>();
            }
            
        }

        public static string ConvertToJson(List<Order> OrderHistory)
        {
            //This converts the list of orders to a json format to write to a file
            return JsonConvert.SerializeObject(OrderHistory, Formatting.Indented);
        }
    }
}
