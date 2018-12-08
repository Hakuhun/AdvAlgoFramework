using HalalFramework.Problem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    class HillClimbingStohacticSmallestBundaryPolygon
    {

        SmallestBoundaryPoligonProblem problem;
        int iteration = 0;
        bool stopCondition = false;

        public HillClimbingStohacticSmallestBundaryPolygon(string filename)
        {
            problem = new SmallestBoundaryPoligonProblem(filename);
            Calc();
        }
        
        void Calc()
        {
            //p <- rnd - S 
            problem.Solution = problem.GenerateRandomPoints(5);

            var newSolution = new List<Point>();
            int i = 0;

            while (!stopCondition)//***
            {
                newSolution = problem.GenerateNewPoints();

                if ((problem.objective(newSolution) <= problem.objective(problem.Solution))
                    && problem.constraint(newSolution))//*** f(q)<f(p) csak
                {
                    iteration = 0;
                    problem.Solution = newSolution; //p <-q
                }
                else
                {
                    iteration++;
                }

                problem.savePointsToFile("hcsto.txt", i);

                if (iteration > 500)
                {
                    stopCondition = true;
                }
                i++;
            }
        }   
    }
}
