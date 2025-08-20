namespace CFObjectMapper.Interfaces
{
    /// <summary>
    /// Object mapper
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Maps source to destination
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source, IReadOnlyDictionary<string, object>? parameters = null);     

        /// <summary>
        /// Indicates if mapping exists
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        bool HasMapping<TSource, TDestination>();
    }
}
