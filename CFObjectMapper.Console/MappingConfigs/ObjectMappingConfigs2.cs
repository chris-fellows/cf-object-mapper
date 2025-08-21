using CFObjectMapper.Console.Interfaces;
using CFObjectMapper.Console.Models;
using CFObjectMapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFObjectMapper.Console.MappingConfigs
{
    /// <summary>
    /// Object mapping configs using method (Reflection)
    /// </summary>
    public class ObjectMappingConfigs2
    {
        [ObjectMapping]
        public ViewModel Map(DataModel source, IReadOnlyDictionary<string, object> parameters, IObjectMapper objectMapper)
        {
            var destination = new ViewModel()
            {
                Id = source.Id,
                Name = source.Name,
                Children = source.Children == null ? null :
                              source.Children.Select(child => objectMapper.Map<DataModelChild, ViewModelChild>(child, parameters)).ToList()
            };
            return destination;
        }

        [ObjectMapping]
        public ViewModelChild Map(DataModelChild source, IReadOnlyDictionary<string, object> parameters, IObjectMapper objectMapper)
        {
            var userService = (IUserService)parameters["UserService"];

            return new ViewModelChild()
            {
                Id = source.Id,
                CreatedByUserName = userService.GetUserModel(source.CreatedByUserId)!.Name,
                CreatedOn = source.CreatedOn
            };
        }
    }
}
