using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Countries.Entities;
using System.IO;

namespace Countries.Parsers
{
    public class CsvCountrieFileParser : ICsvCountrieFileParser
    {
        public IList<Country> Read(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var result = new List<Country>();

            for(int i = 1; i < lines.Length; i++)
            {
                var items = lines[i].Split(',');
                var country = new Country();
                var strCode = items[0];
                var strLatitude = items[1];
                var strLong = items[2];
                var strName = items[3];

                country.Code = strCode;
                country.Name = strName;

                if (!string.IsNullOrEmpty(strLatitude))
                {
                    country.Latitude = Convert.ToDecimal(strLatitude);
                }

                if (!string.IsNullOrEmpty(strLong))
                {
                    country.Longitude = Convert.ToDecimal(strLong);
                }

                result.Add(country);
            }

            return result;
        }
    }
}
