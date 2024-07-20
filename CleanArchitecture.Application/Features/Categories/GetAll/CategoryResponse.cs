namespace CleanArchitecture.Application.Features.Categories.GetAll;

public sealed record CategoryResponse
{
    public Guid Id { get; set; } = default!;
    public string Name { get; set; } = default!;
}
