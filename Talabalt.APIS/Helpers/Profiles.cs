using AutoMapper;
using Talabalt.APIS.DTOS;
using Talabat.Core.Entities;
namespace Talabalt.APIS.Helpers
    
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Products, ProductDTO>().ForMember(pd => pd.Brand, o => o.MapFrom(p => p.Brand.Name))
                .ForMember(pd => pd.Category, o => o.MapFrom(p => p.Category.Name)).ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictureUrlResolve>());
        }
    }
}
