using Contract.Infra.Messaging;
using System;

namespace Contract.Events 
{
    public class ContractCreated : Event
    {
        public readonly string ContractNumber;
        public readonly string Type;
        public readonly string IBAN;
        public readonly double Budget;
        public readonly string CustomerNumber;

        public ContractCreated(Guid messageId, string contractNumber, string type, string iBAN, double budget, string customerNumber) : base(messageId)
        {
            ContractNumber = contractNumber;
            Type = type;
            IBAN = iBAN;
            Budget = budget;
            CustomerNumber = customerNumber;
        }
    }
}