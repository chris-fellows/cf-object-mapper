namespace CFObjectMapper.Interfaces
{
    /// <summary>
    /// Object mapping configs
    /// </summary>
    public interface IObjectMappingConfigs
    {   
        /// <summary>
        /// Adds mapping config
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="mappingFunction"></param>
        void Add<TSource, TDestination>(Func<TSource, IReadOnlyDictionary<string, object>?, IObjectMapper, TDestination> mappingFunction);

        /// <summary>
        /// Removes mapping config
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        void Remove<TSource, TDestination>();

        /// <summary>
        /// Gets mapping config (if exists)
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        object? Get(Type sourceType, Type destinationType);
    }
}
