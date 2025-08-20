using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFObjectMapper.Console.Models
{
    public class DataModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<DataModelChild> Children { get; set; } = new();
    }
}
