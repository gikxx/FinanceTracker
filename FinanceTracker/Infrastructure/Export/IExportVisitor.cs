using FinanceTracker.Domain.Models;

namespace FinanceTracker.Infrastructure.Export
{
    public interface IExportVisitor
    {
        void Visit(BankAccount account);
        void Visit(Category category);
        void Visit(Operation operation);
        void CompleteExport(); // Метод для завершения экспорта
    }
}