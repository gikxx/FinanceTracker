using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Models
{
    public class Category
    {
        // Уникальный идентификатор категории
        public int Id { get; set; }

        // Тип категории: "Income" (доход) или "Expense" (расход)
        public CategoryType Type { get; set; }

        // Название категории (например, "Кафе", "Зарплата")
        public string Name { get; set; }

        // Конструктор для удобства создания объекта
        public Category(int id, CategoryType type, string name)
        {
            Id = id;
            Type = type;
            Name = name;
        }

        // Пустой конструктор для сериализации/десериализации
        public Category() { }
    }
}
