using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Problem
{
    class SmallestBoundaryPoligonProblem
    {
        List<Point> points = new List<Point>();
        List<Point> solution = new List<Point>();
        public static Random RAND = new Random();

        public List<Point> Points { get => points; set => points = value; }
        public List<Point> Solution { get => solution; set => solution = value; }

        public SmallestBoundaryPoligonProblem(string filename)
        {
            loadPointsFromFile(filename);
            DeleteExistingSolution();
        }

        void loadPointsFromFile(string fileName)
        {
            string[] lines = System.IO.File.ReadAllLines(fileName);
            foreach (string loc in lines)
            {
                Point p = new Point();
                p.X = int.Parse(loc.Split('\t')[0]);
                p.Y = int.Parse(loc.Split('\t')[1]);
                Points.Add(p);
            }
        }

        void DeleteExistingSolution()
        {
            if (File.Exists("hcsto.txt"))
            {
                File.Delete("hcsto.txt");
            }
            if (File.Exists("simulatedannealing.txt"))
            {
                File.Delete("simulatedannealing.txt");
            }
            if (File.Exists("taboosearch.txt"))
            {
                File.Delete("taboosearch.txt");
            }
        }

        public void savePointsToFile(string path, int iteration)
        {
            StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
            sw.WriteLine("Clear");
            sw.WriteLine("Iteration\t" + iteration);

            foreach (Point pnt in Points)
            {
                sw.WriteLine(pnt);
            }

            for (int i = 0; i < Solution.Count; i++)
            {
                sw.WriteLine("Line\t" + Solution.ElementAt(i).X + '\t' + Solution.ElementAt(i).Y + '\t' + Solution.ElementAt((i + 1) >= Solution.Count ? 0 : (i + 1)).X + '\t' + Solution.ElementAt((i + 1) >= Solution.Count ? 0 : (i + 1)).Y + "\tred");
                sw.WriteLine(("Point\t" + Solution.ElementAt(i).X + '\t' + Solution.ElementAt(i).Y + "\tred").Replace('.', ','));
            }

            sw.Close();
        }

        //
        // Helper functions
        //
        public double distanceFromLine(Point lp1, Point lp2, Point p)
        {
            // https://en.wikipedia.org/wiki/Distance_from_a_point_to_a_line
            return ((lp2.Y - lp1.Y) * p.X - (lp2.X - lp1.X) * p.Y + lp2.X * lp1.Y - lp2.Y * lp1.X) / Math.Sqrt(Math.Pow(lp2.Y - lp1.Y, 2) + Math.Pow(lp2.X - lp1.X, 2));
        }

        double outerDistanceToBoundary(List<Point> solution)
        {
            double sum_min_distances = 0;

            for (int pi = 0; pi < Points.Count; pi++)
            {
                double min_dist = 0;
                for (int li = 0; li < solution.Count; li++)
                {
                    double act_dist = distanceFromLine(solution[li], solution[(li + 1) % solution.Count], Points[pi]);
                    if (li == 0 || act_dist < min_dist)
                        min_dist = act_dist;
                }
                if (min_dist < 0)
                    sum_min_distances += -min_dist;
            }
            return sum_min_distances;
        }

        double lengthOfBoundary(List<Point> solution)
        {
            double sum_length = 0;

            for (int li = 0; li < solution.Count - 1; li++)
            {
                Point p1 = solution[li];
                Point p2 = solution[(li + 1) % solution.Count];
                sum_length += Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            }
            return sum_length;
        }

        //
        // Objective functions
        //
        public double objective(List<Point> solution)
        {
            return lengthOfBoundary(solution);
        }

        public bool constraint(List<Point> solution)
        {
            return outerDistanceToBoundary(solution) <= 0;
        }

        public double constraint2(List<Point> solution)
        {
            return outerDistanceToBoundary(solution);
        }

        public double AddRandomDist(int dist)
        {
            return RAND.Next(-dist, dist);
        }

        public List<Point> GenerateRandomPoints(int boundarypoints)
        {
            List<Point> solution = new List<Point>();
            bool squared = false;
            while (!squared)
            {
                for (int i = 0; i < boundarypoints; i++)
                {
                    int x = (int)AddRandomDist(1000);
                    int y = (int)AddRandomDist(1000);
                    Point p = new Point() { X = x, Y = y, Solution = true };
                    solution.Add(p);
                }
                if (constraint(solution))
                {
                    squared = true;
                }
                else
                {
                    solution.Clear();
                }
            }

            return solution;
        }

        public List<Point> GenerateNewPoints()
        {
            List<Point> newSolution = new List<Point>();
            foreach (Point p in Solution)
            {
                newSolution.Add(new Point() { X = p.X + AddRandomDist(10), Y = p.Y + AddRandomDist(10), Solution = true });
            }
            return newSolution;
        }
    }

    class Point
    {
        double x, y;

        bool solution = false;

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public bool Solution { get => solution; set => solution = value; }

        public override string ToString()
        {
            if (!Solution)
            {
                return string.Format("Point\t{0}\t{1}\tBlue", X, Y).Replace('.', ','); ;
            }
            else
            {
                return string.Format("Point\t{0}\t{1}\tred", X, Y).Replace('.', ','); ;
            }
        }
    }
}
