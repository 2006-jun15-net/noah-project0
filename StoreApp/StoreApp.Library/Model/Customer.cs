using StoreApp.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// Model for Customer type
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Required customer fields
        /// </summary>
        /// Username needed to login, should be unique
        private string _UserName;
        /// <summary>
        /// Strings that store first and last name of customer
        /// </summary>
        private string _FirstName;
        private string _LastName;
        /// <summary>
        /// Required customer properties to allow data access(get/set)
        /// </summary>
        /// Customer ID used to uniquely identify customers, by default set to 0
        public int CustomerId { get; set; } = 0;

        /// <summary>
        /// Username property, if length of username is 0, throw exception saying the username cannot be omitted
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
        /// FirstName property, if length of first name is 0, throw exception saying the username cannot be omitted
        /// </summary>
        public string FirstName
        {
            get => _FirstName;
            set
            {
                if (value.Length == 0)
                {
                    throw new ArgumentException("Must have a first name.", nameof(value));
                }
                else
                {
                    _FirstName = value;
                }
            }
        }
        /// <summary>
        /// Lastname property, checks if lastname length is 0, if so throw an exception
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
        /// List object that stores orders 
        /// </summary>
        /// <remarks>
        /// Tracks all the orders made by customer
        /// </remarks>
        public List<Order> OrderHistory { get; set; } = new List<Order>();


    }
}
