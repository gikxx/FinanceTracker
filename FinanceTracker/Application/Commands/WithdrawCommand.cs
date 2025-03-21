namespace FinanceTracker.Application.Commands
{
    public class WithdrawCommand : ICommand
    {
        private readonly BankAccountFacade _bankAccountFacade;
        private readonly int _accountId;
        private readonly decimal _amount;
        public WithdrawCommand(
                       BankAccountFacade bankAccountFacade,
                                  int accountId,
                                             decimal amount)
        {
            _bankAccountFacade = bankAccountFacade;
            _accountId = accountId;
            _amount = amount;
        }
        public void Execute()
        {
            _bankAccountFacade.Withdraw(_accountId, _amount);
            Console.WriteLine($"Со счета {_accountId} списано {_amount:C}");
        }
    }
}