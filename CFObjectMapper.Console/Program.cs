// See https://aka.ms/new-console-template for more information
using CFObjectMapper;
using CFObjectMapper.Console.MappingConfigs;
using CFObjectMapper.Console.Models;
using CFObjectMapper.Console.Services;
using CFObjectMapper.Interfaces;

// Create object mapping configs (Singleton)
IObjectMappingConfigs objectMappingConfigs = new ObjectMappingConfigs();

// Load object mappings
new ObjectMappingConfigsLoader().Add(objectMappingConfigs);

// Create object mapper (Scoped, uses singleton IObjectMappingConfigs)
IObjectMapper objectMapper = new ObjectMapper(objectMappingConfigs);

// Create data model
var dataModel1 = new DataModel()
{
    Id = 10010,
    Name = "Data model",
    Children = new List<DataModelChild>()
    {
        new DataModelChild()
        {
            Id = 1,
            CreatedOn = DateTimeOffset.UtcNow,
        },
        new DataModelChild()
        {
            Id = 2,
            CreatedOn = DateTimeOffset.UtcNow,
        }
    }
};

// Map data model to view model
var userService = new UserService();
var parameters = new Dictionary<string, object>()
{
    { "UserService", userService }
};
var viewModel1 = objectMapper.Map<DataModel, ViewModel>(dataModel1, parameters);

int xxx = 1000;