using System;
using System.Collections.Generic;

namespace StoreApp.Library.Model
{
    public class Customer
    {
        private static int customerIDSeed = 0;

        public int CustomerID { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Customer()
        {
            FirstName = null;
            LastName = null;
        }
        
        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            CustomerID = customerIDSeed++;

        }


    }
}
