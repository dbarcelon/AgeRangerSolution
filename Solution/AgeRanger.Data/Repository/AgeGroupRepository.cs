using AgeRanger.Data.IRepository;
using AgeRanger.Utils;
using System.Data;

namespace AgeRanger.Data.Repository
{
    [HandleException(applicationName: "AgeRanger.Data.Repository.AgeGroupRepository")]
    public class AgeGroupRepository:IAgeGroupRepository
    {
        public DataSet GetAgeGroup()
        {
            DataSet response = new DataSet();

            string sqlText = "SELECT Id, MinAge, MaxAge, Description FROM AgeGroup";
            response = SQLDataProvider.ExecuteStoresProcedure(sqlText);

            return response;

        }
    }
}
