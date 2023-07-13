using AutoMapper;
using e_catalog_backend.Dtos.Image;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<ProductImage, ReadProductImageDto>();
        CreateMap<CreateProductImageDto, ProductImage>();
    }
}