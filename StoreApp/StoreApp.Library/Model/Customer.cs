using System;
using System.Collections.Generic;

namespace StoreApp.Library.Model
{
    public class Customer
    {
        private static int customerIDSeed = 1;

        public int CustomerID { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Customer()
        {
            CustomerID = 0;
            FirstName = null;
            LastName = null;
        }
        
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            CustomerID = customerIDSeed;
            customerIDSeed++;

        }


    }
}
