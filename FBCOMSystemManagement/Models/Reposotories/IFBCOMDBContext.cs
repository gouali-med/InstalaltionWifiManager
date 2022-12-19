using FBCOMSystemManagement.Models;

namespace WareHouse.Models.Reposotories
{
    public interface IFBCOMDBContext<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        List<TEntity> Search(string term);

    }
}
