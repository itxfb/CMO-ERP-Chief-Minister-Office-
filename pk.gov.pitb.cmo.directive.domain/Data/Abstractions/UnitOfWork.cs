using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces;
using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Data.Abstractions
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly RepositoryContext _context;
       

        public UnitOfWork(RepositoryContext context)
        {
            _context = context;

        }

        public IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>()
            where TEntity : BaseTableColumns
        {
                return new GenericRepository<TEntity>(_context);
        }
      




    }
}
