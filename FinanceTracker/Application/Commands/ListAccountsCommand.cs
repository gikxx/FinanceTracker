using FinanceTracker.Domain.Models;

namespace FinanceTracker.Application.Commands
{
    public class ListAccountsCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;

        public ListAccountsCommand(BankAccountFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public void Execute()
        {
            var accounts = _bankAccountFacade.GetAllAccounts();

            Console.WriteLine("\n=== Список счетов ===");
            if (accounts.Count == 0)
            {
                Console.WriteLine("Нет доступных счетов.");
            }
            else
            {
                foreach (var account in accounts)
                {
                    Console.WriteLine($"[{account.Id}] {account.Name} — Баланс: {account.Balance}");
                }
            }

        }
    }
}
