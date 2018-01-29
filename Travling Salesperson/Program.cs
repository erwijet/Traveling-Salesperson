using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace Travling_Salesperson
{
    public static class ReturnData
    {
        public static Double STARTDISTANCE = 0;
        public static Double BESTFOUNDDISTANCE = 0;
    }

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int freq = 0;
            int hz = 0;
            Size s = new Size(0, 0);
            string PointHex = "";
            string BestPathHex = "";
            string CurrentPathHex = "";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.WriteLine("Travling Salesperson - v0.1");
            while (true)
            {
                Console.Write("Point frequency: ");
                freq = int.Parse(Console.ReadLine());
                Console.Write("Color hex for points: #");
                PointHex = "#" + Console.ReadLine().ToLower();
                Console.Write("Color for path: #");
                CurrentPathHex = "#" + Console.ReadLine().ToLower();
                Console.Write("Color for optimum path: #");
                BestPathHex = "#" + Console.ReadLine().ToLower();
                Console.Write("Update speed (in ms): ");
                hz = int.Parse(Console.ReadLine());

                Console.Write("Window width: ");
                s.Width = int.Parse(Console.ReadLine());
                Console.Write("Window height: ");
                s.Height = int.Parse(Console.ReadLine());

                Console.WriteLine("\n\n");
                Console.WriteLine("Confirm Data:");
                Console.WriteLine("Vertex number: {0}\nPoint color: {1}\nTesting path color: {2}\nOptimum path color: {3}\n\nRefresh rate: {4}ms\nSize: {5}x{6}", freq.ToString(), PointHex, CurrentPathHex, BestPathHex, hz.ToString(), s.Width, s.Height);
                Console.Write("Is the information above correct? (y/n) ");
                if (Console.ReadKey(true).KeyChar == 'y')
                {
                    Application.Run(new Form1(freq, PointHex, CurrentPathHex, BestPathHex, hz, s));
                    Console.WriteLine();
                   Console.WriteLine("Task terminated: \nStarting distance: {0}\nBest found distance: {1}\n", ReturnData.STARTDISTANCE, ReturnData.BESTFOUNDDISTANCE);
                }
                else
                {
                    Console.Clear();
                }
            }
        }
    }
}
