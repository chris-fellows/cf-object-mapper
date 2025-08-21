using CFObjectMapper.Interfaces;
using System.Reflection;

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
                    MappingClassType = null,
                    MapFunction = mapFunction
                };
                _objectMappingConfigs.Add(objectMappingConfig);
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void Add(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => type.IsClass && type.IsPublic);

            foreach (var mappingClassType in types)
            {
                var methods = mappingClassType.GetMethods();
                foreach (var method in methods)
                {
                    var attribute = method.GetCustomAttribute(typeof(ObjectMappingAttribute));
                    if (attribute != null)
                    {
                        var returnType = method.ReturnType;
                        var parameters = method.GetParameters();

                        Add(parameters[0].ParameterType, returnType, mappingClassType, method);
                    }                    
                }
            }
        }

        private void Add(Type sourceType, Type destinationType, Type mappingClassType, MethodInfo method)
        {
            try
            {
                _mutex.WaitOne();

                // Remove any existing mapping
                var objectMappingConfig = (ObjectMappingConfig?)Get(sourceType, destinationType);
                if (objectMappingConfig != null)
                {
                    _objectMappingConfigs.Remove((ObjectMappingConfig)objectMappingConfig);
                }

                // Add
                objectMappingConfig = new ObjectMappingConfig()
                {
                    SourceType = sourceType,
                    DestinationType = destinationType,
                    MappingClassType = mappingClassType,
                    MapFunction = method
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
