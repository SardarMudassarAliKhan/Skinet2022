using AutoMapper;
using Microsoft.Extensions.Configuration;
using Skinet.Core.Entities;
using Skinet.Dtos;

namespace Skinet.Helpers
{
    public class ProductUrlResolvers : IValueResolver<Products, ProductDto, string>
    {
        private readonly IConfiguration _iconfiguration;

        public ProductUrlResolvers(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }
        public string Resolve(Products source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
               return _iconfiguration["ApiKey"] + source.PictureUrl;
            }
            else
            {
                return null;
            }
        }
    }
}
