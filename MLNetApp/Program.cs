using System;
using System.Collections.Generic;
using Microsoft.ML;
using MLNetApp.Data;
using MLNetApp.Tagging.Models;

namespace MLNetApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var mlContext = new MLContext();
            var viewData = mlContext.Data.LoadFromEnumerable(DataRepository.GetArticles());
            
            
        }
    }
}