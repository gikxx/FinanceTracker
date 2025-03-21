using FinanceTracker.Application.Commands;
using FinanceTracker.Domain.Enums;
using FinanceTracker.Presentation.ConsoleUI;
using FinanceTracker.Presentation.Helpers;

public class CategoryMenu
{
    private readonly CategoryFacade _facade;

    public CategoryMenu(CategoryFacade facade)
    {
        _facade = facade;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управление категориями ===");
            Console.WriteLine("1. Создать категорию");
            Console.WriteLine("2. Просмотреть все категории");
            Console.WriteLine("3. Удалить категорию");
            Console.WriteLine("4. Назад");
            Console.Write("Нажмите клавишу 1-4: ");

            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.D1:
                    CreateCategory();
                    break;
                case ConsoleKey.D2:
                    ShowAllCategories();
                    break;
                case ConsoleKey.D3:
                    DeleteCategory();
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

    private void CreateCategory()
    {
        var type = InputHelper.ReadEnum<CategoryType>("Тип категории:");
        string name = InputHelper.ReadString("Название категории: ");

        var command = new CreateCategoryCommand(_facade, type, name);
        bool success = CommandRunner.TryExecute(command);

        if (!success)
        {
            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    private void ShowAllCategories()
    {
        var command = new ListCategoriesCommand(_facade);
        bool success = CommandRunner.TryExecute(command);
        
        Console.WriteLine("\nНажмите Enter для продолжения...");
        Console.ReadLine();
        
    }


    private void DeleteCategory()
    {
        int categoryId = InputHelper.ReadInt("Введите ID категории для удаления: ");
        var command = new DeleteCategoryCommand(_facade, categoryId);
        bool success = CommandRunner.TryExecute(command);

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
    }
}
