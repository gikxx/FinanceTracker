namespace FinanceTracker.Application.Commands
{
    public class ListOperationsCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;

        public ListOperationsCommand(OperationFacade operationFacade)
        {
            _operationFacade = operationFacade;
        }

        public void Execute()
        {
            var operations = _operationFacade.GetAllOperations();
            Console.WriteLine("\n=== Список операций ===");

            foreach (var op in operations)
            {
                Console.WriteLine($"[{op.Id}] {op.Date:dd.MM.yyyy} | {op.Type} | {op.Amount} | Категория: {op.CategoryId}");
            }
        }
    }
}