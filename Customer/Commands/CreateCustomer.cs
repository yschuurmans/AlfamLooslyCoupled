using Customer.Infra.Messaging;
using System;

namespace Customer.Commands
{
    public class CreateCustomer : Command
    {
        public readonly string CustomerNumber;
        public readonly string Firstname;
        public readonly string Lastname;
        public readonly string PostalCode;
        public readonly string City;
        public readonly string Email;

        public CreateCustomer(Guid messageId, string customerNumber, string firstname, string lastname, string postalCode, string city, 
            string email) : base(messageId)
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