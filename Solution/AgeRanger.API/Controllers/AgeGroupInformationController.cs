using AgeRanger.Utils;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AgeRanger.API.Controllers
{
    [HandleException(applicationName: "AgeGroupInformationController")]
    public class AgeGroupInformationController : ApiController
    {
        #region Fields

        private HttpResponseMessage response = default(HttpResponseMessage);
        private Business.IBusinessService.IAgeGroupService ageGroupService = default(Business.IBusinessService.IAgeGroupService);

        #endregion Fields

        #region Constructors

        public AgeGroupInformationController(Business.IBusinessService.IAgeGroupService ageGroupService)
        {
            this.ageGroupService = ageGroupService;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public HttpResponseMessage GetAgeGroup(dynamic request)
        {
            var result = ageGroupService.GetAgeGroup();
            response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        #endregion
    }
}
