using FinanceTracker.Application.Services;
using FinanceTracker.Presentation.Helpers;

public class MainMenu
{
    private readonly BankAccountFacade _accountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;
    private readonly AnalyticsFacade _analyticsFacade;
    private readonly ShareMenu _shareMenu;

    public MainMenu(
        BankAccountFacade accountFacade,
        CategoryFacade categoryFacade,
        OperationFacade operationFacade,
        AnalyticsFacade analyticsFacade,
        ShareMenu shareMenu)
    {
        _accountFacade = accountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _analyticsFacade = analyticsFacade;
        _shareMenu = shareMenu;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Учет финансов ===");
            Console.WriteLine("1. Управление счетами");
            Console.WriteLine("2. Управление категориями");
            Console.WriteLine("3. Управление операциями");
            Console.WriteLine("4. Аналитика");
            Console.WriteLine("5. Обмен данными");
            Console.WriteLine("6. Выход");
            Console.Write("Нажмите клавишу 1-6: ");

            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    new AccountMenu(_accountFacade).Show();
                    break;
                case ConsoleKey.D2:
                    new CategoryMenu(_categoryFacade).Show();
                    break;
                case ConsoleKey.D3:
                    new OperationMenu(_operationFacade, _accountFacade, _categoryFacade).Show();
                    break;
                case ConsoleKey.D4:
                    ShowAnalyticsMenu();
                    break;
                case ConsoleKey.D5: 
                    _shareMenu.Show();
                    break;
                case ConsoleKey.D6:
                    return;
                default:
                    Console.WriteLine("\nОшибка: неверная клавиша!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void ShowAnalyticsMenu()
    {
        DateTime startDate = InputHelper.ReadDate("Начальная дата (ГГГГ-ММ-ДД): ");
        DateTime endDate = InputHelper.ReadDate("Конечная дата (ГГГГ-ММ-ДД): ");

        try
        {
            decimal netIncome = _analyticsFacade.CalculateNetIncome(startDate, endDate);
            Console.WriteLine($"Чистый доход за период: {netIncome:C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\nНажмите Enter для продолжения...");
        Console.ReadLine();
    }
}