using System;

namespace Munim.CsvNet.Attributes
{
    public class CsvAttribute : Attribute
    {
        public string Header { get; set; }
        public int ColumnIndex { get; set; }
    }
}