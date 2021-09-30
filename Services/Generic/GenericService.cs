using AutoMapper;
using Data.IRepository;
using Data.Repository.GenericRepository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Generic
{
    public abstract class GenericService<T, E> : IGenericService<T>, IDisposable where E : class
    {
        protected readonly IGenericDataRepository<E> repository;
        protected readonly IMapper mapper;

        public GenericService(IGenericDataRepository<E> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual bool Add(params T[] items)
        {
            return repository.Add(mapper.Map<E[]>(items));
        }

        public virtual bool Remove(params T[] items)
        {
            return repository.Remove(mapper.Map<E[]>(items));
        }

        public virtual bool Remove(long id)
        {
            return repository.Remove(id);
        }

        public virtual bool Update(params T[] items)
        {
            return repository.Update(mapper.Map<E[]>(items));
        }

        public virtual IList<T> GetAll()
        {
            return mapper.Map<IList<T>>(repository.GetAll());
        }

        public virtual T GetById(long Id)
        {
            return mapper.Map<T>(repository.GetSingle(Id));
        }

        public virtual IList<T> GetListByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public bool ChangeStatus(long id)
        {
            return repository.ChangeStatus(id);
        }

        public IList<T> GetAllWitnInActive()
        {
            return mapper.Map<IList<T>>(repository.GetAllWitnInActive());
        }

        public void Dispose()
        {
            repository.Dispose();
            //IDisposable disposable = this as IDisposable;
            //if (disposable != null)
            //{
            //    disposable.Dispose();
            //}
        }

    }
}
