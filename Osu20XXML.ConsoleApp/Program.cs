// This file was auto-generated by ML.NET Model Builder. 

using System;
using Osu20XXML.Model;

namespace Osu20XXML.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                Hp = 6F,
                Cs = 4F,
                Od = 6F,
                Ar = 6F,
                AvgDeltaTime = 691.715F,
                StddevDeltaTime = 1426.76F,
                DiffVariance = 0.5143238F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Year with predicted Year from sample data...\n\n");
            Console.WriteLine($"Hp: {sampleData.Hp}");
            Console.WriteLine($"Cs: {sampleData.Cs}");
            Console.WriteLine($"Od: {sampleData.Od}");
            Console.WriteLine($"Ar: {sampleData.Ar}");
            Console.WriteLine($"AvgDeltaTime: {sampleData.AvgDeltaTime}");
            Console.WriteLine($"StddevDeltaTime: {sampleData.StddevDeltaTime}");
            Console.WriteLine($"DiffVariance: {sampleData.DiffVariance}");
            Console.WriteLine($"\n\nPredicted Year value {predictionResult.Prediction} \nPredicted Year scores: [{String.Join(",", predictionResult.Score)}]\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }
    }
}
