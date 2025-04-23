using Eshop.Application.DTO.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Helpers.Utilities.Interface;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Eshop.Application.Service.General;

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
    Task<T> AddAsync<T>(T model, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDTO;
    Task<bool> AddRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default);

    Task<T> UpdateAsync<T>(T model, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default) where T : BaseDTO;
    Task<bool> UpdateRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDTO;

    Task<bool> DeleteAsync(Guid id, bool isSave = true, bool ignoreFilter = true, bool LogicalDelete = true, CancellationToken cancellationToken = default);
    Task<bool> DeleteRangeAsync<T>(IEnumerable<T> models, bool logicalDelete = true, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDTO;
    #endregion
}
