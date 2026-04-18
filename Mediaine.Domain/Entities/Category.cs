using Mediaine.Domain.Common;

namespace Mediaine.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}