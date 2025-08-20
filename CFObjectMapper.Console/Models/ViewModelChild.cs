using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFObjectMapper.Console.Models
{
    public class ViewModelChild
    {
        public int Id { get; set; }

        public string CreatedByUserName {get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
