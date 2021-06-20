using System.Collections.Generic;

namespace MLNetApp.Clustering.Models
{
    public class ArticlesCluster
    {
        public uint ClusterId { get; set; }
        
        public List<ArticleClusterPrediction> Articles { get; set; }
    }
}