using System.Linq.Expressions;
using System.Reflection;

namespace InVent.Extensions
{
    public static class PropertyMapperExtension<TSource, TTarget>
    {
        public static readonly Action<TSource, TTarget> Map = CreateMapFunction();

        private static Action<TSource, TTarget> CreateMapFunction()
        {
            var sourceProps = typeof(TSource).GetProperties();
            var targetProps = typeof(TTarget).GetProperties().ToDictionary(p => (p.Name, p.PropertyType));

            var src = Expression.Parameter(typeof(TSource), "src");
            var trg = Expression.Parameter(typeof(TTarget), "trg");

            var bindings = new List<Expression>();

            foreach (var sp in sourceProps)
            {
                if (targetProps.TryGetValue((sp.Name, sp.PropertyType), out var tp) &&
                    tp.CanWrite && sp.CanRead)
                {
                    var assign = Expression.Assign(
                        Expression.Property(trg, tp),
                        Expression.Property(src, sp)
                    );

                    bindings.Add(assign);
                }
            }

            var body = Expression.Block(bindings);
            return Expression.Lambda<Action<TSource, TTarget>>(body, src, trg).Compile();
        }
    }

    public static class Mapper
    {
        public static TResult Map<TSource, TResult>(TSource source, TResult result)
        {
            if (source == null || result == null) throw new Exception("null entry");

            var targetProps = typeof(TResult).GetProperties();
            var sourceProps = typeof(TSource).GetProperties();

            sourceProps
                .ToList()
                .ForEach(p =>
                     targetProps
                        .FirstOrDefault(x => x.Name == p.Name && x.PropertyType == p.PropertyType)?
                        .SetValue(result, p.GetValue(source))
                );
            return result;
        }
    }

}
