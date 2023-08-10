using pk.gov.pitb.cmo.directive.domain.Data;

namespace pk.gov.pitb.cmo.directive.domain.Contracts.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>()
            where TEntity : BaseTableColumns;

       
    }


}
