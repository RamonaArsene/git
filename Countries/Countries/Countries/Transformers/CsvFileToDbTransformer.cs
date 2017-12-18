using Countries.Entities;
using Countries.Parsers;
using Countries.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Transformers
{
    public class CsvFileToDbTransformer
    {
        ICsvCountrieFileParser parser;
        IRepository<Country> repository;

        public CsvFileToDbTransformer(ICsvCountrieFileParser parser, IRepository<Country> repository)
        {
            this.parser = parser;
            this.repository = repository;
        }

        public void Execute(string filePath)
        {
            var entities = parser.Read(filePath);

            foreach(var entity in entities)
            {
                repository.Insert(entity);
            }
        }
    }
}
