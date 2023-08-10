using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data;
using pk.gov.pitb.cmo.directive.domain.Models.ViewModels.Directives;

namespace pk.gov.pitb.cmo.directive.domain.Data.Abstractions
{
    public class GenericRepository<T> : IRepositoryAsync<T>
       where T : BaseTableColumns

    {
        private readonly RepositoryContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(RepositoryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.Where(a => !a.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            
            var result = await _dbSet.Where(predicate).ToListAsync();

            return result.Where(a => !a.IsDeleted).ToList();
            
        }


        public IQueryable<T> GetAllQueryable() =>  _dbSet.Where(a=>!a.IsDeleted).AsQueryable();

        public async Task<int> GetCount(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(a=>!a.IsDeleted).CountAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetSkipTake(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            return await _dbSet.Where(predicate).Skip(skip).Take(take).Where(a => !a.IsDeleted).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetSkipTakeWithInclude<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, string? columns = "")
        {
            var entityType = typeof(T);

            var tEntity = typeof(TEntity);

            var result = default(IQueryable<TEntity>);

            var includeNavigators = entityType
                .GetProperties()
                .Where(prop => prop.PropertyType.IsClass && !prop.PropertyType.IsArray && prop.PropertyType != typeof(string) && prop.PropertyType != typeof(int) && prop.PropertyType != typeof(bool))
                .Select(prop => prop.Name)
                .ToArray();

            var query = _dbSet.Where(a => !a.IsDeleted);

            // Apply the predicate
            query = query.Where(predicate);

            // Apply skip and take
            query = query.Skip(skip).Take(take);

            // Include navigators
            foreach (var navigator in includeNavigators)
            {
                query = query.Include(navigator);
            }

            if (!string.IsNullOrEmpty(columns))
            {
                // Split the column names
                var selectedColumns = columns.Split(",");

                // Create a parameter expression for the entity type
                var parameter = Expression.Parameter(typeof(T), "a");

                // Create property access expressions for each selected column
                var propertyExpressions = selectedColumns.Select(column =>
                {
                    var property = typeof(T).GetProperty(column.Trim());
                    return Expression.Property(parameter, property);
                });

                // Create the anonymous type with selected properties
                
                var anonymousType = Expression.New(
                    typeof(TEntity).GetConstructor(Type.EmptyTypes)
                );

                // Create an anonymous type initializer with property assignments
                var initializer = Expression.MemberInit(
                    anonymousType,
                    propertyExpressions.Select((expression, index) =>
                        Expression.Bind(
                            tEntity.GetMember(propertyExpressions.ElementAt(index).Member.Name).FirstOrDefault(),
                            expression
                        )
                    )
                );

                

                // Create a lambda expression for the column selection
                var selectorExpression = Expression.Lambda(initializer, parameter);


                //Cast to required type 
                var castedExpression = (Expression<Func<T, TEntity>>)selectorExpression;

                
                // Update the query with the select method call expression
                result = query.Select(castedExpression);


            }
          




            return result;
        }

        public async Task<IEnumerable<T>> GetSkipTakeWithSort(Expression<Func<T, bool>> predicate, int skip, int take, OrderByEnum orderByEnum)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (orderByEnum == OrderByEnum.Asc)
            {
                query = query.OrderBy(m => m.CreatedAt);
            }
            else
            {
                query = query.OrderByDescending(m => m.CreatedAt);
            }

            return await query.Skip(skip).Take(take).ToListAsync();
            
        }

        public async Task<T> GetOne(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(a => !a.IsDeleted).FirstOrDefaultAsync(predicate);
        }

        public async Task<long> Count(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(a => !a.IsDeleted).CountAsync(predicate);
        }

        public async Task<T> Insert(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            try
            {
                _dbSet.Add(entity);
                await _context.SaveChangesAsync();
            }

            catch(Exception ex) {
                throw new Exception(ex.Message);
            }
           

            return entity;
        }

        public async Task<T> Update(int id, T updatedEntity, bool updateCreateTime = false, bool updateUpdatedTime = true)
        {
            var existedEntity =  _dbSet.Where(a => !a.IsDeleted).FirstOrDefault(a=>a.Id==id);
            if (existedEntity != null)
            {
                updatedEntity.CreatedAt = updateCreateTime ? updatedEntity.CreatedAt : existedEntity.CreatedAt;
                if (updateUpdatedTime)
                    updatedEntity.CreatedAt = DateTime.UtcNow;
                _context.Entry(existedEntity).CurrentValues.SetValues(updatedEntity);
                

                await _context.SaveChangesAsync();

                return updatedEntity;
            }
            else
            {
                throw new ArgumentException($"Entity with id {id} not found");
            }
        }
        public async Task<T> AddUpdate(T updatedEntity,int id=0, bool updateCreateTime = false, bool updateUpdatedTime = true)
        {
            var existedEntity = _dbSet.Where(a => !a.IsDeleted).FirstOrDefault(a => a.Id == id);
            if (existedEntity != null)
            {
                updatedEntity.UpdatedAt = updateCreateTime ? updatedEntity.UpdatedAt : existedEntity.UpdatedAt;
                if (updateUpdatedTime)
                    updatedEntity.UpdatedAt = DateTime.Now;
                _context.Entry(existedEntity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();



                return updatedEntity;
            }
            else
            {
                    // Add new entity
                    updatedEntity.CreatedAt = updateCreateTime ? updatedEntity.CreatedAt : DateTime.Now;
                    if (updateUpdatedTime)
                        updatedEntity.CreatedAt = DateTime.Now;

                    _dbSet.Add(updatedEntity);
                    await _context.SaveChangesAsync();

                    return updatedEntity;
            }
        }
        public async Task<long> Delete(int id)
        {
            var entity = _dbSet.Where(a => !a.IsDeleted).FirstOrDefault(a => a.Id == id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<long> Delete(T entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException("Entity to delete is null");
            }
        }



    }

}
