using HalalFramework.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    class TaboSearchSmallestBoundaryPolygon
    {
        SmallestBoundaryPoligonProblem problem;
        int iteration = 0;
        List<List<Point>> tabus = new List<List<Point>>();
        List<Point> tabu2 = new List<Point>();
        public TaboSearchSmallestBoundaryPolygon(SmallestBoundaryPoligonProblem problem)
        {
            this.problem = problem;
            Calc(15, problem.objective, StopCondition);
        }

        void Calc(float ε, Func<List<Point>,  double> f, Func<bool> stopCondtion )
        {
            Console.WriteLine("Smallest Boundary Polygon problem solving with Tabu");
            List<Point> pOpt = null;
            while (!stopCondtion())
            {
                //p <- rnd - S 
                problem.Solution = problem.GenerateRandomPoints(5);
                SetTabuBarrier();
                bool stuck = false;

                while (!stuck && !IsTabu2(problem.Solution) && !StopCondition() && problem.constraint(problem.Solution))
                {
                    if (pOpt == null || f(problem.Solution) < f(pOpt))
                    {
                        pOpt = problem.Solution;
                    }
                    AddTabu(problem.Solution);
                    var q = GetBestShiftForEpsilon(problem.Solution, ε);
                    if (f(q) < f(problem.Solution))
                        problem.Solution = q;
                    else
                        stuck = true;
                }
                iteration++;
                problem.savePointsToFile("taboosearch.txt", iteration, true);
            }
        }

        /// <summary>
        /// gyak Fitnesz függvény
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="ε"></param>
        /// <returns></returns>
        List<Point> GetBestShiftForEpsilon(List<Point> solution,  float ε)
        {
            List<Point> shifted = new List<Point>(solution);
            double[,] directions = new double[9, 2] { 
                { 0, 0 }, 
                { 0, ε },
                { 0, -ε }, 
                { ε, 0 }, 
                { -ε, 0 }, 
                { ε, ε }, 
                { -ε, -ε }, 
                { ε, -ε }, 
                { -ε, ε }
            };
            foreach (Point pnt in solution)
            {
                Point possibleBest = null;
                for (int i = 0; i < directions.GetLength(0); i++)
                {
                    double shiftedX = pnt.X + directions[i, 0];
                    double shiftedY = pnt.Y + directions[i, 1];
                    possibleBest = new Point() { X = shiftedX, Y = shiftedY, Solution = true};

                    int index = solution.IndexOf(pnt);
                    Point old = shifted[index];
                    shifted[index] = possibleBest;

                    if (!(problem.objective(shifted) <= problem.objective(solution) && problem.constraint(shifted)))
                    {
                        shifted[index] = old;
                    }
                }
            }
            return shifted;
        }

        /// <summary>
        /// A Tabu listához hozzáadja az aktuális iterációt.
        /// </summary>
        void AddTabu(List<Point> p)
        {
            tabus.Add(p);
            foreach (var item in p)
            {
                tabu2.Add(item);
            }
        }

        /// <summary>
        /// Eleme már a tabu 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool IsTabu(List<Point> p)
        {
            return tabus.Contains(p);
        }

        bool IsTabu2(List<Point> p)
        {
            foreach (var item in p)
            {
                if (tabu2.Contains(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Ne foglalkozzon az előző keresés pontjaival.
        /// </summary>
        /// <returns></returns>
        bool SetTabuBarrier()
        {
            return true;
        }

        /// <summary>
        /// A már megtalált utak egy részegységének bejárása helyett.
        /// </summary>
        void PurgeTabu(List<Point> p) { }

        bool StopCondition()
        {
            if (iteration > 10000)
            {
                return true;
            }
            return false;
        }
    }
}
