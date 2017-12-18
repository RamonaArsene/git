using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countries.Entities;

namespace Countries.Operations
{
    public class CountriesOperations : ICountriesOperations
    {
        IList<Country> countries;

        public CountriesOperations(IList<Country> countries)
        {
            this.countries = countries;
        }

        public IList<Country> AllWithPosition()
        {
            throw new NotImplementedException();
        }

        public IList<Country> FirstCountriesOrderedByName(int howMany, string name)
        {
            throw new NotImplementedException();
        }

        public IList<LittleCountry> GetAllCountryNames()
        {
            throw new NotImplementedException();
        }

        public IList<Country> GetPageFilterByNameOrderByName(int howMany, int pageNumber, string name)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Country>> GroupByNameFirstLetter()
        {
            var result = new Dictionary<string, List<Country>>();

            foreach(var country in countries)
            {
                var firstLetter = country.Name.Substring(0, 1);

                if (!result.ContainsKey(firstLetter))
                    result.Add(firstLetter, new List<Country>());

                result[firstLetter].Add(country);
            }

            return result;
        }

        public IList<Country> ListNameStartsWith(string name)
        {
            var result = new List<Country>();

            foreach (var country in countries)
                if (country.Name.StartsWith(name))
                    result.Add(country);

            return result;
        }

        public decimal MedianOfSumsOfLatAnLongForNameStartsWith(string name)
        {
            throw new NotImplementedException();
        }

        public Country SingleByCode(string code)
        {
            foreach (var country in countries)
                if (code == country.Code)
                    return country;

            return null;
        }

        public decimal SumOfLatitudeForNameStartsWith(string name)
        {
            throw new NotImplementedException();
        }
    }
}
