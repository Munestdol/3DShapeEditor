using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidСone
{
    class GetValues
    {
        static public List<Points> Points = new List<Points>();
        static public List<Edges> Edges = new List<Edges>();
        static public List<Faces> Faces = new List<Faces>();
        static public List<double> Intensity = new List<double>();

        public void BuildModel(int n, double r1, double r2, double h1, double h2)
        {
            Points.Clear();
            Edges.Clear();
            Faces.Clear();
            List<int> faces = new List<int>();
            double deltaAlpha = Convert.ToInt32(360 / n); ;
            int count;
            double[] alphaN = new double[n];
            alphaN[0] = 0;
            for (int i = 0; i < n; i++)
            {
                if (i > 0)
                    alphaN[i] = alphaN[i - 1] + deltaAlpha;
                Points.Add(new Points(r1 * Math.Cos(alphaN[i] * Math.PI / 180), 0, r1 * Math.Sin(alphaN[i] * Math.PI / 180), 1));
                Points.Add(new Points(0, h1, 0, 1));
            }
            count = Points.Count - 1;        
            for(int i=0; i + 1 < count;i++)
            {
                if(i + 2 >= count)
                {
                    Edges.Add(new Edges(i + 1, 0));
                    Edges.Add(new Edges(i + 2, 0));
                }
                Edges.Add(new Edges(i, i + 1));
                Edges.Add(new Edges(i, i + 2));
             
            }

            Points.Add(new Points(r2 * Math.Cos(150 * Math.PI / 180),0, r2 * Math.Sin(-150 * Math.PI / 180), 1));
            int n0 = Points.Count - 1;
            Points.Add(new Points(0, 0, r2 * Math.Sin(90),1));
            int n1 = Points.Count - 1;
            Points.Add(new Points(r2 * Math.Cos(30 * Math.PI / 180), 0, r2 * Math.Sin(-150 * Math.PI / 180), 1));
            int n2 = Points.Count - 1;

            Points.Add(new Points(0,-h2,0,1));
            int n3 = Points.Count - 1;

            Edges.Add(new Edges(n0,n1));
            Edges.Add(new Edges(n1, n2));
            Edges.Add(new Edges(n2, n0));

            Edges.Add(new Edges(n0, n3));
            Edges.Add(new Edges(n1, n3));
            Edges.Add(new Edges(n2, n3));
           
            Faces.Add(new Faces(new List<int>() { n0, n2, n3 }));
            Faces.Add(new Faces(new List<int>() { n0, n1, n3 }));
            Faces.Add(new Faces(new List<int>() { n0, n1, n2 }));
            Faces.Add(new Faces(new List<int>() { n1, n2, n3 })); 
            for(int i = 0; i+2 <= Points.Count-5; i++)
            {
                if(i + 2 == Points.Count - 5)
                    Faces.Add(new Faces(new List<int>() { i + 1, i + 2, 0 }));
                Faces.Add(new Faces(new List<int>() { i, i + 1, i + 2 }));
            }
        }
    }
}
