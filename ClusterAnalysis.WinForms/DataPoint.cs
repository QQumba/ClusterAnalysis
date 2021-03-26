using System;
using System.Collections.Generic;
using System.Drawing;
using ClusterAnalysis.WinForms.Data;

namespace ClusterAnalysis.WinForms
{
    public class DataPoint
    {
        public DataPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public float X { get; set; }
        public float Y { get; set; }

        public Cluster Cluster { get; set; }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}