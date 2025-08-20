using CFObjectMapper.Console.Interfaces;
using CFObjectMapper.Console.Models;
using CFObjectMapper.Interfaces;

namespace CFObjectMapper.Console.MappingConfigs
{
    /// <summary>
    /// Loads object mapping configs
    /// </summary>
    public class ObjectMappingConfigsLoader
    {
        public void Add(IObjectMappingConfigs objectMappingConfigs)
        {
            objectMappingConfigs.Add<DataModel, ViewModel>((source, parameters, objectMapper) =>
            {
                var destination = new ViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Children = source.Children == null ? null :
                            source.Children.Select(child => objectMapper.Map<DataModelChild, ViewModelChild>(child, parameters)).ToList()
                };
                return destination;
            });

            objectMappingConfigs.Add<DataModelChild, ViewModelChild>((source, parameters, mapper) =>
            {
                var userService = (IUserService)parameters["UserService"];

                return new ViewModelChild()
                {
                    Id = source.Id,
                    CreatedByUserName = userService.GetUserModel(source.CreatedByUserId)!.Name,
                    CreatedOn = source.CreatedOn
                };
            });
        }
    }
}
