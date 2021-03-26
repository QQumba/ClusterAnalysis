using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace ClusterAnalysis.WinForms
{
    public class Cluster
    {
        private readonly List<DataPoint> _points = new List<DataPoint>();
        
        public Cluster(DataPoint center, Color color)
        {
            Center = center;
            Color = color;
        }
        public DataPoint Center { get; set; }
        public Color Color { get; }

        public void AddPoint(DataPoint point)
        {
            point.Cluster = this;
            _points.Add(point);
        }

        public void DisplaceCenter()
        {
            Center.X = 0;
            Center.Y = 0;
            foreach (var point in _points)
            {
                Center.X += point.X;
                Center.Y += point.Y;
            }

            Center.X /= _points.Count;
            Center.Y /= _points.Count;
            _points.Clear();
        }

        public double GetDistanceToCenter(DataPoint point)
        {
            return Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
        }
    }
}