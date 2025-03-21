// Базовый интерфейс команды
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Infrastructure.Export;
using FinanceTracker.Infrastructure.Import;

// Команда для импорта счетов
namespace FinanceTracker.Application.Commands
{
    public class ImportBankAccountsCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly DataImporter _importer;
        private readonly string _filePath;

        public ImportBankAccountsCommand(BankAccountFacade bankAccountFacade, DataImporter importer, string filePath)
        {
            _bankAccountFacade = bankAccountFacade;
            _importer = importer;
            _filePath = filePath;
        }

        public void Execute()
        {
            var accounts = _importer.ImportBankAccounts(_filePath);
            foreach (var account in accounts)
            {
                try
                {
                    _bankAccountFacade.CreateAccountWithId(account.Id, account.Name, account.Balance);
                    Console.WriteLine($"Создан счет: {account.Name} ({account.Id})");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Уже существующий счет с ID {account.Id}");
                    continue;
                }
            }
            Console.WriteLine($"Импортировано счетов: {accounts.Count}");
        }
    }
}