using System;
using ClusterAnalysis.WinForms.Data;

namespace ClusterAnalysis.WinForms
{
    public static class KMeans
    {
        public static void AssignToClosestCluster(this DataPoint point, Cluster[] clusters)
        {
            if (clusters.Length != Values.ClustersCount)
            {
                throw new ArgumentOutOfRangeException(
                    $"Count of clusterization centers should be equal to {Values.ClustersCount}. Given: {clusters.Length}");
            }

            var closestCluster = clusters[0];
            for (int i = 1; i < clusters.Length; i++)
            {
                if (clusters[i].GetDistanceToCenter(point) < closestCluster.GetDistanceToCenter(point))
                {
                    closestCluster = clusters[i];
                }
            }  
            closestCluster.AddPoint(point);
        }
    }
}