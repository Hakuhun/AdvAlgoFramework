using HalalFramework.Problem;
using HalalFramework.Solver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            //ImageSegmentationCluster isc = new ImageSegmentationCluster("slide_sample.bmp",3);

            SmallestBoundaryPoligonProblem problem = SmallestBoundaryPoligonProblem.getInstance("Points.txt");

            //HillClimbingStohacticSmallestBundaryPolygon sphbhc = new HillClimbingStohacticSmallestBundaryPolygon(problem);

            //SimulatedAnnealingSmallestBoundaryPolygon simu = new SimulatedAnnealingSmallestBoundaryPolygon(problem);

            TaboSearchSmallestBoundaryPolygon tabu = new TaboSearchSmallestBoundaryPolygon(problem);
        }
    }
}
