using Contract.Infra.Messaging;
using System;

namespace Contract.Commands
{
    public class UpdateContract : Command
    {
        public string ContractNumber;
        public string Type;
        public string IBAN;
        public double Budget;

        public UpdateContract()
        {
        }

        public UpdateContract(string contractNumber, string type, string iBAN, double budget) : base(new Guid())
        {
            ContractNumber = contractNumber;
            Type = type;
            IBAN = iBAN;
            Budget = budget;
        }
    }
}