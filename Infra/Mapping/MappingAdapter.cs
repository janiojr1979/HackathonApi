using AutoMapper;
using Domain.Interfaces.Mapping;

namespace Infra.Mapping
{
    public class MappingAdapter : IMappingAdapter
    {
        private readonly IMapper _mapper;

        public MappingAdapter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return _mapper.Map<TSource, TDestination>(source, destination);
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, sourceType, destinationType);
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return _mapper.Map(source, destination, sourceType, destinationType);
        }

        public void MergeMap<TDestination, TResource>(out TDestination destinationOut, params TResource[] sources)
            where TDestination : class, new()
        {
            TDestination destination = new TDestination();

            foreach (TResource source in sources)
            {
                _mapper.Map<TResource, TDestination>(source, destination);
            }

            destinationOut = destination;
        }
    }
}
