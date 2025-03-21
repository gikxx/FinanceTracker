using FinanceTracker.Domain.Enums;

namespace FinanceTracker.Application.Commands
{
    public class CreateOperationCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly OperationType _type;
        private readonly int _accountId;
        private readonly decimal _amount;
        private readonly DateTime _date;
        private readonly int _categoryId;

        public CreateOperationCommand(
            OperationFacade operationFacade,
            OperationType type,
            int accountId,
            decimal amount,
            DateTime date,
            int categoryId)
        {
            _operationFacade = operationFacade;
            _type = type;
            _accountId = accountId;
            _amount = amount;
            _date = date;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            var operation = _operationFacade.CreateOperation(
                _type,
                _accountId,
                _amount,
                _date,
                _categoryId
            );

            Console.WriteLine($"Создана операция [{operation.Id}]: {_type} {_amount:C}");
        }
    }
}