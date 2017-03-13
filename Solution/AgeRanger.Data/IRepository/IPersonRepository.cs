using AgeRanger.Data.Model;
using System.Data;


namespace AgeRanger.Data.IRepository
{
    public interface IPersonRepository
    {
        DataSet GetPersonInformation(PersonDTO personDTO=null);
        PersonDTO AddUpdatePersonInformation(PersonDTO personDTO);
        bool DeletePersonInformation(int Id);
    }
}
