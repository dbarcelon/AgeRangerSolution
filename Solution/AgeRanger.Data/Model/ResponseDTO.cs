using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Data.Model
{
    public class ResponseDTO
    {
        public string ExceptionMessage { get; set; }
        public PersonDTO personDTO { get; set; }
    }
}
