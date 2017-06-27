using AutoMapper;
using LibDatabase.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VG_web.App_Start
{
    public static class AutomapperConfiguration
    {
        public static void AddMapping()
        {
            Mapper.Initialize(
                cfg => { cfg.CreateMap<Category, ViewModels.CategoryViewModel>()
                    .ForSourceMember("Subcategories", opt => opt.Ignore())
                    .ReverseMap();

                    cfg.CreateMap<Image, ViewModels.ImageViewModel>()
                    .ForSourceMember("UI", opt => opt.Ignore())
                    .ReverseMap();

                    cfg.CreateMap<Maker, ViewModels.MakerViewModel>()
                    .ForSourceMember("Products", opt => opt.Ignore())
                    .ReverseMap();

                    cfg.CreateMap<Product, ViewModels.ProductViewModel>()
                    .ForSourceMember("ProductDatas", opt => opt.Ignore())
                    .ReverseMap();

                    cfg.CreateMap<ProductData, ViewModels.ProductDataViewModel>()
                    .ReverseMap();

                    cfg.CreateMap<Property, ViewModels.PropertyViewModel>()
                    .ForSourceMember("ProductDatas", opt => opt.Ignore())
                    .ForSourceMember("SubcategoryProperties", opt => opt.Ignore())
                    .ReverseMap();

                    cfg.CreateMap<Subcategory, ViewModels.SubcategoryViewModel>()
                    .ForSourceMember("SubcategoryProperties", opt => opt.Ignore())
                    .ForSourceMember("Products", opt => opt.Ignore())
                    .ForMember("CategoryName", opt => opt.MapFrom(src => src.Category.Name))
                    .ReverseMap();

                    cfg.CreateMap<SubcategoryProperty, ViewModels.SubcategoryPropertyViewModel>()
                    .ReverseMap();
                });                
        }
    }
}