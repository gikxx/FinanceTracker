using FinanceTracker.Application.Commands;
using FinanceTracker.Application.Decorators;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Presentation.ConsoleUI;
using FinanceTracker.Presentation.Helpers;

public class OperationMenu
{
    private readonly OperationFacade _operationFacade;
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly CategoryFacade _categoryFacade;

    public OperationMenu(
        OperationFacade operationFacade,
        BankAccountFacade bankAccountFacade,
        CategoryFacade categoryFacade)
    {
        _operationFacade = operationFacade;
        _bankAccountFacade = bankAccountFacade;
        _categoryFacade = categoryFacade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление операциями ===");
            Console.WriteLine("1. Создать операцию");
            Console.WriteLine("2. Просмотреть все операции");
            Console.WriteLine("3. Удалить операцию"); // Новый пункт
            Console.WriteLine("4. Назад");
            Console.Write("Нажмите клавишу 1-4: ");

            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    CreateOperation();
                    break;
                case ConsoleKey.D2:
                    ShowAllOperations();
                    break;
                case ConsoleKey.D3:
                    DeleteOperation(); // Вызов нового метода
                    break;
                case ConsoleKey.D4:
                    return;
                default:
                    Console.WriteLine("\nОшибка: неверная клавиша!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void CreateOperation()
    {
        var type = InputHelper.ReadEnum<OperationType>("Тип операции:");
        int accountId = InputHelper.ReadInt("ID счета: ");
        decimal amount = InputHelper.ReadDecimal("Сумма: ", minValue: 0.01m);
        DateTime date = InputHelper.ReadDate("Дата (ГГГГ-ММ-ДД): ");
        int categoryId = InputHelper.ReadInt("ID категории: ");

        var command = new CreateOperationCommand(
            _operationFacade,
            type,
            accountId,
            amount,
            date,
            categoryId
        );
        var timedCommand = new TimingCommandDecorator(command);
        bool success = CommandRunner.TryExecute(timedCommand);

        if (!success)
        {
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    private void ShowAllOperations()
    {
        var command = new ListOperationsCommand(_operationFacade);
        bool success = CommandRunner.TryExecute(command);

        Console.WriteLine("\nНажмите Enter для продолжения...");
        Console.ReadLine();
    }

    private void DeleteOperation()
    {
        int operationId = InputHelper.ReadInt("Введите ID операции для удаления: ");

        var command = new DeleteOperationCommand(_operationFacade, operationId);
        bool success = CommandRunner.TryExecute(command);

        if (!success)
        {
            Console.WriteLine("Ошибка при удалении операции.");
        }

        Console.WriteLine("Нажмите Enter для продолжения...");
        Console.ReadLine();
    }
}
