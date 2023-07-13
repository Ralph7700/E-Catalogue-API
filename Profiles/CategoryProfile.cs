using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, ReadCategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
    }
}