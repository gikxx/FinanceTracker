using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Domain.Models
{
    public class BankAccount
    {
        // Уникальный идентификатор счета
        public int Id { get; set; }

        // Название счета (например, "Основной счет")
        public string Name { get; set; }

        // Текущий баланс счета
        public decimal Balance { get; set; }

        // Конструктор для удобства создания объекта
        public BankAccount(int id, string name, decimal balance)
        {
            Id = id;
            Name = name;
            Balance = balance;
        }

        // Пустой конструктор для сериализации/десериализации
        public BankAccount() { }

        // Публичный метод для пополнения счета
        public void Deposit(decimal amount)
        {
            if (amount < 0) // <=
                throw new ArgumentException("Сумма должна быть положительной.");

            Balance += amount;
        }

        // Публичный метод для списания средств
        public void Withdraw(decimal amount)
        {
            if (amount < 0) // <=
                throw new ArgumentException("Сумма должна быть положительной.");

            if (Balance < amount)
                throw new InvalidOperationException("Недостаточно средств на счете.");

            Balance -= amount;
        }
    }
}
