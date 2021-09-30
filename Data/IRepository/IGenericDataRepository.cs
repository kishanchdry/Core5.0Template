using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepository
{
    public interface IGenericDataRepository<T> : IDisposable where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(long id, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        bool Add(params T[] items);
        //bool Add(T items);
        bool Update(params T[] items);
        bool Remove(params T[] items);
        bool Remove(long Id);
        bool ChangeStatus(long id);
        IList<T> GetAllWitnInActive(params Expression<Func<T, object>>[] navigationProperties);
    }
}
