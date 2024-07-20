using MediatR;
using Nlabs.Result;

namespace CleanArchitecture.Application.Features.Categories.CreateCategory;
public sealed record CreateCategoryCommand(string Name) : IRequest<Result<string>>;
