using Contract.Infra.Messaging;
using System;

namespace Contract.Commands
{
    public class CreateContract : Command
    {
        public readonly string ContractNumber;
        public readonly string Type;
        public readonly string IBAN;
        public readonly double Budget;

        public CreateContract(Guid messageId, string contractNumber, string type, string iBAN, double budget) : base(messageId)
        {
            ContractNumber = contractNumber;
            Type = type;
            IBAN = iBAN;
            Budget = budget;
        }
    }
}