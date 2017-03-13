using AgeRanger.Data.Model;
using System.Collections.Generic;

namespace AgeRanger.Business.IBusinessService
{
    public interface IAgeGroupService
    {
        List<AgeGroupDTO> GetAgeGroup();
    }
}
