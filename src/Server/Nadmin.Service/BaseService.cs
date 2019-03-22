using Nadmin.Common;
using Nadmin.IService;
using Nadmin.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Nadmin.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity, new()
    {
        protected SimpleClient<TEntity> BaseEntity { get; }

        protected SqlSugarClient DbClient { get; } = DataBaseClient.Create();

        public BaseService()
        {
            BaseEntity = new SimpleClient<TEntity>(DbClient);
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.Count(whereExpression));
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.Delete(whereExpression));
        }

        public async Task<bool> Delete(TEntity deleteObj)
        {
            return await Task.Run(() => BaseEntity.Delete(deleteObj));
        }

        public async Task<bool> DeleteById(string id)
        {
            return await Task.Run(() => BaseEntity.DeleteById(id));
        }

        public async Task<bool> DeleteByIds(dynamic[] ids)
        {
            return await Task.Run(() => BaseEntity.DeleteByIds(ids));
        }

        public async Task<TEntity> GetById(string id)
        {
            return await Task.Run(() => BaseEntity.GetById(id));
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.GetList(whereExpression));
        }

        public async Task<List<TEntity>> GetList()
        {
            return await Task.Run(() => BaseEntity.GetList());
        }

        public async Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page, OrderByType orderByType)
        {
            return await Task.Run(() => BaseEntity.GetPageList(conditionalList, page, null, orderByType));
        }

        public async Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<TEntity, object>> orderByExpression, OrderByType orderByType)
        {
            return await Task.Run(() => BaseEntity.GetPageList(conditionalList, page, orderByExpression, orderByType));
        }

        public async Task<List<TEntity>> GetPageList(List<IConditionalModel> conditionalList, PageModel page)
        {
            return await Task.Run(() => BaseEntity.GetPageList(conditionalList, page));
        }

        public async Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page, OrderByType orderByType)
        {
            return await Task.Run(() => BaseEntity.GetPageList(whereExpression, page, null, orderByType));
        }

        public async Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page, Expression<Func<TEntity, object>> orderByExpression,
            OrderByType orderByType = OrderByType.Asc)
        {
            return await Task.Run(() => BaseEntity.GetPageList(whereExpression, page, orderByExpression, orderByType));
        }

        public async Task<List<TEntity>> GetPageList(Expression<Func<TEntity, bool>> whereExpression, PageModel page)
        {
            return await Task.Run(() => BaseEntity.GetPageList(whereExpression, page));
        }

        public async Task<TEntity> GetSingle(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.GetSingle(whereExpression));
        }

        public async Task<bool> Insert(TEntity insertObj)
        {
            return await Task.Run(() => BaseEntity.Insert(insertObj));
        }

        public async Task<bool> InsertRange(TEntity[] insertObjs)
        {
            return await Task.Run(() => BaseEntity.InsertRange(insertObjs));
        }

        public async Task<bool> InsertRange(List<TEntity> insertObjs)
        {
            return await Task.Run(() => BaseEntity.InsertRange(insertObjs));
        }

        public async Task<bool> IsAny(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.IsAny(whereExpression));
        }

        public async Task<bool> Update(TEntity updateObj)
        {
            return await Task.Run(() => BaseEntity.Update(updateObj));
        }

        public async Task<bool> Update(Expression<Func<TEntity, TEntity>> columns, Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => BaseEntity.Update(columns, whereExpression));
        }

        public async Task<bool> UpdateRange(TEntity[] updateObjs)
        {
            return await Task.Run(() => BaseEntity.UpdateRange(updateObjs));
        }

        public async Task<bool> UpdateRange(List<TEntity> updateObjs)
        {
            return await Task.Run(() => BaseEntity.UpdateRange(updateObjs));
        }
    }
}