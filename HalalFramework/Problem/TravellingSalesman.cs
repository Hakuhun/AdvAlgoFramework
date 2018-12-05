using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalalFramework.Problem
{
    class TravellingSalesman
    {
        protected List<Town> towns;
        
        public float objective(List<Town> route)
        {
            float sum_length = 0;

            for (int ti = 0; ti < route.Count- 1; ti++)
            {
                Town t1 = route[ti];
                Town t2 = route[ti + 1];
                //sum_length += sqrt(pow(t1.x - t2.x, 2) + pow(t1.y - t2.y, 2));
                //sum_length += Math.Sqrt(Math.Pow());
            }
            return sum_length;
        }
    }

    class Town
    {
        private float x, y;

        public float X { get => x; set => x = value; }
        public float Y { get => y; set => y = value; }
    }
}
