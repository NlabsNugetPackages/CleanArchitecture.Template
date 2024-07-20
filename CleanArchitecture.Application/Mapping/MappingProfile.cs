using AutoMapper;
using CleanArchitecture.Application.Features.Auth.Register;
using CleanArchitecture.Application.Features.Categories.CreateCategory;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Mapping;
public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, RegisterCommand>().ReverseMap();
        CreateMap<CreateCategoryCommand, Category>().ReverseMap();
    }
}