using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munim.CsvNet.Tests
{
    public class CsvNetTestBase
    {
        [ClassInitialize]
        public void LoadCsv()
        {
            
        }


        protected string GetSample(int id)
        {
            return File.ReadAllText("test_data\\" + id + ".csv");
        }
    }
}