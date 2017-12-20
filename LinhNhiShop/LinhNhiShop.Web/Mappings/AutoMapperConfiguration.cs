using AutoMapper;
using LinhNhiShop.Model.Models;
using LinhNhiShop.Web.Models;

namespace LinhNhiShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<OrderDetail, OrderDetailViewModel>();
                cfg.CreateMap<Order, OrderViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<PostTag, PostTagViewModel>();
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();
                cfg.CreateMap<Footer, FooterViewModel>();
                cfg.CreateMap<Slide, SlideViewModel>();
                cfg.CreateMap<Page, PageViewModel>();
                cfg.CreateMap<ContactDetail, ContactDetailViewModel>();
            });
        }
    }
}