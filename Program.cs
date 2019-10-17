using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Cursor = System.Windows.Forms.Cursor;

namespace GameAssistant
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        

        

        public static async Task<bool> StartTask()
        {
            var sprinting = false;
            while (true)
            {
                //0x7B5922 or rgb(123, 89, 34) at xy(187, 78)
                var stamloc = new Point(197, 78);
                var c = PixelDetection.Pixels.GetColorAt(stamloc);
                var highstam = (c.R == 123 && c.G == 89 && c.B == 34);

                if (Keyboard.IsKeyDown(Key.W) && highstam && !sprinting)
                {
                    sprinting = true;
                    Controls.Shift.ShiftDown();
                }

                if ((Keyboard.IsKeyUp(Key.W) || !highstam) && sprinting)
                {
                    sprinting = false;
                    Controls.Shift.ShiftUp();
                }

                await Task.Delay(Form1.update_frequency);
            }
        }

        
        public static async Task<bool> SnipeAH()
        {
            var timer = new Stopwatch();
            var AHrunning = false;
            var snipepoint = new Point();
            while (true)
            {

                if (Keyboard.IsKeyDown(Key.Insert))
                {
                    timer.Start();
                    AHrunning = true;
                    snipepoint = Cursor.Position;
                    await Task.Delay(Form1.update_frequency);
                }

                if (AHrunning && Keyboard.IsKeyDown(Key.Insert))
                {
                    timer.Stop();
                    AHrunning = false;
                    await Task.Delay(Form1.update_frequency);
                    MessageBox.Show("Stopped");
                }

                var c2 = PixelDetection.Pixels.GetColorAt(snipepoint);
                var black = (c2.R == 0 && c2.G == 0 && c2.B == 0);
                if (AHrunning && !black)
                {
                    timer.Restart();
                    if (Cursor.Position != snipepoint)
                        Cursor.Position = snipepoint;
                        
                    SendKeys.Send("4");
                }

                if (timer.ElapsedMilliseconds > 60000)
                {
                    timer.Restart();
                    SendKeys.Send("4");
                    await Task.Delay(Form1.update_frequency);
                    Movement.Mover.randomove();
                }
                    

                Random random = new Random();
                await Task.Delay(Form1.update_frequency + random.Next(-20, 20));
            }            
        }

        public static async Task<bool> StopTask()
        {            
            Application.Exit();
            await Task.Delay(10);
            return true;
        }
    }
}
