﻿namespace BoredMatrix;

public class CodeStream(int sx, int sy)
{
    public int StartX = sx;
    public int StartY = sy;

    public int HeadY = sy;
    public bool IsFinished = false;
    public int TrailCount = 16;
    public int Generation = 0;

    public char[] Chars;
    internal void GenerateStringContent()
    {
        Chars = Program.RandomString(TrailCount).ToCharArray();
    }

    internal void SwitchRandomChar()
    {
        for (int q = 0; q < Math.Max(0, TrailCount); q++)
        {
            if (DateTime.Now >= NextCharSwitch && TrailCount > 0)
            {
                var i = Program.Random.Next(0, TrailCount);
                Chars[i] = (char)Program.Random.Next(33, 126);
                NextCharSwitch = DateTime.Now.AddMilliseconds(Program.Random.Next(0, 20));
            }
        }
    }

    private DateTime NextCharSwitch = DateTime.Now;
}
