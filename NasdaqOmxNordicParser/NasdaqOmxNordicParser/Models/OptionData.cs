using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasdaqOmxNordicParser.Models
{
    internal class OptionData
    {
        [Name("Date")]
        public string Date { get; set; }
        [Name("Type")]
        public string Type { get; set; }
        [Name("Strike")]
        public decimal Strike { get; set; }
        [Name("Bid")]
        public decimal Bid { get; set; }
        [Name("Ask")]
        public decimal Ask { get; set; }
        [Name("Last")]
        public decimal Last { get; set; }
        [Name("Volume")]
        public int Volume { get; set; }
        [Name("OpenInterest")]
        public int OpenInterest { get; set; }
    }
}
