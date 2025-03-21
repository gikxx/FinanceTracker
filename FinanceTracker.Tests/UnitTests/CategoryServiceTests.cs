using Xunit;
using FinanceTracker.Domain.Models;
using FinanceTracker.Domain.Services;
using FinanceTracker.Domain.Enums;
using System;

namespace FinanceTracker.Tests.UnitTests
{
    public class CategoryServiceTests
    {
        [Fact]
        public void CreateCategory_ValidData_CategoryCreated()
        {
            // Arrange
            var service = new CategoryService();
            var type = CategoryType.Income;
            string name = "Зарплата";

            // Act
            var category = service.CreateCategory(type, name);

            // Assert
            Assert.Equal(type, category.Type);
            Assert.Equal(name, category.Name);
        }

        [Fact]
        public void UpdateCategory_ValidData_CategoryUpdated()
        {
            // Arrange
            var service = new CategoryService();
            var category = service.CreateCategory(CategoryType.Expense, "Кафе");

            // Act
            category.Name = "Ресторан";
            service.UpdateCategory(category);

            // Assert
            var updatedCategory = service.GetCategoryById(category.Id);
            Assert.Equal("Ресторан", updatedCategory.Name);
        }

        [Fact]
        public void DeleteCategory_ValidId_CategoryDeleted()
        {
            // Arrange
            var service = new CategoryService();
            var category = service.CreateCategory(CategoryType.Income, "Кэшбэк");

            // Act
            service.DeleteCategory(category.Id);

            // Assert
            Assert.Throws<KeyNotFoundException>(() => service.GetCategoryById(category.Id));
        }
    }
}