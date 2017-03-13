using AgeRanger.Data.IRepository;
using AgeRanger.Data.Model;
using AgeRanger.Utils;
using System;
using System.Data;

namespace AgeRanger.Data.Repository
{
    [HandleException(applicationName: "AgeRanger.Data.Repository.PersonRepository")]
    public class PersonRepository:IPersonRepository
    {
        const string sqlTextId = "SELECT seq FROM sqlite_sequence WHERE Name = 'Person'";

        public DataSet GetPersonInformation(PersonDTO personDTO=null)
        {
            DataSet response = new DataSet();

            string sqlText = "SELECT a.Id, a.FirstName, a.LastName, a.Age, b.Description AS AgeGroup";
            sqlText += "  FROM Person a";
            sqlText += " INNER JOIN AgeGroup b";
            sqlText += " ON (a.Age>=b.MinAge OR b.MinAge IS NULL)";
            sqlText += " AND (a.Age<b.MaxAge OR b.MaxAge IS NULL)";

            //string sqlText = "SELECT Id, FirstName, LastName, Age, AgeGroup FROM PersonAgeGroupView";
            if (personDTO!=null )
            {
                sqlText += string.Format(" WHERE FirstName='{0}' AND LastName='{1}'", personDTO.FirstName, personDTO.LastName);
            }
            response = SQLDataProvider.ExecuteStoresProcedure(sqlText);

            return response;
        }

        public PersonDTO AddUpdatePersonInformation(PersonDTO personDTO)
        {
            string sqlText = "INSERT INTO Person(FirstName,LastName,Age)";
            sqlText += string.Format(" VALUES('{0}','{1}',{2})", personDTO.FirstName, personDTO.LastName, personDTO.Age);

            if (personDTO.Id>0)
            {
                sqlText = string.Format("Update Person SET FirstName='{0}', LastName='{1}', Age={2} WHERE Id={3}", personDTO.FirstName, personDTO.LastName, personDTO.Age, personDTO.Id);
            }
            if (SQLDataProvider.ExecuteNonQuery(sqlText))
            {
                personDTO.Id = Convert.ToInt32( SQLDataProvider.ExecuteScalar(sqlTextId));
            }
            return personDTO;
        }

        public bool DeletePersonInformation(int Id)
        {
            string sqlText = string.Format("DELETE FROM Person WHERE Id={0}", Id);
            return SQLDataProvider.ExecuteNonQuery(sqlText);
        }
    }
}
