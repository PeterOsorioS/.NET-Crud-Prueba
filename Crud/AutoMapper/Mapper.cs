using AutoMapper;
using Crud.DTO;
using Crud.Models;

namespace Crud.AutoMapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(d => d.Password, o => o.MapFrom(c => c.Password))
                .ForSourceMember(c => c.ConfirmPassword, o => o.DoNotValidate());

            CreateMap<ProductDTO, Product>();

            CreateMap<ProductPurchaseDTO, ProductPurchase>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar Id
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => DateTime.UtcNow)) // Establecer PurchaseDate
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Products.Sum(p => p.Price * p.Quantity))) // Calcular Total
                .ForMember(dest => dest.Products, opt => opt.Ignore());
        }
    }
}
