using Nadmin.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nadmin.IService
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<int> Count(Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> Delete(TEntity deleteObj);
        Task<bool> DeleteById(string id);
        Task<bool> DeleteByIds(dynamic[] ids);
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> GetList();
        Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page, OrderByType orderByType);
        Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<TEntity, object>> orderByExpression, OrderByType orderByType);
        Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page);
        Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page, OrderByType orderByType);
        Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page, Expression<Func<TEntity, object>> orderByExpression, OrderByType orderByType = OrderByType.Asc);
        Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page);
        Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> Insert(TEntity insertObj);
        Task<bool> InsertRange(TEntity[] insertObjs);
        Task<bool> InsertRange(List<TEntity> insertObjs);
        Task<bool> IsAny(Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> Update(TEntity updateObj);
        Task<bool> Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> UpdateRange(TEntity[] updateObjs);
        Task<bool> UpdateRange(List<TEntity> updateObjs);
    }
}