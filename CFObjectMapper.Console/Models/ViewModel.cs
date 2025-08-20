using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFObjectMapper.Console.Models
{
    public class ViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ViewModelChild> Children { get; set; } = new();
    }
}
