using StoreApp.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Customer
    {
        private string _UserName;
        private string _FirstName;
        private string _LastName;
        /// <summary>
        /// Id for the customer
        /// </summary>
        public int CustomerId { get; set; } = 0;
        
        /// <summary>
        /// Username for the customer
        /// </summary>
        public string UserName 
        {
            get => _UserName;
          set
            {
                if(value.Length == 0)
                {
                    throw new ArgumentException("Must have a username.", nameof(value));
                }
                else
                {
                    _UserName = value;
                }
            }
        }

        /// <summary>
        /// First name of the customer
        /// </summary>
        public string FirstName
        {
            get => _FirstName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Must have a last name.", nameof(value));
                }
                else
                {
                    _FirstName = value;
                }
            }
        }
        /// <summary>
        /// Last name of the customer
        /// </summary>
        public string LastName
        {
            get => _LastName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Must have a last name.", nameof(value));
                }
                else
                {
                    _LastName = value;
                }
            }
        }
        /// <summary>
        /// Order history of the customer
        /// </summary>
        public List<Order> OrderHistory { get; set; } = new List<Order>();


    }
}
