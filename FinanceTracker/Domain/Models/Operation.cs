using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Domain.Models
{
    public class Operation
    {
        // Публичные свойства
        public int Id { get; set; }
        public OperationType Type { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        // Конструктор
        public Operation(int id, OperationType type, int bankAccountId, decimal amount, DateTime date, int categoryId, string description = null)
        {
            Id = id;
            Type = type;
            BankAccountId = bankAccountId;
            Amount = amount;
            Date = date;
            CategoryId = categoryId;
            Description = description;
        }

        // Пустой конструктор для сериализации/десериализации
        public Operation() { }

        // Публичный метод для изменения суммы операции
        public void UpdateAmount(decimal newAmount)
        {
            if (newAmount < 0) // <=
                throw new ArgumentException("Сумма должна быть положительной.");

            Amount = newAmount;
        }
    }
}
