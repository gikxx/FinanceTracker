using FinanceTracker.Application.Commands;
using FinanceTracker.Application.Decorators;
using FinanceTracker.Presentation.ConsoleUI;
using FinanceTracker.Presentation.Helpers;

public class AccountMenu
{
    private readonly BankAccountFacade _facade;

    public AccountMenu(BankAccountFacade facade)
    {
        _facade = facade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление счетами ===");
            Console.WriteLine("1. Создать счет");
            Console.WriteLine("2. Пополнить счет");
            Console.WriteLine("3. Список всех счетов");
            Console.WriteLine("4. Удалить счет"); // Новый пункт
            Console.WriteLine("5. Назад");
            Console.Write("Нажмите клавишу 1-5: ");

            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    CreateAccount();
                    break;
                case ConsoleKey.D2:
                    DepositToAccount();
                    break;
                case ConsoleKey.D3:
                    ShowAllAccounts();
                    break;
                case ConsoleKey.D4:
                    DeleteAccount(); 
                    break;
                case ConsoleKey.D5:
                    return;
                default:
                    Console.WriteLine("\nОшибка: неверная клавиша!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void CreateAccount()
    {
        string name = InputHelper.ReadString("Введите название счета: ");
        decimal balance = InputHelper.ReadDecimal("Введите начальный баланс: ", minValue: 0);

        var command = new CreateBankAccountCommand(_facade, name, balance);
        var timedCommand = new TimingCommandDecorator(command);
        bool success = CommandRunner.TryExecute(timedCommand);

        if (!success)
        {
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    private void DepositToAccount()
    {
        int accountId = InputHelper.ReadInt("Введите ID счета: ");
        decimal amount = InputHelper.ReadDecimal("Введите сумму для пополнения: ", minValue: 0.01m);

        var command = new DepositCommand(_facade, accountId, amount);
        var timedCommand = new TimingCommandDecorator(command);
        bool success = CommandRunner.TryExecute(timedCommand);

        if (!success)
        {
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    private void ShowAllAccounts()
    {
        var command = new ListAccountsCommand(_facade);
        bool success = CommandRunner.TryExecute(command);

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }

    private void DeleteAccount()
    {
        int accountId = InputHelper.ReadInt("Введите ID счета для удаления: ");

        var command = new DeleteBankAccountCommand(_facade, accountId);
        bool success = CommandRunner.TryExecute(command);

        if (!success)
        {
            Console.WriteLine("Ошибка при удалении счета.");
        }

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }
}
