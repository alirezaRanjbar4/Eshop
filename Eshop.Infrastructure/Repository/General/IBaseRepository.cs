using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Eshop.Infrastructure.Repository.General;

public interface IBaseRepository<TModel> : IDisposable where TModel : class
{
    #region Query

    Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    Task<bool> IsExistAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<OperationResult<List<TModel>>> GetAllAsyncWithTotal(BaseSearchDTO requestDto, Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    #endregion

    #region Command

    Task<bool> AddAsync(TModel model, bool IsSave = true, CancellationToken cancellationToken = default);
    Task<bool> AddRangeAsync(IEnumerable<TModel> models, bool IsSave = true, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TModel model, bool IsSave = true, CancellationToken cancellationToken = default);
    Task<bool> UpdateRangeAsync(IEnumerable<TModel> models, bool IsSave = true, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(TModel model, bool logicalDelete = true, bool IsSave = true, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync(IEnumerable<TModel> models, bool logicalDelete = true, bool IsSave = true, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    #endregion

}
