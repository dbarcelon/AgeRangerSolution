using System;
using System.Data;

namespace AgeRanger.Data.IRepository
{
    public interface IAgeGroupRepository
    {
        DataSet GetAgeGroup();
    }
}
