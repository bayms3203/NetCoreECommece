using System.Collections.Generic;

// Database işlemlerini üstlenecek olan methodlar

namespace TestMVCApp.libs.Data.Repositories
{

    public interface IRepository<TEntity>
    {
        void Create(TEntity entity);
        void Delete(string Id);

        void Update(string Id, TEntity entity);

        TEntity Find(string Id);

        List<TEntity> toList();
    }

}