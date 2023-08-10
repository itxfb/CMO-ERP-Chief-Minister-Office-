using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces
{

    public interface IRepositoryAsync<T>
       where T : BaseTableColumns
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetSkipTake(Expression<Func<T, bool>> predicate, int skip, int take);
        Task<IEnumerable<TEntity>> GetSkipTakeWithInclude<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, string? columns);
        Task<int> GetCount(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAllQueryable();

        Task<T> GetOne(Expression<Func<T, bool>> predicate);

        Task<T> Insert(T entity);

        Task<long> Delete(T entity);

        Task<long> Delete(int id);

        Task<T> Update(int id, T entity, bool updateCreateTime = false, bool updateUpdatedTime = true);
        Task<T> AddUpdate(T entity,int id=0, bool updateCreateTime = false, bool updateUpdatedTime = true);

        Task<long> Count(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetSkipTakeWithSort(Expression<Func<T, bool>> predicate, int skip, int take, OrderByEnum orderByEnum);
    }
    public enum OrderByEnum
    {
        Asc = 0,
        Desc = 1,
    }

}
