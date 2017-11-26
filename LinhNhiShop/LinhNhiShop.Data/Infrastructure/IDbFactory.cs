using System;

namespace LinhNhiShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        LinhNhiShopDbContext Init();
    }
}
