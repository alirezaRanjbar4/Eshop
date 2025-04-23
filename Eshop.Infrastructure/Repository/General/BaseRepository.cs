using Eshop.Domain.General;
using Eshop.Infrastructure.DBContext;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Eshop.Infrastructure.Repository.General;

public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, IBaseEntity
{
    protected readonly ApplicationContext _dataContext;
    protected DbSet<TModel> dbSet { get; }
    public BaseRepository(ApplicationContext dataContext)
    {
        _dataContext = dataContext;
        dbSet = _dataContext.Set<TModel>();
    }

    #region Query

    public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<TModel> query = ignoreFilter ? dbSet.AsNoTracking().IgnoreQueryFilters() : dbSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return expression != null ? await query.FirstOrDefaultAsync(expression, cancellationToken) : await query.FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> IsExistAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<TModel> query = ignoreFilter == true ? dbSet.AsNoTracking().IgnoreQueryFilters() : dbSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return expression != null ? await query.AnyAsync(expression, cancellationToken) : await query.AnyAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<TModel> query = ignoreFilter == true ? dbSet.AsNoTracking().IgnoreQueryFilters() : dbSet.AsNoTracking();

            if (include != null)
                query = include(query);

            return expression != null ? await query.CountAsync(expression, cancellationToken) : await query.CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<TModel> query = ignoreFilter ? dbSet.AsNoTracking().IgnoreQueryFilters() : dbSet.AsNoTracking();

            if (expression != null)
                query = query.Where(expression);

            if (include != null)
                query = include(query);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<OperationResult<List<TModel>>> GetAllAsyncWithTotal(BaseSearchDTO requestDto, Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            if (requestDto == null)
                throw new ArgumentNullException("requestDto Can not be null");
            IQueryable<TModel> query = ignoreFilter ? dbSet.AsNoTracking().IgnoreQueryFilters() : dbSet.AsNoTracking();

            int totalCount = await CountAsync(expression, include, ignoreFilter, cancellationToken);

            if (expression != null)
                query = query.Where(expression);

            if (orderBy != null)
                query = orderBy(query);

            if (include != null)
                query = include(query);

            query = query.Skip((requestDto.Page - 1) * requestDto.PageSize).Take(requestDto.PageSize);

            return new OperationResult<List<TModel>>()
            {
                Data = await query.ToListAsync(cancellationToken),
                TotalRecords = totalCount,
                Page = requestDto.Page,
                PageSize = requestDto.PageSize
            };
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Command

    public virtual async Task<bool> AddAsync(TModel model, bool isSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            await dbSet.AddAsync(model, cancellationToken);

            if (!isSave)
                return true;

            var changes = await _dataContext.SaveChangesAsync(cancellationToken);
            _dataContext.Entry(model).State = EntityState.Detached;

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while adding the entity.", ex);
        }
    }

    public async Task<bool> AddRangeAsync(IEnumerable<TModel> models, bool IsSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            await dbSet.AddRangeAsync(models, cancellationToken);

            if (!IsSave)
                return true;

            var changes = await _dataContext.SaveChangesAsync(cancellationToken);
            foreach (var item in models)
            {
                _dataContext.Entry(item).State = EntityState.Detached;
            }

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while adding the entity.", ex);
        }
    }

    public virtual async Task<bool> UpdateAsync(TModel model, bool isSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            dbSet.Update(model);

            if (!isSave)
                return true;

            var changes = await _dataContext.SaveChangesAsync(cancellationToken);
            _dataContext.Entry(model).State = EntityState.Detached;

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while editing the entity.", ex);
        }
    }

    public virtual async Task<bool> UpdateRangeAsync(IEnumerable<TModel> models, bool IsSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            dbSet.UpdateRange(models);

            if (!IsSave)
                return true;

            var changes = await _dataContext.SaveChangesAsync(cancellationToken);
            foreach (var item in models)
            {
                _dataContext.Entry(item).State = EntityState.Detached;
            }

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while editing the entity.", ex);
        }
    }

    public async Task<bool> DeleteAsync(TModel model, bool logicalDelete = true, bool IsSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            dbSet.Remove(model);

            if (!IsSave)
                return true;

            if (logicalDelete)
                return await _dataContext.SaveChangesAsync() > 0 ? true : false;

            var changes = logicalDelete ? await _dataContext.SaveChangesAsync(cancellationToken) : await _dataContext.PhysicalDeleteSaveChanges(cancellationToken);
            _dataContext.Entry(model).State = EntityState.Detached;

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while deleting the entity.", ex);
        }
    }

    public async Task<bool> DeleteRangeAsync(IEnumerable<TModel> models, bool logicalDelete = true, bool IsSave = true, CancellationToken cancellationToken = default)
    {
        try
        {
            dbSet.RemoveRange(models);

            if (!IsSave)
                return true;

            if (logicalDelete)
                return await _dataContext.SaveChangesAsync() > 0 ? true : false;

            var changes = logicalDelete ? await _dataContext.SaveChangesAsync(cancellationToken) : await _dataContext.PhysicalDeleteSaveChanges(cancellationToken);
            foreach (var item in models)
            {
                _dataContext.Entry(item).State = EntityState.Detached;
            }

            return changes > 0;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while deleting the entity.", ex);
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dataContext.SaveChangesAsync(cancellationToken);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

}
