using Alcohol.Consumption.Models;
using Microsoft.ML;
using System;
using System.IO;
using System.Linq;

namespace Alcohol.Consumption
{
    class Program
    {
        const string fileName = "drinks.csv";

        static void Main(string[] args)
        {
            var path = GetFilePath(fileName);
            var mlContext = new MLContext(seed: 0);
            Console.WriteLine($"Reading data from path: {path}");
            var dataView = mlContext.Data.LoadFromTextFile<AlcoholData>(path, hasHeader: true, separatorChar: ',');
            Console.WriteLine("Completed reading data from file \n");

            Console.WriteLine("Trasformation of data \n");
            var names = ReflectionExtension.GetParameterNames(typeof(AlcoholData));
            var feature = names[0];
            var dataColumns = names.Where(x => x != feature).ToArray();
            var pipeline = mlContext.Transforms
                .Conversion.MapValueToKey(feature)
                .Append(mlContext.Transforms.Concatenate("Features", dataColumns))
                .AppendCacheCheckpoint(mlContext)
                .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumnName: feature, featureColumnName: "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            Console.WriteLine("Create prediction model");
            var model = pipeline.Fit(dataView);

            Console.WriteLine("Prediction of model");

            

            Console.ReadKey();
        }

        static string GetFilePath(string fileName)
        {
            var path = Path.Combine(Environment.CurrentDirectory, fileName);
            return path;
        }
    }
}
