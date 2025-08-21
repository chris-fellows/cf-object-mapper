using CFObjectMapper.Interfaces;
using System.Reflection;

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

                // Execute mapping
                if (objectMappingConfig.MappingClassType == null)   // Use map function
                {
                    var mapFunction = (Func<TSource, IReadOnlyDictionary<string, object>?, IObjectMapper, TDestination>)objectMappingConfig.MapFunction;
                    return mapFunction(source, parameters, this);
                }
                else     // Use map method (Reflection)
                {
                    var mapFunction = (MethodInfo)objectMappingConfig.MapFunction;

                    // Set method parameters. We allow the user to set the parameters in any order and Parameters & IObjectMapper
                    // are optional.
                    var methodParameters = new List<object>();
                    foreach(var parameter in mapFunction.GetParameters())
                    {
                        if (parameter.ParameterType == typeof(IObjectMapper))
                        {
                            methodParameters.Add(this);
                        }
                        else if (parameter.ParameterType == typeof(IReadOnlyDictionary<string, object>))
                        {
                            methodParameters.Add(parameters);
                        }
                        else
                        {
                            methodParameters.Add(source);
                        }
                    }

                    var mappingClassInstance = Activator.CreateInstance(objectMappingConfig.MappingClassType);
                    var result = (TDestination)mapFunction.Invoke(mappingClassInstance, methodParameters.ToArray());
                    return result;
                }               
            }
            finally
            {
                //_mutex.ReleaseMutex();
            }
        }        
    }
}
