using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidСone
{
    public class Points
    {
        public double X;
        public double Y;
        public double Z;
        public double W;
        public Points() { }
        public Points(double p1, double p2, double p3, double p4)
        {
            X = p1;
            Y = p2;
            Z = p3;
            W = p4;
        }
        public Points(Points p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            W = p.W;
        }

        public double[,] ToMatr()
        {
            double[,] Rez = new double[1, 4];
            Rez[0, 0] = X;
            Rez[0, 1] = Y;
            Rez[0, 2] = Z;
            Rez[0, 3] = W;
            return Rez;
        }
    }
}
