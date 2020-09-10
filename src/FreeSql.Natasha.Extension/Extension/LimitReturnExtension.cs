﻿using System.Collections.Generic;

namespace FreeSql.Natasha.Extension
{
    public static class LimitReturnExtension
    {
        /// <summary>
        /// 限制结果集合
        /// 会受到 PropertiesCache<TEntity>.BlockSelectFields 的影响
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="select">查询操作句柄</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToLimitList<TEntity>(this ISelect<TEntity> select) where TEntity : class
        {
            return select.ToList<TEntity>(LimitReturnOperator<TEntity>.ReturnScript);
        }
    }
}
