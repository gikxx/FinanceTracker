using FinanceTracker.Domain.Enums;
using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace FinanceTracker.Infrastructure.Caching
{
    public class CategoryProxy : ICategoryService
    {
        private readonly ICategoryService _categoryService;
        private readonly Dictionary<int, Category> _cache = new Dictionary<int, Category>();
        private readonly string _filePath = "categories.json";

        public CategoryProxy(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            LoadDataFromFile(); // Загрузка данных из файла при инициализации
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                var categories = JsonSerializer.Deserialize<List<Category>>(json);
                foreach (var category in categories)
                {
                    _cache[category.Id] = category;
                    _categoryService.CreateCategoryWithId(category.Id, category.Type, category.Name);

                }
                
            }
        }

        private void SaveDataToFile()
        {
            var json = JsonSerializer.Serialize(_cache.Values);
            File.WriteAllText(_filePath, json);
        }

        public Category CreateCategory(CategoryType type, string name)
        {
            var category = _categoryService.CreateCategory(type, name);
            _cache[category.Id] = category;
            SaveDataToFile(); // Сохранение данных в файл
            return category;
        }
        public Category CreateCategoryWithId(int id, CategoryType type, string name)
        {
            var category = _categoryService.CreateCategoryWithId(id, type, name);
            _cache[category.Id] = category;
            SaveDataToFile(); // Сохранение данных в файл
            return category;
        }

        public Category GetCategoryById(int categoryId)
        {
            if (_cache.ContainsKey(categoryId))
                return _cache[categoryId];

            var category = _categoryService.GetCategoryById(categoryId);
            if (category != null)
            {
                _cache[categoryId] = category;
            }
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
            _cache[category.Id] = category;
            SaveDataToFile(); // Сохранение данных в файл
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryService.DeleteCategory(categoryId);
            _cache.Remove(categoryId);
            SaveDataToFile(); // Сохранение данных в файл
        }
        public List<Category> GetAllCategories()
        {
            return _cache.Values.ToList();
        }
        public void SetNextCategoryId(int nextId)
        {
            if (_categoryService is CategoryService service)
            {
                service.SetNextCategoryId(nextId);
            }
        }
    }
}