using CFObjectMapper.Interfaces;

namespace CFObjectMapper
{
    public class ObjectMapper : IObjectMapper
    {            
        private readonly IObjectMappingConfigs _objectMappingConfigs;

        public ObjectMapper(IObjectMappingConfigs objectMappingConfigs)
        {
            _objectMappingConfigs = objectMappingConfigs;
        }

        public bool HasMapping<TSource, TDestination>()
        {                      
            var objectMappingConfig = _objectMappingConfigs.Get(typeof(TSource), typeof(TDestination));
            return objectMappingConfig != null;            
        }

        public TDestination Map<TSource, TDestination>(TSource source,
                                            IReadOnlyDictionary<string, object>? parameters = null)
        {
            try
            {                                
                var objectMappingConfig = (ObjectMappingConfig?)_objectMappingConfigs.Get(typeof(TSource), typeof(TDestination));
                if (objectMappingConfig == null)
                {
                    throw new ArgumentException("No mapping defined");
                }

                var mapFunction = (Func<TSource, IReadOnlyDictionary<string, object>?, IObjectMapper, TDestination>)objectMappingConfig.MapFunction;
                return mapFunction(source, parameters, this);
            }
            finally
            {
                //_mutex.ReleaseMutex();
            }
        }        
    }
}
