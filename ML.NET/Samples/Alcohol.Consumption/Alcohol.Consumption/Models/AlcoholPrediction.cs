using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alcohol.Consumption.Models
{
    public class AlcoholPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
