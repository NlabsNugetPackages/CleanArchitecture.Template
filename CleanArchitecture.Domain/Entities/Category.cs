namespace CleanArchitecture.Domain.Entities;
public sealed class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = default!;
}
