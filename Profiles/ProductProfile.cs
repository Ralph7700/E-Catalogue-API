using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Models;

namespace e_catalog_backend.Profiles;

public class ProductProfile : Profile
{
        public ProductProfile()
        {
            CreateMap<Product, ReadProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
}