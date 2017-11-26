using LinhNhiShop.Data.Infrastructure;
using LinhNhiShop.Model.Models;

namespace LinhNhiShop.Data.Repositories
{
    public interface ISlideRepository
    {

    }



    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(DbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
