using HalalFramework.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    class SimulatedAnnealingSmallestBoundaryPolygon
    {
        SmallestBoundaryPoligonProblem problem;
        int actIteration = 0;
        double T;
        readonly double Kb = 1.380649 * Math.Pow(10, -23);

        public SimulatedAnnealingSmallestBoundaryPolygon(SmallestBoundaryPoligonProblem problem)
        {
            this.problem = problem;
            T = 10000;
            Calc();
        }

        void Calc()
        {
            Console.WriteLine("Smallest Boundary Polygon problem solving with Simulated Annealing");
            //p <-rnd- S
            problem.Solution = problem.GenerateRandomPoints(5);
            //Popt <- p
            var popt = problem.Solution;
            //t <- 1
            int t = 1;

            List<Point> newSolution;

            while (!StopCondition())
            {
                newSolution = problem.GenerateNewPoints();
                var fitness_difference = problem.objective(newSolution) 
                    - problem.objective(problem.Solution);
                if (fitness_difference < 0 && problem.constraint(newSolution))
                {
                    problem.Solution = newSolution;
                    if (problem.objective(problem.Solution) < problem.objective(popt))
                    {
                        popt = problem.Solution;
                        problem.Solution = newSolution;
                    }
                }
                else
                {
                    T = Tempearture(t);
                    var P = Math.Pow(Math.E, -(fitness_difference/(Kb*T)));
                    if (SmallestBoundaryPoligonProblem.RAND.NextDouble() < P && problem.constraint(newSolution)) 
                    {
                        problem.Solution = newSolution;
                    }
                }
                actIteration++;
                //Console.WriteLine(T + Environment.NewLine);
                problem.savePointsToFile("simulatedannealing.txt", actIteration, true);
            }

        }

        private double Tempearture(int t)
        {
            return T *= 1 - 0.003;
        }

        /*A T állandóan csökken*/
        bool StopCondition()
        {
            return T<1;
        }

    }
}
