using Contract.Infra.Messaging;
using System;

namespace Contract.Events 
{
    public class ContractUpdated : Event
    {
        public readonly string CustomerNumber;
        public readonly string Firstname;
        public readonly string Lastname;
        public readonly string PostalCode;
        public readonly string City;
        public readonly string Email;

        public ContractUpdated(Guid messageId, string customerNumber, string firstname, string lastname, string postalCode, string city, 
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