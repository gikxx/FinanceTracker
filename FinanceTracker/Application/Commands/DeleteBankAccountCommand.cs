using FinanceTracker.Application.Commands;

public class DeleteBankAccountCommand : ICommand
{
    private readonly BankAccountFacade _bankAccountFacade;
    private readonly int _accountId;

    public DeleteBankAccountCommand(BankAccountFacade bankAccountFacade, int accountId)
    {
        _bankAccountFacade = bankAccountFacade;
        _accountId = accountId;
    }

    public void Execute()
    {
        _bankAccountFacade.DeleteAccount(_accountId);
        Console.WriteLine("Счет успешно удален.");
    }
}
