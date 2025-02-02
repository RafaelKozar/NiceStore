using AutoMapper;

namespace NiceStore.Catalog.Application.AutoMapper
{
    public partial class DomainToDTOMappingProfile
    {
        public class DTOToDomainMappingProfile : Profile
        {
            public DTOToDomainMappingProfile()
            {
                CreateMap<DTOs.ProductDTO, Domain.Product>()
                    .ConstructUsing(p =>
                                           new Domain.Product(p.Name, p.Description, p.Active, p.Price, p.CategoryId, p.CreatedAt, p.Image,   new Domain.Dimensions(p.Height, p.Width, p.Depth)));
                CreateMap<DTOs.CategoryDTO, Domain.Category>()
                   .ConstructUsing(c => new Domain.Category(c.Name, c.Code));
            }
        }   
    }
}
