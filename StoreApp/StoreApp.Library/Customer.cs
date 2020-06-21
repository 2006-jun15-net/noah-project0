using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StoreApp.Library
{
    public class Customer
    {
        private static int customerIDSeed = 0;

        public int CustomerID { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Order> OrderHistory { get; set; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            CustomerID = customerIDSeed++;

        }


    }
}
