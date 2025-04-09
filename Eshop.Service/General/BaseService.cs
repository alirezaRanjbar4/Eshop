using AutoMapper;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.Entity.General;
using Eshop.Repository.General;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Eshop.Service.General;

public class BaseService<TModel> : IBaseService<TModel> where TModel : class, IBaseEntity
{
    protected readonly IBaseRepository<TModel> _baseRepository;
    protected readonly IMapper _mapper;

    public BaseService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public BaseService(IBaseRepository<TModel> baseRepository, IMapper mapper) : this(mapper)
    {
        _baseRepository = baseRepository;
    }


    #region Query

    public async Task<T> GetAsync<T>(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _baseRepository.GetAsync(expression, include, ignoreFilter, cancellationToken);
            var resultMap = _mapper.Map<T>(result);
            return resultMap;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> IsExistAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await _baseRepository.IsExistAsync(expression, include, ignoreFilter, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        return await _baseRepository.CountAsync(expression, include, ignoreFilter, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync<T>(Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        var result = await _baseRepository.GetAllAsync(expression, include, orderBy, ignoreFilter, cancellationToken);
        var resultMap = _mapper.Map<List<T>>(result);
        return resultMap;
    }

    public async Task<OperationResult<List<T>>> GetAllAsyncWithTotal<T>(BaseSearchDTO requestDto = default, Expression<Func<TModel, bool>> expression = null, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
    {
        try
        {
            var resultGetAll = await _baseRepository.GetAllAsyncWithTotal(requestDto, expression, include, orderBy, ignoreFilter, cancellationToken);
            var resultMap = _mapper.Map<List<T>>(resultGetAll.Data);

            return new OperationResult<List<T>>()
            {
                Data = resultMap,
                TotalRecords = resultGetAll.TotalRecords,
                Page = requestDto.Page,
                PageSize = requestDto.PageSize,
            };
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Command

    public async virtual Task<T> AddAsync<T>(T model, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDTO
    {
        var resultMap = _mapper.Map<TModel>(model);
        var resultAdd = await _baseRepository.AddAsync(resultMap, isSave, cancellationToken);

        if (!resultAdd)
            return null;

        model.Id = resultMap.Id;

        return model;
    }

    public async Task<bool> AddRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default)
    {
        var resultMap = _mapper.Map<IList<TModel>>(models);
        return await _baseRepository.AddRangeAsync(resultMap, isSave, cancellationToken);
    }

    public async virtual Task<T> UpdateAsync<T>(T model, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default) where T : BaseDTO
    {
        var entityResult = await _baseRepository.GetAsync(x => x.Id == model.Id, null, ignoreFilter, cancellationToken);
        _mapper.Map(model, entityResult);

        var resultEdit = await _baseRepository.UpdateAsync(entityResult, isSave, cancellationToken);
        return model;
    }

    public virtual async Task<bool> UpdateRangeAsync<T>(IEnumerable<T> models, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDTO
    {
        var entityResult = await _baseRepository.GetAllAsync(x => models.Select(m => m.Id).Contains(x.Id), null, null, false, cancellationToken);
        foreach (var item in entityResult)
        {
            var model = models.FirstOrDefault(x => x.Id == item.Id);
            if (model == null)
                continue;
            _mapper.Map(model, item);
        }
        return await _baseRepository.UpdateRangeAsync(entityResult, isSave, cancellationToken);
    }

    public async virtual Task<bool> DeleteAsync(Guid id, bool isSave = true, bool ignoreFilter = true, bool LogicalDelete = true, CancellationToken cancellationToken = default)
    {
        var findResult = await _baseRepository.GetAsync(x => x.Id == id, null, ignoreFilter, cancellationToken);
        if (findResult == null)
            return false;

        return await _baseRepository.DeleteAsync(findResult, LogicalDelete, isSave, cancellationToken);
    }

    public async Task<bool> DeleteRangeAsync<T>(IEnumerable<T> models, bool isSave = true, bool logicalDelete = true, CancellationToken cancellationToken = default) where T : BaseDTO
    {
        var entityResult = await _baseRepository.GetAllAsync(x => models.Select(m => m.Id).Contains(x.Id), null, null, false, cancellationToken);
        return await _baseRepository.DeleteRangeAsync(entityResult, logicalDelete, isSave, cancellationToken);
    }

    #endregion

    #region Dispose

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                if (_baseRepository != null)
                    _baseRepository.Dispose();
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

