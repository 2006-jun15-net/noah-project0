using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;
using StoreApp.DataAccess.Repos;
using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.App
{
    public class Program
    {
        //REQUIREMENTS:
        //place orders to store locations for customers
        //add a new customer
        //search customers by name
        //display details of an order
        //display all order history of a store location
        //display all order history of a customer

        static void Main(string[] args)
        {
            CustomerController cc = new CustomerController();
            OrderController oc = new OrderController();
            ProductController pc = new ProductController();
            StoreController sc = new StoreController();
            Customers currentCustomer = null;

            bool quit = false;
            Console.WriteLine("Welcome to the Store Application");
            string homeMenu =
                "What would you like to do:\n" +
                "1: Register as a new customer or sign in\n" +
                "2: Create new store\n" +
                "3: Create new product\n" +
                "4: Add products to a store\n" +
                "5. Place an order\n" +
                "6: Search for customer by username\n" +
                "7: Display details of an order\n" +
                "8: Display order history of a customer\n" +
                "9: Display order history of a store\n" +
                "10: Delete store\n" +
                "11: Unregister Customer\n" +
                "0: Quit";
            

            while(!quit)
            {
                Console.WriteLine(homeMenu);
                Console.WriteLine("Enter your selection(0-9):");
                int input = Int32.Parse(Console.ReadLine());
                if (input != 0 && input != 1 && input != 2 && input != 3 && input != 4 && input != 5 
                    && input != 6 && input != 7 && input != 8 && input != 9 && input != 10 && input != 11)
                {
                    Console.WriteLine("Invalid input. Please enter 0-9.");
                    input = Int32.Parse(Console.ReadLine());
                }
                

                switch(input)
                {
                    case 1:
                        //Register Customers
                        currentCustomer = RegisterCustomer(cc);
                        break;
                    case 2:
                        //Add Stores
                        BuildStore(sc);
                        break;
                    case 3:
                        //Add Products
                        BuildProducts(pc);
                        break;
                    case 4:
                        //Add Products to Stores
                        AddProductToStore(pc, sc);
                        break;
                    case 5:
                        //Place an order
                        if(currentCustomer == null)
                        {
                            Console.WriteLine("You must register or sign in first.");
                        }
                        else
                        {
                            PlaceOrder(currentCustomer, sc, pc, oc);
                        }
                        break;
                    case 6:
                        //Search customers by username
                        Console.WriteLine("Enter username to search:");
                        string username = Console.ReadLine();
                        cc.SearchCustomerByUsername(username);
                        break;
                    case 7:
                        //Get order details for a particular order
                        Console.WriteLine("Select the order you want details for:");
                        oc.DisplayOrders();
                        Console.WriteLine("Enter the Order ID to get details: ");
                        int oid = Int32.Parse(Console.ReadLine());
                        oc.DisplayOrderDetails(oid);
                        break;
                    case 8:
                        //Check current customer order history
                        if (currentCustomer == null)
                        {
                            Console.WriteLine("You must register or sign in first.");
                        }
                        else
                        {
                            Console.WriteLine($"Order history for current customer {currentCustomer.UserName}:");
                            oc.DisplayOrderDetailsOfCustomer(currentCustomer.CustomerId);
                        }
                        break;
                    case 9:
                        //Check order history for a particular store
                        Console.WriteLine("Select the store you want the order history for:");
                        sc.DisplayStores();
                        Console.WriteLine("Enter the store ID to see order history:");
                        int sid = Int32.Parse(Console.ReadLine());
                        Console.WriteLine($"Order history for {sc.repository.GetById(sid).StoreName}");
                        oc.DisplayOrderDetailsOfStore(sid);
                        break;
                    case 10:
                        //Delete a particular store
                        DeleteStore(sc);
                        break;
                    case 11:
                        //Delete the current customer
                        Console.WriteLine("Are you sure you want to unregister?(y/n)");
                        string choice = Console.ReadLine().ToLower();
                        while(!choice.Equals("y") && !choice.Equals("n"))
                        {
                            Console.WriteLine("Please type y or n.");
                            choice = Console.ReadLine().ToLower();
                        }
                        if(choice.Equals("y"))
                        {
                            UnregisterCustomer(cc, currentCustomer.CustomerId);
                            currentCustomer = null;
                        }
                        else
                        {
                            Console.WriteLine($"{currentCustomer.UserName} was not unregistered.");
                        }
                        
                        break;
                    case 0:
                        quit = true;
                        break;
                    
                }
            }
            
        }
        /// <summary>
        /// Removes a customer from the database
        /// </summary>
        /// <param name="cc">The CustomerController</param>
        /// <param name="id">The id of the customer</param>
        private static void UnregisterCustomer(CustomerController cc, int id)
        { 
            try
            {
                cc.repository.Delete(id);
                cc.repository.Save();
                Console.WriteLine("Unregistered successfully!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error occured when trying to unregister customer with ID: {id}");
            }
           
        }

        /// <summary>
        /// User interface that handles removing a store from the database
        /// </summary>
        /// <param name="sc">The StoreController</param>
        private static void DeleteStore(StoreController sc)
        {
            Console.WriteLine("Select store you want to delete:");
            sc.DisplayStores();
            Console.WriteLine("Enter the store ID to delete: ");
            int sid = Int32.Parse(Console.ReadLine());
            if(sc.repository.GetAll().Any(s => s.StoreId == sid))
            {
                try
                {
                    sc.repository.Delete(sid);
                    sc.repository.Save();
                    Console.WriteLine("Store deleted successfully!");
                }
                catch (Exception)
                {
                    Console.WriteLine($"Error occured when trying to delete store with ID: {sid}");
                }
            }
            else
            {
                Console.WriteLine($"No store with ID: {sid}");
            }
            
            
        }

        /// <summary>
        /// User interface that guides the user to place an order for the current customer.
        /// </summary>
        /// <param name="currentCustomer">The current customer placing the order</param>
        /// <param name="sc">The StoreController</param>
        /// <param name="pc">The ProductController</param>
        /// <param name="oc">The OrderController</param>
        private static void PlaceOrder(Customers currentCustomer, StoreController sc, ProductController pc, OrderController oc)
        {
            //Select the store you want to place an order to
            Console.WriteLine("Which store would you like to place an order to: ");
            sc.DisplayStores();
            Console.WriteLine("Enter the store ID: ");
            int sid = Int32.Parse(Console.ReadLine());

            //If the store id that was inputted exists list the products in that store
            if (sc.repository.GetAll().Any(s => s.StoreId == sid))
            {
                var currentStore = sc.repository.GetById(sid);
               
                using var context = new _2006StoreApplicationContext(GenericRepository<Stores>.Options);
                var inventory = context.Inventory
                    .Include(i => i.Product)
                    .Where(i => i.StoreId == sid)
                    .ToList();
                
                //Keep track of the number of products in the order to calculate the final total of the order later
                Dictionary<Products, int> productsInOrder = new Dictionary<Products, int>();
                
                //Keep asking the user to add more products to the order until they type 0
                while(true)
                {
                    Console.WriteLine("Select a product to add to order:");
                    foreach (var item in inventory)
                    {
                        Console.WriteLine($"Product: {item.Product.ProductName} Price: ${item.Product.Price} ID: {item.Product.ProductId} In Stock: {item.Amount}\n");
                    }
                    Console.WriteLine("Enter product ID to add to order(or type 0 to quit):");
                    int pid = Int32.Parse(Console.ReadLine());

                    //prevent the user from adding the same item to their order
                    if(productsInOrder != null)
                    {
                        while (productsInOrder.Keys.Any(p => p.ProductId == pid))
                        {
                            Console.WriteLine("Item was already added to order.");
                            foreach (var item in inventory)
                            {
                                Console.WriteLine($"Product: {item.Product.ProductName} Price: ${item.Product.Price} ID: {item.Product.ProductId} In Stock: {item.Amount}\n");
                            }
                            Console.WriteLine("Enter product ID to add to order(or type 0 to quit):");
                            pid = Int32.Parse(Console.ReadLine());
                        }
                    }
                    
                    
                    if(pid == 0) { break; }

                    //Check to see if the product id the user enter matches any of the products available in the store
                    if (inventory.Any(i => i.Product.ProductId == pid))
                    {
                        //Get the product info from the id the user entered, then get the amount from the inventory to 
                        //check that it is >= 0
                        var p = pc.repository.GetById(pid);
                        Console.WriteLine($"How many {p.ProductName}s do you want to add to the order:");
                        int qty = Int32.Parse(Console.ReadLine());
                        if (qty > 0)
                        {
                            
                            Inventory inventoryLine = inventory.First(i => i.Product.ProductId == pid);
                            if(inventoryLine.Amount == 0)
                            {
                                Console.WriteLine($"{p.ProductName} no longer in stock.");
                               
                            }
                            else if(inventoryLine.Amount < qty)
                            {
                                Console.WriteLine("You can't order more products than are available.");
                            }
                            //If the product is available and in stock, add keep track of the product, decrement the inventory,
                            //and update the inventory when selecting more products
                            else
                            {
                                productsInOrder.Add(p, qty);
                                inventoryLine.Amount -= qty;
                                context.Update(inventoryLine);
                                context.SaveChanges();
                                Console.WriteLine("Product added to order!");
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid qty. Input a positive integer.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Product with ID: {pid} is not available in the current store.");
                    }

                }
                //Check that the user actually selected products before actually creating the order in the database
                if(productsInOrder.Count == 0)
                {
                    Console.WriteLine("No products were added to order.");
                }
                else
                {
                    //calculate the total cost of the order and display it to the user
                    decimal totalCostOfOrder = 0;
                    foreach (var item in productsInOrder.Keys)
                    {
                        totalCostOfOrder += (item.Price * productsInOrder[item]);
                    }
                    Console.WriteLine("Total cost of your order: $" + totalCostOfOrder);

                    //ask for a description to uniquely identify the order in order to find it later
                    Console.WriteLine("Please provide a unique description for your order: ");
                    string desc = Console.ReadLine();
                    if (desc == null || oc.repository.GetAll().Any(o => o.OrderDescription.Equals(desc)))
                    {
                        Console.WriteLine("Description already exists or no description was entered.");

                    }
                    else
                    {
                        //Save the order, then retrieve it again to create the orderline for it
                        Orders newOrder = new Orders { CustomerId = currentCustomer.CustomerId, StoreId = currentStore.StoreId, OrderDescription = desc, TotalCost = totalCostOfOrder };
                        oc.repository.Add(newOrder);
                        oc.repository.Save();

                        newOrder = oc.repository.GetAll().First(o => o.OrderDescription.Equals(desc));

                        //Link products that were in the recently created order to the orderId 
                        //This creates a new OrderLine that keeps track of what products belong to what order
                        foreach (var item in productsInOrder.Keys)
                        {
                            var product = context.Products
                                .Include(p => p.OrderLines)
                                .First(p => p.ProductId == item.ProductId);
                            product.OrderLines.Add(new OrderLines { Order = newOrder, Amount = productsInOrder[item] });
                        }

                        context.SaveChanges();

                    }
                }
                

            }
            else
            {
                Console.WriteLine($"Store with ID: {sid} does not exist.");
            }

        }

        /// <summary>
        /// User interface that guides the user to add Products to a single store
        /// </summary>
        /// <param name="pc">The ProductController</param>
        /// <param name="sc">The StoreController</param>
        private static void AddProductToStore(ProductController pc, StoreController sc)
        {
            //Select the store to add products to and make sure it exists
            Console.WriteLine("Which store do you want to add products to: \n");
            sc.DisplayStores();
            Console.WriteLine("Enter Store ID: ");
            int sid = Int32.Parse(Console.ReadLine());

            if(sc.repository.GetAll().Any(s => s.StoreId == sid))
            {
                //Display the full set of products that exist in the database
                Console.WriteLine($"Which products do you want to add to the store: {sc.repository.GetById(sid).StoreName}");
                pc.DisplayProducts();

                //Select the product id, making sure it exists, to link a product to the store
                Console.WriteLine("Enter Product ID to add to store: ");
                int pid = Int32.Parse(Console.ReadLine());
                if(pc.repository.GetAll().Any(p => p.ProductId == pid))
                {
                    //Get that product to be later added to the store inventory
                    Products product = pc.repository.GetById(pid);
                    //Enter the amount of products to be stored in the stores inventory
                    Console.WriteLine("Enter the quantity of the product to be added:");
                    int qty = Int32.Parse(Console.ReadLine());
                    //Try adding the products to the store
                    //If the product already existed in the store the product won't be added and an error is thrown
                    try
                    {
                        using var context = new _2006StoreApplicationContext(GenericRepository<Products>.Options);
                        var store = context.Stores
                            .Include(s => s.Inventory)
                            .First(s => s.StoreId == sid);
                        store.Inventory.Add(new Inventory { Product = product, Amount = qty });
                        context.SaveChanges();
                        Console.WriteLine("Product was added to the store!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error occurred when trying to add product to store.");
                    }

                }
                else
                {
                    Console.WriteLine($"No products with ID: {pid}");
                }
            }
            else
            {
                Console.WriteLine($"No stores with ID: {sid}");
            }

        }

        /// <summary>
        /// User interface that guides the user to create a new product and add it to the database
        /// </summary>
        /// <param name="pc">The ProductController</param>
        private static void BuildProducts(ProductController pc)
        {
            //Give the product a name and then make sure it doesn't already exist
            Console.WriteLine("Enter Product name:");
            string pname = Console.ReadLine();
            if(pc.repository.GetAll().Any(p => p.ProductName.Equals(pname, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Product already exists with name: {pname}");
            }
            else
            {
                //Give the product a price that can't be zero or negative
                Console.WriteLine("Enter price for the product:");
                decimal price = Decimal.Parse(Console.ReadLine());
                if (price <= 0)
                {
                    Console.WriteLine("Price can't be negative or zero.");
                }
                else
                {
                    //Create the new product with the inputted properties and save to the database
                    Products newProduct = new Products { ProductName = pname, Price = price };
                    pc.repository.Add(newProduct);
                    pc.repository.Save();
                    newProduct = pc.repository.GetAll().First(p => p.ProductName.Equals(pname));
                    Console.WriteLine($"Product was added! {newProduct.ProductName} Price: {newProduct.Price} ID: {newProduct.ProductId}");
                }
                
            }
        }

        /// <summary>
        /// User interface that guides the user to create a new store and adds it to the database
        /// </summary>
        /// <param name="sc">The StoreController</param>
        private static void BuildStore(StoreController sc)
        {
            //Give the new store a name
            Console.WriteLine("Enter new store name: ");
            string sname = Console.ReadLine();
            if(sc.repository.GetAll().Any(s => s.StoreName.Equals(sname)))
            {
                Console.WriteLine($"Store already exists with name: {sname}");
            }
            else
            {
                //Create a new store and set its name property, then save it to the database
                Stores newStore = new Stores { StoreName = sname };
                sc.repository.Add(newStore);
                sc.repository.Save();
                newStore = sc.repository.GetAll().First(s => s.StoreName.Equals(sname));
                Console.WriteLine($"New store added: {newStore.StoreName} ID: {newStore.StoreId}");
            }
            
        }

        /// <summary>
        /// User interface that allows the user to register as a new customer or sign in as an existing customer
        /// A new customer is added to the database
        /// </summary>
        /// <param name="cc">The CustomerController</param>
        /// <returns>The Customer (either recently created or taken from the database)</returns>
        private static Customers RegisterCustomer(CustomerController cc)
        {
            //Ask if the user is registered or not
            Customers currentCustomer = new Customers();
            Console.WriteLine("Register as new customer.(If already registered type 1, else type 2):");
            int input = Int32.Parse(Console.ReadLine());

            //If they are not registered, display a list of registered customers
            if (input == 1)
            {
                if (cc.repository.GetAll().FirstOrDefault() == null)
                {
                    Console.WriteLine("No registered customers");
                }
                else
                {
                    cc.DisplayCustomers();
                    Console.WriteLine("Enter customer ID to begin: ");
                    int cid = Int32.Parse(Console.ReadLine());
                    currentCustomer = cc.repository.GetById(cid);
                   
                }
            }
            //if they are not registered, get first name, last name, and unique username
            else if (input == 2)
            {
                Console.WriteLine("Enter First Name:");
                string fn = Console.ReadLine();
                Console.WriteLine("Enter Last Name: ");
                string ln = Console.ReadLine();
                Console.WriteLine("Enter UserName:");
                string un = Console.ReadLine();
                if (cc.repository.GetAll().Any(c => c.UserName == un))
                {
                    Console.WriteLine($"Username: {un} already exists.");
                }
                else
                {
                    //Initialize a new a customer with above credentials, add it to the database, and display newly created id
                    Customers newCustomer = new Customers { FirstName = fn, LastName = ln, UserName = un };
                    cc.repository.Add(newCustomer);
                    cc.repository.Save();
                    currentCustomer = cc.repository.GetById(cc.repository.GetAll().First(c => c.UserName == un).CustomerId);
                    Console.WriteLine($"Registration successful! Username: {un} ID: {currentCustomer.CustomerId}");
                }

            }
            else
            {
                Console.WriteLine("Invalid input");
            }
            return currentCustomer;
        }
    }
}
