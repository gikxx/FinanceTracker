using FinanceTracker.Domain.Models;

public class OperationDTO
{
    public int Id { get; set; }
    public string Type { get; set; }
    public int BankAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

    public OperationDTO(Operation operation)
    {
        Id = operation.Id;
        Type = operation.Type.ToString();
        BankAccountId = operation.BankAccountId;
        Amount = operation.Amount;
        Date = operation.Date;
        Description = operation.Description;
        CategoryId = operation.CategoryId;
    }
}