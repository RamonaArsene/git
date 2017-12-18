using Countries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Operations
{
    public interface ICountriesOperations
    {
        Country SingleByCode(string code);
        IList<Country> ListNameStartsWith(string name);
        Dictionary<string, List<Country>> GroupByNameFirstLetter();
        IList<Country> AllWithPosition();
        decimal SumOfLatitudeForNameStartsWith(string name);
        decimal MedianOfSumsOfLatAnLongForNameStartsWith(string name);
        IList<Country> FirstCountriesOrderedByName(int howMany, string name);
        IList<Country> GetPageFilterByNameOrderByName(int howMany, int pageNumber, string name);
        IList<LittleCountry> GetAllCountryNames();
    }
}
