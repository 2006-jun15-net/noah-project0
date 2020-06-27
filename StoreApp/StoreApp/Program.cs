using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using StoreApp.DataAccess.Model;
using StoreApp.DataAccess.Repos;
using StoreApp.Library.Model;
using System;
using System.Linq;

namespace StoreApp.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            CustomerController cc = new CustomerController();
            OrderController oh = new OrderController();
            ProductController pc = new ProductController();
            StoreController sc = new StoreController();
            Customers currentCustomer;
            //Register Customers
            //RegisterCustomer(cc);
            

            //cc.DisplayCustomers();


            //Add Stores
            //BuildStore(sc);

            //Add Products
            //BuildProducts(pc);

            //Add Products to Stores
            //AddProductToStore(pc, sc);

            //Add Orders by Customers

            //REQUIREMENTS:
            //place orders to store locations for customers
            //add a new customer
            //search customers by name
            //display details of an order
            //display all order history of a store location
            //display all order history of a customer


        }

        private static void AddProductToStore(ProductController pc, StoreController sc)
        {
            Console.WriteLine("Which store do you want to add products to: \n");
            sc.DisplayStores();
            Console.WriteLine("Enter Store ID: ");
            int sid = Int32.Parse(Console.ReadLine());
            if(sc.repository.GetAll().Any(s => s.StoreId == sid))
            {
                Stores store = sc.repository.GetById(sid);
                Console.WriteLine($"Which products do you want to add to the store: {store.StoreName}");
                pc.DisplayProducts();
                Console.WriteLine("Enter Product ID to add to store: ");
                int pid = Int32.Parse(Console.ReadLine());
                if(pc.repository.GetAll().Any(p => p.ProductId == pid))
                {
                    Console.WriteLine("Enter the quantity of the product to be added:");
                    int qty = Int32.Parse(Console.ReadLine());
                    using var context = new _2006StoreApplicationContext(GenericRepository<Stores>.Options);
                    var product = context.Products
                        .Include(p => p.Inventory)
                        .First(p => p.ProductId == pid);
                    product.Inventory.Add(new Inventory { Store = store, Amount = qty});
                    context.SaveChanges();
                    Console.WriteLine("Product was added to the store!");


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

        private static void BuildProducts(ProductController pc)
        {
            Console.WriteLine("Enter Product name:");
            string pname = Console.ReadLine();
            if(pc.repository.GetAll().Any(p => p.ProductName.Equals(pname)))
            {
                Console.WriteLine($"Product already exists with name: {pname}");
            }
            else
            {
                Console.WriteLine("Enter price for the product:");
                decimal price = Decimal.Parse(Console.ReadLine());
                if (price <= 0)
                {
                    Console.WriteLine("Price can't be negative or zero.");
                }
                else
                {
                    Products newProduct = new Products { ProductName = pname, Price = price };
                    pc.repository.Add(newProduct);
                    pc.repository.Save();
                    newProduct = pc.repository.GetAll().First(p => p.ProductName.Equals(pname));
                    Console.WriteLine($"Product was added! {newProduct.ProductName} Price: {newProduct.Price} ID: {newProduct.ProductId}");
                }
                
            }
        }

        private static void BuildStore(StoreController sc)
        {
            Console.WriteLine("Enter new store name: ");
            string sname = Console.ReadLine();
            if(sc.repository.GetAll().Any(s => s.StoreName.Equals(sname)))
            {
                Console.WriteLine($"Store already exists with name: {sname}");
            }
            else
            {
                Stores newStore = new Stores { StoreName = sname };
                sc.repository.Add(newStore);
                sc.repository.Save();
                newStore = sc.repository.GetAll().First(s => s.StoreName.Equals(sname));
                Console.WriteLine($"New store added: {newStore.StoreName} ID: {newStore.StoreId}");
            }
            
        }

        private static void RegisterCustomer(CustomerController cc)
        {
            Customers currentCustomer = new Customers();
            Console.WriteLine("Register as new customer.(If already registered type 1, else type 2):");
            int input = Int32.Parse(Console.ReadLine());
            if (input == 1)
            {
                if (cc.repository.GetAll().FirstOrDefault() == null)
                {
                    Console.WriteLine("No registered customers");
                }
                else
                {
                    Console.WriteLine("List of Customers:\n");
                    foreach (var c in cc.repository.GetAll().ToList())
                    {
                        Console.WriteLine($"{c.FirstName} {c.LastName} ID: {c.CustomerId}");
                    }
                   
                }
            }
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
            
        }
    }
}
