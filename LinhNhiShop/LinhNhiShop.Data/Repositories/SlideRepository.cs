﻿using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface ISlideRepository : IRepository<Slide>
    {

    }



    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
