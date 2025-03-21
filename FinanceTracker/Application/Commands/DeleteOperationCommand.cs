using FinanceTracker.Application.Commands;

public class DeleteOperationCommand : ICommand
{
    private readonly OperationFacade _operationFacade;
    private readonly int _operationId;

    public DeleteOperationCommand(OperationFacade operationFacade, int operationId)
    {
        _operationFacade = operationFacade;
        _operationId = operationId;
    }

    public void Execute()
    {
        _operationFacade.DeleteOperation(_operationId);
        Console.WriteLine("Операция успешно удалена.");
    }
}
