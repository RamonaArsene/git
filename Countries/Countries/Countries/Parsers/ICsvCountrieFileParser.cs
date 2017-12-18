using Countries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Parsers
{
    public interface ICsvCountrieFileParser
    {
        IList<Country> Read(string filePath);
    }
}
