using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiceStore.Catalog.Application.AutoMapper
{
    public partial class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Domain.Product, DTOs.ProductDTO>()
                .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
                .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
                .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));
                
            CreateMap<Domain.Category, DTOs.CategoryDTO>();
        }
    }
}
