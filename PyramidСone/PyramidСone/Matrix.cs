using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidСone
{
    class Matrix
    {
        static public List<Points> Action(double[,] M, List<Points> list)
        {
            double[,] buff;

            for (int i = 0; i < list.Count; i++)
            {
                buff = Multiply(list[i].ToMatr(), M);
                list[i].X = buff[0, 0];
                list[i].Y = buff[0, 1];
                list[i].Z = buff[0, 2];
                list[i].W = buff[0, 3];
            }
            return list;
        }

        static public double[,] Multiply(double[,] M1, double[,] M2)
        {
            double[,] Rez = new double[M1.GetLength(0), M2.GetLength(1)];

            for (int i = 0; i < M1.GetLength(0); i++)
            {
                for (int j = 0; j < M2.GetLength(1); j++)
                {
                    for (int k = 0; k < M2.GetLength(0); k++)
                    {
                        Rez[i, j] += M1[i, k] * M2[k, j];
                    }
                }
            }
            return Rez;
        }

        static public double[,] Movement(double dx, double dy, double dz)
        {
            var matrix = new[,]
            {
                { 1d, 0d, 0d, 0d },
                { 0d, 1d, 0d, 0d },
                { 0d, 0d, 1d, 0d },
                { dx, dy, dz, 1d }
            };
            return matrix;
        }

        static public double[,] Scale(double Sx, double Sy, double Sz)
        {
            var matrix = new[,]
            {
                { Sx, 0d, 0d, 0d },
                { 0d, Sy, 0d, 0d },
                { 0d, 0d, Sz, 0d },
                { 0d, 0d, 0d, 1d }
            };
            return matrix;
        }

        static public double[,] RotateZ(double angle)
        {
            if (angle < 0) angle += 360;
            var matrix = new[,]
            {
                {Math.Cos(angle * Math.PI / 180d), Math.Sin(angle * Math.PI / 180d), 0d, 0d},
                {-Math.Sin(angle * Math.PI / 180d), Math.Cos(angle * Math.PI / 180d), 0d, 0d},
                {0d, 0d, 1d, 0d},
                {0d, 0d, 0d, 1d}
            };
            return matrix;
        }

        static public double[,] RotateX(double angle)
        {
            if (angle < 0) angle += 360;
            var matrix = new[,]
            {
                {1d, 0d, 0d, 0d},
                {0d, Math.Cos(angle * Math.PI / 180d), Math.Sin(angle * Math.PI / 180d), 0d},
                {0d, -Math.Sin(angle * Math.PI / 180d), Math.Cos(angle * Math.PI / 180d), 0d},
                {0d, 0d, 0d, 1d}
            };
            return matrix;
        }

        static public double[,] RotateY(double angle)
        {
            if (angle < 0) angle += 360;
            var matrix = new[,]
            {
                {Math.Cos(angle * Math.PI / 180d), 0d, -Math.Sin(angle * Math.PI / 180d), 0d},
                {0d, 1d, 0d, 0d},
                {Math.Sin(angle * Math.PI / 180d), 0d, Math.Cos(angle * Math.PI / 180d), 0d},
                {0d, 0d, 0d, 1d}
            };
            return matrix;
        }
        static public double[,] Oblique(double a, double L)
        {
            var matrix = new[,]
            {
                {1d, 0d, 0d, 0d},
                {0d, 1d, 0d, 0d},
                {Math.Cos(a * Math.PI / 180d) * L, Math.Sin(a * Math.PI / 180d) * L, 1d, 0d},
                {0d, 0d, 0d, 1d}
            };
            return matrix;
        }

        static public double[,] Frontal()
        {
            var matrix = new[,]
            {
                { 1d, 0d, 0d, 0d },
                { 0d, 1d, 0d, 0d },
                { 0d, 0d, 0d, 0d },
                { 0d, 0d, 0d, 1d }
            };
            return matrix;
        }

        static public double[,] Horizontal()
        {
            var matrix = new[,]
            {
                { 1d, 0d, 0d, 0d },
                { 0d, 0d, 0d, 0d },
                { 0d, 0d, 1d, 0d },
                { 0d, 0d, 0d, 1d }
            };
            return matrix;
        }

        static public double[,] Profile()
        {
            var matrix = new[,]
            {
                { 0d, 0d, 0d, 0d },
                { 0d, 1d, 0d, 0d },
                { 0d, 0d, 1d, 0d },
                { 0d, 0d, 0d, 1d }
            };
            return matrix;
        }

        static public double[,] Perspective(double tetta,double fi,double p)
        {
            var matrix = new[,]
            {
                {-1*(float)Math.Sin(tetta*Math.PI/180),-1*(float)Math.Cos(fi*Math.PI/180)*(float)Math.Cos(tetta*Math.PI/180),-1*(float)Math.Sin(fi*Math.PI/180)*(float)Math.Cos(tetta*Math.PI/180),0 },
                {(float)Math.Cos(tetta*Math.PI/180),-1*(float)Math.Cos(fi*Math.PI/180)*(float)Math.Sin(tetta*Math.PI/180),-1*(float)Math.Sin(fi*Math.PI/180)*(float)Math.Cos(tetta*Math.PI/180),0 },
                {0,(float)Math.Sin(fi*Math.PI/180),-1*(float)Math.Cos(fi*Math.PI/180),0 },
                {0,0,p,1 }

            };
            return matrix;
        }
    }
}
