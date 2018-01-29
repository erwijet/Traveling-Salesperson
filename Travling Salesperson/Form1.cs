using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Travling_Salesperson
{
    public partial class Form1 : Form
    {
        public int c;
        public double BestDist;
        public Point[] v;
        public Point[] b;
        public double startDistance = 0.0f;
        public Pen PointPen = new Pen(Brushes.White);
        public Pen CurrentPathPen = new Pen(Brushes.White);
        public Pen BestPathPen = new Pen(Brushes.Blue, 5.0f);
        public Graphics GraphicsObject;

        public Form1(int points, string PointColorHex, string CurrentPathColorHex, string BestPathColorHex, int RefreshRate, Size ws)
        {
            InitializeComponent();
            this.Size = ws;
            PointPen.Color = System.Drawing.ColorTranslator.FromHtml(PointColorHex);
            BestPathPen.Color = System.Drawing.ColorTranslator.FromHtml(BestPathColorHex);
            CurrentPathPen.Color = System.Drawing.ColorTranslator.FromHtml(CurrentPathColorHex);
            c = points;
            ReloadPoints();
            BeginRender();
            t.Interval = RefreshRate;
            GraphicsObject = CreateGraphics();

            FormClosing += BeforeFormCloses;
        }

        private void BeforeFormCloses(object sender, FormClosingEventArgs e)
        {
            Travling_Salesperson.ReturnData.BESTFOUNDDISTANCE = BestDist;
            Travling_Salesperson.ReturnData.STARTDISTANCE = startDistance;
        }

        public void RenderPoints(Point[] vals, Pen p, Graphics g)
        {
            foreach (Point thisP in vals)
            {
                g.FillEllipse(PointPen.Brush, thisP.X - 10, thisP.Y - 10, 20, 20);
            }
        }

        public void RenderLines(Point[] vals, Pen p, Graphics g)
        {
            for (int i = 0; i < vals.Length - 1; i++)
            {
                g.DrawLine(p, vals[i], vals[i + 1]);
            }
        }

        public Point[] GetPoints(int count, Random r)
        {
            Point[] vals = new Point[count];
            for (int i = 0; i < count; i++)
            {
                vals[i] = new Point(r.Next(Width - 50), r.Next(Height - 50));
            }
            return vals;
        }

        public void SwapPoints(int loc1, int loc2)
        {
            Point temp = v[loc1];
            v[loc1] = v[loc2];
            v[loc2] = temp;
        }

        public void DoShuffle()
        {
            Random r = new Random();
            SwapPoints(r.Next(c), r.Next(c));
        }

        private void BeginRender()
        {
            t.Start();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            DoShuffle();
            RenderPoints(v, new Pen(Brushes.Red), GraphicsObject);
            GraphicsObject.Clear(Color.Black);
            RenderLines(v, CurrentPathPen, GraphicsObject);
            RenderLines(b, BestPathPen, GraphicsObject);
            RenderPoints(v, new Pen(Brushes.Red), GraphicsObject);
            CrunchNumbers();
        }

        public static double GetDist(Point[] path)
        {
            double sum = 0.0000;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Point p1 = path[i];
                Point p2 = path[i + 1];

                double x1 = p1.X;
                double y1 = p1.Y;
                double x2 = p2.X;
                double y2 = p2.Y;

                double ThisDist = Math.Sqrt(((x1 - x2) * (x1 - x2) + (y1 - y1) * (y1 - y2)));

                sum += ThisDist;
            }
            return sum;
        }

        public void SavePath()
        {
            for (int i = 0; i < c; i++)
            {
                b[i] = v[i];
            }
        }

        public void CrunchNumbers()
        {
            double ThisDist = GetDist(v);
            if (ThisDist < BestDist)
            {
                BestDist = ThisDist;
                SavePath();
            }
        }

        private void ReloadPoints()
        {
            v = GetPoints(c, new Random());
            b = new Point[c];
            BestDist = GetDist(v);
            startDistance = GetDist(v);
            SavePath();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
