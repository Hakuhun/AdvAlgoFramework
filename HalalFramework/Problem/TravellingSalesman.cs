using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Problem
{
    class TravellingSalesman
    {
        private List<Town> towns;

        private List<Route> routes;

        public List<Town> Towns { get => towns; set => towns = value; }
        internal List<Route> Routes { get => routes; set => routes = value; }

        /// <summary>
        /// Ő a genotype
        /// </summary>
        /// <param name="filename"></param>
        public TravellingSalesman(string filename)
        {
            Towns = this.loadTownsFromFile(filename);
        }

        public double objective(List<Town> route)
        {
            double sum_length = 0;
            double minval = double.MaxValue;

            for (int ti = 0; ti < route.Count- 1; ti++)
            {
                Town t1 = route[ti];
                Town t2 = route[ti + 1];
                double value = Math.Sqrt(Math.Pow((t1.X - t2.X), 2) + Math.Pow((t1.Y - t2.Y), 2));
            }
            return sum_length;
        }

        List<Town> loadTownsFromFile(string fileName)
        {
            List<Town> local = new List<Town>();
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                string[] splited = line.Split('\t');
                int x, y;
                int.TryParse(splited[0], out x);
                int.TryParse(splited[1], out y);
                Town t = new Town() { X = x, Y = y };
                local.Add(t);
            }
            return local;
        }

        private List<Route> generateRoutes()
        {
            List<Route> routes = new List<Route>();
            foreach (var star in Towns)
            {
                foreach (var end in Towns)
                {
                    routes.Add(new Route() { Start = star, End = end });
                }
            }
            return routes;
        }

        void SaveTownsToFile(string fileName, List<Town> towns)
        {
            StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8);

            foreach (Town town in towns)
            {
                sw.WriteLine(string.Format("{0}}\t{1}",town.X, town.Y));
            }
            sw.Close();
        }
    }

    class Town
    {
        private float x, y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }

        public override string ToString()
        {
            return string.Format(@"Point\t{0}\t{1}\tBlue",X,Y);
        }
    }

    class Route
    {
        private Town start, end;

        public Town Start { get => start; set => start = value; }
        public Town End { get => end; set => end = value; }

        double Distance()
        {
            return Math.Sqrt(Math.Pow((start.X - end.X), 2) + Math.Pow((start.Y - end.Y), 2));
        }

        public override string ToString()
        {
            return string.Format(@"Arrow\t{0}\t{1}\t{2}\t{3}\tred", Start.X, Start.Y, End.X, End.Y);
        }
    }
}
