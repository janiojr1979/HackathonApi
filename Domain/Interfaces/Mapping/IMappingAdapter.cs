namespace Domain.Interfaces.Mapping
{
    public interface IMappingAdapter
    {
        void MergeMap<TDestination, TResource>(out TDestination destinationOut, params TResource[] sources) where TDestination : class, new();
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        object Map(object source, Type sourceType, Type destinationType);
        object Map(object source, object destination, Type sourceType, Type destinationType);
    }
}
