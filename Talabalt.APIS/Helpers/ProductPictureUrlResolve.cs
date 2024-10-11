using AutoMapper;
using Talabalt.APIS.DTOS;
using Talabat.Core.Entities;

namespace Talabalt.APIS.Helpers
{
    public class ProductPictureUrlResolve : IValueResolver<Products, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolve(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Products source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (source.PictureUrl!=null)
            {
               return  $"{_configuration["APIUrl"]}/{source.PictureUrl}";
            }

            return string.Empty ;
        }
    }
}
