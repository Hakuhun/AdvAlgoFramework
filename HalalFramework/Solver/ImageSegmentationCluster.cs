using HalalFramework.Problem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    
    class ImageSegmentationCluster
    {
        static Random rnd = new Random();
        ImageSegmentationProblem problem;
        private List<Cluster> clusters;
        public ImageSegmentationCluster(string filename, int NumberOfClusters)
        {
            problem = new ImageSegmentationProblem(filename);
            this.KMeans(NumberOfClusters, problem.RawGrayscale);
            savePixelClusterToFile("clustered_"+filename);
        }

        public void KMeans(int NumberOfClusters, byte[] dataset)
        {
            //N mennyiségű klaszter létrehozása.
            this.clusters = InitializeCentroids(problem.RawGrayscale, NumberOfClusters);
            List<Cluster> newClusters = null;
            do
            {
                newClusters = clusters;
                for (int i = 0; i < dataset.Length; i++)
                {
                    Cluster nearest = NearestCluster(dataset[i], newClusters);
                    newClusters.Where(x => x.Equals(nearest)).FirstOrDefault().Elements.Add(dataset[i]);
                }
                for (int i = 0; i < clusters.Count; i++)
                {
                    clusters[i].Centroid = clusters[i].Elements.Sum() / clusters[i].Elements.Count;
                }
                               
            } while (!StopCondition(newClusters, clusters));
                
        }

        private void savePixelClusterToFile(string filename)
        {
            Bitmap clustered = new Bitmap(problem.Width, problem.Height);
            for (int i = 0; i < problem.RawGrayscale.Count(); i++)
            {
                clustered.SetPixel(i % problem.Width, i / problem.Width, 
                    clusters.Where(x => x.Elements.Contains(problem.RawGrayscale[i])).FirstOrDefault().Color);
            }
            clustered.RotateFlip(RotateFlipType.Rotate90FlipX);
            clustered.Save(filename);
        }

        private bool StopCondition(List<Cluster> newCluster, List<Cluster> oldCluster)
        {
            return oldCluster.Equals(oldCluster);
        }

        private List<Cluster> InitializeCentroids(byte[] dataset, int n)
        {
            List<Cluster> local = new List<Cluster>();
            for (int i = 0; i < n; i++)
            {
                Cluster c = new Cluster() { Centroid = dataset[rnd.Next(dataset.Length)]};                 
                local.Add(c);
            }

            return local;
        }

        Cluster NearestCluster(int point, List<Cluster> clusters)
        {
            Dictionary<Cluster, double> distances = new Dictionary<Cluster, double>();
            foreach (Cluster cluster in clusters)
            {
                distances[cluster] = Distance(cluster.Centroid, point);
            }
            return distances.Where(x => x.Value == distances.Values.Min()).FirstOrDefault().Key;
        }

        //Euklédeszi távolság
        private double Distance(int point1, int point2)
        {
            return Math.Sqrt(Math.Pow((point2 - point1), 2));
        }
    }

    class Cluster
    {
        static Random rnd = new Random();
        private List<int> elements = new List<int>();
        private int centroid;
        private Color color;
        public Cluster()
        {
            Color = Color.FromArgb(rnd.Next(Int32.MaxValue));
        }

        public List<int> Elements { get => elements; set => elements = value; }
        public int Centroid { get => centroid; set => centroid = value; }
        public Color Color { get => color; set => color = value; }
    }
}
