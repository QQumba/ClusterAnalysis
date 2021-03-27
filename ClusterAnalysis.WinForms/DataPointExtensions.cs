namespace ClusterAnalysis.WinForms
{
    public static class DataPointExtensions
    {
        public static DataPoint Displace(this DataPoint point, float x, float y)
        {
            point.X += x;
            point.Y += y;
            return point;
        }
    }
}