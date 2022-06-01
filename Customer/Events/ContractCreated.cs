using Customer.Infra.Messaging;
using System;

namespace Contract.Events 
{
    public class ContractCreated : Event
    {
        public readonly string ContractNumber;
        public readonly string Type;
        public readonly string IBAN;
        public readonly double Budget;

        public ContractCreated(Guid messageId, string contractNumber, string type, string iBAN, double budget) : base(messageId)
        {
            ContractNumber = contractNumber;
            Type = type;
            IBAN = iBAN;
            Budget = budget;
        }
    }
}