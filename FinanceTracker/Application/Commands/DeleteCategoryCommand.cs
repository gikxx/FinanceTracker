namespace FinanceTracker.Application.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly int _categoryId;

        public DeleteCategoryCommand(CategoryFacade categoryFacade, int categoryId)
        {
            _categoryFacade = categoryFacade;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            _categoryFacade.DeleteCategory(_categoryId);
        }
    }
}