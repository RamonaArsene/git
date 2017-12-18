using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.V2.Tests
{
    public static class DosarExtensions
    {
        public static void AssertAllPropertiesAreEqual(this Dosar expected, Dosar actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Code, actual.Code);    
            Assert.AreEqual(expected.Description, actual.Description);    
        }

    }
}
