using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidСone
{
    class Faces
    {
        public List<int> F = new List<int>();
        public Faces() { }
        public Faces(List<int> L)
        {
            F = new List<int>(L);
        }
        public Point[] ToArray(List<Points> P, int x, int y)
        {
            Point[] Arr = new Point[F.Count];
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr[i].X = x + Convert.ToInt32(Math.Round(P[F[i]].X));
                Arr[i].Y = y - Convert.ToInt32(Math.Round(P[F[i]].Y));
            }
            return Arr;
        }
    }
}
