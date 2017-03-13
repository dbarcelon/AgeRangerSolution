using AgeRanger.Business.IBusinessService;
using AgeRanger.Data.IRepository;
using AgeRanger.Data.Model;
using AgeRanger.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace AgeRanger.Business.BusinessService
{
    [HandleException(applicationName: "AgeRanger.Business.BusinessService.PersonService")]
    public class PersonService:IPersonService
    {
        #region Fields
        private readonly IPersonRepository personRepository = default(IPersonRepository);
        #endregion

        #region Constructor

        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        #endregion Constructors

        #region Methods
        public List<PersonDTO> GetPersonInformation()
        {
            var result = default(List<PersonDTO>);
            var response = this.personRepository.GetPersonInformation();
            if (response != null && response.Tables != null && response.Tables.Count > 0)
            {
                result = response.Tables[0].AsEnumerable().Select(data => new PersonDTO()
                {
                    Id = Convert.ToInt32( data["Id"]),
                    FirstName = data.Field<string>("FirstName"),
                    LastName = data.Field<string>("LastName"),
                    Age = Convert.ToInt32(data["Age"]),
                    AgeGroup = data.Field<string>("AgeGroup")
                }).ToList();
            }
            return result;
        }

        public ResponseDTO AddUpdatePersonInformation(PersonDTO personDTO)
        {
            var result = new ResponseDTO(); 
            
            result.ExceptionMessage = ""; 
            var response = this.personRepository.GetPersonInformation(personDTO);
            if (response != null && response.Tables != null && response.Tables.Count > 0)
            {
                DataRow dr = null;
                for(int i=0;i<response.Tables[0].Rows.Count;i++)
                {
                    dr = response.Tables[0].Rows[i];
                    if (personDTO.Id!=Convert.ToInt32(dr["Id"]) && personDTO.Age== Convert.ToInt32(dr["Age"])) {
                        result.ExceptionMessage = "Name already exists.";
                        break;
                    }   
                }
            }
            if (result.ExceptionMessage=="" )
                result.personDTO = this.personRepository.AddUpdatePersonInformation(personDTO);  
            return result;
        }

        public bool DeletePersonInformation(int Id)
        {
            return this.personRepository.DeletePersonInformation(Id);  
        }
        #endregion
    }
}
