using System;
using System.Collections.Generic;
using Accord.Statistics.Distributions.Univariate;
using ClusterAnalysis.WinForms.Data;

namespace ClusterAnalysis.WinForms
{
    public class Generator
    {
        public DataPoint[] GeneratePoints(int pointsCount, double standardDeviation)
        {
            var nd = new NormalDistribution(0, standardDeviation);
            var values = nd.Generate(pointsCount * 2);
            var points = new DataPoint[pointsCount];

            for (int i = 0; i < pointsCount; i++) 
            {
                points[i] = new DataPoint((float)values[i * 2], (float)values[i * 2 + 1]);
            }
            return points;
        }

        public DataPoint[] GenerateClustersPoints(int pointsCount,
            int clustersCount, double standardDeviation, double clusterStandardDeviation)
        {
            var clusterCenters = GeneratePoints(clustersCount, clusterStandardDeviation);
            var points = new DataPoint[pointsCount];
            if (clustersCount < 2)
            {
                return GeneratePoints(pointsCount, standardDeviation);
            }
            
            var random = new Random();
            var pointsLeft = pointsCount;
            DataPoint[] clusterPoints;
            for (int i = 0; i < clustersCount - 1; i++)
            {
                var minPointsPerCluster = pointsLeft / (clustersCount - i) - pointsLeft / (clustersCount - i) / 2;
                var maxPointsPerCluster = pointsLeft / (clustersCount - i) + pointsLeft / (clustersCount - i) / 2;
                
                clusterPoints = GeneratePoints(random.Next(minPointsPerCluster, maxPointsPerCluster), standardDeviation);
                for (int j = 0; j < clusterPoints.Length; j++)
                {
                    points[j + pointsCount - pointsLeft] = clusterPoints[j]
                        .Displace(
                            clusterCenters[i].X,
                            clusterCenters[i].Y);
                }
                pointsLeft -= clusterPoints.Length;
            }

            clusterPoints = GeneratePoints(pointsLeft, standardDeviation);
            for (int j = 0; j < clusterPoints.Length; j++)
            {
                points[j + pointsCount - pointsLeft] = clusterPoints[j]
                    .Displace(
                        clusterCenters[^1].X,
                        clusterCenters[^1].Y);
            }
            
            return points;
        }
    }
}