using CFObjectMapper.Interfaces;

namespace CFObjectMapper
{
    public class ObjectMappingConfigs : IObjectMappingConfigs
    {
        private readonly List<ObjectMappingConfig> _objectMappingConfigs = new List<ObjectMappingConfig>();
        private readonly Mutex _mutex = new Mutex();

        public object? Get(Type sourceType, Type destinationType)
        {
            try
            {
                _mutex.WaitOne();

                return _objectMappingConfigs.FirstOrDefault(om => om.SourceType == sourceType && om.DestinationType == destinationType);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void Add<TSource, TDestination>(Func<TSource, IReadOnlyDictionary<string, object>, IObjectMapper, TDestination> mapFunction)
        {
            try
            {
                _mutex.WaitOne();

                // Remove any existing mapping
                var objectMappingConfig = (ObjectMappingConfig?)Get(typeof(TSource), typeof(TDestination));
                if (objectMappingConfig != null)
                {
                    _objectMappingConfigs.Remove((ObjectMappingConfig)objectMappingConfig);
                }

                // Add
                objectMappingConfig = new ObjectMappingConfig()
                {
                    SourceType = typeof(TSource),
                    DestinationType = typeof(TDestination),
                    MapFunction = mapFunction
                };
                _objectMappingConfigs.Add(objectMappingConfig);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void Remove<TSource, TDestination>()
        {
            try
            {
                _mutex.WaitOne();

                var objectMappingConfig = (ObjectMappingConfig?)Get(typeof(TSource), typeof(TDestination));
                if (objectMappingConfig == null)
                {
                    throw new ArgumentException("No mapping defined");
                }
                else
                {
                    _objectMappingConfigs.Remove(objectMappingConfig);
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }
    }
}
