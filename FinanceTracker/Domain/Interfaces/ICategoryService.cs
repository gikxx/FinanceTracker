using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Models;

namespace FinanceTracker.Domain.Interfaces
{
    public interface ICategoryService
    {
        Category CreateCategory(CategoryType type, string name);
        Category CreateCategoryWithId(int id, CategoryType type, string name);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        List<Category> GetAllCategories();
    }
}