using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;

namespace FinanceTracker.Application.Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private readonly BankAccountFacade _facade;
        private readonly string _name;
        private readonly decimal _initialBalance;

        public CreateBankAccountCommand(BankAccountFacade facade, string name, decimal balance)
        {
            _facade = facade;
            _name = name;
            _initialBalance = balance;
        }

        public void Execute()
        {
            var account = _facade.CreateAccount(_name, _initialBalance);
            Console.WriteLine($"Счет создан: {account.Name}");
        }
    }
}