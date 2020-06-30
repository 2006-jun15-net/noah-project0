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
        public int CustomerId { get; set; } = 0;
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
        public List<Order> OrderHistory { get; set; } = new List<Order>();


    }
}
