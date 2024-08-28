using System;
using System.Data;
using System.Text;

namespace BoredMatrix
{
    internal partial class Program
    {
        private static char[,] _buffer;
        private static Random _random = new();

        static void Main(string[] args)
        {
            var w = Console.WindowWidth;
            var h = Console.WindowHeight;

            var spawnDelay = TimeSpan.FromMilliseconds(46);
            var nextSpawn = DateTime.Now.Add(TimeSpan.FromSeconds(2));

            _buffer = new char[w, h];

            List<CodeStream> streams =
            [
                new CodeStream(25,0) {TrailCount = 24 },
            ];

            streams[0].GenerateStringContent();

            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                ClearBuffer(w, h);

                if (DateTime.Now >= nextSpawn)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        var str = new CodeStream(_random.Next(0, w), _random.Next(0, 0))
                        {
                            TrailCount = _random.Next(6, 45)
                        };
                        str.GenerateStringContent();
                        streams.Add(str);
                        nextSpawn = DateTime.Now.Add(spawnDelay);
                    }
                }

                foreach (var stream in streams.ToList())
                {
                    if (stream.TrailCount <= 0)
                    {
                        streams.Remove(stream);
                    }

                    if (stream.HeadY >= h)
                    {
                        stream.HeadY = h - 1;
                        stream.TrailCount--;
                    }
                    else
                    {
                        _buffer[stream.StartX, stream.HeadY] = '0';
                    }
                    for (int i = 0; i < Math.Min(stream.Generation, stream.TrailCount); i++)
                    {
                        var y = stream.HeadY - i;
                        if (y < 0 || y >= h)
                            break;

                        _buffer[stream.StartX, stream.HeadY - i] = stream.Chars[i];
                    }

                    stream.SwitchRandomChar();
                    stream.Generation++;
                    stream.HeadY++;
                }


                Render(w, h);

                Thread.Sleep(16);
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDE FGHIJKLMNOP QRSTUVWXYZ01 23456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private static void ClearBuffer(int w, int h)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    _buffer[x, y] = ' ';
                }
            }
        }

        private static void Render(int w, int h)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.CursorVisible = false;
            for (int y = 0; y < h; y++)
            {
                var row = new StringBuilder();
                for (int x = 0; x < w; x++)
                {
                    row.Append(_buffer[x, y]);
                }
                Console.WriteLine(row.ToString());
            }
        }
    }
}
