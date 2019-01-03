using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlasma
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.CursorVisible = false;

            var frameSize = new Size();
            int tick = DateTime.Now.Millisecond;

            while (!Console.KeyAvailable)
            {
                frameSize = Frame(frameSize, tick);
                tick++;

                await Task.Delay(100);
            }

            Console.ResetColor();
            Console.CursorVisible = true;
        }

        private static Size Frame(Size oldSize, int tick)
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            if (oldSize.Width != width || oldSize.Height != height)
            {
                Console.Clear();
            }

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.Black;

            var sb = new StringBuilder();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Skip the very last character in the frame to prevent the console from scrolling.
                    if (y == height - 1 && x == width - 1)
                    {
                        continue;
                    }

                    tick = tick == 0 ? 1 : tick;

                    double a = x / (height / 16.0);
                    double b = y / (width / 128.0);
                    
                    double c = (x - width / 2.0) * (x - width / 2.0);
                    double d = (y - height / 2.0) * (y - height / 2.0);

                    double t = Math.Sin(tick / 8.0);

                    double e = Math.Sin(((tick * 2.0) + ((t - 0.5) * a)) / 8.0);
                    double f = Math.Sin(((tick / 2.0) + b) / 8.0);
                    double g = Math.Sin(Math.Sqrt(c + d * 1.1) / (t + 4.0) * 0.1);

                    double intensity = (e + f + g) / 3.0;

                    sb.Append(Character(intensity));
                }
            }

            Console.Write(sb.ToString());

            return new Size(width, height);
        }

        private static char Character(double d)
        {
            int i = (int)Math.Round(d * 2) + 2;

            switch (i)
            {
                case 0:
                    return ' ';         // 0%
                case 1:
                    return '\u2591';    // 25%
                case 2:
                    return '\u2592';    // 50%
                case 3:
                    return '\u2593';    // 75%
                case 4:
                    return '\u2588';    // 100%
                default:
                    System.Diagnostics.Debug.WriteLine(i);
                    return '?';
            }
        }
    }
}
