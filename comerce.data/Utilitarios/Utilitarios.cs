using comerce.domain.ModelView;
using Microsoft.Office.Interop.Excel;
using System.Linq.Expressions;
using System.Reflection;


namespace comerce.data.Utilitarios
{
    public static class Utilitarios
    {
        public static IQueryable<TSource> Autocomplete<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate, Expression<Func<TSource, TKey>> orderKeySelector, int take = 20)
        {
            return source.Where(predicate).OrderBy(orderKeySelector).Take(take);
        }

        public static List<TSource> ToSort<TSource>(this IQueryable<TSource> source, List<EstockModelView> estock)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (estock == null || !estock.Any())
                return source.ToList();

            var orderedQuery = ApplySorts(source, estock);
            return orderedQuery.ToList();
        }

        private static IQueryable<TSource> ApplySorts<TSource>(IQueryable<TSource> source, List<EstockModelView> estock)
        {
            bool first = true;
            IOrderedQueryable<TSource> orderedQuery = null;

            foreach (var estoc in estock.Where(s => !string.IsNullOrWhiteSpace(s.Direction)))
            {
                var methodName = first ? (estoc.Direction == "desc" ? "OrderByDescending" : "OrderBy") : (estoc.Direction == "desc" ? "ThenByDescending" : "ThenBy");
                var propertyInfo = GetProperty<TSource>(estoc.PropertyName);
                var selector = GetLambdaExpressionSelector<TSource>(propertyInfo);
                orderedQuery = orderedQuery == null ? source.ApplySort(selector, methodName) : orderedQuery.ApplySort(selector, methodName);
                first = false;
            }

            return orderedQuery ?? source;
        }

        private static IOrderedQueryable<TSource> ApplySort<TSource>(this IQueryable<TSource> query, LambdaExpression selector, string methodName)
        {
            var method = typeof(Queryable).GetMethods().SingleOrDefault(
                m => m.Name == methodName &&
                     m.IsGenericMethodDefinition &&
                     m.GetGenericArguments().Length == 2 &&
                     m.GetParameters().Length == 2);

            if (method == null)
                throw new InvalidOperationException($"Método '{methodName}' não encontrado.");

            var genericMethod = method.MakeGenericMethod(typeof(TSource), selector.ReturnType);
            var result = (IOrderedQueryable<TSource>)genericMethod.Invoke(null, new object[] { query, selector });

            return result;
        }

        private static LambdaExpression GetLambdaExpressionSelector<TSource>(PropertyInfo propertyInfo)
        {
            var parameter = Expression.Parameter(typeof(TSource), "x");
            var member = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(member, parameter);
            return lambda;
        }

        private static PropertyInfo GetProperty<TSource>(string propertyName)
        {
            var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
                throw new ArgumentException($"Propriedade '{propertyName}' não encontrada na entidade '{typeof(TSource).Name}'.");

            return propertyInfo;
        }
    }
}
