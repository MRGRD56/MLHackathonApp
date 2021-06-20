using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Transforms.Text;
using MLNetApp.Clustering;
using MLNetApp.Clustering.Models;
using MLNetApp.Data;
using MLNetApp.Shared.Models;
using MLNetApp.Tagging.Models;
using Newtonsoft.Json;

namespace MLNetApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var articleClusterer = new ArticleClusterer(DataRepository.GetArticles());
            var clusteredArticles = articleClusterer.GetClusters(3).ToList();

            var clustersList = clusteredArticles
                .GroupBy(article => article.PredictedClusterId)
                .Select(grouping => new ArticlesCluster
                {
                    ClusterId = grouping.Key,
                    Articles = grouping.ToList()
                })
                .ToList();

            var clustersRootDirectory = Path.Combine(Environment.CurrentDirectory, $"{DateTime.Now:yyyy-MM-ddTHH-mm-ss}_clusters_{clustersList.Count}");
            foreach (var articlesCluster in clustersList)
            {
                var clustersDirectory = Path.Combine(Environment.CurrentDirectory, $@"{clustersRootDirectory}\{articlesCluster.ClusterId}\");
                if (!Directory.Exists(clustersDirectory))
                {
                    Directory.CreateDirectory(clustersDirectory);
                }

                for (var index = 0; index < articlesCluster.Articles.Count; index++)
                {
                    var article = articlesCluster.Articles[index];
                    var filePath = Path.Combine(clustersDirectory, $"{index + 1}.txt");
                    File.WriteAllText(filePath, article.Text);
                }
            }
        }
    }
}