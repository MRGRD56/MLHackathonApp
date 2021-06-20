using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MLNetApp.Shared.Extensions;
using MLNetApp.Shared.Models;
using MLNetApp.Tagging.Models;

namespace MLNetApp.Data
{
    internal static class DataRepository
    {
        private static readonly string ArticlesDirectory = Path.Combine(Environment.CurrentDirectory, @"Data\Articles\");

        private const string StopWordsResourceName = "MLNetApp.Data.Additional.stopWords.txt";

        private static IEnumerable<string> GetTextFromFiles(string directoryPath, string searchPattern = "*.*")
        {
            var articlesFiles = Directory.GetFiles(directoryPath, searchPattern);

            return articlesFiles.Select(File.ReadAllText);
        }
        
        internal static IEnumerable<Article> GetArticles()
        {
            var articlesFilesContent = GetTextFromFiles(ArticlesDirectory, "*.txt");

            return articlesFilesContent.Select(file => new Article(file));
        }

        internal static IEnumerable<string> GetStopWords()
        {
            return EmbeddedResources.GetEmbeddedResourceString(StopWordsResourceName).Split('\n');
        }
    }
}