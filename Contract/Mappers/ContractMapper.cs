using Contract.Commands;
using Contract.Events;
using System;

namespace Contract.Mappers
{
    public static class ContractMapper
    {
        public static ContractCreated MapToContractCreated(this CreateContract command) => new ContractCreated
        (
            new Guid(),
            command.ContractNumber,
            command.Type,
            command.IBAN,
            command.Budget,
            ""
        );

        public static ContractUpdated MapToContractUpdated(this UpdateContract command) => new ContractUpdated
        (
            new Guid(),
            command.ContractNumber,
            command.Type,
            command.IBAN,
            command.Budget
        );

        public static Model.Contract MapToContract(this CreateContract command) => new Model.Contract
        {
            ContractNumber = command.ContractNumber,
            Type = command.Type,
            IBAN = command.IBAN,
            Budget = command.Budget
        };

        public static Model.Contract MapToContract(this UpdateContract command) => new Model.Contract
        {
            ContractNumber = command.ContractNumber,
            Type = command.Type,
            IBAN = command.IBAN,
            Budget = command.Budget
        };
    }
}