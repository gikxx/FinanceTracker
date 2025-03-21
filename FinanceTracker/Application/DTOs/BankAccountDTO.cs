using FinanceTracker.Domain.Models;

public class BankAccountDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Balance { get; set; }

    public BankAccountDTO(BankAccount account)
    {
        Id = account.Id;
        Name = account.Name;
        Balance = account.Balance;
    }
}