using AgeRanger.Data.Model;
using AgeRanger.Utils;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgeRanger.API.Controllers
{
    [HandleException(applicationName: "PersonInformationController")]
    public class PersonInformationController : ApiController
    {
        #region Fields

        private HttpResponseMessage response = default(HttpResponseMessage);
        private Business.IBusinessService.IPersonService personService = default(Business.IBusinessService.IPersonService);

        #endregion Fields

        #region Constructors

        public PersonInformationController(Business.IBusinessService.IPersonService personService)
        {
            this.personService = personService;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public HttpResponseMessage GetPersonList(PersonDTO personDTO)
        {
            var result = personService.GetPersonInformation();
            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [HttpPost]
        public HttpResponseMessage AddUpdatePersonInformation(PersonDTO personDTO)
        {
            var result = personService.AddUpdatePersonInformation(personDTO);
            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage DeletePersonInformation(dynamic request)
        {
            int Id = request.Id;
            var result = personService.DeletePersonInformation(Id);
            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        #endregion Method
    }
}
