using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FilterPage;

namespace FilterPage
{
    public static class FilterPageOption
    {
        public static IQueryable<T> FilterablePage<T>(this IQueryable<T> self
            , params Filters[] filters)
        {
            var parameter = Expression.Parameter(typeof(T), "a");

            Expression expressions = null;

            foreach (var filter in filters)
            {
                var leftHand = Expression.Property(parameter, filter.Filter.Field);

                var rightHand = Expression.Constant(filter.Filter.Value);

                var expressionOperator = ResolveOperator(leftHand, rightHand, filter.Filter.Operator);

                if(expressions == null)
                {
                    expressions = expressionOperator;
                }else
                {
                    expressions
                        = ResolveLogic(expressionOperator, expressions, filter.Logic);
                }
            }

            Expression<Func<T, bool>> lambda
                = Expression.Lambda<Func<T, bool>>(expressions, parameter);

            Func<T, bool> delegateLambda = lambda.Compile();

            return self.Where(delegateLambda).AsQueryable();

        }

        private static Expression ResolveLogic(Expression left, Expression right, FilterLogic logic)
        {
            switch(logic)
            {
                case FilterLogic.And:
                    return Expression.And(left, right);

                case FilterLogic.Or:
                    return Expression.Or(left, right);

                default:
                    return Expression.AndAlso(left, right);

            }
        }

        private static Expression ResolveOperator(Expression left,  Expression right, FilterOperator @operator)
        {
            MethodInfo StringExpression(string op)
                => typeof(string).GetMethod(op, new Type[] { typeof(string) });

            switch (@operator)
            {
                default:
                case FilterOperator.Eq:
                    return Expression.Equal(left, right);

                case FilterOperator.Neq:
                    return Expression.NotEqual(left, right);

                case FilterOperator.StartsWith:
                    var st = StringExpression("StartsWith");
                    return Expression.Call(left, st, right);

                case FilterOperator.Constains:
                    var ct = StringExpression("Constains");
                    return Expression.Call(left, ct, right);
            }
        }
    }
}
