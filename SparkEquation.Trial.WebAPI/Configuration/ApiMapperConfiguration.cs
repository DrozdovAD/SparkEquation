using System;
using System.Linq;
using AutoMapper;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Configuration
{
    public static class ApiMapperConfiguration
    {
        public static void ConfigureMappings(IMapperConfigurationExpression config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config.CreateMap<ProductMessage, Product>()
                .ForMember(dest => dest.CategoryProducts, 
                           opt => opt.MapFrom(pm => pm.CategoryIds.Select(id =>
                                new CategoryProduct() { ProductId = pm.Id, CategoryId = id })
                           )
                );
        }
    }
}