using Customer.Commands;
using Customer.Events;

namespace Customer.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerCreated MapToCustomerCreated(this CreateCustomer command) => new CustomerCreated
        (
            System.Guid.NewGuid(),
            command.CustomerNumber,
            command.Firstname,
            command.Lastname,
            command.PostalCode,
            command.City,
            command.Email
        );

        public static CustomerUpdated MapToCustomerUpdated(this UpdateCustomer command) => new CustomerUpdated
        (
            System.Guid.NewGuid(),
            command.CustomerNumber,
            command.Firstname,
            command.Lastname,
            command.PostalCode,
            command.City,
            command.Email
        );

        public static Model.Customer MapToCustomer(this CreateCustomer command) => new Model.Customer
        {
            CustomerNumber = command.CustomerNumber,
            Firstname = command.Firstname,
            Lastname = command.Lastname,
            PostalCode = command.PostalCode,
            City = command.City,
            Email = command.Email,
        };

        public static Model.Customer MapToCustomer(this UpdateCustomer command) => new Model.Customer
        {
            CustomerNumber = command.CustomerNumber,
            Firstname = command.Firstname,
            Lastname = command.Lastname,
            PostalCode = command.PostalCode,
            City = command.City,
            Email = command.Email,
        };
    }
}