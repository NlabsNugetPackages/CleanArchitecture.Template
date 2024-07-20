using MediatR;

namespace CleanArchitecture.Application.Features.Categories.GetAll;
public sealed record GetAllCategoriesQuery : IRequest<List<CategoryResponse>>;
