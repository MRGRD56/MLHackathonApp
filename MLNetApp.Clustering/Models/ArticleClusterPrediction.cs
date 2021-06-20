using Microsoft.ML.Data;
using MLNetApp.Shared.Models;

namespace MLNetApp.Clustering.Models
{
    public class ArticleClusterPrediction : Article
    {
        [ColumnName("PredictedLabel")]
        public uint PredictedClusterId { get; set; }
        
        [ColumnName("Score")]
        public float[] Distances { get; set; }
        
        public string[] Tokens { get; set; }
    }
}