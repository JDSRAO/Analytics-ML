using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alcohol.Consumption.Models
{
    public class AlcoholData
    {
        [LoadColumn(0)]
        public string Country { get; set; }

        [LoadColumn(1)]
        public string BeerServings { get; set; }

        [LoadColumn(2)]
        public double SpiritServings { get; set; }

        [LoadColumn(4)]
        public string PureAlcohol { get; set; }
    }
}
