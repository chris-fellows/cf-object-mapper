namespace CFObjectMapper
{
    internal class ObjectMappingConfig
    {
        public Type SourceType { get; set; } = default!;

        public Type DestinationType { get; set; } = default!;

        public Type? MappingClassType { get; set; } = default!;

        public object MapFunction { get; set; } = default!;     
    }
}
