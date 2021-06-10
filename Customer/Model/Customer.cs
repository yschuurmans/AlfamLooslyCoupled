using System.Collections.Generic;

namespace Customer.Model
{
    public class Customer {
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public List<Contract> Contracts { get;set; }
    }
}