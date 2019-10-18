using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace GameAssistant.Controls
{
    class MouseMove
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }


        private static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        private static double Hypot(double x, double y)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        static int _mouseSpeed;
        public static async void HumanWindMouse(double xs, double ys, double xe, double ye, double gravity, double wind, double targetArea)
        {
            double veloX = 0,
                veloY = 0,
                windX = 0,
                windY = 0;
            
            var msp = _mouseSpeed;
            var sqrt2 = Math.Sqrt(2);
            var sqrt3 = Math.Sqrt(3);
            var sqrt5 = Math.Sqrt(5);
            var rnd = new Random();

            var tDist = (int)Distance(Math.Round(xs), Math.Round(ys), Math.Round(xe), Math.Round(ye));
            var t = (uint)(Environment.TickCount + 10000);

            do
            {
                if (Environment.TickCount > t)
                    break;

                var dist = Hypot(xs - xe, ys - ye);
                wind = Math.Min(wind, dist);

                if (dist < 1)
                    dist = 1;

                var d = (Math.Round(Math.Round((double)tDist) * 0.3) / 7);

                if (d > 25)
                    d = 25;

                if (d < 5)
                    d = 5;

                double rCnc = rnd.Next(6);

                if (rCnc == 1)
                    d = 2;

                double maxStep;

                if (d <= Math.Round(dist))
                    maxStep = d;
                else
                    maxStep = Math.Round(dist);

                if (dist >= targetArea)
                {
                    windX = windX / sqrt3 + (rnd.Next((int)(Math.Round(wind) * 2 + 1)) - wind) / sqrt5;
                    windY = windY / sqrt3 + (rnd.Next((int)(Math.Round(wind) * 2 + 1)) - wind) / sqrt5;
                }
                else
                {
                    windX = windX / sqrt2;
                    windY = windY / sqrt2;
                }

                veloX = veloX + windX;
                veloY = veloY + windY;
                veloX = veloX + gravity * (xe - xs) / dist;
                veloY = veloY + gravity * (ye - ys) / dist;

                if (Hypot(veloX, veloY) > maxStep)
                {
                    var randomDist = maxStep / 2.0 + rnd.Next((int)(Math.Round(maxStep) / 2));
                    var veloMag = Math.Sqrt(veloX * veloX + veloY * veloY);
                    veloX = (veloX / veloMag) * randomDist;
                    veloY = (veloY / veloMag) * randomDist;
                }

                var lastX = (int)Math.Round(xs);
                var lastY = (int)Math.Round(ys);
                xs = xs + veloX;
                ys = ys + veloY;

                if (lastX != Math.Round(xs) || (lastY != Math.Round(ys)))
                    SetCursorPos((int)Math.Round(xs), (int)Math.Round(ys));

                var w = (rnd.Next((int)(Math.Round((double)(100 / msp)))) * 6);

                if (w < 5)
                    w = 5;

                w = (int)Math.Round(w * 0.9);
                await Task.Delay(w);
            } while (!(Hypot(xs - xe, ys - ye) < 1));

            if (Math.Round(xe) != Math.Round(xs) || (Math.Round(ye) != Math.Round(ys)))
                SetCursorPos((int)Math.Round(xe), (int)Math.Round(ye));

            _mouseSpeed = msp;
        }
    }
}
