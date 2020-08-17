﻿using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeSql.Natasha.Extension
{
    public static class HttpContextQueryOperator<TEntity> where TEntity : class
    {
        public static readonly Action<ISelect<TEntity>, ICollection<string>, TEntity> SelectWhereHandler;
        public static readonly Action<IUpdate<TEntity>, ICollection<string>, TEntity> UpdateWhereHandler;
        public static readonly Action<IDelete<TEntity>, ICollection<string>, TEntity> DeleteWhereHandler;
        static HttpContextQueryOperator()
        {

            var stringBuilder = new StringBuilder();
            var props = typeof(TEntity).GetProperties();
            stringBuilder.AppendLine("foreach(var field in arg2){");
            foreach (var item in props)
            {

                stringBuilder.AppendLine($"if(field == \"{item}\"){{  arg1.Where(obj=>obj.{item.Name}==arg3.{item.Name});  }}");
                stringBuilder.Append("else ");

            }
            stringBuilder.Length -= 5;
            stringBuilder.Append("}");
            var result = stringBuilder.ToString();
            DeleteWhereHandler += NDelegate
   .DefaultDomain()
   .Action<IDelete<TEntity>, ICollection<string>, TEntity>(result);

            UpdateWhereHandler += NDelegate
    .DefaultDomain()
    .Action<IUpdate<TEntity>, ICollection<string>, TEntity>(result);

            SelectWhereHandler += NDelegate
    .DefaultDomain()
    .Action<ISelect<TEntity>, ICollection<string>, TEntity>(result);


        }
    }
}
