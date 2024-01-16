using System;
using System.Collections.Generic;
using System.Drawing;
using FlaxEngine;

namespace Game;

/// <summary>
/// Helpers Script.
/// </summary>
public class Helpers
{
    public static string ShadeColor(string color, int percent)
    {
        int R = int.Parse(color.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
        int G = int.Parse(color.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
        int B = int.Parse(color.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

        R = (int)Math.Round(R * (100 + percent) / 100.0);
        G = (int)Math.Round(G * (100 + percent) / 100.0);
        B = (int)Math.Round(B * (100 + percent) / 100.0);

        R = (R < 255) ? R : 255;
        G = (G < 255) ? G : 255;
        B = (B < 255) ? B : 255;

        string RR = ((R.ToString("X").Length == 1) ? "0" + R.ToString("X") : R.ToString("X"));
        string GG = ((G.ToString("X").Length == 1) ? "0" + G.ToString("X") : G.ToString("X"));
        string BB = ((B.ToString("X").Length == 1) ? "0" + B.ToString("X") : B.ToString("X"));

        return "#" + RR + GG + BB;
    }
}
