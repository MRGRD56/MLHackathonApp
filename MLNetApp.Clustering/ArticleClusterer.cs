using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using MLNetApp.Clustering.Models;
using MLNetApp.Shared.Models;
using Newtonsoft.Json;

namespace MLNetApp.Clustering
{
    public class ArticleClusterer
    {
        private readonly IEnumerable<Article> _articles;

        public ArticleClusterer(IEnumerable<Article> articles)
        {
            _articles = articles;
        }

        public IEnumerable<ArticleClusterPrediction> GetClusters(int clustersCount)
        {
            var mlContext = new MLContext();
            var emptyData = mlContext.Data.LoadFromEnumerable(_articles);

            var pipeline = mlContext.Transforms.Text.TokenizeIntoWords(
                    "Tokens", "Text", 
                    new[] {' ', ',', '.', '\n', '\r', '\t', '—', '(', ')', '[', ']', '"', 
                        '\'', '«', '‹', '»', '›', '〞', '〟', ';', '<', '>', '?', '!'})
                .Append(mlContext.Transforms.Text.RemoveDefaultStopWords(
                    "Tokens", "Tokens", StopWordsRemovingEstimator.Language.Russian))
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", "Tokens"))
                .Append(mlContext.Clustering.Trainers.KMeans(numberOfClusters: clustersCount));

            var model = pipeline.Fit(emptyData);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<Article, ArticleClusterPrediction>(model);

            var articlesArray = _articles.ToArray();
            foreach (var article in articlesArray)
            {
                yield return predictionEngine.Predict(article);
            }
        }
    }
}