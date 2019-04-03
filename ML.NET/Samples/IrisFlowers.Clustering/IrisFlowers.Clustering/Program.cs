using IrisFlowers.Clustering.Models;
using Microsoft.ML;
using System;
using System.IO;

namespace IrisFlowers.Clustering
{
    class Program
    {
        // <SnippetPaths>
        static readonly string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "iris.data");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "IrisClusteringModel.zip");
        // </SnippetPaths>

        static void Main(string[] args)
        {
            // <SnippetCreateContext>
            var mlContext = new MLContext(seed: 0);
            // </SnippetCreateContext>

            // <SnippetCreateDataView>
            var dataView = mlContext.Data.LoadFromTextFile<IrisData>(_dataPath, hasHeader: false, separatorChar: ',');
            // </SnippetCreateDataView>

            // <SnippetCreatePipeline>
            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "SepalLength", "SepalWidth", "PetalLength", "PetalWidth")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, clustersCount: 3));
            // </SnippetCreatePipeline>

            // <SnippetTrainModel>
            var model = pipeline.Fit(dataView);
            // </SnippetTrainModel>

            // <SnippetSaveModel>
            using (var fileStream = new FileStream(_modelPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model, fileStream);
            }
            // </SnippetSaveModel>

            // <SnippetPredictor>
            var predictor = model.CreatePredictionEngine<IrisData, ClusterPrediction>(mlContext);
            // </SnippetPredictor>

            // < SnippetPredictionExample >
            var prediction = predictor.Predict(TestIrisData.Setosa);
            Console.WriteLine($"Cluster: {prediction.PredictedClusterId}");
            Console.WriteLine($"Distances: {string.Join(" ", prediction.Distances)}");
            // </SnippetPredictionExample>

            Console.ReadKey();
        }
    }
}
