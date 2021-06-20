using System.Collections.Generic;
using MLNetApp.Shared.Models;

namespace MLNetApp.Clustering.Models
{
    public class ArticlesCluster
    {
        public uint ClusterId { get; set; }
        
        public List<ArticleClusterPrediction> Articles { get; set; }
    }
}