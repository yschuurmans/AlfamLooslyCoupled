using Customer.Infra.Messaging;
using System;

namespace Customer.Commands
{
    public class CreateCustomer : Command
    {
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        public CreateCustomer(){

        }

        public CreateCustomer(string customerNumber, string firstname, string lastname, string postalCode, string city, 
            string email)
        {
            CustomerNumber = customerNumber;
            Firstname = firstname;
            Lastname = lastname;
            PostalCode = postalCode;
            City = city;
            Email = email;
        }
    }
}