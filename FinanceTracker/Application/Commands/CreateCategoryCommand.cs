using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly CategoryType _type;
        private readonly string _name;

        public CreateCategoryCommand(
            CategoryFacade categoryFacade,
            CategoryType type,
            string name)
        {
            _categoryFacade = categoryFacade;
            _type = type;
            _name = name;
        }

        public void Execute()
        {
            var category = _categoryFacade.CreateCategory(_type, _name);
            Console.WriteLine($"Создана категория: {category.Name} ({category.Type})");
        }
    }
}