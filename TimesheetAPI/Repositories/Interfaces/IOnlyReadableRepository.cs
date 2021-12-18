using System;
using System.Linq;
using System.Linq.Expressions;

namespace TimesheetAPI.api.Repositories.Interfaces
{
    public interface IOnlyReadableRepository<ID, E> : IDisposable
    {
        IQueryable<E> Find();
        E Find(Expression<Func<E, bool>> predicate, string navigationProperties = null);
        IQueryable<E> Filter(Expression<Func<E, bool>> predicate, string navigationProperties = null);
        IQueryable<E> Filter(Expression<Func<E, bool>> predicate);
        E Find(ID id);
        int Count(Expression<Func<E, bool>> filterPredicate = null);
    }
}