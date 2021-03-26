using Accord.Statistics.Distributions.Univariate;
using ClusterAnalysis.WinForms.Data;

namespace ClusterAnalysis.WinForms
{
    public class Generator
    {
        public DataPoint[] GeneratePoints(int pointsCount = Values.PointsCount)
        {
            var nd = new NormalDistribution(0, Values.StandardDeviation);
            var values = nd.Generate(pointsCount * 2);
            var points = new DataPoint[pointsCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new DataPoint((float)values[i * 2], (float)values[i * 2 + 1]);
            }

            return points;
        }
    }
}