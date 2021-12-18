using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TimesheetAPI.api.Repositories.Interfaces
{
    public interface ICRUDRepository<ID, E> : IOnlyReadableRepository<ID, E>
    {
        E Add(E entity);

        void SaveChanges();

        E Create(E entity);

        IEnumerable<E> CreateRange(IEnumerable<E> entities);

        void Update(E entity);

        void UpdateRange(IEnumerable<E> entities);

        void Delete(E entity);

        void Delete(ID id);

        void Delete(Expression<Func<E, bool>> predicate);

        IEnumerable<E> AddRange(IEnumerable<E> entities);

        IEnumerable<E> GetAll();
    }
}