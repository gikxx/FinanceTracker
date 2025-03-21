using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using System.Collections.Generic;

namespace FinanceTracker.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly List<Category> _categories = new List<Category>();
        private int _nextCategoryId = 1;

        public Category CreateCategory(CategoryType type, string name)
        {
            var category = new Category(_nextCategoryId++, type, name);
            _categories.Add(category);
            return category;
        }

        public Category CreateCategoryWithId(int id, CategoryType type, string name)
        {
            var category = new Category(id, type, name);
            _categories.Add(category);
            if (id >= _nextCategoryId)
                _nextCategoryId = id + 1;
            return category;
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _categories.Find(c => c.Id == category.Id);
            if (existingCategory == null)
                throw new KeyNotFoundException("Категория не найдена.");

            existingCategory.Type = category.Type;
            existingCategory.Name = category.Name;
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _categories.Find(c => c.Id == categoryId);
            if (category == null)
                throw new KeyNotFoundException("Категория не найдена.");

            _categories.Remove(category);
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categories.Find(c => c.Id == categoryId)
                ?? throw new KeyNotFoundException("Категория не найдена.");
        }

        public List<Category> GetAllCategories()
        {
            return _categories.ToList(); // Возвращаем копию списка
        }
        public void SetNextCategoryId(int nextId)
        {
            _nextCategoryId = nextId;
        }
    }
}