using AgeRanger.Business.IBusinessService;
using AgeRanger.Data.IRepository;
using AgeRanger.Data.Model;
using AgeRanger.Utils;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AgeRanger.Business.BusinessService
{
    [HandleException(applicationName: "AgeRanger.Business.BusinessService.AgeGroupService")]
    public class AgeGroupService:IAgeGroupService
    {
        #region Fields
        private readonly IAgeGroupRepository ageGroupRepository = default(IAgeGroupRepository);
        #endregion

        #region Constructor

        public AgeGroupService(IAgeGroupRepository ageGroupRepository)
        {
            this.ageGroupRepository = ageGroupRepository;
        }
        #endregion Constructors

        #region Methods
        public List<AgeGroupDTO> GetAgeGroup()
        {
            var result = default(List<AgeGroupDTO>);
            var response = this.ageGroupRepository.GetAgeGroup();
            if (response != null && response.Tables != null && response.Tables.Count > 0)
            {
                result = response.Tables[0].AsEnumerable().Select(data => new AgeGroupDTO()
                {
                    Id = Convert.ToInt32(data["Id"]),
                    MinAge = data.Field<long?>("MinAge"),
                    MaxAge = data.Field<long?>("MaxAge"),
                    Description = data.Field<string>("Description")
                }).ToList();
            }
            return result;

        }
        #endregion
    }
}
