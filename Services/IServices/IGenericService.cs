using System.Collections.Generic;

namespace Services.IServices
{
    public interface IGenericService<T>
    {
        bool Add(params T[] items);
        IList<T> GetAll();
        IList<T> GetAllWitnInActive();
        T GetById(long Id);
        bool Remove(long id);
        bool Remove(params T[] items);
        bool Update(params T[] items);
        IList<T> GetListByUserId(string userId);
        bool ChangeStatus(long id);
    }
}