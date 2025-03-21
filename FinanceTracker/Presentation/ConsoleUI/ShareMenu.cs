using FinanceTracker.Application.Commands;
using FinanceTracker.Infrastructure.Export;
using FinanceTracker.Infrastructure.Import;
using FinanceTracker.Presentation.ConsoleUI;
using FinanceTracker.Presentation.Helpers;
using Microsoft.Extensions.DependencyInjection;

public class ShareMenu
{
    private readonly BankAccountFacade _accountFacade;
    private readonly CategoryFacade _categoryFacade;
    private readonly OperationFacade _operationFacade;
    private readonly IServiceProvider _serviceProvider;

    public ShareMenu(
        BankAccountFacade accountFacade,
        CategoryFacade categoryFacade,
        OperationFacade operationFacade,
        IServiceProvider serviceProvider)
    {
        _accountFacade = accountFacade;
        _categoryFacade = categoryFacade;
        _operationFacade = operationFacade;
        _serviceProvider = serviceProvider;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Обмен данными ===");
            Console.WriteLine("1. Импорт данных");
            Console.WriteLine("2. Экспорт данных");
            Console.WriteLine("3. Назад");
            Console.Write("Нажмите клавишу 1-3: ");

            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    ShowImportSubMenu();
                    break;
                case ConsoleKey.D2:
                    ShowExportSubMenu();
                    break;
                case ConsoleKey.D3:
                    return;
                default:
                    Console.WriteLine("\nОшибка: неверная клавиша!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void ShowImportSubMenu()
    {
        Console.WriteLine("Выберите формат импорта:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. YAML");
        int choice = InputHelper.ReadInt("Ваш выбор: ", 1, 3);

        string filePath = InputHelper.ReadString("Введите путь к файлу: ");
        string fullPath = GetFullPath(filePath); // Получаем полный путь

        if (!File.Exists(fullPath))
        {
            Console.WriteLine($"Файл не найден: {fullPath}");
            return;
        }

        DataImporter importer = choice switch
        {
            1 => _serviceProvider.GetRequiredService<CsvImporter>(),
            2 => _serviceProvider.GetRequiredService<JsonImporter>(),
            3 => _serviceProvider.GetRequiredService<YamlImporter>(),
            _ => throw new InvalidOperationException()
        };
        ImportBankAccountsCommand command = new ImportBankAccountsCommand(_accountFacade, importer, fullPath);
        bool success = CommandRunner.TryExecute(command);
        Console.WriteLine("Счета успешно добавлены в систему.");
        Console.ReadLine();
    }

    private void ShowExportSubMenu()
    {
        Console.WriteLine("Выберите тип данных для экспорта:");
        Console.WriteLine("1. Счета");
        Console.WriteLine("2. Категории");
        Console.WriteLine("3. Операции");
        Console.WriteLine("4. Все данные");
        int dataChoice = InputHelper.ReadInt("Ваш выбор: ", 1, 4);

        Console.WriteLine("Выберите формат экспорта:");
        Console.WriteLine("1. CSV");
        Console.WriteLine("2. JSON");
        Console.WriteLine("3. YAML");
        int formatChoice = InputHelper.ReadInt("Ваш выбор: ", 1, 3);

        string filePath = InputHelper.ReadString("Введите путь для сохранения: ");
        string fullPath = GetFullPath(filePath); // Получаем полный путь

        // Проверяем и создаем директорию, если её нет
        string directory = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        IExportVisitor exporter = formatChoice switch
        {
            1 => new CsvExporter(fullPath),
            2 => new JsonExporter(fullPath),
            3 => new YamlExporter(fullPath),
            _ => throw new InvalidOperationException()
        };

        // Выбор типа данных для экспорта
        switch (dataChoice)
        {
            case 1:
                ExportBankAccounts(exporter);
                break;
            case 2:
                ExportCategories(exporter);
                break;
            case 3:
                ExportOperations(exporter);
                break;
            case 4:
                ExportAllData(exporter);
                break;
        }

        Console.WriteLine($"Экспорт завершен! Файл сохранен: {fullPath}");
    }

    private void ExportBankAccounts(IExportVisitor exporter)
    {
        var accounts = _accountFacade.GetAllAccounts();
        foreach (var account in accounts)
        {
            exporter.Visit(account);
        }
        exporter.CompleteExport();
        Console.WriteLine($"Экспортировано счетов: {accounts.Count}");
    }

    private void ExportCategories(IExportVisitor exporter)
    {
        var categories = _categoryFacade.GetAllCategories();
        foreach (var category in categories)
        {
            exporter.Visit(category);
        }
        exporter.CompleteExport();
        Console.WriteLine($"Экспортировано категорий: {categories.Count}");
    }

    private void ExportOperations(IExportVisitor exporter)
    {
        var operations = _operationFacade.GetAllOperations();
        foreach (var operation in operations)
        {
            exporter.Visit(operation);
        }
        exporter.CompleteExport();
        Console.WriteLine($"Экспортировано операций: {operations.Count}");
    }

    private void ExportAllData(IExportVisitor exporter)
    {
        ExportBankAccounts(exporter);
        ExportCategories(exporter);
        ExportOperations(exporter);
        Console.WriteLine("Экспортированы все данные.");
    }

    private string GetFullPath(string userPath)
    {
        // Если путь относительный, добавляем текущую директорию
        if (!Path.IsPathRooted(userPath))
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "Data", userPath);
        }

        return userPath; // Если путь абсолютный, возвращаем как есть
    }
}