using AgeRanger.Data.Model;
using System.Collections.Generic;

namespace AgeRanger.Business.IBusinessService
{
    public interface IPersonService
    {
        List<PersonDTO> GetPersonInformation();
        ResponseDTO AddUpdatePersonInformation(PersonDTO personDTO);
        bool DeletePersonInformation(int Id);
    }
}
