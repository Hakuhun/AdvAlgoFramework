﻿using HalalFramework.Problem;
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

        public HillClimbingStohacticSmallestBundaryPolygon(string filename)
        {
            problem = new SmallestBoundaryPoligonProblem(filename);
            Calc();
        }
        
        void Calc()
        {
            //p <- rnd - S 
            problem.Solution = problem.GenerateRandomPoints(5);

            List<Point> newSolution;
            int i = 0;

            while (!StopCondition())//***
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


                i++;
            }
        }   

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
