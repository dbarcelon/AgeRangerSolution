using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Model
{
    public class AgeGroupDTO
    {
        public int Id { get; set; }
        public long? MinAge { get; set; }
        public long? MaxAge { get; set; }
        public string Description { get; set; }
    }
}
