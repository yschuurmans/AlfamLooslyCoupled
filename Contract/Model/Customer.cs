using System.Collections.Generic;

namespace Contract.Model
{
    public class Customer {
        public string CustomerNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Contract> Contracts { get;set; }
    }
}