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
            ImageSegmentationCluster isc = new ImageSegmentationCluster("cica.bmp",5);
        }
    }
}
