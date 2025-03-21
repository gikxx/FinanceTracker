using System;

namespace FinanceTracker.Application.Commands
{
    public class DepositCommand : ICommand
    {
        private readonly BankAccountFacade _facade;
        private readonly int _accountId;
        private readonly decimal _amount;

        public DepositCommand(BankAccountFacade facade, int accountId, decimal amount)
        {
            _facade = facade;
            _accountId = accountId;
            _amount = amount;
        }

        public void Execute()
        {
            _facade.Deposit(_accountId, _amount);
            Console.WriteLine($"Счет {_accountId} пополнен на {_amount}");
        }
    }
}