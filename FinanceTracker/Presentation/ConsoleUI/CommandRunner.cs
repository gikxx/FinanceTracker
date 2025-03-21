using FinanceTracker.Application.Commands;

namespace FinanceTracker.Presentation.ConsoleUI
{
    public static class CommandRunner
    {
        public static bool TryExecute(ICommand command)
        {
            try
            {
                command.Execute();
                Console.WriteLine("Операция выполнена успешно!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return false;
            }
        }
    }
}