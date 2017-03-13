using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgeRanger.API.Controllers;
using AgeRanger.Business.IBusinessService;
using AgeRanger.Data.IRepository;
using AgeRanger.Data.Repository;
using AgeRanger.Business.BusinessService;
using AgeRanger.Data.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Web.Http;

namespace AgeRanger.API.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShowPerson()
        {
            IPersonRepository personRepository = new PersonRepository();
            IPersonService personService = new PersonService(personRepository);
            var personDetail = personService.GetPersonInformation();
            var controller = new PersonInformationController(personService)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            var _response = controller.GetPersonList(null);

            var responseResult = JsonConvert.DeserializeObject<List<PersonDTO>>(_response.Content.ReadAsStringAsync().Result);
            var testResult = personService.GetPersonInformation();
            Assert.AreEqual(testResult.Count, responseResult.Count);
        }

        [TestMethod]
        public void AddPerson()
        {
            PersonDTO personDTO = null;
            Random rnd = new Random();
            int age = 0;
            IPersonRepository personRepository = new PersonRepository();
            IPersonService personService = new PersonService(personRepository);
            var controller = new PersonInformationController(personService)
            {
                Request = new System.Net.Http.HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            //var controller = new PersonInformationController(personService);

            for (int i=1;i<=50;i++)
            {
                age = rnd.Next(1, 50);
                personDTO = new PersonDTO();
                personDTO.Id = 0; 
                personDTO.FirstName = "Person " + i;
                personDTO.LastName = "Person " + i;
                personDTO.Age = age;
                var _response = controller.AddUpdatePersonInformation(personDTO);

                var responseResult = JsonConvert.DeserializeObject<ResponseDTO>(_response.Content.ReadAsStringAsync().Result);
                Assert.AreEqual(0, responseResult.ExceptionMessage.Length);
            }
        }

    }
}
