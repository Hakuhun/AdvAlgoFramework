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

        public TaboSearchSmallestBoundaryPolygon(string filename)
        {
            problem = new SmallestBoundaryPoligonProblem("Points.txt");
        }

        void Calc(List<Point> S, Func<Point, Point, Point, double> ds,
            float ε, Func<List<Point>, double> f, Func<Boolean> stopCondtion )
        {
            List<Point> pOpt = null;
            while (!stopCondtion())
            {
                //p <- rnd - S 
                problem.Solution = problem.GenerateRandomPoints(5);
                SetTabuBarrier();
                bool stuck = false;

                while (!stuck && !IsTabu(problem.Solution) && !StopCondition())
                {
                    if (pOpt == null || f(problem.Solution) < f(pOpt))
                    {
                        pOpt = problem.Solution;
                    }
                    AddTabu(problem.Solution);
                    var q = new List<Point>();

                    //q <-min(f(x)) {x E S | ds(x,p) = epszilon} , most az epszilon 10
                    //cica


                }

            }
            
        }

        //List<Point> ShiftingPoint(List<Point> solution, float ε)
        //{

        //}

        /// <summary>
        /// A Tabu listához hozzáadja az aktuális iterációt.
        /// </summary>
        void AddTabu(List<Point> p)
        {
            tabus.Add(p);
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
            if (iteration > 500)
            {
                return true;
            }
            return false;
        }
    }
}
