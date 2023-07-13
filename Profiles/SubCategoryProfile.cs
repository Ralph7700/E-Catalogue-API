using AutoMapper;
using e_catalog_backend.Dtos.SubCategory;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class SubCategoryProfile : Profile
{
    public SubCategoryProfile()
    {
        CreateMap<SubCategory, ReadSubCategoryDto>();
        CreateMap<CreateSubCategoryDto, SubCategory>();
        CreateMap<UpdateSubCategoryDto, SubCategory>();
    }
}