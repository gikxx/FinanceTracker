using FinanceTracker.Domain.Interfaces;
using FinanceTracker.Domain.Models;

public class BankAccountFacade
{
    private readonly IBankAccountService _bankAccountService;

    public BankAccountFacade(IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
    }

    public BankAccount CreateAccount(string name, decimal balance)
        => _bankAccountService.CreateAccount(name, balance);
    public BankAccount CreateAccountWithId(int id, string name, decimal balance)
        => _bankAccountService.CreateAccountWithId(id, name, balance);
    public void Deposit(int accountId, decimal amount)
        => _bankAccountService.Deposit(accountId, amount);
    public List<BankAccount> GetAllAccounts()
       => _bankAccountService.GetAllAccounts();

    public void Withdraw(int accountId, decimal amount)
        => _bankAccountService.Withdraw(accountId, amount);
    public void DeleteAccount(int accountId)
        => _bankAccountService.DeleteAccount(accountId);


}