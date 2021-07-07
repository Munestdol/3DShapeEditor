using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PyramidСone
{
    public partial class Form1 : Form
    {
        double x, y = 0;
        public Form1()
        {
            InitializeComponent();
            x = pictureBox1.Width / 2;
            y = pictureBox1.Height / 2;
        }


        public void Draw()
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.FromArgb(205,90,0), 2);
            g.Clear(Color.FromArgb(44, 48, 55));

            List<Points> points = Projections();

            for (int i = 0; i < GetValues.Edges.Count; i++)
            {
                g.DrawLine(pen,
                   Convert.ToInt32(Math.Round(x + points[GetValues.Edges[i].P1].X)),
                   Convert.ToInt32(Math.Round(y - points[GetValues.Edges[i].P1].Y)),
                   Convert.ToInt32(Math.Round(x + points[GetValues.Edges[i].P2].X)),
                   Convert.ToInt32(Math.Round(y - points[GetValues.Edges[i].P2].Y))
                   );
            }
        }

        public List<Points> Projections()
        {
            double[,] M;

            List<Points> points = new List<Points>();
            foreach (Points p in GetValues.Points)
            {
                points.Add(new Points(p));
            }    

            if (rbF.Checked)
            {
                M = Matrix.RotateX(0);
                points = Matrix.Action(M, points);
            }
            if (rbP.Checked)
            {
                M = Matrix.RotateY(90);
                points = Matrix.Action(M, points);
            }
            if (rbH.Checked)
            {
                M = Matrix.RotateX(270);
                points = Matrix.Action(M, points);
            }
            if (rbPerspective.Checked)
            {
                double[,] buff;
                double d = Convert.ToDouble(tbD.Text);
                double p = Convert.ToDouble(tbRo.Text);
                M = Matrix.Multiply(Matrix.RotateZ(Convert.ToDouble(tbTeta.Text)), Matrix.RotateX(Convert.ToDouble(tbFi.Text)));

                for (int i = 0; i < points.Count; i++)
                {
                    buff = Matrix.Multiply(points[i].ToMatr(), M);
                    buff[0, 2] = buff[0, 2] + p;
                    if((buff[0,2] < 0.001d) && (buff[0, 2] >= 0)) { buff[0, 2] = 0.001d; }
                    if ((buff[0, 2] > -0.001d) && (buff[0, 2] < 0)) { buff[0, 2] = -0.001d; }
                    points[i].X = buff[0, 0] / (buff[0, 2] / d);
                    points[i].Y = buff[0, 1] / (buff[0, 2] / d);
                    points[i].Z = buff[0, 2];
                    points[i].W = 1;
                }               
            }
            if (rbAxonometric.Checked)
            {
                M = Matrix.Multiply(Matrix.RotateY(Convert.ToDouble(tbPsi.Text)), Matrix.RotateX(Convert.ToDouble(tbFia.Text)));
                points = Matrix.Action(M, points);
            }
            if (rbOblique.Checked)
            {
                M = Matrix.Oblique(Convert.ToDouble(tbAlpha.Text), Convert.ToDouble(tbL.Text));
                points = Matrix.Action(M, points);
            }

            return points;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double[,] S = Matrix.Scale(Convert.ToDouble(tbSx.Text),
                Convert.ToDouble(tbSy.Text),
                Convert.ToDouble(tbSz.Text));

            GetValues.Points = Matrix.Action(S, GetValues.Points);
            GDraw();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[,] Rx = Matrix.RotateX(Convert.ToDouble(tbA.Text));
            double[,] Ry = Matrix.RotateY(Convert.ToDouble(tbA.Text));
            double[,] Rz = Matrix.RotateZ(Convert.ToDouble(tbA.Text));

            if (rbX.Checked==true)
                GetValues.Points = Matrix.Action(Rx, GetValues.Points);
            if (rbY.Checked == true)
                GetValues.Points = Matrix.Action(Ry, GetValues.Points);
            if (rbZ.Checked == true)
                GetValues.Points = Matrix.Action(Rz, GetValues.Points);

            GDraw();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[,] M = Matrix.Movement(Convert.ToDouble(tbMx.Text), 
                Convert.ToDouble(tbMy.Text), 
                Convert.ToDouble(tbMz.Text));

            GetValues.Points = Matrix.Action(M, GetValues.Points);

            GDraw();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GDraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetValues m = new GetValues();
            m.BuildModel(Convert.ToInt32(tbN.Text), Convert.ToInt32(tbR1.Text), Convert.ToInt32(tbR2.Text), Convert.ToInt32(tbH1.Text), Convert.ToInt32(tbH2.Text));
            rbF.Select();
            Draw();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GDraw();
        }

        private void GDraw()
        {
            if (cbP.Checked)
                DrawFaces();
            else
                Draw();
        }

        private void rbF_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void DrawFaces()
        {
            Graphics g = pictureBox1.CreateGraphics();
            Pen pen = new Pen(Color.FromArgb(205, 90, 0), 1);
            g.Clear(Color.FromArgb(44, 48, 55));
            Brush br = new SolidBrush(Color.White);   
            List<Points> localPoints = Projections();
            double[] dist = new double[GetValues.Faces.Count];
            int[] num = new int[GetValues.Faces.Count];

            if (cbL.Checked)
            {
                Light(localPoints);
            }
            double Ia = Convert.ToDouble(tbIo.Text);
            double IL = Convert.ToDouble(tbI.Text);
            int R, G, B;

            for (int f = 0; f < GetValues.Faces.Count; f++)
            {
                num[f] = f;
                for (int i = 0; i < GetValues.Faces[f].F.Count; i++)
                {
                    dist[f] += localPoints[GetValues.Faces[f].F[i]].Z;
                }
                dist[f] = dist[f] / GetValues.Faces[f].F.Count;
            }
            double buff;
            int buff_i;
            for (int i = 0; i < dist.Length; i++)
            {
                for (int j = i; j < dist.Length; j++)
                {
                    if (dist[j] > dist[i])
                    {
                        buff = dist[i];
                        dist[i] = dist[j];
                        dist[j] = buff;

                        buff_i = num[i];
                        num[i] = num[j];
                        num[j] = buff_i;
                    }
                }
            }
            Point[] Arr;
            int n0 = 0;
            int n1 = num.Length;
            if ((rbPerspective.Checked) && (Convert.ToDouble(tbRo.Text) < 0))
            {
                n0 = -num.Length;
                n1 = 0;
            }
            for (int nn = n0; nn < n1; nn++)
            {
                int n = Math.Abs(nn);
                if ((rbPerspective.Checked) && (Convert.ToDouble(tbRo.Text) < 0))
                {
                    n--;
                }
                Arr = GetValues.Faces[num[n]].ToArray(localPoints, Convert.ToInt32(x), Convert.ToInt32(y));
                if (cbL.Checked)
                {
                    R = Color.White.R;
                    G = Color.White.G;
                    B = Color.White.B;
                    if (rbLR.Checked)
                    {
                        R = Color.DarkRed.R;
                        G = Color.DarkRed.G;
                        B = Color.DarkRed.B;
                    }
                    if (rbLY.Checked)
                    {
                        R = Color.Yellow.R;
                        G = Color.Yellow.G;
                        B = Color.Yellow.B;
                    }
                    if (rbLW.Checked)
                    {
                        R = Color.White.R;
                        G = Color.White.G;
                        B = Color.White.B;
                    }
                    if (rbLG.Checked)
                    {
                        R = Color.Green.R;
                        G = Color.Green.G;
                        B = Color.Green.B;
                    }                 

                    R = (int)(R * Ia / 100 + R * IL * GetValues.Intensity[num[n]] / 100);
                    R = R < 255 ? R : 255;
                    R = R > 0 ? R : 0;
                    G = (int)(G * Ia / 100 + G * IL * GetValues.Intensity[num[n]] / 100);
                    G = G < 255 ? G : 255;
                    G = G > 0 ? G : 0;
                    B = (int)(B * Ia / 100 + B * IL * GetValues.Intensity[num[n]] / 100);
                    B = B < 255 ? B : 255;
                    B = B > 0 ? B : 0;

                    br = new System.Drawing.SolidBrush(Color.FromArgb(R, G, B));
                }
                g.FillPolygon(br, Arr);
                if (cbR.Checked == false)
                {
                    g.DrawPolygon(pen, Arr);
                }
            }
        }
        public void Light(List<Points> P)
        {
            GetValues.Intensity.Clear();

            double X = Convert.ToDouble(tbLx.Text);
            double Y = Convert.ToDouble(tbLy.Text);
            double Z = Convert.ToDouble(tbLz.Text);

            double x, y, z;
            double x1, y1, z1, x2, y2, z2, x3, y3, z3;
            double x0, y0, z0;
            double xL, yL, zL;
            double modA, modB, cos;

            foreach (Faces face in GetValues.Faces)
            {
                x1 = P[face.F[0]].X; y1 = P[face.F[0]].Y; z1 = P[face.F[0]].Z;
                x2 = P[face.F[1]].X; y2 = P[face.F[1]].Y; z2 = P[face.F[1]].Z;
                x3 = P[face.F[2]].X; y3 = P[face.F[2]].Y; z3 = P[face.F[2]].Z;

                x = y1 * z2 + y2 * z3 + y3 * z1 - y2 * z1 - y3 * z2 - y1 * z3;
                y = z1 * x2 + z2 * x3 + z3 * x1 - z2 * x1 - z3 * x2 - z1 * x3;
                z = x1 * y2 + x2 * y3 + x3 * y1 - x2 * y1 - x3 * y2 - x1 * y3;

                x0 = 0d; y0 = 0d; z0 = 0d;
                for (int i = 0; i < face.F.Count; i++)
                {
                    x0 += P[face.F[i]].X;
                    y0 += P[face.F[i]].Y;
                    z0 += P[face.F[i]].Z;
                }
                x0 = x0 / Convert.ToDouble(face.F.Count);
                y0 = y0 / Convert.ToDouble(face.F.Count);
                z0 = z0 / Convert.ToDouble(face.F.Count);
                xL = X - x0; yL = Y - y0; zL = Z - z0;

                modA = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
                modB = Math.Sqrt(Math.Pow(xL, 2) + Math.Pow(yL, 2) + Math.Pow(zL, 2));

                cos = (x * xL + y * yL + z * zL) / (modA * modB);

                if (cos < 0)
                {
                    cos = 0;
                }

                GetValues.Intensity.Add(cos);
            }
        }
    }
}