namespace FinanceTracker.Application.Commands
{
    public class ListCategoriesCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;

        public ListCategoriesCommand(CategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        public void Execute()
        {
            Console.WriteLine("\n=== Список категорий ===");
            var categories = _categoryFacade.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"[{category.Id}] {category.Type} - {category.Name}");
            }

        }
    }
}