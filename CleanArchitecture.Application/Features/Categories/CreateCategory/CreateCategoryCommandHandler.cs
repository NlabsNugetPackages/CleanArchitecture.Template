using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using MediatR;
using Nlabs.GenericRepository;
using Nlabs.Result;

namespace CleanArchitecture.Application.Features.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler
    (
        ICategoryRepository categoryRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryIsExists = await categoryRepository.AnyAsync(p => p.Name == request.Name, cancellationToken);
        if (categoryIsExists)
        {
            return Result<string>.Failure("Category already exists!");
        }

        var category = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(category, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("The action successsfull");
    }
}
