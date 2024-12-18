using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.General;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Eshop.Service.General;

public interface IBaseService<TModel> : IScopedDependency, IDisposable
{
    #region Query
    Task<T> GetAsync<T>(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    Task<bool> IsExistAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    Task<List<T>> GetAllAsync<T>(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    Task<OperationResult<List<T>>> GetAllAsyncWithTotal<T>(BaseSearchDTO requestDto = default, Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
    #endregion

    #region Command
    Task<T> AddAsync<T>(T model, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDto;
    Task<bool> AddRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync<T>(T model, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default) where T : BaseDto;
    Task<bool> UpdateRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, bool isSave = true, bool ignoreFilter = true, bool LogicalDelete = true, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync<T>(IEnumerable<T> models, bool logicalDelete = true, bool isSave = true, CancellationToken cancellationToken = default);
    #endregion
}
