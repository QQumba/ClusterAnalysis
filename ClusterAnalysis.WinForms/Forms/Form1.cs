using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClusterAnalysis.WinForms.Data;

namespace ClusterAnalysis.WinForms.Forms
{
    public partial class Form1 : Form
    {
        private readonly int _width = 800;
        private readonly int _height = 800;
        
        private DataPoint[] _points;
        private Cluster[] _clusters;
        
        private readonly Color[] _colors = {Color.OrangeRed, Color.LawnGreen, Color.Magenta, Color.Bisque, Color.Aqua, Color.Coral, Color.Yellow,  };
        
        private readonly PictureBox _pictureBox = new PictureBox()
        {
            Size = new Size(800,800),
            Location = new System.Drawing.Point(0,0)
        };

        private readonly Button _resetButton = new Button()
        {
            Size = new Size(300, 100),
            Location = new System.Drawing.Point(50, 800),
            Text = "Reset",
        };

        private readonly Button _clusterizeButton = new Button()
        {
            Size = new Size(300, 100),
            Location = new System.Drawing.Point(450, 800),
            Text = "Clusterize"
        };

        public Form1()
        {
            InitializeComponent();

            _resetButton.Click += (sender, args) =>
            {
                InitializeData();
                DrawPoints(sender, args);
            };
            _clusterizeButton.Click += async (sender, args) =>
            {
                Clusterize(sender, args);
                DrawPoints(sender, args);
                // await Task.Delay(200);
            };

            this.Size = new Size(_width, _height + 200);
            this.Controls.Add(_pictureBox);
            this.Controls.Add(_resetButton);
            this.Controls.Add(_clusterizeButton);
            
            InitializeData();
            DrawPoints(this, EventArgs.Empty);
        }
        private void InitializeData()
        {
            _points = GeneratePoints(Values.PointsCount);
            _clusters = new Cluster[Values.ClustersCount];
            var clusterCenters = GeneratePoints(Values.ClustersCount);
            for (int i = 0; i < _clusters.Length; i++)
            {
                _clusters[i] = new Cluster(clusterCenters[i], _colors[i]);
            }

            foreach (var point in _points)
            {
                point.AssignToClosestCluster(_clusters);
            }
        }

        private void DrawPoints(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(_pictureBox.Width, _pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(new SolidBrush(Color.Black), 0,0,bitmap.Width,bitmap.Height);

            foreach (var cluster in _clusters)
            {
                g.FillRectangle(new SolidBrush(cluster.Color),
                    cluster.Center.X * 200 + bitmap.Width / 2,
                    cluster.Center.Y * 200 + bitmap.Height / 2,
                    9, 
                    9);
            }

            foreach (var point in _points)
            {
                g.FillRectangle(new SolidBrush(point.Cluster?.Color ?? Color.White),
                    point.X * 200 + bitmap.Width / 2,
                    point.Y * 200 + bitmap.Height / 2,
                    3,
                    3);
            }

            _pictureBox.Image = bitmap;
        }

        private void Clusterize(object sender, EventArgs e)
        {
            foreach (var cluster in _clusters)
            {
                cluster.DisplaceCenter();
            }

            foreach (var point in _points)
            {
                point.AssignToClosestCluster(_clusters);
            }
        }

        private DataPoint[] GeneratePoints(int count)
        {
            var generator = new Generator();
            return generator.GeneratePoints(count);
        }
    }
}