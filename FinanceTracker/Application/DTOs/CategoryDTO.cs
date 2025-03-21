using FinanceTracker.Domain.Models;

namespace FinanceTracker.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }

        public CategoryDTO(Category category)
        {
            Id = category.Id;
            Type = category.Type.ToString();
            Name = category.Name;
        }
    }
}