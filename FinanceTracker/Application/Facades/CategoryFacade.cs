using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;

public class CategoryFacade
{
    private readonly ICategoryService _categoryService;

    public CategoryFacade(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public Category CreateCategory(CategoryType type, string name)
        => _categoryService.CreateCategory(type, name);

    public List<Category> GetAllCategories()
        => _categoryService.GetAllCategories(); // Добавленный метод
    public Category GetCategoryById(int categoryId)
       => _categoryService.GetCategoryById(categoryId);

    public void UpdateCategory(Category category)
        => _categoryService.UpdateCategory(category);
    public void DeleteCategory(int categoryId)
        => _categoryService.DeleteCategory(categoryId);
}