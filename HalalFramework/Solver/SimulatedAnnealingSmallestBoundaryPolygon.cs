using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Solver
{
    class SimulatedAnnealingSmallestBoundaryPolygon
    {

        void Calc()
        {
            //while (T > 0)//*** amig !stopcondition()
            //{
            //    //q <-rnd- {x E S | ds(x,p) = epszilon} , most az epszilon 2
            //    foreach (Point p in solution)
            //    {
            //        solution2.Add(new Point(p.X + GenerateRandom(2), p.Y + GenerateRandom(2)));
            //    }

            //    float deltaE = Objective(solution2) - Objective(solution); // f(gpm(q)) - f(gpm(p))

            //    if (deltaE < 0)//***
            //    {
            //        solution = solution2; //p <- q

            //        if (Objective(solution2) < Objective(solution))//f(gpm(p))<f(gpm(popt))
            //        {
            //            solution = solution2; //popt <- p
            //        }
            //    }
            //    else
            //    {
            //        T = Temperature(t); //T <- Temperature(t)
            //        double P = 100 * Math.Pow(e, -(deltaE / Kb * T));//*** a szazzal szorzas csak az r.next miatt kell

            //        if (r.Next(0, 101) < P)// ha RNDu(0,1)<P
            //        {
            //            solution = solution2; //p <- q
            //        }
            //    }

            //    SaveToLogFile(logfilename);
            //}

        }

    }
}
